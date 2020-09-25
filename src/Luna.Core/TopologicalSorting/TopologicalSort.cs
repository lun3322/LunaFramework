using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Luna.TopologicalSorting
{
    public class TopologicalSort : IEnumerable<ISet<OrderedProcess>>, IEnumerable<OrderedProcess>
    {
        private readonly List<ISet<OrderedProcess>> _collections = new List<ISet<OrderedProcess>>();

        public TopologicalSort()
        {
        }

        public TopologicalSort(DependencyGraph g)
            : this()
        {
            g.CalculateSort(this);
        }

        IEnumerator<ISet<OrderedProcess>> IEnumerable<ISet<OrderedProcess>>.GetEnumerator()
        {
            return _collections.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<OrderedProcess>).GetEnumerator();
        }

        IEnumerator<OrderedProcess> IEnumerable<OrderedProcess>.GetEnumerator()
        {
            IEnumerable<IEnumerable<OrderedProcess>> collections = this;

            return collections.SelectMany(collection => collection).GetEnumerator();
        }

        internal void Append(ISet<OrderedProcess> collection)
        {
            _collections.Add(collection);
        }
    }

    public class TopologicalSort<T>
        : IEnumerable<ISet<OrderedProcess<T>>>, IEnumerable<OrderedProcess<T>>
    {
        private readonly List<ISet<OrderedProcess<T>>> _collections = new List<ISet<OrderedProcess<T>>>();

        public TopologicalSort()
        {
        }


        public TopologicalSort(DependencyGraph<T> g)
            : this()
        {
            g.CalculateSort(this);
        }

        IEnumerator<ISet<OrderedProcess<T>>> IEnumerable<ISet<OrderedProcess<T>>>.GetEnumerator()
        {
            return _collections.GetEnumerator();
        }


        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<OrderedProcess<T>>).GetEnumerator();
        }


        IEnumerator<OrderedProcess<T>> IEnumerable<OrderedProcess<T>>.GetEnumerator()
        {
            IEnumerable<IEnumerable<OrderedProcess<T>>> collections = this;

            return collections.SelectMany(collection => collection).GetEnumerator();
        }

        internal void Append(ISet<OrderedProcess<T>> collection)
        {
            _collections.Add(collection);
        }
    }
}