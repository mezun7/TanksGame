using UnityEngine;
using System.Collections;

public class BulletDeletion : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	bool isOnGround ()
	{//Проверка на нахождение на земле
		RaycastHit hit;
		Physics.Raycast (transform.position, -Vector3.up, out hit);
		if (hit.distance < 1.0f) {                
			return true;
		}
		return false;
	}
	
}
