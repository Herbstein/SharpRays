namespace SharpRays.Materials {
    using System.Numerics;
    using Core;
    using Utility;

    internal class Metal : IMaterial {
        public Vector3 Albedo;
        public float Fuzz;

        public Metal(Vector3 a, float f) {
            Albedo = a;
            Fuzz = f;
        }

        public bool Scatter(Ray inRay, HitRecord rec, ref Vector3 attenuation, ref Ray scattered) {
            var reflected = Vector3.Normalize(inRay.Direction).Reflect(rec.N);
            scattered = new Ray(rec.P, reflected + Fuzz * Rand.RandomInUnitSphere(), inRay.Time);
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, rec.N) > 0;
        }
    }
}
