using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {

	public float invulnPeriod = 0;
	public int health = 1;
	float invulnTimer = 0;
	int correctLayer;

	void start(){
		correctLayer = gameObject.layer;
	}
	void onTriggerEnter2D(){
		health--;
		invulnTimer = invulnPeriod;
		gameObject.layer = 10;
	}
	void Update(){
		invulnTimer -= Time.deltaTime;
		if (invulnTimer <= 0) {
			gameObject.layer = correctLayer;
		}
		if (health <= 0) {
			Die ();
		}
	}
	void Die(){
		Destroy (gameObject);
	}
}
