using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoResourcesAsyn : MonoBehaviour 
{
	//不同平台下StreamingAssets的路径是不同的，这里需要注意一下。  
	string AssetbundlePath = "";

	class ResAsynLoadNode
	{
		public string ResFile = "";
		public List<tyoResources.ResLoadInfo> ResList = null;
		public bool LoadFromPack = false;
		public bool IsRun = false;
	}

	List<ResAsynLoadNode> ResAsynNodeList = new List<ResAsynLoadNode>();

	void Awake()
	{	
		#if UNITY_EDITOR
		AssetbundlePath =  "file://" + Application.dataPath + "/StreamingAssets/"; 
		#elif UNITY_ANDROID  
		AssetbundlePath =  "jar:file://" + Application.dataPath + "!/assets/";  
		#elif UNITY_IPHONE  
		AssetbundlePath =  "file://" + Application.dataPath + "/Raw/";  
		#elif UNITY_STANDALONE_WIN
		AssetbundlePath =  "file://" + Application.dataPath + "/StreamingAssets/";  
		#else  
		AssetbundlePath = string.Empty;  
		#endif

		tyoCore.resources.InitResourceAsyn(this);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < ResAsynNodeList.Count; ++i)
		{
			if(!ResAsynNodeList[i].IsRun)
			{
				ResAsynNodeList[i].IsRun = true;

				if ( ResAsynNodeList[i].LoadFromPack )
				{
					StartCoroutine(LoadResoucesFromPack(ResAsynNodeList[i].ResFile));
				}
				else
				{
					StartCoroutine(LoadResourceByCoroutine(ResAsynNodeList[i].ResFile,ResAsynNodeList[i].ResList));
				}
			}
		}
	}

	public void LoadResouceNoPack(string _resFile,List<tyoResources.ResLoadInfo> _resList)
	{
		ResAsynLoadNode _node = new ResAsynLoadNode();

		_node.ResFile = _resFile;
		_node.ResList = _resList;
		_node.LoadFromPack = false;

		ResAsynNodeList.Add(_node);

		Caching.ClearCache();
	}

	private IEnumerator LoadResoucesFromPack(string _packPath)
	{  
		//tyoCore.Log(AssetbundlePath + _packPath);

		//AssetBundleCreateRequest _bundle = AssetBundle.LoadFromFileAsync(AssetbundlePath + _packPath);
        WWW _bundle = WWW.LoadFromCacheOrDownload(AssetbundlePath + _packPath,0);  
		
        yield return _bundle;  

		tyoCore.Log("Res:" + _packPath + " -- downloads over!");
		
		//Sprite[] _list = _bundle.assetBundle.LoadAllAssets<Sprite>();
        //加载
		AssetBundleRequest _req = _bundle.assetBundle.LoadAllAssetsAsync<Sprite>();
		yield return _req;

		tyoCore.Log("load over : " + _req.allAssets.Length.ToString());


		for ( int i = 0; i < _req.allAssets.Length; ++i )
		{
			tyoCore.resources.AddSprite(_req.allAssets[i] as Sprite);
		}
	 	 
        _bundle.assetBundle.Unload(false);

		tyoCore.resources.LoadedDone(_packPath);
    }

	IEnumerator LoadResourceByCoroutine(string _resFile,List<tyoResources.ResLoadInfo> _resList)  
    {  
        for(int i = 0; i < _resList.Count; ++i )
		{
			switch(_resList[i].type)
			{
				case "fonts":
				{
					yield return tyoCore.resources.LoadFonts(_resList[i].name);
					//tyoCore.Log(string.Format("fonts {0} res:{1}", _count ,resList[loadIndex].name));
					break;
				}
				case "perfeb":
				{
					yield return tyoCore.resources.LoadPerfeb(_resList[i].name);
					//tyoCore.Log(string.Format("perfeb {0} res:{1}", _count ,resList[loadIndex].name));
					break;
				}
				case "sprites":
				{
					yield return tyoCore.resources.LoadSprites(_resList[i].name);
					//tyoCore.Log(string.Format("sprites {0} res:{1}", _count ,resList[loadIndex].name));
					break;
				}
				case "pack":
				{
					tyoCore.resources.AddResourceDependenPack(_resFile,_resList[i].name);

					ResAsynLoadNode _node = new ResAsynLoadNode();

					_node.ResFile = _resList[i].name;
					_node.ResList = null;
					_node.LoadFromPack = true;

					ResAsynNodeList.Add(_node);

					yield return true; 
					break;
				}
			}
		}

		tyoCore.resources.LoadedDone(_resFile);
    }  

	public byte[] streamingAssetsByAndroidResult;
	public bool streamingAssetsByAndroidIsDone = false;

	IEnumerator LoadStreamingAssetsByAndroidEnumerator(string _fileName)
	{
		streamingAssetsByAndroidIsDone = false;
		_fileName = System.IO.Path.Combine(Application.streamingAssetsPath, _fileName);
		if (_fileName.Contains("://")) 
		{
        	WWW www = new WWW(_fileName);
        	yield return www;
			while (www.isDone == false)
			{
				yield return new WaitForEndOfFrame();
			}
        	yield return new WaitForSeconds(0.5f);
        	streamingAssetsByAndroidResult = www.bytes;
        	yield return new WaitForEndOfFrame();	
        	
		} 
		else
		{
			 System.IO.FileStream _fs = new System.IO.FileStream(_fileName, System.IO.FileMode.Open);
			 _fs.Read(streamingAssetsByAndroidResult,0,(int)_fs.Length);
		}

		streamingAssetsByAndroidIsDone = true;
	}

	public void LoadFileTextInStreamingAssetsByAndroid(string _fileName)
	{
		StartCoroutine(LoadStreamingAssetsByAndroidEnumerator(_fileName));
	}
}
