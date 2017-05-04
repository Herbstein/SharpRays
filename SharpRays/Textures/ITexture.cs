namespace SharpRays.Textures {
	using System.Numerics;

	internal interface ITexture {
		Vector3 Value(double u, double v, Vector3 p);
	}
}
