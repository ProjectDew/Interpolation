using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
	[Serializable]
    public struct ColorTransformValue
    {
		[field: SerializeField]
		public Color Color { get; private set; }
		
		[field: SerializeField]
		public Vector3 Position { get; private set; }
		
		[field: SerializeField]
		public Quaternion Rotation { get; private set; }
		
		[field: SerializeField]
		public Vector3 Scale { get; private set; }

		public ColorTransformValue (Color color, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			Color = color;
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		public readonly override string ToString () => $"Color: {Color}. Position: {Position}. Rotation: {Rotation}. Scale: {Scale}.";
    }
}
