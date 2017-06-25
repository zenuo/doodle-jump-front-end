using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;

public class Gaming : MonoBehaviour
{
	//按钮
	public Button pause;
	public Button cancle;

	//是否已经初始化
	public bool isInitialized = false;

	//生成远程doodle对象的数组索引偏移量
	public int createRemoteDoodleArrayOffset = 0;

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

	//远程玩家doodle实例
	public Doodle remoteDoodle;

	//platform队列
	public Queue<Platform> platformQueue = new Queue<Platform> ();

	//platform信息队列
	public Queue<string> platformInfoQueue = new Queue<string> ();

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

	void Awake ()
	{
		//添加按钮监听
		pause.onClick.AddListener (PauseTask);
		cancle.onClick.AddListener (CancleTask);
	}

	void Update ()
	{
		if (isInitialized) {
			//在线游戏时，定时推送和获取用户状态
			if (gameStatus == Constant.GAME_ONLINE) {
				//检查地图信息队列，如果数量小于设定的最小值，则从服务器下载地图信息，并加入队列
				if (platformInfoQueue.Count <= Constant.MIN_COUNT_OF_PLATFROM_INFO_QUEUE) {
					HTTPUtil.getPlatformInfo ();
				}
				//获取其他队员状态
				new Thread (() => {
					Thread.CurrentThread.IsBackground = true;
					playerStatuses = HTTPUtil.pull ();
				}).Start ();
				//推送本地玩家的状态
				playerStatus.coin = playerInfo.coin;
				playerStatus.life = life;
				playerStatus.x = doodle.transform.position.x;
				playerStatus.y = doodle.transform.position.y;
				new Thread (() => {
					Thread.CurrentThread.IsBackground = true;
					HTTPUtil.push (playerStatus);
				}).Start ();
				try {
					//绘制远程玩家doodle
					remoteDoodle.transform.Translate (
						new Vector3 (playerStatuses [0].x, playerStatuses [0].y, 0F)
						-
						remoteDoodle.transform.position
					);
				} catch (System.Exception e) {
					Debug.Log (e.Message);
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
				    life <= 0) {
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
	}

	//初始步骤
	public void initialize ()
	{
		Debug.Log ("Gaming initialize");
		isGaming = true;
		life = 1;
		score = 0;
		//如果是在线游戏
		if (gameStatus == Constant.GAME_ONLINE) {
			//初始化本地玩家状态对象
			playerStatus = new PlayerStatus (playerInfo.id);
			//获取第一页地图
			if (platformInfoQueue.Count <= Constant.MIN_COUNT_OF_PLATFROM_INFO_QUEUE) {
				HTTPUtil.getPlatformInfo ();
			}
			//初始化远程玩家doodle对象
			playerNum = team.players.Length;
			playerStatuses = new PlayerStatus[playerNum - 1];
			remoteDoodle = Doodle.create (getTeamPlayerDoodleType ());
			remoteDoodle.gameObject.name = "RemoteDoodle";
			remoteDoodle.isDirvedLocal = false;
		}
		//初始化本地玩家doodle对象
		doodle = Doodle.create (
			doodleType
		);
		//完成初始化
		isInitialized = true;
		UIManager.INSTANCE.loadPlayerPanel ();
	}

	//结束步骤
	public void finalize ()
	{
		//重置游戏状态
		isGaming = false;

		//清空队列
		while (platformQueue.Count > 0) {
			platformQueue.Dequeue ().destroy ();
		}
		platformInfoQueue.Clear ();

		//销毁doodle
		Destroy (doodle.gameObject);

		//隐藏游戏界面
		UIManager.INSTANCE.gaming.gameObject.SetActive (false);

		//销毁头像和信息界面
		Destroy (UIManager.INSTANCE.panel1.gameObject);
		Destroy (UIManager.INSTANCE.avator1.gameObject);
		if (playerNum == 2) {
			Destroy (remoteDoodle.gameObject);
			Destroy (UIManager.INSTANCE.panel2.gameObject);
			Destroy (UIManager.INSTANCE.avator2.gameObject);
		}

		//重置玩家人数
		playerNum = 1;
		playerStatuses = null;

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

	//暂停游戏
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

	//退出游戏
	void CancleTask ()
	{
		finalize ();
		UIManager.INSTANCE.gaming.gameObject.SetActive (false);
		UIManager.INSTANCE.loadGameOver ();
	}

	//获取队友doodle类型
	public int getTeamPlayerDoodleType ()
	{
		foreach (int type in team.avators) {
			if (type != doodleType) {
				return type;
			}
		}
		return 0;
	}
}
