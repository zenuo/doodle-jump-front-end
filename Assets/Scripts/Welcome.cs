using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{

	public Button signUp;
	public Button signIn;
	public Button offline;

	void Start ()
	{
		Button signUpBtn = signUp.GetComponent<Button> ();
		signUpBtn.onClick.AddListener (SignUpTask);

		Button signInBtn = signIn.GetComponent<Button> ();
		signInBtn.onClick.AddListener (SignInTask);

		Button offlineBtn = offline.GetComponent<Button> ();
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

		//GameManager.INSTANCE.doodleType = 2;
		UIManager.INSTANCE.loadChooseSkin ();
	}
}
