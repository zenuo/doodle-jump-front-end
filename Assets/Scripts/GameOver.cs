using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

	public Button cancle;

	public Button again;

	int temp;

	public Text socre;

	void Start ()
	{
		cancle.onClick.AddListener (CancleTask);

		again.onClick.AddListener (AgainTask);
	}

	void Update ()
	{
		if (temp != GameManager.INSTANCE.gaming.score) {
			if (GameManager.INSTANCE.gaming.score > GameManager.INSTANCE.gaming.playerInfo.record) {
				temp = GameManager.INSTANCE.gaming.score;
				GameManager.INSTANCE.gaming.playerInfo.record = temp;
				HTTPUtil.updateRecord ();
				socre.text = string.Format ("New Record: {0}", GameManager.INSTANCE.gaming.score);
			} else {
				socre.text = string.Format ("Score: {0}", GameManager.INSTANCE.gaming.score);
			}
		}
	}

	void AgainTask ()
	{
		Debug.Log ("GameOver: Again");
		UIManager.INSTANCE.gameover.gameObject.SetActive (false);
		UIManager.INSTANCE.loadGaming ();
		GameManager.INSTANCE.gaming.initialize ();
	}

	void CancleTask ()
	{
		Debug.Log ("GameOver: Cancle");
		UIManager.INSTANCE.gameover.gameObject.SetActive (false);
		UIManager.INSTANCE.loadWelcome ();
	}
}
