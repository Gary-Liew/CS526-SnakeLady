using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Running_Attack: MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 20.0f;
	//public Snake player;
	public GameObject player;
	public Transform[] patrolPoints;
	public float speed;
	bool playerInRange;
	float timer;
	Animator animator;
	Transform currentPatrolPoint;//where the enemy supposed to going.
	int currentPatrolIndex;

	void OnTriggerStay2D(Collider2D other){
		if (other.name.StartsWith ("head")) {
			playerInRange = true;
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here 
			animator.SetTrigger("monk1Attack");
//			Debug.Log ("In range");
		} else if(other.name.StartsWith("body")){
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here
			animator.SetTrigger("monk1Attack");
			Debug.Log (other.name);
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("head");
		animator = GetComponent<Animator> ();
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
	}

	void Update(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		//check if we have reached the patrol point.
		if (Vector3.Distance (transform.position, currentPatrolPoint.position) < .1f) {
			//we have reach the patrol point, get the next one;
			//check if we have anymore patrol points, if not go back to beginning;
			if (currentPatrolIndex + 1 < patrolPoints.Length) {
				currentPatrolIndex++;
			} else {
				currentPatrolIndex = 0;
			}
			currentPatrolPoint = patrolPoints[currentPatrolIndex];
		}
		//turn to face the current patrol points;
		Vector3 patrolPointDir = currentPatrolPoint.position-transform.position;
		float angle = Mathf.Atan2 (patrolPointDir.y,patrolPointDir.x)* Mathf.Rad2Deg-90f;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180f);

	}
}