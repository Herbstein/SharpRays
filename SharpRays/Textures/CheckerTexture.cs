namespace SharpRays.Textures {
	using System;
	using System.Numerics;

	internal class CheckerTexture : ITexture {
		public ITexture Even;
		public ITexture Odd;

		public CheckerTexture(ITexture t0, ITexture t1) {
			Odd = t0;
			Even = t1;
		}

		public Vector3 Value(double u, double v, Vector3 p) {
			return Math.Sin(10 * p.X) * Math.Sin(10 * p.Y) * Math.Sin(10 * p.Z) < 0
				       ? Odd.Value(u, v, p)
				       : Even.Value(u, v, p);
		}
	}
}
