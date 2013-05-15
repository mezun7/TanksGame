using UnityEngine;
using System.Collections;

public class TankMotion : MonoBehaviour
{
	public float speed = 20f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (isOnGround ()) {
			rigidbody.AddForce (new Vector3 (speed * Input.GetAxis ("Horizontal") * Time.fixedDeltaTime, 0f, 0f), ForceMode.Impulse);
		}
	}
	
	bool isOnGround ()
	{//Проверка на нахождение на земле
		RaycastHit hit;
		Physics.Raycast (transform.position, -Vector3.up, out hit);
		if (hit.distance < 0.5) {                
			return true;
		}
		return false;
	}
}
