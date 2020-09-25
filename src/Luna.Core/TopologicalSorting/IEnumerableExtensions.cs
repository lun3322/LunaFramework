using System.Collections.Generic;

namespace Luna.TopologicalSorting
{
    public static class EnumerableExtensions
    {
        internal static bool IsEmpty<T>(this IEnumerable<T> e)
        {
            return !e.GetEnumerator().MoveNext();
        }

        public static OrderedProcess After(this IEnumerable<OrderedProcess> followers, OrderedProcess predecessor)
        {
            predecessor.Before(followers);

            return predecessor;
        }

        public static OrderedProcess<T> After<T>(this IEnumerable<OrderedProcess<T>> followers, OrderedProcess<T> predecessor)
        {
            predecessor.Before(followers);

            return predecessor;
        }

        public static IEnumerable<OrderedProcess> After(this IEnumerable<OrderedProcess> followers, params OrderedProcess[] predecessors)
        {
            return After(followers, predecessors as IEnumerable<OrderedProcess>);
        }

        public static IEnumerable<OrderedProcess<T>> After<T>(this IEnumerable<OrderedProcess<T>> followers, params OrderedProcess<T>[] predecessors)
        {
            return After(followers, predecessors as IEnumerable<OrderedProcess<T>>);
        }

        public static IEnumerable<OrderedProcess> After(this IEnumerable<OrderedProcess> followers, IEnumerable<OrderedProcess> predecessors)
        {
            foreach (var predecessor in predecessors) predecessor.Before(followers);

            return predecessors;
        }

        public static IEnumerable<OrderedProcess<T>> After<T>(this IEnumerable<OrderedProcess<T>> followers, IEnumerable<OrderedProcess<T>> predecessors)
        {
            foreach (var predecessor in predecessors) predecessor.Before(followers);

            return predecessors;
        }

        public static OrderedProcess Before(this IEnumerable<OrderedProcess> predecessors, OrderedProcess follower)
        {
            follower.After(predecessors);

            return follower;
        }

        public static OrderedProcess<T> Before<T>(this IEnumerable<OrderedProcess<T>> predecessors, OrderedProcess<T> follower)
        {
            follower.After(predecessors);

            return follower;
        }

        public static IEnumerable<OrderedProcess> Before(this IEnumerable<OrderedProcess> predecessors, params OrderedProcess[] followers)
        {
            return Before(predecessors, followers as IEnumerable<OrderedProcess>);
        }

        public static IEnumerable<OrderedProcess<T>> Before<T>(this IEnumerable<OrderedProcess<T>> predecessors, params OrderedProcess<T>[] followers)
        {
            return Before(predecessors, followers as IEnumerable<OrderedProcess<T>>);
        }

        public static IEnumerable<OrderedProcess> Before(this IEnumerable<OrderedProcess> predecessors, IEnumerable<OrderedProcess> followers)
        {
            foreach (var follower in followers) follower.After(predecessors);

            return followers;
        }

        public static IEnumerable<OrderedProcess<T>> Before<T>(this IEnumerable<OrderedProcess<T>> predecessors, IEnumerable<OrderedProcess<T>> followers)
        {
            foreach (var follower in followers) follower.After(predecessors);

            return followers;
        }
    }
}