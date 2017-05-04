namespace SharpRays.Hitables {
	using Core;

	internal interface IHitable {
		bool Hit(Ray r, float tmin, float tmax, ref HitRecord rec);
		bool BoundingBox(float t0, float t1, out AABB box);
	}
}
