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

	//玩家信息
	public PlayerInfo playerInfo = new PlayerInfo ();

	//队伍
	public Team team = new Team();

	//玩家状态
	public PlayerStatus playerStatus = new PlayerStatus ();

	//同一队伍的玩家的状态
	public PlayerStatus[] playerStatuses;

	//玩家人数
	public int playerNum = 1;

	//游戏是否正常运行
	public bool isGaming;

	//游戏状态
	public int gameStatus = 0;

	//会话id
	public string sessionId;

	//Doodle实例
	public Doodle doodle;

	//Doodle类型
	public int doodleType;

	//platform队列
	public Queue<Platform> platformQueue = new Queue<Platform> ();

	//platform信息队列
	public Queue<string> platformInfoQueue = new Queue<string> ();

	//是否需要从服务器加载地面信息
	public bool isNeedToLoadPlatformInfoFromServer = false;

	//地面类型
	int platformType = 1;

	//地面道具类型
	int platformPropType = 0;

	//地面位置
	public Vector3 platformPosition = new Vector3 ();

	//加载地面信息的页数
	public int platfromInfoPage = 1;

	//分数
	public int score = 0;

	//生命
	public int life = 0;

	void Awake ()
	{
		INSTANCE = this;
		canvas = (GameObject)GameObject.Find ("Canvas");
		isGaming = false;
		life = 5;
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

	void Update ()
	{
		if (isGaming) {
			spawnPlatform ();
			//判定Doodle离开摄像机下方
			if (doodle.transform.position.y < Camera.main.transform.position.y - Constant.SCENE_HEIGHT / 2) {
				//生命减1
				life--;
				Debug.Log ("Life:" + life);
				//判断生命是否大于等于1
				if (life >= 1) {
					//重生Doodle
					doodle.rebrithTimer = 1F;
					//将Doodle移动到摄像机的位置
					doodle.transform.Translate (
						new Vector3 (0F, (float)score, 0F) - doodle.transform.position
					);
				} else {
					//生命小于1，游戏结束
					UIManager.INSTANCE.loadGameOver ();
				}
			}

		}
	}

	// 创建地面
	void spawnPlatform ()
	{
		//如果队列内地面数量不大于6
		if (platformQueue.Count <= Constant.NUMBER_OF_PLATFORMS_QUEUE - 1) {
			if (GameManager.INSTANCE.gameStatus == Constant.GAME_ONLINE) {
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
			} else if (GameManager.INSTANCE.gameStatus == Constant.GAME_OFFLINE) {
				//离线地面生成
				//首先在零点创建一个地面
				Platform platform = Platform.create (
					                    (int)UnityEngine.Random.Range (1F, 4F),
					                    Platform.getPropType (),
					                    platformPosition);
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
			peak.destroyChild ();
			Destroy (peak.gameObject);
		}
	}

	//更新状态
	void pushStatus ()
	{
		playerStatus.text ();
	}
}
