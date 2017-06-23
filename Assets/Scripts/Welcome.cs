using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Welcome : MonoBehaviour
{

	public Button signUp;
	public Button signIn;
	public Button offline;

	void Start ()
	{
		signUp.onClick.AddListener (SignUpTask);

		signIn.onClick.AddListener (SignInTask);

		offline.onClick.AddListener (offlineTask);
	}

	void SignUpTask ()
	{
		Debug.Log ("Welcome: SignUp");
		UIManager.INSTANCE.welcome.gameObject.SetActive (false);
		UIManager.INSTANCE.loadSignUp ();
	}

	void SignInTask ()
	{
		Debug.Log ("Welcome: SignIn");
		UIManager.INSTANCE.welcome.gameObject.SetActive (false);
		UIManager.INSTANCE.loadSignIn ();
	}

	void offlineTask()
	{
		Debug.Log ("Welcome: Offline");
		UIManager.INSTANCE.welcome.gameObject.SetActive (false);
		GameManager.INSTANCE.gameStatus = Constant.GAME_OFFLINE;
		UIManager.INSTANCE.loadChooseSkin ();
	}
}
