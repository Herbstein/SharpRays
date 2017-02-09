namespace SharpRays.Materials {
    using System;

    using SharpRays.Core;
    using SharpRays.Utility;

    internal class Dielectric : IMaterial {
        private readonly double refractionIndex;

        public Dielectric(double ri) {
            refractionIndex = ri;
        }

        public bool Scatter(Ray ray, HitRecord rec, ref Vector attenuation, ref Ray scattered) {
            Vector outwardNormal;
            var reflected = ray.Direction.Reflect(rec.N);
            double niOverNt;
            attenuation = new Vector(1, 1, 1);
            var refracted = new Vector();
            double reflectProb;
            double cosine;
            if (ray.Direction.Dot(rec.N) > 0) {
                outwardNormal = -rec.N;
                niOverNt = refractionIndex;
                cosine = refractionIndex * ray.Direction.Dot(rec.N) / ray.Direction.Length;
            } else {
                outwardNormal = rec.N;
                niOverNt = 1 / refractionIndex;
                cosine = -ray.Direction.Dot(rec.N) / ray.Direction.Length;
            }
            if (ray.Direction.Refract(outwardNormal, niOverNt, ref refracted)) {
                reflectProb = Schlick(cosine, refractionIndex);
            } else {
                reflectProb = 1;
            }

            scattered = Rand.Double < reflectProb ? new Ray(rec.P, reflected) : new Ray(rec.P, refracted);

            return true;
        }

        private static double Schlick(double cosine, double refractionIndex) {
            var r0 = (1 - refractionIndex) / (1 + refractionIndex);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
        }
    }
}