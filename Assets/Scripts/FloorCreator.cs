using UnityEngine;
using System.Collections;

public class FloorCreator : MonoBehaviour
{
	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;
	public Vector3[] normals;
	// Use this for initialization
	
	void Generate ()
	{
		
		newVertices = new Vector3[10];
		newUV = new Vector2[10];
		newTriangles = new int[10];
		normals = new Vector3[10];
		for (int i=0; i<10; i++) {
			newVertices [i] = new Vector3(Random.Range(0, 100),Random.Range(0, 100),Random.Range(0, 100));
			newTriangles [i] =(int) Random.Range(0, 10);
			newUV [i] = new Vector2(Random.Range(0, 100),Random.Range(0, 100));
			normals[i] = -Vector3.forward;
		}
		
	}
	
	void Start ()
	{
		/*
		Generate();
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = newVertices;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;
		mesh.normals = normals;
		*/
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
