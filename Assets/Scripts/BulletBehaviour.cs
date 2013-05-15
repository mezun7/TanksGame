
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
		
		GameObject newBang = Instantiate (blowZone) as GameObject;
		newBang.transform.position = transform.position + new Vector3(0, -0.6f, 1f);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		
	}
	bool isOnGround() {
		RaycastHit hit;
		Physics.Raycast(transform.position, -Vector3.up, out hit);
        if (hit.distance < 0.6f) {                
			return true;
		}
		return false;
	}
}

