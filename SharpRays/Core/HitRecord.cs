namespace SharpRays.Core {
    using System.Numerics;
    using Materials;

    internal struct HitRecord {
	    /// <summary>
	    ///     The ray-paramter of the hit
	    /// </summary>
	    public float T;

	    /// <summary>
	    ///     The 3D point of the hit
	    /// </summary>
	    public Vector3 P;

	    /// <summary>
	    ///     The normal of the hit
	    /// </summary>
	    public Vector3 N;

	    /// <summary>
	    ///     The material of the hit object
	    /// </summary>
	    public IMaterial M;
    }
}
