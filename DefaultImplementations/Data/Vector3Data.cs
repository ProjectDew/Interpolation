using UnityEngine;

namespace ProjectDew.Interpolation
{
	using static InterpolationEditorConstants;

    [CreateAssetMenu (fileName = Vector3Name, menuName = Vector3Path, order = Vector3Order)]
    public sealed class Vector3Data : InterpolatorData<Vector3>
    {
        public override Vector3 GetInterpolatedValue (Vector3 startValue, Vector3 endValue, float interpolationValue) =>
            Vector3.Lerp (startValue, endValue, interpolationValue);
    }
}
