using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class bullet : MonoBehaviour {
	[SerializeField]
	private float speed;
	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	//private int life1 = 1;
	//private int life2 = 1;
	//private int life3 = 2;

	public int damage = 1;
	public bool isEnemyShot = false;
	private GameObject blood_monk;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		myRigidbody.velocity = direction * speed;
	}
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D other){
//		Debug.Log ("name"+other.name);
//		if (other.name == "monk1(Clone)") {
//			life3--;
//		} else if (other.name == "monk_chasing") {
//			life1--;
//		} else if (other.name == "monk_walking") {
//			life2--;
//		}
//
//		if (life1 <= 0 || life2 <= 0 || life3 <= 0) {
//			Destroy (other.gameObject);
//		}
		if (other.name.StartsWith("monk")) {
			
			Destroy (other.gameObject);
			//new Vector3 (6.5f, 2.22f, 0f)
			blood_monk =  Instantiate (Resources.Load ("blood_monk"), other.gameObject.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
			//blood_monk =  Instantiate (Resources.Load ("blood_monk"),new Vector3 (6.5f, 2.22f, 0f), Quaternion.identity) as GameObject;
			int level = GameObject.Find ("head").GetComponent<Snake_head> ().getLevel ();
			if (level == 4) {
				GameObject.Find ("boss").GetComponent<boss> ().monkDie ();
			}
		} 
	}

	public void Initialize(Vector2 direction){
		this.direction = direction;
	}
	void OnBecameInVisible(){
		Destroy (gameObject);
	}
}