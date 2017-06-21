using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaming : MonoBehaviour {

	public Button pause;

	public Button resume;

	void Start () {
		Button pauseBtn = pause.GetComponent<Button> ();
		pauseBtn.onClick.AddListener (PauseTask);

		Button resumeBtn = resume.GetComponent<Button> ();
		resumeBtn.onClick.AddListener (ResumeTask);
	}

	void Update () {
		
	}

	void PauseTask()
	{
		Debug.Log ("Gaming: PauseTask");
		Time.timeScale = 0f;
		resume.gameObject.SetActive (true);
		pause.gameObject.SetActive (false);
	}

	void ResumeTask()
	{
		Debug.Log ("Gaming: ResumeTask");
		Time.timeScale = 1f;
		pause.gameObject.SetActive (true);
		resume.gameObject.SetActive (false);
	}
}
