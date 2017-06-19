using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	//类型
	public int type = 0;

	//速度
	public float xV = 0f;
	public float yV = 0f;

	//道具
	public int propId = 0;

	//皮肤
	Transform skin;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
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
	public static Platform create(int type, float xV, float yV, Vector3 position)
	{
		GameObject platformPrefab = Resources.Load<GameObject> ("Platform");
		GameObject platformObject = Instantiate<GameObject> (
			platformPrefab,
			position,
			Quaternion.identity);
		Platform platform = platformObject.AddComponent<Platform> ();
		platform.xV = xV;
		platform.yV = yV;
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

	public void destrySkin()
	{
		Destroy (skin.gameObject);
	}
}
