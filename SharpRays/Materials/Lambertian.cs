namespace SharpRays.Materials {
    using SharpRays.Core;
    using SharpRays.Utility;

    internal class Lambertian : IMaterial {
        public Vector Albedo;

        public Lambertian(Vector a) {
            Albedo = a;
        }

        public bool Scatter(Ray ray, HitRecord rec, ref Vector attenuation, ref Ray scattered) {
            var target = rec.P + rec.N + Rand.RandomInUnitSphere;
            scattered = new Ray(rec.P, target - rec.P);
            attenuation = Albedo;
            return true;
        }
    }
}