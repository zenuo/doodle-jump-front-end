using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Doodle控制
 */
public class Doodle : MonoBehaviour
{
	//类型
	public int type;

	//初始速度
	const float initialYVelocity = 8F;

	//速度
	float yVelocity;

	//运动
	Vector3 translation = new Vector3 ();

	//是否使用道具
	public bool isUsingProp = false;

	//道具速度
	float propVelocity = 0F;

	//道具计时
	float propTime = 0F;

	//Doodle与摄像机y坐标的差值
	float yDifferenceOfDoodleAndCamera = 0F;

	//皮肤
	Transform skin;

	//方向
	bool isRight = true;

	//水平速度因数
	float xVelocityFactor = 1F;

	//上一帧是否使用道具
	bool isLastFrameUsedProp = false;

	//重生计时
	public float rebrithTimer = -1F;

	void Start ()
	{
		yVelocity = initialYVelocity;
	}

	void Update ()
	{
		if (GameManager.INSTANCE.gaming.isGaming) {
			updatePosition ();
			updateSkin ();
		}
	}

	//更新皮肤
	void updateSkin ()
	{
		bool isLeftKeyDown = Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow);
		bool isRightKeyDown = Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow);

		if (isRight && isLeftKeyDown) {
			//当前方向右且左键按下
			isRight = false;
			changeSkinDirection ();
		} else if (!isRight && isRightKeyDown) {
			//当前方向不向右且右键按下
			isRight = true;
			changeSkinDirection ();
		}
	}

	//改变皮肤方向
	void changeSkinDirection ()
	{
		Vector3 scale = this.skin.localScale;
		scale.x *= -1;
		this.skin.localScale = scale;
	}

	//更新位置
	void updatePosition ()
	{
		translation.x = Time.deltaTime * getXVelocity ();
		translation.y = Time.deltaTime * getYVelocity ();
		this.transform.Translate (translation);
		//更新摄像机y坐标
		yDifferenceOfDoodleAndCamera = this.transform.position.y - Camera.main.transform.transform.position.y;
		if (yDifferenceOfDoodleAndCamera > 0F) {
			Camera.main.transform.transform.Translate (new Vector3 (0F, yDifferenceOfDoodleAndCamera, 0F));
		}
		//Doodle超出左边界
		if (this.transform.position.x < -Constant.SCENE_WIDTH / 2F) {
			translation.x = Constant.SCENE_WIDTH;
			translation.y = 0F;
			this.transform.Translate (translation);
			return;
		}
		//Doodle超出右边界
		if (this.transform.position.x > Constant.SCENE_WIDTH / 2F) {
			translation.x = -Constant.SCENE_WIDTH;
			translation.y = 0F;
			this.transform.Translate (translation);
			return;
		}

		//更新分数
		if (this.transform.position.y > GameManager.INSTANCE.gaming.score) {
			GameManager.INSTANCE.gaming.score = (int)this.transform.position.y;
		}
	}

	//获取竖直速度
	float getYVelocity ()
	{
		//判断是否使用道具
		if (!isUsingProp) {
			//未使用
			if (isLastFrameUsedProp) {
				//未使用道具且上一帧未使用道具
				isLastFrameUsedProp = false;
				yVelocity = Constant.VELOCITY_OF_PROP;
				return Constant.VELOCITY_OF_PROP;
			} else if (rebrithTimer < 0F) {
				//Debug.Log ("!isUsingProp && rebrithTimer < 0F");
				yVelocity += Time.deltaTime * Constant.G;
				return yVelocity;
			} else {
				//处于重生状态
				//Debug.Log ("rebrithTimer > 0F");
				rebrithTimer -= Time.deltaTime;
				return Constant.VELOCITY_OF_PROP;
			}
		} else {
			//使用道具
			if ((propTime -= 0.02F) > 0) {
				//Debug.Log ("(propTime -= 0.02F) > 0");
				//若道具未失效
				isLastFrameUsedProp = true;
				return propVelocity;
			} else {
				//若道具失效
				Debug.Log ("Prop expired.");
				endUsingProp ();
				//isLastFrameUsedProp = true;
				return Constant.VELOCITY_OF_PROP;
			}
		}
	}

	//获得水平速度
	float getXVelocity ()
	{
		bool isLeftKey = Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow);
		bool isRightKey = Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow);
		if (isLeftKey) {
			//左键被按下
			return -Constant.X_VELOCITY_OF_DOODLE * xVelocityFactor;
		}
		if (isRightKey) {
			//右键被按下
			return Constant.X_VELOCITY_OF_DOODLE * xVelocityFactor;
		}
		return 0F;
	}

	//碰撞检测
	void OnTriggerEnter2D (Collider2D other)
	{
		if (!isUsingProp && other.tag.Equals ("Platform")) {
			yVelocity = initialYVelocity;
		}
		if (!isUsingProp
		    &&
		    other.tag.Equals ("Prop")
		    &&
		    rebrithTimer < 0F) {
			switch (other.name) {
			case "coin(Clone)":
				GameManager.INSTANCE.gaming.playerInfo.coin += 200;
				break;
			case "heart(Clone)":
				GameManager.INSTANCE.gaming.life++;
				break;
			case "mushroom(Clone)":
				isUsingProp = true;
				propTime = 3F;
				xVelocityFactor = 0F;
				break;
			case "rocket(Clone)":
				isUsingProp = true;
				propTime = 5F;
				propVelocity = 30F;
				break;
			case "spring(Clone)":
				isUsingProp = true;
				propTime = 1F;
				propVelocity = 30F;
				break;
			case "hole(Clone)":
				GameManager.INSTANCE.gaming.life -= 3;
				break;
			case "monster(Clone)":
				GameManager.INSTANCE.gaming.life -= 1;
				break;
			case "ufo(Clone)":
				GameManager.INSTANCE.gaming.life -= 2;
				break;
			default:
				break;
			}
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
		propVelocity = 0F;
		propTime = 0F;
		xVelocityFactor = 1F;
	}

	//创建Doodle
	public static Doodle create (int type)
	{
		//创建doodle
		GameObject doodlePrefab = Resources.Load<GameObject> ("Doodle");
		GameObject doodleObject = Instantiate<GameObject> (
			                          doodlePrefab
		                          );
		Doodle doodle = doodleObject.AddComponent<Doodle> ();
		doodle.type = type;
		doodle.setSkin (doodleObject);
		doodle.transform.SetParent (UIManager.INSTANCE.gaming);
		return doodle;
	}

	//设置皮肤
	public void setSkin (GameObject doodleObject)
	{
		//皮肤
		GameObject skinPrefab = Resources.Load<GameObject> ("doodle/" + getSkinName ());
		GameObject skinGameObject = Instantiate <GameObject> (skinPrefab);
		this.skin = skinGameObject.transform;
		this.skin.SetParent (doodleObject.transform);
	}

	//获取皮肤名称
	public static string getSkinName ()
	{
		switch (GameManager.INSTANCE.gaming.doodleType) {
		case 1:
			return "bunny";
		case 2:
			return "christmas";
		case 3:
			return "doodlestein";
		case 4:
			return "ghost";
		case 5:
			return "ice";
		case 6:
			return "jungle";
		case 7:
			return "ninja";
		case 8:
			return "normal";
		case 9:
			return "soccer";
		case 10:
			return "space";
		case 11:
			return "underwater";
		default:
			return "normal";
		}
	}

	//重生
	public void rebirth()
	{
		//重生Doodle
		this.rebrithTimer = 1F;
		//将Doodle移动到摄像机的位置
		this.transform.Translate (
			new Vector3 (0F, (float)GameManager.INSTANCE.gaming.score, 0F)
			-
			this.transform.position
		);
	}
}
