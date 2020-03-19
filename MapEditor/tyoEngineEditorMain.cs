using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.Xml;

namespace tyoEngineEditor
{
    public partial class tyoEngineEditorMain : Form
    {
        Log log = new Log();

        Process _particleViewApp = null;
        bool _particleViewAppRunning = false;

        private delegate void LoadMapHandle(object o);
        private int countLoadMap = 0;

        //private bool hasAnimation = false;

        //打开地图的线程
        Thread threadLoadMap = null;
        //控制进度条
        Thread threadProgressBar = null;


        public int SystemFPS
        {
            get
            {
                return _systemFPS;
            }
        }

        int _systemFPS = 20;

        public void SetSystemFPS(int fps)
        {
            _systemFPS = fps;

            systemTime.Stop();
            systemTime.Interval = 1000 / _systemFPS;
            systemTime.Start();

        }

        public tyoEngineEditorMain()
        {
            InitializeComponent();
        }

        private void tyoEngineEditorMain_Load(object sender, EventArgs e)
        {
            systemTime.Interval = 1000 / _systemFPS;
            systemTime.Start();

            _particleViewApp = new Process();

        }

        private void systemTime_Tick(object sender, EventArgs e)
        {
            if (_mapeditForm != null && _mapeditForm.Visible == false)
            {
                btShowMap.Enabled = true;
            }
        }

        private void tyoEngineEditorMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_particleViewAppRunning)
            {
                if (!_particleViewApp.HasExited)
                {
                    _particleViewApp.Kill();
                }
            }
            if (threadLoadMap != null)
            {
                try
                {
                    threadLoadMap.Join();
                }
                catch (ThreadAbortException ex)
                {
                    log.log(ex.Message);
                }
            }
        }

        ///////////////////////////////////////////
        // MAP:
        ///////////////////////////////////////////

        MapInfos _nowMapInfos = null;

        private void btMAP_Edit_Click(object sender, EventArgs e)
        {
            if (_nowMapInfos._mapLayerInfosByIndex.Count == 0)
            {
                MessageBox.Show("没有地图图层..");

                return;
            }

            if (_nowMapInfos._mapTileInfosByIndex.Count == 0)
            {
                MessageBox.Show("没有地图图块..");

                return;
            }

            if (_mapeditForm != null)
            {
                _mapeditForm.Dispose();
                _mapeditForm = null;
            }

            _mapeditForm = new tyoEngineTileMapEditWindow();

            _mapeditForm.SetMapInfos(_nowMapInfos);

            _mapeditForm.SetUpdateTimer(this.systemTime.Interval);

            _mapeditForm.ShowDialog();

        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabSystemMenuControl.SelectedIndex = 0;

            if (_nowMapInfos != null)
            {

                MessageBox.Show("对不起，请使用Re New Map来重新构造新的地图.");

                return;
            }

            _nowMapInfos = new MapInfos();

            btMAP_Edit.Enabled = true;
            btMAP_RenewMap.Enabled = true;

            propertyGridMapEditor_MapInfos.SelectedObject = _nowMapInfos;
        }

        private void btMAP_RenewMap_Click(object sender, EventArgs e)
        {
            if (_nowMapInfos != null)
            {
                _nowMapInfos = new MapInfos();
                _nowMapInfos.loadPath = string.Empty;
                propertyGridMapEditor_MapInfos.SelectedObject = _nowMapInfos;
                listBoxMapEditor_TileList.Items.Clear();

                propertyGridMapEditor_MapLayer.SelectedObject = null;
                listBoxMapEditor_MapLayer.Items.Clear();
            }
        }

        private void btMAP_RefurbishMapTiles_Click(object sender, EventArgs e)
        {
            if (_nowMapInfos == null)
            {
                MessageBox.Show("对不起，你还没有新建地图.");

                return;
            }

            if (listBoxMapEditor_TileList.Items.Count > 0)
            {
                DialogResult dr = MessageBox.Show("当前地图块列表含有已经编辑过的元素，是否刷新重置？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }


            string _currentDirectory = System.Windows.Forms.Application.StartupPath;

            _nowMapInfos._mapTileInfosByIndex.Clear();

            listBoxMapEditor_TileList.Items.Clear();

            propertyGridMapEditor_TitleInfos.SelectedObject = null;

            string abyPath = "\\MAPResource\\Tiles";

            DirectoryInfo rootdir = new DirectoryInfo(_currentDirectory + abyPath);

            ReadAllFiles(rootdir.FullName);
        }

        string tempName = string.Empty;
        private void ReadAllFiles(string rootdir)
        {
            DirectoryInfo dir = new DirectoryInfo(rootdir);

            foreach (string item in Directory.GetDirectories(rootdir))
            {
                ReadAllFiles(item);
            }

            foreach (FileInfo dChild in dir.GetFiles("*"))
            {
                string ext = Path.GetExtension(dChild.Name);

                if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                {

                    MapTileInfos tmpMapTitlesInfos = new MapTileInfos();

                    tmpMapTitlesInfos._name = dChild.Name;
                    tmpMapTitlesInfos._filename = dChild.Name;
                    tmpMapTitlesInfos._filepath = rootdir + "\\" + dChild.Name;
                    tmpMapTitlesInfos.dirname = rootdir.ToString()
                        .Substring(rootdir.ToString().LastIndexOf("\\") + 1,
                        rootdir.ToString().Length - rootdir.ToString().LastIndexOf("\\") - 1);

                    string listname = tmpMapTitlesInfos._name;

                    int listindex = listBoxMapEditor_TileList.Items.Add(listname);

                    tmpMapTitlesInfos.index = listindex;

                    _nowMapInfos._mapTileInfosByIndex[listindex] = tmpMapTitlesInfos;
                }
            }

        }

        private void listBoxMapEditor_TileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMapEditor_TileList.SelectedIndex >= 0)
            {
                propertyGridMapEditor_TitleInfos.SelectedObject = _nowMapInfos._mapTileInfosByIndex[listBoxMapEditor_TileList.SelectedIndex];

                pictureBoxMapEditor_TitlePic.Load(_nowMapInfos._mapTileInfosByIndex[listBoxMapEditor_TileList.SelectedIndex].FilePath);
            }
        }

        private void propertyGridMapEditor_TileInfos_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                int nowlistselectindex = listBoxMapEditor_TileList.SelectedIndex;

                listBoxMapEditor_TileList.Items.RemoveAt(nowlistselectindex);

                listBoxMapEditor_TileList.Items.Insert(nowlistselectindex, e.ChangedItem.Value);
            }
        }

        private void btMAP_AddLayer_Click(object sender, EventArgs e)
        {
            if (_nowMapInfos == null)
            {
                MessageBox.Show("对不起，你还没有新建地图.");

                return;
            }

            MapLayerInfos layer = new MapLayerInfos();

            int index = listBoxMapEditor_MapLayer.Items.Add(layer.Name);

            _nowMapInfos._mapLayerInfosByIndex[index] = layer;
        }

        int _nowMapLayerInfosSelect = -1;

        private void listBoxMapLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMapEditor_MapLayer.SelectedIndex >= 0)
            {
                _nowMapLayerInfosSelect = listBoxMapEditor_MapLayer.SelectedIndex;

                propertyGridMapEditor_MapLayer.SelectedObject = _nowMapInfos._mapLayerInfosByIndex[_nowMapLayerInfosSelect];
            }
        }

        List<MapLayerInfos> _MapLayerInfosListTmp = new List<MapLayerInfos>();

        private void btMAP_DelLayer_Click(object sender, EventArgs e)
        {
            if (_nowMapLayerInfosSelect >= 0)
            {
                _nowMapInfos._mapLayerInfosByIndex.Remove(_nowMapLayerInfosSelect);

                listBoxMapEditor_MapLayer.Items.Clear();
                _MapLayerInfosListTmp.Clear();

                foreach (int index in _nowMapInfos._mapLayerInfosByIndex.Keys)
                {
                    _MapLayerInfosListTmp.Add(_nowMapInfos._mapLayerInfosByIndex[index]);
                }

                _nowMapInfos._mapLayerInfosByIndex.Clear();

                for (int i = 0; i < _MapLayerInfosListTmp.Count; ++i)
                {
                    int index = listBoxMapEditor_MapLayer.Items.Add(_MapLayerInfosListTmp[i].Name);

                    _nowMapInfos._mapLayerInfosByIndex[index] = _MapLayerInfosListTmp[i];
                }

                propertyGridMapEditor_MapLayer.SelectedObject = null;
            }
        }

        private void propertyGridMapLayer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                listBoxMapEditor_MapLayer.Items.RemoveAt(_nowMapLayerInfosSelect);

                listBoxMapEditor_MapLayer.Items.Insert(_nowMapLayerInfosSelect, e.ChangedItem.Value);
            }
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabSystemMenuControl.SelectedIndex = 0;

            OpenFileDialog _dlg = new OpenFileDialog();
            _dlg.Filter = "tyo Engine Map Data|*.json";
            _dlg.InitialDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _dlg.ShowDialog();

            String _filePath = _dlg.FileName;

            //打开地图
            threadLoadMap = new Thread(LoadMapThread);
            threadLoadMap.SetApartmentState(ApartmentState.STA);
            threadLoadMap.Start(_filePath);

            //初始化进度条的值
            this.dealingToolProgressBar.Value = 0;
            countLoadMap = 0;

            //处理进度条
            threadProgressBar = new Thread(ProgressBarThread);
            threadProgressBar.Start();
        }

        private void ProgressBarThread()
        {
            SetProgressBarVisible(true);
            int value = 0;
            while (true)
            {
                if (value < countLoadMap)
                {
                    value = countLoadMap;
                    SetProgressBarValue(countLoadMap);
                }
                //if (!hasAnimation && countLoadMap == 7)
                //{ break; }
                if (countLoadMap == Parameters.loadMapCount)
                {
                    break;
                }
            }

            SetProgressBarVisible(false);
        }

        private void SetProgressBarValue(object i)
        {
            if (this.InvokeRequired)
            {
                LoadMapHandle progressBarHandle = new LoadMapHandle(SetProgressBarValue);
                this.Invoke(progressBarHandle, new object[] { i });
            }
            else
            {
                this.dealingToolProgressBar.Value = (int)i;
            }
        }

        private void SetProgressBarVisible(object value)
        {
            if (this.InvokeRequired)
            {
                LoadMapHandle progressBarHandle = new LoadMapHandle(SetProgressBarVisible);
                this.Invoke(progressBarHandle, new object[] { value });
            }
            else
            {
                this.dealingToolLabel.Visible =
                    this.dealingToolProgressBar.Visible = (bool)value;
            }
        }

        private void LoadMapThread(object _filePath)
        {
            if (Path.GetExtension(_filePath.ToString()).ToLower() == ".json")
            {
                _nowMapInfos = new MapInfos();
                ClearItemsHandle(null);

                LoadMap(_filePath.ToString());
                return;
            }

            string s = _filePath.ToString();
            if (_filePath.ToString() == "")
            {
                MessageBox.Show("读取文件出错");
                if (this.threadProgressBar != null)
                {
                    this.countLoadMap = Parameters.loadMapCount;//终止线程
                    //this.threadProgressBar.Join();
                }
            }
        }

        private void ClearItemsHandle(object o)
        {
            if (this.InvokeRequired)
            {
                LoadMapHandle loadMapHandle = new LoadMapHandle(ClearItemsHandle);
                this.Invoke(loadMapHandle, new object[] { null });
            }
            else
            {
                propertyGridMapEditor_MapInfos.SelectedObject = _nowMapInfos;
                propertyGridMapEditor_MapLayer.SelectedObject = null;
                listBoxMapEditor_TileList.Items.Clear();
                listBoxMapEditor_MapLayer.Items.Clear();
            }
        }

        tyoEngineTileMapEditWindow _mapeditForm = null;
        private void LoadMap(string _filePath)
        {
            if (_mapeditForm != null)
            {
                _mapeditForm.Dispose();
                _mapeditForm = null;
            }

            FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
            StreamReader fileReader = new StreamReader(fs);

            MapDataJsonFile _jsonFile = new MapDataJsonFile();

            string _jsonString = fileReader.ReadToEnd();

            _jsonFile = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataJsonFile>(_jsonString);

            //BinaryReader binFile = new BinaryReader(fs);
            _nowMapInfos.loadPath = _filePath;


            LoadMapName(_jsonFile);
            LoadMapTileInfos(_jsonFile, _filePath);
            LoadMapSize(_jsonFile);
            LoadLayerShowFlag(_jsonFile);
            LoadLayerInfos(_jsonFile);
            LoadTileUseInfo(_jsonFile, _filePath);
            LoadMapAllData(_jsonFile);
            LoadMapTileAttributeData(_jsonFile);

            if ( _jsonFile.AnimationUsedInfosList.Count > 0)
            {

            }
            else
            {
                countLoadMap += 4;
            }

//             hasAnimation = binFile.PeekChar() > -1 ? true : false;
//             if (hasAnimation)
//             {
//                 LoadAnimationInfo(binFile, _filePath);
//                 LoadAnimationOffsets(binFile);
// 
//                 //LoadUseAnimationInfo(binFile, _filePath);1749
//                 LoadAnimationPNG(_filePath);
//                 LoadAnimationXML(_filePath);
//             }

            //binFile.Close();
            fileReader.Close();
            fs.Close();

            //propertyGridMapEditor_MapInfos.Refresh();
            RefreshEditor(null);

            _nowMapInfos._IsLoadMap = true;

            _mapeditForm = new tyoEngineTileMapEditWindow();

            _mapeditForm.SetMapInfos(_nowMapInfos);

            _mapeditForm.SetUpdateTimer(this.systemTime.Interval);

            _mapeditForm.ShowDialog();
        }

        private void RefreshEditor(object o)
        {
            if (this.InvokeRequired)
            {
                LoadMapHandle loadMapHandle = new LoadMapHandle(RefreshEditor);
                this.Invoke(loadMapHandle, new object[] { null });
            }
            else
            {
                propertyGridMapEditor_MapInfos.Refresh();
            }
            countLoadMap++;
        }

        private void LoadMapTileAttributeData(MapDataJsonFile _jsonFile)
        {
            foreach(MapDataJsonFile.__MapTileAttribute _attr in _jsonFile.MapTileAttributeList)
            {
                MapTileAttribute _tileAttribute = new MapTileAttribute(_attr._tile_x, _attr._tile_y, _attr._tile_layer);

                _tileAttribute.Value1 = _attr.Value1;
                _tileAttribute.Value1_Type = _attr.Value1_Type;
                _tileAttribute.Value1_Description = _attr.Value1_Description;

                _tileAttribute.Value2 = _attr.Value2;
                _tileAttribute.Value2_Type = _attr.Value2_Type;
                _tileAttribute.Value2_Description = _attr.Value2_Description;

                _tileAttribute.Value3 = _attr.Value3;
                _tileAttribute.Value3_Type = _attr.Value3_Type;
                _tileAttribute.Value3_Description = _attr.Value3_Description;

                _tileAttribute.Value4 = _attr.Value4;
                _tileAttribute.Value4_Type = _attr.Value4_Type;
                _tileAttribute.Value4_Description = _attr.Value4_Description;

                _nowMapInfos._mapTileAttributeList.Add(_tileAttribute);
            }
        }

        private void LoadMapAllData(MapDataJsonFile _jsonFile)
        {
            _nowMapInfos._mapTile = new int[_nowMapInfos._mapLayerInfosByIndex.Count, _nowMapInfos.Map_Size_Width, _nowMapInfos.Map_Size_Height];
            _nowMapInfos._mapExternFlag1 = new int[_nowMapInfos.Map_Size_Width, _nowMapInfos.Map_Size_Height];
            _nowMapInfos._mapBlockFlag = new bool[_nowMapInfos.Map_Size_Width, _nowMapInfos.Map_Size_Height];
            _nowMapInfos._mapAnimationTile = new int[_nowMapInfos._mapLayerInfosByIndex.Count, _nowMapInfos.Map_Size_Width, _nowMapInfos.Map_Size_Height];

            for (int i = 0; i < _nowMapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _nowMapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0; y < _nowMapInfos.Map_Size_Height; ++y)
                    {
                        _nowMapInfos._mapTile[i, x, y] = _jsonFile.MapTile[i, x, y];

                        if (i == 0)
                        {
                            _nowMapInfos._mapExternFlag1[x, y] = _jsonFile.MapTile[i, x, y];
                            _nowMapInfos._mapBlockFlag[x, y] = _jsonFile.MapBlockFlag[x, y];
                        }

                        _nowMapInfos._mapAnimationTile[i, x, y] = _jsonFile.MapAnimationTile[i, x, y];
                    }
                }
            }
            countLoadMap++;
        }

        Dictionary<string, Bitmap> _CacheMapTile = new Dictionary<string, Bitmap>();

        private List<Thread> _LoadThradList = new List<Thread>();
        private static object _LoadTileUseInfoChildObj = new object();

        private void LoadTileUseInfoChild(int _start, int _count)
        {
            lock (_LoadTileUseInfoChildObj)
            {
                for (int i = _start; i < (_start + _count); ++i)
                {

                    if (i >= _nowMapInfos._mapTileUseInfo.Count)
                    {
                        break;
                    }

                    if (_nowMapInfos._mapTileUseInfo[i]._image._tile != null)
                    {
                        continue;
                    }

                    int _comboxIndex = _nowMapInfos._mapTileUseInfo[i]._comboIndex;
                    Bitmap _tBmp = _CacheMapTile[_nowMapInfos._mapTileInfosByIndex[_comboxIndex]._filepath];

                    _nowMapInfos._mapTileUseInfo[i]._image._tile = _tBmp.Clone(new Rectangle(
                            _nowMapInfos._mapTileUseInfo[i]._image._x,
                            _nowMapInfos._mapTileUseInfo[i]._image._y,
                            _nowMapInfos._mapTileUseInfo[i]._image._w,
                            _nowMapInfos._mapTileUseInfo[i]._image._h)
                            , _tBmp.PixelFormat);


                    //Monitor.Exit(this);
                }

                LoadTileUseInfoChild_Over(_start, new EventArgs());//引发完成事件
            }
        }

        int _OverCount = 0;
        private void LoadTileUseInfoChild_Over(object sender, EventArgs e)
        {

            _OverCount++;

            if (_LoadThradList.Count == _OverCount)
            {

                for (int i = 0; i < _LoadThradList.Count; ++i)
                {
                    //if (_LoadThradList[i].Name == ("LoadTitleUseInfoChild_" + ((int)sender).ToString()) )
                    {
                        //_LoadThradList[i].Abort();
                        _LoadThradList[i] = null;
                        //_LoadThradList.RemoveAt(i);
                    }
                }

                _LoadThradList.Clear();
                //_LoadOver = true;
            }

        }

        //bool _LoadOver = false;

        private void LoadTileUseInfo(MapDataJsonFile _jsonFile, string path)
        {
            //_LoadOver = false;

            //int _count = binFile.ReadInt32();

            _CacheMapTile.Clear();
            
            for (int i = 0; i < _jsonFile.MapUsedTileInfosList.Count; ++i)
            {
                string _name = _jsonFile.MapUsedTileInfosList[i].Name;
                int _id = _jsonFile.MapUsedTileInfosList[i].TileID;
                int _comboxIndex = _jsonFile.MapUsedTileInfosList[i].ComboxIndex;
                bool _flag = _jsonFile.MapUsedTileInfosList[i].UsedFlag;

                MapUseTileInfo _tmp = new MapUseTileInfo();

                _tmp._name = _name;
                _tmp._id = _id;
                _tmp._comboIndex = _comboxIndex;
                _tmp._flag = _flag;

                _tmp._image = new tyoEngineTileMapEditWindow.MapTilePiece();
                _tmp._image._x = _jsonFile.MapUsedTileInfosList[i].TileX;
                _tmp._image._y = _jsonFile.MapUsedTileInfosList[i].TileY;
                _tmp._image._w = _jsonFile.MapUsedTileInfosList[i].TileW;
                _tmp._image._h = _jsonFile.MapUsedTileInfosList[i].TileH;

                //optimize to read use title bmp..


                Bitmap _tBmp = null;

                if (!_name.Equals(Parameters.SYSYTEMUSE))
                {
                    if (_comboxIndex >= _nowMapInfos._mapTileInfosByIndex.Count || _comboxIndex < 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (_CacheMapTile.ContainsKey(_nowMapInfos._mapTileInfosByIndex[_comboxIndex]._filepath))// chaeck is hved?
                        {
                            _tBmp = _CacheMapTile[_nowMapInfos._mapTileInfosByIndex[_comboxIndex]._filepath];
                        }
                        else
                        {
                            _tBmp = (Bitmap)Image.FromFile(_nowMapInfos._mapTileInfosByIndex[_comboxIndex]._filepath);
                            _CacheMapTile[_nowMapInfos._mapTileInfosByIndex[_comboxIndex]._filepath] = _tBmp;
                        }
                    }
                   
                }
                else
                {
                    if (_CacheMapTile.ContainsKey(_id.ToString()))// check is haved?
                    {
                        _tBmp = _CacheMapTile[_id.ToString()];
                    }
                }

                _tmp._image._tile = new Bitmap(_tmp._image._w, _tmp._image._h);
                Graphics imgGraphics = Graphics.FromImage(_tmp._image._tile);
                Rectangle destRect = new Rectangle(new Point(0, 0), new Size(_tmp._image._w, _tmp._image._h));
                imgGraphics.DrawImage(_tBmp, destRect, new Rectangle(_tmp._image._x, _tmp._image._y, _tmp._image._w, _tmp._image._h), GraphicsUnit.Pixel);
                // _tBmp.Clone(new Rectangle(_tmp._image._x, _tmp._image._y, _tmp._image._w, _tmp._image._h), _tBmp.PixelFormat);

                _nowMapInfos._mapTileUseInfo.Add(_tmp);

                //String _imagePath = System.Text.Encoding.Default.GetString(binFile.ReadBytes(binFile.ReadInt32()));

                //                 String _filepath = Path.GetDirectoryName(path);
                //                 _filepath = _filepath + "\\" + _imagePath;
            }

            //             int _stepCount = 100;
            //             int _threadCount = _nowMapInfos._mapTitleUseInfo.Count / _stepCount + 1;
            // 
            //             for (int i = 0; i < _threadCount; ++i)
            //             {
            //                 Thread _tmp = new Thread( () => LoadTitleUseInfoChild(i * _stepCount , _stepCount) );
            //                 _tmp.Name = "LoadTitleUseInfoChild_" + (i * _stepCount).ToString();
            //                 _tmp.Start();
            //                 _LoadThradList.Add(_tmp);
            //             }
            // 
            //             while (true)
            //             {
            //                 if (_LoadOver)
            //                 {
            //                     break;
            //                 }
            // 
            //                 Thread.Sleep(10);
            //             }

            _CacheMapTile.Clear();
            countLoadMap++;
        }

        private void LoadLayerInfos(MapDataJsonFile _jsonFile)
        {
            //int _count = binFile.ReadInt32();

            for (int i = 0; i < _jsonFile.MapLayerInfosList.Count; ++i)
            {
                int _index = _jsonFile.MapLayerInfosList[i].Index;

                string _name = _jsonFile.MapLayerInfosList[i].Name;

                int _depth = _jsonFile.MapLayerInfosList[i].Depth;

                MapLayerInfos layer = new MapLayerInfos();
                layer.Name = _name;
                layer.Depth = _depth;

                InsertMapLayerHandle(new object[] { _index, _name });
                //listBoxMapEditor_MapLayer.Items.Insert(_index, _name);

                _nowMapInfos._mapLayerInfosByIndex[_index] = layer;
            }
            countLoadMap++;
        }

        private void InsertMapLayerHandle(object obj)
        {
            if (this.InvokeRequired)
            {
                LoadMapHandle loadMapHandle = new LoadMapHandle(InsertMapLayerHandle);
                this.Invoke(loadMapHandle, new object[] { obj });
            }
            else
            {
                listBoxMapEditor_MapLayer.Items.Insert((int)((object[])obj)[0], ((object[])obj)[1].ToString());
            }
        }

        private void LoadLayerShowFlag(MapDataJsonFile _jsonFile)
        {
            _nowMapInfos._LayerShowFlag = new bool[_jsonFile.MapLayerShowFlagList.Count];

            for (int i = 0; i < _jsonFile.MapLayerShowFlagList.Count; ++i)
            {
                _nowMapInfos._LayerShowFlag[i] = _jsonFile.MapLayerShowFlagList[i];
            }
            countLoadMap++;
        }

        private void LoadMapSize(MapDataJsonFile _jsonFile)
        {
            _nowMapInfos.Map_Size_Width = _jsonFile.MapSizeWidth;
            _nowMapInfos.Map_Size_Height = _jsonFile.MapSizeHeight;
            _nowMapInfos.Map_Tile_Width = _jsonFile.MapTileWidth;
            _nowMapInfos.Map_Tile_Height = _jsonFile.MapTileHeight;

            countLoadMap++;
        }

        private void LoadMapName(MapDataJsonFile _jsonFile)
        {
            _nowMapInfos.Name = _jsonFile.MapName;
            countLoadMap++;
        }



        private void LoadMapTileInfos(MapDataJsonFile _jsonFile, string path)
        {
            //int _count = binFile.ReadInt32();

            for (int i = 0; i < _jsonFile.MapTileInfosList.Count; ++i)
            {
                int _index = _jsonFile.MapTileInfosList[i].Index;
                string _name = _jsonFile.MapTileInfosList[i].Name;
                string _filename = _jsonFile.MapTileInfosList[i].Directory;

                string _filepath = Path.GetDirectoryName(path);
                _filepath = _filepath + "\\" + _filename;

                MapTileInfos tmpMapTilesInfos = new MapTileInfos();

                tmpMapTilesInfos._name = _name;
                tmpMapTilesInfos._filename = _filename;
                tmpMapTilesInfos._filepath = _filepath;
                if (_filename.LastIndexOf("\\") < 0)
                {
                    tmpMapTilesInfos.dirname = "Tiles";
                }
                else
                {
                    tmpMapTilesInfos.dirname = _filename.Substring(0, _filename.LastIndexOf("\\"));
                }

                string listname = tmpMapTilesInfos._name;

                InsertTileListHandle(new object[] { _index, listname });

                tmpMapTilesInfos.index = _index;
                _nowMapInfos._mapTileInfosByIndex[_index] = tmpMapTilesInfos;
            }
            countLoadMap++;
        }

        private void InsertTileListHandle(object obj)
        {
            //int _index = (int)((object[])obj)[0];
            //string listname = ((object[])obj)[1].ToString();
            if (this.InvokeRequired)
            {
                LoadMapHandle loadMapHandle = new LoadMapHandle(InsertTileListHandle);
                this.Invoke(loadMapHandle, new object[] { obj });
            }
            else
            {
                listBoxMapEditor_TileList.Items.Insert((int)((object[])obj)[0], ((object[])obj)[1].ToString());
            }
        }


        private void btShowMap_Click(object sender, EventArgs e)
        {
            if (_mapeditForm != null)
            {
                btShowMap.Enabled = false;
                _mapeditForm.Show();
            }
        }

        int frameID = 0;
        List<tyoAnimationFrameInfo> aniFrameList = new List<tyoAnimationFrameInfo>();

        private void btAni_AddAniTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dlg = new OpenFileDialog();
            _dlg.Filter = "图片文件(*.jpg,*.tga,*.bmp,*.png)|*.jpg;*.tga;*.bmp;*.png";
            _dlg.InitialDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _dlg.ShowDialog();

            string _filePath = _dlg.FileName;

            if (_filePath.Length > 0)
            {
                tyoAnimationFrameInfo _info = new tyoAnimationFrameInfo(_filePath);
                _info.FrameID = frameID;

                //aniFrameList.Add(_info);

                frameID++;

                int _listIdx = aniListBox_FrameList.Items.Add(frameID);
                aniFrameList.Add(_info);
            }
        }

        private void aniListBox_FrameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aniListBox_FrameList.SelectedIndex >= 0)
            {
                aniPropertyGrid_Infos.SelectedObject = aniFrameList[aniListBox_FrameList.SelectedIndex];

                aniPictureBox_frameTexture.Image = aniFrameList[aniListBox_FrameList.SelectedIndex].FrameImage;

                if (aniPictureBox_frameTexture.Image.Width > 128 || aniPictureBox_frameTexture.Image.Height > 128)
                {
                    aniPictureBox_frameTexture.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    aniPictureBox_frameTexture.SizeMode = PictureBoxSizeMode.Normal;
                }
            }
        }

        private void aniPropertyGrid_Infos_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if( e.ChangedItem.PropertyDescriptor.Description == "帧图像" )
            {
                aniPictureBox_frameTexture.Refresh();
            }
        }

        bool isPlayAnimation = false;
        private void btAni_PlayAnimation_Click(object sender, EventArgs e)
        {
            if(isPlayAnimation)
            {
                btAni_PlayAnimation.Text = "播放动画";
                aniTimer_AnimationPlayDt.Stop();

                aniPictureBox_AniShow.Image = null;

                isPlayAnimation = false;
                return;
            }
            
            btAni_PlayAnimation.Text = "停止播放";

            int _fps = int.Parse(aniTextBox_FPS.Text);

            if(_fps < 1 || _fps > 100)
            {
                _fps = 60;
            }

            aniTimer_AnimationPlayDt.Interval = 1000 / _fps;
            aniTimer_AnimationPlayDt.Start();

            isPlayAnimation = true;
        }

        int currentAnimationFrameIndex = -1;
        private void aniTimer_AnimationPlayDt_Tick(object sender, EventArgs e)
        {
            currentAnimationFrameIndex++;

            if(currentAnimationFrameIndex >= aniListBox_FrameList.Items.Count)
            {
                currentAnimationFrameIndex = 0;
            }

            aniPictureBox_AniShow.Image = aniFrameList[currentAnimationFrameIndex].FrameImage;

            if (aniPictureBox_AniShow.Image.Width > 512 || aniPictureBox_AniShow.Image.Height > 512)
            {
                aniPictureBox_AniShow.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                aniPictureBox_AniShow.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void btAni_DelAniTexture_Click(object sender, EventArgs e)
        {
            if(aniListBox_FrameList.SelectedIndex >= 0 )
            {
                aniFrameList.RemoveAt(aniListBox_FrameList.SelectedIndex);
            }

            aniListBox_FrameList.Items.Clear();

            foreach(tyoAnimationFrameInfo _info in aniFrameList)
            {
                aniListBox_FrameList.Items.Add(_info.FrameID.ToString());
            }
        }

        private void btAni_SortAnimationFrameList_Click(object sender, EventArgs e)
        {
            aniFrameList.Sort((_v1, _v2) => { return _v1.FrameID.CompareTo(_v2.FrameID); });

            aniListBox_FrameList.Items.Clear();

            foreach (tyoAnimationFrameInfo _info in aniFrameList)
            {
                aniListBox_FrameList.Items.Add(_info.FrameID.ToString());
            }
        }

        private void ClearAnimationEditor()
        {
            aniFrameList.Clear();
            aniPropertyGrid_Infos.SelectedObject = null;

            aniListBox_FrameList.Items.Clear();

            aniPictureBox_frameTexture.Image = null;
            aniPictureBox_AniShow.Image = null;

            aniTextBox_FPS.Text = "60";
            aniTextBox_Name.Text = "";

            aniTimer_AnimationPlayDt.Stop();

            btAni_PlayAnimation.Text = "播放动画";

            currentAnimationFrameIndex = -1;
            frameID = 0;
        }

        private void btAni_ClearAnimation_Click(object sender, EventArgs e)
        {
            ClearAnimationEditor();
        }

        private void btAni_SaveAnimation_Click(object sender, EventArgs e)
        {
            if (aniTextBox_Name.Text.Length == 0 )
            {
                MessageBox.Show("请输入动画名字");
                return;
            }


            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "tyo Engine Animation Data|*.json";
            saveDlg.ShowDialog();

            string _filePath = saveDlg.FileName;

            if (Path.GetExtension(_filePath).ToLower() == ".json")
            {
                if (Directory.Exists(_filePath))
                {
                    if(MessageBox.Show("有相同名字的动画文件，是否替换？（素材也会被替换）", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    Directory.Delete(_filePath);
                }

                tyoAnimationJsonFile _jsonFile = new tyoAnimationJsonFile();

                _jsonFile.AnimationName = aniTextBox_Name.Text;
                _jsonFile.AnimationFPS = int.Parse(aniTextBox_FPS.Text);

                string _texDict = Path.GetDirectoryName(_filePath);

                _texDict += "\\Textures\\";

                if(!Directory.Exists(_texDict))
                {
                    Directory.CreateDirectory(_texDict);
                }

                foreach (tyoAnimationFrameInfo _info in aniFrameList)
                {
                    string _texFileName = string.Format("{0}_{1}", _jsonFile.AnimationName, _info.FrameID);

                    string _texFilePath = _texDict + _texFileName + ".png";

                    if (Directory.Exists(_texFilePath))
                    {
                        Directory.Delete(_texFilePath);
                    }

                    _info.FrameImage.Save(_texFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    _jsonFile.AnimationNameList.Add(_texFileName);
                }

                string _jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_jsonFile);
                //MapDataJsonFile _t2 = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataJsonFile>(_jsonString);

                //_pPath = _pDir + Path.GetFileName(_path.Replace(".tmd",".json"));

                FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
                StreamWriter _writer = new StreamWriter(fs);
                _writer.Write(_jsonString);
                _writer.Close();
                fs.Close();

                return;
            }

            if (_filePath != "")
            {
                MessageBox.Show("保存文件出错");
            }
        }

        private void btAni_LoadAnimation_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dlg = new OpenFileDialog();
            _dlg.Filter = "tyo Engine Animation Data|*.json";
            _dlg.InitialDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _dlg.ShowDialog();

            string _filePath = _dlg.FileName;

            if (_filePath.Length > 0)
            {
                FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
                StreamReader fileReader = new StreamReader(fs);

                tyoAnimationJsonFile _jsonFile = new tyoAnimationJsonFile();

                string _jsonString = fileReader.ReadToEnd();

                _jsonFile = Newtonsoft.Json.JsonConvert.DeserializeObject<tyoAnimationJsonFile>(_jsonString);

                fileReader.Close();
                fs.Close();
                ClearAnimationEditor();

                aniTextBox_FPS.Text = _jsonFile.AnimationFPS.ToString();
                aniTextBox_Name.Text = _jsonFile.AnimationName;

                string _texDict = Path.GetDirectoryName(_filePath);
                _texDict += "\\Textures\\";

                foreach(string _texName in _jsonFile.AnimationNameList)
                {
                    string _texFilePath = _texDict + _texName + ".png";

                    tyoAnimationFrameInfo _info = new tyoAnimationFrameInfo(_texFilePath);
                    _info.FrameID = frameID;

                    frameID++;

                    int _listIdx = aniListBox_FrameList.Items.Add(frameID);
                    aniFrameList.Add(_info);
                }

            }
        }
    }

}
