using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *常量类
 */
public class Constant {
	//摄像机初始位置
	public static Vector3 INITICAL_POSITION_OF_CAMERA = new Vector3(0F, 4F, -10F);

	//游戏状态：离线游戏
	public const int GAME_OFFLINE = 1;

	//游戏状态：在线游戏
	public const int GAME_ONLINE = 2;

	//重力加速度
	public const float G = -11F;

	//Doodle水平移动速度
	public const float X_VELOCITY_OF_DOODLE = 3F;

	//Doodle缩放比例
	public const float SCALING_OF_DOODLE = 0.65F;

	//地面竖直距离
	public const float VERTICAL_DISTANCE_BETWEEN_PLATFORMS = 1.4F;

	//场景高度
	public const float SCENE_HEIGHT = 8F;

	//场景宽度
	public const float SCENE_WIDTH = 5.6F;

	//队列维护platform数量
	public const int NUMBER_OF_PLATFORMS_QUEUE = 6;

	//不稳定的地面竖直移动速度
	public const float Y_VELOCITY_OF_UNSTABLE_PLATFORM = -0.8F;

	//移动的地面的移动速度
	public const float VELOCITY_OF_MOVING_PLATFORM = 0.5F;

	//主机地址
	public const string HOST = "http://yziyz.xin:8080/";

	//地面信息队列中地面信息数量的最大值
	public const int MAX_COUNT_OF_PLATFROM_INFO_QUEUE = 400;

	//地面信息队列中地面信息数量的最小值
	public const int MIN_COUNT_OF_PLATFROM_INFO_QUEUE = 200;

	//道具速度
	public const float VELOCITY_OF_PROP = 30F;
}
