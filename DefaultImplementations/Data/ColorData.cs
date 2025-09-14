using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

	[CreateAssetMenu (fileName = ColorName, menuName = ColorPath, order = ColorOrder)]
	public sealed class ColorData : InterpolatorData<Color>
	{
		public override Color GetInterpolatedValue (Color startValue, Color endValue, float interpolationValue) =>
			Color.Lerp (startValue, endValue, interpolationValue);
	}
}
