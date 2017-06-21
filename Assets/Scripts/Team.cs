using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team {
	public int id;
	public int[] players;
	public int createPlayerId;
	public bool isOpen;
	public long createTime;
	public long closeTime;

	public static Team create(string jsonString)
	{
		return JsonUtility.FromJson<Team> (jsonString);
	}

	public string text()
	{
		return JsonUtility.ToJson (this);
	}
}
