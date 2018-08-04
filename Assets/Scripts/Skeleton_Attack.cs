using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 20.0f;
	//public Snake player;
	public GameObject player;
	bool playerInRange;
	float timer;
	Animator animator;

	void OnTriggerStay2D(Collider2D other){
		if (other.name.StartsWith ("head")) {
			playerInRange = true;
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here 
			animator.SetTrigger("skeleton_Attack");

//			Debug.Log ("In range");

		} else if(other.name.StartsWith("body")){
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here
			animator.SetTrigger("skeleton_Attack");
			Debug.Log (other.name);
		} else if (other.name.StartsWith ("bullet")) {
			Destroy (other.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("head");
		animator = GetComponent<Animator> ();
	}
}