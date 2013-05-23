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
	bool isGrounded;
	
	
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
	
	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Tank") {
			Damage ((int)collision.rigidbody.velocity.magnitude);
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
			_myRenderer.material.mainTextureOffset += new Vector2 (0.25f, 0f) * Mathf.Sign (rigidbody.velocity.x);
			lastFrameChange = Time.time;
		}
	}
	
	void FixedUpdate ()
	{
		if (transform.localRotation.eulerAngles.z > 45f && transform.rotation.eulerAngles.z < 315f) {
			if (transform.localRotation.eulerAngles.z <= 180f) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 45f);					
			} else {
				transform.localRotation = Quaternion.Euler (0f, 0f, 315f);					
			} 		
		}
		
		// (isOnGround ()) {	
		if (isGrounded) {
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
	
	void OnCollisionStay (Collision info)
	{
		if (info.collider.tag == "Floor") {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}
	
	void OnCollisionExit ()
	{
		isGrounded = false;
	}
	
	bool isOnGround ()
	{//Проверка на нахождение на земле
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit)) {
			if (hit.distance < 1f) {
				Debug.Log (name + " stays on " + hit.collider.name);
				return true;
			}
		}
		return false;
	}
}

public enum Player
{
	Player1,
	Player2
}