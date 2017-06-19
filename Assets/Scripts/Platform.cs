using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	//类型
	public int type = 0;

	//速度
	public float xV = 0f;
	public float yV = 0f;

	//位置
	public float xP = 0f;
	public float yP = 0f;

	//道具
	public int propId = 0;

	//皮肤
	Transform skin;

	// Use this for initialization
	void Start () {
		
		//皮肤
		GameObject skinPrefab = Resources.Load<GameObject> ("platform/" + getName ());
		GameObject skinGameObject = Instantiate <GameObject> (skinPrefab);
		skin = skinGameObject.transform;
		skin.SetParent (this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	string getName()
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
}
