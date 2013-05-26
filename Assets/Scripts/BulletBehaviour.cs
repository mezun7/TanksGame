using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject blowZone;
	public GameObject fireZone;
	public GameObject bulletCollisionZone;
	public AudioClip blast;
	public int damage;
	public float bulletPositionX = 0f;
	public float bulletPositionY = -1.5f;
	public float bulletPositionZ = -6f;

	// Use this for initialization
	void Start ()
	{		
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
			collision.collider.transform.parent.GetComponent<SmoothLandGenerator> ().AddHole(transform.position,new Vector3 (bulletPositionX,bulletPositionY,bulletPositionZ),collision.collider.transform.parent.GetComponent<SmoothLandGenerator>().GenerateLand());
			LeaveSound ();
			//GameObject newBang = Instantiate (blowZone) as GameObject;
			//newBang.transform.position = transform.position + new Vector3 (bulletPositionX, bulletPositionY, bulletPositionZ);
		} else if (collision.collider.tag == "Tank") {
			LeaveSound ();
			collision.collider.gameObject.SendMessage ("Damage", 10 + damage);
			GameObject newFire = Instantiate (fireZone) as GameObject;
			newFire.transform.position = transform.position + new Vector3 (bulletPositionX, bulletPositionY, bulletPositionZ);
		} else if (collision.collider.tag == "Bullet") {
			LeaveSound ();
			GameObject newBulletCollisionZone = Instantiate (bulletCollisionZone) as GameObject;
			newBulletCollisionZone.transform.position = transform.position + new Vector3 (bulletPositionX, bulletPositionY, bulletPositionZ);
		}
		
		
		
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

