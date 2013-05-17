using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject blowZone;
	public AudioClip blast;

	public int damage;
	// Use this for initialization
	void Start ()
	{		
		Canon canon = GameObject.Find("Canon").GetComponent<Canon>();
		damage = canon.damage;
		Debug.Log("Getted Damage"+damage);
		Debug.Log(rigidbody.velocity);
	}
	
	
	
	
	void LeaveSound ()
	{
		GameObject blastGO = new GameObject ("Blast Sound", typeof(AudioSource));
		blastGO.audio.clip = blast;
		blastGO.audio.Play ();
		Destroy (blastGO, 1f);
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Floor") {
			
			LeaveSound();
			GameObject newBang = Instantiate (blowZone) as GameObject;
			newBang.transform.position = transform.position + new Vector3 (0, -1.5f, -6f);
		}
		
		if (collision.collider.tag == "Tank") {
			LeaveSound();
			collision.collider.gameObject.SendMessage ("Damage", 10+damage);
		}
		
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

