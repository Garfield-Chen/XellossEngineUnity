using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class tyoHttp : MonoBehaviour  
{
	class HttpWorker
	{
		public HttpWorker(long _id)
		{
			id = _id;
		}

		public long id = 0;
		public OnMessageResponse callback = null;
		public string content = "";
		public Dictionary<string, string> data = null;
		public bool isGet = false;
		public string url = "";
	}

	float curProgress = 0;
	string tmpStr = "null"; 
	Texture2D texture;  
	long workID = 0;
	bool workingNow = false;
	public delegate void OnMessageResponse(string _message);

	Dictionary<long,HttpWorker> httpWorkList = new Dictionary<long,HttpWorker>();
	void Start()
	{
		
	}

	void Update()
	{
		if ( !workingNow )
		{
			foreach(KeyValuePair<long,HttpWorker> _worker in httpWorkList)
			{
				if ( _worker.Value.isGet )
				{
					StartCoroutine(GET(_worker.Key, _worker.Value.url, _worker.Value.data)); 
				}
				else
				{
					StartCoroutine(POST(_worker.Key, _worker.Value.url, _worker.Value.data)); 
				}

				workingNow = true;
				break;
			}
		}
		
	}

	void OnWorkingDone(long _id)
	{
		workingNow = false;

		if ( httpWorkList.ContainsKey(_id) )
		{
			httpWorkList[_id].callback(httpWorkList[_id].content);
			httpWorkList.Remove(_id);
			
		}
	}

    public float GetProgress()  
    {  
        return curProgress;  
    }

	public long GetData(string _url,Dictionary<string, string> _get,OnMessageResponse _response)
	{
		if ( !_get.ContainsKey("command") )
		{
			return 0;
		}

		HttpWorker _worker = new HttpWorker(workID++);

		_worker.id = workID;
		_worker.url = _url;
		_worker.data = _get;
		_worker.callback = _response;
		_worker.isGet = true;


		httpWorkList.Add(workID,_worker);

		return workID;
	}

	public long PostData(string _url,Dictionary<string, string> _post,OnMessageResponse _response)
	{
		if ( !_post.ContainsKey("command") )
		{
			return 0;
		}
		
		HttpWorker _worker = new HttpWorker(workID++);

		_worker.id = workID;
		_worker.url = _url;
		_worker.data = _post;
		_worker.callback = _response;
		_worker.isGet = false;

		httpWorkList.Add(workID,_worker);
		
		return workID;
	}

    IEnumerator POST(long _id,string _url, Dictionary<string, string> _post)  
    {  
        WWWForm form = new WWWForm();  
 
        foreach (KeyValuePair<string, string> _post_arg in _post)  
        {  
            form.AddField(_post_arg.Key, _post_arg.Value);  
        }  
 
        WWW _www = new WWW(_url, form);  
  
        yield return _www;  
        curProgress = _www.progress;  
  
		if ( httpWorkList.ContainsKey(_id) )
		{
			if (_www.error != null)  
			{  
				httpWorkList[_id].content =  "error :" + _www.error;  
			}  
			else  
			{   
				httpWorkList[_id].content = _www.text;  
			}  
		}
		
		OnWorkingDone(_id);
    }  
  
    IEnumerator GET(long _id, string _url, Dictionary<string, string> _get)  
    {  
        string _parameters;  
        bool _first;  
        if (_get.Count > 0)  
        {  
            _first = true;  
            _parameters = "?";  
 
            foreach (KeyValuePair<string, string> _post_arg in _get)  
            {  
                if (_first)  
                    _first = false;  
                else  
                    _parameters += "&";  
  
                _parameters += _post_arg.Key + "=" + _post_arg.Value;  
            }  
        }  
        else  
        {  
            _parameters = "";  
        }  
  
        tmpStr = "getURL :" + _parameters;  
  
        WWW _www = new WWW(_url + _parameters);  
        yield return _www;  
        curProgress = _www.progress;  
  
        if ( httpWorkList.ContainsKey(_id) )
		{
			if (_www.error != null)  
			{  
				httpWorkList[_id].content =  "error :" + _www.error;  
			}  
			else  
			{   
				httpWorkList[_id].content = _www.text;  
			}  
		}
		
		OnWorkingDone(_id);
    }  
  
    IEnumerator GETTexture(string _textureURL)  
    {  
        WWW _wwwTexture = new WWW(_textureURL);  
  
        yield return _wwwTexture;  
  
        if (_wwwTexture.error != null)  
        {  
            Debug.Log("error :" + _wwwTexture.error);  
        }  
        else  
        {  
            texture = _wwwTexture.texture;  
        }  
    }  
}
