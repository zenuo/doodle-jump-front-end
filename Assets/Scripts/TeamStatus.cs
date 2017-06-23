using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatus : MonoBehaviour {

	public Text status;

	public Button cancle;

	public Button start;

	float timer = 1F;

	void Start () {
		cancle.onClick.AddListener (CancleTask);
		start.onClick.AddListener (StartTask);
	}

	void Update () {
		//更新队伍信息
		if ((timer -= Time.deltaTime) < 0) {
			timer = 1F; 
			GameManager.INSTANCE.team = HTTPUtil.getTeam ();
			status.text = string.Format (
				"Id: {0}\nPlayers Count: {1}\nIsOpen: {2}",
				GameManager.INSTANCE.team.id,
				GameManager.INSTANCE.team.players.Length,
				GameManager.INSTANCE.team.open
			);
			//检测队伍是否被锁定，如果被锁定，则开始游戏
			if (!GameManager.INSTANCE.team.open) {
				//开始游戏逻辑

			}
		}
	}

	void StartTask()
	{
		//请求服务器：锁定队伍
		GameManager.INSTANCE.gameStatus = Constant.GAME_ONLINE;
		Debug.Log ("TeamStatus: StartTask");
		HTTPUtil.lockTeam ();
	}

	void CancleTask()
	{
		Debug.Log ("TeamStatus: CancleTask");
		UIManager.INSTANCE.teamstatus.gameObject.SetActive (false);
		UIManager.INSTANCE.loadChooseMode ();
	}
}
