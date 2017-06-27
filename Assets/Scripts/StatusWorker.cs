using System;
using System.Threading;
using UnityEngine;

/**
 * 使用线程池解决直接新建线程导致崩溃的问题
 */
public class StatusWorker
{
	public static void work ()
	{
		Thread thread = new Thread (() => {
			Thread.CurrentThread.IsBackground = true;
			task ();
		});
		thread.Start ();
		//thread.Join (2000);
	}

	private static void task ()
	{
		while (true) {
			HTTPUtil.push (GameManager.INSTANCE.gaming.playerStatus);
			GameManager.INSTANCE.gaming.playerStatuses = HTTPUtil.pull ();
			Thread.Sleep (16);
		}
	}
}