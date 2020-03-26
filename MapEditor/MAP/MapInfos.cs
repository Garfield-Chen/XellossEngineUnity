using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace tyoEngineEditor
{
    public class MapTileInfos
    {
        public MapTileInfos()
        {

        }

        [Description("地图图块名字。"), Category("基本信息")]
        public string Name
        {
            set
            {
                _name = value;
            }

            get
            {
                return _name;
            }

        }

        [Description("地图图块文件名字。"), Category("基本信息")]
        public string FileName
        {
            get
            {
                return _filename;
            }

        }

        [Description("地图图块文件绝对路径。"), Category("基本信息")]
        public string FilePath
        {
            get
            {
                return _filepath;
            }

        }

        public string _name;

        public string _filename;
        public string _filepath;

        public string dirname;
        public int index;

        public override string ToString()
        {
            return Name;
        }
    }

    public class MapLayerInfos
    {
        public MapLayerInfos()
        {

        }

        [Description("地图图层名字。"), Category("基本信息")]
        public string Name
        {
            set
            {
                _name = value;
            }

            get
            {
                return _name;
            }

        }

        [Description("地图图层顺序。"), Category("基本信息")]
        public int Depth
        {
            set
            {
                _depth = value;
            }

            get
            {
                return _depth;
            }

        }

        public string _name = "默认图层";
        public int _depth = 0;
        public string opType = string.Empty;

        public override string ToString()
        {
            return _name;
        }
    }

    public class MapUseTileInfo //地图使用的块块信息
    {
        public MapUseTileInfo()
        {

        }

        public tyoEngineTileMapEditWindow.MapTilePiece _image = null; //图块资源
        public string _name = ""; //使用的图块名字
        public int _id = -1; //使用的图块id
        public int _comboIndex = 0; //在combox中的index
        public bool _flag = false; //是否使用过
    }

    public class ActionInfo
    {
        public class ActionInfoNode
        {
            public ActionInfoNode(int _data, bool _blockData, int _layer, int _x, int _y)
            {
                _Data = _data;
                _BlockData = _blockData;
                _Layer = _layer;
                _X = _x;
                _Y = _y;
            }

            public int _Data = -1;
            public bool _BlockData = false;
            public int _Layer = -1;
            public int _X = -1;
            public int _Y = -1;
        }

        public ActionInfo()
        {

        }

        public void AddAction(int _data, bool _blockData,int _layer, int _x, int _y)
        {
            ActionInfoNode _node = new ActionInfoNode(_data, _blockData, _layer, _x, _y);
            _ActionNodeList.Add(_node);
        }

        public bool _IsBlock = false;
        public bool _IsDelPiece = false;
        public List<ActionInfoNode> _ActionNodeList = new List<ActionInfoNode>(256);
    }

    public enum MapTileAttributeType
    {
        string_attr = 0x1,
        float_attr ,
        int_attr ,
        bool_attr ,
        double_attr ,
    }

    public class MapTileAttribute
    {
        [Description("属性_1"), Category("属性1")]
        public string Value1
        {
            set
            {
                _value1 = value;
            }

            get
            {
                return _value1;
            }
        }

        [Description("属性_1_类型"), Category("属性1")]
        public MapTileAttributeType Value1_Type
        {
            set
            {
                _value1_type = value;
            }

            get
            {
                return _value1_type;
            }
        }

        [Description("属性_1_描述"), Category("属性1")]
        public string Value1_Description
        {
            set
            {
                _value1_description = value;
            }

            get
            {
                return _value1_description;
            }
        }

        string _value1 = "0";
        MapTileAttributeType _value1_type = MapTileAttributeType.string_attr;
        string _value1_description = "default";

        [Description("属性_2"), Category("属性2")]
        public string Value2
        {
            set
            {
                _value2 = value;
            }

            get
            {
                return _value2;
            }
        }

        [Description("属性_2_类型"), Category("属性2")]
        public MapTileAttributeType Value2_Type
        {
            set
            {
                _value2_type = value;
            }

            get
            {
                return _value1_type;
            }
        }

        [Description("属性_2_描述"), Category("属性2")]
        public string Value2_Description
        {
            set
            {
                _value2_description = value;
            }

            get
            {
                return _value2_description;
            }
        }

        string _value2 = "0";
        MapTileAttributeType _value2_type = MapTileAttributeType.string_attr;
        string _value2_description = "default";

        [Description("属性_3"), Category("属性3")]
        public string Value3
        {
            set
            {
                _value3 = value;
            }

            get
            {
                return _value3;
            }
        }

        [Description("属性_3_类型"), Category("属性3")]
        public MapTileAttributeType Value3_Type
        {
            set
            {
                _value3_type = value;
            }

            get
            {
                return _value1_type;
            }
        }

        [Description("属性_3_描述"), Category("属性3")]
        public string Value3_Description
        {
            set
            {
                _value3_description = value;
            }

            get
            {
                return _value3_description;
            }
        }

        string _value3 = "0";
        MapTileAttributeType _value3_type = MapTileAttributeType.string_attr;
        string _value3_description = "default";

        [Description("属性_4"), Category("属性4")]
        public string Value4
        {
            set
            {
                _value4 = value;
            }

            get
            {
                return _value4;
            }
        }

        [Description("属性_4_类型"), Category("属性4")]
        public MapTileAttributeType Value4_Type
        {
            set
            {
                _value4_type = value;
            }

            get
            {
                return _value1_type;
            }
        }

        [Description("属性_4_描述"), Category("属性4")]
        public string Value4_Description
        {
            set
            {
                _value4_description = value;
            }

            get
            {
                return _value4_description;
            }
        }

        string _value4 = "0";
        MapTileAttributeType _value4_type = MapTileAttributeType.string_attr;
        string _value4_description = "default";

        public MapTileAttribute(int _x,int _y,int _layer)
        {
            _tile_x = _x;
            _tile_y = _y;
            _tile_layer = _layer;
        }

        public bool Find(int _x, int _y, int _layer)
        {
            if( _tile_x == _x && _tile_y == _y && _tile_layer == _layer)
            {
                return true;
            }

            return false;
        }


        public int _tile_x = -1;
        public int _tile_y = -1;
        public int _tile_layer = -1;

        [Description("删除[标记True后,结束编辑将会从列表清除]"), Category("编辑属性")]
        public bool DeleteFlag
        {
            set
            {
                _deleteFlag = value;
            }

            get
            {
                return _deleteFlag;
            }
        }

        public bool _deleteFlag = false;
    }

    public class MapInfos
    {
        public MapInfos()
        {
            for (int i = 0; i < 128; ++i)
            {
                ActionInfo _tmp = new ActionInfo();
                _ActionList.Add(_tmp);
            }
        }

        [Description("地图名字。"), Category("基本信息")]
        public string Name
        {
            set
            {
                _name = value;
            }

            get
            {
                return _name;
            }

        }

        [Description("地图宽度。"), Category("基本信息")]
        public int Map_Size_Width
        {
            set
            {
                _map_Width = value;
            }

            get
            {
                return _map_Width;
            }
        }

        [Description("地图高度。"), Category("基本信息")]
        public int Map_Size_Height
        {
            set
            {
                _map_Height = value;
            }

            get
            {
                return _map_Height;
            }
        }

        [Description("地图图块宽度。"), Category("基本信息")]
        public int Map_Tile_Width
        {
            set
            {
                _map_tile_w = value;
            }

            get
            {
                return _map_tile_w;
            }
        }

        [Description("地图图块高度。"), Category("基本信息")]
        public int Map_Tile_Height
        {
            set
            {
                _map_tile_h = value;
            }

            get
            {
                return _map_tile_h;
            }
        }

        string _name = "新的地图"; //地图名字
        int _map_Width = 32; //窗口宽
        int _map_Height = 32; //高
        int _map_tile_w = 32; //地图图块宽
        int _map_tile_h = 32; //地图图块高

        public string loadPath = string.Empty;//读取地图的路径，为空则是新建地图

        public bool _IsLoadMap = false;

        public bool _IsLoadMonstor = false;

        public Dictionary<int, MapTileInfos> _mapTileInfosByIndex = new Dictionary<int, MapTileInfos>();

        public List<MapUseTileInfo> _mapTileUseInfo = new List<MapUseTileInfo>();

        public List<string> animationPNGpath = new List<string>();

        public List<string> animationXML = new List<string>();

        public int[,,] _mapTile = null;

        public int[,] _mapExternFlag1 = null;

        public bool[,] _mapBlockFlag = null;

        public bool[] _LayerShowFlag = null;

        public Dictionary<int, MapLayerInfos> _mapLayerInfosByIndex = new Dictionary<int, MapLayerInfos>();

        public List<MapTileAttribute> _mapTileAttributeList = new List<MapTileAttribute>();

        int _ActionIndex = -1;
        int _ActionCount = 0;
        int _UnDoCount = 0;

        int _RedoCount = 0;

        List<ActionInfo> _ActionList = new List<ActionInfo>(128);

        #region 方法

        void ClearRedoCount()
        {
            _RedoCount = 0;
            _UnDoCount = 126;
        }

        //重做
        public void Redo()
        {
            if (_RedoCount > 0)
            {
                SetMapDataByAction(_ActionIndex++);

                _RedoCount--;
                _UnDoCount++;
            }
        }

        //撤销
        public void UnDo()
        {
            if (_UnDoCount > 0 && _ActionIndex > 0 && _ActionIndex <= _ActionCount)
            {

                SetMapDataByAction(--_ActionIndex);

                _RedoCount++;
                _UnDoCount--;
                
            }
        }

        public MapTileAttribute GetMapTileAttribute(int _x,int _y,int _layer)
        {
            MapTileAttribute _attribute = _mapTileAttributeList.Find(t => t.Find(_x, _y, _layer) == true);

            if (_attribute != null)
            {
                return _attribute;
            }

            _attribute = new MapTileAttribute(_x, _y, _layer);

            _mapTileAttributeList.Add(_attribute);

            return _attribute;
        }

        void SetMapDataByAction(int _actionIndex)
        {
            if (_ActionList[_actionIndex]._IsBlock)
            {
                for (int i = 0; i < _ActionList[_actionIndex]._ActionNodeList.Count; ++i)
                {
                    if (_ActionList[_actionIndex]._ActionNodeList[i]._X < 0 || _ActionList[_actionIndex]._ActionNodeList[i]._Y < 0)
                    {
                        continue;
                    }

                    _mapBlockFlag[_ActionList[_actionIndex]._ActionNodeList[i]._X, _ActionList[_actionIndex]._ActionNodeList[i]._Y] = _ActionList[_actionIndex]._ActionNodeList[i]._BlockData;
                }
            }
            else
            {
                for (int i = 0; i < _ActionList[_actionIndex]._ActionNodeList.Count; ++i)
                {
                    if (_ActionList[_actionIndex]._ActionNodeList[i]._Layer < 0 || _ActionList[_actionIndex]._ActionNodeList[i]._X < 0 || _ActionList[_actionIndex]._ActionNodeList[i]._Y < 0)
                    {
                        continue;
                    }

                    //                     if (_ActionList[_actionIndex]._ActionNodeList[i]._Data == -1 && _ActionList[_actionIndex]._IsDelPiece == false)
                    //                     {
                    //                         continue;
                    //                     }

                    _mapTile[
                        _ActionList[_actionIndex]._ActionNodeList[i]._Layer,
                        _ActionList[_actionIndex]._ActionNodeList[i]._X,
                        _ActionList[_actionIndex]._ActionNodeList[i]._Y
                        ]
                        = _ActionList[_actionIndex]._ActionNodeList[i]._Data;
                }
            }
        }

        /// <summary>
        /// 图块，图块编辑界面中的combox的序号，名称，id号
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="comboxIndex"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddMapUseTileInfo(tyoEngineTileMapEditWindow.MapTilePiece piece, 
            int comboxIndex, String name, int id)
        {
            for (int i = 0; i < _mapTileUseInfo.Count; ++i)
            {
                if (_mapTileUseInfo[i]._id == id && _mapTileUseInfo[i]._name 
                    == name && _mapTileUseInfo[i]._comboIndex == comboxIndex)
                {
                    return i;
                }
            }

            MapUseTileInfo tmpUseInfos = new MapUseTileInfo();

            tmpUseInfos._image = piece;
            tmpUseInfos._comboIndex = comboxIndex;
            tmpUseInfos._name = name;
            tmpUseInfos._id = id;

            _mapTileUseInfo.Add(tmpUseInfos);

            return _mapTileUseInfo.Count - 1;
        }

        public void ReInit()
        {
            _mapTileUseInfo.Clear();

            _mapTile = new int[_mapLayerInfosByIndex.Count,_map_Width, _map_Height];
            _mapExternFlag1 = new int[_map_Width, _map_Height];
            _mapBlockFlag = new bool[_map_Width, _map_Height];

            for (int i = 0; i < _mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _map_Width; ++x)
                {
                    for (int y = 0; y < _map_Height; ++y)
                    {
                        _mapTile[i, x, y] = -1;

                        if(i == 0)
                        {
                            _mapExternFlag1[x, y] = 0;
                            _mapBlockFlag[x, y] = false;
                        }
                    }
                }
            }

            _LayerShowFlag = new bool[_mapLayerInfosByIndex.Count];

            _ActionIndex = -1;
            _ActionCount = 0;
            //_ActionList.Clear();
            _RedoCount = 0;
        }

        public void ActionBegin(bool _isBlock,bool _isDel)
        {
            if (_ActionIndex < 0 || _ActionIndex >= 128)
            {
                _ActionIndex = 1;
            }
            else
            {
                _ActionIndex++;
            }

            _ActionList[_ActionIndex - 1]._IsBlock = _isBlock;
            _ActionList[_ActionIndex - 1]._IsDelPiece = _isDel;
            _ActionList[_ActionIndex - 1]._ActionNodeList.Clear();

            if (_ActionCount <= 128)
            {
                _ActionCount++;
            }

            ClearRedoCount();

            
        }

        public void ActionEnd()
        {
            int _lastActionIndex = _ActionIndex - 2;

            if (_lastActionIndex <= -1 )
            {
                _lastActionIndex = 127;
            }

            if ( _ActionList[_ActionIndex - 1]._IsBlock == _ActionList[_lastActionIndex]._IsBlock &&
                _ActionList[_ActionIndex - 1]._ActionNodeList.Count == _ActionList[_lastActionIndex]._ActionNodeList.Count)
            {
                for (int i = 0; i < _ActionList[_ActionIndex - 1]._ActionNodeList.Count; ++i)
                {
                    if ( _ActionList[_ActionIndex - 1]._ActionNodeList[i]._BlockData != _ActionList[_lastActionIndex]._ActionNodeList[i]._BlockData ||
                        _ActionList[_ActionIndex - 1]._ActionNodeList[i]._Data != _ActionList[_lastActionIndex]._ActionNodeList[i]._Data ||
                        _ActionList[_ActionIndex - 1]._ActionNodeList[i]._Layer != _ActionList[_lastActionIndex]._ActionNodeList[i]._Layer ||
                        _ActionList[_ActionIndex - 1]._ActionNodeList[i]._X != _ActionList[_lastActionIndex]._ActionNodeList[i]._X ||
                        _ActionList[_ActionIndex - 1]._ActionNodeList[i]._Y != _ActionList[_lastActionIndex]._ActionNodeList[i]._Y)
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            _ActionIndex = _lastActionIndex;
        }

        public void SetMapTile(int layer,int x, int y,int index)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return;
            }

            if (layer < 0 || layer >= _mapLayerInfosByIndex.Count)
            {
                return;
            }

            _ActionList[_ActionIndex - 1].AddAction(_mapTile[layer, x, y], false, layer, x, y);

            _mapTile[layer, x, y] = index;
        }

        public int GetMapTilePieceIndex(int layer, int x, int y)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return -2;
            }

            if (layer < 0 || layer >= _mapLayerInfosByIndex.Count)
            {
                return -2;
            }

            return _mapTile[layer, x, y];
        }

        public void SetMapTileBlock(int x, int y, bool isblock)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return;
            }

            if (_ActionIndex < 0 || _ActionIndex > 16)
            {
                _ActionIndex = 1;
            }
            else
            {
                _ActionIndex++;
            }

            _ActionList[_ActionIndex - 1].AddAction(-1, _mapBlockFlag[x, y], -1, x, y);

            _mapBlockFlag[x, y] = isblock;

            if (_ActionCount <= 16)
            {
                _ActionCount++;
            }

            ClearRedoCount();
        }
        #endregion
    }

    //排序用
    public class MapLayerDrawInfos
    {
        public MapLayerDrawInfos()
        {

        }

        public int _depth = 0;
        public int _index = 0;
    }



    public class MapDataJsonFile
    {
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
}
