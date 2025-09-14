using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

	[CreateAssetMenu (fileName = QuaternionName, menuName = QuaternionPath, order = QuaternionOrder)]
	public sealed class QuaternionData : InterpolatorData<Quaternion>
	{
		public override Quaternion GetInterpolatedValue (Quaternion startValue, Quaternion endValue, float interpolationValue) =>
			Quaternion.Lerp (startValue, endValue, interpolationValue);
	}
}
