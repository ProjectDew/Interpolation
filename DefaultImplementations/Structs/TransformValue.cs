using System;
using UnityEngine;

namespace ProjectDew.Interpolation
{
	[Serializable]
    public struct TransformValue
    {
		[field: SerializeField]
		public Vector3 Position { get; private set; }
		
		[field: SerializeField]
		public Quaternion Rotation { get; private set; }
		
		[field: SerializeField]
		public Vector3 Scale { get; private set; }

		public TransformValue (Vector3 position, Quaternion rotation, Vector3 scale)
		{
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		public readonly override string ToString () => $"Position: {Position}. Rotation: {Rotation}. Scale: {Scale}.";
    }
}
