using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	//静态实例
	public static UIManager INSTANCE;

	//面板
	public Transform welcome;
	public Transform signIn;
	public Transform signUp;
	public Transform gameover;
	public Transform chooseskin;
	public Transform gaming;
	public Transform choosemode;
	public Transform chooseteam;
	public Transform teamstatus;

	public Transform panel1;
	public Text text1;
	public Transform avator1;
	public Transform panel2;
	public Text text2;
	public Transform avator2;

	void Awake ()
	{
		INSTANCE = this;
	}

	//加载欢迎界面
	public void loadWelcome ()
	{
		if (welcome == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/welcome");
			welcome = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			welcome.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			welcome.gameObject.SetActive (true);
		}
	}

	//加载登录界面
	public void loadSignIn ()
	{
		if (signIn == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/signIn");
			signIn = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			signIn.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			signIn.gameObject.SetActive (true);
		}
	}

	//加载注册界面
	public void loadSignUp ()
	{
		if (signUp == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/signUp");
			signUp = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			signUp.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			signUp.gameObject.SetActive (true);
		}
	}

	//加载游戏结束界面
	public void loadGameOver ()
	{
		if (gameover == null) {
			GameObject prefab = Resources.Load <GameObject> ("panel/gameover");
			gameover = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			gameover.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			gameover.gameObject.SetActive (true);
		}
	}

	//加载皮肤选择界面
	public void loadChooseSkin ()
	{
		if (chooseskin == null) {
			GameObject prefab = Resources.Load <GameObject> ("panel/chooseskin");
			chooseskin = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			chooseskin.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			chooseskin.gameObject.SetActive (true);
		}
	}

	//加载游戏界面
	public void loadGaming ()
	{
		if (gaming == null) {
			GameObject prefab = Resources.Load <GameObject> ("panel/gaming");
			gaming = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			gaming.SetParent (GameManager.INSTANCE.canvas.transform);

		} else {
			gaming.gameObject.SetActive (true);
		}
	}

	//加载选择游戏模式
	public void loadChooseMode ()
	{
		if (choosemode == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/choosemode");
			choosemode = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			choosemode.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			choosemode.gameObject.SetActive (true);
		}
	}

	//加载选择队伍
	public void loadChooseTeam ()
	{
		if (chooseteam == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/chooseteam");
			chooseteam = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			chooseteam.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			chooseteam.gameObject.SetActive (true);
		}
	}

	//已创建的队伍信息
	public void loadTeamStatus ()
	{
		if (teamstatus == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/teamstatus");
			teamstatus = Instantiate<GameObject> (
				prefab,
				new Vector3 (360F, 512F, 0F),
				Quaternion.identity
			).transform;
			teamstatus.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			teamstatus.gameObject.SetActive (true);
		}
	}

	//加载玩家信息
	public void loadPlayerPanel ()
	{
		if (GameManager.INSTANCE.gaming.isGaming) {
			//加载玩家面板
			GameObject prefab1 = Resources.Load<GameObject> ("panel/player1");
			//生成
			panel1 = Instantiate<GameObject> (
				prefab1,
				new Vector3 (120f, 974f, 0f),
				Quaternion.identity
			).transform;
			//设置父对象
			panel1.transform.SetParent (GameManager.INSTANCE.canvas.transform);
			text1 = panel1.transform.GetComponentInChildren<Text> ();
			//头像
			avator1 = Instantiate<GameObject> (
				Resources.Load<GameObject> ("avator/" + Doodle.getSkinName (
					GameManager.INSTANCE.gaming.doodleType
				)),
				new Vector3 (-2.48f, 7.63f, 0f),
				Quaternion.identity
			).transform;
			avator1.transform.SetParent (Camera.main.transform);
			//加载队友
			if (GameManager.INSTANCE.gaming.playerNum == 2) {
				GameObject prefab2 = Resources.Load<GameObject> ("panel/player2");
				//生成
				panel2 = Instantiate<GameObject> (
					prefab2,
					new Vector3 (360, 974f, 0f),
					Quaternion.identity
				).transform;
				//设置父对象
				panel2.transform.SetParent (GameManager.INSTANCE.canvas.transform);
				text2 = panel2.transform.GetComponentInChildren<Text> ();
				//头像
				avator2 = Instantiate<GameObject> (
					Resources.Load<GameObject> ("avator/" + Doodle.getSkinName (GameManager.INSTANCE.gaming.getTeamPlayerDoodleType ())),
					new Vector3 (-0.624f, 7.63f, 0f),
					Quaternion.identity
				).transform;
				avator2.transform.SetParent (Camera.main.transform);
			}
		}
	}

	public void Update ()
	{
		if (GameManager.INSTANCE.gaming.isGaming && GameManager.INSTANCE.gaming.isInitialized) {
			text1.text = string.Format ("score: {0}\ncoin:{1}\nlife:{2}",
				GameManager.INSTANCE.gaming.score, 
				GameManager.INSTANCE.gaming.playerInfo.coin, 
				GameManager.INSTANCE.gaming.life);
			if (GameManager.INSTANCE.gaming.gameStatus == Constant.GAME_ONLINE) {
				try {
					if (GameManager.INSTANCE.gaming.playerStatuses.Length >= 1) {
						text2.text = string.Format ("score: {0}\ncoin:{1}\nlife:{2}",
							(int)GameManager.INSTANCE.gaming.playerStatuses [0].y, 
							GameManager.INSTANCE.gaming.playerStatuses [0].coin, 
							GameManager.INSTANCE.gaming.playerStatuses [0].life
						);
					}
				} catch (System.Exception e){
				}
			}
		}
	}
}
