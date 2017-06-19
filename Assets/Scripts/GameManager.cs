using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//静态实例
	public static GameManager INSTANCE;
	//游戏是否正常运行
	public bool isGaming = true;

	//Doodle
	GameObject doodle;
	//doodle名称
	public string doodleSkinName;

	//platform队列
	Queue<Transform> platforms = new Queue<Transform>();
	//platfrom位置
	Vector3 platformPosition = new Vector3();
	//字符串
	string[] platformString = {"0,1,1,1,1,1"};


	// Use this for initialization
	void Start () {
		INSTANCE = this;
		//创建doodle
		GameObject doodlePrefab = Resources.Load<GameObject> ("Doodle");
		doodle = Instantiate<GameObject> (doodlePrefab);
		doodleSkinName = "normal";
	}
	
	// Update is called once per frame
	void Update () {
		updatePlatform ();
	}

	// 创建platform
	void updatePlatform()
	{
		if (platforms.Count <= 6) {
			//设置platform位置
			platformPosition.x = Random.value * Constant.SCENE_WIDTH / 2;
			platformPosition.y += Constant.VERTICAL_DISTANCE_BETWEEN_PLATFORMS;
			//加载一个platform的prefab
			GameObject platformPrefab = Resources.Load<GameObject> ("Platform");
			GameObject platformObject = Instantiate<GameObject> (
				platformPrefab,
				platformPosition,
				Quaternion.identity);
			Platform platform = platformObject.GetComponent<Platform> ();
			Debug.Log (platform.xP);
			platforms.Enqueue (platformObject.transform);
		}
	}
}
