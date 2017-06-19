using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//静态实例
	public static GameManager INSTANCE;

	//游戏是否正常运行
	public bool isGaming = true;

	//Doodle
	GameObject doodle;

	public string doodleSkinName;

	// Use this for initialization
	void Start () {
		INSTANCE = this;
		GameObject doodlePrefab = Resources.Load<GameObject> ("Doodle");
		doodle = Instantiate<GameObject> (doodlePrefab);
		doodleSkinName = "ninja";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
