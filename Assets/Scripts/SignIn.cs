﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour {

	public Button signIn;

	public Button cancle;

	public InputField nameInput;

	public InputField passwordInput;

	public Text result;

	void Start () {
		signIn.onClick.AddListener (SignInTask);

		cancle.onClick.AddListener (CancleTask);
	}

	void SignInTask()
	{
		string name = nameInput.text;
		string password = passwordInput.text;
		//Debug.Log (string.Format ("SignIn: SignInTask\n{0}\n{1}", name, password));
		string sessionId = HTTPUtil.signIn (name, password);
		if (sessionId.Length == 32) {
			result.text = string.Format ("\"{0}\" succeed", name);
			GameManager.INSTANCE.sessionId = sessionId;
			UIManager.INSTANCE.signIn.gameObject.SetActive (false);
			GameManager.INSTANCE.gaming.playerInfo = HTTPUtil.getPlayerInfo ();
			//加载皮肤选择
			UIManager.INSTANCE.loadChooseSkin ();
		} else {
			result.text = string.Format ("\"{0}\" failed", name);
		}
	}

	void CancleTask()
	{
		Debug.Log ("SignIn: Cancle");
		UIManager.INSTANCE.signIn.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
	}
}