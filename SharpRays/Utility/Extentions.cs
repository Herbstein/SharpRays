namespace SharpRays.Utility {
	using System;
	using System.Numerics;

	public static class Extentions {
		public static bool IsWithin(this float val, float min, float max) { return val > min && val < max; }

		public static float Get(this Vector3 v, int index) {
			switch (index) {
				case 0:
					return v.X;
				case 1:
					return v.Y;
				case 2:
					return v.Z;
				default:
					throw new IndexOutOfRangeException();
			}
		}

		public static Vector3 ProcessComponents(this Vector3 v, Func<float, float> f) {
			return new Vector3(f(v.X), f(v.Y), f(v.Z));
		}

		public static Vector3 Reflect(this Vector3 v, Vector3 n) { return v - 2 * Vector3.Dot(v, n) * n; }

		public static bool Refract(this Vector3 v, Vector3 n, float niOverNt, ref Vector3 refracted) {
			var uv = Vector3.Normalize(v);
			var dt = Vector3.Dot(uv, n);
			var discriminant = 1 - niOverNt * niOverNt * (1 - dt * dt);

			if (!(discriminant > 0)) {
				return false;
			}

			refracted = niOverNt * (uv - n * dt) - n * Mathf.Sqrt(discriminant);
			return true;
		}

		public static string ToColorString(this Vector3 v) { return $"{v.X} {v.Y} {v.Z}"; }
	}
}
