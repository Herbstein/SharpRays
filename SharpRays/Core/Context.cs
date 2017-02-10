namespace SharpRays.Core {
    using System.Diagnostics;
    using System.IO;
    using System.Numerics;
    using System.Text;
    using Utility;

    /// <summary>
    ///     Contains the image being drawn.
    /// </summary>
    internal class Context {
        // The image height
        private readonly int height;
        // The pixel-data of the image
        private readonly Vector3[,] image;
        // The image width
        private readonly int width;

        /// <summary>
        ///     Instantiate a new <see cref="Context" />
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public Context(int width, int height) {
            image = new Vector3[width, height];
            this.width = width;
            this.height = height;
        }

        /// <summary>
        ///     Sets the pixel-data of the specified pixel
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel</param>
        /// <param name="y">Te y-coordinate of the pixel</param>
        /// <param name="pixel">The pixel-data</param>
        public void SetPixel(int x, int y, Vector3 pixel) {
            image[x, y] = pixel;
        }

        /// <summary>
        ///     Convert the internal pixel-data into an image format. Here it's the *.ppm format
        /// </summary>
        /// <returns>A string containing the image in the PPM format</returns>
        public string ToPPMString() {
            var builder = new StringBuilder();
            builder.Append($"P3\n{width} {height}\n255\n");

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var pixel = image[x, y];
                    builder.Append($"{pixel.ToColorString()}\n");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Convert the image to the *.ppm-format, save it to disk, and open the image in the default *.ppm-viewer
        /// </summary>
        /// <param name="path">The path of the image.</param>
        public void SaveToFileAndOpen(string path) {
            File.WriteAllText(path, ToPPMString());
            Process.Start(path);
        }
    }
}
