using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//静态实例
	public static GameManager INSTANCE;

	//用户信息
	public PlayerInfo playerInfo;

	//游戏是否正常运行
	public bool isGaming = true;

	//会话id
	public string sessionId;

	//Doodle
	public Doodle doodle;

	//platform队列
	Queue<Platform> platforms = new Queue<Platform>();

	//platfrom位置
	Vector3 platformPosition = new Vector3();

	//字符串
	string[] platformString = {"0,1,1,1,1,1"};

	//分数
	public int score = 0;

	// Use this for initialization
	void Start () {
		INSTANCE = this;
		doodle = Doodle.create (4);
		/*
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
	
	// Update is called once per frame
	void Update () {
		spawnPlatform ();
	}

	// 创建platform
	void spawnPlatform()
	{
		//如果队列内地面数量不大于6
		if (platforms.Count <= Constant.NUMBER_OF_PLATFORMS_QUEUE - 1) {
			//加载一个platform的prefab
			Platform platform = Platform.create ((int)Random.Range(0f, 3f), platformPosition);
			//设置platform位置
			platformPosition.x = Random.Range (-Constant.SCENE_WIDTH / 2, Constant.SCENE_WIDTH / 2);
			platformPosition.y += Constant.VERTICAL_DISTANCE_BETWEEN_PLATFORMS;
			platforms.Enqueue (platform);
		}
		//如果队列中的队首地面在摄像机范围的下面
		if (platforms.Peek ().transform.position.y < Camera.main.transform.position.y - Constant.SCENE_HEIGHT / 2) {
			//删除队首
			Platform peak = platforms.Dequeue ();
			peak.destroySkin ();
			Destroy (peak.gameObject);
		}
	}
}
