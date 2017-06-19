using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//静态实例
	public static GameManager INSTANCE;
	//游戏是否正常运行
	public bool isGaming = true;

	//Doodle
	Doodle doodle;

	//platform队列
	Queue<Platform> platforms = new Queue<Platform>();
	//platfrom位置
	Vector3 platformPosition = new Vector3();
	//字符串
	string[] platformString = {"0,1,1,1,1,1"};


	// Use this for initialization
	void Start () {
		INSTANCE = this;
		doodle = Doodle.create (3);
	}
	
	// Update is called once per frame
	void Update () {
		updatePlatform ();
	}

	// 创建platform
	void updatePlatform()
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
