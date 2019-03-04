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

        private Dictionary<string, AnimationInfo> animationInfos = null;
        private string animationVersion = string.Empty;

        //private bool hasAnimation = false;

        //打开地图的线程
        Thread threadLoadMap = null;
        //控制进度条
        Thread threadProgressBar = null;

        private int dgvAnimation_mouseClick = -1;

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

            if ( _jsonFile.AnimationUsedInfosList.Count > 0)
            {
                LoadAnimationUsedInfos(_jsonFile, _filePath);
                LoadAnimationOffsets(_jsonFile);

                LoadAnimationPNG(_filePath);
                LoadAnimationXML(_filePath);
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

        private void LoadAnimationXML(string path) 
        {
            DirectoryInfo dir = Directory.GetParent(path);
            foreach (FileInfo item in dir.GetFiles())
            {
                if (item.FullName.ToUpper().EndsWith("XML")) 
                {
                    _nowMapInfos.animationXML.Add(item.FullName);
                }
            }
            countLoadMap++;
        }

        private void LoadAnimationUsedInfos(MapDataJsonFile _jsonFile, string path)
        {
            MapUseAnimationInfo mapUseAnimationInfo;

            DirectoryInfo dir = Directory.GetParent(path);

            for (int i = 0; i < _jsonFile.AnimationUsedInfosList.Count; ++i)
            {
                mapUseAnimationInfo = new MapUseAnimationInfo();
                _nowMapInfos._mapAnimationUseInfo.Add(_jsonFile.AnimationUsedInfosList[i].Index, mapUseAnimationInfo);
                mapUseAnimationInfo.animationInfo.xmlPath = _jsonFile.AnimationUsedInfosList[i].FilePath;
                mapUseAnimationInfo.animationInfo.name = _jsonFile.AnimationUsedInfosList[i].NodeName;
                mapUseAnimationInfo.animationInfo.direction = _jsonFile.AnimationUsedInfosList[i].Direction;

                AnimationInfo animationInfo = Animation.GetSpeicalAnimation(dir.FullName + mapUseAnimationInfo.animationInfo.xmlPath,mapUseAnimationInfo.animationInfo.name, mapUseAnimationInfo.animationInfo.direction);

                LoadUseAnimationInfo(dir, mapUseAnimationInfo, animationInfo.images);

                mapUseAnimationInfo.w = animationInfo.images[0].originalW;
                mapUseAnimationInfo.h = animationInfo.images[0].originalH;
                mapUseAnimationInfo.bitmapsLength = animationInfo.images.Count;
            }

            countLoadMap++;
        }

        private void LoadAnimationOffsets(MapDataJsonFile _jsonFile) 
        {
            for (int i = 0; i < _jsonFile.AnimationOffsetList.Count; i++) 
            {
                AnimationOffset animationOffset = new AnimationOffset();

                animationOffset._id = _jsonFile.AnimationOffsetList[i].Index;

                animationOffset._x = _jsonFile.AnimationOffsetList[i].X;
                animationOffset._y = _jsonFile.AnimationOffsetList[i].Y;
                animationOffset._layer = _jsonFile.AnimationOffsetList[i].Layer;
                animationOffset._offsetX = _jsonFile.AnimationOffsetList[i].OffsetX;
                animationOffset._offsetY = _jsonFile.AnimationOffsetList[i].OffsetY;

                _nowMapInfos._mapAnimationOffsets.Add(animationOffset._id, animationOffset);
            }

            countLoadMap++;
        }

        private void LoadAnimationInfo(BinaryReader binFile, string path) 
        {
            int _count = binFile.ReadInt32();
            MapUseAnimationInfo mapUseAnimationInfo;

            DirectoryInfo dir = Directory.GetParent(path);

            for (int i = 0; i < _count; ++i) 
            {
                mapUseAnimationInfo = new MapUseAnimationInfo();
                _nowMapInfos._mapAnimationUseInfo.Add(binFile.ReadInt32(), mapUseAnimationInfo);
                mapUseAnimationInfo.animationInfo.xmlPath = System.Text.Encoding.Default.GetString(binFile.ReadBytes(binFile.ReadInt32()));
                mapUseAnimationInfo.animationInfo.name = System.Text.Encoding.Default.GetString(binFile.ReadBytes(binFile.ReadInt32()));
                mapUseAnimationInfo.animationInfo.direction = System.Text.Encoding.Default.GetString(binFile.ReadBytes(binFile.ReadInt32()));

                AnimationInfo animationInfo = Animation.GetSpeicalAnimation(dir.FullName + mapUseAnimationInfo.animationInfo.xmlPath, 
                    mapUseAnimationInfo.animationInfo.name, mapUseAnimationInfo.animationInfo.direction);

                LoadUseAnimationInfo(dir, mapUseAnimationInfo, animationInfo.images);


                mapUseAnimationInfo.w = animationInfo.images[0].originalW;
                mapUseAnimationInfo.h = animationInfo.images[0].originalH;
                mapUseAnimationInfo.bitmapsLength = animationInfo.images.Count;
                
            }

            countLoadMap++;
        }

        private void LoadUseAnimationInfo(DirectoryInfo d, MapUseAnimationInfo mapUseAnimationInfo, List<AnimationImageInfo> animationImageInfos)
        {
            for (int j = 0; j < animationImageInfos.Count; j++)
            {
                AnimationImageInfo animationImageInfo = new AnimationImageInfo();
                //每一帧的宽
                animationImageInfo.w = animationImageInfos[j].w;

                //每一帧的高
                animationImageInfo.h = animationImageInfos[j].h;

                //帧在大图中的X坐标
                animationImageInfo.xClone = animationImageInfos[j].xClone;

                //帧在大图中的Y坐标
                animationImageInfo.yClone = animationImageInfos[j].yClone;

                //原图的宽
                animationImageInfo.originalW = animationImageInfos[j].originalW;

                //原图的高
                animationImageInfo.originalH = animationImageInfos[j].originalH;

                //原图的X坐标
                animationImageInfo.x = animationImageInfos[j].x;

                //原图的Y坐标
                animationImageInfo.y = animationImageInfos[j].y;

                //帧图名称
                animationImageInfo.path = animationImageInfos[j].path;

                //
                animationImageInfo.onPanelBitmap = animationImageInfos[j].onPanelBitmap;
                animationImageInfo.bitmap = animationImageInfos[j].bitmap;
                animationImageInfo.originalBitmap = animationImageInfos[j].originalBitmap;


                //切割后的图片
                //Bitmap tmpBitmap = null;
                //Bitmap bitmap = new Bitmap(animationImageInfo.w, animationImageInfo.h);
                //Graphics graphics = Graphics.FromImage(bitmap);

                //using (tmpBitmap = (Bitmap)Image.FromFile(d.FullName + animationImageInfo.path))
                //{
                //    graphics.DrawImage(tmpBitmap,
                //    new Rectangle(0, 0, animationImageInfo.w, animationImageInfo.h),
                //    new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone,
                //        animationImageInfo.w, animationImageInfo.h), GraphicsUnit.Pixel);

                //    animationImageInfo.bitmap = bitmap;
                //}

                //Bitmap bitmapOriginal = new Bitmap(animationImageInfo.originalW, animationImageInfo.originalH);
                //Graphics graphicsOriginal = Graphics.FromImage(bitmapOriginal);

                ////原图
                //using (tmpBitmap = (Bitmap)Image.FromFile(d.FullName + "\\" + animationImageInfo.path))
                //{
                //    graphicsOriginal.DrawImage(tmpBitmap,
                //    new Rectangle(animationImageInfo.x, animationImageInfo.y,
                //        animationImageInfo.w, animationImageInfo.h),
                //    new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone,
                //        animationImageInfo.w, animationImageInfo.h), GraphicsUnit.Pixel);

                //    animationImageInfo.originalBitmap = bitmapOriginal;
                //}

                mapUseAnimationInfo.animationImageInfos.Add(animationImageInfo);
            }
        }

        private void LoadAnimationArray(BinaryReader binFile)
        {
            _nowMapInfos._mapAnimationTile = new int[_nowMapInfos._mapLayerInfosByIndex.Count, _nowMapInfos.Map_Size_Width, _nowMapInfos.Map_Size_Height];

            for (int i = 0; i < _nowMapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _nowMapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0; y < _nowMapInfos.Map_Size_Height; ++y)
                    {
                        _nowMapInfos._mapAnimationTile[i, x, y] = binFile.ReadInt32();
                    }
                }
            }
            countLoadMap++;
        }

        private void LoadUseAnimationInfo(BinaryReader binFile, string path)
        {
            int _count = binFile.ReadInt32();
            DirectoryInfo d = Directory.GetParent(path);
            for (int i = 0; i < _count; ++i)
            {
                int _count2 = binFile.ReadInt32();

                MapUseAnimationInfo useAnimationInfo = new MapUseAnimationInfo();

                useAnimationInfo.bitmapsLength = _count2;
                useAnimationInfo.w = binFile.ReadInt32();
                useAnimationInfo.h = binFile.ReadInt32();

                for (int j = 0; j < _count2; j++)
                {
                    AnimationImageInfo animationImageInfo = new AnimationImageInfo();
                    //每一帧的宽
                    animationImageInfo.w = binFile.ReadInt32();

                    //每一帧的高
                    animationImageInfo.h = binFile.ReadInt32();

                    //帧在大图中的X坐标
                    animationImageInfo.xClone = binFile.ReadInt32();

                    //帧在大图中的Y坐标
                    animationImageInfo.yClone = binFile.ReadInt32();

                    //原图的宽
                    animationImageInfo.originalW = binFile.ReadInt32();

                    //原图的高
                    animationImageInfo.originalH = binFile.ReadInt32();

                    //原图的X坐标
                    animationImageInfo.x = binFile.ReadInt32();

                    //原图的Y坐标
                    animationImageInfo.y = binFile.ReadInt32();

                    //帧图名称
                    animationImageInfo.path = System.Text.Encoding.Default.GetString(binFile.ReadBytes(binFile.ReadInt32()));

                    //切割后的图片
                    Bitmap tmpBitmap = null;
                    Bitmap bitmap = new Bitmap(animationImageInfo.w, animationImageInfo.h);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    
                    using (tmpBitmap = (Bitmap)Image.FromFile(d.FullName + animationImageInfo.path)) 
                    {
                        graphics.DrawImage(tmpBitmap,
                        new Rectangle(0, 0, animationImageInfo.w, animationImageInfo.h),
                        new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone,
                            animationImageInfo.w, animationImageInfo.h), GraphicsUnit.Pixel);

                        animationImageInfo.bitmap = bitmap;
                    }

                    Bitmap bitmapOriginal = new Bitmap(animationImageInfo.originalW, animationImageInfo.originalH);
                    Graphics graphicsOriginal = Graphics.FromImage(bitmapOriginal);
                    
                    //原图
                    using (tmpBitmap = (Bitmap)Image.FromFile(d.FullName + "\\" + animationImageInfo.path)) 
                    {
                        graphicsOriginal.DrawImage(tmpBitmap,
                        new Rectangle(animationImageInfo.x, animationImageInfo.y,
                            animationImageInfo.w, animationImageInfo.h),
                        new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone,
                            animationImageInfo.w, animationImageInfo.h), GraphicsUnit.Pixel);

                        animationImageInfo.originalBitmap = bitmapOriginal;
                    } 
                    
                    useAnimationInfo.animationImageInfos.Add(animationImageInfo);
                }
                _nowMapInfos._mapAnimationUseInfo.Add(binFile.ReadInt32(), useAnimationInfo);
            }
            countLoadMap++;
        }

        private void LoadAnimationPNG(string _path)
        {
            string name = _path.Substring(0,_path.LastIndexOf('\\'));
            DirectoryInfo d = new DirectoryInfo(name + Parameters.PICFILE);
            if (!Directory.Exists(d.FullName)) 
            {
                Directory.CreateDirectory(d.FullName);
            }
            foreach (FileInfo item in d.GetFiles())
            {
                _nowMapInfos.animationPNGpath.Add(item.FullName);
            }
            countLoadMap++;
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
#warning optimize to read use title bmp.

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

                String _name = _jsonFile.MapLayerInfosList[i].Name;

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

        #region Animation

        private void CloneBitmapALL(string[] paths, string rowid,string name)
        {
            int orderIndex = 0;
            Bitmap image1;
            Rectangle rect;

            int wStart = 0;
            int wEnd = 0;

            int hStart = 0;
            int hEnd = 0;

            int w = 0, h = 0;

            AnimationInfo animation = new AnimationInfo();
            AnimationImageInfo aii;

            int index = 1;
            int directionIndex = 1;
            int pageCount = paths.Length / 8;

            foreach (string path in paths)
            {
                //DateTime st = DateTime.Now;
                image1 = (Bitmap)Image.FromFile(path);

                wStart = image1.Width;
                wEnd = 0;

                hStart = image1.Height;
                hEnd = 0;

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);

                        if (pixelColor.A > 0)
                        {
                            if (wStart > x)
                            {
                                wStart = x;
                            }
                            if (wEnd < x)
                            {
                                wEnd = x;
                            }

                            if (hStart > y)
                            {
                                hStart = y;
                            }
                            if (hEnd < y)
                            {
                                hEnd = y;
                            }
                        }
                    }
                }

                w = wEnd - wStart;
                h = hEnd - hStart;

                //DateTime et = DateTime.Now;
                //TimeSpan ts = et - st;
                //Console.WriteLine(ts.ToString());

                //st = DateTime.Now;
                rect = new Rectangle(wStart, hStart, w, h);

                Bitmap cloneBit = new Bitmap(w, h);
                Graphics imgGraphics = Graphics.FromImage(cloneBit);
                imgGraphics.DrawImage(image1, new Rectangle(0, 0, w, h), rect, GraphicsUnit.Pixel);

                //Bitmap cloneBit = image1.Clone(rect, format);

                aii = new AnimationImageInfo();
                aii.bitmap = cloneBit;
                aii.originalBitmap = image1;
                aii.x = wStart;
                aii.y = hStart;
                aii.w = w;
                aii.h = h;
                aii.originalW = image1.Width;
                aii.originalH = image1.Height;
                aii.orderIndex = orderIndex;
                aii.name = path.Substring(path.LastIndexOf('\\') + 1);
                aii.path = path;
                aii.area = w * h;

                //et = DateTime.Now;
                //ts = et - st;
                //Console.WriteLine(ts.ToString());
                orderIndex = orderIndex + 1;
                animation.images.Add(aii);

                if (index % pageCount == 0)
                {
                    string direction = string.Empty;
                    switch (directionIndex)
                    {
                        case 1:
                            direction = "up";
                            break;

                        case 2:
                            direction = "rightUp";
                            break;
                        case 3:
                            direction = "right";
                            break;
                        case 4:
                            direction = "rightDown";
                            break;
                        case 5:
                            direction = "down";
                            break;
                        case 6:
                            direction = "leftDown";
                            break;
                        case 7:
                            direction = "left";
                            break;
                        case 8:
                            direction = "leftUp";
                            break;
                        default:
                            break;
                    }

                    animation.direction = direction;
                    animation.name = name;

                    animationInfos.Add(rowid + Animation.TransDirectionEN2CN(direction), animation);
                    animation = new AnimationInfo();
                    directionIndex++;
                    orderIndex = 0;
                }

                index++;
            }

        }

        private void btNewAnimation_Click(object sender, EventArgs e)
        {
            if (txtAnimation.Text.Equals(string.Empty))
            {
                MessageBox.Show("请先输入动画名字！");
                return;
            }
            this.btPicAdd.Enabled = this.btPicDel.Enabled = true;
            animationInfos = new Dictionary<string, AnimationInfo>();
        }


        private void btPicAdd_Click(object sender, EventArgs e)
        {
            if (dgvAnimation.Rows.Count > 0
                && dgvAnimation.Rows[dgvAnimation.Rows.Count - 1].Cells[0].Value == null)
            {
                MessageBox.Show("请先输入动态图名称！");
                return;
            }
            int i = this.dgvAnimation.Rows.Add();
            dgvAnimation.Rows[i].Cells[3].Value = i;

        }

        private void dgvAnimation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAnimation_mouseClick = e.RowIndex;
            if (e.ColumnIndex == 2)
            {
                //if (dgvAnimation.Rows[e.RowIndex].Cells[1].Value == null)
                //{
                //    //MessageBox.Show("请先选择图片方向！");
                //    CloneBitmapALL(string[] paths, string rowid,string name);
                //    return;
                //}
                if (dgvAnimation.Rows[e.RowIndex].Cells[1].Value != null) 
                {
                    string key = dgvAnimation.Rows[e.RowIndex].Cells[3].Value.ToString()
                        + dgvAnimation.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (animationInfos != null && animationInfos.ContainsKey(key))
                    {
                        if (MessageBox.Show("已经上传了动态图，是否覆盖？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            animationInfos.Remove(key);
                        }
                    }
                }

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    if (dgvAnimation.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        CloneBitmapALL(ofd.FileNames, dgvAnimation.Rows[e.RowIndex].Cells[3].Value.ToString(),
                            dgvAnimation.Rows[e.RowIndex].Cells[0].Value.ToString());
                        MessageBox.Show("完成！");
                        return;
                    }

                    AnimationInfo animationInfo = new AnimationInfo();
                    animationInfo.name = dgvAnimation.Rows[e.RowIndex].Cells[0].Value.ToString();
                    animationInfo.direction = dgvAnimation.Rows[e.RowIndex].Cells[1].Value.ToString();
                    CloneBitmap(ofd.FileNames, animationInfo);
                    //填到listbox里面去
                    FillListBox(ofd.FileNames);

                    animationInfos.Add(dgvAnimation.Rows[e.RowIndex].Cells[3].Value.ToString()
                        + dgvAnimation.Rows[e.RowIndex].Cells[1].Value.ToString(), animationInfo);
                }
            }

        }

        private void dgvAnimation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 1)
            {
                listBoxAnimation.Items.Clear();
                string key = string.Empty;
                //if (animationVersion.Equals("1")) 
                //{
                //    key = "";
                //}
                //else 
                //{
                    key = dgvAnimation.Rows[e.RowIndex].Cells[3].Value.ToString()
                        + dgvAnimation.Rows[e.RowIndex].Cells[1].Value.ToString();
                //}
                if (animationInfos.ContainsKey(key))
                {
                    AnimationInfo animation = animationInfos[key];
                    foreach (AnimationImageInfo item in animation.images)
                    {
                        listBoxAnimation.Items.Add(item);
                    }
                }

                panelAnimation.CreateGraphics().Clear(Color.White);

                cbPlay.Checked = false;
            }
        }

        private void FillListBox(string[] paths)
        {
            listBoxAnimation.Items.Clear();
            string name = string.Empty;
            AnimationImageInfo animationImageInfo;
            foreach (string item in paths)
            {
                name = item.Substring(item.LastIndexOf('\\') + 1);
                animationImageInfo = new AnimationImageInfo();
                animationImageInfo.name = name;
                animationImageInfo.path = item;
                listBoxAnimation.Items.Add(animationImageInfo);
            }
        }

        private void FillListBox(AnimationInfo animationInfo)
        {
            foreach (AnimationImageInfo item in animationInfo.images)
            {
                listBoxAnimation.Items.Add(item);
            }
        }

        private void CloneBitmap(string[] paths, AnimationInfo animationInfo)
        {
            int orderIndex = 0;
            Bitmap image1;
            Rectangle rect;

            int wStart = 0;
            int wEnd = 0;

            int hStart = 0;
            int hEnd = 0;

            int w = 0, h = 0;

            AnimationImageInfo aii;

            foreach (string path in paths)
            {
                //DateTime st = DateTime.Now;
                image1 = (Bitmap)Image.FromFile(path);

                wStart = image1.Width;
                wEnd = 0;

                hStart = image1.Height;
                hEnd = 0;

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);

                        if (pixelColor.A > 0)
                        {
                            if (wStart > x)
                            {
                                wStart = x;
                            }
                            if (wEnd < x)
                            {
                                wEnd = x;
                            }

                            if (hStart > y)
                            {
                                hStart = y;
                            }
                            if (hEnd < y)
                            {
                                hEnd = y;
                            }
                        }
                    }
                }

                w = wEnd - wStart;
                h = hEnd - hStart;

                //DateTime et = DateTime.Now;
                //TimeSpan ts = et - st;
                //Console.WriteLine(ts.ToString());

                //st = DateTime.Now;
                rect = new Rectangle(wStart, hStart, w, h);

                Bitmap cloneBit = new Bitmap(w, h);
                Graphics imgGraphics = Graphics.FromImage(cloneBit);
                imgGraphics.DrawImage(image1, new Rectangle(0, 0, w, h), rect, GraphicsUnit.Pixel);

                //Bitmap cloneBit = image1.Clone(rect, format);

                aii = new AnimationImageInfo();
                aii.bitmap = cloneBit;
                aii.originalBitmap = image1;
                aii.x = wStart;
                aii.y = hStart;
                aii.w = w;
                aii.h = h;
                aii.originalW = image1.Width;
                aii.originalH = image1.Height;
                aii.orderIndex = orderIndex;
                aii.name = path.Substring(path.LastIndexOf('\\') + 1);
                aii.path = path;
                aii.area = w * h;

                //et = DateTime.Now;
                //ts = et - st;
                //Console.WriteLine(ts.ToString());
                orderIndex = orderIndex + 1;
                animationInfo.images.Add(aii);

            }
        }

        private void btSaveAnimation_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "XML文件(*.xml)|*.xml";//文本文件(*.txt)|*.txt|所有文件(*.*)|*.*
            if (sfg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = sfg.FileName.Substring(sfg.FileName.LastIndexOf('\\') + 1);
                string path = sfg.FileName.Substring(0, sfg.FileName.LastIndexOf('\\') + 1);
                SaveAsPic(name, path);
                SaveAsXML(name, path);

                MessageBox.Show("保存完成！");
            }
        }



        private void SaveAsPic(string fileName, string savePath)
        {
            #region old
            //Bitmap bitmap = new Bitmap(Parameters.PICSIZE, Parameters.PICSIZE);
            //Graphics graphics = Graphics.FromImage(bitmap);
            //AnimationImageInfo animationIamgeInfo;
            //AnimationImageInfo animationPre = null;

            //int indexCount = 0;
            //int xCount = 0;
            //int yCount = 0;

            //int picIndex = 0;

            //string path = string.Empty;

            //path = savePath + txtAnimation.Text + "\\" + fileName + "_" + picIndex.ToString() + ".png";

            //foreach (KeyValuePair<string,AnimationInfo> item in animationInfos)
            //{
            //    for (int j = 0; j < item.Value.images.Count; j++)
            //    {
            //        animationIamgeInfo = item.Value.images[j];

            //        if (xCount + animationIamgeInfo.w > Parameters.PICSIZE)
            //        {
            //            xCount = 0;
            //            yCount = yCount + animationIamgeInfo.h + Parameters.HEIGHTSPACING;
            //            if (yCount > Parameters.PICSIZE)
            //            {
            //                if (!Directory.Exists(savePath + txtAnimation.Text))
            //                {
            //                    Directory.CreateDirectory(savePath + txtAnimation.Text);
            //                }
            //                bitmap.Save(path);
            //                picIndex++;
            //                path = savePath + txtAnimation.Text + "\\" +fileName + "_" + picIndex.ToString() + ".png";
            //                bitmap = new Bitmap(Parameters.PICSIZE, Parameters.PICSIZE);
            //                graphics = Graphics.FromImage(bitmap);

            //                xCount = yCount = 0;
            //            }
            //        }

            //        graphics.DrawImage(animationIamgeInfo.bitmap,
            //        new Point(xCount, yCount));

            //        animationIamgeInfo.path = fileName + "_" + picIndex.ToString() + ".png";
            //        animationIamgeInfo.xClone = xCount;
            //        animationIamgeInfo.yClone = yCount;

            //        xCount = xCount + animationIamgeInfo.w;

            //        animationPre = animationIamgeInfo;

            //        Console.WriteLine(indexCount++);
            //    }
            //}

            //if (!Directory.Exists(savePath + txtAnimation.Text)) 
            //{
            //    Directory.CreateDirectory(savePath + txtAnimation.Text);
            //}

            //bitmap.Save(path);
            #endregion

            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            Dictionary<int, BigImageImfo> bigImageInfos = new Dictionary<int, BigImageImfo>();
            int count = 0;//计算有多少张大图
            int idCount = 0;//计算每一个节点的ID号

            List<AnimationImageInfo> imageList = new List<AnimationImageInfo>();
            bool isAdd = true;
            foreach (KeyValuePair<string, AnimationInfo> item in animationInfos)
            {
                for (int m = 0; m < item.Value.images.Count; m++)
                {
                    isAdd = true;
                    for (int i = 0; i < imageList.Count; i++)
                    {
                        if (item.Value.images[m].area > imageList[i].area)
                        {
                            imageList.Insert(i, item.Value.images[m]);
                            isAdd = false;
                            break;
                        }

                    }
                    if (isAdd)
                    {
                        imageList.Add(item.Value.images[m]);
                    }
                }
            }


            bool goOn = true;
            AnimationImageInfo image;
            for (int i = 0; i < imageList.Count; )//上图后,i才+1
            {
                image = imageList[i];

                foreach (KeyValuePair<int, BigImageImfo> bigImageInfo in bigImageInfos)
                {
                    PreOrder(bigImageInfo.Value._rootLeaf, image, ref goOn, bigImageInfo.Key);
                    if (!goOn) break;
                }

                if (getDrawLeaf != null)
                {
                    //1、切图，得到左右节点
                    //2、判断能不能切，不能切则树叶节点为null
                    //3、当图片的h大于w时，横起切，当图片的w大于h时竖起切
                    if (image.h >= image.w)//横切
                    {
                        if ((getDrawLeaf.h - image.h) > 0)//得到左节点
                        {
                            idCount = idCount + 1;
                            getDrawLeaf.leftLeaf = new Leaf(getDrawLeaf.x, getDrawLeaf.y + getDrawLeaf.image.h, getDrawLeaf.w,
                                getDrawLeaf.h - image.h,
                                    null, null, getDrawLeaf, null, true, getDrawLeaf.key, idCount);
                        }
                        if ((getDrawLeaf.w - image.w) > 0)//得到右节点
                        {
                            idCount = idCount + 1;
                            getDrawLeaf.rightLeaf = new Leaf(getDrawLeaf.x + getDrawLeaf.image.w, getDrawLeaf.y,
                                getDrawLeaf.w - image.w, image.h,
                                    null, null, getDrawLeaf, null, false, getDrawLeaf.key, idCount);
                        }
                    }
                    else //竖切
                    {
                        if ((getDrawLeaf.h - image.h) > 0)
                        {
                            idCount = idCount + 1;
                            getDrawLeaf.leftLeaf = new Leaf(getDrawLeaf.x, getDrawLeaf.y + getDrawLeaf.image.h, image.w,
                                getDrawLeaf.h - image.h,
                                    null, null, getDrawLeaf, null, true, getDrawLeaf.key, idCount);
                        }
                        if ((getDrawLeaf.w - image.w) > 0)
                        {
                            idCount = idCount + 1;
                            getDrawLeaf.rightLeaf = new Leaf(getDrawLeaf.x + getDrawLeaf.image.w, getDrawLeaf.y,
                                getDrawLeaf.w - image.w, getDrawLeaf.h,
                                    null, null, getDrawLeaf, null, false, getDrawLeaf.key, idCount);
                        }
                    }


                    //上图
                    bigImageInfos[getDrawLeaf.key]._graphics.DrawImage(image.bitmap,
                        new Rectangle(getDrawLeaf.x, getDrawLeaf.y, image.w, image.h),
                    new Rectangle(0, 0, image.w, image.h), GraphicsUnit.Pixel);

                    image.path = Parameters.PICFILE + "\\" + fileName + "_" + getDrawLeaf.key + ".png";
                    image.xClone = getDrawLeaf.x;
                    image.yClone = getDrawLeaf.y;

                    #region  测试切块是否正确
                    ////自己的边框
                    //bigImageInfos[getDrawLeaf.key]._graphics.DrawRectangle(new Pen(Color.Black),
                    //    new Rectangle(getDrawLeaf.x, getDrawLeaf.y, image.w, image.h));

                    ////左节点的边框
                    //if (getDrawLeaf.leftLeaf != null) 
                    //{
                    //    bigImageInfos[getDrawLeaf.key]._graphics.DrawRectangle(new Pen(Color.Black),
                    //        new Rectangle(getDrawLeaf.leftLeaf.x, getDrawLeaf.leftLeaf.y, getDrawLeaf.leftLeaf.w, getDrawLeaf.leftLeaf.h));

                    //}

                    ////右节点的边框
                    //if (getDrawLeaf.rightLeaf != null) 
                    //{
                    //    bigImageInfos[getDrawLeaf.key]._graphics.DrawRectangle(new Pen(Color.Black),
                    //        new Rectangle(getDrawLeaf.rightLeaf.x, getDrawLeaf.rightLeaf.y, getDrawLeaf.rightLeaf.w, getDrawLeaf.rightLeaf.h));

                    //}
                    #endregion

                    getDrawLeaf = null;
                    goOn = true;

                    i++;
                }
                else
                {
                    count++;
                    idCount = CreateRootLeaf(imageList[i], count, bigImageInfos, idCount);
                }
            }

            foreach (KeyValuePair<int, BigImageImfo> bigImageInfo in bigImageInfos)
            {
                if (!Directory.Exists(savePath + txtAnimation.Text + Parameters.PICFILE))
                {
                    Directory.CreateDirectory(savePath + txtAnimation.Text + Parameters.PICFILE);
                }
                bigImageInfo.Value._bitmap.Save(savePath + txtAnimation.Text + Parameters.PICFILE + "\\" + fileName + "_" + bigImageInfo.Key + ".png");
            }
        }

        private void SaveAsXML(string fileName, string savePath)
        {
            if (animationInfos != null)
            {
                savePath = savePath + txtAnimation.Text + "\\" + fileName;
                XmlDocument myXmlDoc = new XmlDocument();
                XmlElement rootElement = myXmlDoc.CreateElement("root");
                rootElement.SetAttribute("num", animationInfos.Count.ToString());
                rootElement.SetAttribute("version", "1");

                myXmlDoc.AppendChild(rootElement);

                XmlElement animationElement;
                XmlElement nameElement = null;
                XmlElement picElement;
                XmlElement firstLevelElement1;
                XmlElement secondLevelElement11;
                XmlElement thirdLevelElementXClone;
                XmlElement thirdLevelElementYClone;
                XmlElement thirdLevelElementX;
                XmlElement thirdLevelElementY;
                XmlElement thirdLevelElementW;
                XmlElement thirdLevelElementH;
                XmlElement thirdLevelElementOriginalW;
                XmlElement thirdLevelElementOriginalH;
                XmlElement thirdLevelElementPath;
                XmlElement thirdLevelElementName;

                animationElement = myXmlDoc.CreateElement("Animation");
                rootElement.AppendChild(animationElement);

                List<string> picNames = new List<string>();
                string picName = string.Empty;
                List<string> animationNames = new List<string>();

                foreach (KeyValuePair<string, AnimationInfo> item in animationInfos)
                {
                    if (!animationNames.Contains(item.Value.name))
                    {
                        nameElement = myXmlDoc.CreateElement("Name");
                        nameElement.InnerText = item.Value.name;
                        nameElement.SetAttribute("num", "1");
                        animationElement.AppendChild(nameElement);
                        animationNames.Add(item.Value.name);
                    }
                    else
                    {
                        if (nameElement != null)
                        {
                            int i = int.Parse(nameElement.GetAttribute("num"));
                            nameElement.SetAttribute("num", (i + 1).ToString());
                        }

                    }


                    firstLevelElement1 = myXmlDoc.CreateElement(item.Value.name);
                    firstLevelElement1.SetAttribute("direction", Animation.TransDirectionCN2EN(item.Value.direction));
                    firstLevelElement1.SetAttribute("num", item.Value.images.Count.ToString());

                    rootElement.AppendChild(firstLevelElement1);

                    for (int j = 0; j < item.Value.images.Count; j++)
                    {
                        secondLevelElement11 = myXmlDoc.CreateElement("orderIndex");
                        secondLevelElement11.SetAttribute("value", item.Value.images[j].orderIndex.ToString());
                        firstLevelElement1.AppendChild(secondLevelElement11);

                        thirdLevelElementXClone = myXmlDoc.CreateElement("xClone");
                        thirdLevelElementXClone.InnerText = item.Value.images[j].xClone.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementXClone);

                        thirdLevelElementYClone = myXmlDoc.CreateElement("yClone");
                        thirdLevelElementYClone.InnerText = item.Value.images[j].yClone.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementYClone);

                        thirdLevelElementX = myXmlDoc.CreateElement("x");
                        thirdLevelElementX.InnerText = item.Value.images[j].x.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementX);

                        thirdLevelElementY = myXmlDoc.CreateElement("y");
                        thirdLevelElementY.InnerText = item.Value.images[j].y.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementY);

                        thirdLevelElementW = myXmlDoc.CreateElement("w");
                        thirdLevelElementW.InnerText = item.Value.images[j].w.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementW);

                        thirdLevelElementH = myXmlDoc.CreateElement("h");
                        thirdLevelElementH.InnerText = item.Value.images[j].h.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementH);

                        thirdLevelElementOriginalW = myXmlDoc.CreateElement("originalW");
                        thirdLevelElementOriginalW.InnerText = item.Value.images[j].originalW.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementOriginalW);

                        thirdLevelElementOriginalH = myXmlDoc.CreateElement("originalH");
                        thirdLevelElementOriginalH.InnerText = item.Value.images[j].originalH.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementOriginalH);

                        thirdLevelElementPath = myXmlDoc.CreateElement("path");
                        thirdLevelElementPath.InnerText = item.Value.images[j].path.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementPath);

                        thirdLevelElementName = myXmlDoc.CreateElement("name");
                        thirdLevelElementName.InnerText = item.Value.images[j].name.ToString();
                        secondLevelElement11.AppendChild(thirdLevelElementName);

                        picName = thirdLevelElementPath.InnerText.Substring(thirdLevelElementPath.InnerText.LastIndexOf('\\') + 1);
                        if (!picNames.Contains(picName))
                        {
                            picNames.Add(picName);
                        }
                    }
                }

                foreach (string item in picNames)
                {
                    picElement = myXmlDoc.CreateElement("pic");
                    picElement.InnerText = Parameters.PICFILE + "\\" + item;
                    animationElement.AppendChild(picElement);
                }

                myXmlDoc.Save(savePath);

                Console.WriteLine("done!");
            }
        }


        private void listBoxAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelAnimation.CreateGraphics().Clear(Color.White);
            if (listBoxAnimation.SelectedIndex == -1) return;

            AnimationImageInfo a = (AnimationImageInfo)listBoxAnimation.Items[listBoxAnimation.SelectedIndex];

            if (a.originalBitmap != null)
            {
                panelAnimation.CreateGraphics().DrawImage(a.originalBitmap, 0, 0, panelAnimation.Width, panelAnimation.Height);
            }
            else
            {
                panelAnimation.CreateGraphics().DrawImage(Image.FromFile(a.path), 0, 0, panelAnimation.Width, panelAnimation.Height);
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            if (animationInfo != null)
            {
                count = count == animationInfo.images.Count - 1 ? 0 : count + 1;
                panelAnimation.Refresh();
            }
        }

        private void cbPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlay.Checked)
            {
                timerAnimation.Start();
                panelAnimation.Refresh();
            }
            else
            {
                timerAnimation.Stop();
                animationInfo = null;
                count = 0;
            }
        }

        int count = 0;
        AnimationInfo animationInfo;

        private void panelAnimation_Paint(object sender, PaintEventArgs e)
        {
            if (cbPlay.Checked && dgvAnimation_mouseClick != -1)
            {
                if (dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[1].Value == null)
                {
                    cbPlay.Checked = false;
                    return;
                }
                string key = dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[3].Value.ToString()
                    + dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[1].Value.ToString();
                if (animationInfos.ContainsKey(key))
                {
                    animationInfo = animationInfos[key];
                    e.Graphics.DrawImage(animationInfo.images[count].originalBitmap, 0, 0,
                        panelAnimation.Width, panelAnimation.Height);
                }
            }
        }

        private void btPicDel_Click(object sender, EventArgs e)
        {
            cbPlay.Checked = false;
            panelAnimation.CreateGraphics().Clear(Color.White);
            if (dgvAnimation_mouseClick != -1 && dgvAnimation.Rows.Count > 0)
            {
                if (dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[0].Value == null)
                {
                    dgvAnimation.Rows.RemoveAt(dgvAnimation_mouseClick);
                    return;
                }
                string name = dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[0].Value.ToString();
                string id = dgvAnimation.Rows[dgvAnimation_mouseClick].Cells[3].Value.ToString();
                if (MessageBox.Show("确定删除" + name + "下所有的动态图？", "警告", MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    dgvAnimation.Rows.RemoveAt(dgvAnimation_mouseClick);

                    animationInfos.Remove(id + "1-上");
                    animationInfos.Remove(id + "2-右上");
                    animationInfos.Remove(id + "3-右");
                    animationInfos.Remove(id + "4-右下");
                    animationInfos.Remove(id + "5-下");
                    animationInfos.Remove(id + "6-左下");
                    animationInfos.Remove(id + "7-左");
                    animationInfos.Remove(id + "8-左上");
                    animationInfos.Remove(id + "0-无");
                }
            }
        }

        private void btLoadAnimation_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            if (animationInfos != null && animationInfos.Count != 0)
            {
                if (MessageBox.Show("加载会丢失已经编辑的数据，是否继续？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    text = OpenAnimationXML();
                }
                else
                {
                    return;
                }
            }
            else
            {
                text = OpenAnimationXML();
            }
            string temp = string.Empty;
            if (animationInfos != null)
            {
                foreach (KeyValuePair<string, AnimationInfo> item in animationInfos)
                {
                    if (!temp.Equals(item.Value.name))
                    {
                        int i = this.dgvAnimation.Rows.Add();
                        dgvAnimation.Rows[i].Cells[0].Value = item.Value.name;
                        dgvAnimation.Rows[i].Cells[3].Value = i;
                        dgvAnimation.Rows[i].Cells[1].Value = "0-无";

                        temp = item.Value.name;
                    }
                }
                this.btNewAnimation.Enabled = false;
                this.btPicAdd.Enabled = true;
                this.btPicDel.Enabled = true;

                this.txtAnimation.Text = text;
            }

        }
        private string OpenAnimationXML()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Parameters.FILTERXML;
            ofd.Multiselect = false;
            string name = string.Empty;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                animationInfos = Animation.ReadAnimationXML(ofd.FileName);
                animationVersion = Animation.GetAnimationVersion();
                DirectoryInfo di = Directory.GetParent(ofd.FileName);
                name = di.Name;
            }
            return name;
        }

        private int CreateRootLeaf(AnimationImageInfo image, int count,
    Dictionary<int, BigImageImfo> bigImageInfos, int idCount)
        {
            //制造根节点
            Bitmap bitmap = new Bitmap(2048, 2048);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawImage(image.bitmap, new Rectangle(0, 0, image.w, image.h),
                new Rectangle(0, 0, image.w, image.h), GraphicsUnit.Pixel);

            //永远都横起切
            Leaf leaf = new Leaf(0, 0, bitmap.Width, bitmap.Height, null,
                null, null, image, false, count, idCount);
            leaf.leftLeaf = new Leaf(0, image.h, bitmap.Width, bitmap.Height - image.h, null, null, leaf, null, true, count, idCount + 1);
            leaf.rightLeaf = new Leaf(image.w, 0, bitmap.Width - image.w, image.h, null, null, leaf, null, false, count, idCount + 2);

            bigImageInfos.Add(count, new BigImageImfo(bitmap, graphics, leaf));

            return idCount + 2;
        }

        public Leaf getDrawLeaf;

        private void PreOrder(Leaf leaf, AnimationImageInfo image, ref bool goOn, int key)
        {
            if (!goOn) return;
            if (leaf != null)
            {
                if (leaf.image == null && goOn) //首先确定该叶节点有没有图片
                {
                    if (leaf.w >= image.w && leaf.h >= image.h) //该叶节点能不能画指定的图片,如果可以画则返回这个叶节点
                    {
                        goOn = false;
                        leaf.image = image;
                        leaf.key = key;
                        getDrawLeaf = leaf;

                        Console.WriteLine("x:" + leaf.x + " y:" + leaf.y + " id:" + leaf.id);
                    }
                }

                PreOrder(leaf.leftLeaf, image, ref goOn, key);
                PreOrder(leaf.rightLeaf, image, ref goOn, key);
            }
        }
        #endregion

        private void btShowMap_Click(object sender, EventArgs e)
        {
            if (_mapeditForm != null)
            {
                btShowMap.Enabled = false;
                _mapeditForm.Show();
            }
        }
    }

}
