using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	//类型
	int type = 0;

	//速度
	float xVelocity = 0f;
	float yVelocity = 0f;

	//位置
	float xPosition = 0f;
	float yPosition = 0f;

	//模型
	GameObject platform;

	//构造方法
	public Platform(int type, float xPosition, float yPosition, float xVelocity, float yVelocity){
		this.type = type;
		this.xPosition = xPosition;
		this.yPosition = yPosition;
		this.xVelocity = xVelocity;
		this.yVelocity = yVelocity;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
