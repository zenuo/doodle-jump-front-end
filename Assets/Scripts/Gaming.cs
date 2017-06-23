using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaming : MonoBehaviour
{

	public Button pause;

	public Button resume;

	float timer;

	void Start ()
	{
		Button pauseBtn = pause.GetComponent<Button> ();
		pauseBtn.onClick.AddListener (PauseTask);

		Button resumeBtn = resume.GetComponent<Button> ();
		resumeBtn.onClick.AddListener (ResumeTask);

		GameManager.INSTANCE.life = 5;
		GameManager.INSTANCE.platformPosition = new Vector3 ();
	}

	void Update ()
	{
		if (GameManager.INSTANCE.gameStatus == Constant.GAME_ONLINE)
		if ((timer -= Time.deltaTime) <= 0F) {
			HTTPUtil.pull ();
			HTTPUtil.push ();
		}
	}

	void PauseTask ()
	{
		Debug.Log ("Gaming: PauseTask");
		Time.timeScale = 0F;
		resume.gameObject.SetActive (true);
		pause.gameObject.SetActive (false);
	}

	void ResumeTask ()
	{
		Debug.Log ("Gaming: ResumeTask");
		Time.timeScale = 1F;
		pause.gameObject.SetActive (true);
		resume.gameObject.SetActive (false);
	}
}
