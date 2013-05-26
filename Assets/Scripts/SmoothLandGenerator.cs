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
			newGO.tag = "Floor";
			Transform newGOTransform = newGO.transform;
			newGOTransform.parent = transform;
			newGOTransform.localScale = new Vector3 (Vector3.Distance (vertices [i], vertices [i + 2]), 1f, 10f);
			newGOTransform.rotation = Quaternion.LookRotation (vertices [i + 2] - vertices [i]);
			newGOTransform.Rotate (0f, 90f, 0f);
			newGOTransform.localPosition = vertices [i] - newGOTransform.rotation * new Vector3 (newGOTransform.localScale.x * 0.5f, newGOTransform.localScale.y * 0.5f, 0);
			//vertices[i]
		}
	}

	public Mesh AddHole (Vector3 pos, Vector3 direction, MeshFilter meshFilter)
	{
		float xPos=pos.x;
		Mesh ret = new Mesh ();
		ret.name = "Land Mesh";
		Vector3[] newVertices = new Vector3[meshFilter.mesh.vertices.Length + 2];
	
		int[] newTriangles = new int[meshFilter.mesh.triangles.Length + 2];
		Vector3 newVertex;		
		Vector3[] vertices = meshFilter.mesh.vertices;
		int i = 0;
		
		//new Vertices
		while (vertices[i].x < xPos) {
			newVertices [i] = vertices [i];		
			newVertices [i + 1] = vertices [i + 1];		
			i += 2;			
		}
		
		newVertex = pos + direction;
		newVertices [i] = newVertex;		
		newVertices [i + 1] = new Vector3 (newVertices [i].x, 0f, newVertices [i].z);
		
		i += 2;
		while (i<newVertices.Length-1) {
			newVertices [i + 1] = vertices [i];		
			newVertices [i + 2] = vertices [i + 1];		
			i += 2;			
		}
		ret.vertices = newVertices;
		
		// new Triangles
		
		newTriangles = meshFilter.mesh.triangles;
		
		newTriangles [newTriangles.Length - 6] = newTriangles.Length - 1;
		newTriangles [newTriangles.Length - 5] = newTriangles.Length - 2;
		newTriangles [newTriangles.Length - 4] = newTriangles.Length - 3;
		newTriangles [newTriangles.Length - 3] = newTriangles.Length;
		newTriangles [newTriangles.Length - 2] = newTriangles.Length - 2;
		newTriangles [newTriangles.Length - 1] = newTriangles.Length - 1;

		ret.triangles = newTriangles;
		return ret;
	}
}
