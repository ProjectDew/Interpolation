using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu (fileName = TransformName, menuName = TransformPath, order = TransformOrder)]
	public sealed class TransformData : InterpolatorData<TransformValue>
    {
		public override TransformValue GetInterpolatedValue (TransformValue startValue, TransformValue endValue, float interpolationValue)
		{
			Vector3 position = Vector3.Lerp (startValue.Position, endValue.Position, interpolationValue);
			Quaternion rotation = Quaternion.Lerp (startValue.Rotation, endValue.Rotation, interpolationValue);
			Vector3 scale = Vector3.Lerp (startValue.Scale, endValue.Scale, interpolationValue);

			return new TransformValue (position, rotation, scale);
		}
    }
}
