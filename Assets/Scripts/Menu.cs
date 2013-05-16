using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	
	private float oldTimeScale;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	void OnGUI ()
	{
		if (Time.timeScale == 0) {
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 180), "Pause menu"); 
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "New Game")) { 
				Time.timeScale = oldTimeScale;
				Application.LoadLevel (Application.loadedLevel);
				
			} 
			
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Return")) { 
				Time.timeScale = oldTimeScale;    
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
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0f) {
				oldTimeScale = Time.timeScale;
				Time.timeScale = 0f;
			} else {
				Time.timeScale = oldTimeScale;
			}
		}
	}
}
