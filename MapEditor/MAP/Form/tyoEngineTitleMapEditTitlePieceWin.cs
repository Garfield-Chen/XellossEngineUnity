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
    public partial class tyoEngineTitleMapEditTitlePieceWin : Form
    {
        public tyoEngineTitleMapEditTitlePieceWin()
        {
            InitializeComponent();
        }

        tyoEngineTitleMapEditWindow _MapEditWin = null;

        private const int MapTitlePanelSizeW = 800;
        private const int MapTitlePanelSizeH = 800;

        public void InitData( tyoEngineTitleMapEditWindow _dlg )
        {
            _MapEditWin = _dlg;

            mapTitlePanel.Size = new Size(MapTitlePanelSizeW, MapTitlePanelSizeH);
            this.Size = new Size(830, 890);
        }

        public void UpdateTick()
        {
            mapTitlePanel.Refresh();
        }

        private void tyoEngineTitleMapEditTitlePieceWin_Shown(object sender, EventArgs e)
        {
            UpdateTitleCombox();
        }

        private void UpdateTitleCombox()
        {
            this.comboxTitleDirSelect.Items.Clear();

            string currentPath = System.Environment.CurrentDirectory
                + "\\MAPResource\\Titles";
            if (!_MapEditWin.GetMapInfos().loadPath.Equals(string.Empty)) 
            {
                currentPath = _MapEditWin.GetMapInfos().loadPath.Substring(0,
                    _MapEditWin.GetMapInfos().loadPath.LastIndexOf("\\"));
            }
            string[] dirs = Directory.GetDirectories(currentPath);

            string dirName = string.Empty;
            if (!this.comboxTitleDirSelect.Items.Contains("Titles")) 
            {
                this.comboxTitleDirSelect.Items.Add("Titles");
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
                MapTitleInfos tempTitle = (MapTitleInfos)comboxTitleSelect.SelectedItem;
                _MapEditWin.SetMapTitelSelect(Image.FromFile(_MapEditWin.GetMapInfos()
                    ._mapTitleInfosByIndex[tempTitle.index]._filepath));

                _MapEditWin.GetTitleImageList().Clear();

                // 先切割图片，看看长宽可以分割成多少个快快。
                _maxTitleW = _MapEditWin.GetMapTitleSelect().Width / _MapEditWin.GetMapInfos().Map_Title_Width;
                _maxTitleH = _MapEditWin.GetMapTitleSelect().Height / _MapEditWin.GetMapInfos().Map_Title_Height;


                float _wScale = (float)MapTitlePanelSizeW / (float)_MapEditWin.GetMapTitleSelect().Width ;
                float _hScale = (float)MapTitlePanelSizeH / (float)_MapEditWin.GetMapTitleSelect().Height;

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
                        Bitmap imgTitle = new Bitmap(_MapEditWin.GetMapInfos().Map_Title_Width, _MapEditWin.GetMapInfos().Map_Title_Height);

                        Graphics imgGraphics = Graphics.FromImage(imgTitle);

                        Rectangle destRect = new Rectangle(new Point(0, 0), new Size(_MapEditWin.GetMapInfos().Map_Title_Width, _MapEditWin.GetMapInfos().Map_Title_Height));//目标位置

                        Rectangle origRect = new Rectangle(
                            new Point(x * _MapEditWin.GetMapInfos().Map_Title_Width, y * _MapEditWin.GetMapInfos().Map_Title_Height),
                            new Size(_MapEditWin.GetMapInfos().Map_Title_Width, _MapEditWin.GetMapInfos().Map_Title_Height));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                        imgGraphics.DrawImage(_MapEditWin.GetMapTitleSelect(), destRect, origRect, GraphicsUnit.Pixel);

                        tyoEngineTitleMapEditWindow.MapTitlePiece _piece = new tyoEngineTitleMapEditWindow.MapTitlePiece();
                        
                        _piece._x = origRect.X;
                        _piece._y = origRect.Y;
                        _piece._w = origRect.Width;
                        _piece._h = origRect.Height;

                        //_piece._titleinfo = _MapEditWin.GetMapInfos()._mapTitleInfosByIndex[comboxTitleSelect.SelectedIndex];
                        _piece._title = imgTitle;

                        _MapEditWin.GetTitleImageList().Add(_piece);
                    }
                }
            }
        }

        private void mapTitlePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.ScaleTransform(_TitleScale, _TitleScale);

            if (_MapEditWin.GetMapTitleSelect() != null)
            {
                int __iw = _MapEditWin.GetMapTitleSelect().Width;

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

                        if ((y * _maxTitleW + x) >= _MapEditWin.GetTitleImageList().Count)
                        {
                            continue;
                        }

                        e.Graphics.DrawImage(_MapEditWin.GetTitleImageList()[y * _maxTitleW + x]._title, 
                            drawx * _MapEditWin.GetMapInfos().Map_Title_Width,
                            drawy * _MapEditWin.GetMapInfos().Map_Title_Height);


                        drawy++;
                    }

                    drawx++;

                    drawy = 0;
                }
            }

            UpdateMouseRect();

            e.Graphics.DrawRectangle(_MapEditWin.GetWhilePen(), _TitleMousePointX, _TitleMousePointY, _TitleMousePointW, _TitleMousePointH);
            e.Graphics.DrawRectangle(_MapEditWin.GetBlackPen(), _TitleMousePointX + 1, _TitleMousePointY + 1, _TitleMousePointW - 2, _TitleMousePointH - 2);
            e.Graphics.DrawRectangle(_MapEditWin.GetWhilePen(), _TitleMousePointX + 2, _TitleMousePointY + 2, _TitleMousePointW - 4, _TitleMousePointH - 4);
        }

        private void mapTitlePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int _mx = e.X;
                int _my = e.Y;

                _StartSelectTitle = false;

                int x = mapTitlePanel.Width / GetTitleWidthInScale();
                int y = mapTitlePanel.Height / GetTitleHeightInScale();

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (_mx >= i * GetTitleWidthInScale() && _mx < (i * GetTitleWidthInScale() + GetTitleWidthInScale()) &&
                            _my >= j * GetTitleHeightInScale() && _my < (j * GetTitleHeightInScale() + GetTitleHeightInScale()))
                        {
                            _TitleMousePointEnd.X = i * _MapEditWin.GetMapInfos().Map_Title_Width;
                            _TitleMousePointEnd.Y = j * _MapEditWin.GetMapInfos().Map_Title_Height;

                            return;
                        }
                    }
                }
            }
        }

        float _TitleScale = 1.0f;

        private int GetTitleWidthInScale()
        {
            return (int)(_MapEditWin.GetMapInfos().Map_Title_Width * _TitleScale);
        }

        private int GetTitleHeightInScale()
        {
            return (int)(_MapEditWin.GetMapInfos().Map_Title_Height * _TitleScale);
        }

        bool _StartSelectTitle = false;

        Point _TitleMousePointStart = new Point();
        Point _TitleMousePointEnd = new Point();

        public int _TitleMousePointW = 0;
        public int _TitleMousePointH = 0;
        public int _TitleMousePointX = 0;
        public int _TitleMousePointY = 0;

        private void mapTitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            _MapEditWin.GetMapInfos()._IsLoadMonstor = false;
            if (e.Button == MouseButtons.Left)
            {
                _StartSelectTitle = true;

                int _mx = e.X ;
                int _my = e.Y ;

                _TitleMousePointStart.X = _mx;
                _TitleMousePointStart.Y = _my;

                _TitleMousePointEnd.X = _mx;
                _TitleMousePointEnd.Y = _my;

                int x = mapTitlePanel.Width / GetTitleWidthInScale();
                int y = mapTitlePanel.Height / GetTitleHeightInScale();

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (_mx >= i * GetTitleWidthInScale() && _mx < (i * GetTitleWidthInScale() + GetTitleWidthInScale()) &&
                            _my >= j * GetTitleHeightInScale() && _my < (j * GetTitleHeightInScale() + GetTitleHeightInScale()))
                        {
                            _TitleMousePointStart.X = i * _MapEditWin.GetMapInfos().Map_Title_Width;
                            _TitleMousePointStart.Y = j * _MapEditWin.GetMapInfos().Map_Title_Height;

                            return;
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                UpdateMouseRect();

                _MapEditWin.SetNowSelectPiece( new Bitmap(_TitleMousePointW, _TitleMousePointH) );

                Graphics imgGraphics = Graphics.FromImage(_MapEditWin.GetNowSelectPiece());

                Rectangle destRect = new Rectangle(new Point(0, 0), new Size(_TitleMousePointW, _TitleMousePointH));//目标位置
                Rectangle origRect = new Rectangle(new Point(_TitleMousePointX , _TitleMousePointY )
                    , new Size(_TitleMousePointW, _TitleMousePointH));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                imgGraphics.DrawImage(_MapEditWin.GetMapTitleSelect(), destRect, origRect, GraphicsUnit.Pixel);

                AddTitlePieceImage();
            }
        }

        private void AddTitlePieceImage()
        {
            int titlePanelPieceW = mapTitlePanel.Width / _MapEditWin.GetMapInfos().Map_Title_Width;
            int titlePanelPieceH = mapTitlePanel.Height / _MapEditWin.GetMapInfos().Map_Title_Height;

            _MapEditWin._nowSelectPieceIndexByMap.Clear();

            int x = _TitleMousePointX / _MapEditWin.GetMapInfos().Map_Title_Width;
            int y = _TitleMousePointY / _MapEditWin.GetMapInfos().Map_Title_Height;

            _MapEditWin._nowSelectPieceW = _TitleMousePointW / _MapEditWin.GetMapInfos().Map_Title_Width;
            _MapEditWin._nowSelectPieceH = _TitleMousePointH / _MapEditWin.GetMapInfos().Map_Title_Height;

            MapTitleInfos tempTitle = null;
            for (int x1 = 0; x1 < _MapEditWin._nowSelectPieceW; ++x1)
            {
                for (int y1 = 0; y1 < _MapEditWin._nowSelectPieceH; ++y1)
                {
                    if (((y + y1) * _maxTitleW + (x + x1)) >= _MapEditWin.GetTitleImageList().Count)
                    {
                        continue;
                    }
                    tempTitle = (MapTitleInfos)comboxTitleSelect.SelectedItem;
                    int index = _MapEditWin.GetMapInfos().AddMapUseTitleInfo(
                        _MapEditWin.GetTitleImageList()[(y + y1) * _maxTitleW + (x + x1)],
                        tempTitle.index,
                        _MapEditWin.GetMapInfos()._mapTitleInfosByIndex[tempTitle.index]._name,
                        (y + y1) * _maxTitleW + (x + x1));

                    tyoEngineTitleMapEditWindow.SelectIndexType stype = new tyoEngineTitleMapEditWindow.SelectIndexType();

                    stype._x = x1;
                    stype._y = y1;
                    stype._type = index;

                    _MapEditWin._nowSelectPieceIndexByMap.Add(stype);
                }
            }

        }

        private void mapTitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_StartSelectTitle == false)
            {
                return;
            }

            int _mx = e.X;
            int _my = e.Y;

            int x = mapTitlePanel.Width / GetTitleWidthInScale();
            int y = mapTitlePanel.Height / GetTitleHeightInScale();

            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (_mx >= i * GetTitleWidthInScale() && _mx < (i * GetTitleWidthInScale() + GetTitleWidthInScale()) &&
                        _my >= j * GetTitleHeightInScale() && _my < (j * GetTitleHeightInScale() + GetTitleHeightInScale()))
                    {
                        _TitleMousePointEnd.X = i * _MapEditWin.GetMapInfos().Map_Title_Width;
                        _TitleMousePointEnd.Y = j * _MapEditWin.GetMapInfos().Map_Title_Height;

                        return;
                    }
                }
            }
        }

        private void UpdateMouseRect()
        {
            _TitleMousePointW = Math.Abs(_TitleMousePointStart.X - _TitleMousePointEnd.X);
            _TitleMousePointH = Math.Abs(_TitleMousePointStart.Y - _TitleMousePointEnd.Y);
            _TitleMousePointX = 0;
            _TitleMousePointY = 0;

            if (_TitleMousePointStart.X >= _TitleMousePointEnd.X)
            {
                _TitleMousePointX = _TitleMousePointEnd.X;
            }
            else
            {
                _TitleMousePointX = _TitleMousePointStart.X;
            }

            if (_TitleMousePointStart.Y >= _TitleMousePointEnd.Y)
            {
                _TitleMousePointY = _TitleMousePointEnd.Y;
            }
            else
            {
                _TitleMousePointY = _TitleMousePointStart.Y;
            }

            if (_TitleMousePointW < _MapEditWin.GetMapInfos().Map_Title_Width)
            {
                _TitleMousePointW = _MapEditWin.GetMapInfos().Map_Title_Width;
            }
            else
            {
                _TitleMousePointW += _MapEditWin.GetMapInfos().Map_Title_Width;
            }

            if (_TitleMousePointH < _MapEditWin.GetMapInfos().Map_Title_Height)
            {
                _TitleMousePointH = _MapEditWin.GetMapInfos().Map_Title_Height;
            }
            else
            {
                _TitleMousePointH += _MapEditWin.GetMapInfos().Map_Title_Height;
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
                MapTitleInfos tmpMapTitlesInfos = new MapTitleInfos();

                tmpMapTitlesInfos._name = textTitleName.Text;
                tmpMapTitlesInfos._filename = Path.GetFileName(_filePath);
                tmpMapTitlesInfos._filepath = _filePath;
                tmpMapTitlesInfos.dirname = this.comboxTitleDirSelect.SelectedItem.ToString();
                
                string listname = tmpMapTitlesInfos._name;

                //int listindex = listBoxMapEditor_TitleList.Items.Add(listname);

                int _index = _MapEditWin.GetMapInfos()._mapTitleInfosByIndex.Count;
                tmpMapTitlesInfos.index = _index;
                _MapEditWin.GetMapInfos()._mapTitleInfosByIndex[_index] = tmpMapTitlesInfos;

                UpdateTitleCombox();

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
                _TitleScale = maxScale * 0.75f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 2)
            {
                _TitleScale = maxScale * 0.5f;
            }
            else if (comboBoxTitleScale.SelectedIndex == 3)
            {
                _TitleScale = maxScale * 0.25f;
            }
            
            //mapTitlePanel.Size = new Size((int)(_MapEditWin.GetMapTitleSelect().Width * _TitleScale), (int)(_MapEditWin.GetMapTitleSelect().Height * _TitleScale));
            //this.Size = new Size((int)(_MapEditWin.GetMapTitleSelect().Width * _TitleScale) + 35, (int)(_MapEditWin.GetMapTitleSelect().Height * _TitleScale) + 90);
        }

        private void comboxTitleDirSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboxTitleSelect.Items.Clear();

            foreach (int index in _MapEditWin.GetMapInfos()._mapTitleInfosByIndex.Keys)
            {
                if (this.comboxTitleDirSelect.SelectedItem.ToString().Equals(_MapEditWin.GetMapInfos()._mapTitleInfosByIndex[index].dirname))
                {
                    comboxTitleSelect.Items.Add(_MapEditWin.GetMapInfos()
                        ._mapTitleInfosByIndex[index]);
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
