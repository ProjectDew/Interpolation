using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
	[Serializable]
    public class InterpolableData<T> where T : struct
    {
		[SerializeField]
		private T value;

		[SerializeField]
		private OverrideTimeData timeData;

		public T Value => value;

		public OverrideTimeData TimeData => timeData;
    }
}
