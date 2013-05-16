using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
	
	public GameObject bullet_prefab;
	public float bulletPower = 200f;
	public float cannonRotationSpeed = 0.5f;
	public AudioClip clip;
	public float oldTimeScale;
	float lastShotTime;

	// Use this for initialization
	void Start ()
	{
		lastShotTime = Time.time;
		
	}
	
	
	void OnGUI(){
		if(Time.timeScale==0){
		GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 180), "Pause menu"); 
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "New Game")) { 
				Time.timeScale = oldTimeScale;
			Application.LoadLevel(Application.loadedLevel);
				
		} 
			
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Return")) { 
			Time.timeScale = oldTimeScale;    
		} 	
			
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 , 180, 30), "Options")) { 
			Application.LoadLevel (2);     
		} 
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 + 40, 180, 30), "Quit")) { 
			Application.Quit (); // выход      
		} 
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0) {
				oldTimeScale = Time.timeScale;
				Time.timeScale = (float)0.0;
			} else {
				Time.timeScale = oldTimeScale;
			}
		}
		
		
		if (Time.time - lastShotTime > 0.2) {			
			if (Input.GetButton ("Fire1")&&(Time.timeScale!=0)) {
				Fire (bulletPower);
				lastShotTime = Time.time;
			}		
		}
		
		transform.Rotate (0f, 0f, Input.GetAxis ("Canon Rotation") * cannonRotationSpeed); 
		if (transform.rotation.eulerAngles.z > 45f && transform.rotation.eulerAngles.z < 315f) {
			if (transform.rotation.eulerAngles.z <= 180f) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 45f);					
			} else {
				transform.localRotation = Quaternion.Euler (0f, 0f, 315f);					
			}
				
		}
		//Debug.Log(transform.rotation.eulerAngles.z);
	}

	void Fire (float power)
	{
		audio.PlayOneShot (clip);
		GameObject newBullet = Instantiate (bullet_prefab) as GameObject;
	
		newBullet.transform.position = transform.position + transform.rotation * new Vector3 (0f, 2.8f, 0f);
		newBullet.rigidbody.AddForce (transform.rotation * new Vector3 (0, power, 0), ForceMode.Impulse);
		
	}
}
