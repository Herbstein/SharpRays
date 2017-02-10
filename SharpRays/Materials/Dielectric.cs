// ReSharper disable ImpureMethodCallOnReadonlyValueField

namespace SharpRays.Materials {
    using System;
    using System.Numerics;
    using Core;
    using Utility;

    internal class Dielectric : IMaterial {
        private readonly float refractionIndex;

        public Dielectric(float ri) {
            refractionIndex = ri;
        }

        public bool Scatter(Ray inRay, HitRecord rec, ref Vector3 attenuation, ref Ray scattered) {
            Vector3 outwardNormal;
            var reflected = inRay.Direction.Reflect(rec.N);
            float niOverNt;
            attenuation = new Vector3(1, 1, 1);
            var refracted = new Vector3();
            float cosine;

            if (Vector3.Dot(inRay.Direction, rec.N) > 0) {
                outwardNormal = -rec.N;
                niOverNt = refractionIndex;
                cosine = refractionIndex * Vector3.Dot(inRay.Direction, rec.N) / inRay.Direction.Length();
            } else {
                outwardNormal = rec.N;
                niOverNt = 1 / refractionIndex;
                cosine = Vector3.Dot(-inRay.Direction, rec.N) / inRay.Direction.Length();
            }

            var reflectProb = inRay.Direction.Refract(outwardNormal, niOverNt, ref refracted)
                                  ? Schlick(cosine, refractionIndex)
                                  : 1;
            scattered = Rand.Float < reflectProb
                            ? new Ray(rec.P, reflected, inRay.Time)
                            : new Ray(rec.P, refracted, inRay.Time);

            return true;
        }

        private static double Schlick(double cosine, double refractionIndex) {
            var r0 = (1 - refractionIndex) / (1 + refractionIndex);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
        }
    }
}
