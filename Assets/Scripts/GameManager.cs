using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;                      

	private static int level = 1;
	private int count;
	private bool condition = false;
	private int  levelFlag = level;

	private bool hit = false;
	private int cflag = 0;
	private int c = 0;
	//private string activeScene = "1.0";

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame(){
		boardScript.SetupScene(level);
		//Debug.Log ("init");
	}

	void InitExit(){
		boardScript.showExit ();
	}

	void InitDefence(){
		boardScript.showDefence ();
	}

	void SpawnFoods(){
		boardScript.foodSpawn ();
	}

	//Display total 3 shielfs per i*2+4 seconds
	void DisplayShield(int sCount){
		for (int i = 0; i < sCount; i++) {
			Invoke ("InitDefence", i * 2 + 4);
		}
	}	

	void Start () {
		//Debug.Log ("defence1");
		DisplayShield(3);
		if (level < 4) {
			InvokeRepeating ("SpawnFoods", 3, 4);
		}
		//Debug.Log ("defence2");

	}

	void Update () {
		//Debug.Log (SceneManager.GetActiveScene ().name);
		//activeScene = SceneManager.GetActiveScene ().name;
		if (SceneManager.GetActiveScene ().name == "Menu" || SceneManager.GetActiveScene ().name == "GameOver") {
			Debug.Log("cancelinvoke");
			CancelInvoke ("SpawnFoods");
			CancelInvoke ("InitDefence");
			CancelInvoke ("InitExit");
			//condition = false;
			//CancelInvoke ("DisplayShield");
			//DisplayShield (1,activeScene);
		} else if (SceneManager.GetActiveScene ().name == "1.0") {
			level = GameObject.Find ("head").GetComponent<Snake_head> ().getLevel ();
			count = GameObject.Find ("head").GetComponent<Snake_head> ().getTailCount ();
			int sCount = 4 - level;//total shield counts for every level
			hit = GameObject.Find ("head").GetComponent<Snake_head> ().getHit ();
			c = GameObject.Find ("head").GetComponent<Snake_head> ().getC ();
			if (levelFlag != level) {
				//Debug.Log (level);
				condition = false;
				//condition1 = false;
				InitGame ();
				DisplayShield (sCount);
				if (level == 4) {
					CancelInvoke ("SpawnFoods");
				}
			}
			if (hit == true && cflag != c) {
				condition = false;
				InitGame ();
				DisplayShield (sCount);
				InvokeRepeating ("SpawnFoods", 3, 4);
				if (level == 4) {
					CancelInvoke ("SpawnFoods");
				}

			}
			if (condition == false) {
				if (count == level) {
					Invoke ("InitExit", 1);
					condition = true;
				}
			}
			cflag = c;//flag used to check level change when hit the tree
			levelFlag = level; // levalFlag used to check level change
		}
	}

}
