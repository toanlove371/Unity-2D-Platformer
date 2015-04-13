using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public GameObject explosion;		// Prefab of explosion effect.

	float direction;
	float speed;
	float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Move () {
		this.GetComponent<Rigidbody2D>().velocity =
			new Vector2(Mathf.Cos (Mathf.Deg2Rad * direction) * speed,
			            Mathf.Sin (Mathf.Deg2Rad * direction) * speed);
		this.transform.Rotate(Vector3.forward, direction + 180);
	}

	public void SetMove(float dir, float sp, float dam) {
		direction = dir;
		speed = sp;
		damage = dam;
		Move();
	}

	void Explode () {
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
		// Destroy itself
		Destroy(this.gameObject);
	}


	void OnTriggerEnter2D (Collider2D col) {
		// if it hit player
		if (col.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);
			Explode();
		}
	}
}
