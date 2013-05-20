using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	
	public static PauseMenu Instance {
		get {
			if (_instance == null) {
				_instance = Camera.main.GetComponent<PauseMenu> ();
			}
			return _instance;
		}
	}

	private static PauseMenu _instance;
	private bool isWon = false;
	private float oldTimeScale;
	private string _message;
	private bool isGamePaused = false;
	private GameObject soundPlayer;
	public AudioClip pauseMenuSound;
	// Use this for initialization
	void Start ()
	{
		
		
	}
	
	void OnGUI ()
	{		
		if (isWon) {
				 
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 180), _message + " Won! Our Congrats"); 
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "Main Menu")) {			
				Application.LoadLevel (0);
				
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 + 40, 180, 30), "Quit")) { 
				Application.Quit (); // выход      
			} 
		} else if (isGamePaused) {
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 180), "Pause menu"); 
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "New Game")) { 
				Time.timeScale = oldTimeScale;
				Application.LoadLevel (Application.loadedLevel);
				
			} 
			
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Return")) { 
				Time.timeScale = oldTimeScale;    
				isGamePaused = !isGamePaused;
				soundPlayer.audio.Stop();
				Destroy(soundPlayer);
			} 	
			
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2, 180, 30), "Options")) { 
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
		if (Input.GetKeyDown (KeyCode.Escape)&&!isWon) {
			if (isGamePaused) {
				Time.timeScale = oldTimeScale;
				soundPlayer.audio.Stop();
				Destroy(soundPlayer);
			} else {
				oldTimeScale = Time.timeScale;
				Time.timeScale = (float)0.0;
				soundPlayer = new GameObject ("Pause Menu Sound", typeof(AudioSource));
				soundPlayer.audio.clip = pauseMenuSound;
				soundPlayer.audio.Play();
			}
			isGamePaused = !isGamePaused;
		}
	}
	
	public void Win (string message)
	{
		isWon = true;	
		_message = message;
		GameObject[] tanks = GameObject.FindGameObjectsWithTag ("Tank");
		for (int i = 0; i < tanks.Length; i++) {
			tanks [i].GetComponent<TankMotion> ().enabled = false;
			tanks [i].transform.Find ("Canon").GetComponent<Canon> ().enabled = false;
		}
	}
	
}
