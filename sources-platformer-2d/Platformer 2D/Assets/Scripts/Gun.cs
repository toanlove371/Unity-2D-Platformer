using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	//public float speed = 20f;				// The speed the rocket will fire at.


	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	#region GunFather
	public GameObject bullet; 
	public float speed = 2.5f;
	public float cooldown = 0.3f;
	public float direction = 45f;
	public float damage = 5.0f;
	
	private bool isRapidFire = true;
	private float cooldownTick = 0;
	#endregion

	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();

	}


	void FixedUpdate ()
	{
		if(Input.GetButtonDown("Fire1")) {
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			GetComponent<AudioSource>().Play();

			if (playerCtrl.dirVertical == 0) {
				if (playerCtrl.facingRight == true) {
					direction = 0;
				}
				else {
					direction = 180;
				}
			}
			else {
				if (playerCtrl.dirVertical > 0) {
					if (playerCtrl.dirHorizontal > 0) {
						direction = 45;
					}
					else if (playerCtrl.dirHorizontal < 0) {
						direction = 135;
					}
					else if (playerCtrl.dirHorizontal == 0) {
						direction = 90;
					}
				}
				else if (playerCtrl.dirVertical < 0) {
					if (playerCtrl.dirHorizontal > 0) {
						direction = 315;
					}
					else if (playerCtrl.dirHorizontal < 0) {
						direction = 225;
					}
					else if (playerCtrl.dirHorizontal == 0) {
						direction = 270;
					}
				}
			}

			isRapidFire = true;
		}
		else {
			isRapidFire = false;
		}
		//GunFather
		RapidFire();

		// If the fire button is pressed...
//		if(Input.GetButtonDown("Fire1"))
//		{
//			// ... set the animator Shoot trigger parameter and play the audioclip.
//			anim.SetTrigger("Shoot");
//			GetComponent<AudioSource>().Play();
//
//
//			if (playerCtrl.dirHorizontal != 0 || playerCtrl.dirVertical != 0) {
//				Rigidbody2D bulletInstance = 
//					Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
//				bulletInstance.velocity = new Vector2(playerCtrl.dirHorizontal * speed,
//				                                      playerCtrl.dirVertical * speed);
//			} else {
//				if(playerCtrl.facingRight)
//				{
//					// ... instantiate the rocket facing right and set it's velocity to the right. 
//					Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
//					bulletInstance.velocity = new Vector2(speed, 0);
//				}
//				else
//				{
//					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
//					Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
//					bulletInstance.velocity = new Vector2(-speed, 0);
//				}
//			}


//			// If the player is facing right...
//			if(playerCtrl.facingRight)
//			{
//				// ... instantiate the rocket facing right and set it's velocity to the right. 
//				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
//				bulletInstance.velocity = new Vector2(speed, 0);
//			}
//			else
//			{
//				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
//				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
//				bulletInstance.velocity = new Vector2(-speed, 0);
//			}
//		}
	}

	#region GunFatherFunc
	void Fire () {
		GameObject bulletInstance
			= (GameObject)Instantiate(
				bullet, transform.position, Quaternion.Euler(new Vector3(0,0,180f)));
		bulletInstance.GetComponent<Rocket>().SetMove(direction, speed, damage);
	}

	void RapidFire () {
		if (isRapidFire) {
			if (cooldownTick <= 0) {
				Fire();
				cooldownTick = cooldown;
			} else {
				cooldownTick -= 0.01f;
			}
		}
		else if (cooldownTick > 0) {
			cooldownTick -= 0.01f;
		}
	}
	#endregion
}
