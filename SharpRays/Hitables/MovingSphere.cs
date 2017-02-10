namespace SharpRays.Hitables {
    using System.Numerics;
    using Core;
    using Materials;
    using Utility;

    internal class MovingSphere : IHitable {
        public Vector3 Center0, Center1;
        public IMaterial Material;
        public float Radius;
        public float Time0, Time1;

        public MovingSphere(Vector3 c0, Vector3 c1, float t0, float t1, float r, IMaterial m) {
            Center0 = c0;
            Center1 = c1;
            Time0 = t0;
            Time1 = t1;
            Radius = r;
            Material = m;
        }

        public bool Hit(Ray r, float tmin, float tmax, ref HitRecord rec) {
            var oc = r.Origin - Center(r.Time);
            var a = Vector3.Dot(r.Direction, r.Direction);
            var b = Vector3.Dot(oc, r.Direction);
            var c = Vector3.Dot(oc, oc) - Radius * Radius;
            var d = b * b - a * c;
            if (d > 0) {
                var t = (-b - Mathf.Sqrt(d)) / a;
                if (t < tmax && t > tmin) {
                    rec.T = t;
                    rec.P = r.PointAtParameter(t);
                    rec.N = (rec.P - Center(r.Time)) / Radius;
                    rec.M = Material;
                    return true;
                }
                t = (-b + Mathf.Sqrt(d)) / a;
                if (t < tmax && t > tmin) {
                    rec.T = t;
                    rec.P = r.PointAtParameter(t);
                    rec.N = (rec.P - Center(r.Time)) / Radius;
                    rec.M = Material;
                    return true;
                }
            }

            return false;
        }

        public bool BoundingBox(float t0, float t1, out AABB box) {
            box = AABB.SurroundingBox(
                                      new AABB(Center(Time0) - new Vector3(Radius), Center(Time0) + new Vector3(Radius)),
                                      new AABB(Center(Time1) - new Vector3(Radius), Center(Time1) + new Vector3(Radius)));
            return true;
        }

        public Vector3 Center(float time) {
            return Center0 + (time - Time0) / (Time1 - Time0) * (Center1 - Center0);
        }
    }
}
