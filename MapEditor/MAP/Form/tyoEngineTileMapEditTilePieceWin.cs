using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace tyoEngineEditor
{
    public partial class tyoEngineTileMapEditTilePieceWin : Form
    {
        public tyoEngineTileMapEditTilePieceWin()
        {
            InitializeComponent();
        }

        tyoEngineTileMapEditWindow _MapEditWin = null;

        private const int MapTilePanelSizeW = 800;
        private const int MapTilePanelSizeH = 800;

        public void InitData( tyoEngineTileMapEditWindow _dlg )
        {
            _MapEditWin = _dlg;

            mapTitlePanel.Size = new Size(MapTilePanelSizeW, MapTilePanelSizeH);
            this.Size = new Size(830, 890);
        }

        public void UpdateTick()
        {
            mapTitlePanel.Refresh();
        }

        private void tyoEngineTitleMapEditTilePieceWin_Shown(object sender, EventArgs e)
        {
            UpdateTileCombox();
        }

        private void UpdateTileCombox()
        {
            this.comboxTitleDirSelect.Items.Clear();

            string currentPath = System.Environment.CurrentDirectory
                + "\\MAPResource\\Tiles";
            if (!_MapEditWin.GetMapInfos().loadPath.Equals(string.Empty)) 
            {
                currentPath = _MapEditWin.GetMapInfos().loadPath.Substring(0,
                    _MapEditWin.GetMapInfos().loadPath.LastIndexOf("\\"));
            }
            string[] dirs = Directory.GetDirectories(currentPath);

            string dirName = string.Empty;
            if (!this.comboxTitleDirSelect.Items.Contains("Tiles")) 
            {
                this.comboxTitleDirSelect.Items.Add("Tiles");
            }
            
            for (int i = 0; i < dirs.Length; i++) 
            {
                dirName = dirs[i]
                    .Substring(dirs[i].LastIndexOf("\\") + 1, 
                    dirs[i].Length - dirs[i].LastIndexOf("\\") - 1);
                if (!this.comboxTitleDirSelect.Items.Contains(dirName))
                {
                    this.comboxTitleDirSelect.Items.Add(dirName);
                }
            }

            this.comboxTitleDirSelect.SelectedIndex = 0;
        }

        int _maxTitleW = 0;
        int _maxTitleH = 0;
        float maxScale = 1.0f; 

        private void comboxTitleSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxTitleSelect.SelectedIndex >= 0)
            {
                MapTileInfos tempTitle = (MapTileInfos)comboxTitleSelect.SelectedItem;
                _MapEditWin.SetMapTileSelect(Image.FromFile(_MapEditWin.GetMapInfos()
                    ._mapTileInfosByIndex[tempTitle.index]._filepath));

                _MapEditWin.GetTileImageList().Clear();

                // 先切割图片，看看长宽可以分割成多少个快快。
                _maxTitleW = _MapEditWin.GetMapTileSelect().Width / _MapEditWin.GetMapInfos().Map_Tile_Width;
                _maxTitleH = _MapEditWin.GetMapTileSelect().Height / _MapEditWin.GetMapInfos().Map_Tile_Height;


                float _wScale = (float)MapTilePanelSizeW / (float)_MapEditWin.GetMapTileSelect().Width ;
                float _hScale = (float)MapTilePanelSizeH / (float)_MapEditWin.GetMapTileSelect().Height;

                if(_wScale < 1.0f || _hScale < 1.0f)
                {
                    if (_wScale < _hScale)
                    {
                        _TitleScale = _wScale;
                    }
                    else 
                    {
                        _TitleScale = _hScale;
                    }

                    maxScale = _TitleScale;
                }
               
                for (int y = 0; y < _maxTitleH; ++y)
                {
                    for (int x = 0; x < _maxTitleW; ++x)
                    {
                        Bitmap imgTile = new Bitmap(_MapEditWin.GetMapInfos().Map_Tile_Width, _MapEditWin.GetMapInfos().Map_Tile_Height);

                        Graphics imgGraphics = Graphics.FromImage(imgTile);

                        Rectangle destRect = new Rectangle(new Point(0, 0), new Size(_MapEditWin.GetMapInfos().Map_Tile_Width, _MapEditWin.GetMapInfos().Map_Tile_Height));//目标位置

                        Rectangle origRect = new Rectangle(
                            new Point(x * _MapEditWin.GetMapInfos().Map_Tile_Width, y * _MapEditWin.GetMapInfos().Map_Tile_Height),
                            new Size(_MapEditWin.GetMapInfos().Map_Tile_Width, _MapEditWin.GetMapInfos().Map_Tile_Height));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                        imgGraphics.DrawImage(_MapEditWin.GetMapTileSelect(), destRect, origRect, GraphicsUnit.Pixel);

                        tyoEngineTileMapEditWindow.MapTilePiece _piece = new tyoEngineTileMapEditWindow.MapTilePiece();
                        
                        _piece._x = origRect.X;
                        _piece._y = origRect.Y;
                        _piece._w = origRect.Width;
                        _piece._h = origRect.Height;

                        //_piece._titleinfo = _MapEditWin.GetMapInfos()._mapTitleInfosByIndex[comboxTitleSelect.SelectedIndex];
                        _piece._tile = imgTile;

                        _MapEditWin.GetTileImageList().Add(_piece);
                    }
                }
            }
        }

        private void mapTilePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.ScaleTransform(_TitleScale, _TitleScale);

            if (_MapEditWin.GetMapTileSelect() != null)
            {
                int __iw = _MapEditWin.GetMapTileSelect().Width;

                //e.Graphics.DrawImage(_MapEditWin.GetMapTitleSelect(), 0, 0);

                //int titlePanelPieceW = mapTitlePanel.Width / _MapEditWin.GetMapInfos().Map_Title_Width;
                //int titlePanelPieceH = mapTitlePanel.Height / _MapEditWin.GetMapInfos().Map_Title_Height;

                int drawx = 0;
                int drawy = 0;

                for (int x = 0; x < _maxTitleW; ++x)
                {
                    for (int y = 0; y < _maxTitleH; ++y)
                    {
//                         if (x > titlePanelPieceW || y > titlePanelPieceH)
//                         {
//                             continue;
//                         }

                        if ((y * _maxTitleW + x) >= _MapEditWin.GetTileImageList().Count)
                        {
                            continue;
                        }

                        e.Graphics.DrawImage(_MapEditWin.GetTileImageList()[y * _maxTitleW + x]._tile, 
                            drawx * _MapEditWin.GetMapInfos().Map_Tile_Width,
                            drawy * _MapEditWin.GetMapInfos().Map_Tile_Height);


                        drawy++;
                    }

                    drawx++;

                    drawy = 0;
                }
            }

            UpdateMouseRect();

            e.Graphics.DrawRectangle(_MapEditWin.GetWhilePen(), _TileMousePointX, _TileMousePointY, _TileMousePointW, _TileMousePointH);
            e.Graphics.DrawRectangle(_MapEditWin.GetBlackPen(), _TileMousePointX + 1, _TileMousePointY + 1, _TileMousePointW - 2, _TileMousePointH - 2);
            e.Graphics.DrawRectangle(_MapEditWin.GetWhilePen(), _TileMousePointX + 2, _TileMousePointY + 2, _TileMousePointW - 4, _TileMousePointH - 4);
        }

        private void mapTilePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int _mx = e.X;
                int _my = e.Y;

                _StartSelectTile = false;

                int x = mapTitlePanel.Width / GetTileWidthInScale();
                int y = mapTitlePanel.Height / GetTileHeightInScale();

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (_mx >= i * GetTileWidthInScale() && _mx < (i * GetTileWidthInScale() + GetTileWidthInScale()) &&
                            _my >= j * GetTileHeightInScale() && _my < (j * GetTileHeightInScale() + GetTileHeightInScale()))
                        {
                            _TileMousePointEnd.X = i * _MapEditWin.GetMapInfos().Map_Tile_Width;
                            _TileMousePointEnd.Y = j * _MapEditWin.GetMapInfos().Map_Tile_Height;

                            return;
                        }
                    }
                }
            }
        }

        float _TitleScale = 1.0f;

        private int GetTileWidthInScale()
        {
            return (int)(_MapEditWin.GetMapInfos().Map_Tile_Width * _TitleScale);
        }

        private int GetTileHeightInScale()
        {
            return (int)(_MapEditWin.GetMapInfos().Map_Tile_Height * _TitleScale);
        }

        bool _StartSelectTile = false;

        Point _TileMousePointStart = new Point();
        Point _TileMousePointEnd = new Point();

        public int _TileMousePointW = 0;
        public int _TileMousePointH = 0;
        public int _TileMousePointX = 0;
        public int _TileMousePointY = 0;

        private void mapTitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            _MapEditWin.GetMapInfos()._IsLoadMonstor = false;
            if (e.Button == MouseButtons.Left)
            {
                _StartSelectTile = true;

                int _mx = e.X ;
                int _my = e.Y ;

                _TileMousePointStart.X = _mx;
                _TileMousePointStart.Y = _my;

                _TileMousePointEnd.X = _mx;
                _TileMousePointEnd.Y = _my;

                int x = mapTitlePanel.Width / GetTileWidthInScale();
                int y = mapTitlePanel.Height / GetTileHeightInScale();

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (_mx >= i * GetTileWidthInScale() && _mx < (i * GetTileWidthInScale() + GetTileWidthInScale()) &&
                            _my >= j * GetTileHeightInScale() && _my < (j * GetTileHeightInScale() + GetTileHeightInScale()))
                        {
                            _TileMousePointStart.X = i * _MapEditWin.GetMapInfos().Map_Tile_Width;
                            _TileMousePointStart.Y = j * _MapEditWin.GetMapInfos().Map_Tile_Height;

                            return;
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                UpdateMouseRect();

                _MapEditWin.SetNowSelectPiece( new Bitmap(_TileMousePointW, _TileMousePointH) );

                Graphics imgGraphics = Graphics.FromImage(_MapEditWin.GetNowSelectPiece());

                Rectangle destRect = new Rectangle(new Point(0, 0), new Size(_TileMousePointW, _TileMousePointH));//目标位置
                Rectangle origRect = new Rectangle(new Point(_TileMousePointX , _TileMousePointY )
                    , new Size(_TileMousePointW, _TileMousePointH));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                imgGraphics.DrawImage(_MapEditWin.GetMapTileSelect(), destRect, origRect, GraphicsUnit.Pixel);

                AddTitlePieceImage();
            }
        }

        private void AddTitlePieceImage()
        {
            int titlePanelPieceW = mapTitlePanel.Width / _MapEditWin.GetMapInfos().Map_Tile_Width;
            int titlePanelPieceH = mapTitlePanel.Height / _MapEditWin.GetMapInfos().Map_Tile_Height;

            _MapEditWin._nowSelectPieceIndexByMap.Clear();

            int x = _TileMousePointX / _MapEditWin.GetMapInfos().Map_Tile_Width;
            int y = _TileMousePointY / _MapEditWin.GetMapInfos().Map_Tile_Height;

            _MapEditWin._nowSelectPieceW = _TileMousePointW / _MapEditWin.GetMapInfos().Map_Tile_Width;
            _MapEditWin._nowSelectPieceH = _TileMousePointH / _MapEditWin.GetMapInfos().Map_Tile_Height;

            MapTileInfos tempTitle = null;
            for (int x1 = 0; x1 < _MapEditWin._nowSelectPieceW; ++x1)
            {
                for (int y1 = 0; y1 < _MapEditWin._nowSelectPieceH; ++y1)
                {
                    if (((y + y1) * _maxTitleW + (x + x1)) >= _MapEditWin.GetTileImageList().Count)
                    {
                        continue;
                    }

                    tempTitle = (MapTileInfos)comboxTitleSelect.SelectedItem;
                    int index = _MapEditWin.GetMapInfos().AddMapUseTileInfo(
                        _MapEditWin.GetTileImageList()[(y + y1) * _maxTitleW + (x + x1)],
                        tempTitle.index,
                        _MapEditWin.GetMapInfos()._mapTileInfosByIndex[tempTitle.index]._name,
                        (y + y1) * _maxTitleW + (x + x1));

                    tyoEngineTileMapEditWindow.SelectIndexType stype = new tyoEngineTileMapEditWindow.SelectIndexType();

                    stype._x = x1;
                    stype._y = y1;
                    stype._type = index;

                    _MapEditWin._nowSelectPieceIndexByMap.Add(stype);
                }
            }

        }

        private void mapTitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_StartSelectTile == false)
            {
                return;
            }

            int _mx = e.X;
            int _my = e.Y;

            int x = mapTitlePanel.Width / GetTileWidthInScale();
            int y = mapTitlePanel.Height / GetTileHeightInScale();

            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (_mx >= i * GetTileWidthInScale() && _mx < (i * GetTileWidthInScale() + GetTileWidthInScale()) &&
                        _my >= j * GetTileHeightInScale() && _my < (j * GetTileHeightInScale() + GetTileHeightInScale()))
                    {
                        _TileMousePointEnd.X = i * _MapEditWin.GetMapInfos().Map_Tile_Width;
                        _TileMousePointEnd.Y = j * _MapEditWin.GetMapInfos().Map_Tile_Height;

                        return;
                    }
                }
            }
        }

        private void UpdateMouseRect()
        {
            _TileMousePointW = Math.Abs(_TileMousePointStart.X - _TileMousePointEnd.X);
            _TileMousePointH = Math.Abs(_TileMousePointStart.Y - _TileMousePointEnd.Y);
            _TileMousePointX = 0;
            _TileMousePointY = 0;

            if (_TileMousePointStart.X >= _TileMousePointEnd.X)
            {
                _TileMousePointX = _TileMousePointEnd.X;
            }
            else
            {
                _TileMousePointX = _TileMousePointStart.X;
            }

            if (_TileMousePointStart.Y >= _TileMousePointEnd.Y)
            {
                _TileMousePointY = _TileMousePointEnd.Y;
            }
            else
            {
                _TileMousePointY = _TileMousePointStart.Y;
            }

            if (_TileMousePointW < _MapEditWin.GetMapInfos().Map_Tile_Width)
            {
                _TileMousePointW = _MapEditWin.GetMapInfos().Map_Tile_Width;
            }
            else
            {
                _TileMousePointW += _MapEditWin.GetMapInfos().Map_Tile_Width;
            }

            if (_TileMousePointH < _MapEditWin.GetMapInfos().Map_Tile_Height)
            {
                _TileMousePointH = _MapEditWin.GetMapInfos().Map_Tile_Height;
            }
            else
            {
                _TileMousePointH += _MapEditWin.GetMapInfos().Map_Tile_Height;
            }
        }

        private void btAddTitle_Click(object sender, EventArgs e)
        {
            if (textTitleName.Text.Length <= 0)
            {
                MessageBox.Show("请输入新增地图图块名字!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog _dlg = new OpenFileDialog();
            _dlg.Filter = "tyo Engine Title |*.png;*.bmp;*.jpg";

            _dlg.ShowDialog();

            String _filePath = _dlg.FileName;

            if (Path.GetExtension(_filePath).ToLower() == ".png" ||
                Path.GetExtension(_filePath).ToLower() == ".jpg" ||
                Path.GetExtension(_filePath).ToLower() == ".bmp")
            {
                MapTileInfos tmpMapTitlesInfos = new MapTileInfos();

                tmpMapTitlesInfos._name = textTitleName.Text;
                tmpMapTitlesInfos._filename = Path.GetFileName(_filePath);
                tmpMapTitlesInfos._filepath = _filePath;
                tmpMapTitlesInfos.dirname = this.comboxTitleDirSelect.SelectedItem.ToString();
                
                string listname = tmpMapTitlesInfos._name;

                //int listindex = listBoxMapEditor_TitleList.Items.Add(listname);

                int _index = _MapEditWin.GetMapInfos()._mapTileInfosByIndex.Count;
                tmpMapTitlesInfos.index = _index;
                _MapEditWin.GetMapInfos()._mapTileInfosByIndex[_index] = tmpMapTitlesInfos;

                UpdateTileCombox();

                textTitleName.Text = "";

                Image image = Image.FromFile(_filePath);

                string dir = _MapEditWin.GetMapInfos().loadPath
                    + "\\" + this.comboxTitleDirSelect.SelectedItem.ToString();

                if (!Directory.Exists(dir)) 
                {
                    Directory.CreateDirectory(dir);
                }

                image.Save(dir
                    + "\\" + textTitleName.Text + ".jpg");

                return;
            }

            MessageBox.Show("增加图块失败");

            textTitleName.Text = "";
        }

        private void comboBoxTitleScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTitleScale.SelectedIndex == 0)
            {
                _TitleScale = maxScale;
            }
            else if (comboBoxTitleScale.SelectedIndex == 1)
            {
                _TitleScale = maxScale * 3.0f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 2)
            {
                _TitleScale = maxScale * 2.0f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 3)
            {
                _TitleScale = maxScale * 1.5f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 4)
            {
                _TitleScale = maxScale * 0.75f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 5)
            {
                _TitleScale = maxScale * 0.5f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 6)
            {
                _TitleScale = maxScale * 0.25f;
            }
            
            //mapTitlePanel.Size = new Size((int)(_MapEditWin.GetMapTitleSelect().Width * _TitleScale), (int)(_MapEditWin.GetMapTitleSelect().Height * _TitleScale));
            //this.Size = new Size((int)(_MapEditWin.GetMapTitleSelect().Width * _TitleScale) + 35, (int)(_MapEditWin.GetMapTitleSelect().Height * _TitleScale) + 90);
        }

        private void comboxTitleDirSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboxTitleSelect.Items.Clear();

            foreach (int index in _MapEditWin.GetMapInfos()._mapTileInfosByIndex.Keys)
            {
                if (this.comboxTitleDirSelect.SelectedItem.ToString().Equals(_MapEditWin.GetMapInfos()._mapTileInfosByIndex[index].dirname))
                {
                    comboxTitleSelect.Items.Add(_MapEditWin.GetMapInfos()
                        ._mapTileInfosByIndex[index]);
                }
            }

            if (comboxTitleSelect.Items.Count > 0)
            {
                comboxTitleSelect.SelectedIndex = 0;
            }

            comboxTitleSelect.Refresh();
        }
    }
}
