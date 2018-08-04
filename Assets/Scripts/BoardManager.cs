using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour {

	public float exitx, exity;
	public GameObject food1, monk1, shield;
	public GameObject bg1, bg2, bg3, bgboss;
	//public GameObject fences, flower1, flower2, flower3, keng, road1, road2, stone1, stone2, tai, tombstone, tree1, water1, water2, water3, water4, well, wood1, exit;

	//public GameObject bg;
	private Transform boardHolder;
	private List <Vector3> usedPositions = new List<Vector3> ();
	//private SpriteRenderer spriteRenderer;
		
	void InitItem(string objName, float x, float y, float scalex, float scaley){
		if (objName != null) {
			GameObject temp = Instantiate (Resources.Load (objName), new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
			temp.transform.localScale = new Vector3 (scalex, scaley, 1.0f);
			usedPositions.Add (new Vector3 (x, y, 0f));
			if (objName == "fahaiboss") {
				temp.name = "boss";
			}
		}
	}

	void parseData(string fileName){
		//Call InitItem to generate items
//		using (StreamReader r = new StreamReader("./Assets/Level_Design/" + fileName + ".json"))
//		{
			//string json = r.ReadToEnd();
			string json = "";
		if (fileName.Equals ("level2")) {
			json = "{\n    \n\t  \"monk_crab\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -9.45,\n            \"y\": -3.2,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": -1.84,\n            \"y\": 2.52,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -6.0,\n            \"y\": 0.0,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 3.91,\n            \"y\": 1.79,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 4.17,\n            \"y\": -3.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -2.02,\n            \"y\": -2.13,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t}\n\t\t],\n    \"level2_tree\": [\n\n        {\n            \"x\": -9.84,\n            \"y\": 3.9,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -2.3,\n            \"y\": 3.3,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 6.6,\n            \"y\": -1.76,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 10.21,\n            \"y\": 0.44,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 10.75,\n            \"y\": -3.7,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"seawind\": [\n        {\n            \"x\": -8.42,\n            \"y\": 3.45,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        },\n        {\n            \"x\": -4.6,\n            \"y\": -3.8,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 0.8,\n            \"y\": -3.7,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n          {\n            \"x\": 1.14,\n            \"y\": 0.61,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        },\n          {\n            \"x\": 8.75,\n            \"y\": 3.07,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        },\n        {\n            \"x\": 7.7,\n            \"y\": -2.2,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n    ],\n    \"umbrella\": [\n        {\n            \"x\": -10.37,\n            \"y\": -4.21,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n        },\n        {\n            \"x\": -4.26,\n            \"y\": 2.35,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n        },\n        {\n            \"x\": 2.29,\n            \"y\": 4.27,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n        },\n        {\n            \"x\": 6.7,\n            \"y\": 1.5,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n        },\n        {\n            \"x\": -8,\n            \"y\": -1.74,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n        }\n    ]\n}";

		} else if (fileName.Equals ("level3")) {
			json = "{\n   \n\t  \"monk1\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -9.8,\n            \"y\": 1.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t  {\n\t\t\t\t\t  \"x\": -0.96,\n            \"y\": 0.76,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t  {\n\t\t\t\t\t  \"x\": 7.58,\n            \"y\": 2.23,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t  {\n\t\t\t\t\t  \"x\": 0.23,\n            \"y\": -3.0,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t  {\n\t\t\t\t\t  \"x\": 10.25,\n            \"y\": -1.41,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t}\n\t\t],\n    \"tree1\": [\n        {\n            \"x\": 9.1,\n            \"y\": 2.69,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n\t\t\t  {\n            \"x\": -1.09,\n            \"y\": 1.92,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": 1.25,\n            \"y\": 1.92,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": 0.03,\n            \"y\": 3.71,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": 0.03,\n            \"y\": 0.75,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": 2.47,\n            \"y\": 1.92,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n           {\n            \"x\": 0.03,\n            \"y\": 2.84,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n         {\n            \"x\": -7.27,\n            \"y\": -2.11,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n         {\n            \"x\": -7.27,\n            \"y\": -2.69,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": -7.27,\n            \"y\": -3.14,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": -7.27,\n            \"y\": -3.86,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        },\n        {\n            \"x\": -7.27,\n            \"y\": -4.21,\n            \"scaleX\": 1.923829,\n            \"scaleY\": 1.868342\n        }\n    ],\n    \"stone2\": [\n        {\n            \"x\": -9.942001,\n            \"y\": -4.510284,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.942001,\n            \"y\": -4.097258,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.942001,\n            \"y\": -3.68,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.942001,\n            \"y\": -3.202365,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.425718,\n            \"y\": -3.202365,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"wood1\": [\n        {\n            \"x\": -5.777312,\n            \"y\": -1.79119,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -6.293596,\n            \"y\": -1.79119,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -6.78,\n            \"y\": -1.79119,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.291745,\n            \"y\": -1.79119,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.842447,\n            \"y\": -1.79119,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.842447,\n            \"y\": -2.513987,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.842447,\n            \"y\": -3.099108,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.842447,\n            \"y\": -3.72,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -7.842447,\n            \"y\": -4.407028,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n          {\n            \"x\": -9.36,\n            \"y\": -3.72,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -8.43,\n            \"y\": -4.372609,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -8.875015,\n            \"y\": -4.372609,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.35688,\n            \"y\": -4.372609,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"well\": [\n        {\n            \"x\": 6.34,\n            \"y\": -3.34,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"tombstone\": [\n        {\n            \"x\": 0.1427417,\n            \"y\": 1.788378,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 8.558748,\n            \"y\": 2.357763,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"road2\": [\n        {\n            \"x\": -5.996,\n            \"y\": 2.56,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"road1\": [\n        {\n            \"x\": -0.06377184,\n            \"y\": 1.547446,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 8.38,\n            \"y\": 2.04,\n            \"scaleX\": 0.562398,\n            \"scaleY\": 0.4754368\n        }\n    ],\n    \"flower1\": [\n        {\n            \"x\": 8.010008,\n            \"y\": 1.963624,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 8.844371,\n            \"y\": 1.963624,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 8.42719,\n            \"y\": 1.963624,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n        \n    ],\n    \"water1\": [\n        {\n            \"x\": 6.90297,\n            \"y\": -3.71342,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 5.593985,\n            \"y\": -3.71342,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n        \n    ],\n    \"water4\": [\n        {\n            \"x\": -6.054,\n            \"y\": 2.618,\n            \"scaleX\": 0.6,\n            \"scaleY\": 0.6\n        }\n    ]\n}";

		} else if (fileName.Equals ("level1")) {
			json = "{\n    \n\t  \"monk_skeleton\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -8.3,\n            \"y\": -2.2,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n\t\t\t\t},\n\t\t\t  {\n\t\t\t\t\t  \"x\": 7.32,\n            \"y\": -3.42,\n            \"scaleX\": 1.5,\n            \"scaleY\": 1.5\n\t\t\t\t}\n\t\t],\n    \"level3_tree\": [\n\n        {\n            \"x\": -8.2,\n            \"y\": 4.1,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -6.9,\n            \"y\": -4.1,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 10,\n            \"y\": -2.8,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"oxBody\": [\n        {\n            \"x\": 0,\n            \"y\": 2,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"tombstone\": [\n        {\n            \"x\": -7.5,\n            \"y\": 3.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -10,\n            \"y\": -3.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -9.5,\n            \"y\": -3.7,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n          {\n            \"x\": 9.5,\n            \"y\": -4,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": -8.8,\n            \"y\": -3.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"oxHead\": [\n        {\n            \"x\": 8.6,\n            \"y\": 3.8,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n        {\n            \"x\": 0,\n            \"y\": 0,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n    \"monk_bat\": [\n        {\n            \"x\": 4,\n            \"y\": 0,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_batc\": [\n        {\n            \"x\": -9,\n            \"y\": -4,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ]\n}";

		} else if (fileName.Equals ("boss1")) {
			json = "{ \n\t  \"monk1\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -5.31,\n            \"y\": 2.13,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 2.58,\n            \"y\": 1.73,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 6.89,\n            \"y\": -3.07,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -7.46,\n            \"y\": -2.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -0.68,\n            \"y\": -0.56,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t}\n\t\t],\n    \"monk_walking\": [\n\n        {\n            \"x\": -5.97,\n            \"y\": -0.32,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_walkingp\": [\n\t\t\t\n        {\n            \"x\": -2.33,\n            \"y\": -3.83,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_walkingpp\": [\n\t\t\t\n        {\n            \"x\": 4.77,\n            \"y\": 2.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_chasing\": [\n        {\n            \"x\": -3.23,\n            \"y\": 0.02,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n    ],\n\t\"boss_send_monk\": [\n\t\t {\n\t\t\t\t\t  \"x\": -5.21,\n            \"y\": 2.23,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 2.48,\n            \"y\": 1.83,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 6.79,\n            \"y\": -3.17,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -7.56,\n            \"y\": -2.71,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -0.78,\n            \"y\": -0.56,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n        {\n\t\t\t\"x\": -3.13,\n            \"y\": -0.08,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        },\n        {\n            \"x\": -5.97,\n            \"y\": -0.32,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\t\t        {\n            \"x\": -2.33,\n            \"y\": -3.83,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\t\t   {\n            \"x\": 4.77,\n            \"y\": 2.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ]  \n}";

		} else if (fileName.Equals ("boss2")) {
			json = "{ \n\t  \"monk1\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -9.43,\n            \"y\": 3.11,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 9.74,\n            \"y\": 2.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 9.77,\n            \"y\": -3.53,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -2.65,\n            \"y\": -1.07,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -8.11,\n            \"y\": -0.02,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 3.36,\n            \"y\": -1.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t}\n\t\t],\n    \"monk_walkingpoo\": [\n\n        {\n            \"x\": -9.98,\n            \"y\": -1.39,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_walkingpo\": [\n\t\t\t\n        {\n            \"x\": 0.53,\n            \"y\": -2.94,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_chasing\": [\n        {\n            \"x\": 2.21,\n            \"y\": -0.25,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n    ],\n\t\"boss_send_monk\":[\n\t  {\n\t\t\t\t\t  \"x\": -9.43,\n            \"y\": 3.11,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 9.74,\n            \"y\": 2.81,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 9.77,\n            \"y\": -3.53,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -2.65,\n            \"y\": -1.07,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -8.11,\n            \"y\": -0.02,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": 3.36,\n            \"y\": -1.5,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\n\t\t {\n            \"x\": -9.98,\n            \"y\": -1.39,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\t\t {\n            \"x\": 0.53,\n            \"y\": -2.94,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\t\t       {\n            \"x\": 2.21,\n            \"y\": -0.25,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n\t\t]  \n}";

		} else if (fileName.Equals ("boss3")) {
			json = "{ \n\t  \"monk1\": [\n\t\t\t  {\n\t\t\t\t\t  \"x\": -5.62,\n            \"y\": -1.53,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 5.03,\n            \"y\": 3.44,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -2.06,\n            \"y\": 1.78,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t}\n\t\t],\n    \"monk_walkingpu\": [\n\n        {\n            \"x\": 5.47,\n            \"y\": 1.49,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_walkingpuu\": [\n\t\t\t\n        {\n            \"x\": -3.47,\n            \"y\": -3.34,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        }\n    ],\n\t  \"monk_chasing\": [\n        {\n            \"x\": 4.54,\n            \"y\": -3.63,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n    ],\n\t\"boss_send_monk\": [\n\t\n\t\t {\n\t\t\t\t\t  \"x\": -5.62,\n            \"y\": -1.53,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n          {\n\t\t\t\t\t  \"x\": 5.03,\n            \"y\": 3.44,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\t\t\t    {\n\t\t\t\t\t  \"x\": -2.06,\n            \"y\": 1.78,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n\t\t\t\t},\n\n\n        {\n            \"x\": 5.47,\n            \"y\": 1.49,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\t\t     {\n            \"x\": -3.47,\n            \"y\": -3.34,\n            \"scaleX\": 1,\n            \"scaleY\": 1\n        },\n\n\t\t      {\n            \"x\": 4.54,\n            \"y\": -3.63,\n            \"scaleX\": 0.8,\n            \"scaleY\": 0.8\n        }\n\n\n\t\t] \n}";

		}

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
//		}
	}

	void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;
	}

	public void showExit(){
//		Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
//		while (usedPositions.Contains (pos)) {
//			pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
//		}
//		Instantiate (exit, pos, Quaternion.identity);
//		usedPositions.Add (pos);
		InitItem ("exit", -0.8f, -0.4f, 1, 1);
	}

	public void showDefence(){
		//InitItem (shield, -2.0f, -1.0f, 3, 3);
		Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
		while (usedPositions.Contains (pos)) {
			pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
		}
		Instantiate (shield, pos, Quaternion.identity);
		usedPositions.Add (pos);
	}

	public void foodSpawn(){
		Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
		while (usedPositions.Contains (pos)) {
			pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0f);
		}
		Instantiate (food1, pos, Quaternion.identity);
		usedPositions.Add (pos);
	}

	public void SetupScene(int level){
		//Depend on level, maybe read arguments from json files to locate items
		//To be discussed
		GetComponent<AudioSource>().Play ();
		BoardSetup();
		if (level == 1) {
			Instantiate (bg1, new Vector2 (0.0f, 0.0f), Quaternion.identity);
			InitItem ("monk_chasing", -7.0f, 1.0f, 1.0f, 1.0f);
			InitItem ("monk_walking", 1.0f, 1.0f, 1.0f, 1.0f);
		} else if (level == 2) {
			Instantiate (bg2, new Vector2 (0.0f, 0.0f), Quaternion.identity);
			//InitItem ("monk_walking", 1.0f, 1.0f, 1.0f, 1.0f);
		} else if (level == 3) {
			Instantiate (bg3, new Vector2 (0.0f, 0.0f), Quaternion.identity);
			InitItem ("monk_walking", 1.0f, 1.0f, 1.0f, 1.0f);
		}
		if (level != 4) {
			string fileName = "level" + level.ToString ();
			parseData (fileName);
			//InvokeRepeating ("foodSpawn", 3, 4);
		} else {
			Instantiate (bgboss, new Vector2 (0.0f, 0.0f), Quaternion.identity);
			InitItem ("fahaiboss", 7.5f, 0.25f, 1.0f, 1.0f);
		}
	}
}
