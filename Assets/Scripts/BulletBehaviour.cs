
using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject blowZone;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Floor") {
			GameObject newBang = Instantiate (blowZone) as GameObject;
			newBang.transform.position = transform.position + new Vector3(0, -1.5f, -6f);
		}
		
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

