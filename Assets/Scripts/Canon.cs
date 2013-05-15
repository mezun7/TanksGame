using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour {
	
	public GameObject bullet_prefab;
	public float bulletPower = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButton ("Fire1")) {
			Fire(bulletPower);
		}
		if (Input.GetButton ("Fire2")) {
			transform.Rotate(0f, 0f,0.5f);
		}
	}

	void Fire (float power)
	{
		GameObject newBullet = Instantiate(bullet_prefab) as GameObject;
		newBullet.transform.position = transform.position;
		newBullet.rigidbody.AddForce (transform.rotation * new Vector3 (0, power, 0), ForceMode.Acceleration);
	}
}
