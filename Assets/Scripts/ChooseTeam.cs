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

	TeamDTO[] teams;

	void Start ()
	{
		choose.onClick.AddListener (ChooseTask);
		cancle.onClick.AddListener (CancleTask);
		dropdown.onValueChanged.AddListener (ChangeCaptionText);

		dropdown.captionText.text = "Team List";

		teams = HTTPUtil.listTeam ();
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
			teams = HTTPUtil.listTeam ();
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
		//GameManager.INSTANCE.gaming.team.id = dropdown.value;错误的取值方法
		UIManager.INSTANCE.chooseteam.gameObject.SetActive (false);
		HTTPUtil.joinTeam ();
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
		dropdown.captionText.text = dropdown.options [index].text;
		//将选择的队伍id传给GameManager.INSTANCE.gaming.team
		GameManager.INSTANCE.gaming.team.id =
			int.Parse (dropdown.options [index].text);
	}
}
