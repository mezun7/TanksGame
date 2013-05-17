using UnityEngine;
using System.Collections;

public class TankMotion : MonoBehaviour
{
	public TankMotion tank;
	public float speed = 20f;
	public int health = 100;
	public Player player;
	public float animationFrequency = 1f;
	private float lastFrameChange;
	private Renderer _myRenderer;
	float movementFactor = 0f;
	
	
	// Use this for initialization
	void Start ()
	{
		_myRenderer = transform.Find ("graphics").renderer;
		transform.Find ("decal").renderer.material.color = new Color (Random.Range (0, 2), Random.Range (0, 2), Random.Range (0, 2));
	}
	
	void OnGUI ()
	{ 
		//GUI.Box (new Rect (5, 5, 100, 20), "Health: " + getHealth ()); 
		
		
		
		switch (player) {
		case Player.Player1:
			GUI.Box (new Rect (5, 5, 100, 20), "Health: " + getHealth ()); 
			
			break;
		case Player.Player2:
			GUI.Box (new Rect (Screen.width - 105, 5, 100, 20), "Health: " + getHealth ()); 
			break;
		}
	}
	
	void Damage (int dmg)
	{
		health -= dmg;
		if (health <= 0) {
			switch (player) {
			case Player.Player1:
				PauseMenu.Instance.Win ("Player2");
				break;
			case Player.Player2:
				PauseMenu.Instance.Win ("Player1");

				break;
			}
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
		// Animation
		if (lastFrameChange + animationFrequency < Time.time && Mathf.Abs (rigidbody.velocity.x) > 0.5f) {
			_myRenderer.material.mainTextureOffset += new Vector2 (0.25f, 0f) * Mathf.Sign(rigidbody.velocity.x);
			lastFrameChange = Time.time;
		}
	}
	
	void FixedUpdate ()
	{
		if (isOnGround ()) {
			
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