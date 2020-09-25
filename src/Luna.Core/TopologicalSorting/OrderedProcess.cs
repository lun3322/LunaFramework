using System.Collections.Generic;

namespace Luna.TopologicalSorting
{
    public class OrderedProcess
    {
        private readonly HashSet<OrderedProcess> _followers = new HashSet<OrderedProcess>();
        private readonly HashSet<OrderedProcess> _predecessors = new HashSet<OrderedProcess>();
        private readonly HashSet<Resource> _resources = new HashSet<Resource>();
        public readonly DependencyGraph Graph;
        public readonly string Name;

        public OrderedProcess(DependencyGraph graph, string name)
        {
            Graph = graph;
            Name = name;

            Graph.Add(this);
        }

        public IEnumerable<OrderedProcess> Predecessors => _predecessors;
        public IEnumerable<OrderedProcess> Followers => _followers;
        public IEnumerable<Resource> Resources => _resources;
        internal ISet<Resource> ResourcesSet => _resources;

        public OrderedProcess Before(OrderedProcess follower)
        {
            DependencyGraph.CheckGraph(this, follower);

            if (_followers.Add(follower))
                follower.After(this);

            return follower;
        }

        public IEnumerable<OrderedProcess> Before(params OrderedProcess[] followers)
        {
            return Before(followers as IEnumerable<OrderedProcess>);
        }

        public IEnumerable<OrderedProcess> Before(IEnumerable<OrderedProcess> followers)
        {
            foreach (var ancestor in followers)
                Before(ancestor);

            return followers;
        }

        public OrderedProcess After(OrderedProcess predecessor)
        {
            DependencyGraph.CheckGraph(this, predecessor);

            if (_predecessors.Add(predecessor))
                predecessor.Before(this);

            return predecessor;
        }

        public IEnumerable<OrderedProcess> After(params OrderedProcess[] predecessors)
        {
            return After(predecessors as IEnumerable<OrderedProcess>);
        }

        public IEnumerable<OrderedProcess> After(IEnumerable<OrderedProcess> predecessors)
        {
            foreach (var predecessor in predecessors)
                After(predecessor);

            return predecessors;
        }

        public void Requires(Resource resource)
        {
            DependencyGraph.CheckGraph(resource, this);

            if (_resources.Add(resource))
                resource.UsedBy(this);
        }

        public override string ToString()
        {
            return "Process { " + Name + " }";
        }
    }

    public class OrderedProcess<T>
    {
        private readonly HashSet<OrderedProcess<T>> _followers = new HashSet<OrderedProcess<T>>();
        private readonly HashSet<OrderedProcess<T>> _predecessors = new HashSet<OrderedProcess<T>>();
        private readonly HashSet<Resource<T>> _resources = new HashSet<Resource<T>>();
        public readonly DependencyGraph<T> Graph;
        public readonly T Value;

        public OrderedProcess(DependencyGraph<T> graph, T value)
        {
            Graph = graph;
            Value = value;

            Graph.Add(this);
        }

        public IEnumerable<OrderedProcess<T>> Predecessors => _predecessors;
        public IEnumerable<OrderedProcess<T>> Followers => _followers;
        public IEnumerable<Resource<T>> Resources => _resources;
        internal ISet<Resource<T>> ResourcesSet => _resources;

        public OrderedProcess<T> Before(OrderedProcess<T> follower)
        {
            DependencyGraph<T>.CheckGraph(this, follower);

            if (_followers.Add(follower))
                follower.After(this);

            return follower;
        }

        public IEnumerable<OrderedProcess<T>> Before(params OrderedProcess<T>[] followers)
        {
            return Before(followers as IEnumerable<OrderedProcess<T>>);
        }

        public IEnumerable<OrderedProcess<T>> Before(IEnumerable<OrderedProcess<T>> followers)
        {
            foreach (var ancestor in followers)
                Before(ancestor);

            return followers;
        }

        public OrderedProcess<T> After(OrderedProcess<T> predecessor)
        {
            DependencyGraph<T>.CheckGraph(this, predecessor);

            if (_predecessors.Add(predecessor))
                predecessor.Before(this);

            return predecessor;
        }

        public IEnumerable<OrderedProcess<T>> After(params OrderedProcess<T>[] predecessors)
        {
            return After(predecessors as IEnumerable<OrderedProcess<T>>);
        }

        public IEnumerable<OrderedProcess<T>> After(IEnumerable<OrderedProcess<T>> predecessors)
        {
            foreach (var predecessor in predecessors)
                After(predecessor);

            return predecessors;
        }

        public void Requires(Resource<T> resource)
        {
            DependencyGraph<T>.CheckGraph(resource, this);

            if (_resources.Add(resource))
                resource.UsedBy(this);
        }

        public override string ToString()
        {
            return "Process { " + Value + " }";
        }
    }
}