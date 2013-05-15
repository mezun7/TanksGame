using UnityEngine;
using System.Collections;

public class TankMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate(){
		if (isOnGround()){
			rigidbody.AddForce(new Vector3(20f * Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0f, 0f),ForceMode.Impulse);
		}
	}
	
	bool isOnGround() {
		RaycastHit hit;
		Physics.Raycast(transform.position, -Vector3.up, out hit);
        if (hit.distance < 1.0f) {                
			return true;
		}
		return false;
	}
}
