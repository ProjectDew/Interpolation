using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu (fileName = FloatName, menuName = FloatPath, order = FloatOrder)]
    public sealed class FloatData : InterpolatorData<FloatValue>
    {
        public override FloatValue GetInterpolatedValue (FloatValue startValue, FloatValue endValue, float interpolationValue) =>
            new FloatValue (Mathf.Lerp (startValue.Float, endValue.Float, interpolationValue));
    }
}
