using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu (fileName = Vector2Name, menuName = Vector2Path, order = Vector2Order)]
    public sealed class Vector2Data : InterpolatorData<Vector2>
    {
        public override Vector2 GetInterpolatedValue (Vector2 startValue, Vector2 endValue, float interpolationValue) =>
            Vector2.Lerp (startValue, endValue, interpolationValue);
    }
}
