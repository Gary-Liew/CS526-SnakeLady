using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkChaseing : MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 20.0f;
	//public Snake player;
	public GameObject player;
	Transform target;
	public float speed;
	bool playerInRange;
	float timer;
	Animator animator;
	public float chaseRange;
	//发射子弹
	//public float attackRange;
	//public int damage;
	//private float lastAttackTime;
	//public float attackDelay;
	//public GameObject projectile;
	//public float bulletForce;
	//public Transform raycastPoint;
	public float rotSpeed = 90f;

	/*//发射子弹
	public Vector3 bulletOffSet = new Vector3(0,0.5f,0);
	public GameObject bulletPrefab;


	public float fireDelay = 0.5f;
	float coolDownTimer = 0;*/



	void OnTriggerStay2D(Collider2D other){
		if (other.name.StartsWith ("head")) {
			playerInRange = true;
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here 
			GetComponent<AudioSource>().Play ();
			animator.SetTrigger("monk1Attack");
			//			Debug.Log ("In range");
		} else if(other.name.StartsWith("body")){
			player.SendMessage ("TakeDamage", attackDamage*Time.deltaTime);
			// add here
			GetComponent<AudioSource>().Play ();
			animator.SetTrigger("monk1Attack");
			//Debug.Log (other.name);
		} else if (other.name.StartsWith ("bullet")) {
			Destroy (other.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("head");
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			GameObject go= GameObject.Find ("head");
			if (go != null) {
				target = go.transform;
			}
		}
		if (target == null)
			return;
		//chase the player AI
		float distanceToTarget = Vector3.Distance(transform.position,target.position);
		if (distanceToTarget < chaseRange) {
			//start chasing the player - turn and move towards target;
			Vector3 targetDir = target.position  - transform.position;
			float degree = Mathf.Atan2(targetDir.y,targetDir.x)*Mathf.Rad2Deg-90f;
			Quaternion newq = Quaternion.Euler (0, 0, degree);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,newq,rotSpeed*Time.deltaTime);
			transform.Translate(Vector3.up*Time.deltaTime*speed);
		}
		//face the player;
		/*Vector3 dir = target.position - transform.position;
		dir.Normalize ();
		float zAngle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 90f;
		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
		transform.rotation =  Quaternion.RotateTowards (transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
		coolDownTimer -= Time.deltaTime;
		if (coolDownTimer <= 0) {
			coolDownTimer = fireDelay;
			Vector3 offset = transform.rotation * bulletOffSet;
			GameObject bulletGo = (GameObject)Instantiate (bulletPrefab, transform.position + offset, transform.rotation);
			bulletGo.layer = gameObject.layer;
		}*/
	}
}
