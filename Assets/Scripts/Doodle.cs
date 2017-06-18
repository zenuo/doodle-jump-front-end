using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Doodle控制
 */
public class Doodle : MonoBehaviour
{
	//静态变量
	public static Doodle INSTANCE;

	//初始速度
	const float initialXVelocity = 0f;
	const float initialYVelocity = 5f;

	//速度
	float xVelocity;
	float yVelocity;

	//运动
	Vector3 translation = new Vector3 ();

	//是否处于自控状态
	bool isUsingProp = false;

	//道具速度
	public float propVelocity = 0f;

	//道具计时
	public float propTime = 0f;

	//Doodle与摄像机y坐标的差值
	float yDifferenceOfDoodleAndCamera = 0f;

	// Use this for initialization
	void Start ()
	{
		INSTANCE = this;
		xVelocity = initialXVelocity;
		yVelocity = initialYVelocity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//判定Doodle离开摄像机下方
		if (this.transform.position.y < Camera.main.transform.position.y - 4f) {
			GameManager.INSTANCE.isGaming = false;
		}
		UpdatePosition ();
	}

	//更新位置
	void UpdatePosition ()
	{
		translation.x = Time.deltaTime * getXVelocity ();
		translation.y = Time.deltaTime * getYVelocity ();
		this.transform.Translate (translation);
		//更新摄像机y坐标
		yDifferenceOfDoodleAndCamera = this.transform.position.y - Camera.main.transform.transform.position.y;
		if (yDifferenceOfDoodleAndCamera > 0f) {
			Camera.main.transform.transform.Translate (new Vector3(0f, yDifferenceOfDoodleAndCamera, 0f));
		}
		//Doodle超出左边界
		if (this.transform.position.x < -2.8f) {
			translation.x = 5.6f;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		}
		//Doodle超出右边界
		if (this.transform.position.x > 2.8f) {
			translation.x = -5.6f;
			translation.y = 0f;
			this.transform.Translate (translation);
			return;
		}
	}

	//获取竖直速度
	float getYVelocity ()
	{
		if (!isUsingProp) {
			//若没有使用道具
			yVelocity += Time.deltaTime * Constant.G;
			return yVelocity;
		} else {
			//若使用道具
			if ((propTime -= Time.deltaTime) > 0) {
				//若道具未失效
				return propVelocity;
			} else {
				//若道具失效
				endUsingProp ();
				return 0f;
			}
		}
	}

	//获得水平速度
	float getXVelocity ()
	{
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			return -Constant.X_VELOCITY_OF_DOODLE;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			return Constant.X_VELOCITY_OF_DOODLE;
		}
		return 0f;
	}

	//碰撞检测
	void OnTriggerEnter2D ()
	{
		if (!isUsingProp) {
			xVelocity = initialXVelocity;
			yVelocity = initialYVelocity;
		}
	}

	//开始使用道具
	void startUsingProp (float propVelocity, float propDurationTime)
	{
		isUsingProp = true;
		this.propVelocity = propVelocity;
		this.propTime = propDurationTime;
	}

	//结束使用道具
	void endUsingProp ()
	{
		isUsingProp = false;
		propVelocity = 0f;
		propTime = 0f;
	}
}
