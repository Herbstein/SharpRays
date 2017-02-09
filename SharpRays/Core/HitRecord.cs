namespace SharpRays.Core {
    using SharpRays.Materials;

    internal struct HitRecord {
        /// <summary>
        ///     The ray-paramter of the hit
        /// </summary>
        public double T;
        /// <summary>
        ///     The 3D point of the hit
        /// </summary>
        public Vector P;
        /// <summary>
        ///     The normal of the hit
        /// </summary>
        public Vector N;
        /// <summary>
        ///     The material of the hit object
        /// </summary>
        public IMaterial M;
    }
}