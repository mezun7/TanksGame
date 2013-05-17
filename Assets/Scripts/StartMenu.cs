using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	

	
	// Use this for initialization
	void Start ()
	{
	
	}
	void OnGUI ()
	{
		GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 180), "Main menu"); 
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 80, 180, 30), "Play")) { 
			Application.LoadLevel (1); 
		} 
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Options")) { 
			Application.LoadLevel (2);     
		} 
		if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 + 40, 180, 30), "Quit")) { 
			Application.Quit (); // выход      
		} 
	}
	// Update is called once per frame
	void Update ()
	{
		if(!audio.isPlaying){
		audio.Play();	
		}
	}
}
