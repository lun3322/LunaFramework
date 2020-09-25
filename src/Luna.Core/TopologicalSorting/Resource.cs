using System.Collections.Generic;

namespace Luna.TopologicalSorting
{
    public class Resource
    {
        private readonly HashSet<OrderedProcess> _users = new HashSet<OrderedProcess>();

        public readonly DependencyGraph Graph;
        public readonly string Name;

        public Resource(DependencyGraph graph, string name)
        {
            Graph = graph;
            Name = name;

            Graph.Add(this);
        }

        public IEnumerable<OrderedProcess> Users => _users;

        public void UsedBy(OrderedProcess process)
        {
            DependencyGraph.CheckGraph(this, process);

            if (_users.Add(process)) process.Requires(this);
        }

        public void UsedBy(params OrderedProcess[] processes)
        {
            UsedBy(processes as IEnumerable<OrderedProcess>);
        }

        public void UsedBy(IEnumerable<OrderedProcess> processes)
        {
            foreach (var process in processes)
                UsedBy(process);
        }

        public override string ToString()
        {
            return "Resource { " + Name + " }";
        }
    }

    public class Resource<T>
    {
        private readonly HashSet<OrderedProcess<T>> _users = new HashSet<OrderedProcess<T>>();
        public readonly DependencyGraph<T> Graph;
        public readonly T Value;

        public Resource(DependencyGraph<T> graph, T value)
        {
            Graph = graph;
            Value = value;

            Graph.Add(this);
        }

        public IEnumerable<OrderedProcess<T>> Users => _users;

        public override string ToString()
        {
            return "Resource { " + Value + " }";
        }

        public void UsedBy(OrderedProcess<T> process)
        {
            DependencyGraph<T>.CheckGraph(this, process);

            if (_users.Add(process)) process.Requires(this);
        }

        public void UsedBy(params OrderedProcess<T>[] processes)
        {
            UsedBy(processes as IEnumerable<OrderedProcess<T>>);
        }

        public void UsedBy(IEnumerable<OrderedProcess<T>> processes)
        {
            foreach (var process in processes) UsedBy(process);
        }
    }
}