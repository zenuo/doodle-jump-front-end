using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;

public class HTTPUtil
{
	//POST表单并返回相应信息
	public static string POSTForm (string URL, NameValueCollection formData)
	{
		Debug.Log ("POSTForm " + URL);
		WebClient client = new WebClient ();
		byte[] response = client.UploadValues (URL, formData);
		return Encoding.UTF8.GetString (response);
	}

	//POST对象json并返回响应信息
	public static string POSTJson (string URL, string stringData)
	{
		Debug.Log ("POSTJson " + URL);
		WebClient client = new WebClient ();
		client.Headers.Set ("Content-Type", "application/json");
		byte[] response = client.UploadData (URL, Encoding.UTF8.GetBytes (stringData));
		return Encoding.UTF8.GetString (response);
	}

	public static string GET (String URL)
	{
		Debug.Log ("GET: " + URL);
		return new WebClient ().DownloadString (URL);
	}

	//注册
	public static string signUp (string name, string password, string bio, string email)
	{
		NameValueCollection formData = new NameValueCollection ();
		formData.Add ("name", name);
		formData.Add ("password", password);
		formData.Add ("bio", bio);
		formData.Add ("email", email);
		return POSTForm (Constant.HOST + "auth/signup", formData);
	}

	//登录
	public static string signIn (string name, string password)
	{
		NameValueCollection formData = new NameValueCollection ();
		formData.Add ("name", name);
		formData.Add ("password", password);
		return POSTForm (Constant.HOST + "auth/signin", formData);
	}

	//登出
	public static string signOut ()
	{
		return GET (string.Format ("{0}auth/signout?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId));
	}

	//获得玩家信息
	public static PlayerInfo getPlayerInfo ()
	{
		string jsonString = GET (string.Format ("{0}auth/info?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId));
		return PlayerInfo.createFromJosn (jsonString);
	}

	//发送信息
	public static int sendMessage (Message message)
	{
		return int.Parse (POSTJson (
			string.Format ("{0}message/send-{1}", Constant.HOST, GameManager.INSTANCE.sessionId),
			message.text ()
		));
	}

	//获取信息
	public static Message[] getMessage ()
	{
		string jsonString = GET (
			                    string.Format ("{0}message/get?session={1}",
				                    Constant.HOST,
				                    GameManager.INSTANCE.sessionId)
		                    );
		return JsonHelper.getJsonArray<Message> (jsonString);
	}

	//获取队伍列表
	public static TeamDTO[] listTeam ()
	{
		string jsonString = GET (
			                    string.Format ("{0}team/list?session={1}",
				                    Constant.HOST,
				                    GameManager.INSTANCE.sessionId)
		                    );

		return JsonHelper.getJsonArray<TeamDTO> (jsonString);
	}

	//创建队伍
	public static Team createTeam ()
	{
		string jsonString = GET (
			                    string.Format ("{0}team/create?session={1}&avator={2}&coin={3}",
				                    Constant.HOST,
				                    GameManager.INSTANCE.sessionId,
				GameManager.INSTANCE.gaming.doodleType,
				GameManager.INSTANCE.gaming.playerInfo.coin
			                    )
		                    );
		return Team.create (jsonString);
	}

	//获取状态
	public static void pull (PlayerStatus[] playerStatuses)
	{
		string jsonString = GET (
			                    string.Format ("{0}team/pull?session={1}", Constant.HOST, GameManager.INSTANCE.sessionId)
		                    );
		playerStatuses = JsonHelper.getJsonArray<PlayerStatus> (jsonString);
	}

	//上传状态
	public static void push (PlayerStatus playerStatus)
	{
		POSTJson (
			string.Format ("{0}team/push-{1}", Constant.HOST, GameManager.INSTANCE.sessionId),
			JsonUtility.ToJson (playerStatus)
		);
	}

	//获取地面信息队列
	public static void getPlatformInfo (Queue<string> platformInfoQueue, bool isNeedToLoadPlatformInfoFromServer)
	{
		if (isNeedToLoadPlatformInfoFromServer) {
			//从服务器加载
			string responseString = GET (string.Format ("{0}team/platform?session={1}&page={2}", Constant.HOST, GameManager.INSTANCE.sessionId, GameManager.INSTANCE.gaming.platfromInfoPage));
			//增加页数，供下次调用
			GameManager.INSTANCE.gaming.platfromInfoPage++;
			//拆分responseString为数组
			string[] tokens = responseString.Split (new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
			//遍历数组，入队
			foreach (string s in tokens) {
				platformInfoQueue.Enqueue (s);
			}
		}
	}

	public static Team getTeam ()
	{
		String jsonString = GET (string.Format (
			                    "{0}team/get?session={1}&teamId={2}",
			                    Constant.HOST,
			                    GameManager.INSTANCE.sessionId,
			GameManager.INSTANCE.gaming.team.id
		                    ));
		//Debug.Log (jsonString);
		return JsonUtility.FromJson<Team> (jsonString);
	}

	public static int joinTeam ()
	{
		return int.Parse (
			GET (
				string.Format (
					"{0}team/join?session={1}&teamId={2}",
					Constant.HOST,
					GameManager.INSTANCE.sessionId,
					GameManager.INSTANCE.gaming.team.id
				)
			)
		);
	}

	public static int lockTeam ()
	{
		return int.Parse (
			GET (
				string.Format (
					"{0}team/lock?session={1}&teamId={2}",
					Constant.HOST,
					GameManager.INSTANCE.sessionId,
					GameManager.INSTANCE.gaming.team.id
				)
			)
		);
	}
}