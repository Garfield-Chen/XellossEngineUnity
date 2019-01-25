﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace tyoEngineEditor
{
    public partial class tyoEngineTileMapEditWindow : Form
    {
        public class MapEditPanel : System.Windows.Forms.Panel
        {
            public MapEditPanel()
            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

                this.SetStyle(ControlStyles.UserPaint, true);

                this.UpdateStyles();
            }
        }

        public class ComboBoxNoWheel : System.Windows.Forms.ComboBox, IMessageFilter
        {
            public ComboBoxNoWheel()
            {
                Application.AddMessageFilter(this);
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x020A)
                {
                    return true;
                }

                return false;
            }
        }

        public tyoEngineTileMapEditWindow()
        {
            InitializeComponent();

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            //this.SetStyle(ControlStyles.UserPaint, true);

            //this.UpdateStyles();

            //this.DoubleBuffered = true;

            //this.DoubleBuffered = true;
            // SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //this.UpdateStyles();

            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //             this.SetStyle(ControlStyles.UserPaint, true);
            //             this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //             this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //             this.SetStyle(ControlStyles.DoubleBuffer, true);
            //             this.SetStyle(ControlStyles.ResizeRedraw, true);
            //             this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            // 
            //             this.UpdateStyles();

            _penMesh.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.panelMap.MouseWheel += panelMap_MouseWheel;
        }

        void panelMap_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta.Equals(-120) && this.mapVScrollBar.Value < this.mapVScrollBar.Maximum)
            {
                if (this.mapVScrollBar.Value
                    + this.mapVScrollBar.Maximum / Parameters.mouseWheelScale
                    > this.mapVScrollBar.Maximum)
                {
                    this.mapVScrollBar.Value = this.mapVScrollBar.Maximum;
                }
                else
                {
                    this.mapVScrollBar.Value = this.mapVScrollBar.Value
                        + this.mapVScrollBar.Maximum / Parameters.mouseWheelScale;
                }
            }
            else if (e.Delta.Equals(120) && this.mapVScrollBar.Value > 0)
            {
                if (this.mapVScrollBar.Value
                    - this.mapVScrollBar.Maximum / Parameters.mouseWheelScale < 0)
                {
                    this.mapVScrollBar.Value = 0;
                }
                else
                {
                    this.mapVScrollBar.Value = this.mapVScrollBar.Value
                        - this.mapVScrollBar.Maximum / Parameters.mouseWheelScale;
                }
            }
        }

        public class MapTilePiece
        {
            public Bitmap _tile = null;
            //public MapTitleInfos _titleinfo;
            public int _x;
            public int _y;
            public int _w;
            public int _h;
        }

        MapInfos _mapInfos = null;
        tyoEngineOffset _offset = null;
        AnimationOffset _selectedAnimationKey = new AnimationOffset();

        public MapInfos GetMapInfos()
        {
            return _mapInfos;
        }

        Image _mapTileSelect = null;

        public void SetMapTileSelect(Image _img)
        {
            _mapTileSelect = _img;
        }

        public Image GetMapTileSelect()
        {
            return _mapTileSelect;
        }

        public void SetMapInfos(MapInfos mapinfos)
        {
            if (mapinfos != null)
            {
                _mapInfos = mapinfos;

                return;
            }
        }

        public void SetUpdateTimer(int _dt)
        {
            this.updateTimer.Interval = _dt;
        }

        int _mapWidthByPixel = 0;
        int _mapHeightByPixel = 0;

        int _mapHBarCount = 0;
        int _mapVBarCount = 0;

        Size _WindowSize = new Size();

        tyoEngineTileMapEditTilePieceWin _PieceDlg = null;
        tyoEngineTileMapPreview _PreviewDlg = null;

        private void tyoEngineTileMapEditWindow_Shown(object sender, EventArgs e)
        {
            if (_mapInfos == null)
            {
                Close();

                return;
            }
            else
            {
                if (_mapInfos._IsLoadMap == false)
                {
                    _mapInfos.ReInit();
                }
            }

            this.Text = _mapInfos.Name;

            if (comboBoxFun1.Items.Count > 0)
            {
                comboBoxFun1.SelectedIndex = 0;
            }

            if (comboBoxFun3.Items.Count > 0)
            {
                comboBoxFun3.SelectedIndex = 1;
            }

            _WindowSize.Width = Width;
            _WindowSize.Height = Height;

            UpdateMapScrollBar();

            _mapLayerDepth.Clear();

            comboBoxMapLayer.Items.Clear();
            checkedListBoxMapLayerShow.Items.Clear();

            foreach (int index in _mapInfos._mapLayerInfosByIndex.Keys)
            {
                MapLayerDrawInfos tmpLayerDepth = new MapLayerDrawInfos();

                tmpLayerDepth._depth = _mapInfos._mapLayerInfosByIndex[index]._depth;
                tmpLayerDepth._index = index;

                _mapLayerDepth.Add(tmpLayerDepth);

                if (_mapInfos._mapLayerInfosByIndex[index].Name.Equals(Parameters.SYSYTEMUSE))
                {
                    continue;
                }
                checkedListBoxMapLayerShow.Items.Insert(index, _mapInfos._mapLayerInfosByIndex[index].Name);

                checkedListBoxMapLayerShow.SelectedIndex = index;

                checkedListBoxMapLayerShow.SetItemChecked(index, true);

                comboBoxMapLayer.Items.Insert(index, _mapInfos._mapLayerInfosByIndex[index].Name);
            }

            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                int index = -1;

                for (int j = i + 1; j < _mapInfos._mapLayerInfosByIndex.Count; ++j)
                {
                    if (_mapLayerDepth[i]._depth > _mapLayerDepth[j]._depth)
                    {
                        index = j;
                    }
                }

                if (index != -1)
                {
                    int tmpdepth = _mapLayerDepth[index]._depth;
                    int tmpindex = _mapLayerDepth[index]._index;

                    _mapLayerDepth[index]._depth = _mapLayerDepth[i]._depth;
                    _mapLayerDepth[index]._index = _mapLayerDepth[i]._index;

                    _mapLayerDepth[i]._depth = tmpdepth;
                    _mapLayerDepth[i]._index = tmpindex;
                }
            }

            if (checkedListBoxMapLayerShow.Items.Count > 0)
            {
                checkedListBoxMapLayerShow.SelectedIndex = 0;
            }

            if (comboBoxMapLayer.Items.Count > 0)
            {
                comboBoxMapLayer.SelectedIndex = 0;
            }



            this.updateTimer.Start();
        }

        List<MapLayerDrawInfos> _mapLayerDepth = new List<MapLayerDrawInfos>();

        List<MapTilePiece> _tileImageList = new List<MapTilePiece>();

        public List<MapTilePiece> GetTileImageList()
        {
            return _tileImageList;
        }

        bool _showMeshGrid = false;

        bool _showBlockGrid = false;


        Pen _whilePen = new Pen(Color.White, 1);

        public Pen GetWhilePen()
        {
            return _whilePen;
        }

        Pen _blackPen = new Pen(Color.Black, 1);

        public Pen GetBlackPen()
        {
            return _blackPen;
        }

        Pen _deletPen = new Pen(Color.RosyBrown, 2);

        public Pen GetDeletePen()
        {
            return _deletPen;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (_PieceDlg != null)
            {
                _PieceDlg.UpdateTick();
            }

            //for (int i = 0; i < _mapInfos._mapAnimationUseInfo.Count; i++)
            //{
            //    _mapInfos._mapAnimationUseInfo[i].count =
            //        _mapInfos._mapAnimationUseInfo[i].count == _mapInfos._mapAnimationUseInfo[i].bitmapsLength - 1 ?
            //        0 : _mapInfos._mapAnimationUseInfo[i].count + 1;
            //}

            //panelMap.Refresh();

            int x = _mapMouse.X / _mapInfos.Map_Tile_Width + mapHScrollBar.Value;
            int y = _mapMouse.Y / _mapInfos.Map_Tile_Height + mapVScrollBar.Value;

            labelMapMousPos.Text = "坐标: X=" + x.ToString() + " Y=" + y.ToString();
        }


        public class SelectIndexType
        {
            public int _x;
            public int _y;

            public int _type;
        }

        public List<SelectIndexType> _nowSelectPieceIndexByMap = new List<SelectIndexType>(64);

        //记录选中的动态图的键
        public List<int> _nowAnimationKeys = new List<int>();

        private int _nowAnimationKey = -1;

        Point _mapMouse = new Point(0, 0);

        public int _nowSelectPieceW = 0;
        public int _nowSelectPieceH = 0;

        //获取type
        //1 x + 1
        //2 x - 1
        //3 y + 1
        //4 y - 1
        //return index
        private int GetNowSelectPieceType(int x, int y, int direction)
        {
            if (direction == 1)
            {
                x += 1;

                if (x >= _nowSelectPieceW)
                {
                    x = 0;
                }
            }
            else if (direction == 2)
            {
                x -= 1;

                if (x < 0)
                {
                    x = (_nowSelectPieceW - 1);
                }
            }
            else if (direction == 3)
            {
                y += 1;

                if (y >= _nowSelectPieceH)
                {
                    y = 0;
                }

            }
            else if (direction == 4)
            {
                y -= 1;

                if (y < 0)
                {
                    y = (_nowSelectPieceH - 1);
                }
            }


            for (int i = 0; i < _nowSelectPieceIndexByMap.Count; ++i)
            {
                if (_nowSelectPieceIndexByMap[i]._x == x &&
                    _nowSelectPieceIndexByMap[i]._y == y)
                {
                    return i;
                }
            }

            return -1;
        }

        Bitmap _nowSelectPiece = null;

        public void SetNowSelectPiece(Bitmap _img)
        {
            _nowSelectPiece = _img;
            pictureBoxNowSelect.Image = _nowSelectPiece;
        }

        public Bitmap GetNowSelectPiece()
        {
            return _nowSelectPiece;
        }

        Pen _penMesh = new Pen(Brushes.DarkGray);
        Brush _blockBrush1 = new SolidBrush(Color.FromArgb(120, Color.DeepPink));
        Brush _blockBrush2 = new SolidBrush(Color.FromArgb(120, Color.BlueViolet));
        Brush _blockBrush_Green = new SolidBrush(Color.FromArgb(120, Color.SeaGreen));

        private void AnimationPaint(int key,Graphics e,int x,int y,int id) 
        {
            if (_mapInfos._mapAnimationUseInfo.ContainsKey(key))
            {
                int offsetx=0, offsety = 0;
                if (_mapInfos._mapAnimationOffsets.ContainsKey(id)) 
                {
                    offsetx = _mapInfos._mapAnimationOffsets[id]._offsetX;
                    offsety = _mapInfos._mapAnimationOffsets[id]._offsetY;
                }

                e.DrawImage(
                _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap,
                (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width + offsetx,
                (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height + offsety,
                 _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap.Width,
                 _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap.Height);

                e.FillRectangle(_blockBrush_Green, (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width,
                    (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height,
                    _mapInfos.Map_Tile_Width, _mapInfos.Map_Tile_Height);
            }
        }

        private void panelMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(_MapTileScale, _MapTileScale);

            // this paint will be change to only update screen size object.
            int _wcount = (int)((float)panelMap.Width / (_mapInfos.Map_Tile_Width * _MapTileScale)) + 20;
            int _hcount = (int)((float)panelMap.Height / (_mapInfos.Map_Tile_Height * _MapTileScale)) + 20;

            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count/*comboBoxMapLayer.Items.Count*/; ++i)
            {
                if (_mapInfos._LayerShowFlag[_mapLayerDepth[i]._index] == false)
                {
                    continue;
                }

                int _tmpXOffsetByRender = 0;

                if (mapHScrollBar.Value > 20)
                {
                    _tmpXOffsetByRender = mapHScrollBar.Value - 20;
                }

                for (int x = _tmpXOffsetByRender; x < mapHScrollBar.Value + _wcount + 1/*(_mapInfos.Map_Size_Width)*/; ++x)
                {
                    if (x >= _mapInfos.Map_Size_Width)
                    {
                        break;
                    }

                    int _tmpYOffsetByRender = 0;

                    if (mapVScrollBar.Value > 20)
                    {
                        _tmpYOffsetByRender = mapVScrollBar.Value - 20;
                    }

                    for (int y = _tmpYOffsetByRender; y < mapVScrollBar.Value + _hcount + 1/*_mapInfos.Map_Size_Height*/; ++y)
                    {
                        if (y >= _mapInfos.Map_Size_Height)
                        {
                            break;
                        }

                        int index = _mapInfos._mapTile[_mapLayerDepth[i]._index, x, y];

                        //if (_mapInfos._mapTitle[1, x, y] != -1)
                        //{
                        //    index = _mapInfos._mapTitle[1, x, y];
                        //    MapUseTitleInfo m = _mapInfos._mapTitleUseInfo[index];
                        //}

                        if (index != -1)
                        {
                            //e.Graphics.DrawImage(_mapInfos._mapTitleUseInfo[index]._image, (x - mapHScrollBar.Value) * _mapInfos.Map_Title_Width, (y - mapVScrollBar.Value) * _mapInfos.Map_Title_Height);

                            if (_mapInfos._mapTileUseInfo[index]._image._tile != null)
                            {
                                e.Graphics.DrawImage(
                                _mapInfos._mapTileUseInfo[index]._image._tile,
                                (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width,
                                (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height,
                                _mapInfos._mapTileUseInfo[index]._image._w,
                                _mapInfos._mapTileUseInfo[index]._image._h);
                            }
                        }
                        if (_mapInfos._mapAnimationTile != null)
                        {
                            int key = _mapInfos._mapAnimationTile[_mapLayerDepth[i]._index, x, y];
                            int id = -1;
                            foreach (KeyValuePair<int, AnimationOffset> items in _mapInfos._mapAnimationOffsets)
                            {
                                if (items.Value._x == x && items.Value._y == y)
                                {
                                    id = items.Key;
                                    break;
                                }
                            }

                            if (key!=-1&&_offset != null)
                            {
                                if (_selectedAnimationKey._layer == _mapLayerDepth[i]._index &&
                                    _selectedAnimationKey._x == x && _selectedAnimationKey._y == y) 
                                {

                                    e.Graphics.DrawImage(
                                        _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap,
                                        (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width + _offset._offsetX,
                                        (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height + _offset._offsetY,
                                         _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap.Width,
                                         _mapInfos._mapAnimationUseInfo[key].animationImageInfos[_mapInfos._mapAnimationUseInfo[key].count].onPanelBitmap.Height);

                                    e.Graphics.FillRectangle(_blockBrush_Green, (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width,
                                        (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height,
                                        _mapInfos.Map_Tile_Width, _mapInfos.Map_Tile_Height);

                                    
                                        _mapInfos._mapAnimationOffsets[id]._offsetX = _offset._offsetX;
                                        _mapInfos._mapAnimationOffsets[id]._offsetY = _offset._offsetY;
                                    
                                }
                                else 
                                {
                                    AnimationPaint(key, e.Graphics, x, y, id);
                                }
                            }

                            if (key != -1 && _offset == null)
                            {
                                AnimationPaint(key, e.Graphics, x, y, id);
                            }
                            
                        }
                    }
                }
            }

            if (_showBlockGrid)
            {

                for (int x = mapHScrollBar.Value; x < mapHScrollBar.Value + _wcount + 1/*_mapInfos.Map_Size_Width*/; ++x)
                {
                    if (x >= _mapInfos.Map_Size_Width)
                    {
                        break;
                    }

                    for (int y = mapVScrollBar.Value; y < mapVScrollBar.Value + _hcount + 1/*_mapInfos.Map_Size_Height*/; ++y)
                    {
                        if (y >= _mapInfos.Map_Size_Height)
                        {
                            break;
                        }

                        if (_mapInfos._mapBlockFlag[x, y] == true)
                        {
                            e.Graphics.FillRectangle(_blockBrush1, (x - mapHScrollBar.Value) * _mapInfos.Map_Tile_Width, (y - mapVScrollBar.Value) * _mapInfos.Map_Tile_Height, _mapInfos.Map_Tile_Width, _mapInfos.Map_Tile_Height);
                        }
                    }
                }
            }


            if (_PieceDlg != null)
            {
                if (comboBoxFun1.SelectedIndex == 1)
                {
                    //pictureBoxNowSelect.Image = null;
                    e.Graphics.DrawLine(_deletPen, _mapMouse.X + 2, _mapMouse.Y + 2, _mapMouse.X + _PieceDlg._TileMousePointW, _mapMouse.Y + _PieceDlg._TileMousePointH);

                    e.Graphics.DrawRectangle(_whilePen, _mapMouse.X, _mapMouse.Y, _PieceDlg._TileMousePointW, _PieceDlg._TileMousePointH);
                    e.Graphics.DrawRectangle(_blackPen, _mapMouse.X + 1, _mapMouse.Y + 1, _PieceDlg._TileMousePointW, _PieceDlg._TileMousePointH);
                    e.Graphics.DrawRectangle(_whilePen, _mapMouse.X + 2, _mapMouse.Y + 2, _PieceDlg._TileMousePointW, _PieceDlg._TileMousePointH);

                }
                else if (comboBoxFun1.SelectedIndex == 2)
                {
                    e.Graphics.FillRectangle(_blockBrush1, _mapMouse.X, _mapMouse.Y, _PieceDlg._TileMousePointW, _PieceDlg._TileMousePointH);
                    //e.Graphics.FillRectangle(Brushes.DeepPink, _mapMouse.X, _mapMouse.Y, _titleMousePointW, _titleMousePointH);
                }
                else if (comboBoxFun1.SelectedIndex == 3)
                {
                    e.Graphics.FillRectangle(_blockBrush2, _mapMouse.X, _mapMouse.Y, _PieceDlg._TileMousePointW, _PieceDlg._TileMousePointH);
                    //e.Graphics.FillRectangle(Brushes.Gainsboro, _mapMouse.X, _mapMouse.Y, _titleMousePointW, _titleMousePointH);
                }
            }

            if (comboBoxFun1.SelectedIndex == 0 || comboBoxFun1.SelectedIndex == 4)
            {
                if (pictureBoxNowSelect.Image != null)
                {
                    e.Graphics.DrawImage(pictureBoxNowSelect.Image, _mapMouse.X, _mapMouse.Y);
                    if (_mapInfos._IsLoadMonstor)
                    {
                        e.Graphics.FillRectangle(_blockBrush1, _mapMouse.X, _mapMouse.Y,
                        2 * GetMapTitleWidthInScale(), 2 * GetMapTitleHeightInScale());
                    }

                    if (_mapInfos._IsClickAnimation)
                    {
                        e.Graphics.FillRectangle(_blockBrush_Green, _mapMouse.X, _mapMouse.Y,
                                        _mapInfos.Map_Tile_Width, _mapInfos.Map_Tile_Height);
                    }

                    e.Graphics.DrawRectangle(_whilePen, _mapMouse.X, _mapMouse.Y, _nowSelectPiece.Width, _nowSelectPiece.Height);
                    e.Graphics.DrawRectangle(_blackPen, _mapMouse.X + 1, _mapMouse.Y + 1, _nowSelectPiece.Width - 2, _nowSelectPiece.Height - 2);
                    e.Graphics.DrawRectangle(_whilePen, _mapMouse.X + 2, _mapMouse.Y + 2, _nowSelectPiece.Width - 4, _nowSelectPiece.Height - 4);
                }
            }

            if (_showMeshGrid)
            {
                // 网格的列数
                int meshcol = _mapInfos.Map_Size_Height - mapVScrollBar.Value;
                // 网格的行数
                int meshrow = _mapInfos.Map_Size_Width - mapHScrollBar.Value;

                //                 int drawRow = 0;
                //                 int drawCol = 0;

                int _colcount = 0;// _wcount;
                int _rowcount = 0;
                for (int x = 0; x < meshrow; x++)
                {
                    for (int y = 0; y < meshcol; y++)
                    {
                        if (_colcount > _hcount)
                        {
                            break;
                        }

                        e.Graphics.DrawRectangle(_penMesh, x * _mapInfos.Map_Tile_Width, y * _mapInfos.Map_Tile_Height, _mapInfos.Map_Tile_Width, _mapInfos.Map_Tile_Height);

                        _colcount++;
                    }

                    if (_rowcount > _wcount)
                    {
                        break;
                    }

                    _rowcount++;
                    _colcount = 0;
                }

                //                     // 画水平线
                //                     for (int i = 0; i <= meshrow; i++)
                //                     {
                //                         e.Graphics.DrawLine(_penMesh, 0, drawCol, meshrow * _mapInfos.Map_Title_Width, drawCol);
                //                         drawCol += _mapInfos.Map_Title_Width;
                //                     }
                //                 // 画垂直线
                //                 for (int j = 0; j <= meshcol; j++)
                //                 {
                //                     e.Graphics.DrawLine(_penMesh, drawRow, 0, drawRow, meshcol * _mapInfos.Map_Title_Height);
                //                     drawRow += _mapInfos.Map_Title_Height;
                //                 }
            }
        }

        public int GetMapTitleWidthInScale()
        {
            return (int)(_mapInfos.Map_Tile_Width * _MapTileScale);
        }

        public int GetMapTitleHeightInScale()
        {
            return (int)(_mapInfos.Map_Tile_Height * _MapTileScale);
        }

        private void panelMap_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < panelMap.Width / GetMapTitleWidthInScale(); ++i)
            {
                for (int j = 0; j < panelMap.Height / GetMapTitleHeightInScale(); ++j)
                {
                    if (e.X >= i * GetMapTitleWidthInScale() && e.X < (i * GetMapTitleWidthInScale() + GetMapTitleWidthInScale()) &&
                        e.Y >= j * GetMapTitleHeightInScale() && e.Y < (j * GetMapTitleHeightInScale() + GetMapTitleHeightInScale()))
                    {
                        _mapMouse.X = i * _mapInfos.Map_Tile_Width;
                        _mapMouse.Y = j * _mapInfos.Map_Tile_Height;

                        break;
                    }
                }
            }

            DrawMapTitlePiece();
        }

        private void panelMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = _mapInfos._mapAnimationTile[comboBoxMapLayer.SelectedIndex,
                (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                    (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value];

            int id = -1;
            foreach (KeyValuePair<int, AnimationOffset> items in _mapInfos._mapAnimationOffsets)
            {
                if (items.Value._x == (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value
                    && items.Value._y == (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value)
                {
                    id = items.Key;
                    break;
                }
            }

            //Console.WriteLine("double X:" + (_mapMouse.X / _mapInfos.Map_Title_Width) + mapHScrollBar.Value
            //    + " Y:" + (_mapMouse.Y / _mapInfos.Map_Title_Height) + mapVScrollBar.Value
            //    + " layer:" + comboBoxMapLayer.SelectedIndex);

            if (i != -1) 
            {
                _selectedAnimationKey = new AnimationOffset(comboBoxMapLayer.SelectedIndex,
                    (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                _mapMouse.Y / _mapInfos.Map_Tile_Height + mapVScrollBar.Value,-1);

                if (_mapInfos._mapAnimationOffsets.ContainsKey(id)) 
                {
                    _offset = new tyoEngineOffset(_mapInfos._mapAnimationOffsets[id]._offsetX, 
                        _mapInfos._mapAnimationOffsets[id]._offsetY);
                }
                else 
                {
                    _offset = new tyoEngineOffset();
                }
                
                _offset.ShowDialog();
            }
        }

        bool _IsMapPanelLBtDown = false;


        float _MapTileScale = 1.0f;
        /*        float _MapScale = 1.0f;*/

        private void DrawMapTitlePiece()
        {

            if (_PieceDlg == null)
            {
                return;
            }

            if (_IsMapPanelLBtDown)
            {
                if (comboBoxFun1.SelectedIndex == 0) //绘制
                {
                    int _count = 0;

                    _mapInfos.ActionBegin(false, false);

                    for (int x = 0; x < (_PieceDlg._TileMousePointW / _mapInfos.Map_Tile_Width); ++x)
                    {
                        for (int y = 0; y < (_PieceDlg._TileMousePointH / _mapInfos.Map_Tile_Height); ++y)
                        {
                            if (_count >= _nowSelectPieceIndexByMap.Count)
                            {
                                continue;
                            }

                            _mapInfos.SetMapTile(comboBoxMapLayer.SelectedIndex,
                                (_mapMouse.X / _mapInfos.Map_Tile_Width) + x + mapHScrollBar.Value,
                                (_mapMouse.Y / _mapInfos.Map_Tile_Height) + y + mapVScrollBar.Value,
                                _nowSelectPieceIndexByMap[_count]._type);

                            //     _mapInfos.SetMapAnimationTilte(comboBoxMapLayer.SelectedIndex,
                            //(_mapMouse.X / _mapInfos.Map_Title_Width) + mapHScrollBar.Value,
                            //(_mapMouse.Y / _mapInfos.Map_Title_Height) + mapVScrollBar.Value,
                            //_nowAnimationKey);

                            _count++;
                        }
                    }

                    _mapInfos.ActionEnd();

                    //绘制怪物
                    //_mapInfos.ActionBegin(true, false);

                    //for (int x = 0; x < 2; ++x)
                    //{
                    //    for (int y = 0; y < 2; ++y)
                    //    {
                    //        _mapInfos.SetMapTilteBlock((_mapMouse.X / _mapInfos.Map_Title_Width) + x + mapHScrollBar.Value,
                    //            (_mapMouse.Y / _mapInfos.Map_Title_Height) + y + mapVScrollBar.Value, true);
                    //    }
                    //}

                    //_mapInfos.ActionEnd();
                }
                else if (comboBoxFun1.SelectedIndex == 1) //清楚
                {
                    _mapInfos.ActionBegin(false, true);

                    for (int x = 0; x < (_PieceDlg._TileMousePointW / _mapInfos.Map_Tile_Width); ++x)
                    {
                        for (int y = 0; y < (_PieceDlg._TileMousePointH / _mapInfos.Map_Tile_Height); ++y)
                        {
                            _mapInfos.SetMapTile(comboBoxMapLayer.SelectedIndex, (_mapMouse.X / _mapInfos.Map_Tile_Width) + x + mapHScrollBar.Value, (_mapMouse.Y / _mapInfos.Map_Tile_Height) + y + mapVScrollBar.Value, -1);
                        }
                    }

                    _mapInfos.ActionEnd();
                }
                else if (comboBoxFun1.SelectedIndex == 2) //设置阻挡
                {
                    _mapInfos.ActionBegin(true, false);

                    for (int x = 0; x < (_PieceDlg._TileMousePointW / _mapInfos.Map_Tile_Width); ++x)
                    {
                        for (int y = 0; y < (_PieceDlg._TileMousePointH / _mapInfos.Map_Tile_Height); ++y)
                        {
                            _mapInfos.SetMapTileBlock((_mapMouse.X / _mapInfos.Map_Tile_Width) + x + mapHScrollBar.Value, (_mapMouse.Y / _mapInfos.Map_Tile_Height) + y + mapVScrollBar.Value, true);
                        }
                    }

                    _mapInfos.ActionEnd();
                }
                else if (comboBoxFun1.SelectedIndex == 3) //清除阻挡
                {
                    _mapInfos.ActionBegin(true, false);

                    for (int x = 0; x < (_PieceDlg._TileMousePointW / _mapInfos.Map_Tile_Width); ++x)
                    {
                        for (int y = 0; y < (_PieceDlg._TileMousePointH / _mapInfos.Map_Tile_Height); ++y)
                        {
                            _mapInfos.SetMapTileBlock((_mapMouse.X / _mapInfos.Map_Tile_Width) + x + mapHScrollBar.Value, (_mapMouse.Y / _mapInfos.Map_Tile_Height) + y + mapVScrollBar.Value, false);
                        }
                    }

                    _mapInfos.ActionEnd();
                }
                else if (comboBoxFun1.SelectedIndex == 4) //填充图块
                {
                    //int _count = 0;

                    _mapInfos.ActionBegin(false, false);

                    int startmousex = (_mapMouse.X / _mapInfos.Map_Tile_Width);
                    int startmousey = (_mapMouse.Y / _mapInfos.Map_Tile_Height);

                    _FillMapPosList.Clear();
                    _FillMapPieceCallCount = 0;

                    FillMapPiece(startmousex, startmousey, 0, 0, 0);

                    comboBoxFun1.SelectedIndex = 0;

                    _mapInfos.ActionEnd();
                    //MessageBox.Show(_FillMapPieceCallCount.ToString());
                }

            }
        }

        class FillMapPos
        {
            public Point _pos = new Point(0, 0);
            public Point _piece = new Point(0, 0);
            public int _direction = 0;
        }


        List<FillMapPos> _FillMapPosList = new List<FillMapPos>();

        //检测是否查询过的点
        private bool GetIsFillFindPos(int x, int y)
        {
            for (int i = 0; i < _FillMapPosList.Count; ++i)
            {
                if (_FillMapPosList[i]._pos.X == x && _FillMapPosList[i]._pos.Y == y)
                {
                    return true;
                }
            }

            return false;
        }

        int _FillMapPieceCallCount = 0;

        private void AddFillMapPiecePos()
        {
            while (true)
            {
                bool whileFlag = false;

                for (int i = 0; i < _FillMapPosList.Count; ++i)
                {
                    int index = GetNowSelectPieceType(_FillMapPosList[i]._piece.X, _FillMapPosList[i]._piece.Y, _FillMapPosList[i]._direction);

                    if (index == -1)
                    {
                        continue;
                    }

                    if ((_FillMapPosList[i]._pos.X + 1) < _mapInfos.Map_Size_Width)
                    {
                        if (GetIsFillFindPos(_FillMapPosList[i]._pos.X + 1, _FillMapPosList[i]._pos.Y) == false)
                        {
                            if (_mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X + 1, _FillMapPosList[i]._pos.Y) != -2 &&
                            _mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X + 1, _FillMapPosList[i]._pos.Y) < 0)
                            {
                                FillMapPos fillMapPos = new FillMapPos();

                                fillMapPos._pos.X = _FillMapPosList[i]._pos.X + 1;
                                fillMapPos._pos.Y = _FillMapPosList[i]._pos.Y;
                                fillMapPos._piece.X = _nowSelectPieceIndexByMap[index]._x;
                                fillMapPos._piece.Y = _nowSelectPieceIndexByMap[index]._y;
                                fillMapPos._direction = 1;

                                _FillMapPosList.Add(fillMapPos);

                                whileFlag = true;
                            }
                        }
                    }

                    if ((_FillMapPosList[i]._pos.X - 1) >= 0)
                    {
                        if (GetIsFillFindPos(_FillMapPosList[i]._pos.X - 1, _FillMapPosList[i]._pos.Y) == false)
                        {
                            if (_mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X - 1, _FillMapPosList[i]._pos.Y) != -2 &&
                            _mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X - 1, _FillMapPosList[i]._pos.Y) < 0)
                            {
                                FillMapPos fillMapPos = new FillMapPos();

                                fillMapPos._pos.X = _FillMapPosList[i]._pos.X - 1;
                                fillMapPos._pos.Y = _FillMapPosList[i]._pos.Y;
                                fillMapPos._piece.X = _nowSelectPieceIndexByMap[index]._x;
                                fillMapPos._piece.Y = _nowSelectPieceIndexByMap[index]._y;
                                fillMapPos._direction = 2;

                                _FillMapPosList.Add(fillMapPos);

                                whileFlag = true;
                            }
                        }
                    }

                    if ((_FillMapPosList[i]._pos.Y + 1) < _mapInfos.Map_Size_Height)
                    {
                        if (GetIsFillFindPos(_FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y + 1) == false)
                        {
                            if (_mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y + 1) != -2 &&
                            _mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y + 1) < 0)
                            {
                                FillMapPos fillMapPos = new FillMapPos();

                                fillMapPos._pos.X = _FillMapPosList[i]._pos.X;
                                fillMapPos._pos.Y = _FillMapPosList[i]._pos.Y + 1;
                                fillMapPos._piece.X = _nowSelectPieceIndexByMap[index]._x;
                                fillMapPos._piece.Y = _nowSelectPieceIndexByMap[index]._y;
                                fillMapPos._direction = 3;

                                _FillMapPosList.Add(fillMapPos);

                                whileFlag = true;
                            }
                        }
                    }

                    if ((_FillMapPosList[i]._pos.Y - 1) >= 0)
                    {
                        if (GetIsFillFindPos(_FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y - 1) == false)
                        {
                            if (_mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y - 1) != -2 &&
                            _mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y - 1) < 0)
                            {
                                FillMapPos fillMapPos = new FillMapPos();

                                fillMapPos._pos.X = _FillMapPosList[i]._pos.X;
                                fillMapPos._pos.Y = _FillMapPosList[i]._pos.Y - 1;
                                fillMapPos._piece.X = _nowSelectPieceIndexByMap[index]._x;
                                fillMapPos._piece.Y = _nowSelectPieceIndexByMap[index]._y;
                                fillMapPos._direction = 4;

                                _FillMapPosList.Add(fillMapPos);

                                whileFlag = true;
                            }
                        }
                    }
                }

                if (whileFlag == false)
                {
                    break;
                }
            }
        }

        private void FillMapPieceByPosList()
        {
            for (int i = 0; i < _FillMapPosList.Count; ++i)
            {
                int index = GetNowSelectPieceType(_FillMapPosList[i]._piece.X, _FillMapPosList[i]._piece.Y,
                    _FillMapPosList[i]._direction);

                if (index == -1)
                {
                    continue;
                }

                if (_mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y) != -2 &&
                        _mapInfos.GetMapTilePieceIndex(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y) < 0)
                {
                    _mapInfos.SetMapTile(comboBoxMapLayer.SelectedIndex, _FillMapPosList[i]._pos.X, _FillMapPosList[i]._pos.Y, _nowSelectPieceIndexByMap[index]._type);
                }
            }
        }

        private void FillMapPiece(int x, int y, int piecex, int piecey, int direction)
        {
            _FillMapPieceCallCount++;

            int index = GetNowSelectPieceType(piecex, piecey, direction);

            if (index == -1)
            {
                return;
            }

            FillMapPos fillMapPos = new FillMapPos();

            fillMapPos._pos.X = x;
            fillMapPos._pos.Y = y;
            fillMapPos._piece.X = piecex;
            fillMapPos._piece.Y = piecey;
            fillMapPos._direction = direction;

            _FillMapPosList.Add(fillMapPos);

            AddFillMapPiecePos();
            FillMapPieceByPosList();
        }

        private void panelMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsMapPanelLBtDown = true;
                this.panelMap.Focus();
                DrawAnimation(_nowAnimationKey);
                SetMonstorProperties();

                DrawMapTitlePiece();

            }
            else if (e.Button == MouseButtons.Right)
            {
                pictureBoxNowSelect.Image = null;
                _nowSelectPieceIndexByMap.Clear();
                _nowSelectPiece = null;
            }
        }

        private void DrawAnimation(int key)
        {
            if (comboBoxFun1.SelectedIndex == 0) //设置
            {
                if (pictureBoxNowSelect.Image != null && key >= 0)
                {
                    _mapInfos.ActionBegin(false, false);

                    _mapInfos.SetMapAnimationTile(comboBoxMapLayer.SelectedIndex,
                        (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                        (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value,
                        key);

                    //Console.WriteLine("draw X:" + (_mapMouse.X / _mapInfos.Map_Title_Width) + mapHScrollBar.Value
                    //+ " Y:" + (_mapMouse.Y / _mapInfos.Map_Title_Height) + mapVScrollBar.Value
                    //+ " layer:" + comboBoxMapLayer.SelectedIndex);
                    int id = _mapInfos._mapAnimationOffsets.Count + 1;
                    _mapInfos._mapAnimationOffsets.Add(id,
                        new AnimationOffset(comboBoxMapLayer.SelectedIndex, (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                            (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value, id));

                    _mapInfos.ActionEnd();

                    pictureBoxNowSelect.Image = null;
                    _nowSelectPieceIndexByMap.Clear();
                    _nowSelectPiece = null;

                }
            }
            else if (comboBoxFun1.SelectedIndex == 1) //清楚
            {
                _mapInfos.ActionBegin(false, true);

                int id = -1;
                foreach (KeyValuePair<int, AnimationOffset> items in _mapInfos._mapAnimationOffsets)
                {
                    if (items.Value._x == (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value
                        && items.Value._y == (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value)
                    {
                        id = items.Key;
                        break;
                    }
                }

                if (id >= 0)
                {
                    _mapInfos._mapAnimationOffsets.Remove(id);
                }

                _mapInfos.SetMapAnimationTile(comboBoxMapLayer.SelectedIndex,
                    (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                    (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value,
                    -1);

                _mapInfos.ActionEnd();

                pictureBoxNowSelect.Image = null;
                _nowSelectPieceIndexByMap.Clear();
                _nowSelectPiece = null;

            }


        }

        private void SetMonstorProperties()
        {
            if (_mapInfos._IsLoadMonstor && comboBoxFun1.SelectedIndex == 0 && _IsMapPanelLBtDown
                    && pictureBoxNowSelect.Image != null)
            {
                _mapInfos.ActionBegin(false, false);

                _mapInfos.SetMapTile(_mapInfos._mapLayerInfosByIndex.Count - 1,
                           (_mapMouse.X / _mapInfos.Map_Tile_Width) + mapHScrollBar.Value,
                           (_mapMouse.Y / _mapInfos.Map_Tile_Height) + mapVScrollBar.Value,
                           _mapInfos._mapTileUseInfo.Count - 1);


                _mapInfos.ActionEnd();

                return;
            }
        }

        private void comboBoxFun1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFun1.SelectedIndex == 2 || comboBoxFun1.SelectedIndex == 3)
            {
                comboBoxFun3.SelectedIndex = 2;
                comboBoxFun3.Enabled = false;
            }
            else if (comboBoxFun1.SelectedIndex == 5)
            {
                DialogResult dr = MessageBox.Show("是否要清空当前图层所有图块?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    _mapInfos.ActionBegin(false, true);

                    for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                    {
                        for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                        {
                            _mapInfos.SetMapTile(comboBoxMapLayer.SelectedIndex, x, y, -1);
                        }
                    }

                    _mapInfos.ActionEnd();

                    comboBoxFun1.SelectedIndex = 0;
                }
                else
                {
                    comboBoxFun1.SelectedIndex = 0;
                }

            }
            else
            {
                if (comboBoxFun3.Enabled == false)
                {
                    comboBoxFun3.Enabled = true;
                }
            }
        }

        private void panelMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsMapPanelLBtDown = false;
                if (_PreviewDlg != null)
                {
                    _PreviewDlg.DrawImageHandle();
                }
            }
        }

        private void comboBoxFun3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFun3.SelectedIndex == 0)
            {
                _showMeshGrid = true;
            }
            else if (comboBoxFun3.SelectedIndex == 1)
            {
                _showMeshGrid = false;
            }
            else if (comboBoxFun3.SelectedIndex == 2)
            {
                _showBlockGrid = true;
            }
            else if (comboBoxFun3.SelectedIndex == 3)
            {
                _showBlockGrid = false;
            }
            else if (comboBoxFun3.SelectedIndex == 4) //地图原始
            {
                _MapTileScale = 1.0f;
                UpdateMapScrollBar();
            }
            else if (comboBoxFun3.SelectedIndex == 5) //地图75%
            {
                _MapTileScale = 0.75f;
                UpdateMapScrollBar();
            }
            else if (comboBoxFun3.SelectedIndex == 6) //地图50%
            {
                _MapTileScale = 0.5f;
                UpdateMapScrollBar();
            }
            else if (comboBoxFun3.SelectedIndex == 7) //地图25%
            {
                _MapTileScale = 0.25f;
                UpdateMapScrollBar();
            }


        }

        private void checkedListBoxMapLayerShow_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                _mapInfos._LayerShowFlag[e.Index] = true;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                _mapInfos._LayerShowFlag[e.Index] = false;
            }
        }

        private void btOutputPNGFile_Click(object sender, EventArgs e)
        {

            //return;

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "地图预览图|*.png";

            dlg.ShowDialog();

            if (dlg.FileName.Length > 0)
            {
                Bitmap tmp = new Bitmap(_mapInfos.Map_Size_Width * _mapInfos.Map_Tile_Width, _mapInfos.Map_Size_Height * _mapInfos.Map_Tile_Height);

                Graphics tmpG = Graphics.FromImage(tmp);

                for (int i = 0; i < comboBoxMapLayer.Items.Count; ++i)
                {
                    for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                    {
                        for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                        {
                            int index = _mapInfos._mapTile[_mapLayerDepth[i]._index, x, y];

                            if (index != -1)
                            {
                                tmpG.DrawImageUnscaled(
                                    _mapInfos._mapTileUseInfo[index]._image._tile,
                                    x * _mapInfos.Map_Tile_Width,
                                    y * _mapInfos.Map_Tile_Height);
                            }
                        }
                    }
                }

                tmp.Save(dlg.FileName);
                tmpG.Dispose();
            }
        }

        private void checkedListBoxMapLayerShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxMapLayerShow.SelectedIndex >= 0 && comboBoxMapLayer.SelectedIndex != -1)
            {
                comboBoxMapLayer.SelectedIndex = checkedListBoxMapLayerShow.SelectedIndex;
            }
        }

        private void btMapSharp_Click(object sender, EventArgs e)
        {
            mapHScrollBar.Value = 0;
            mapVScrollBar.Value = 0;
        }

        private void SaveEditorDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "tyo Engine Map Data|*.json";
            saveDlg.ShowDialog();

            string _filePath = saveDlg.FileName;

            if (Path.GetExtension(_filePath).ToLower() == ".json")
            {
                SaveMapDataToEditor(_filePath);
                return;
            }

            if (_filePath != "")
            {
                MessageBox.Show("保存文件出错");
            }
        }

        private void SaveMapDataToEditor(string _path)
        {
            string _pDir = Path.GetDirectoryName(_path);

            _pDir += "\\" + _mapInfos.Name + (string.Format("_{0}-{1}-{2}_{3}-{4}-{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Second, DateTime.Now.Minute));
            _pDir += "\\";

            if (Directory.Exists(_pDir))
            {
                MessageBox.Show("你的地图名字已经存在，请备份你当前的地图或选择其他的文件夹存储！");
                return;
            }

            Directory.CreateDirectory(_pDir);

            string _pPath = _pDir + Path.GetFileName(_path);

            //FileStream fs = new FileStream(_pPath, FileMode.OpenOrCreate);

            //BinaryWriter binFile = new BinaryWriter(fs);

            //Newtonsoft
            MapDataJsonFile _jsonFile = new MapDataJsonFile();

            SaveMapName(_jsonFile);
            SaveMapTitleInfos(_jsonFile, _pPath);
            SaveMapSize(_jsonFile);
            SaveLayerShowFlag(_jsonFile);
            SaveLayerInfos(_jsonFile);
            SaveTileUseInfo(_jsonFile, _pPath);
            SaveMapAllData(_jsonFile);

            SaveUsedAnimationInfos(_jsonFile);
            SaveAnimationOffsets(_jsonFile);
            //SaveAnimationArray(binFile);
            //SaveUseAnimationInfo(binFile);
            SaveAnimationPNG(_pPath);
            SaveAnimationXML(_pPath);

            //binFile.Close();
            //fs.Close();

            string _jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_jsonFile);
            //MapDataJsonFile _t2 = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataJsonFile>(_jsonString);

            //_pPath = _pDir + Path.GetFileName(_path.Replace(".tmd",".json"));

            FileStream fs = new FileStream(_pPath, FileMode.OpenOrCreate);
            StreamWriter _writer = new StreamWriter(fs);
            _writer.Write(_jsonString);
            _writer.Close();
            fs.Close();

        }

        private void SaveAnimationArray(BinaryWriter _file)
        {
            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                    {
                        _file.Write(_mapInfos._mapAnimationTile[i, x, y]);
                    }
                }
            }
        }

        private void SaveUsedAnimationInfos(MapDataJsonFile _file)
        {
            //_file.Write(_mapInfos._mapAnimationUseInfo.Count);

            foreach (KeyValuePair<int, MapUseAnimationInfo> item in _mapInfos._mapAnimationUseInfo)
            {
                MapDataJsonFile.__AnimationUsedInfosJson _animationUsedInfos = new MapDataJsonFile.__AnimationUsedInfosJson();

                _animationUsedInfos.Index = item.Key;
                _animationUsedInfos.FilePath = item.Value.animationInfo.xmlPath;
                _animationUsedInfos.NodeName = item.Value.animationInfo.name;
                _animationUsedInfos.Direction = Animation.TransDirectionCN2EN(item.Value.animationInfo.direction);

                _file.AnimationUsedInfosList.Add(_animationUsedInfos);
            }
        }

        private void SaveUseAnimationInfo(BinaryWriter _file)
        {
            _file.Write(_mapInfos._mapAnimationUseInfo.Count);

            foreach (KeyValuePair<int, MapUseAnimationInfo> item in _mapInfos._mapAnimationUseInfo)
            {
                //多少帧
                _file.Write(item.Value.bitmapsLength);

                //放到地图上的宽
                _file.Write(item.Value.w);

                //放到地图上的高
                _file.Write(item.Value.h);

                for (int i = 0; i < item.Value.bitmapsLength; i++)
                {
                    //每一帧的宽
                    _file.Write(item.Value.animationImageInfos[i].w);

                    //每一帧的高
                    _file.Write(item.Value.animationImageInfos[i].h);

                    //帧在大图中的X坐标
                    _file.Write(item.Value.animationImageInfos[i].xClone);

                    //帧在大图中的Y坐标
                    _file.Write(item.Value.animationImageInfos[i].yClone);

                    //原图的宽
                    _file.Write(item.Value.animationImageInfos[i].originalW);

                    //原图的高
                    _file.Write(item.Value.animationImageInfos[i].originalH);

                    //原图的X坐标
                    _file.Write(item.Value.animationImageInfos[i].x);

                    //原图的Y坐标
                    _file.Write(item.Value.animationImageInfos[i].y);

                    //帧图名称
                    byte[] pathNames = System.Text.Encoding.Default.GetBytes(item.Value.animationImageInfos[i].path);
                    _file.Write(pathNames.Length);
                    _file.Write(pathNames, 0, pathNames.Length);
                }

                _file.Write(item.Key);
            }

        }

        private void SaveAnimationPNG(string _path)
        {
            DirectoryInfo d = Directory.GetParent(_path);
            string mapPNGPath = string.Empty;

            foreach (string s in _mapInfos.animationPNGpath)
            {
                mapPNGPath = s.Substring(s.LastIndexOf('\\'));
                if (!Directory.Exists(d.FullName + Parameters.PICFILE))
                {
                    Directory.CreateDirectory(d.FullName + Parameters.PICFILE);
                }
                if (File.Exists(d.FullName + Parameters.PICFILE + mapPNGPath))
                {
                    File.Delete(d.FullName + Parameters.PICFILE + mapPNGPath);
                }
                System.IO.File.Copy(s, d.FullName + Parameters.PICFILE + mapPNGPath);
            }
        }

        private void SaveAnimationXML(string _path)
        {
            string fileName = string.Empty;
            DirectoryInfo dir = Directory.GetParent(_path);
            foreach (string s in _mapInfos.animationXML)
            {
                fileName = s.Substring(s.LastIndexOf('\\'));
                if (File.Exists(dir.FullName + fileName))
                {
                    File.Delete(dir.FullName + fileName);
                }
                System.IO.File.Copy(s, dir.FullName + fileName);
            }
        }

        //保存地图数据
        private void SaveMapAllData(MapDataJsonFile _file)
        {
            _file.InitMapTile();

            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                    {
                        _file.MapTile[i, x, y] = _mapInfos._mapTile[i, x, y];

                        if (i == 0)
                        {
                            _file.MapTileExternFlag[x, y] = _mapInfos._mapExternFlag1[x, y];
                            _file.MapBlockFlag[x, y] = _mapInfos._mapBlockFlag[x, y];
                        }

                        //if (_mapInfos._mapAnimationTitle == null)
//                         {
//                             //_mapInfos._mapAnimationTitle = new int[_mapInfos._mapLayerInfosByIndex.Count, _mapInfos.Map_Size_Width, _mapInfos.Map_Size_Height];
//                             for (int m = 0; m < _mapInfos._mapLayerInfosByIndex.Count; ++m)
//                             {
//                                 for (int n = 0; n < _mapInfos.Map_Size_Width; ++n)
//                                 {
//                                     for (int p = 0; p < _mapInfos.Map_Size_Height; ++p)
//                                     {
//                                         //_mapInfos._mapAnimationTitle[m, n, p] = -1;
//                                         _file.MapAnimationTitle[m, n, p] = -1;
//                                     }
//                                 }
//                             }
//                         }

                        //_file.Write(_mapInfos._mapAnimationTitle[i, x, y]);
                        _file.MapAnimationTile[i, x, y] = _mapInfos._mapAnimationTile[i, x, y];
                    }
                }
            }
        }

        private void SaveTileUseInfo(MapDataJsonFile _file, string _path)
        {
            //_file.Write(_mapInfos._mapTitleUseInfo.Count);

            int _ttmCount = 0;

            for (int i = 0; i < _mapInfos._mapTileUseInfo.Count; ++i)
            {
                MapDataJsonFile.__MapUsedTileInfosJson _usedInfo = new MapDataJsonFile.__MapUsedTileInfosJson();

                //使用的图块名字
                _usedInfo.Name = _mapInfos._mapTileUseInfo[i]._name;

                //使用的图块id
                _usedInfo.TileID = _mapInfos._mapTileUseInfo[i]._id;

                //在combox中的index
                _usedInfo.ComboxIndex = _mapInfos._mapTileUseInfo[i]._comboIndex;

                //是否使用过
                _usedInfo.UsedFlag = _mapInfos._mapTileUseInfo[i]._flag;

                //title x y w h信息
                _usedInfo.TileX = _mapInfos._mapTileUseInfo[i]._image._x;
                _usedInfo.TileY = _mapInfos._mapTileUseInfo[i]._image._y;
                _usedInfo.TileW = _mapInfos._mapTileUseInfo[i]._image._w;
                _usedInfo.TileH = _mapInfos._mapTileUseInfo[i]._image._h;

                //使用的图块缓存
//                 _ttmCount++;
// 
//                 Image _tmp = _mapInfos._mapTitleUseInfo[i]._image._title;
// 
//                 string _fileName = Path.GetFileName(_path);
//                 string _imgPath = Path.GetDirectoryName(_path);
//                 Directory.CreateDirectory(_imgPath + "\\tmpfile\\");
//                 _imgPath = _imgPath + "\\tmpfile\\" + _fileName + "_" + _ttmCount.ToString() + ".png";
// 
//                 _tmp.Save(_imgPath);

                //_imgPath = "tmpfile\\" + _ttmCount.ToString() + ".png";
                // 
                //                 byte[] bytesNames2 = System.Text.Encoding.Default.GetBytes(_imgPath);
                // 
                //                 _file.Write(bytesNames2.Length);
                //                 _file.Write(bytesNames2, 0, bytesNames2.Length);

                _file.MapUsedTileInfosList.Add(_usedInfo);
            }
        }

        private void SaveLayerInfos(MapDataJsonFile _file)
        {
            foreach (int index in _mapInfos._mapLayerInfosByIndex.Keys)
            {
                MapDataJsonFile.__MapLayerInfosJson _layerInfo = new MapDataJsonFile.__MapLayerInfosJson();
                //写 index
                _layerInfo.Index = index;

                //写地图图层名字
                _layerInfo.Name = _mapInfos._mapLayerInfosByIndex[index].Name;

                //写地图图层顺序
                _layerInfo.Depth = _mapInfos._mapLayerInfosByIndex[index].Depth;

                _file.MapLayerInfosList.Add(_layerInfo);
            }
        }

        private void SaveLayerShowFlag(MapDataJsonFile _file)
        {
            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                _file.MapLayerShowFlagList.Add(_mapInfos._LayerShowFlag[i]);
            }
        }

        private void SaveMapSize(MapDataJsonFile _file)
        {
            _file.MapSizeWidth = _mapInfos.Map_Size_Width;
            _file.MapSizeHeight = _mapInfos.Map_Size_Height;
            _file.MapTileWidth = _mapInfos.Map_Tile_Width;
            _file.MapTileHeight = _mapInfos.Map_Tile_Height;
        }

        private void SaveMapName(MapDataJsonFile _file)
        {
            _file.MapName = _mapInfos.Name;
        }

        //写地图 图块信息 并复制图块
        private void SaveMapTitleInfos(MapDataJsonFile _file, string _path)
        {
            //写总数
            //_file.Write(_mapInfos._mapTitleInfosByIndex.Count);
            string tempDir = string.Empty;
            foreach (int index in _mapInfos._mapTileInfosByIndex.Keys)
            {
                MapDataJsonFile.__MapTileInfosJson _titleInfos = new MapDataJsonFile.__MapTileInfosJson();
                //写index
                _titleInfos.Index = index;

                //写名字
                _titleInfos.Name = _mapInfos._mapTileInfosByIndex[index].Name;

                //写文件名字
                string _directory = _mapInfos._mapTileInfosByIndex[index].dirname + "\\" + _mapInfos._mapTileInfosByIndex[index]._filename;
                if (_mapInfos._mapTileInfosByIndex[index]._filename.StartsWith(_mapInfos._mapTileInfosByIndex[index].dirname))
                {
                    _directory = _mapInfos._mapTileInfosByIndex[index]._filename;
                }

                _titleInfos.Directory = _directory;

                _file.MapTileInfosList.Add(_titleInfos);

                Image _tmp = Image.FromFile(_mapInfos._mapTileInfosByIndex[index]._filepath);

                String _imgPath = Path.GetDirectoryName(_path);
                string dir = _imgPath + "\\" + _mapInfos._mapTileInfosByIndex[index].dirname;

                tempDir = dir.Substring(dir.LastIndexOf("\\") + 1, dir.Length - dir.LastIndexOf("\\") - 1);
                if (!tempDir.Equals(_mapInfos._mapTileInfosByIndex[index].dirname))
                {
                    dir = _imgPath + "\\" + _mapInfos._mapTileInfosByIndex[index].dirname;
                }

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                tempDir = _mapInfos._mapTileInfosByIndex[index]._filename.Substring(
                    _mapInfos._mapTileInfosByIndex[index]._filename.LastIndexOf("\\") + 1,
                    _mapInfos._mapTileInfosByIndex[index]._filename.Length -
                    _mapInfos._mapTileInfosByIndex[index]._filename.LastIndexOf("\\") - 1);
                _imgPath = dir + "\\" + tempDir;

                _tmp.Save(_imgPath);
            }
        }

        private void UpdateMapScrollBar()
        {
            _mapWidthByPixel = _mapInfos.Map_Size_Width * GetMapTitleWidthInScale();

            if (_mapWidthByPixel > panelMap.Width)
            {
                int grsize = _mapWidthByPixel - panelMap.Width;

                _mapHBarCount = grsize / GetMapTitleWidthInScale() + 1;

                if (_mapHBarCount == 0)
                {
                    _mapHBarCount = 1;
                }

                mapHScrollBar.Minimum = 0;
                mapHScrollBar.Maximum = _mapHBarCount;
            }
            else
            {
                _mapHBarCount = 0;
                mapHScrollBar.Maximum = _mapHBarCount;
            }

            _mapHeightByPixel = _mapInfos.Map_Size_Height * GetMapTitleHeightInScale();

            if (_mapHeightByPixel > panelMap.Height)
            {
                int grsize = _mapHeightByPixel - panelMap.Height;

                _mapVBarCount = grsize / GetMapTitleHeightInScale() + 1;

                if (_mapVBarCount == 0)
                {
                    _mapVBarCount = 1;
                }

                mapVScrollBar.Minimum = 0;
                mapVScrollBar.Maximum = _mapVBarCount;
            }
            else
            {
                _mapVBarCount = 0;
                mapVScrollBar.Maximum = _mapVBarCount;
            }
        }

        private void tyoEngineTitleMapEditWindow_ResizeEnd(object sender, EventArgs e)
        {
            int _w = this.Size.Width;
            int _h = this.Size.Height;

            if (_w < 820 && _h < 720)
            {
                this.Size = new Size(820, 720);
                //return;
            }
            else
            {
                if (_w < 820)
                {
                    this.Size = new Size(820, _h);
                }

                if (_h < 720)
                {
                    this.Size = new Size(_w, 720);
                }
            }


            _w = this.Size.Width;
            _h = this.Size.Height;

            int _wo = _w - _WindowSize.Width;
            int _ho = _h - _WindowSize.Height;

            panelMap.Size = new Size(panelMap.Size.Width + _wo, panelMap.Size.Height + _ho);
            groupBox2.Size = new Size(groupBox2.Size.Width + _wo, groupBox2.Size.Height + _ho);
            mapHScrollBar.Location = new Point(mapHScrollBar.Location.X /*+ _wo*/, mapHScrollBar.Location.Y + _ho);
            mapVScrollBar.Location = new Point(mapVScrollBar.Location.X + _wo, mapVScrollBar.Location.Y/* + _ho*/);
            btMapSharp.Location = new Point(btMapSharp.Location.X + _wo, btMapSharp.Location.Y + _ho);
            mapVScrollBar.Size = new Size(mapVScrollBar.Width, panelMap.Height);
            mapHScrollBar.Size = new Size(panelMap.Width, mapHScrollBar.Height);

            _WindowSize.Width = this.Size.Width;
            _WindowSize.Height = this.Size.Height;

            UpdateMapScrollBar();
        }

        bool _TitleDlgIsShow = false;

        private void btShowMapTitleDlg_Click(object sender, EventArgs e)
        {
            if (_PieceDlg == null)
            {
                _PieceDlg = new tyoEngineTileMapEditTilePieceWin();
                _PieceDlg.InitData(this);
            }

            _mapInfos._IsClickAnimation = false;

            if (_TitleDlgIsShow)
            {
                _PieceDlg.Hide();
            }
            else
            {
                _PieceDlg.Show();
            }

            _TitleDlgIsShow = !_TitleDlgIsShow;
        }

        private void tyoEngineTitleMapEditWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PieceDlg != null)
            {
                _PieceDlg.Hide();
                _PieceDlg = null;
            }

            if (_PreviewDlg != null)
            {
                _PreviewDlg.Close();
            }

            this.Hide();

            e.Cancel = true;
        }

        private void SaveMapDataToGame(String _path)
        {
            String _pDir = Path.GetDirectoryName(_path);

            _pDir += "\\" + _mapInfos.Name + (string.Format("_{0}-{1}-{2}_{3}-{4}-{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Second, DateTime.Now.Minute));
            _pDir += "\\";

            if (Directory.Exists(_pDir))
            {
                MessageBox.Show("你的地图名字已经存在，请备份你当前的地图或选择其他的文件夹存储！");
                return;
            }

            Directory.CreateDirectory(_pDir);

            String _pPath = _pDir + Path.GetFileName(_path);

            //FileStream fs = new FileStream(_pPath, FileMode.OpenOrCreate);

            //BinaryWriter binFile = new BinaryWriter(fs);

            //Newtonsoft
            MapDataJsonFile _jsonFile = new MapDataJsonFile();

            SaveMapName(_jsonFile);
            SaveMapTitleInfos(_jsonFile, _pPath);
            SaveMapSize(_jsonFile);
            SaveLayerShowFlag(_jsonFile);
            SaveLayerInfos(_jsonFile);
            SaveTileUseInfo(_jsonFile, _pPath);
            SaveMapAllData(_jsonFile);

            SaveUsedAnimationInfos(_jsonFile);
            SaveAnimationOffsets(_jsonFile);
            //SaveAnimationArray(binFile);
            //SaveUseAnimationInfo(binFile);
            SaveAnimationPNG(_pPath);
            SaveAnimationXML(_pPath);

            //binFile.Close();
            //fs.Close();

            string _jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_jsonFile);
            //MapDataJsonFile _t2 = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataJsonFile>(_jsonString);

            //_pPath = _pDir + Path.GetFileName(_path.Replace(".tmd",".json"));

            FileStream fs = new FileStream(_pPath, FileMode.OpenOrCreate);
            BinaryWriter binFile = new BinaryWriter(fs);

            byte[] _bytelist = System.Text.Encoding.Default.GetBytes(_jsonString);
            string _b64string = Convert.ToBase64String(_bytelist);
            _b64string = _b64string.Replace('=', '$');
            _b64string = _b64string.Replace('0', '#');
            _b64string = _b64string.Replace('1', ')');
            _b64string = _b64string.Replace('2', '(');
            _b64string = _b64string.Replace('3', '@');
            _b64string = _b64string.Replace('4', '%');
            _b64string = _b64string.Replace('5', '&');
            _b64string = _b64string.Replace('6', '{');
            _b64string = _b64string.Replace('7', '}');
            _b64string = _b64string.Replace('8', '[');
            _b64string = _b64string.Replace('9', ']');

            byte[] _bytelistSave = System.Text.Encoding.Default.GetBytes(_b64string);
            binFile.Write(_bytelistSave.Length);
            binFile.Write(_bytelistSave);

            binFile.Close();
            fs.Close();
        }

        #region 优化图层效率（暂定）

        private void SaveMapBlockData(BinaryWriter _file)
        {
            List<AllDataModel> externList = GetBlockOrExternList("Extern");

            _file.Write(externList.Count);

            foreach (AllDataModel item in externList)
            {
                _file.Write(item.x);
                _file.Write(item.y);
                _file.Write(item.data);
            }

            List<AllDataModel> blockList = GetBlockOrExternList("Block");

            _file.Write(blockList.Count);

            foreach (AllDataModel item in blockList)
            {
                _file.Write(item.x);
                _file.Write(item.y);
            }
        }

        private List<AllDataModel> GetBlockOrExternList(string type)
        {
            List<AllDataModel> all = new List<AllDataModel>();
            AllDataModel a;
            for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
            {
                for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                {
                    if (type.Equals("Extern") && _mapInfos._mapExternFlag1[x, y] != 0)
                    {
                        a = new AllDataModel();
                        a.x = x;
                        a.y = y;
                        a.data = _mapInfos._mapExternFlag1[x, y];

                        all.Add(a);
                    }
                    if (type.Equals("Block") && _mapInfos._mapBlockFlag[x, y])
                    {
                        a = new AllDataModel();
                        a.x = x;
                        a.y = y;
                        all.Add(a);
                    }
                }
            }
            return all;
        }

        class AllDataModel
        {
            public int x;
            public int y;
            public int data;
        }

        private void SaveMapAllDataToGame(BinaryWriter _file)
        {
            List<int> layerHaveValue = GetLayerHaveValue();

            int layerIndex = 0;

            //图层层数
            _file.Write(layerHaveValue.Count);

            foreach (int item in layerHaveValue)
            {
                //写 index
                _file.Write(layerIndex);

                //写地图图层名字
                byte[] bytesNames = System.Text.Encoding.Default.GetBytes(_mapInfos._mapLayerInfosByIndex[item].Name);

                _file.Write(bytesNames.Length);
                _file.Write(bytesNames, 0, bytesNames.Length);

                //写地图图层顺序
                _file.Write(_mapInfos._mapLayerInfosByIndex[item].Depth);

                //每一层已绘制 图块 的数量
                List<AllDataModel> allDataMapTitle = GetAllDataList(item, _mapInfos._mapTile);

                _file.Write(allDataMapTitle.Count);
                foreach (AllDataModel alldataModel in allDataMapTitle)
                {
                    _file.Write(alldataModel.x);
                    _file.Write(alldataModel.y);
                    _file.Write(alldataModel.data);
                }

                //每一层已绘制 特效 的数量
                allDataMapTitle = GetAllDataList(item, _mapInfos._mapAnimationTile);

                _file.Write(allDataMapTitle.Count);
                foreach (AllDataModel alldataModel in allDataMapTitle)
                {
                    _file.Write(alldataModel.x);
                    _file.Write(alldataModel.y);
                    _file.Write(alldataModel.data);


                    foreach (AnimationOffset itemOffset in _mapInfos._mapAnimationOffsets.Values)
                    {
                        if (itemOffset._x == alldataModel.x && itemOffset._y == alldataModel.y) 
                        {
                            //偏移量X
                            _file.Write(itemOffset._offsetX);

                            //偏移量Y
                            _file.Write(itemOffset._offsetY);

                            break;
                        }

                    }
                }

                layerIndex++;
            }
        }

        private List<AllDataModel> GetAllDataList(int item, int[, ,] array)
        {
            List<AllDataModel> allDataList = new List<AllDataModel>();
            AllDataModel a;
            for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
            {
                for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                {
                    if (array[item, x, y] != -1)
                    {
                        a = new AllDataModel();
                        a.x = x;
                        a.y = y;
                        a.data = array[item, x, y];
                        allDataList.Add(a);
                    }
                }
            }
            return allDataList;
        }

        private List<int> GetLayerHaveValue()
        {
            List<int> layerHaveValue = new List<int>();
            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                if (_mapInfos._mapLayerInfosByIndex[i]._name.Equals("player")) 
                {
                    layerHaveValue.Add(i);
                    continue;
                }

                for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0; y < _mapInfos.Map_Size_Height; ++y)
                    {
                        if (_mapInfos._mapTile[i, x, y] != -1 || _mapInfos._mapAnimationTile[i, x, y] != -1)
                        {
                            layerHaveValue.Add(i);
                            break;
                        }
                    }
                    if (layerHaveValue.Contains(i))
                    {
                        break;
                    }
                }
            }
            return layerHaveValue;
        }

        #endregion

        private void SaveAnimationOffsets(MapDataJsonFile _file) 
        {
            foreach (AnimationOffset item in _mapInfos._mapAnimationOffsets.Values)
            {
                MapDataJsonFile.__AnimationOffsets _offset = new MapDataJsonFile.__AnimationOffsets();
                _offset.Index = item._id;
                _offset.X = item._x;
                _offset.Y = item._y;
                _offset.Layer = item._layer;
                _offset.OffsetX = item._offsetX;
                _offset.OffsetY = item._offsetY;

                _file.AnimationOffsetList.Add(_offset);
            }
        }

        private void btOutputMapDataFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "tyo Engine Game Map Data|*.tyomap";

            saveDlg.ShowDialog();

            String _filePath = saveDlg.FileName;

            if (Path.GetExtension(_filePath).ToLower() == ".tyomap")
            {
                SaveMapDataToGame(_filePath);
                return;
            }

            if (_filePath != "")
            {
                MessageBox.Show("保存文件出错");
            }
        }

        private void tyoEngineTitleMapEditWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Q | Keys.Alt))
            {
                comboBoxFun1.SelectedIndex = 1;
            }
            else if (e.KeyData == (Keys.W | Keys.Alt))
            {
                comboBoxFun1.SelectedIndex = 3;
            }
            else if (e.KeyData == (Keys.Z | Keys.Control))
            {
                _mapInfos.UnDo();
            }
            else if (e.KeyData == (Keys.Y | Keys.Control))
            {
                _mapInfos.Redo();
            }
            else if (e.KeyData == Keys.Q)
            {
                comboBoxFun1.SelectedIndex = 0;
            }
            else if (e.KeyData == Keys.W)
            {
                comboBoxFun1.SelectedIndex = 2;
            }
            else if (e.KeyData == Keys.D1)
            {
                if (comboBoxMapLayer.Items.Count >= 1)
                {
                    comboBoxMapLayer.SelectedIndex = 0;
                }
            }
            else if (e.KeyData == Keys.D2)
            {
                if (comboBoxMapLayer.Items.Count >= 2)
                {
                    comboBoxMapLayer.SelectedIndex = 1;
                }
            }
            else if (e.KeyData == Keys.D3)
            {
                if (comboBoxMapLayer.Items.Count >= 3)
                {
                    comboBoxMapLayer.SelectedIndex = 2;
                }
            }
            else if (e.KeyData == Keys.D4)
            {
                if (comboBoxMapLayer.Items.Count >= 4)
                {
                    comboBoxMapLayer.SelectedIndex = 3;
                }
            }
            else if (e.KeyData == Keys.D5)
            {
                if (comboBoxMapLayer.Items.Count >= 5)
                {
                    comboBoxMapLayer.SelectedIndex = 4;
                }
            }
            else if (e.KeyData == Keys.D6)
            {
                if (comboBoxMapLayer.Items.Count >= 6)
                {
                    comboBoxMapLayer.SelectedIndex = 5;
                }
            }
            else if (e.KeyData == Keys.D7)
            {
                if (comboBoxMapLayer.Items.Count >= 7)
                {
                    comboBoxMapLayer.SelectedIndex = 6;
                }
            }
            else if (e.KeyData == Keys.D8)
            {
                if (comboBoxMapLayer.Items.Count >= 8)
                {
                    comboBoxMapLayer.SelectedIndex = 7;
                }
            }
            else if (e.KeyData == Keys.D9)
            {
                if (comboBoxMapLayer.Items.Count >= 9)
                {
                    comboBoxMapLayer.SelectedIndex = 8;
                }
            }
            else if (e.KeyData == Keys.D0)
            {
                if (comboBoxMapLayer.Items.Count >= 10)
                {
                    comboBoxMapLayer.SelectedIndex = 9;
                }
            }
            //else if (e.KeyData == Keys.OemOpenBrackets)
            else if (e.KeyData == (Keys.Shift | Keys.D9))
            {
                if (_MapTileScale == 1.0f)
                {
                    comboBoxFun3.SelectedIndex = 5;
                }
                else if (_MapTileScale == 0.75f)
                {
                    comboBoxFun3.SelectedIndex = 6;
                }
                else if (_MapTileScale == 0.5f)
                {
                    comboBoxFun3.SelectedIndex = 7;
                }
            }
            //else if (e.KeyData == Keys.OemCloseBrackets)
            else if (e.KeyData == (Keys.Shift | Keys.D0))
            {
                if (_MapTileScale == 0.25f)
                {
                    comboBoxFun3.SelectedIndex = 6;
                }
                else if (_MapTileScale == 0.5f)
                {
                    comboBoxFun3.SelectedIndex = 5;
                }
                else if (_MapTileScale == 0.75f)
                {
                    comboBoxFun3.SelectedIndex = 4;
                }
            }

        }

        bool isPreviewShow = true;

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (_PreviewDlg == null)
            {
                _PreviewDlg = new tyoEngineTileMapPreview();
                _PreviewDlg.SetMapInfos(_mapInfos);

            }
            _mapInfos._IsClickAnimation = false;
            if (isPreviewShow)
            {
                _PreviewDlg.Show();
            }
            else
            {
                _PreviewDlg.Hide();
            }

            isPreviewShow = !isPreviewShow;
        }

        private void mapVScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void loadMonsterBtn_Click(object sender, EventArgs e)
        {

            /*
            //加一个图层放怪物和NPC
            bool b = _mapInfos._mapLayerInfosByIndex[_mapInfos._mapLayerInfosByIndex.Count - 1]._name
                .Equals(Parameters.SYSYTEMUSE);
            if (!b && !(_mapInfos._mapLayerInfosByIndex.Count).Equals(comboBoxMapLayer.Items.Count + 1))
            {
                MapLayerInfos layer = new MapLayerInfos();
                layer._name = Parameters.SYSYTEMUSE;
                layer._depth = _mapInfos._mapLayerInfosByIndex.Count;

                _mapInfos._mapLayerInfosByIndex[_mapInfos._mapLayerInfosByIndex.Count] = layer;

                //在_LayerShowFlag加一层，并可以显示
                bool[] tempBool = new bool[_mapInfos._LayerShowFlag.Length + 1];
                _mapInfos._LayerShowFlag.CopyTo(tempBool, 0);
                _mapInfos._LayerShowFlag = tempBool;
                _mapInfos._LayerShowFlag[_mapInfos._LayerShowFlag.Length - 1] = true;

                //在_mapLayerDepth加一层，并赋值到最高层
                MapLayerDrawInfos tmpLayerDepth = new MapLayerDrawInfos();
                tmpLayerDepth._depth = layer._depth;
                tmpLayerDepth._index = _mapLayerDepth[_mapLayerDepth.Count - 1]._index + 1;
                _mapLayerDepth.Add(tmpLayerDepth);

                //在_mapTitle加一层
                int[, ,] tempMapTitle = new int[_mapInfos._mapTitle.GetLength(0) + 1,
                    _mapInfos._mapTitle.GetLength(1), _mapInfos._mapTitle.GetLength(2)];
                Array.Copy(_mapInfos._mapTitle, tempMapTitle, _mapInfos._mapTitle.Length);
                _mapInfos._mapTitle = tempMapTitle;

                for (int x = 0; x < _mapInfos._mapTitle.GetLength(1); ++x)
                {
                    for (int y = 0; y < _mapInfos._mapTitle.GetLength(2); ++y)
                    {
                        _mapInfos._mapTitle[_mapInfos._mapTitle.GetLength(0) - 1, x, y] = -1;
                    }
                }

                //在_mapAnimationTitle加一层
                tempMapTitle = new int[_mapInfos._mapAnimationTitle.GetLength(0) + 1,
                    _mapInfos._mapAnimationTitle.GetLength(1), _mapInfos._mapAnimationTitle.GetLength(2)];
                Array.Copy(_mapInfos._mapAnimationTitle, tempMapTitle, _mapInfos._mapAnimationTitle.Length);
                _mapInfos._mapAnimationTitle = tempMapTitle;

                for (int x = 0; x < _mapInfos._mapAnimationTitle.GetLength(1); ++x)
                {
                    for (int y = 0; y < _mapInfos._mapAnimationTitle.GetLength(2); ++y)
                    {
                        _mapInfos._mapAnimationTitle[_mapInfos._mapTitle.GetLength(0) - 1, x, y] = -1;
                    }
                }
            }

            tyoEngineTitleMapMonster monsterForm = new tyoEngineTitleMapMonster(this, _mapInfos);
            monsterForm.Show();
             * */
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Parameters.FILTERXML;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tyoEngineAnimation tea = new tyoEngineAnimation(ofd.FileName, this);
                tea.Show();
            }
        }

        public void AddAnimationPiece(List<AnimationImageInfo> animationImageInfos, AnimationInfo animationInfo)
        //public void AddAnimationPiece(List<AnimationImageInfo> animationImageInfos, int w, int h)
        {
            MapUseAnimationInfo model = new MapUseAnimationInfo();

            model.w = animationImageInfos[0].onPanelBitmap.Width;
            model.h = animationImageInfos[0].onPanelBitmap.Height;

            model.bitmapsLength = animationImageInfos.Count;

            model.animationImageInfos = animationImageInfos;

            model.animationInfo = animationInfo;

            for (int i = 0; i < _mapInfos._mapAnimationUseInfo.Count; ++i)
            {
                if (_mapInfos._mapAnimationUseInfo[i].animationInfo.name == animationInfo.name &&
                    _mapInfos._mapAnimationUseInfo[i].animationInfo.direction == animationInfo.direction)
                {
                    _nowAnimationKey = i;
                    SetNowSelectPiece(animationImageInfos[0].onPanelBitmap);
                    return;
                }
            }

            _nowAnimationKey = _mapInfos._mapAnimationUseInfo.Count;

            _mapInfos._mapAnimationUseInfo.Add(_nowAnimationKey, model);

            SetNowSelectPiece(animationImageInfos[0].onPanelBitmap);
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _mapInfos._mapAnimationUseInfo.Count; i++)
            {
                _mapInfos._mapAnimationUseInfo[i].count =
                    _mapInfos._mapAnimationUseInfo[i].count == _mapInfos._mapAnimationUseInfo[i].bitmapsLength - 1 ?
                    0 : _mapInfos._mapAnimationUseInfo[i].count + 1;
            }

            panelMap.Refresh();
        }

        private void hSystemFPSBar_ValueChanged(object sender, EventArgs e)
        {
            timerAnimation.Stop();
            timerAnimation.Interval = 1000 / hSystemFPSBar.Value;
            timerAnimation.Start();
        }

        #region 编辑图层

        private void btEditLayer_Click(object sender, EventArgs e)
        {
            tyoEngineLayerEdit tele = new tyoEngineLayerEdit(_mapInfos);
            _mapInfos._IsClickAnimation = false;
            if (tele.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListBox listBox = tele.getListBox();
                #region 草稿
                //MapLayerInfos tmpMapLayerInfos;
                //for (int i = 1; i < listBox.Items.Count; i++)
                //{
                //    tmpMapLayerInfos = (MapLayerInfos)listBox.Items[i];
                //    for (int j = i - 1; j >= 0; j--)
                //    {
                //        if (((MapLayerInfos)listBox.Items[j]).Depth > tmpMapLayerInfos.Depth)
                //        {
                //            listBox.Items[j + 1] = listBox.Items[j];
                //            if (j == 0)
                //            {
                //                listBox.Items[0] = tmpMapLayerInfos;
                //                break;
                //            }
                //        }
                //        else
                //        {
                //            listBox.Items[j + 1] = tmpMapLayerInfos;
                //            break;
                //        }
                //    }
                //}

                //for (int i = 0; i < listBox.Items.Count; i++) 
                //{
                //    MapLayerInfos m = (MapLayerInfos)listBox.Items[i];
                //    List<MapLayerInfos> tmpList = new List<MapLayerInfos>();
                //    if (m.opType.Equals("ADD")) 
                //    {
                //        MapLayerInfos layer = new MapLayerInfos();
                //        layer._name = m.Name;
                //        layer._depth = m.Depth;

                //        _mapInfos._mapLayerInfosByIndex.Clear();
                //        for (int n = 0; n < listBox.Items.Count; n++)
                //        {
                //            _mapInfos._mapLayerInfosByIndex.Add(((MapLayerInfos)listBox.Items[n]).Depth, (MapLayerInfos)listBox.Items[n]);
                //        }

                //        #region 直接加了一层
                //        //在_LayerShowFlag加一层，并可以显示
                //        bool[] tempBool = new bool[_mapInfos._LayerShowFlag.Length + 1];
                //        _mapInfos._LayerShowFlag.CopyTo(tempBool, 0);
                //        _mapInfos._LayerShowFlag = tempBool;
                //        _mapInfos._LayerShowFlag[_mapInfos._LayerShowFlag.Length - 1] = true;

                //        //在_mapTitle加一层
                //        int[, ,] tempMapTitle = new int[_mapInfos._mapTitle.GetLength(0) + 1,
                //            _mapInfos._mapTitle.GetLength(1), _mapInfos._mapTitle.GetLength(2)];
                //        Array.Copy(_mapInfos._mapTitle, tempMapTitle, _mapInfos._mapTitle.Length);
                //        _mapInfos._mapTitle = tempMapTitle;

                //        for (int x = 0; x < _mapInfos._mapTitle.GetLength(1); ++x)
                //        {
                //            for (int y = 0; y < _mapInfos._mapTitle.GetLength(2); ++y)
                //            {
                //                _mapInfos._mapTitle[_mapInfos._mapTitle.GetLength(0) - 1, x, y] = -1;
                //            }
                //        }

                //        //在_mapAnimationTitle加一层
                //        tempMapTitle = new int[_mapInfos._mapAnimationTitle.GetLength(0) + 1,
                //            _mapInfos._mapAnimationTitle.GetLength(1), _mapInfos._mapAnimationTitle.GetLength(2)];
                //        Array.Copy(_mapInfos._mapAnimationTitle, tempMapTitle, _mapInfos._mapAnimationTitle.Length);
                //        _mapInfos._mapAnimationTitle = tempMapTitle;

                //        for (int x = 0; x < _mapInfos._mapAnimationTitle.GetLength(1); ++x)
                //        {
                //            for (int y = 0; y < _mapInfos._mapAnimationTitle.GetLength(2); ++y)
                //            {
                //                _mapInfos._mapAnimationTitle[_mapInfos._mapTitle.GetLength(0) - 1, x, y] = -1;
                //            }
                //        }
                //        #endregion

                //        //在_mapLayerDepth加一层，并将后面的depth加1
                //        for (int j = i; j < _mapLayerDepth.Count; j++) 
                //        {
                //            _mapLayerDepth[j]._depth = _mapLayerDepth[j]._depth + 1;
                //            _mapLayerDepth[j]._index = _mapLayerDepth[j]._index + 1;
                //        }
                //        MapLayerDrawInfos tmpLayerDepth = new MapLayerDrawInfos();
                //        tmpLayerDepth._depth = layer._depth;
                //        tmpLayerDepth._index = layer._depth;
                //        _mapLayerDepth.Insert(layer._depth, tmpLayerDepth);

                //        comboBoxMapLayer.Items.Insert(i, _mapInfos._mapLayerInfosByIndex[i].Name);
                //        checkedListBoxMapLayerShow.Items.Insert(i, _mapInfos._mapLayerInfosByIndex[i].Name);

                //        checkedListBoxMapLayerShow.SelectedIndex = i;

                //        checkedListBoxMapLayerShow.SetItemChecked(i, true);
                //    }
                //}
                #endregion

                ChagneLayer(listBox);
            }
        }

        private void ChagneLayer(ListBox listBox)
        {
            MapLayerInfos tmpMapLayerInfos;
            //先排序
            for (int i = 1; i < listBox.Items.Count; i++)
            {
                tmpMapLayerInfos = (MapLayerInfos)listBox.Items[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (((MapLayerInfos)listBox.Items[j]).Depth > tmpMapLayerInfos.Depth)
                    {
                        listBox.Items[j + 1] = listBox.Items[j];
                        if (j == 0)
                        {
                            listBox.Items[0] = tmpMapLayerInfos;
                            break;
                        }
                    }
                    else
                    {
                        listBox.Items[j + 1] = tmpMapLayerInfos;
                        break;
                    }
                }
            }

            ChangeLyaerMapTitle(listBox);

            ChangeLayerInfosByIndex(listBox);

            ChangeLayerShowFlageAndMapDepth(listBox);
        }

        private void ChangeLyaerMapTitle(ListBox listBox)
        {
            int layer = -1;
            int[, ,] tmpTitle = new int[listBox.Items.Count, _mapInfos._mapTile.GetLength(1), _mapInfos._mapTile.GetLength(2)];
            int[, ,] tmpAnimationTitle = null;
            if (_mapInfos._mapAnimationTile != null)
            {
                tmpAnimationTitle =
                    new int[listBox.Items.Count, _mapInfos._mapAnimationTile.GetLength(1), _mapInfos._mapAnimationTile.GetLength(2)];
            }


            for (int i = 0; i < listBox.Items.Count; i++)
            {
                layer = -1;
                foreach (KeyValuePair<int, MapLayerInfos> item in _mapInfos._mapLayerInfosByIndex)
                {
                    if (item.Value.Name == ((MapLayerInfos)listBox.Items[i]).Name)
                    {
                        layer = item.Key;
                        break;
                    }
                }

                if (layer == -1)
                {
                    for (int x = 0; x < _mapInfos.Map_Size_Width; x++)
                    {
                        for (int y = 0; y < _mapInfos.Map_Size_Height; y++)
                        {
                            tmpTitle[i, x, y] = -1;
                            if (_mapInfos._mapAnimationTile != null)
                            {
                                tmpAnimationTitle[i, x, y] = -1;
                            }

                        }
                    }
                }
                else
                {
                    for (int x = 0; x < _mapInfos.Map_Size_Width; x++)
                    {
                        for (int y = 0; y < _mapInfos.Map_Size_Height; y++)
                        {
                            tmpTitle[i, x, y] = _mapInfos._mapTile[layer, x, y];
                            if (_mapInfos._mapAnimationTile != null)
                            {
                                tmpAnimationTitle[i, x, y] = _mapInfos._mapAnimationTile[layer, x, y];
                            }

                        }
                    }
                }
            }
            _mapInfos._mapTile = tmpTitle;
            if (_mapInfos._mapAnimationTile != null)
            {
                _mapInfos._mapAnimationTile = tmpAnimationTitle;
            }

        }

        private void ChangeLayerInfosByIndex(ListBox listBox)
        {

            Dictionary<int, MapLayerInfos> tmpDic = new Dictionary<int, MapLayerInfos>();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                tmpDic.Add(i, (MapLayerInfos)listBox.Items[i]);
            }
            _mapInfos._mapLayerInfosByIndex = tmpDic;
        }

        private void ChangeLayerShowFlageAndMapDepth(ListBox listBox)
        {
            bool[] tempBool = new bool[listBox.Items.Count];
            for (int i = 0; i < tempBool.Length; i++)
            {
                tempBool[i] = true;
            }
            _mapInfos._LayerShowFlag = tempBool;

            _mapLayerDepth.Clear();
            comboBoxMapLayer.Items.Clear();
            checkedListBoxMapLayerShow.Items.Clear();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                MapLayerDrawInfos tmpLayerDepth = new MapLayerDrawInfos();
                tmpLayerDepth._depth = ((MapLayerInfos)listBox.Items[i])._depth;
                tmpLayerDepth._index = i;
                _mapLayerDepth.Add(tmpLayerDepth);

                comboBoxMapLayer.Items.Insert(i, _mapInfos._mapLayerInfosByIndex[i].Name);
                checkedListBoxMapLayerShow.Items.Insert(i, _mapInfos._mapLayerInfosByIndex[i].Name);

                checkedListBoxMapLayerShow.SetItemChecked(i, true);
            }
        }
        #endregion

    }
}