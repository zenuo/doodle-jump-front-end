using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkin : MonoBehaviour {

	public Dropdown dropDown;

	public Button choose;

	public Button cancle;

	void Start () {
		choose.onClick.AddListener (ChooseTask);
		cancle.onClick.AddListener (CancleTask);
	}
		
	void ChooseTask()
	{
		//doodle类型赋值给GameManager
		GameManager.INSTANCE.gaming.doodleType = dropDown.value;
		UIManager.INSTANCE.chooseskin.gameObject.SetActive (false);
		//判断游戏类型
		if (GameManager.INSTANCE.gaming.gameStatus == Constant.GAME_ONLINE) {
			UIManager.INSTANCE.loadChooseMode ();
		} else if (GameManager.INSTANCE.gaming.gameStatus == Constant.GAME_OFFLINE) {
			UIManager.INSTANCE.loadGaming ();
			GameManager.INSTANCE.gaming.initialize ();
		}
	}

	void CancleTask()
	{
		Debug.Log ("SignUp: Cancle");
		UIManager.INSTANCE.chooseskin.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
	}
}
