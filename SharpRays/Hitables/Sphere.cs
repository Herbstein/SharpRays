namespace SharpRays.Hitables {
	using System.Numerics;
	using Core;
	using Materials;
	using Utility;

	internal class Sphere : IHitable {
		public Vector3 Center;
		public IMaterial Material;
		public float Radius;

		public Sphere(Vector3 c, float r, IMaterial m) {
			Center = c;
			Radius = r;
			Material = m;
		}

		public bool Hit(Ray r, float tmin, float tmax, ref HitRecord rec) {
			var oc = r.Origin - Center;
			var a = Vector3.Dot(r.Direction, r.Direction);
			var b = Vector3.Dot(oc, r.Direction);
			var c = Vector3.Dot(oc, oc) - Radius * Radius;
			var d = b * b - a * c;
			if (d > 0) {
				var temp = (-b - Mathf.Sqrt(d)) / a;
				if (temp.IsWithin(tmin, tmax)) {
					rec.T = temp;
					rec.P = r.PointAtParameter(rec.T);
					rec.N = (rec.P - Center) / Radius;
					rec.M = Material;
					return true;
				}
				temp = (-b + Mathf.Sqrt(d)) / a;
				if (temp.IsWithin(tmin, tmax)) {
					rec.T = temp;
					rec.P = r.PointAtParameter(rec.T);
					rec.N = (rec.P - Center) / Radius;
					rec.M = Material;
					return true;
				}
			}

			return false;
		}

		public bool BoundingBox(float t0, float t1, out AABB box) {
			box = new AABB(Center - new Vector3(Radius), Center + new Vector3(Radius));
			return true;
		}
	}
}
