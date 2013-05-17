using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
	public TankMotion tank;
	public GameObject bullet_prefab;
	public float minBulletPower = 50f;
	public float maxBulletPower = 200f;
	private float powerFactor;
	public float cannonRotationSpeed = 0.5f;
	float lastShotTime;
	public float canonPower = 0f;
	public AudioClip shotSound;
	public int damage;
	// Use this for initialization
	void Start ()
	{
		lastShotTime = Time.time;
	}
	
	
	// Update is called once per frame
	void Update ()
	{	
		// Canon rotation
		transform.Rotate (0f, 0f, Input.GetAxis ("Canon Rotation") * cannonRotationSpeed); 
		if (transform.rotation.eulerAngles.z > 45f && transform.rotation.eulerAngles.z < 315f) {
			if (transform.rotation.eulerAngles.z <= 180f) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 45f);					
			} else {
				transform.localRotation = Quaternion.Euler (0f, 0f, 315f);					
			} 		
		}
		
		// Power adjusting
		switch (tank.player) {
		case Player.Player1:
			canonPower += Input.GetAxis ("Power1");
			break;
		case Player.Player2:
			canonPower += Input.GetAxis ("Power2");
			break;
		}
		
		if (canonPower < minBulletPower) {
			canonPower = minBulletPower;
		} else if (canonPower > maxBulletPower) {
			canonPower = maxBulletPower;
		}
		powerFactor = canonPower / maxBulletPower;
		
		// Shooting
		if (Time.time - lastShotTime > 0.2&&Time.timeScale!=0) {
			bool isFirePressed = false;
			
			switch (tank.player) {
			case Player.Player1:
				isFirePressed = Input.GetButton ("Fire1");
				break;
			case Player.Player2:
				isFirePressed = Input.GetButton ("Fire2");
				break;
			}
			
			if (isFirePressed) {
				Fire (canonPower);
				lastShotTime = Time.time;
			}	
		}
	}
	
	void PlaySound ()
	{
		GameObject canonGO = new GameObject ("Canon Sound", typeof(AudioSource));
		canonGO.audio.clip = shotSound;
		canonGO.audio.Play ();
		Destroy (canonGO, 1f);
	}
	
	
	void culcDamage()
	{
		damage = (int)((100*canonPower)/((maxBulletPower-minBulletPower)*5));
		Debug.Log("culc dam:"+damage);
	}
	
	void Fire (float power)
	{

		PlaySound ();
		culcDamage();
		BulletBehaviour newBullet = (Instantiate (bullet_prefab) as GameObject).GetComponent<BulletBehaviour> ();
  
  	newBullet.transform.position = transform.position + transform.rotation * new Vector3 (0f, 2.8f, 0f);
  	newBullet.rigidbody.AddForce (transform.rotation * new Vector3 (0, power, 0), ForceMode.Impulse);
  	newBullet.damage = damage;
		
	}

	void OnGUI ()
	{
		switch (tank.player) {
		case Player.Player1:
			{
				GUI.Box (new Rect (5, 30, 100, 20), "");
				GUI.Box (new Rect (5, 30, 100 * powerFactor, 20), "");
			}
			break;
		case Player.Player2:
			{
				GUI.Box (new Rect (Screen.width - 105, 30, 100, 20), "");			
				GUI.Box (new Rect (Screen.width - 105, 30, powerFactor * 100, 20), "");
			}
			break;
		}		
		
	}
}
