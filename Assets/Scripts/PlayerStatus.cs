using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
	public int id;
	public int l;
	public int c;
	public int d = 1;
	public float x;
	public float y;

	//构造方法
	public PlayerStatus (int id)
	{
		this.id = id;
	}

	public static PlayerStatus[] createArray (string jsonString)
	{
		return JsonHelper.getJsonArray<PlayerStatus> (jsonString);
	}
}
