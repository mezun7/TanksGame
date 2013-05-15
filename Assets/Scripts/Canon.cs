using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
	
	public GameObject bullet_prefab;
	public float bulletPower = 200f;
	public float cannonRotationSpeed = 0.5f;
	float lastShotTime;

	// Use this for initialization
	void Start ()
	{
		lastShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - lastShotTime > 0.2) {			
			if (Input.GetButton ("Fire1")) {
				Fire (bulletPower);
				lastShotTime = Time.time;
			}		
		}	
		transform.Rotate (0f, 0f, Input.GetAxis("Canon Rotation") * cannonRotationSpeed); 
		
	}

	void Fire (float power)
	{
		GameObject newBullet = Instantiate (bullet_prefab) as GameObject;
		newBullet.transform.position = transform.position + transform.rotation * new Vector3(0f, 2.8f, 0f);
		newBullet.rigidbody.AddForce (transform.rotation * new Vector3 (0, power, 0),ForceMode.Impulse);
	}
}
