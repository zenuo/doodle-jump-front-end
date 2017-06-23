using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team {
	public int id;
	public int[] players;
	public int[] avators;
	public int[] coins;
	public int createPlayerId;
	public bool open;
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
