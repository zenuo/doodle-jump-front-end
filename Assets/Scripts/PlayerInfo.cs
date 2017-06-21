using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo {
	//玩家ID
	public int id;
	//玩家昵称
	public string name;
	//玩家个性签名
	public string bio;
	//玩家电邮
	public string email;
	//玩家密码
	public string password;
	//玩家最高记录
	public int record;
	//玩家金币
	public int coin;

	public static PlayerInfo createFromJosn(string JsonString)
	{
		return JsonUtility.FromJson<PlayerInfo> (JsonString);
	}

	public string text()
	{
		return JsonUtility.ToJson (this);
	}
}
