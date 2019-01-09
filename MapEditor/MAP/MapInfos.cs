using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace tyoEngineEditor
{
    public class MapTitleInfos
    {
        public MapTitleInfos()
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

    public class MapUseTitleInfo //地图使用的块块信息
    {
        public MapUseTitleInfo()
        {

        }

        public tyoEngineTitleMapEditWindow.MapTitlePiece _image = null; //图块资源
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

        public void AddAction(int _data, bool _blockData, int _layer, int _x, int _y)
        {
            ActionInfoNode _node = new ActionInfoNode(_data, _blockData, _layer, _x, _y);
            _ActionNodeList.Add(_node);
        }

        public bool _IsBlock = false;
        public bool _IsDelPiece = false;
        public List<ActionInfoNode> _ActionNodeList = new List<ActionInfoNode>(256);
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
        public int Map_Title_Width
        {
            set
            {
                _map_title_w = value;
            }

            get
            {
                return _map_title_w;
            }
        }

        [Description("地图图块高度。"), Category("基本信息")]
        public int Map_Title_Height
        {
            set
            {
                _map_title_h = value;
            }

            get
            {
                return _map_title_h;
            }
        }

        string _name = "新的地图"; //地图名字
        int _map_Width = 32; //窗口宽
        int _map_Height = 32; //高
        int _map_title_w = 32; //地图图块宽
        int _map_title_h = 32; //地图图块高

        public string loadPath = string.Empty;//读取地图的路径，为空则是新建地图

        public bool _IsLoadMap = false;

        public bool _IsLoadMonstor = false;

        public bool _IsClickAnimation = false;

        public Dictionary<int, MapTitleInfos> _mapTitleInfosByIndex = new Dictionary<int, MapTitleInfos>();

        public List<MapUseTitleInfo> _mapTitleUseInfo = new List<MapUseTitleInfo>();

        public Dictionary<int, MapUseAnimationInfo> _mapAnimationUseInfo = new Dictionary<int, MapUseAnimationInfo>();

        public Dictionary<int,AnimationOffset> _mapAnimationOffsets = new Dictionary<int,AnimationOffset>();

        public List<string> animationPNGpath = new List<string>();

        public List<string> animationXML = new List<string>();

        public int[,,] _mapTitle = null;

        public int[, ,] _mapAnimationTitle = null;

        public int[,] _mapExternFlag1 = null;

        public bool[,] _mapBlockFlag = null;

        public bool[] _LayerShowFlag = null;

        public Dictionary<int, MapLayerInfos> _mapLayerInfosByIndex = new Dictionary<int, MapLayerInfos>();

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

                    if (_IsClickAnimation) 
                    {
                        _mapAnimationTitle[
                            _ActionList[_actionIndex]._ActionNodeList[i]._Layer,
                            _ActionList[_actionIndex]._ActionNodeList[i]._X,
                            _ActionList[_actionIndex]._ActionNodeList[i]._Y
                            ]
                            = _ActionList[_actionIndex]._ActionNodeList[i]._Data;
                    }
                    else 
                    {
                        _mapTitle[
                        _ActionList[_actionIndex]._ActionNodeList[i]._Layer,
                        _ActionList[_actionIndex]._ActionNodeList[i]._X,
                        _ActionList[_actionIndex]._ActionNodeList[i]._Y
                        ]
                        = _ActionList[_actionIndex]._ActionNodeList[i]._Data;
                    }
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
        public int AddMapUseTitleInfo(tyoEngineTitleMapEditWindow.MapTitlePiece piece, 
            int comboxIndex, String name, int id)
        {
            for (int i = 0; i < _mapTitleUseInfo.Count; ++i)
            {
                if (_mapTitleUseInfo[i]._id == id && _mapTitleUseInfo[i]._name 
                    == name && _mapTitleUseInfo[i]._comboIndex == comboxIndex)
                {
                    return i;
                }
            }

            MapUseTitleInfo tmpUseInfos = new MapUseTitleInfo();

            tmpUseInfos._image = piece;
            tmpUseInfos._comboIndex = comboxIndex;
            tmpUseInfos._name = name;
            tmpUseInfos._id = id;

            _mapTitleUseInfo.Add(tmpUseInfos);

            return _mapTitleUseInfo.Count - 1;
        }

        public void ReInit()
        {
            _mapTitleUseInfo.Clear();

            _mapTitle = new int[_mapLayerInfosByIndex.Count,_map_Width, _map_Height];
            _mapAnimationTitle = new int[_mapLayerInfosByIndex.Count, _map_Width, _map_Height];
            _mapExternFlag1 = new int[_map_Width, _map_Height];
            _mapBlockFlag = new bool[_map_Width, _map_Height];

            for (int i = 0; i < _mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _map_Width; ++x)
                {
                    for (int y = 0; y < _map_Height; ++y)
                    {
                        _mapTitle[i, x, y] = -1;
                        _mapAnimationTitle[i, x, y] = -1;

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

        public void SetMapTilte(int layer,int x, int y,int index)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return;
            }

            if (layer < 0 || layer >= _mapLayerInfosByIndex.Count)
            {
                return;
            }

            _ActionList[_ActionIndex - 1].AddAction(_mapTitle[layer, x, y], false, layer, x, y);

            _mapTitle[layer, x, y] = index;
        }

        public void SetMapAnimationTilte(int layer, int x, int y, int index)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return;
            }

            if (layer < 0 || layer >= _mapLayerInfosByIndex.Count)
            {
                return;
            }

            if (_mapAnimationTitle == null) 
            {
                _mapAnimationTitle = new int[_mapLayerInfosByIndex.Count, _map_Width, _map_Height];
                for (int i = 0; i < _mapLayerInfosByIndex.Count; ++i)
                {
                    for (int m = 0; m < _map_Width; ++m)
                    {
                        for (int n = 0; n < _map_Height; ++n)
                        {
                            _mapAnimationTitle[i, m, n] = -1;
                        }
                    }
                }
            }

            _ActionList[_ActionIndex - 1].AddAction(_mapAnimationTitle[layer, x, y], false, layer, x, y);

            _mapAnimationTitle[layer, x, y] = index;
        }

        public int GetMapTitlePieceIndex(int layer, int x, int y)
        {
            if (x >= Map_Size_Width || y >= Map_Size_Height)
            {
                return -2;
            }

            if (layer < 0 || layer >= _mapLayerInfosByIndex.Count)
            {
                return -2;
            }

            return _mapTitle[layer, x, y];
        }

        public void SetMapTilteBlock(int x, int y, bool isblock)
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

    public class AnimationOffset
    {
        public int _id;

        public int _x = -1;
        public int _y = -1;
        public int _layer = -1;

        public int _offsetX = 0;
        public int _offsetY = 0;

        public AnimationOffset()
        {
            _x = _y = _layer = -1;
        }

        public AnimationOffset(int layer, int x, int y,int id)
        {
            _x = x;
            _y = y;
            _layer = layer;
            _id = id;
        }

        public AnimationOffset(int layer, int x, int y,int offsetX,int offsetY)
        {
            _x = x;
            _y = y;
            _layer = layer;
            _offsetX = offsetX;
            _offsetY = offsetY;
        }
    }

    //动态图
    public class MapUseAnimationInfo
    {
        public List<AnimationImageInfo> animationImageInfos = new List<AnimationImageInfo>();
        public int count = 0;
        public int bitmapsLength = 0;

        public AnimationInfo animationInfo = new AnimationInfo();

        //每次动态图《切块》的宽和高，不是原图第一张的宽和高
        public int w = 0, h = 0;
        //每次动态图《切块》相对于原图的x和y
        public int x = 0, y = 0;
    }

    public class AnimationInfo 
    {
        public List<AnimationImageInfo> images = new List<AnimationImageInfo>();
        public string direction = string.Empty;
        public string name = string.Empty;

        public string xmlPath = string.Empty;//xml的相对路径
    }

    public class AnimationImageInfo 
    {
        public Bitmap bitmap;
        public Bitmap originalBitmap;
        public Bitmap onPanelBitmap;

        public int x;
        public int y;
        public int w;
        public int h;

        public int xClone;
        public int yClone;

        public int originalW;
        public int originalH;

        public int orderIndex;

        public string path;

        public string name;

        public int area;

        public override string ToString()
        {
            return name;
        }
    }

    public class Leaf
    {
        public int w;//宽
        public int h;//高

        public int x;
        public int y;

        public Leaf leftLeaf = null;
        public Leaf rightLeaf = null;

        public Leaf parentLeaf = null;

        public bool isLeftLeaf = true;

        public AnimationImageInfo image;

        public int key;//属于哪一张大图

        public int id;

        public Leaf(int x, int y, int w, int h, Leaf leftLeaf, Leaf rightLeaf,
            Leaf parentLeaf, AnimationImageInfo image, bool isLeftLeaf, int key, int id)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.leftLeaf = leftLeaf;
            this.rightLeaf = rightLeaf;
            this.parentLeaf = parentLeaf;
            this.image = image;
            this.isLeftLeaf = isLeftLeaf;
            this.key = key;
            this.id = id;
        }
    }

    public class BigImageImfo
    {
        public Bitmap _bitmap;
        public Graphics _graphics;
        public Leaf _rootLeaf;

        public BigImageImfo(Bitmap bitmap, Graphics graphics, Leaf rootLeaf)
        {
            _bitmap = bitmap;
            _graphics = graphics;
            _rootLeaf = rootLeaf;
        }
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

        private int _mapTitleWidth = 0;
        public int MapTitleWidth
        {
            get { return _mapTitleWidth; }
            set { _mapTitleWidth = value; }
        }

        private int _mapTitleHeight = 0;
        public int MapTitleHeight
        {
            get { return _mapTitleHeight; }
            set { _mapTitleHeight = value; }
        }

        public class __MapTitleInfosJson
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
        private List<__MapTitleInfosJson> _mapTitleInfosList = new List<__MapTitleInfosJson>();
        public List<__MapTitleInfosJson> MapTitleInfosList
        {
            get { return _mapTitleInfosList; }
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

        public class __MapUsedTitleInfosJson
        {
            private string _name = "";
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private int _titleId = -1;
            public int TitleID
            {
                get { return _titleId; }
                set { _titleId = value; }
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

            private int _titleX = 0;
            public int TitleX
            {
                get { return _titleX; }
                set { _titleX = value; }
            }

            private int _titleY = 0;
            public int TitleY
            {
                get { return _titleY; }
                set { _titleY = value; }
            }

            private int _titleW = 0;
            public int TitleW
            {
                get { return _titleW; }
                set { _titleW = value; }
            }

            private int _titleH = 0;
            public int TitleH
            {
                get { return _titleH; }
                set { _titleH = value; }
            }
        }
        private List<__MapUsedTitleInfosJson> _mapUsedTitleInfosList = new List<__MapUsedTitleInfosJson>();
        public List<__MapUsedTitleInfosJson> MapUsedTitleInfosList
        {
            get { return _mapUsedTitleInfosList; }
        }

        public int[,,] MapTitle = null;
        public int[,] MapTitleExternFlag = null;
        public bool[,] MapBlockFlag = null;
        public int[,,] MapAnimationTitle = null;

        public void InitMapTitel()
        {
            MapTitle = new int[MapLayerInfosList.Count, MapSizeWidth, MapSizeHeight];
            MapBlockFlag = new bool[MapSizeWidth, MapSizeHeight];
            MapTitleExternFlag = new int[MapSizeWidth, MapSizeHeight];
            MapAnimationTitle = new int[MapLayerInfosList.Count, MapSizeWidth, MapSizeHeight];

            for (int m = 0; m < MapLayerInfosList.Count; ++m)
            {
                for (int n = 0; n < MapSizeWidth; ++n)
                {
                    for (int p = 0; p < MapSizeHeight; ++p)
                    {
                        MapAnimationTitle[m, n, p] = -1;
                    }
                }
            }
        }
    }
}
