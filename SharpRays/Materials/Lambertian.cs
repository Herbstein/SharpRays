namespace SharpRays.Materials {
    using System.Numerics;
    using Core;
    using Textures;
    using Utility;

    internal class Lambertian : IMaterial {
        public ITexture Albedo;

        public Lambertian(ITexture a) {
            Albedo = a;
        }

        public bool Scatter(Ray inRay, HitRecord rec, ref Vector3 attenuation, ref Ray scattered) {
            Vector3 target = rec.P + rec.N + Rand.RandomInUnitSphere();
            scattered = new Ray(rec.P, target - rec.P, inRay.Time);
            attenuation = Albedo.Value(0, 0, rec.P);
            return true;
        }
    }
}
