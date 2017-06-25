using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	//类型
	public int type = 0;

	//道具
	public int propId = 0;

	//皮肤
	Transform skin;

	//道具
	Transform prop;

	//运动
	Vector3 translation = new Vector3 ();

	//被触发
	public bool isTriggered = false;

	void Update ()
	{
		try {
			if (GameManager.INSTANCE.gaming.isGaming) {
				updatePosition ();
			}
		} catch (System.Exception e) {
			Debug.Log (e.StackTrace);
		}
	}

	//获取名称字符串
	string getName (int type)
	{
		switch (type) {
		case 1:
			return "stable";
		case 2:
			return "moving";
		case 3:
			return "unstable";
		default:
			return "stable";
		}
	}

	//获取道具名称
	string getPropName (int type)
	{
		switch (type) {
		case 1:
			return "coin";
		case 2:
			return "heart";
		case 3:
			return "mushroom";
		case 4:
			return "rocket";
		case 5:
			return "spring";
		case 6:
			return "hole";
		case 7:
			return "monster";
		case 8:
			return "ufo";
		default:
			return "coin";
		}
	}

	//创建platform
	public static Platform create (int type, int propType, Vector3 position)
	{
		GameObject platformPrefab = Resources.Load<GameObject> ("Platform");
		GameObject platformObject = Instantiate<GameObject> (
			                            platformPrefab,
			                            position,
			                            Quaternion.identity);
		
		Platform platform = platformObject.AddComponent<Platform> ();
		//设置父对象为gaming面板
		platform.transform.SetParent (UIManager.INSTANCE.gaming);
		platform.type = type;
		platform.setSkin (type, platformObject, position);
		platform.setProp (propType, platform.transform);
		return platform;
	}



	//随机生成道具
	public static int getPropType()
	{
		float randomValue = Random.value;
		if (randomValue < 0.05F) {
			return 1;
		} else if (randomValue >= 0.1F && randomValue < 0.11F) {
			return 2;
		} else if (randomValue >= 0.11F && randomValue < 0.13F) {
			return 3;
		} else if (randomValue >= 0.13F && randomValue < 0.14F) {
			return 4;
		} else if (randomValue >= 0.14F && randomValue < 0.16F) {
			return 5;
		} else if (randomValue >= 0.16F && randomValue < 0.17F) {
			return 6;
		} else if (randomValue >= 0.18F && randomValue < 0.19F) {
			return 7;
		} else if (randomValue >= 0.19F && randomValue < 0.2F) {
			return 8;
		} else {
			return 0;
		}
	}

	//随机生成地面类型
	public static int getPlatformType()
	{
		float randomValue = Random.value;
		if (randomValue < 0.5F) {
			return 1;
		} else if (randomValue >= 0.5F && randomValue < 0.8F) {
			return 2;
		} else {
			return 3;
		}
	}

	//设置皮肤
	public void setSkin (int type, GameObject parent, Vector3 position)
	{
		//皮肤
		GameObject skinPrefab = Resources.Load<GameObject> ("platform/" + getName (type));
		skin = Instantiate <GameObject> (
			skinPrefab,
			position,
			Quaternion.identity).transform;
		skin.SetParent (parent.transform);
	}

	public void setProp (int type, Transform parent)
	{
		if (type != 0) {
			Vector3 position = parent.transform.position;
			position.y += 0.20F;
			//如果是怪物，则修改x位置
			if (type > 5) {
				position.x += position.x >= 2F ? 2F : -2F;
			}
			GameObject propPrefab = Resources.Load<GameObject> ("prop/" + getPropName (type));
			Prop prop = Instantiate<GameObject> (
				propPrefab,
				position,
				Quaternion.identity
			).AddComponent<Prop> ();
			prop.transform.SetParent (parent);
		}
	}

	//销毁道具
	public void destroy ()
	{
		Destroy (skin.gameObject);
		//Destroy (prop.gameObject);
		Destroy (this.gameObject);
	}

	//碰撞后的处理
	void OnTriggerEnter2D (Collider2D other)
	{
		//如果被玩家碰撞
		if (other.name.Equals ("Doodle(Clone)") && !GameManager.INSTANCE.gaming.doodle.isUsingProp) {
			isTriggered = true;	
		}
	}

	//更新位置
	void updatePosition ()
	{
		//超出左边界
		if (this.transform.position.x < -Constant.SCENE_WIDTH / 2) {
			translation.x = Constant.SCENE_WIDTH;
			translation.y = 0F;
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
		if (type == 1) {
			//stable
			return;
		} else if (type == 2) {
			//moving
			translation.x = Constant.VELOCITY_OF_MOVING_PLATFORM * Time.deltaTime;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		} else if (type == 3 && isTriggered) {
			//unstable
			translation.x = 0f;
			translation.y = Constant.Y_VELOCITY_OF_UNSTABLE_PLATFORM * Time.deltaTime;
			this.transform.Translate (translation);
			return;
		}
	}
}
