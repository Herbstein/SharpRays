namespace SharpRays.Hitables {
    using System;

    using SharpRays.Core;
    using SharpRays.Materials;
    using SharpRays.Utility;

    internal class Sphere : IHitable {
        public Vector Center;
        public IMaterial Material;
        public double Radius;

        public Sphere(Vector c, double r, IMaterial m) {
            Center = c;
            Radius = r;
            Material = m;
        }

        public bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec) {
            var oc = r.Origin - Center;
            var a = r.Direction.Dot(r.Direction);
            var b = oc.Dot(r.Direction);
            var c = oc.Dot(oc) - Radius * Radius;
            var d = b * b - a * c;
            if (d > 0) {
                var temp = (-b - Math.Sqrt(d)) / a;
                if (temp.IsWithin(tmin, tmax)) {
                    rec.T = temp;
                    rec.P = r.PointAtParameter(rec.T);
                    rec.N = (rec.P - Center) / Radius;
                    rec.M = Material;
                    return true;
                }
                temp = (-b + Math.Sqrt(d)) / a;
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
    }
}