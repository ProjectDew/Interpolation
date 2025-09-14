using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
    public class Interpolator<T> where T : struct
    {
		private enum LerpPhase
		{
			Offset,
			Interpolate,
			Interval,
			Pause,
			Stop
		}

		private readonly InterpolatorData<T> data;

		private readonly float offsetTime;

		private readonly int initialIndex;

		private LerpPhase previousPhase;
		private LerpPhase currentPhase;

		private float offsetCounter;
		private float interpolationCounter;
		private float intervalCounter;

		private int direction;

		public T CurrentValue { get; private set; }
		
		public int StartIndex
		{
			get
			{
				int startIndex = EndIndex - direction;

				if (startIndex < 0)
					return data.TotalInterpolables - 1;
				else if (startIndex >= data.TotalInterpolables)
					return 0;

				return startIndex;
			}
		}
		
		public int EndIndex { get; private set; }

		public bool IsInterpolating => currentPhase != LerpPhase.Pause && currentPhase != LerpPhase.Stop;

		public bool IsMovingForward => direction >= 0;

		public bool IsAtInitialIndex { get; private set; }

		public bool IsAtStart { get; private set; }

		public bool IsAtEnd { get; private set; }

		public Interpolator (InterpolatorData<T> interpolatorData) : this (interpolatorData, 0) { }

		public Interpolator (InterpolatorData<T> data, int initialIndex)
		{
			if (data == null)
				throw new ArgumentNullException ("The interpolator data is null. " +
					$"To use an existing one, open the Create menu in the Assets folder, go to {InterpolationEditorConstants.InterpolationFolder} and select a type of data from the list. " +
					"If you need a more custom set of data, create a Scriptable Object that inherits from InterpolatorData<T>, where T is your custom serializable struct.");

			if (data.TotalInterpolables == 0)
				throw new ArgumentException ("You need to provide at least one value to interpolate into. You can do so in the inspector of the data object.");

			if (initialIndex < 0 || initialIndex >= data.TotalInterpolables)
				throw new IndexOutOfRangeException ($"The initial index is out of bounds. Index: {initialIndex}. Length: {data.TotalInterpolables}.");

			this.data = data;
			this.initialIndex = initialIndex;
			
			offsetTime = data.OffsetTime;

			Reset (initialIndex);
		}

		public void Reset () => Reset (initialIndex);

		public void Reset (int initialIndex)
		{
			CurrentValue = data.GetInterpolableValue (initialIndex);

			offsetCounter = 0;
			direction = 1;

			EndIndex = initialIndex;

			SetNextIndex ();

			currentPhase = (offsetTime > 0) ? LerpPhase.Offset : LerpPhase.Interpolate;
		}

		public void Resume ()
		{
			currentPhase = previousPhase;
		}

		public void Pause ()
		{
			previousPhase = currentPhase;
			currentPhase = LerpPhase.Pause;
		}

		public void Stop ()
		{
			currentPhase = LerpPhase.Stop;
		}

		public T Lerp () => Lerp (null);

		public T Lerp (Action action)
		{
			if (!IsInterpolating)
				return CurrentValue;

			float intervalTime = data.GetIntervalTime (EndIndex);
			float interpolationTime = data.GetInterpolationTime (EndIndex);

			if (currentPhase == LerpPhase.Offset)
				UpdateOffset ();
			else if (currentPhase == LerpPhase.Interval)
				UpdateInterval (intervalTime);
			else if (currentPhase == LerpPhase.Interpolate)
				Interpolate (action, interpolationTime, intervalTime);

			return CurrentValue;
		}

		private void UpdateOffset ()
		{
			offsetCounter += Time.deltaTime;

			if (offsetCounter < offsetTime)
				return;

			offsetCounter = 0;
				
			currentPhase = LerpPhase.Interpolate;
		}

		private void UpdateInterval (float intervalTime)
		{
			intervalCounter += Time.deltaTime;

			if (intervalCounter < intervalTime)
				return;
			
			SetNextIndex ();

			intervalCounter = 0;
			
			if (IsInterpolating)
				currentPhase = LerpPhase.Interpolate;
		}

		private void Interpolate (Action action, float interpolationTime, float intervalTime)
		{
			interpolationCounter += Time.deltaTime;

			T startValue = data.GetInterpolableValue (StartIndex);
			T endValue = data.GetInterpolableValue (EndIndex);

			CurrentValue = data.GetInterpolatedValue (startValue, endValue, interpolationCounter / interpolationTime);

			if (interpolationCounter >= interpolationTime)
			{
				IsAtInitialIndex = EndIndex == initialIndex;

				IsAtStart = EndIndex == 0;
				IsAtEnd = EndIndex == data.TotalInterpolables - 1;

				action?.Invoke ();
				
				if (intervalTime > 0)
					currentPhase = LerpPhase.Interval;
				else
					SetNextIndex ();

				interpolationCounter = 0;
			}
		}

		private void SetNextIndex ()
		{
			int nextIndex = EndIndex + direction;

			if (nextIndex >= 0 && nextIndex < data.TotalInterpolables)
			{
				EndIndex = nextIndex;
				return;
			}

			if (data.InterpolationMode == InterpolationMode.Forward)
			{
				Stop ();
			}
			else if (data.InterpolationMode == InterpolationMode.Endless)
			{
				EndIndex = IsMovingForward ? 0 : data.TotalInterpolables - 1;
			}
			else if (data.InterpolationMode == InterpolationMode.BackAndForth)
			{
				if (IsMovingForward)
				{
					direction = -1;
					EndIndex += direction;
				}
				else
				{
					Stop ();
				}
			}
			else if (data.InterpolationMode == InterpolationMode.EndlessBackAndForth)
			{
				direction = IsMovingForward ? -1 : 1;
				EndIndex += direction;
			}
		}
    }
}
