using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour {

	public Button signUp;

	public Button cancle;

	public InputField nameInput;

	public InputField emailInput;

	public InputField bioInput;

	public InputField passwordInput;

	void Start () {
		signUp.onClick.AddListener (SignUpTask);

		cancle.onClick.AddListener (CancleTask);
	}

	void SignUpTask()
	{
		Debug.Log ("SignUp: SignUp");
		string name = nameInput.text;
		string email = emailInput.text;
		string bio = bioInput.text;
		string password = passwordInput.text;
		Debug.Log (string.Format ("{0}\n{1}\n{2}\n{3}\n", name, email, bio, password));
	}

	void CancleTask()
	{
		Debug.Log ("SignUp: Cancle");
		UIManager.INSTANCE.signUp.gameObject.SetActive (false);
		UIManager.INSTANCE.welcome.gameObject.SetActive (true);
	}
}
