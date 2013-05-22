using UnityEngine;
using System.Collections;

public class BulletCollisionMotion : MonoBehaviour {
public float animationFrequency = 1f;
	private float lastFrameChange;
	public float bulletCollisionPositionX = 0.25f;
	public float bulletCollisionPositionY = 0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	// Animation
		if (lastFrameChange + animationFrequency < Time.time) {
			renderer.material.mainTextureOffset += new Vector2 (bulletCollisionPositionX, bulletCollisionPositionY);
			if(renderer.material.mainTextureOffset.x>=1){
				Destroy(gameObject);
			}
			lastFrameChange = Time.time;
		}
	}
}
