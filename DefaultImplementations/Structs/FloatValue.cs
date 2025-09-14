using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
	[Serializable]
    public struct FloatValue
    {
		[field: SerializeField]
		public float Float { get; private set; }

		public FloatValue (float value) => Float = value;

		public readonly override string ToString () => Float.ToString ("F2");
	}
}
