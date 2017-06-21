using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Button cancle;

	public Button again;

	public Text text;

	void Start () {
		Button cancleBtn = cancle.GetComponent<Button> ();
		cancleBtn.onClick.AddListener (CancleTask);

		Button againBtn = again.GetComponent<Button> ();
		againBtn.onClick.AddListener (AgainTask);

		UIManager.INSTANCE.DisActiveGaming ();
	}

	void AgainTask()
	{
		Debug.Log ("GameOver: Again");
		UIManager.INSTANCE.gameover.gameObject.SetActive (false);
		GameManager.INSTANCE.isGaming = true;
		GameManager.INSTANCE.doodle = Doodle.create (GameManager.INSTANCE.doodleType);
		UIManager.INSTANCE.loadGaming ();
		UIManager.INSTANCE.loadPlayerPanel ();
	}

	void CancleTask()
	{
		Debug.Log ("GameOver: Cancle");
		UIManager.INSTANCE.gameover.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
	}
}
