using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Gaming : MonoBehaviour
{

	public Button pause;

	public float timer;

	//地面类型
	public int platformType = 1;

	//地面道具类型
	public int platformPropType = 0;

	//地面位置
	public Vector3 platformPosition = new Vector3 ();

	//玩家状态
	public PlayerStatus playerStatus;

	//同一队伍的玩家的状态
	public PlayerStatus[] playerStatuses;

	//玩家人数
	public int playerNum = 1;

	//Doodle实例
	public Doodle doodle;

	//platform队列
	public Queue<Platform> platformQueue = new Queue<Platform> ();

	//platform信息队列
	public Queue<string> platformInfoQueue = new Queue<string> ();

	//是否需要从服务器加载地面信息
	public bool isNeedToLoadPlatformInfoFromServer = false;

	//Doodle类型
	public int doodleType;

	//玩家信息
	public PlayerInfo playerInfo = new PlayerInfo ();

	//队伍
	public Team team = new Team ();

	//生命
	public int life;

	//分数
	public int score = 0;

	//游戏状态
	public int gameStatus = 0;

	//是否游戏
	public bool isGaming;

	//加载地面信息的页数
	public int platfromInfoPage = 1;

	void Awake()
	{
		//添加按钮监听
		pause.onClick.AddListener (PauseTask);
	}

	void Update ()
	{
		//在线游戏时，定时推送和获取用户状态
		if (gameStatus == Constant.GAME_ONLINE) {
			if ((timer -= Time.deltaTime) <= 0F) {
				HTTPUtil.pull (playerStatuses);
				playerStatus.coin = playerInfo.coin;
				playerStatus.life = life;
				playerStatus.x = doodle.transform.position.x;
				playerStatus.y = doodle.transform.position.y;
				HTTPUtil.push (playerStatus);
			}
		}
		//判断死亡和游戏结束
		if (isGaming) {
			//生成地面
			spawnPlatform ();
			//判定生命
			if (doodle.transform.position.y <
			    Camera.main.transform.position.y -
			    Constant.SCENE_HEIGHT / 2 || 
				life <=0
			) {
				//生命减1
				life--;
				//判断生命是否大于等于1
				if (life >= 1) {
					doodle.rebirth ();
				} else {
					//生命小于1，游戏结束
					finalize ();
				}
			}
		}
	}

	//初始步骤
	public void initialize ()
	{
		Debug.Log ("Gaming initialize");
		isGaming = true;
		life = 1;
		score = 0;
		if (gameStatus == Constant.GAME_ONLINE) {
			playerStatus = new PlayerStatus (playerInfo.id);
		}
		doodle = Doodle.create (
			doodleType
		);
		UIManager.INSTANCE.loadPlayerPanel ();
	}

	//结束步骤
	public void finalize ()
	{
		isGaming = false;
		while (platformQueue.Count > 0) {
			platformQueue.Dequeue ().destroy ();
		}
		Destroy (doodle.gameObject);

		UIManager.INSTANCE.gaming.gameObject.SetActive (false);
		Destroy (UIManager.INSTANCE.panel1.gameObject);
		Destroy (UIManager.INSTANCE.avator1.gameObject);
		if (playerNum >= 2) {
			Destroy (UIManager.INSTANCE.panel2.gameObject);
			Destroy (UIManager.INSTANCE.avator2.gameObject);
		}
		if (playerNum == 3) {
			Destroy (UIManager.INSTANCE.panel3.gameObject);
			Destroy (UIManager.INSTANCE.avator3.gameObject);
		}

		//重置地面y坐标
		platformPosition.y = 0F;

		//重置摄像机位置
		Camera.main.transform.Translate (
			Constant.INITICAL_POSITION_OF_CAMERA
			-
			Camera.main.transform.position
		);

		UIManager.INSTANCE.loadGameOver ();
	}

	// 创建地面
	void spawnPlatform ()
	{
		//如果队列内地面数量不大于6
		if (platformQueue.Count <= Constant.NUMBER_OF_PLATFORMS_QUEUE - 1) {
			//Debug.Log ("platformQueue.Count <= Constant.NUMBER_OF_PLATFORMS_QUEUE - 1");
			if (gameStatus == Constant.GAME_ONLINE) {
				//在线地面生成
				//首先在零点创建一个地面
				Platform platform = Platform.create (
					                    platformType, 
					                    platformPropType, 
					                    platformPosition);
				//入队
				platformQueue.Enqueue (platform);

				//解析地面信息
				string[] tokens = platformInfoQueue.Dequeue ().Split (new string[] { "," }, StringSplitOptions.None);
				//地面类型
				platformType = int.Parse (tokens [0]);
				//道具类型
				platformPropType = int.Parse (tokens [1]);
				//设置地面位置，下一次执行使用
				platformPosition.x = float.Parse (tokens [2]);
				//platformPosition.x = Random.Range (-Constant.SCENE_WIDTH / 2, Constant.SCENE_WIDTH / 2);
				platformPosition.y += Constant.VERTICAL_DISTANCE_BETWEEN_PLATFORMS;

				//判断地面信息队列剩余数量
				if (platformInfoQueue.Count <= Constant.MIN_COUNT_OF_PLATFROM_INFO_QUEUE) {
					isNeedToLoadPlatformInfoFromServer = true;
				}
			} else if (gameStatus == Constant.GAME_OFFLINE) {
				//离线地面生成，首先在零点创建一个地面
				Platform platform = Platform.create (
					                    Platform.getPlatformType (),
					                    Platform.getPropType (),
					                    platformPosition
				                    );
				//入队
				platformQueue.Enqueue (platform);
				//设置地面位置，下一次执行使用
				platformPosition.x = UnityEngine.Random.Range (-Constant.SCENE_WIDTH / 2, Constant.SCENE_WIDTH / 2);
				platformPosition.y += Constant.VERTICAL_DISTANCE_BETWEEN_PLATFORMS;
			}
		}

		//如果队列中的队首地面在摄像机范围的下面
		if (platformQueue.Peek ().transform.position.y < Camera.main.transform.position.y - Constant.SCENE_HEIGHT / 2) {
			//出列并销毁
			Platform peak = platformQueue.Dequeue ();
			peak.destroy ();
		}
	}

	void PauseTask ()
	{
		Debug.Log ("Gaming: PauseTask");
		//改变时间缩放和按钮上的文字
		if (Time.timeScale > 0F) {
			Time.timeScale = 0F;
			pause.GetComponentInChildren<Text> ().text = "Resume"; 
		} else {
			Time.timeScale = 1F;
			pause.GetComponentInChildren<Text> ().text = "Pause"; 
		}
	}
}
