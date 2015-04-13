using UnityEngine;
using System.Collections;

public class EnemyBaseClass : MonoBehaviour {

	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.

	
	protected SpriteRenderer ren;		// Reference to the sprite renderer.
	protected bool dead = false;		// Whether or not the enemy is dead.
	protected Score score;				// Reference to the Score script.
	
	
	void Awake()
	{
		AwakeBase ();
	}

	protected virtual void AwakeBase () {
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		score = GameObject.Find("Score").GetComponent<Score>();
	}
	
	void FixedUpdate ()
	{
		FixedUpdateBase ();

	}

	public virtual void FixedUpdateBase () {

		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public virtual void Hurt(int damage)
	{
		// Reduce the number of hit points by one.
		HP -= damage;
	}
	
	protected virtual void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();
		
		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}
		
		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy;
		
		// Increase the score by 100 points
		score.score += 100;
		
		// Set dead to true.
		dead = true;
		

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}
		
		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
		
		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;
		
		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}

}
