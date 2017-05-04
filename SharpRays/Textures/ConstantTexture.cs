namespace SharpRays.Textures {
	using System.Numerics;

	internal class ConstantTexture : ITexture {
		public Vector3 Color;

		public ConstantTexture(Vector3 c) { Color = c; }

		public Vector3 Value(double u, double v, Vector3 p) { return Color; }
	}
}
