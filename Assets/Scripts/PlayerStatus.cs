using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus{
	public int id;
	public int life;
	public int coin;
	public float x;
	public float y;

	//构造方法
	public PlayerStatus()
	{
		if (GameManager.INSTANCE != null) {
			this.id = GameManager.INSTANCE.playerInfo.id;
		}
	}

	public string text()
	{
		//更新信息
		this.life = GameManager.INSTANCE.life;
		this.coin = GameManager.INSTANCE.playerInfo.coin;
		this.x = GameManager.INSTANCE.doodle.transform.position.x;
		this.y = GameManager.INSTANCE.doodle.transform.position.y;

		return JsonUtility.ToJson (this);
	}

	public static PlayerStatus[] createArray(string jsonString)
	{
		return JsonHelper.getJsonArray<PlayerStatus> (jsonString);
	}
}
