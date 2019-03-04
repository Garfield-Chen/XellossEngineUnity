using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoHttpProxy  
{
	static long tyoHttpProxyID = 0;
	public tyoHttpProxy()
	{

	}

	public static long HttpRequset(string _url,Dictionary<string, string> _args,tyoSocketClient.SocketClientMessage _response)
	{
		if ( !_args.ContainsKey("command") )
		{
			return 0;
		}

		tyoHttpProxyID++;

		string _args_string = "";

		foreach(KeyValuePair<string, string> _arg in _args)
		{
			_args_string += _arg.Key + "=" + _arg.Value + "&&";
		}

		Newtonsoft.Json.Linq.JObject _object = new Newtonsoft.Json.Linq.JObject(
			new Newtonsoft.Json.Linq.JProperty("id", tyoHttpProxyID.ToString()),
			new Newtonsoft.Json.Linq.JProperty("url", _url),
			new Newtonsoft.Json.Linq.JProperty("arg", _args_string),
			new Newtonsoft.Json.Linq.JProperty("action", "post"));

		//tyoCore.Log(_url+_args_string);

		byte[] _sendBuf = System.Text.ASCIIEncoding.Unicode.GetBytes(_object.ToString());

		tyoCore.socket.AddHttpProxyRequest(tyoHttpProxyID,_sendBuf,_response);

		return tyoHttpProxyID;
	}
}
