using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class tyoMap 
{
	public class MapDataJsonFile
    {
        public enum MapTileAttributeType
        {
            string_attr = 0x1,
            float_attr ,
            int_attr ,
            bool_attr ,
            double_attr ,
        }

        private string _mapName = "";
        public string MapName
        {
            get { return _mapName; }
            set { _mapName = value; }
        }

        private int _mapSizeWidth = 0;
        public int MapSizeWidth
        {
            get { return _mapSizeWidth; }
            set { _mapSizeWidth = value; }
        }

        private int _mapSizeHeight = 0;
        public int MapSizeHeight
        {
            get { return _mapSizeHeight; }
            set { _mapSizeHeight = value; }
        }

        private int _mapTileWidth = 0;
        public int MapTileWidth
        {
            get { return _mapTileWidth; }
            set { _mapTileWidth = value; }
        }

        private int _mapTileHeight = 0;
        public int MapTileHeight
        {
            get { return _mapTileHeight; }
            set { _mapTileHeight = value; }
        }

        public class __MapTileInfosJson
        {
            private string _name = "";
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            
            private string _directory = "";
            public string Directory
            {
                get { return _directory; }
                set { _directory = value; }
            }

            private int _index = -1;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }
        }
        private List<__MapTileInfosJson> _mapTileInfosList = new List<__MapTileInfosJson>();
        public List<__MapTileInfosJson> MapTileInfosList
        {
            get { return _mapTileInfosList; }
        }

        public class __MapLayerInfosJson
        {
            private string _name = "";
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private int _index = -1;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            private int _depth = 0;
            public int Depth
            {
                get { return _depth; }
                set { _depth = value; }
            }
        }
        private List<__MapLayerInfosJson> _mapLayerInfosList = new List<__MapLayerInfosJson>();
        public List<__MapLayerInfosJson> MapLayerInfosList
        {
            get { return _mapLayerInfosList; }
        }

        private List<bool> _mapLayerShowFlagList = new List<bool>();
        public List<bool> MapLayerShowFlagList
        {
            get { return _mapLayerShowFlagList; }
        }

        public class __MapUsedTileInfosJson
        {
            private string _name = "";
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private int _tileId = -1;
            public int TileID
            {
                get { return _tileId; }
                set { _tileId = value; }
            }

            private int _comboxIndex = 0;
            public int ComboxIndex
            {
                get { return _comboxIndex; }
                set { _comboxIndex = value; }
            }

            private bool _usedFlag = false;
            public bool UsedFlag
            {
                get { return _usedFlag; }
                set { _usedFlag = value; }
            }

            private int _tileX = 0;
            public int TileX
            {
                get { return _tileX; }
                set { _tileX = value; }
            }

            private int _tileY = 0;
            public int TileY
            {
                get { return _tileY; }
                set { _tileY = value; }
            }

            private int _tileW = 0;
            public int TileW
            {
                get { return _tileW; }
                set { _tileW = value; }
            }

            private int _tileH = 0;
            public int TileH
            {
                get { return _tileH; }
                set { _tileH = value; }
            }
        }
        private List<__MapUsedTileInfosJson> _mapUsedTileInfosList = new List<__MapUsedTileInfosJson>();
        public List<__MapUsedTileInfosJson> MapUsedTileInfosList
        {
            get { return _mapUsedTileInfosList; }
        }

        public int[,,] MapTile = null;
        public int[,] MapTileExternFlag = null;
        public bool[,] MapBlockFlag = null;
        public int[,,] MapAnimationTile = null;

        public void InitMapTile()
        {
            MapTile = new int[MapLayerInfosList.Count, MapSizeWidth, MapSizeHeight];
            MapBlockFlag = new bool[MapSizeWidth, MapSizeHeight];
            MapTileExternFlag = new int[MapSizeWidth, MapSizeHeight];
            MapAnimationTile = new int[MapLayerInfosList.Count, MapSizeWidth, MapSizeHeight];

            for (int m = 0; m < MapLayerInfosList.Count; ++m)
            {
                for (int n = 0; n < MapSizeWidth; ++n)
                {
                    for (int p = 0; p < MapSizeHeight; ++p)
                    {
                        MapAnimationTile[m, n, p] = -1;
                    }
                }
            }
        }

        public class __AnimationUsedInfosJson
        {
            private int _index = 0;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            private string _filePath = "";
            public string FilePath
            {
                get { return _filePath; }
                set { _filePath = value; }
            }

            private string _nodeName = "";
            public string NodeName
            {
                get { return _nodeName; }
                set { _nodeName = value; }
            }

            private string _direction = "";
            public string Direction
            {
                get { return _direction; }
                set { _direction = value; }
            }
        }

        private List<__AnimationUsedInfosJson> _animationUsedInfosList = new List<__AnimationUsedInfosJson>();
        public List<__AnimationUsedInfosJson> AnimationUsedInfosList
        {
            get { return _animationUsedInfosList; }
        }

        public class __AnimationOffsets
        {
            private int _index = 0;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            private int _x = -1;
            public int X
            {
                get { return _x; }
                set { _x = value; }
            }

            private int _y = -1;
            public int Y
            {
                get { return _y; }
                set { _y = value; }
            }

            private int _offsetX = -1;
            public int OffsetX
            {
                get { return _offsetX; }
                set { _offsetX = value; }
            }

            private int _offsetY = -1;
            public int OffsetY
            {
                get { return _offsetY; }
                set { _offsetY = value; }
            }

            private int _layer = -1;
            public int Layer
            {
                get { return _layer; }
                set { _layer = value; }
            }
        }

        private List<__AnimationOffsets> _animationOffsetList = new List<__AnimationOffsets>();
        public List<__AnimationOffsets> AnimationOffsetList
        {
            get { return _animationOffsetList; }
        }

        public class __MapTileAttribute
        {
            public string Value1
            {
                set{ _value1 = value; }
                get{ return _value1; }
            }

            public MapTileAttributeType Value1_Type
            {
                set { _value1_type = value; }
                get { return _value1_type; }
            }

            public string Value1_Description
            {
                set { _value1_description = value; }
                get { return _value1_description; }
            }

            string _value1 = "0";
            MapTileAttributeType _value1_type = MapTileAttributeType.string_attr;
            string _value1_description = "default";

            public string Value2
            {
                set { _value2 = value; }
                get { return _value2; }
            }

            public MapTileAttributeType Value2_Type
            {
                set { _value2_type = value; }
                get { return _value2_type; }
            }

            public string Value2_Description
            {
                set { _value2_description = value; }
                get { return _value2_description; }
            }

            string _value2 = "0";
            MapTileAttributeType _value2_type = MapTileAttributeType.string_attr;
            string _value2_description = "default";

            public string Value3
            {
                set { _value3 = value; }
                get { return _value3; }
            }

            public MapTileAttributeType Value3_Type
            {
                set { _value3_type = value; }
                get { return _value3_type; }
            }

            public string Value3_Description
            {
                set { _value3_description = value; }
                get { return _value3_description; }
            }

            string _value3 = "0";
            MapTileAttributeType _value3_type = MapTileAttributeType.string_attr;
            string _value3_description = "default";

            public string Value4
            {
                set { _value4 = value; }
                get { return _value4; }
            }

            public MapTileAttributeType Value4_Type
            {
                set { _value4_type = value; }
                get { return _value4_type; }
            }

            public string Value4_Description
            {
                set { _value4_description = value; }
                get { return _value4_description; }
            }

            string _value4 = "0";
            MapTileAttributeType _value4_type = MapTileAttributeType.string_attr;
            string _value4_description = "default";

            public __MapTileAttribute(int _x, int _y, int _layer)
            {
                _tile_x = _x;
                _tile_y = _y;
                _tile_layer = _layer;
            }

            public int _tile_x = -1;
            public int _tile_y = -1;
            public int _tile_layer = -1;
        }

        private List<__MapTileAttribute> _mapTileAttributeList = new List<__MapTileAttribute>();
        public List<__MapTileAttribute> MapTileAttributeList
        {
            get { return _mapTileAttributeList; }
        }
    }

    MapDataJsonFile mapJsonFile = null;
    

    class TileSprite
    {
        public Sprite spriteObject;
        public int tileID = -1;
        public TileSprite(Sprite _sprite ,int _id)
        {
            spriteObject = _sprite;
            tileID = _id;
        }
    }


    List<TileSprite> mapTileRenderSpriteList = new List<TileSprite>();

    int mapPosX = 0;
    int mapPosY = 0;

    int mapNextPosX = 0;
    int mapNextPosY = 0;

    float mapTileScaleX = 1.0f;
    float mapTileScaleY = 1.0f;

    int mapCenterOffsetX = 0;
    int mapCenterOffsetY = 0;

    uint playerUID = 0;

    int fixedScreenWidth = 1920;
    int fixedScreenHeight = 1080;
    int fixedScreenOffsetX = 0;
    int fixedScreenOffsetY = 0;

    tyoStructure.tyoPointInt mapRenderOffset = new tyoStructure.tyoPointInt();
    List<tyoStructure.tyoPointInt> allowPathPointList = new List<tyoStructure.tyoPointInt>();
    List<tyoPlayer> playerList = new List<tyoPlayer>();

    tyoScene.tyoSceneNode mapRenderSceneNode = null;

    public class MapRenderSprite
    {
        public MapRenderSprite()
        {

        }   

        public tyoSprite sprite = null;
        public int x = 0;
        public int y = 0;
        public int depth = 0;
    }
    List<MapRenderSprite> currentRenderMapTileSpriteList = new List<MapRenderSprite>();

	public tyoMap()
	{
		
	}
	
	public void LoadMap(string _mapFile,tyoScene.tyoSceneNode _renderSceneNode)
	{
        try
        {
           string _b64Json = tyoCore.resources.LoadMapJson(_mapFile);

            _b64Json = _b64Json.Replace('$', '=');
            _b64Json = _b64Json.Replace('#', '0');
            _b64Json = _b64Json.Replace(')', '1');
            _b64Json = _b64Json.Replace('(', '2');
            _b64Json = _b64Json.Replace('@', '3');
            _b64Json = _b64Json.Replace('%', '4');
            _b64Json = _b64Json.Replace('&', '5');
            _b64Json = _b64Json.Replace('{', '6');
            _b64Json = _b64Json.Replace('}', '7');
            _b64Json = _b64Json.Replace('[', '8');
            _b64Json = _b64Json.Replace(']', '9');

            byte[] _dncodeJson = System.Convert.FromBase64String(_b64Json);
            string _jsonString = System.Text.Encoding.Default.GetString(_dncodeJson);
            
            //tyoCore.Log(_jsonString);
            mapJsonFile = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataJsonFile>(_jsonString);
            InitMap();
            mapRenderSceneNode = _renderSceneNode;

        }
        catch (System.Exception ex)
        {
            tyoCore.Log(ex.Message);
        }
	}

    void InitMap()
    {
        mapTileRenderSpriteList.Clear();

        for (int i = 0; i < mapJsonFile.MapUsedTileInfosList.Count; ++i)
        {
            string _name = mapJsonFile.MapUsedTileInfosList[i].Name;
            int _id = mapJsonFile.MapUsedTileInfosList[i].TileID;

            _name = System.IO.Path.GetFileNameWithoutExtension(_name) ;
            Sprite _spriteTile = tyoCore.resources.GetSpriteByName(_name);

            if ( _spriteTile != null )
            {
                Rect _spriteRect = new Rect(mapJsonFile.MapUsedTileInfosList[i].TileX,
                (_spriteTile.texture.height - mapJsonFile.MapUsedTileInfosList[i].TileH - mapJsonFile.MapUsedTileInfosList[i].TileY),
                mapJsonFile.MapUsedTileInfosList[i].TileW,
                mapJsonFile.MapUsedTileInfosList[i].TileH
                );

                Sprite _spr = Sprite.Create(_spriteTile.texture,_spriteRect,new Vector2(0.5f,0.5f),1);
                mapTileRenderSpriteList.Add(new TileSprite(_spr,_id));
            }
        }

        if ( mapJsonFile.MapTileWidth > 0 )
        {
            if (Screen.width > fixedScreenWidth)
            {
                mapCenterOffsetX = fixedScreenWidth / mapJsonFile.MapTileWidth / 2;

                int _gloOffsetX = Screen.width - fixedScreenWidth;

                if(_gloOffsetX > 0)
                {
                    fixedScreenOffsetX = _gloOffsetX / 2;
                }
            }
            else
            {
                fixedScreenWidth = Screen.width;
                mapCenterOffsetX = Screen.width / mapJsonFile.MapTileWidth / 2;
            }
        }

        if ( mapJsonFile.MapTileHeight > 0 )
        {
            if (Screen.height > fixedScreenHeight)
            {
                mapCenterOffsetY = fixedScreenHeight / mapJsonFile.MapTileHeight / 2;

                int _gloOffsetY = Screen.height - fixedScreenHeight;

                if(_gloOffsetY > 0)
                {
                    fixedScreenOffsetY = _gloOffsetY / 2;
                }
            }
            else
            {
                fixedScreenHeight = Screen.height;
                mapCenterOffsetY = Screen.height / mapJsonFile.MapTileHeight / 2;
            }
        }

        for ( int x = 0; x < mapJsonFile.MapSizeWidth; ++x)
        {
            for ( int y = 0; y < mapJsonFile.MapSizeHeight; ++y)
            {
                if ( !mapJsonFile.MapBlockFlag[x,y])
                {
                    tyoStructure.tyoPointInt _p = new tyoStructure.tyoPointInt(x,y);
                    allowPathPointList.Add(_p);
                }
            }
        }


        if ( allowPathPointList.Count > 0)
        {
            allowPathPointList = tyoHelper.GetRandomList(allowPathPointList);
        }

        
    }

    public void Clear()
    {

    }

    tyoStructure.tyoPointInt lastRenderMapPos = new tyoStructure.tyoPointInt(-1,-1);

    float GetMapRenderOffsetX()
    {
        if ( mapPosX <= 1 || mapPosX >= mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3)
        {
            if(mapPosX == 1 && mapNextPosX == 2)
            {
                return mapRenderOffset.X;
            }

            if(mapPosX == mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3 && mapNextPosX == mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 4)
            {
                return mapRenderOffset.X;
            }

            return 0.0f;
        }
        
        return mapRenderOffset.X;
    }

    float GetMapRenderOffsetY()
    {
        if ( mapPosY <= 1 || mapPosY >=  mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2)
        {
            if(mapPosY == 1 && mapNextPosY == 2)
            {
                return mapRenderOffset.Y;
            }

            if(mapPosY ==  mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2 && mapNextPosY == mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 3)
            {
                return mapRenderOffset.Y;
            }

            return 0.0f;
        }

        return mapRenderOffset.Y;
    }

    float GetPlayerRenderOffsetX(tyoPlayer _player)
    {
        if ( mapPosX <= 1 || mapPosX >= mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3)
        {
            if(mapPosX == 1 && mapNextPosX == 2)
            {
                return 0.0f;
            }

            if(mapPosX == mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3 && mapNextPosX == mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 4)
            {
                return 0.0f;
            }

            return _player.moveOffset.X;
        }

        return 0.0f;
    }

    float GetPlayerRenderOffsetY(tyoPlayer _player)
    {
        if ( mapPosY <= 1 || mapPosY >=  mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2)
        {
            if(mapPosY == 1 && mapNextPosY == 2)
            {
                return 0.0f;
            }

            if(mapPosY ==  mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2 && mapNextPosY == mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 3)
            {
                return 0.0f;
            }

            return _player.moveOffset.Y;
        }

        return 0.0f;
    }

    public void RenderMap()
    {
        if (lastRenderMapPos.X == mapPosX && lastRenderMapPos.Y == mapPosY)
        {
            for (int i = 0; i < mapJsonFile.MapLayerInfosList.Count; ++i)
            {
                if ( mapJsonFile.MapLayerInfosList[i].Name == "player" )
                {
                    foreach(tyoPlayer _player in playerList)
                    {
                        if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                        {
                            float _pos_x = (float)((_player.currentPosition.X - mapPosX) * mapJsonFile.MapTileWidth) + GetPlayerRenderOffsetX(_player) + fixedScreenOffsetX;
                            float _pos_y = (float)((_player.currentPosition.Y - mapPosY) * mapJsonFile.MapTileHeight) + GetPlayerRenderOffsetY(_player) + fixedScreenOffsetY;
                            float _pos_z = (float)( mapJsonFile.MapLayerInfosList[i].Depth * -1.0f);

                            _player.GetPlayerSprite().SetPosition(_pos_x,_pos_y,_pos_z);     

                            
                            foreach(MapRenderSprite _renderSprite in currentRenderMapTileSpriteList)
                            {
                                _pos_x = (float)(_renderSprite.x * mapJsonFile.MapTileWidth) + GetMapRenderOffsetX() + fixedScreenOffsetX;
                                _pos_y = (float)(_renderSprite.y * mapJsonFile.MapTileHeight) + GetMapRenderOffsetY() + fixedScreenOffsetY;
                                _pos_z = (float)(_renderSprite.depth) * -1.0f;

                                _renderSprite.sprite.SetPosition(_pos_x,_pos_y,_pos_z);
                            }
                        }
                        else
                        {
                            float _pos_x = (float)((_player.currentPosition.X - mapPosX) * mapJsonFile.MapTileWidth) + GetMapRenderOffsetX() + GetPlayerRenderOffsetX(_player) + fixedScreenOffsetX;
                            float _pos_y = (float)((_player.currentPosition.Y - mapPosY) * mapJsonFile.MapTileHeight) + GetMapRenderOffsetY() + GetPlayerRenderOffsetY(_player) + fixedScreenOffsetY;
                            float _pos_z = (float)( mapJsonFile.MapLayerInfosList[i].Depth * -1.0f);

                            _player.GetPlayerSprite().SetPosition(_pos_x,_pos_y,_pos_z);     
                        }
                    }

                    break;
                }
            }

            return;
        }
        else
        {
            foreach(MapRenderSprite _renderSprite in currentRenderMapTileSpriteList)
            {
                _renderSprite.sprite.RemoveSceneRender(mapRenderSceneNode);
            }

            currentRenderMapTileSpriteList.Clear();
        }

        int _wcount = (int)((float)fixedScreenWidth / (mapJsonFile.MapTileWidth * mapTileScaleX)) + 1;
        int _hcount = (int)((float)fixedScreenHeight / (mapJsonFile.MapTileHeight * mapTileScaleY)) + 1;

        for (int i = 0; i < mapJsonFile.MapLayerInfosList.Count; ++i)
        {
            if ( mapJsonFile.MapLayerInfosList[i].Name == "player" )
            {
                foreach(tyoPlayer _player in playerList)
                {
                    if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                    {
                        float _pos_x = (float)((_player.currentPosition.X - mapPosX) * mapJsonFile.MapTileWidth) + GetPlayerRenderOffsetX(_player) + fixedScreenOffsetX;
                        float _pos_y = (float)((_player.currentPosition.Y - mapPosY) * mapJsonFile.MapTileHeight) + GetPlayerRenderOffsetY(_player) + fixedScreenOffsetY;
                        float _pos_z = (float)( mapJsonFile.MapLayerInfosList[i].Depth * -1.0f);

                        _player.GetPlayerSprite().SetPosition(_pos_x,_pos_y,_pos_z);
                    }
                    else
                    {
                        float _pos_x = (float)((_player.currentPosition.X - mapPosX) * mapJsonFile.MapTileWidth) + GetMapRenderOffsetX() + GetPlayerRenderOffsetX(_player) + fixedScreenOffsetX;
                        float _pos_y = (float)((_player.currentPosition.Y - mapPosY) * mapJsonFile.MapTileHeight) + GetMapRenderOffsetY() + GetPlayerRenderOffsetY(_player) + fixedScreenOffsetY;
                        float _pos_z = (float)( mapJsonFile.MapLayerInfosList[i].Depth * -1.0f);

                        _player.GetPlayerSprite().SetPosition(_pos_x,_pos_y,_pos_z);
                    }
                }

                continue;
            }
            
            for (int x = mapPosX; x < mapPosX + _wcount; ++x)
            {
                if (x >= mapJsonFile.MapSizeWidth)
                {
                    break;
                }

                for (int y = mapPosY; y < mapPosY + _hcount; ++y)
                {
                    if (y >= mapJsonFile.MapSizeHeight)
                    {
                        break;
                    }

                    int index = mapJsonFile.MapTile[i,x,y];

                    if (index != -1)
                    {
                        TileSprite _tileSprite = mapTileRenderSpriteList[index]; 
                        if ( _tileSprite != null )
                        {
                            if ( _tileSprite.spriteObject != null )
                            {
                                MapRenderSprite _renderSprite = new MapRenderSprite();

                                _renderSprite.sprite = new tyoSprite(_tileSprite.spriteObject);
                                _renderSprite.sprite.name = "tile sprite";

                                _renderSprite.x = x - mapPosX;
                                _renderSprite.y = y - mapPosY;
                                _renderSprite.depth = mapJsonFile.MapLayerInfosList[i].Depth;

                                float _pos_x = (float)(_renderSprite.x * mapJsonFile.MapTileWidth) + GetMapRenderOffsetX() + fixedScreenOffsetX;
                                float _pos_y = (float)(_renderSprite.y * mapJsonFile.MapTileHeight) + GetMapRenderOffsetY() + fixedScreenOffsetY;
                                float _pos_z = (float)(_renderSprite.depth) * -1.0f;

                                _renderSprite.sprite.SetPosition(_pos_x,_pos_y,_pos_z);
                                _renderSprite.sprite.AddSceneRender(mapRenderSceneNode);

                                currentRenderMapTileSpriteList.Add(_renderSprite);
                            }
                        }
                    }
                }
            }
        }
        
        lastRenderMapPos.X = mapPosX;
        lastRenderMapPos.Y = mapPosY;
    }

    public uint AddMapPlayer(tyoPlayer _player)
    {
        playerUID += (uint)(UnityEngine.Random.Range(0,100));
        playerList.Add(_player);
        _player.playerUIDInMap = playerUID;
        return playerUID;
    }

    public void SetPlayerRandomPos(uint _playerUID)
    {
        tyoPlayer _player = playerList.Find( t => t.playerUIDInMap == _playerUID);

        if ( _player != null )
        {
            int _idx = UnityEngine.Random.Range(0,allowPathPointList.Count);

            int _p_x = allowPathPointList[_idx].X;
            int _p_y = allowPathPointList[_idx].Y;

            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
            {
                if ( _p_x - mapCenterOffsetX >= 0)
                {
                    mapPosX = _p_x - mapCenterOffsetX + 1;
                }

                if ( _p_x + mapCenterOffsetX + 3 >= mapJsonFile.MapSizeWidth)
                {
                    mapPosX = mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3;
                }

                if ( _p_y - mapCenterOffsetY >= 0)
                {
                    mapPosY = _p_y - mapCenterOffsetY + 1;
                }

                if ( _p_y + mapCenterOffsetY + 2 >= mapJsonFile.MapSizeHeight)
                {
                    mapPosY = mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2;
                }
            } 

            _player.InitPosition(_p_x,_p_y);
        }
        
        //mapJsonFile.MapLayerInfosList
    }

    void UpdateMapPosFromPlayer(tyoPlayer _player)
    {
        if ( _player.currentPosition.X - mapCenterOffsetX >= 0)
        {
            mapPosX = _player.currentPosition.X - mapCenterOffsetX + 1;
        }

        if ( _player.currentPosition.X + mapCenterOffsetX + 3 >= mapJsonFile.MapSizeWidth)
        {
            mapPosX = mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3;
        }

        if ( _player.currentPosition.Y - mapCenterOffsetY >= 0)
        {
            mapPosY = _player.currentPosition.Y - mapCenterOffsetY + 1;
        }

        if ( _player.currentPosition.Y + mapCenterOffsetY + 2 >= mapJsonFile.MapSizeHeight)
        {
            mapPosY = mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2;
        }
    }

    void UpdateNextMapPosFromNextPlayerPos(int _posX,int _posY)
    {
        if ( _posX - mapCenterOffsetX >= 0)
        {
            mapNextPosX = _posX - mapCenterOffsetX + 1;
        }

        if ( _posX + mapCenterOffsetX + 3 >= mapJsonFile.MapSizeWidth)
        {
            mapNextPosX = mapJsonFile.MapSizeWidth - mapCenterOffsetX * 2 - 3;
        }

        if ( _posY - mapCenterOffsetY >= 0)
        {
            mapNextPosY = _posY - mapCenterOffsetY + 1;
        }

        if ( _posY + mapCenterOffsetY + 2 >= mapJsonFile.MapSizeHeight)
        {
            mapNextPosY = mapJsonFile.MapSizeHeight - mapCenterOffsetY * 2 - 2;
        }
    }

    public void UpdateMap(float _dt)
    {
        foreach(tyoPlayer _player in playerList)
        {
            if ( _player.CheckMoveDt() )
            {
                if ( _player.currentAction == tyoPlayer.PlayerAction._move_left)
                {
                    int _next_x = _player.currentPosition.X - 1;
                    int _next_y = _player.currentPosition.Y;

                    UpdateNextMapPosFromNextPlayerPos(_next_x,_next_y);

                    if (!(_next_x < 0 || mapJsonFile.MapBlockFlag[_next_x,_next_y]))
                    {
                        int _t_ofx = _player.moveOffset.X - _player.moveSpeed;
                        int _t_mfx = mapRenderOffset.X + _player.moveSpeed;

                        if ( Mathf.Abs(_t_ofx) >= mapJsonFile.MapTileWidth )
                        {
                            _player.currentPosition.X = _next_x;
                            _player.currentPosition.Y = _next_y;

                            _player.moveOffset.X = _t_ofx + mapJsonFile.MapTileWidth;
                            
                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.X = _t_mfx - mapJsonFile.MapTileWidth;
                            }

                            UpdateMapPosFromPlayer(_player);
                        }
                        else
                        {
                            _player.moveOffset.X = _t_ofx;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.X = _t_mfx;
                            }
                            
                        }   
                    }

                }
                else if ( _player.currentAction == tyoPlayer.PlayerAction._move_right)
                {
                    int _next_x = _player.currentPosition.X + 1;
                    int _next_y = _player.currentPosition.Y;

                    UpdateNextMapPosFromNextPlayerPos(_next_x,_next_y);

                    if (!(_next_x >= mapJsonFile.MapSizeWidth || mapJsonFile.MapBlockFlag[_next_x,_next_y]))
                    {
                        int _t_ofx = _player.moveOffset.X + _player.moveSpeed;
                        int _t_mfx = mapRenderOffset.X - _player.moveSpeed;

                        if ( Mathf.Abs(_t_ofx) >= mapJsonFile.MapTileWidth )
                        {
                        
                                _player.currentPosition.X = _next_x;
                                _player.currentPosition.Y = _next_y;

                                _player.moveOffset.X = _t_ofx - mapJsonFile.MapTileWidth ;

                                if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                                {
                                    mapRenderOffset.X = _t_mfx + mapJsonFile.MapTileWidth;
                                }

                                UpdateMapPosFromPlayer(_player);
                            
                        }
                        else
                        {
                            _player.moveOffset.X = _t_ofx;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.X = _t_mfx;
                            }
                        }
                    }
                }
                else if ( _player.currentAction == tyoPlayer.PlayerAction._move_up)
                {
                    int _next_x = _player.currentPosition.X;
                    int _next_y = _player.currentPosition.Y - 1;

                    UpdateNextMapPosFromNextPlayerPos(_next_x,_next_y);

                    if (!(_next_y < 0 || mapJsonFile.MapBlockFlag[_next_x,_next_y]))
                    {
                        int _t_ofy = _player.moveOffset.Y - _player.moveSpeed;
                        int _t_mfy = mapRenderOffset.Y + _player.moveSpeed;

                        if ( Mathf.Abs(_t_ofy) >= mapJsonFile.MapTileHeight )
                        {
                            
                            _player.currentPosition.X = _next_x;
                            _player.currentPosition.Y = _next_y;

                            _player.moveOffset.Y = _t_ofy + mapJsonFile.MapTileHeight ;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.Y = _t_mfy - mapJsonFile.MapTileHeight;
                            }

                            UpdateMapPosFromPlayer(_player);
                        
                            
                        }
                        else
                        {
                            _player.moveOffset.Y = _t_ofy;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.Y = _t_mfy;
                            }
                        }
                    }
                
                }
                else if ( _player.currentAction == tyoPlayer.PlayerAction._move_down )
                {
                    int _next_x = _player.currentPosition.X;
                    int _next_y = _player.currentPosition.Y + 1;

                    UpdateNextMapPosFromNextPlayerPos(_next_x,_next_y);

                    if (!(_next_y >= mapJsonFile.MapSizeHeight || mapJsonFile.MapBlockFlag[_next_x,_next_y]))
                    {
                        int _t_ofy = _player.moveOffset.Y + _player.moveSpeed;
                        int _t_mfy = mapRenderOffset.Y - _player.moveSpeed;

                        if ( Mathf.Abs(_t_ofy) >= mapJsonFile.MapTileHeight )
                        {
                            
                            _player.currentPosition.X = _next_x;
                            _player.currentPosition.Y = _next_y;

                            _player.moveOffset.Y = _t_ofy - mapJsonFile.MapTileHeight;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.Y = _t_mfy + mapJsonFile.MapTileHeight;
                            }

                            UpdateMapPosFromPlayer(_player);
                            
                        }
                        else
                        {
                            _player.moveOffset.Y = _t_ofy;

                            if ( _player.currentRoleType == tyoPlayer.PlayerRoleType._main )
                            {
                                mapRenderOffset.Y = _t_mfy;
                            }
                        }
                    }
                }
            }

            _player.Update(_dt);
        }
    }
}
