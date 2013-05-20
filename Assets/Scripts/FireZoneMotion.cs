using UnityEngine;
using System.Collections;

public class FireZoneMotion : MonoBehaviour {
	public float animationFrequency = 1f;
	private float lastFrameChange;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	// Animation
		if (lastFrameChange + animationFrequency < Time.time) {
			renderer.material.mainTextureOffset += new Vector2 (0.25f, 0f);
			if(renderer.material.mainTextureOffset.x>=1){
				Destroy(gameObject);
			}
			lastFrameChange = Time.time;
		}
	}
}
