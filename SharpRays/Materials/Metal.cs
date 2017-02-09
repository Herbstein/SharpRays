namespace SharpRays.Materials {
    using SharpRays.Core;
    using SharpRays.Utility;

    internal class Metal : IMaterial {
        public Vector Albedo;
        public double Fuzz;

        public Metal(Vector a, double f) {
            Albedo = a;
            Fuzz = f;
        }

        public bool Scatter(Ray ray, HitRecord rec, ref Vector attenuation, ref Ray scattered) {
            var reflected = ray.Direction.Normalized.Reflect(rec.N);
            scattered = new Ray(rec.P, reflected + Fuzz * Rand.RandomInUnitSphere);
            attenuation = Albedo;
            return scattered.Direction.Dot(rec.N) > 0;
        }
    }
}