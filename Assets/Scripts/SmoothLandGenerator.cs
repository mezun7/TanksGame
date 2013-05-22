using UnityEngine;
using System.Collections;

public class SmoothLandGenerator : MonoBehaviour
{
	public int noiseDensity = 10;
	public float flattingFactor = 2f;
	public float width = 100f;
	public float height = 30f;
	// Use this for initialization
	void Start ()
	{
		GetComponent<MeshFilter> ().mesh = GenerateLand (noiseDensity, flattingFactor, width, height);
		GenerateColliders (GetComponent<MeshFilter> ().mesh);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	/// <summary>
	/// Generates the land mesh.
	/// </summary>
	/// <returns>
	/// The land mesh.
	/// </returns>
	/// <param name='noiseDensity'>
	/// How much parts on the top of land.
	/// </param>
	/// <param name='width'>
	/// Width of the land.
	/// </param>
	/// <param name='height'>
	/// Height of the land.
	/// </param>
	public Mesh GenerateLand (int noiseDensity, float flattingFactor, float width, float height)
	{
		Mesh ret = new Mesh ();
		ret.name = "Land Mesh";
		
		float noiseHeightRange = height / noiseDensity / flattingFactor;
		float halfNoiseHeightRange = noiseHeightRange * 0.5f;
		
		//Vertices
		Vector3 lastVertix = new Vector3 (0f, height, 0f);
		Vector3[] vertices = new Vector3[noiseDensity * 2];
		
		for (int i = 0; i < vertices.Length; i += 2) {
			lastVertix += new Vector3 (width / noiseDensity, Random.Range (-halfNoiseHeightRange, halfNoiseHeightRange), 0f);
			vertices [i] = lastVertix;
			vertices [i + 1] = new Vector3 (lastVertix.x, 0f, lastVertix.z);
		}
		ret.vertices = vertices;
		
		// Triangles
		int[] triangles = new int[(vertices.Length - 2) * 3];
		
		for (int i = 0; i < triangles.Length; i += 3) {
			triangles [i] = (2 - 2 * (i % 2)) + i / 3;
			triangles [i + 1] = 1 + i / 3;
			triangles [i + 2] = (0 + 2 * (i % 2)) + i / 3;
		}
		
		ret.triangles = triangles;
		
		
		//ret.vertices = new Vector3[] {new Vector3 (1f, 0f, 0f),new Vector3 (0f, 1f, 0f),new Vector3 (0f, 0f, 0f)};
		//ret.triangles = new int[] {2,1,0};
		
		
		return ret;
	}

	void GenerateColliders (Mesh mesh)
	{
		Vector3[] vertices = mesh.vertices;
		
		for (int i = 0; i < vertices.Length-2; i += 2) {
			GameObject newGO = new GameObject ("Land Collider " + i, typeof(BoxCollider));
			newGO.transform.parent = transform;
			newGO.transform.localScale = new Vector3 (Vector3.Distance (vertices [i], vertices [i + 2]), 1f, 10f);
			newGO.transform.rotation = Quaternion.LookRotation (vertices [i + 2] - vertices [i]);
			newGO.transform.localPosition = vertices[i];
			//vertices[i]
		}
	}
}
