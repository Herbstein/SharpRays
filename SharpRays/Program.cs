namespace SharpRays {
    using System;

    using SharpRays.Core;
    using SharpRays.Hitables;
    using SharpRays.Materials;
    using SharpRays.Utility;

    internal class Program {
        private static Vector Color(Ray ray, IHitable world, int depth) {
            var rec = new HitRecord();
            if (world.Hit(ray, 0.001, double.MaxValue, ref rec)) {
                var scattered = new Ray();
                var attenuation = new Vector();
                if ((depth < 50) && rec.M.Scatter(ray, rec, ref attenuation, ref scattered)) {
                    return attenuation * Color(scattered, world, depth + 1);
                }

                return new Vector(0);
            }
            var unitDirection = ray.Direction.Normalized;
            var t = 0.5 * (unitDirection.Y + 1);
            return (1 - t) * new Vector(1) + t * new Vector(0.5, 0.7, 1);
        }

        private static void Main() {
            const int Width = 500;
            const int Height = 250;
            const int Samples = 100;

            var context = new Context(Width, Height);
            var cam = new Camera();

            var list = new IHitable[] {
                           new Sphere(new Vector(0, 0, -1), 0.5, new Lambertian(new Vector(0.1, 0.2, 0.5))),
                           new Sphere(new Vector(0, -100.5, -1), 100, new Lambertian(new Vector(0.8, 0.8, 0))),
                           new Sphere(new Vector(1, 0, -1), 0.5, new Metal(new Vector(0.8, 0.6, 0.2), 0)),
                           new Sphere(new Vector(-1, 0, -1), 0.5, new Dielectric(1.5)),
                           new Sphere(new Vector(-1, 0, -1), -0.45, new Dielectric(1.5))
                       };
            var world = new HitableList(list);

            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    var color = new Vector();

                    for (var s = 0; s < Samples; s++) {
                        var u = (x + Rand.Double) / Width;
                        var v = (y + Rand.Double) / Height;
                        var ray = cam.GetRay(u, v);
                        color += Color(ray, world, 0);
                    }

                    color /= Samples;
                    color.ProcessPiecewise(Math.Sqrt);
                    color.ProcessPiecewise(s => (int)(s * 255.99));
                    context.SetPixel(x, y, color);
                }
            }

            context.SaveToFileAndOpen("image.ppm");
        }
    }
}