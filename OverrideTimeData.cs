using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu(fileName = OverridesName, menuName = OverridesPath, order = OverridesOrder)]
    public class OverrideTimeData : ScriptableObject
    {
		[SerializeField]
		private float interpolationTime;
		
		[SerializeField]
		private float intervalTime;

		public float InterpolationTime => interpolationTime;
		
		public float IntervalTime => intervalTime;
    }
}
