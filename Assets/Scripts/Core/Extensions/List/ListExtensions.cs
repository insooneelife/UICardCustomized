using UnityEngine;
using System.Collections.Generic;
using System;

namespace Extensions
{
	public static class ListExtensions
	{
		public static void Shuffle<T>(this IList<T> ts)
		{
			var count = ts.Count;
			var last = count - 1;
			for (var i = 0; i < last; ++i)
			{
				var r = UnityEngine.Random.Range(i, count);
				var tmp = ts[i];
				ts[i] = ts[r];
				ts[r] = tmp;
			}
		}

		public static T Random<T>(this List<T> list)
		{
			if (list.Count == 0)
				throw new IndexOutOfRangeException("List is Empty");

			int index = UnityEngine.Random.Range(0, list.Count);
			return list[index];
		}

		public static T First<T>(this List<T> list)
		{
			return list[0];
		}

		public static T Last<T>(this List<T> list)
		{
			return list[list.Count - 1];
		}

		public static T Draw<T>(this List<T> list)
		{
			if (list.Count == 0)
				return default(T);

			int index = UnityEngine.Random.Range(0, list.Count);
			var result = list[index];
			list.RemoveAt(index);
			return result;
		}

		public static List<T> Draw<T>(this List<T> list, int count)
		{
			int resultCount = Mathf.Min(count, list.Count);
			List<T> result = new List<T>(resultCount);
			for (int i = 0; i < resultCount; ++i)
			{
				T item = list.Draw();
				result.Add(item);
			}
			return result;
		}


		/// <summary>
		///     Returns and Remove a random item from inside the
		///     <typeparam name="T">List</typeparam>
		///     >
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T RandomItemRemove<T>(this List<T> list)
		{
			var item = list.Random();
			list.Remove(item);
			return item;
		}

		/// <summary>
		///     Adds an item at the beginning of the List
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		public static void AddToFront<T>(this List<T> list, T item) => list.Insert(0, item);

		/// <summary>
		///     Add an item in before another item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="newItem"></param>
		public static void AddBeforeOf<T>(this List<T> list, T item, T newItem)
		{
			var targetPosition = list.IndexOf(item);
			list.Insert(targetPosition, newItem);
		}

		/// <summary>
		///     Add an item in after another item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="newItem"></param>
		public static void AddAfterOf<T>(this List<T> list, T item, T newItem)
		{
			var targetPosition = list.IndexOf(item) + 1;
			list.Insert(targetPosition, newItem);
		}

		/// <summary>
		///     Prints the list in the following format: [item[0], item[1], ... , item[Count-1]]
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		public static void Print<T>(this List<T> list, string log = "")
		{
			log += "[";
			for (var i = 0; i < list.Count; i++)
			{
				log += list[i].ToString();
				log += i != list.Count - 1 ? ", " : "]";
			}

			Debug.Log(log);
		}
	}

}
