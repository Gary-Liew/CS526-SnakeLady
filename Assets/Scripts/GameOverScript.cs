using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
	void OnGUI()
	{/*
		const int buttonWidth = 80;
		const int buttonHeight = 40;
		if (
			GUI.Button(
				// Center in X, 1/3 of the height in Y
				new Rect(Screen.width / 2 - (buttonWidth / 2),
					(1 * Screen.height / 3) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight),
				"Retry!"
			)
		)
		{
			// Reload the level
			Application.LoadLevel("Scenes/1.0");
		}

		if (
			GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect(Screen.width / 2 - (buttonWidth / 2),
					(2 * Screen.height / 3) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight),
				"Back to menu")
		)
		{
			// Reload the level
			Application.LoadLevel("Scenes/Menu");
		}
		/*
		if (
			GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect(Screen.width / 2 - (buttonWidth / 2),
					(2 * Screen.height / 3) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight),
				new GUIContent(buttonText, buttonTexture))
		)
		{
			// Reload the level
			Application.LoadLevel("Scenes/Menu");
		}
		*/
	}

}