namespace SharpRays.Hitables {
    using SharpRays.Core;

    internal interface IHitable {
        bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec);
    }
}