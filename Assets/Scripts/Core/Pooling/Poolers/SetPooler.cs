using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pool
{
	public class SetPooler : BasePooler
	{
		#region Fields / Properties
		public HashSet<Poolable> Collection = new HashSet<Poolable>();
		#endregion

		#region Public
		public override void Enqueue(Poolable item)
		{
			base.Enqueue(item);
			if (Collection.Contains(item))
				Collection.Remove(item);
		}

		public override void EnqueueObject(GameObject obj)
		{
			base.EnqueueObject(obj);

			Poolable item = obj.GetComponent<Poolable>();
			if (Collection.Contains(item))
				Collection.Remove(item);
		}

		public override Poolable Dequeue()
		{
			Poolable item = base.Dequeue();
			Collection.Add(item);
			return item;
		}

		public override void EnqueueAll()
		{
			foreach (Poolable item in Collection)
				base.Enqueue(item);
			Collection.Clear();
		}
		#endregion
	}
}

