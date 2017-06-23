using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

	public Button cancle;

	public Button again;

	public Text text;

	void Start ()
	{
		GameManager.INSTANCE.isGaming = false;

		cancle.onClick.AddListener (CancleTask);

		again.onClick.AddListener (AgainTask);

		//重置摄像机位置
		Camera.main.transform.Translate (
			Constant.INITICAL_POSITION_OF_CAMERA
			-
			Camera.main.transform.position
		);

		//清空地面队列
		GameManager.INSTANCE.platformQueue.Clear ();

		//销毁对象
		Destroy (UIManager.INSTANCE.gaming.gameObject);
		Destroy (UIManager.INSTANCE.panel1.gameObject);
		Destroy (UIManager.INSTANCE.avator1.gameObject);
		if (GameManager.INSTANCE.playerNum >= 2) {
			Destroy (UIManager.INSTANCE.panel2.gameObject);
			Destroy (UIManager.INSTANCE.avator2.gameObject);
		}
		if (GameManager.INSTANCE.playerNum == 3) {
			Destroy (UIManager.INSTANCE.panel3.gameObject);
			Destroy (UIManager.INSTANCE.avator3.gameObject);
		}
	}

	void AgainTask ()
	{
		Debug.Log ("GameOver: Again");
		GameManager.INSTANCE.isGaming = true;
		GameManager.INSTANCE.doodle = Doodle.create (GameManager.INSTANCE.doodleType);
		UIManager.INSTANCE.loadGaming ();
	}

	void CancleTask ()
	{
		Debug.Log ("GameOver: Cancle");
		UIManager.INSTANCE.gameover.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
		UIManager.INSTANCE.loadGaming ();
	}
}
