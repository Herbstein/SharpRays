namespace SharpRays.Materials {
    using SharpRays.Core;

    internal interface IMaterial {
        bool Scatter(Ray ray, HitRecord rec, ref Vector attenuation, ref Ray scattered);
    }
}