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

	public Transform panel1;
	public Text text1;
	public Transform avator1;
	public Transform panel2;
	public Text text2;
	public Transform avator2;
	public Transform panel3;
	public Text text3;
	public Transform avator3;

	void Awake()
	{
		INSTANCE = this;
	}

	//加载欢迎界面
	public void loadWelcome()
	{
		if (welcome == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/welcome");
			welcome = Instantiate<GameObject> (
				prefab,
				new Vector3 (360f, 512f, 0f),
				Quaternion.identity
			).transform;
			welcome.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			welcome.gameObject.SetActive (true);
		}
	}

	//加载登录界面
	public void loadSignIn()
	{
		if (signIn == null) {
			GameObject prefab = Resources.Load<GameObject> ("panel/signIn");
			signIn = Instantiate<GameObject> (
				prefab,
				new Vector3 (360f, 512f, 0f),
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
				new Vector3 (360f, 512f, 0f),
				Quaternion.identity
			).transform;
			signUp.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			signUp.gameObject.SetActive (true);
		}
	}

	//加载游戏结束界面
	public void loadGameOver()
	{
		if (gameover == null) {
			GameObject prefab = Resources.Load <GameObject>("panel/gameover");
			gameover = Instantiate<GameObject> (
				prefab,
				new Vector3 (360f, 512f, 0f),
				Quaternion.identity
			).transform;
			gameover.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			welcome.gameObject.SetActive (true);
		}
	}

	//加载皮肤选择界面
	public void loadChooseSkin()
	{
		if (chooseskin == null) {
			GameObject prefab = Resources.Load <GameObject>("panel/chooseskin");
			chooseskin = Instantiate<GameObject> (
				prefab,
				new Vector3(360f, 512f, 0f),
				Quaternion.identity
			).transform;
			chooseskin.SetParent (GameManager.INSTANCE.canvas.transform);
		} else {
			chooseskin.gameObject.SetActive (true);
		}
	}

	//加载游戏界面
	public void loadGaming()
	{
		if (gaming == null) {
			GameObject prefab = Resources.Load <GameObject>("panel/gaming");
			gaming = Instantiate<GameObject> (
				prefab,
				new Vector3(360f, 512f, 0f),
				Quaternion.identity
			).transform;
			gaming.SetParent (GameManager.INSTANCE.canvas.transform);

			GameManager.INSTANCE.isGaming = true;
			Debug.Log (GameManager.INSTANCE.isGaming);
			GameManager.INSTANCE.doodle = Doodle.create (GameManager.INSTANCE.doodleType);
			UIManager.INSTANCE.loadPlayerPanel ();
		} else {
			gaming.gameObject.SetActive (true);
		}
	}

	//加载玩家信息
	public void loadPlayerPanel ()
	{
		if (GameManager.INSTANCE.isGaming) {
			//加载玩家面板
			GameObject prefab1 = Resources.Load<GameObject> ("panel/player1");
			//生成
			panel1 = Instantiate<GameObject> (
				prefab1,
				new Vector3(120f, 974f, 0f),
				Quaternion.identity
			).transform;
			//设置父对象
			panel1.transform.SetParent (GameManager.INSTANCE.canvas.transform);
			text1 = panel1.transform.GetComponentInChildren<Text> ();
			//头像
			avator1 = Instantiate<GameObject>(
				Resources.Load<GameObject> ("avator/" + Doodle.getSkinName ()),
				new Vector3(-2.48f, 7.63f, 0f),
				Quaternion.identity
			).transform;
			avator1.transform.SetParent (Camera.main.transform);
			//加载队友
			if (GameManager.INSTANCE.playerNum >= 2) {
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
					Resources.Load<GameObject> ("avator/" + Doodle.getSkinName ()),
					new Vector3 (-0.624f, 7.63f, 0f),
					Quaternion.identity
				).transform;
				avator2.transform.SetParent (Camera.main.transform);
			}
			//加载队友
			if (GameManager.INSTANCE.playerNum == 3) {
				GameObject prefab3 = Resources.Load<GameObject> ("panel/player3");
				//生成
				panel3 = Instantiate<GameObject> (
					prefab3,
					new Vector3(600, 974f, 0f),
					Quaternion.identity
				).transform;
				//设置父对象
				panel3.transform.SetParent (GameManager.INSTANCE.canvas.transform);
				text3 = panel3.transform.GetComponentInChildren<Text> ();
				//头像
				avator3 = Instantiate<GameObject>(
					Resources.Load<GameObject> ("avator/" + Doodle.getSkinName ()),
					new Vector3(1.23f, 7.63f, 0f),
					Quaternion.identity
				).transform;
				avator3.transform.SetParent (Camera.main.transform);
			}

		}
	}

	public void Update ()
	{
		if (GameManager.INSTANCE.isGaming) {
			text1.text = string.Format ("score: {0}\ncoin:{1}\n", GameManager.INSTANCE.score, GameManager.INSTANCE.playerInfo.coin);
			if (GameManager.INSTANCE.playerNum >= 2) {
				text2.text = string.Format ("score: {0}\ncoin:{1}\n", GameManager.INSTANCE.score, GameManager.INSTANCE.playerInfo.coin);
			}
			if (GameManager.INSTANCE.playerNum >= 2) {
				text3.text = string.Format ("score: {0}\ncoin:{1}\n", GameManager.INSTANCE.score, GameManager.INSTANCE.playerInfo.coin);
			}
		}
	}

	public void DisActiveGaming()
	{
		UIManager.INSTANCE.gaming.gameObject.SetActive (false);
		UIManager.INSTANCE.avator1.gameObject.SetActive (false);
		UIManager.INSTANCE.panel1.gameObject.SetActive (false);
		if (GameManager.INSTANCE.playerNum >= 2) {
			UIManager.INSTANCE.panel2.gameObject.SetActive (false);
		} if (GameManager.INSTANCE.playerNum == 3) {
			UIManager.INSTANCE.panel3.gameObject.SetActive (false);
		}
	}
}
