using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class back_button_script : MonoBehaviour {
	
	private Button myselfButton;
	// Use this for initialization
	void Start () {
		myselfButton = GetComponent<Button>();
		myselfButton.onClick.AddListener(() => actionToMaterial(1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void actionToMaterial(int idx)
	{
		//Debug.Log("change material to HIT  on material :  " + idx);
		// Reload the level
		Application.LoadLevel("Scenes/Menu");
	}
}
