using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHouseNet 
{
	public const string SERVER_URL = "http://app.magichouse.cc/app_server/app_server.php";
	public const string SERVER_IP = "app.magichouse.cc";//"139.196.243.226";
	public const int SERVER_PORT = 10888;

	public MagicHouseNet()
	{
		//IPAddress[] address= .GetHostAddresses("test.thisisgame.com.cn");
	}

	public void OnNetMessage(string _message)
	{
		try
		{
			Dictionary<string, string> _msgDictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(_message);

// 			if ( _msgDictionary.ContainsKey("command") )
// 			{
// 				int _cmd = int.Parse(_msgDictionary["command"]);
// 
// 				if ( _cmd == 1002 )
// 				{
// 					Logic.shareData.AstroTipsToday_ACK(_msgDictionary);
// 				}
// 				else if ( _cmd == 1004 )
// 				{
// 					Logic.shareData.AstroData_ACK(_msgDictionary);
// 				}
// 				else if ( _cmd == 1006 )
// 				{
// 					Logic.shareData.AstroSelfResult_ACK(_msgDictionary);
// 				}
// 				else if ( _cmd == 1008 )
// 				{
// 					Logic.shareData.TarotGetCardResult_ACK(_msgDictionary);
// 				}
// 				else if ( _cmd == 1010 )
// 				{
// 					Logic.shareData.appMessageACKFlag = true;
// 				}
// 				else if ( _cmd == 1014 )
// 				{
// 					//tyoCore.Log(_message);
// 					Logic.shareData.AppNews_ACK(_msgDictionary);
// 				}
// 			}

		}
		catch
		{

		}
	}

// 	public void AstroTipsToday_REQ()
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1001");
// 		//_dic.Add("time", "2017-5-12");
// 		
// 		_dic.Add("time", string.Format("{0}-{1}-{2}",
// 			System.DateTime.Now.Year.ToString(),
// 			System.DateTime.Now.Month.ToString(),
// 			System.DateTime.Now.Day.ToString())); 
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void AstroData_REQ(string _astro)
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1003");
// 		//_dic.Add("time", "2017-5-12");
// 		_dic.Add("astro", _astro);
// 
// 		_dic.Add("time", string.Format("{0}-{1}-{2}",
// 			System.DateTime.Now.Year.ToString(),
// 			System.DateTime.Now.Month.ToString(),
// 			System.DateTime.Now.Day.ToString()));
// 		
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void AstroSelfResult_REQ(AstroSelfResultScene.AstroSelfResultSceneData _data)
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1005");
// 		_dic.Add("month", _data.month);
// 		_dic.Add("day", _data.day);
// 		_dic.Add("year", _data.year);
// 		_dic.Add("hour", _data.hour);
// 		_dic.Add("minute", _data.minute);
// 		_dic.Add("lat", _data.lat);
// 		_dic.Add("lng", _data.lng);
// 
// 		Logic.shareData.astroSelfResultData = null;
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void TarotResultGetCard_REQ(int _cardidx)
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1007");
// 		_dic.Add("cardidx", _cardidx.ToString());
// 
// 		Logic.shareData.tarotCardResultData = null;
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void AppMessage_REQ(string _msg)
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1009");
// 		_dic.Add("msg", _msg);
// 
// 		Logic.shareData.appMessageACKFlag = false;
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void AppDataTrace_REQ(string _action,string _data1 = "nil",string _data2 = "nil",string _data3 = "nil",string _data4 = "nil"
// 									,string _data5 = "nil",string _data6 = "nil",string _data7 = "nil",string _data8 = "nil")
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1011");
// 		_dic.Add("trace_action", _action);
// 		_dic.Add("data1", _data1);
// 		_dic.Add("data2", _data2);
// 		_dic.Add("data3", _data3);
// 		_dic.Add("data4", _data4);
// 		_dic.Add("data5", _data5);
// 		_dic.Add("data6", _data6);
// 		_dic.Add("data7", _data7);
// 		_dic.Add("data8", _data8);
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
// 
// 	public void AppNews_REQ()
// 	{
// 		Dictionary<string, string> _dic = new Dictionary<string, string>();
// 
// 		_dic.Add("command", "1013");
// 
// 		tyoHttpProxy.HttpRequset(SERVER_URL, _dic, OnNetMessage);
// 	}
}
