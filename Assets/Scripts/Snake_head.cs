using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class Snake_head : MonoBehaviour {

	// 手机变量
	private Vector2 touchOrigin = -Vector2.one;

	// 关卡变量
	public static Snake_head instance = null;   
	public float levelStartDelay = 1.0f;
	private Text levelText;
	private GameObject levelImage;
	private int levelRange = 4;
	private static int level = 0;
	private bool doingSetup;
	private GameObject healthSetParent;
	private int wallDamage = 1;
	public int health = 20;

	// Dialog相关变量
	public GameObject dialogTextBox;
	public GameObject dialogImage;
	public Text dialogText;
	public string textFile;
	public string[] textLines;
	public int currentLine;
	public int endAtLine;
	private bool isInDialog;
	private bool hasShown = false;
	private float initSpeed = 1.0f;


	// 控制板变量
	public GameObject controlPanel;


	//By default the snake moves to right
	public float currentRotation;
	public float tailRotation;
	public List<Transform> tail = new List<Transform> ();
	public GameObject t;
	bool ate = false;
	bool rotated = false;
	public GameObject bodyPrefab;
	public GameObject tPrefab;
	public Vector2 curdir = Vector2.right;
	public Vector2 taildir = Vector2.left;
	public Image currentHealthBar;
	public Text ratioText;
	private float hitpoint = 150.0f;
	private float maxHitpoint = 150.0f;

	private Animator animator;

	private bool exit = false;
	private bool attack = false;
	private bool defence = false;
	private bool attackM = false;
	private bool treat = false;
	private bool magnet = false;
	private bool shield = false;
	private int shieldlast = 0;
	private int shieldCount = 0;
	private int magnetCount = 0;
	private int treatCount = 0;
	private int aggressCount = 0;
	private static bool dead = false;
	private bool hit = false;
	private static int cflag = 0;

	// 道具相关变量
	private string[] foodEffects = new string[] { "Speed Up", "Slow Down" };
	private float[] speedVals = new float[] { 0.6f, 0.4f, 0.2f, 0.15f, 0.1f };
	private int speedIndex;
	private float speed;
	private Text statusText;
	private float statusDisplayDelay = 5.0f;//effect existing time

	//audio things
	public GameObject fail_audio;
	//bullet things
	//defensive effect
	//public GameObject defenceEffect;
	//private bool hasDefenceEffect = false;

	//蛇发射子弹
	[SerializeField]
	private GameObject bulletPrefab;

	private void UpdateHealthBar(){
		float ratio = hitpoint / maxHitpoint;
		currentHealthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		ratioText.text = (ratio * 100).ToString () + '%';
	}

	public void TakeDamage(float damage){
		if (shield == false) {
			hitpoint -= damage;
			UpdateHealthBar ();
		} else {
			Debug.Log ("de " + shield);
		}
	}

	public void Heal(float health){
		hitpoint += health;
		UpdateHealthBar ();
	}


	void InitGame(){
		//Debug.Log (dead);
		if (dead == true) {
			level--;
			hit = true;
			dead = false;
		}
		// 关卡初始化
		LevelUpController();
		doingSetup = true;
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text>();
		levelText.text = "Tower Floor " + level;
		levelImage.SetActive (true);

		healthSetParent = GameObject.Find ("HealthSet");
		currentHealthBar = GameObject.Find ("health_lvl").GetComponent<Image> ();
		ratioText = GameObject.Find ("Text").GetComponent<Text> ();
		statusText = GameObject.Find ("StatusText").GetComponent<Text> ();


		dialogTextBox = GameObject.Find ("Dialog");
		dialogImage = GameObject.Find ("DialogImage");
		dialogText = GameObject.Find ("DialogText").GetComponent<Text>();
		isInDialog = true;
		dialogTextBox.SetActive (false);


		controlPanel = GameObject.Find ("ControlPanel");
		controlPanel.SetActive (false);


		healthSetParent.SetActive (false);

		Invoke ("HideLevelImage", levelStartDelay);


		// 蛇属性初始化
		speedIndex = 2;
		speed = speedVals[speedIndex];

		hitpoint = 150.0f;
		maxHitpoint = 150.0f;


	
	}

	void LevelUpController() {
		level++;
		if (level > levelRange) {
			level = 1;
		}
	}

	protected void AttackWall(Vector2 pos, Vector2 dir){
		Debug.Log ("Attack");
		Vector2 end = pos;



		/* End of Bullet Thing */


		if (curdir == Vector2.up) {
			pos = pos + new Vector2 (0.0f, 0.6f);
			end = pos + new Vector2 (0.0f, 0.5f);
		} else if (curdir == Vector2.down) {
			pos = pos + new Vector2 (0.0f, -0.6f);
			end = pos + new Vector2 (0.0f, -0.5f);
		} else if (curdir == Vector2.left) {
			pos = pos + new Vector2 (-0.6f, 0.0f);
			end = pos + new Vector2 (-0.5f, 0.0f);
		} else if (curdir == Vector2.right) {
			pos = pos + new Vector2 (0.6f, 0.0f);
			end = pos + new Vector2 (0.5f, 0.0f);
		}

		RaycastHit2D hit = Physics2D.Linecast (pos, end);

		if (hit.transform != null) {
			//Debug.Log ("Hit wall");
			//Debug.Log(hit.transform);
			Wall hitWall = hit.transform.GetComponent <Wall> ();
			if (hitWall != null) {
				Debug.Log ("Hit wall");
				hitWall.DamageWall (wallDamage);
				//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
				//animator.SetTrigger ("playerChop");
			}
		}
		attack = false;
	}

	private void HideLevelImage() {
		levelImage.SetActive (false);
		Invoke ("StartDialogBox", 0.0f);
	}

	private void StartDialogBox() {
		textFile = getDialogText();
		textLines = (textFile.Split ('\n'));

		dialogTextBox.SetActive (true);
		isInDialog = true;

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
	}

	private void EndDialogBox() {
		isInDialog = false;
		dialogTextBox.SetActive (false);
		healthSetParent.SetActive (true);
//		controlPanel.SetActive (true);
		doingSetup = false;
		//		speed = 0.2f;
		//Invoke ("Move", speed);
	}

	private string getDialogText() {
		switch (level) {
		case 1:
			return "Welcome to the Lady Snake!\nWelcome to the Lady Snake! This is a fantastic but challenging world.\nYou have 3 levels to go,\nYou have 3 levels to go, before facing the final boss, Fahai!\nMove the lady snake to avoid the attack from little monks,\nMove the lady snake to avoid the attack from little monks, and eat burgers to refill your health power.\nAre you ready?\n  ";
		
		case 2:
			return "Congradulations for coming to the 2nd floor!\nThe enemies are going to grow stronger...\nThe enemies are going to grow stronger... You would be chased by them!\nBut you can use more skills in the level.\nBut you can use more skills in the level. Fight!\n  ";

		case 3:
			return "Congradulations for coming to the 3rd floor!\nYou are one last step from the boss level...\nYou are one last step from the boss level... Come on!\n  ";

		default:
			return "Wow, you are reaching the end!\nYou are going to face the final boss, Fahai. \nBe smart...\nBe smart... Good luck!\n  ";
		}
	}



	// Use this for initialization
	void Start () {
		//Debug.Log("start");
		cflag++;
		if (instance == null) {
			instance = this;
		}

		InitGame();

		animator = GetComponent<Animator> ();
		t = (GameObject)Instantiate (tPrefab, new Vector2(transform.position.x-0.5f, transform.position.y), Quaternion.identity);
		Invoke ("Move", initSpeed);

		//UpdateHealthBar ();
	}

	// Update is called once per frame, calculate head rotation degree per frame
	void Update () {

		if (exit) {
			return;
		}

		if (isInDialog) {
			//			Invoke ("EndDialogBox", 0.3f);
			if (endAtLine == 0) {
				return;
			}
			if (currentLine > endAtLine) {
				currentLine = endAtLine;
			}
			dialogText.text = textLines [currentLine];

			#if UNITY_ANDROID
				Touch myTouch = Input.touches[0];
				if (myTouch.phase == TouchPhase.Began) {
					currentLine++;
				}
			#elif UNITY_STANDALONE || UNITY_WEBPLAYER
				if (Input.GetKeyDown (KeyCode.Return)) {
					currentLine++;
				}
			#endif

			if (currentLine == endAtLine) {
				Invoke ("EndDialogBox", 0.3f);
			}
			return;
		}


		int horizonal = 0;
		int vertical = 0;


		#if UNITY_ANDROID
		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0) {
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began) {
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}

			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;

				//Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;

				//Calculate the difference between the beginning and end of the touch on the y axis.
				float y = touchEnd.y - touchOrigin.y;

				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				touchOrigin.x = -1;

				//Check if the difference along the x axis is greater than the difference along the y axis.
				if (Mathf.Abs(x) > Mathf.Abs(y)) {
					horizonal = x > 0 ? 1 : -1;
				} else {
					vertical = y > 0 ? -1 : 1;
				}
			}
		}

		#elif UNITY_STANDALONE || UNITY_WEBPLAYER
		// Move in a new Direction?

		if (Input.GetKey(KeyCode.RightArrow))
			horizonal = 1;
		else if (Input.GetKey(KeyCode.DownArrow))
			vertical = 1;
		else if (Input.GetKey(KeyCode.LeftArrow))
			horizonal = -1;
		else if (Input.GetKey(KeyCode.UpArrow))
			vertical = -1;
		#endif

		hitpoint -= 1.0f * Time.deltaTime;
		if (horizonal > 0) {
			if (curdir == Vector2.up) {
				currentRotation += -90.0f;
				curdir = Vector2.right;
			} else if (curdir == -Vector2.up) {
				currentRotation += 90.0f;
				curdir = Vector2.right;
			} else {
				currentRotation += 0.0f;
			}
			rotated = true;
		} else if (vertical > 0) {
			if (curdir == -Vector2.right) {
				currentRotation += 90.0f;
				curdir = -Vector2.up;
			} else if (curdir == Vector2.right) {
				currentRotation += -90.0f;
				curdir = -Vector2.up;
			}else {
				currentRotation += 0.0f;
			}
			rotated = true;
		} else if (horizonal < 0) {
			if (curdir == Vector2.up) {
				currentRotation += 90.0f;
				curdir = -Vector2.right;
			} else if (curdir == -Vector2.up) {
				currentRotation += -90.0f;
				curdir = -Vector2.right;
			}else {
				currentRotation += 0.0f;
			}
			rotated = true;
		} else if (vertical < 0) {
			if (curdir == -Vector2.right) {
				currentRotation += -90.0f;
				curdir = Vector2.up;
			} else if (curdir == Vector2.right) {
				currentRotation += 90.0f;
				curdir = Vector2.up;
			}else {
				currentRotation += 0.0f;
			}
			rotated = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			AttackWall (transform.position, curdir);
			ShowStatus ("Attack the walls");
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			//Attack
			if (aggressCount > 0 && level < 4) {
				aggressCount--;
				ShowStatus ("Attack monk");
				ThrowBullet (0);
			} else if (level == 4) {
				ShowStatus ("Attack monk");
				ThrowBullet (0);
			}
		} else if (Input.GetKeyDown (KeyCode.W)) {
			//Treat
			if (treatCount > 0) {
				Heal (20.0f);
				treatCount--;
				ShowStatus ("Treat");
			}

		} else if (Input.GetKeyDown (KeyCode.E)) {
			//Magnet
			if (magnetCount > 0) {
				Vector2 headloc = transform.position;
				Collider2D[] hitColliders = Physics2D.OverlapCircleAll(headloc, 4.0f);
				int i = 0;
				//Debug.Log (headloc + " hit "+hitColliders.Length);

				while (i < hitColliders.Length)
				{
					if (hitColliders [i].name.StartsWith ("food")) {
						Destroy (hitColliders [i].gameObject);
						Heal (2.0f);
					}
					i++;
				}
				ShowStatus ("Magnet");
			}

		} else if (Input.GetKeyDown (KeyCode.R)) {
			//Shield
			if (shieldCount > 0) {
				shield = true;
				shieldCount--;
				//Invoke ("setShieldF", 3.0f);
				//Debug.Log ("test "+shield);
				ShowStatus ("Shield the attack of monk");
			}
		}
		if (hitpoint < 0) {
			hitpoint = 0.0f;
			exit = true;
			Application.LoadLevel("Scenes/GameOver");
			dead = true;
		}else if (hitpoint > 150) {
			hitpoint = 150.0f;
		}
		UpdateHealthBar ();
	}

	//Tail rotation degree calculation
	void calculateDir(Vector2 lastpos, Vector2 curpos){
		if (curpos.x - lastpos.x == 0.5f) {//Heading Right
			if (taildir == Vector2.up) {
				tailRotation += 90.0f;
			} else if (taildir == Vector2.down) {
				tailRotation += -90.0f;
			} else {
				tailRotation += 0.0f;
			}
			taildir = Vector2.left;
		} else if (curpos.x - lastpos.x == -0.5f) {//Heading Left
			if (taildir == Vector2.up) {
				tailRotation += -90.0f;
			} else if (taildir == Vector2.down) {
				tailRotation += 90.0f;
			} else {
				tailRotation += 0.0f;
			}
			taildir = Vector2.right;
		} else if (curpos.y - lastpos.y == 0.5f) {//Heading Up
			if (taildir == Vector2.right) {
				tailRotation += -90.0f;
			} else if (taildir == Vector2.left) {
				tailRotation += 90.0f;
			} else {
				tailRotation += 0.0f;
			}
			taildir = Vector2.down;
		} else if (curpos.y - lastpos.y == -0.5f) {
			if (taildir == Vector2.right) {
				tailRotation += 90.0f;
			} else if (taildir == Vector2.left) {
				tailRotation += -90.0f;
			} else {
				tailRotation += 0.0f;
			}
			taildir = Vector2.up;
		}
	}

	void Move(){
		if (exit) {
			return;
		}

		if (isInDialog) {
			Invoke ("Move", initSpeed);
			return;
		}

		Vector2 v = transform.position;
		if (ate) {
			GameObject g = (GameObject)Instantiate (bodyPrefab, v, Quaternion.identity);
			tail.Insert (0, g.transform);
			ate = false;
		} else if (tail.Count > 0) {
			Vector2 lastpos = tail.Last ().position;
			tail.Last ().position = v;
			tail.Insert (0, tail.Last ());
			tail.RemoveAt (tail.Count - 1);
			t.transform.position = lastpos;
			Vector2 curpos = tail.Last ().position;
			calculateDir (lastpos, curpos);
			t.transform.rotation = Quaternion.Euler(new Vector3(tPrefab.transform.rotation.x, tPrefab.transform.rotation.y, tailRotation));
		} else if (tail.Count == 0) {
			t.transform.position = v;
		}

		if (curdir == Vector2.right) {
			transform.position = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
		} else if (curdir == Vector2.up) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
		} else if (curdir == -Vector2.right) {
			transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z);
		} else if (curdir == -Vector2.up) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		}

		//Rotation of snake's head and tail
		if (rotated == true) {
			transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
			if (tail.Count == 0) {
				//tailRotation += currentRotation;
				taildir = curdir;
				t.transform.rotation = Quaternion.Euler(new Vector3(tPrefab.transform.rotation.x, tPrefab.transform.rotation.y, currentRotation));
				tailRotation = currentRotation;
			}
			rotated = false;
		}

		Invoke ("Move", speed);

	}

	//Collision with food
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.name.StartsWith ("food")) {

			animator.SetTrigger ("playerAttack");
			ate = true;

			//add sound for biting food
			GetComponent<AudioSource> ().Play ();

			Destroy (coll.gameObject);
			Heal (2.0f);

			// 随机产生食物效果
			int effectIndex = Random.Range (0, foodEffects.Length);
			Debug.Log ("Eat: " + effectIndex);
			switch (foodEffects [effectIndex]) {
			case "Speed Up": // 加速
				SpeedUp (foodEffects [effectIndex]);
				break;
			case "Slow Down": // 减速
				SlowDown (foodEffects [effectIndex]);
				break;
			default:
				break;
			}
		} else if (coll.name.StartsWith ("shield")) {
			//add sound for biting food
			GetComponent<AudioSource> ().Play ();
			Destroy (coll.gameObject);
			defence = true;
			/*
			if (hasDefenceEffect == false) {
				Vector2 pos = gameObject.transform.position;
				hasDefenceEffect = true;
				Instantiate (defenceEffect, pos, Quaternion.identity);
			}*/
			animator.SetTrigger ("snake_defensive");
			ShowStatus ("Defence");

		} else if (coll.name.StartsWith ("exit")) {
			exit = true;
			//	Debug.Log ("tail" + tail.Count);
			while (tail.Count > 0) {
				tail.RemoveAt (tail.Count - 1);
				//Invoke ("removeBody", 0.3f);
				Debug.Log ("tail" + tail.Count);
			}

//			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
//			SceneManager.LoadScene (0);
			Application.LoadLevel ("Scenes/1.0");
			dead = false;

		} else if (coll.name.StartsWith ("monk")) {
		} else if (coll.name.StartsWith ("boss")) {
		} else if (coll.name.StartsWith ("border")) {
			//fail_audio
			//Instantiate (fail_audio, new Vector2(0, 0), Quaternion.identity);
			//SceneManager.LoadScene( SceneManager.GetActiveScene().name );
			//SceneManager.LoadScene (0);
			Application.LoadLevel ("Scenes/GameOver");
			dead = true;
		} else if (coll.name.StartsWith ("potion_health")) {//治疗
			animator.SetTrigger ("playerAttack");
			treatCount++;
			//add sound for biting food
			GetComponent<AudioSource> ().Play ();
			Destroy (coll.gameObject);
			//Heal (50.0f);
		} else if (coll.name.StartsWith ("sword")) {//攻击和尚
			animator.SetTrigger ("playerAttack");
			aggressCount++;
			//add sound for biting food
			GetComponent<AudioSource> ().Play ();
			Destroy (coll.gameObject);
			//aggress function
		} else if (coll.name.StartsWith ("magnet")) {//吸铁石
			animator.SetTrigger ("playerAttack");
			magnetCount++;
			//add sound for biting food
			GetComponent<AudioSource> ().Play ();
			Destroy (coll.gameObject);
		} else if (coll.name.StartsWith ("avoid_monk")) {//盔甲
			animator.SetTrigger ("playerAttack");
			shieldCount++;
			//add sound for biting food
			GetComponent<AudioSource> ().Play ();
			Destroy (coll.gameObject);
		} else if (coll.name.StartsWith ("enemyBullet")) {
			Destroy (coll.gameObject);
			health--;
			if (health <= 0) {
				Application.LoadLevel("Scenes/GameOver");
				dead = true;
			}
		} else if(coll.name.StartsWith("bullet")){
		}else{
			//Debug.Log ("zhuang" + coll.name);
			if(defence == false){
				//Instantiate (fail_audio, new Vector2(0, 0), Quaternion.identity);
				//SceneManager.LoadScene( SceneManager.GetActiveScene().name );
				//SceneManager.LoadScene (0);
				Application.LoadLevel("Scenes/GameOver");
				dead = true;
			}
		} 
	}

	void SpeedUp(string info) {
		if (speedIndex < speedVals.Length - 1) {
			speed = speedVals [++speedIndex];
			ShowStatus (info);
		} else {
			ShowStatus ("Max Speed");
		}
	}

	void SlowDown(string info) {
		if (speedIndex > 0) {
			speed = speedVals [--speedIndex];
			ShowStatus (info);
		} else {
			ShowStatus ("Min Speed");
		}
	}

	void ShowStatus(string info) {
		statusText.text = info;
		Invoke ("HideStatus", statusDisplayDelay);
	}

	void HideStatus() {
		statusText.text = "";
		defence = false;
		shield = false;
		//Debug.Log ("Hide Status: " + statusText.text);
	}
	public int getTailCount(){
		return tail.Count;
	}

	public int getLevel(){
		return level;
	}
	public bool getHit(){
		return hit;
	}
	public int getC(){
		return cflag;
	}

	public void ThrowBullet (int value){
		if (curdir == Vector2.right) {
			GameObject tmp = (GameObject)Instantiate (bulletPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, -90)));
			tmp.GetComponent<bullet> ().Initialize (Vector2.right);
		} else if (curdir == Vector2.left) {

			GameObject tmp = (GameObject)Instantiate (bulletPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
			tmp.GetComponent<bullet> ().Initialize (Vector2.left);
		} else if (curdir == Vector2.up) {
			GameObject tmp = (GameObject)Instantiate (bulletPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			tmp.GetComponent<bullet> ().Initialize (Vector2.up);
		} else {
			GameObject tmp = (GameObject)Instantiate (bulletPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			tmp.GetComponent<bullet> ().Initialize (Vector2.down);
		}
	}
}