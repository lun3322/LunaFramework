using System;
using System.Collections.Generic;
using System.Linq;

namespace Luna.TopologicalSorting
{
    public class DependencyGraph
    {
        private readonly HashSet<OrderedProcess> _processes = new HashSet<OrderedProcess>();

        private readonly HashSet<Resource> _resources = new HashSet<Resource>();

        public TopologicalSort CalculateSort()
        {
            return CalculateSort(new TopologicalSort());
        }

        public TopologicalSort CalculateSort(TopologicalSort instance)
        {
            var unused = new HashSet<OrderedProcess>(_processes);

            do
            {
                var set = new HashSet<OrderedProcess>(
                    unused.Where(p => !unused.Overlaps(p.Predecessors))
                );

                if (set.Count == 0)
                    throw new InvalidOperationException("Cannot order this set of processes");

                unused.ExceptWith(set);

                foreach (var subset in SolveResourceDependencies(set))
                    instance.Append(subset);
            } while (unused.Count > 0);

            return instance;
        }

        private IEnumerable<ISet<OrderedProcess>> SolveResourceDependencies(ISet<OrderedProcess> processes)
        {
            if (_resources.Count == 0 || !processes.SelectMany(p => p.ResourcesSet).Any())
            {
                yield return processes;
            }
            else
            {
                var result = new HashSet<HashSet<OrderedProcess>>();

                foreach (var process in processes)
                {
                    var process1 = process;

                    var agreeableSets = result
                        .Where(set => set
                            .Where(p => p.ResourcesSet.Overlaps(process1.Resources))
                            .IsEmpty());

                    HashSet<OrderedProcess> agreeableSet;

                    if (agreeableSets.IsEmpty())
                    {
                        agreeableSet = new HashSet<OrderedProcess>();
                        result.Add(agreeableSet);
                    }
                    else
                    {
                        agreeableSet = agreeableSets.Aggregate((a, b) => a.Count < b.Count ? a : b);
                    }

                    agreeableSet.Add(process);
                }

                foreach (var set in result)
                    yield return set;
            }
        }

        internal bool Add(OrderedProcess orderedProcess)
        {
            return _processes.Add(orderedProcess);
        }

        internal bool Add(Resource resourceClass)
        {
            return _resources.Add(resourceClass);
        }

        internal static void CheckGraph(OrderedProcess a, OrderedProcess b)
        {
            if (a.Graph != b.Graph) throw new ArgumentException($"process {a} is not associated with the same graph as process {b}");
        }

        internal static void CheckGraph(Resource a, OrderedProcess b)
        {
            if (a.Graph != b.Graph) throw new ArgumentException($"Resource {a} is not associated with the same graph as process {b}");
        }
    }

    public class DependencyGraph<T>
    {
        private readonly HashSet<OrderedProcess<T>> _processes = new HashSet<OrderedProcess<T>>();

        private readonly HashSet<Resource<T>> _resources = new HashSet<Resource<T>>();

        public IEnumerable<OrderedProcess<T>> Processes => _processes;

        public TopologicalSort<T> CalculateSort()
        {
            return CalculateSort(new TopologicalSort<T>());
        }

        public TopologicalSort<T> CalculateSort(TopologicalSort<T> instance)
        {
            var unused = new HashSet<OrderedProcess<T>>(_processes);

            do
            {
                var set = new HashSet<OrderedProcess<T>>(
                    unused.Where(p => !unused.Overlaps(p.Predecessors))
                );

                if (set.Count == 0) throw new InvalidOperationException("Cannot order this set of processes");

                unused.ExceptWith(set);

                foreach (var subset in SolveResourceDependencies(set)) instance.Append(subset);
            } while (unused.Count > 0);

            return instance;
        }

        private IEnumerable<ISet<OrderedProcess<T>>> SolveResourceDependencies(ISet<OrderedProcess<T>> processes)
        {
            if (_resources.Count == 0 || !processes.SelectMany(p => p.ResourcesSet).Any())
            {
                yield return processes;
            }
            else
            {
                var result = new HashSet<HashSet<OrderedProcess<T>>>();
                foreach (var process in processes)
                {
                    var process1 = process;

                    var agreeableSets = result
                        .Where(set => set
                            .Where(p => p.ResourcesSet.Overlaps(process1.Resources))
                            .IsEmpty());

                    HashSet<OrderedProcess<T>> agreeableSet;

                    if (agreeableSets.IsEmpty())
                    {
                        agreeableSet = new HashSet<OrderedProcess<T>>();
                        result.Add(agreeableSet);
                    }
                    else
                    {
                        agreeableSet = agreeableSets.Aggregate((a, b) => a.Count < b.Count ? a : b);
                    }

                    agreeableSet.Add(process);
                }

                foreach (var set in result) yield return set;
            }
        }

        internal bool Add(OrderedProcess<T> orderedProcess)
        {
            return _processes.Add(orderedProcess);
        }

        internal bool Add(Resource<T> resourceClass)
        {
            return _resources.Add(resourceClass);
        }

        internal static void CheckGraph(OrderedProcess<T> a, OrderedProcess<T> b)
        {
            if (a.Graph != b.Graph) throw new ArgumentException($"process {a} is not associated with the same graph as process {b}");
        }

        internal static void CheckGraph(Resource<T> a, OrderedProcess<T> b)
        {
            if (a.Graph != b.Graph) throw new ArgumentException($"Resource {a} is not associated with the same graph as process {b}");
        }
    }
}