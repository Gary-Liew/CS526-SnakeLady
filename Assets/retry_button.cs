using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class retry_button : MonoBehaviour {

	public int index;
	private Button myselfButton;

	void Start()
	{
		myselfButton = GetComponent<Button>();
		myselfButton.onClick.AddListener(() => actionToMaterial(index));
	}


	void actionToMaterial(int idx)
	{
		Debug.Log("change material to HIT  on material :  " + idx);
		// Reload the level
		Application.LoadLevel("Scenes/1.0");
	}
}
