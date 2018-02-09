namespace SharpRays.Materials {
    using System.Numerics;
    using Core;

    internal interface IMaterial {
        bool Scatter(Ray inRay, HitRecord rec, ref Vector3 attenuation, ref Ray scattered);
    }
}
