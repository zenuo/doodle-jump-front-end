using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//静态实例
	public static GameManager INSTANCE;

	//游戏是否正常运行
	public bool isGaming = true;


	// Use this for initialization
	void Start () {
		INSTANCE = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
