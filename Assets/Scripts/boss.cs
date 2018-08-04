using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour {

	//private List <Vector3> usedPositions = new List<Vector3> ();
	private int callCount = 0; //max = 4
	private int monkCount = 0;
	private int hp = 15;
	private bool canAttack = false;
	private float hitpoint = 15.0f;
	private float maxHitpoint = 15.0f;
	private GameObject bar;
	private GameObject healthbar;

	
	public Image currentHealthBar;
	private GameObject defense;
	private GameObject boss_dead_audio;
	private GameObject boss_dead_blood;

	//发射子弹
	public Vector3 bulletOffSet = new Vector3(0,0.5f,0);
	public GameObject bulletPrefab;
	Transform target;
	public float fireDelay = 0.5f;
	float coolDownTimer = 0;
	public float rotSpeed = 90f;

	void InitItem(string objName, float x, float y, float scalex, float scaley){
		if (objName != null) {
			GameObject temp = Instantiate (Resources.Load (objName), new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
			temp.transform.localScale = new Vector3 (scalex, scaley, 1.0f);
			if(objName != "boss_send_monk"){
				monkCount++;
			}
		}
	}
	
	private void UpdateHealthBar(){
		float ratio = hitpoint / maxHitpoint;
		GameObject.Find("xiaoCanvas/bossblood").GetComponent<Image>().rectTransform.localScale = new Vector3 (ratio, 1, 1);
		Debug.Log("baifenbi " + ratio);
		//ratioText.text = (ratio * 100).ToString () + '%';
	}

	public void monkDie(){
		GetComponent<AudioSource>().Play ();
		monkCount--;
		Debug.Log (monkCount);

		if (monkCount == 0){
			Debug.Log ("Can attack the boss now");
			if (defense != null) {
				Destroy (defense);
				defense = null;
			}
			canAttack = true;
		} else if (monkCount < 0) {
			monkCount = 0;
		}
	}

	void parseData(string fileName){
		//Call InitItem to generate items
		using (StreamReader r = new StreamReader("./Assets/Level_Design/" + fileName + ".json"))
		{
			string json = r.ReadToEnd();
			JSONObject jobj = new JSONObject (json);
			for (int i = 0; i < jobj.list.Count; i++) {
				//GameObject t = objMatch (jobj.keys [i]);
				//Debug.Log (t);
				if (jobj.list [i].type == JSONObject.Type.OBJECT) {
					float x = 0.0f, y = 0.0f, scalex = 0.0f, scaley = 0.0f;
					for(int j = 0; j < jobj.list [i].list.Count; j++){
						string key = (string)jobj.list [i].keys[j];
						JSONObject k = (JSONObject)jobj.list [i].list[j];
						if (key == "x") {
							x = k.n;
						} else if (key == "y") {
							y = k.n;
						} else if (key == "scaleX") {
							scalex = k.n;
						} else if (key == "scaleY") {
							scaley = k.n;
						}
					}
					InitItem (jobj.keys [i], x, y, scalex, scaley);
				} else if (jobj.list [i].type == JSONObject.Type.ARRAY) {
					foreach(JSONObject ao in jobj.list [i].list){
						float x = 0.0f, y = 0.0f, scalex = 0.0f, scaley = 0.0f;
						for(int j = 0; j < ao.list.Count; j++){
							string key = (string)ao.keys[j];
							JSONObject k = (JSONObject)ao.list[j];
							if (key == "x") {
								x = k.n;
							} else if (key == "y") {
								y = k.n;
							} else if (key == "scaleX") {
								scalex = k.n;
							} else if (key == "scaleY") {
								scaley = k.n;
							}
						}
						InitItem (jobj.keys [i], x, y, scalex, scaley);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("fangxiaobing", 5.0f, 30.0f);
		bar = Instantiate (Resources.Load ("fahai_bar"), new Vector3 (7.5f, 2.2f, 0f), Quaternion.identity) as GameObject;
		bar.transform.SetParent(GameObject.Find ("xiaoCanvas").transform);
		healthbar = Instantiate (Resources.Load ("boss_lvl"), new Vector3 (6.5f, 2.22f, 0f), Quaternion.identity) as GameObject;
		healthbar.name = "bossblood";
		healthbar.transform.SetParent(GameObject.Find ("xiaoCanvas").transform);

	
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
		Vector3 dir = target.position - transform.position;
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
		}
	}

	void fangxiaobing(){
		canAttack = false;
		if (callCount == 0) {
			parseData ("boss1");
			Debug.Log ("Gong you" + monkCount + "ge he shang");
			defense =  Instantiate (Resources.Load ("fahai_defense"), new Vector3 (7.7f, 2.3f, 0f), Quaternion.identity) as GameObject;
			callCount++;
		} else if (callCount == 1) {
			parseData ("boss2");
			if (defense == null) {
				defense =  Instantiate (Resources.Load ("fahai_defense"), new Vector3 (7.7f, 2.3f, 0f), Quaternion.identity) as GameObject;
			}
			callCount++;
		} else if (callCount == 2) {
			parseData ("boss3");
			if (defense == null) {
				defense =  Instantiate (Resources.Load ("fahai_defense"), new Vector3 (7.7f, 2.3f, 0f), Quaternion.identity) as GameObject;
			}
			callCount++;
		} else if (callCount == 3) {
			callCount = 0;
			//Destroy (defense);
		}
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (other.name.StartsWith("bullet")) {
			Destroy (other.gameObject);
			if(canAttack == true){
				hp--;
				hitpoint -= 1.0f;
				UpdateHealthBar();
				Debug.Log("sheng xia " + hp + "dian xue");
				if(hp == 0){
					//GetComponent<AudioSource>().Play ();
					boss_dead_audio = Instantiate (Resources.Load ("boss_death_audio"), new Vector3 (7.5f, 2.2f, 0f), Quaternion.identity) as GameObject;
					boss_dead_blood = Instantiate (Resources.Load ("blood_monk"), new Vector3 (7.5f, 2.2f, 0f), Quaternion.identity) as GameObject;

					Destroy(gameObject);
					Destroy(bar);
					Destroy(healthbar);
				}
				if(hitpoint < 0.0f){
					hitpoint = 0.0f;
				}
			}
		} 
	}
}
