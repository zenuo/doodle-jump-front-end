using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;

public class HTTPUtil {
	//POST表单并返回相应信息
	public static string POSTForm(string URL, NameValueCollection formData)
	{
		WebClient client = new WebClient ();
		byte[] response = client.UploadValues (URL, formData);
		return Encoding.UTF8.GetString (response);
	}

	//POST字符串并返回响应信息
	public static string POSTString(string URL, string stringData)
	{
		WebClient client = new WebClient ();
		client.Headers.Set ("Content-Type", "application/json");
		byte[] response = client.UploadData (URL, Encoding.UTF8.GetBytes (stringData));
		return Encoding.UTF8.GetString (response);
	}

	public static string GET(String URL)
	{
		return new WebClient ().DownloadString (URL);
	}

	//注册
	public static string signUp(string name, string password, string bio, string email)
	{
		NameValueCollection formData = new NameValueCollection();
		formData.Add ("name", name);
		formData.Add ("password", password);
		formData.Add ("bio", bio);
		formData.Add ("email", email);
		return POSTForm (Constant.HOST + "auth/signup", formData);
	}

	//登录
	public static string signIn(string name, string password)
	{
		NameValueCollection formData = new NameValueCollection();
		formData.Add ("name", name);
		formData.Add ("password", password);
		return POSTForm (Constant.HOST + "auth/signin", formData);
	}

	//登出
	public static string signOut()
	{
		return GET (string.Format ("{0}auth/signout?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId));
	}

	//获得玩家信息
	public static PlayerInfo getPlayerInfo()
	{
		string jsonString = GET (string.Format ("{0}auth/info?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId));
		return PlayerInfo.createFromJosn (jsonString);
	}

	//发送信息
	public static int sendMessage(Message message)
	{
		return int.Parse (POSTString (
			string.Format ("{0}message/send-{1}", Constant.HOST, GameManager.INSTANCE.sessionId),
			message.text ()
		));
	}

	//获取信息
	public static Message[] getMessage()
	{
		string jsonString = GET (
			string.Format ("{0}message/get?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId)
		);
		return JsonHelper.getJsonArray<Message> (jsonString);
	}

	//获取队伍列表
	public static Team[] getTeamlist()
	{
		string jsonString = GET (
			string.Format ("{0}team/list", Constant.HOST)
		);
		return JsonHelper.getJsonArray<Team> (jsonString);
	}

	//创建队伍
	public static void createTeam()
	{
		GET (string.Format ("{0}team/create?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId));
	}

	//更新状态
	public static int push(){
		string jsonString = GameManager.INSTANCE.playerStatus.text ();
		return int.Parse (
			POSTString (
				string.Format ("{0}team/push", Constant.HOST),
				jsonString
			)
		);
	}

	//获取状态
	public static void pull()
	{
		string jsonString = GET (
			string.Format ("{0}team/pull?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId)
		);
		GameManager.INSTANCE.playerStatuses = JsonHelper.getJsonArray<PlayerStatus> (jsonString);
	}
}