using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject blowZone;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Floor") {
			
			audio.Play();
			GameObject newBang = Instantiate (blowZone) as GameObject;
			newBang.transform.position = transform.position + new Vector3 (0, -1.5f, -6f);
		}
		
		if (collision.collider.tag == "Tank") {
			
			
			audio.Play();
			collision.collider.gameObject.SendMessage ("Damage", 10);
		}
		
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

