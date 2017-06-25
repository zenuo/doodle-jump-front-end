using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{

	public Button signUp;
	public Button signIn;
	public Button signOut;
	public Button offline;
	public Button quit;

	void Start ()
	{
		signUp.onClick.AddListener (SignUpTask);
		signIn.onClick.AddListener (SignInTask);
		signOut.onClick.AddListener (SignOutTask);
		offline.onClick.AddListener (OfflineTask);
		quit.onClick.AddListener (QuitTask);
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
		GameManager.INSTANCE.gaming.gameStatus = Constant.GAME_ONLINE;
		UIManager.INSTANCE.loadSignIn ();
	}

	void SignOutTask()
	{
		Debug.Log ("Welcome: SignOut");
		HTTPUtil.signOut ();
	}

	void OfflineTask()
	{
		Debug.Log ("Welcome: Offline");
		UIManager.INSTANCE.welcome.gameObject.SetActive (false);
		GameManager.INSTANCE.gaming.gameStatus = Constant.GAME_OFFLINE;
		UIManager.INSTANCE.loadChooseSkin ();
	}

	void QuitTask()
	{
		Debug.Log ("Welcome: Quit");
		HTTPUtil.signOut ();
		Application.Quit ();
	}
}
