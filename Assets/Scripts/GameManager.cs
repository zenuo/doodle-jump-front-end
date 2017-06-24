using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	//静态实例
	public static GameManager INSTANCE;

	//Canvas实例
	public GameObject canvas;

	//会话id
	public string sessionId;

	//游戏界面实例
	public Gaming gaming;

	void Awake ()
	{
		INSTANCE = this;
		canvas = (GameObject)GameObject.Find ("Canvas");
		UIManager.INSTANCE.loadGaming ();
		gaming = UIManager.INSTANCE.gaming.GetComponent<Gaming> ();
		UIManager.INSTANCE.gaming.gameObject.SetActive (false);
	}

	void Start ()
	{
		//加载欢迎界面
		UIManager.INSTANCE.loadWelcome ();

		/*
		doodle = Doodle.create (doodleType);
		playerNum = 3;
		sessionId = HTTPUtil.signIn ("yz", "123456");
		Debug.Log (sessionId);
		PLAYERINFO = HTTPUtil.getPlayerInfo ();
		Debug.Log (PLAYERINFO.text ());
		Message message = Message.create ("{\"id\":null,\"scope\":0,\"source\":1,\"target\":1,\"createTime\":null,\"sendTime\":null,\"content\":\"Hello World!\"}");
		HTTPUtil.sendMessage (message);
		HTTPUtil.sendMessage (message);
		HTTPUtil.sendMessage (message);
		Debug.Log (HTTPUtil.getMessage ().Length);
		*/
	}
}
