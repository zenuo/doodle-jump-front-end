using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMode : MonoBehaviour {

	public Button create;

	public Button join;

	public Button cancle;

	void Start () {
		create.onClick.AddListener (CreateTask);
		join.onClick.AddListener (JoinTask);
		cancle.onClick.AddListener (CancleTask);
	}

	void CreateTask()
	{
		//创建队伍
		Debug.Log ("ChooseMode Create");
		GameManager.INSTANCE.gaming.team = HTTPUtil.createTeam ();
		//进入新建的队伍信息界面
		UIManager.INSTANCE.choosemode.gameObject.SetActive (false);
		UIManager.INSTANCE.loadTeamStatus ();
	}

	void JoinTask()
	{
		//加入队伍
		Debug.Log ("ChooseMode Join");
		UIManager.INSTANCE.choosemode.gameObject.SetActive (false);
		UIManager.INSTANCE.loadChooseTeam ();
	}

	void CancleTask()
	{
		Debug.Log ("ChooseMode Cancle");
		UIManager.INSTANCE.choosemode.gameObject.SetActive (false);
		UIManager.INSTANCE.loadSignIn ();
	}
}
