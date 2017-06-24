using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTeam : MonoBehaviour
{

	public Button choose;

	public Button cancle;

	public Dropdown dropdown;

	public float timer = 1F;

	void Start ()
	{
		choose.onClick.AddListener (ChooseTask);
		cancle.onClick.AddListener (CancleTask);
		dropdown.onValueChanged.AddListener (ChangeCaptionText);

		dropdown.captionText.text = "Team List";

		TeamDTO[] teams = HTTPUtil.listTeam ();
		foreach (TeamDTO team in teams) {
			dropdown.options.Add (
				new Dropdown.OptionData(team.id.ToString ())
			);
		}
	}

	void Update ()
	{
		//刷新列表
		if ((timer -= Time.deltaTime) <= 0F) {
			timer = 1F;
			dropdown.options.Clear ();
			TeamDTO[] teams = HTTPUtil.listTeam ();
			foreach (TeamDTO team in teams) {
				dropdown.options.Add (
					new Dropdown.OptionData(team.id.ToString ())
				);
			}
		}
	}

	void ChooseTask ()
	{
		Debug.Log ("ChooseTask");
		GameManager.INSTANCE.gaming.team.id = dropdown.value;
		UIManager.INSTANCE.chooseteam.gameObject.SetActive (false);
		UIManager.INSTANCE.loadTeamStatus ();
	}

	void CancleTask ()
	{
		Debug.Log ("CancleTask");
		UIManager.INSTANCE.chooseteam.gameObject.SetActive (false);
		UIManager.INSTANCE.loadChooseMode ();
	}

	void ChangeCaptionText(int index)
	{
		dropdown.captionText.text = dropdown.value.ToString ();
		//dropdown.captionText.text = dropdown.options [index].text;
	}
}
