using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
	[Serializable]
	public abstract class InterpolatorData<T> : ScriptableObject where T : struct
	{
		private const string OUT_OF_RANGE_ERROR = "The index is out of the bounds of the data. Index: {0}. Data length: {1}.";

		[SerializeField]
		private float offsetTime;

		[SerializeField]
		private float interpolationTime;
		
		[SerializeField]
		private float intervalTime;
		
		[SerializeField]
		private InterpolationMode interpolationMode;
		
		[SerializeField]
		private InterpolableData<T>[] interpolableData;
		
		public virtual float OffsetTime => offsetTime;

		public virtual InterpolationMode InterpolationMode => interpolationMode;

		public virtual int TotalInterpolables => interpolableData.Length;

		public abstract T GetInterpolatedValue (T startValue, T endValue, float interpolationValue);

		public virtual T GetInterpolableValue (int index)
		{
			if (index < 0 || index >= interpolableData.Length)
				throw new IndexOutOfRangeException (string.Format (OUT_OF_RANGE_ERROR, index, interpolableData.Length));

			return interpolableData[index].Value;
		}

		public virtual float GetInterpolationTime (int index)
		{
			if (index < 0 || index >= interpolableData.Length)
				throw new IndexOutOfRangeException (string.Format (OUT_OF_RANGE_ERROR, index, interpolableData.Length));

			return (interpolableData[index].TimeData != null) ? interpolableData[index].TimeData.InterpolationTime : interpolationTime;
		}

		public virtual float GetIntervalTime (int index)
		{
			if (index < 0 || index >= interpolableData.Length)
				throw new IndexOutOfRangeException (string.Format (OUT_OF_RANGE_ERROR, index, interpolableData.Length));

			return (interpolableData[index].TimeData != null) ? interpolableData[index].TimeData.IntervalTime : intervalTime;
		}
	}
}
