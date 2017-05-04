namespace SharpRays {
	using System.Collections.Generic;
	using System.Numerics;
	using Core;
	using Hitables;
	using Materials;
	using Textures;
	using Utility;

	internal class Program {
		private static Vector3 Color(Ray ray, IHitable world, int depth) {
			var rec = new HitRecord();
			if (world.Hit(ray, 0.001f, float.MaxValue, ref rec)) {
				var scattered = new Ray();
				var attenuation = new Vector3();
				if (depth < 50 && rec.M.Scatter(ray, rec, ref attenuation, ref scattered)) {
					return attenuation * Color(scattered, world, depth + 1);
				}

				return new Vector3(0);
			}
			var unitDirection = Vector3.Normalize(ray.Direction);
			var t = 0.5f * (unitDirection.Y + 1);
			return (1 - t) * new Vector3(1) + t * new Vector3(0.5f, 0.7f, 1);
		}

		private static void Main() {
			const int width = 640;
			const int height = 360;
			const int samples = 10;

			var context = new Context(width, height);

			var lookFrom = new Vector3(13, 2, 3);
			var lookAt = Vector3.Zero;
			var distanceToFocus = 10.0f;
			var aperture = 0.1f;

			var cam = new Camera(
			                     lookFrom,
			                     lookAt,
			                     new Vector3(0, 1, 0),
			                     20,
			                     (float) width / height,
			                     aperture,
			                     distanceToFocus,
			                     0,
			                     1);

			var world = RandomScene();

			for (var x = 0; x < width; x++) {
				for (var y = 0; y < height; y++) {
					var color = new Vector3();

					for (var s = 0; s < samples; s++) {
						var u = (x + Rand.Float) / width;
						var v = (y + Rand.Float) / height;
						var ray = cam.GetRay(u, v);
						color += Color(ray, world, 0);
					}

					color /= samples;
					color = color.ProcessComponents(Mathf.Sqrt);
					color = color.ProcessComponents(s => (int) (s * 255.99));
					context.SetPixel(x, y, color);
				}
			}

			context.SaveToFileAndOpen("image.ppm");
		}

		private static IHitable RandomScene() {
			var checkerTexture =
				new ConstantTexture(new Vector3(0.5f, 0.5f, 0.5f));

			var list = new List<IHitable> {
				new Sphere(
				           new Vector3(0, -1000.0f, 0),
				           1000,
				           new Lambertian(checkerTexture))
			};

			for (var a = -11; a < 11; a++) {
				for (var b = -11; b < 11; b++) {
					var chooseMat = Rand.Float;
					var center = new Vector3(a + 0.9f * Rand.Float, 0.2f, b + 0.9f * Rand.Float);
					if ((center - new Vector3(4, 0.2f, 0)).Length() > 0.9f) {
						if (chooseMat < 0.8) { // diffuse
							list.Add(
							         new Sphere(
							                    center,
							                    0.2f,
							                    new Lambertian(
							                                   new ConstantTexture(
							                                                       new Vector3(
							                                                                   Rand.Float *
							                                                                   Rand.Float,
							                                                                   Rand.Float *
							                                                                   Rand.Float,
							                                                                   Rand.Float *
							                                                                   Rand.Float)))));
						} else if (chooseMat < 0.95) { // metal
							list.Add(
							         new Sphere(
							                    center,
							                    0.2f,
							                    new Metal(
							                              new Vector3(
							                                          0.5f * (1 + Rand.Float),
							                                          0.5f * (1 + Rand.Float),
							                                          0.5f * (1 + Rand.Float)),
							                              0.5f * Rand.Float)));
						} else { // glass
							list.Add(new Sphere(center, 0.2f, new Dielectric(1.5f)));
						}
					}
				}
			}

			list.Add(new Sphere(new Vector3(0, 1, 0), 1, new Dielectric(1.5f)));
			list.Add(
			         new Sphere(
			                    new Vector3(-4, 1, 0),
			                    1,
			                    new Lambertian(new ConstantTexture(new Vector3(0.4f, 0.2f, 0.1f)))));
			list.Add(new Sphere(new Vector3(4, 1, 0), 1, new Metal(new Vector3(0.7f, 0.6f, 0.6f), 0)));

			return new BVHNode(list, 0, 1);
		}
	}
}
