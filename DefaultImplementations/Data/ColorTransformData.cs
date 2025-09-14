using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu (fileName = ColorTrasformName, menuName = ColorTransformPath, order = ColorTransformOrder)]
    public sealed class ColorTransformData : InterpolatorData<ColorTransformValue>
    {
		public override ColorTransformValue GetInterpolatedValue (ColorTransformValue startValue, ColorTransformValue endValue, float interpolationValue)
		{
			Color color = Color.Lerp (startValue.Color, endValue.Color, interpolationValue);
			Vector3 position = Vector3.Lerp (startValue.Position, endValue.Position, interpolationValue);
			Quaternion rotation = Quaternion.Lerp (startValue.Rotation, endValue.Rotation, interpolationValue);
			Vector3 scale = Vector3.Lerp (startValue.Scale, endValue.Scale, interpolationValue);

			return new ColorTransformValue (color, position, rotation, scale);
		}
    }
}
