using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	//类型
	public int type = 0;

	//道具
	public int propId = 0;

	//皮肤
	Transform skin;

	//运动
	Vector3 translation = new Vector3();

	//被触发
	public bool isTriggered = false;

	// Use this for initialization
	void Start () {
		this.GetComponent <BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		updatePosition ();
	}

	//获取名称字符串
	string getName(int type)
	{
		switch (type)
		{
		case 0:
			return "stable";
			break;
		case 1:
			return "moving";
			break;
		case 2:
			return "unstable";
			break;
		default:
			return "stable";
			break;
		}
	}

	//创建platform
	public static Platform create(int type, Vector3 position)
	{
		GameObject platformPrefab = Resources.Load<GameObject> ("Platform");
		GameObject platformObject = Instantiate<GameObject> (
			platformPrefab,
			position,
			Quaternion.identity);
		
		Platform platform = platformObject.AddComponent<Platform> ();
		platform.type = type;
		platform.setSkin (type, platformObject, position);
		return platform;
	}

	//设置皮肤
	public void setSkin(int type, GameObject parent, Vector3 position)
	{
		//皮肤
		GameObject skinPrefab = Resources.Load<GameObject> ("platform/" + getName (type));
		GameObject skinGameObject = Instantiate <GameObject> (
			skinPrefab,
			position,
			Quaternion.identity);
		skin = skinGameObject.transform;
		skin.SetParent (parent.transform);
	}

	//销毁皮肤
	public void destroySkin()
	{
		Destroy (skin.gameObject);
	}

	//碰撞后的处理
	void OnTriggerEnter2D(Collider2D other)
	{
		//如果被玩家碰撞
		if (other.tag.Equals ("Player")) {
			isTriggered = true;	
		}
	}

	//更新位置
	void updatePosition()
	{
		//超出左边界
		if (this.transform.position.x < -Constant.SCENE_WIDTH / 2) {
			translation.x = Constant.SCENE_WIDTH;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		}
		//超出右边界
		if (this.transform.position.x > Constant.SCENE_WIDTH / 2) {
			translation.x = -Constant.SCENE_WIDTH;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		}
		if (type == 0) {
			return;
		} else if (type == 1) {
			translation.x = Constant.VELOCITY_OF_MOVING_PLATFORM * Time.deltaTime;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		} else if (type == 2 && isTriggered) {
			translation.x = 0f;
			translation.y = Constant.Y_VELOCITY_OF_UNSTABLE_PLATFORM * Time.deltaTime;
			this.transform.Translate (translation);
			return;
		}
	}
}
