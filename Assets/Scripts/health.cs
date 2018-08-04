using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {

	public int hp = 2;
	public bool isEnemy = true;

	void OnTriggerEnter2D(Collider2D collider){
		bullet shot = collider.gameObject.GetComponent<bullet> ();
		if (shot != null) {
			if (shot.isEnemyShot != isEnemy) {
				hp -= shot.damage;
				Destroy (shot.gameObject);
				if (hp <= 0) {
					Destroy (gameObject);
				}
			}
		}
	}
}
