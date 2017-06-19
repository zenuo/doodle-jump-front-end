using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *常量类
 */
public class Constant : MonoBehaviour {
	//重力加速度
	public const float G = -11f;

	//Doodle水平移动速度
	public const float X_VELOCITY_OF_DOODLE = 3f;

	//Doodle缩放比例
	public const float SCALING_OF_DOODLE = 0.65f;

	//Platform竖直距离
	public const float VERTICAL_DISTANCE_BETWEEN_PLATFORMS = 1.4f;

	//场景高度
	public const float SCENE_HEIGHT = 8f;

	//场景宽度
	public const float SCENE_WIDTH = 5.6f;

	//队列维护platform数量
	public const int NUMBER_OF_PLATFORMS_QUEUE = 6;

	//不稳定的地面竖直移动速度
	public const float Y_VELOCITY_OF_UNSTABLE_PLATFORM = -0.8f;

	//移动的地面的移动速度
	public const float VELOCITY_OF_MOVING_PLATFORM = 0.5f;
}
