using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour {

	public Button signIn;

	public Button cancle;

	public InputField nameInput;

	public InputField passwordInput;

	void Start () {
		Button signInBtn = signIn.GetComponent<Button> ();
		signInBtn.onClick.AddListener (SignInTask);

		Button cancleBtn = cancle.GetComponent<Button> ();
		cancleBtn.onClick.AddListener (CancleTask);
	}

	void SignInTask()
	{
		string name = nameInput.text;
		string password = passwordInput.text;
		Debug.Log (string.Format ("SignIn: SignInTask\n{0}\n{1}", name, password));
	}

	void CancleTask()
	{
		Debug.Log ("SignIn: Cancle");
		UIManager.INSTANCE.signIn.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
	}
}