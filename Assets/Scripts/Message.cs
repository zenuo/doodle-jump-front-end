using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message {

	public int id;
	public byte scope;
	public int source;
	public int target;
	public long createTime = 0;
	public long sendTime = 0;
	public string content;

	public static Message create(string jsonString)
	{
		return JsonUtility.FromJson<Message> (jsonString);
	}

	public string text()
	{
		return JsonUtility.ToJson (this);
	}
}