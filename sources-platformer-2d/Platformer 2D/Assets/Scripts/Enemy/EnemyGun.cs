using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	public GameObject bullet; 
	public float speed = 0.5f;
	public float cooldown = 0.3f;
	public float direction = 45f;
	public float damage = 5.0f;

	private bool isRapidFire = true;
	private float cooldownTick = 0;

	// Use this for initialization
	void Start () {
		cooldownTick = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		RapidFire();
	}

	void Fire () {
		GameObject bulletInstance
			= (GameObject)Instantiate(
				bullet, transform.position, Quaternion.Euler(new Vector3(0,0,180f)));
		bulletInstance.GetComponent<EnemyBullet>().SetMove(direction, speed, damage);
	}

	public void SetRapidFire (bool fire) {
		isRapidFire = fire;
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
	}
}
