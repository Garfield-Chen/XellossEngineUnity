using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoResources
{
    public class ResourceNode
    {
        public enum ResNodeType
        {
            unknow = -1,
            none = 0,
            sprite = 1,
            font = 2,
            material = 3,
            perfeb = 4,
            pack = 5,
        }

        public object resourceObject = null;
        public ResNodeType resourceType = ResourceNode.ResNodeType.none;

        public ResourceNode(object _object, ResourceNode.ResNodeType _type)
        {
            resourceObject = _object;
            resourceType = _type;
        }
    }

    public class ResLoadInfo
	{
		public string type = "";
		public string name = "";
	}

    Dictionary<string,ResourceNode> ResourcesList = new Dictionary<string,ResourceNode>();
    List<string> LoadedResourceList = new List<string>();
    tyoResourcesAsyn resourcesAsynObject = null;
    Dictionary<string,bool> ResLoadFlag = new Dictionary<string, bool>();
    Dictionary<string,List<ResLoadInfo>> ResList = new Dictionary<string, List<ResLoadInfo>>();
    Dictionary<string,List<string>> ResLoadFlagDependen = new Dictionary<string,List<string>>();

    public tyoResources()
    {
        
    }

    public string LoadBytesResFile(string _bytesFile)
    {
        tyoFile _resFile = new tyoFile(true);
		if ( _resFile.LoadFile(_bytesFile) )
		{
            string realPath = Application.persistentDataPath + "/cache/" + _resFile.GetBytes();
            System.IO.File.WriteAllBytes(realPath, _resFile.GetBytes());
            return realPath;
        }

        return "";
    }

    public string LoadMapJson(string _mapFile)
    {
        tyoFile _resFile = new tyoFile(true);
		if ( _resFile.LoadFile("Data\\" + _mapFile) )
		{
            return _resFile.GetText();
        }

        return "";
    }

    public void LoadFromResfile(string _tyoResFile)
    {
        tyoFile _resFile = new tyoFile(true);
		if ( _resFile.LoadFile("Data\\" + _tyoResFile) )
		{
            ResList.Add(_tyoResFile,new List<ResLoadInfo>());

			string _txt = _resFile.GetText();

			int _idx = _txt.IndexOf("\r\n");

            if ( _idx >= 0 )
            {
                _txt = _txt.Replace("\r\n", "");
            }
            else
            {
                _idx = _txt.IndexOf("\n");

                if ( _idx >=0 )
                {
                    _txt = _txt.Replace("\n", "");
                }
            }

			string[] _resList = _txt.Split(';');

			foreach(string _resNode in _resList)
			{
				if ( _resNode.Length > 0)
				{
					ResLoadInfo _info = new ResLoadInfo();

					string[] _data = _resNode.Split(',');

					_info.type = _data[1];
					_info.name = _data[2];

					//tyoCore.Log(string.Format("{0},{1}",_info.type,_info.name));

					ResList[_tyoResFile].Add(_info);
				}
			}

            ResLoadFlag[_tyoResFile] = false;
            ResLoadFlagDependen[_tyoResFile] = new List<string>();
		}
    }

    public void BeginLoad(string _tyoResFile)
    {
        if(ResLoadFlag[_tyoResFile])
        {
            return;
        }
        
        if ( resourcesAsynObject == null )
        {
            tyoCore.Log( "resourcesAsynObject == null" );
        }

        resourcesAsynObject.LoadResouceNoPack(_tyoResFile,ResList[_tyoResFile]);
       
    }

    public void AddResourceDependenPack(string _mainRes,string _dependenPack)
    {
        if ( ResLoadFlagDependen.ContainsKey(_mainRes) )
        {
            ResLoadFlagDependen[_mainRes].Add(_dependenPack);

            ResLoadFlag[_dependenPack] = false;

            tyoCore.Log(_mainRes + " ->" + _dependenPack);
        }

        
    }

    public void LoadedDone(string _tyoResFile)
    {
        if ( ResLoadFlag.ContainsKey(_tyoResFile) )
        {
            ResLoadFlag[_tyoResFile] = true;
        }
    }

    public bool IsLoadDone(string _tyoResFile)
    {
        if ( ResLoadFlagDependen.ContainsKey(_tyoResFile) )
        {
            foreach(string _dependen in ResLoadFlagDependen[_tyoResFile])
            {
                if ( ResLoadFlag.ContainsKey(_dependen))
                {
                    if ( ResLoadFlag[_dependen] == false )
                    {
                        return false;
                    }
                }                
            }
        }

        if ( ResLoadFlag.ContainsKey(_tyoResFile) )
        {
            return ResLoadFlag[_tyoResFile];
        }

        return false;
    }

    public void InitResourceAsyn(tyoResourcesAsyn _object)
    {
        resourcesAsynObject = _object;
    }

    public bool IsResourceInitDone()
    {
        if( resourcesAsynObject != null )
        {
            return true;
        }

        return false;
    }

    public void DelayInitResources()
    {
        LoadMaterial("Materials\\");
    }

    int addCount = 0;

    public void AddSprite(Sprite _object)
    {
        if ( _object == null)
        {
            tyoCore.Log(string.Format("Warning , sprite is null!"));
            return;
        }

        string _name = _object.name;
        string _find = LoadedResourceList.Find( t => t == _name);

        if ( _find != null )
        {
            tyoCore.Log(string.Format("Warning , the file {0} is loaded!",_name));
        }
        else 
        {
            LoadedResourceList.Add(_name);
        }

        //tyoCore.Log(string.Format("Sprite {0} is loaded!",_name));

        ResourceNode _resNode = new ResourceNode(_object, ResourceNode.ResNodeType.sprite );
        ResourcesList[_name] = _resNode;
    }

    public int LoadSprites(string _name)
    {
        string _find = LoadedResourceList.Find( t => t == _name);

        if ( _find != null )
        {
            tyoCore.Log(string.Format("Warning , the file {0} is loaded!",_name));
            return -1;
        }
        else 
        {
            LoadedResourceList.Add(_name);
        }

        Sprite[] _sprites = Resources.LoadAll<Sprite>(_name);

        foreach ( Sprite _node in _sprites )
        {
            ResourceNode _resNode = new ResourceNode( _node, ResourceNode.ResNodeType.sprite );
            ResourcesList[_node.name] = _resNode;
        }

        return _sprites.Length;
    } 

    public int LoadFonts(string _name)
    {
        string _find = LoadedResourceList.Find( t => t == _name);

        if ( _find != null )
        {
            tyoCore.Log(string.Format("Warning , the file {0} is loaded!",_name));
            return -1;
        }
        else 
        {
            LoadedResourceList.Add(_name);
        }

        Font[] _fonts = Resources.LoadAll<Font>(_name);

        foreach ( Font _node in _fonts )
        {
            ResourceNode _resNode = new ResourceNode( _node, ResourceNode.ResNodeType.font );
            ResourcesList[_node.name] = _resNode;
        }

        return _fonts.Length;
    }

    public int LoadMaterial(string _name)
    {
        string _find = LoadedResourceList.Find( t => t == _name);

        if ( _find != null )
        {
            tyoCore.Log(string.Format("Warning , the file {0} is loaded!",_name));
            return -1;
        }
        else 
        {
            LoadedResourceList.Add(_name);
        }

        Material[] _materials = Resources.LoadAll<Material>(_name);

        foreach ( Material _node in _materials )
        {
            ResourceNode _resNode = new ResourceNode( _node, ResourceNode.ResNodeType.material );
            
            ResourcesList[_node.name] = _resNode;
        }

        return _materials.Length;
    }

    public int LoadPerfeb(string _name)
    {
        string _find = LoadedResourceList.Find( t => t == _name);

        if ( _find != null )
        {
            tyoCore.Log(string.Format("Warning , the file {0} is loaded!",_name));
            return -1;
        }
        else 
        {
            LoadedResourceList.Add(_name);
        }

        GameObject[] _perfebs = Resources.LoadAll<GameObject>(_name);

        foreach ( GameObject _node in _perfebs )
        {
            ResourceNode _resNode = new ResourceNode( _node, ResourceNode.ResNodeType.perfeb );
            
            ResourcesList[_node.name] = _resNode;
        }

        return _perfebs.Length;
    }

    public GameObject GetPerfebByName(string _name)
    {
        if ( ResourcesList.ContainsKey(_name) )
        {
            if ( ResourcesList[_name].resourceType == ResourceNode.ResNodeType.perfeb )
            {
                return (ResourcesList[_name].resourceObject) as GameObject;
            }
        }

        return null;
    }

    public Sprite GetSpriteByName(string _name)
    {
        if ( ResourcesList.ContainsKey(_name) )
        {
            if ( ResourcesList[_name].resourceType == ResourceNode.ResNodeType.sprite )
            {
                return (ResourcesList[_name].resourceObject) as Sprite;
            }
        }

        return null;
    }

    public Font GetFontByName(string _name)
    {
        if ( ResourcesList.ContainsKey(_name) )
        {
            if ( ResourcesList[_name].resourceType == ResourceNode.ResNodeType.font )
            {
                return (ResourcesList[_name].resourceObject) as Font;
            }
        }

        return null;
    }

    public Material GetMaterialByName(string _name)
    {
        if ( ResourcesList.ContainsKey(_name) )
        {
            if ( ResourcesList[_name].resourceType == ResourceNode.ResNodeType.material )
            {
                return (ResourcesList[_name].resourceObject) as Material;
            }
        }

        return null;
    }
}