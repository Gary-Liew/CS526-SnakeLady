using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	//public AudioClip chopSound1;                //1 of 2 audio clips that play when the wall is attacked by the player.
	//public AudioClip chopSound2;                //2 of 2 audio clips that play when the wall is attacked by the player.
	public Sprite dmgSprite;                    //Alternate sprite to display after Wall has been attacked by player.
	public int hp = 3;                          //hit points for the wall.
	public GameObject item1, item2, item3, item4;
	public GameObject blast_audio;
	public GameObject bomb;

	private SpriteRenderer spriteRenderer;      //Store a component reference to the attached SpriteRenderer.
	Animator animator;

	void Awake ()
	{
		//Get a component reference to the SpriteRenderer.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}


	//DamageWall is called when the player attacks a wall.
	public void DamageWall (int loss)
	{
		//Call the RandomizeSfx function of SoundManager to play one of two chop sounds.
		//SoundManager.instance.RandomizeSfx (chopSound1, chopSound2);
		Debug.Log("damage");
		//Set spriteRenderer to the damaged wall sprite.
		spriteRenderer.sprite = dmgSprite;
		GetComponent<AudioSource>().Play ();
		//Subtract loss from hit point total.
		hp -= loss;
		//If hit points are less than or equal to zero:
		if (hp <= 0) {
			//Disable the gameObject.
			animator.SetTrigger ("wall_explosion");
			Vector2 pos = gameObject.transform.position;
			Instantiate (blast_audio, pos, Quaternion.identity);
			Debug.Log("destroy");
			//xyield WaitForSeconds(1);
			//doSleep(10.0f);
			gameObject.SetActive (false);
			Instantiate (bomb, pos, Quaternion.identity);
			int itemno = Random.Range (1, 6);
			if (itemno == 1) {
				Instantiate (item1, pos, Quaternion.identity);
			} else if (itemno == 2) {
				Instantiate (item2, pos, Quaternion.identity);
			} else if (itemno == 3) {
				Instantiate (item3, pos, Quaternion.identity);
			} else if (itemno == 4) {
				Instantiate (item4, pos, Quaternion.identity);
			}
			Debug.Log (itemno);
		}

	}

	public IEnumerator doSleep(float time) {
		yield return new WaitForSeconds(time); // waits 3 seconds
	}
}
