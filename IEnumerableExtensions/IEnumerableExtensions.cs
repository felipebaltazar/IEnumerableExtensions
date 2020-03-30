using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEnumerableExtensions
{
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// Check if IEnumerable is null or empty
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">Collection</param>
        /// <returns></returns>
        /// <example> 
        /// This sample shows how to call the <see cref="IsNullOrEmpty"/> method.
        /// <code>
        /// if (myList.IsNullOrEmpty())
        /// {
        ///     DoSomething();
        /// }
        /// </code>
        /// </example>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) =>
            source == null || !source.Any();

        /// <summary>
        /// Executes a foreach loop with one action for item on collection
        /// </summary>
        /// <typeparam name="T">type of collection</typeparam>
        /// <param name="collection">collection</param>
        /// <param name="action">action to execute</param>
        /// <returns></returns>
        /// <example> 
        /// This sample shows how to call the <see cref="Foreach"/> method.
        /// <code>
        /// myList.Foreach(e => Console.WriteLine(e.ToString()));
        /// </code>
        /// </example>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action?.Invoke(item);

            return collection;
        }

        /// <summary>
        /// Reverse foreach
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="colletion">collection</param>
        /// <param name="action">action to execute</param>
        /// <example> 
        /// This sample shows how to call the <see cref="ReverseForeach"/> method.
        /// <code>
        /// myList.ReverseForeach(e => Console.WriteLine(e.ToString()));
        /// </code>
        /// </example>
        public static void ReverseForeach<T>(this IEnumerable<T> colletion, Action<int, T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var count = colletion.Count();
            if (count < 1) return;

            for (var i = count - 1; i >= 0; i--)
                action(i, colletion.ElementAt(i));
        }

        /// <summary>
        /// Executes a loop with async tasks
        /// </summary>
        /// <typeparam name="TSource">Source type of collection</typeparam>
        /// <typeparam name="TResult">Result tasks type</typeparam>
        /// <param name="collection">Async collection</param>
        /// <param name="action">Action to axecute</param>
        /// <returns></returns>
        /// <example> 
        /// This sample shows how to call the <see cref="ForeachAsync"/> method.
        /// <code>
        /// var resultCollection = await myList.ForeachAsync(e => DoSomeAsyncTask(e));
        /// </code>
        /// </example>
        public static async Task<IEnumerable<TResult>> ForeachAsync<TSource, TResult>(
            this IEnumerable<TSource> collection, Func<TSource, Task<TResult>> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            return await Task.WhenAll(collection.SelectAsync(action)).ConfigureAwait(false);
        }


        /// <summary>
        /// Select async function tasks
        /// </summary>
        /// <typeparam name="TSource">Source type of collection</typeparam>
        /// <typeparam name="TResult">Result tasks type</typeparam>
        /// <param name="collection">Async collection</param>
        /// <param name="action">Action to axecute</param>
        /// <returns></returns>
        /// <example> 
        /// This sample shows how to call the <see cref="SelectAsync"/> method.
        /// <code>
        /// var taskCollection = myList.SelectAsync(e => DoSomeAsyncTask(e));
        /// </code>
        /// </example>
        public static IEnumerable<Task<TResult>> SelectAsync<TSource, TResult>(
            this IEnumerable<TSource> collection, Func<TSource, Task<TResult>> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in collection)
                yield return action.Invoke(item);
        }

        /// <summary>
        /// Split collection in container groups
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="groupCount">Container amount</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitInContainerGroups<T>(this IEnumerable<T> collection, int groupCount)
        {
            return collection
                .Select((v, i) => new { Index = i, Value = v })
                .GroupBy(x => x.Index / groupCount)
                .Select(g => g.Select(x => x.Value));
        }

        /// <summary>
        /// Returns the first element of a sequence that matches specific cast, or a default value if the sequence contains
        ///     no elements.
        /// </summary>
        /// <typeparam name="TResult">The type of the cast</typeparam>
        /// <param name="collection">The System.Collections.Generic.IEnumerable`1 to return the first element of.</param>
        /// <returns></returns>
        /// <example> 
        /// This sample shows how to call the <see cref="FirstCastOrDefault"/> method.
        /// <code>
        /// TypeForCast result = myList.FirstCastOrDefault<TypeForCast>();
        /// </code>
        /// </example>

        public static TResult FirstCastOrDefault<TResult>(this IEnumerable<object> collection)
        {
            foreach (var element in collection)
            {
                if (element is TResult parsedResult)
                    return parsedResult;
            }

            return default(TResult);
        }
    }
}
