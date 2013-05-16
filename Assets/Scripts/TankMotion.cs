using UnityEngine;
using System.Collections;

public class TankMotion : MonoBehaviour
{
	public TankMotion tank;
	public float speed = 20f;
	public int health = 100;
	public Player player;
	// Use this for initialization
	
	void OnGUI ()
	{ 
		GUI.Box (new Rect (5, 5, 100, 20), "Health: " + getHealth ()); 
	}
	
	void Start ()
	{
	
	}
	
	void Damage (int dmg)
	{
		health -= dmg;
		if (health <= 0) {
			Application.Quit ();
			Destroy (gameObject);	
		}
		
	}
	
	int getHealth ()
	{
		return health;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (isOnGround ()) {
			float movementFactor = 0f;
			switch (player) {
			case Player.Player1:
				movementFactor = Input.GetAxis ("Movement 1");
				break;
			case Player.Player2:
				movementFactor = Input.GetAxis ("Movement 2");
				break;
			}
			rigidbody.AddForce (new Vector3 (speed * movementFactor * Time.fixedDeltaTime, 0f, 0f), ForceMode.Impulse);
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

public enum Player
{
	Player1,
	Player2
}