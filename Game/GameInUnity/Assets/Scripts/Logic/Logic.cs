using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Logic : tyoLogic 
{
	//public static ShareSceneData shareData = new ShareSceneData();
	const bool DEV_MODE = false;
	const bool BUILD_ASSET = false;
	const float NEWS_UPDATE_TIME = 1000 * 60 * 10;
	float news_dt = 0.0f;
	
	//2592 1440
	public override void Init() 
	{	
		tyoCore.resources.LoadPerfeb("Perfeb");
		tyoCore.resources.LoadSprites("Textures");

		tyoCore.scene.AddScene(new BattleScene("battle scene"));
		tyoCore.scene.ChangeScene("battle scene");
		
	}


	public override void Run() 
	{
		
	}

	public override void LogicUpdate(float _dt)
	{
		/*
		if(news_dt >= NEWS_UPDATE_TIME)
		{
			news_dt = 0.0f;
			shareData.netObject.AppNews_REQ();
		}
		else
		{
			news_dt += _dt;
		}
		 */
	}
}
