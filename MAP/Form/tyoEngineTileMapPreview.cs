using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using tyoEngineEditor.Others;

namespace tyoEngineEditor
{
    public partial class tyoEngineTileMapPreview : Form
    {
        MapInfos _mapInfos = null;

        List<MapLayerDrawInfos> _mapLayerDepth = new List<MapLayerDrawInfos>();

        float _MapTitleScale = 0.04f;

        public void SetMapInfos(MapInfos mapinfos)
        {
            if (mapinfos != null)
            {
                _mapInfos = mapinfos;
                //decimal result = Math.Round((decimal)_mapInfos.Map_Size_Width / this.panelPreView.Width, 2);
                //result = Math.Round((decimal)0.04 / result,2);
                //_MapTitleScale = float.Parse(result.ToString());
            }
        }

        public tyoEngineTileMapPreview()
        {
            InitializeComponent();
        }

        private void panelPreView_Paint(object sender, PaintEventArgs e)
        {
            DoPaint(e.Graphics);
        }

        public void DrawImageHandle() 
        {
            this.Refresh();
        }

        private void DoPaint(Graphics g) 
        {
            g.ScaleTransform(_MapTitleScale, _MapTitleScale);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            for (int i = 0; i < _mapInfos._mapLayerInfosByIndex.Count; ++i)
            {
                if (_mapInfos._LayerShowFlag[_mapLayerDepth[i]._index] == false)
                {
                    continue;
                }

                for (int x = 0; x < _mapInfos.Map_Size_Width; ++x)
                {
                    for (int y = 0;y <  _mapInfos.Map_Size_Height; ++y)
                    {
                        int index = _mapInfos._mapTitle[i, x, y];

                        if (index != -1)
                        {
                            if (_mapInfos._mapTitleUseInfo[index]._image._title != null)
                            {
                                g.DrawImage(
                                _mapInfos._mapTitleUseInfo[index]._image._title,
                                x * _mapInfos.Map_Title_Width,
                                y * _mapInfos.Map_Title_Height,
                                _mapInfos._mapTitleUseInfo[index]._image._w,
                                _mapInfos._mapTitleUseInfo[index]._image._h);
                            }
                        }
                    }
                }
            }
        }

        private void tyoEngineTileMapPreview_Shown(object sender, EventArgs e)
        {
            if (_mapInfos == null)
            {
                Close();

                return;
            }

            this.Text = _mapInfos.Name;

            _mapLayerDepth.Clear();

            foreach (int index in _mapInfos._mapLayerInfosByIndex.Keys)
            {

                MapLayerDrawInfos tmpLayerDepth = new MapLayerDrawInfos();

                tmpLayerDepth._depth = _mapInfos._mapLayerInfosByIndex[index]._depth;
                tmpLayerDepth._index = index;

                _mapLayerDepth.Add(tmpLayerDepth);
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
            
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            try
            {
                this._MapTitleScale = float.Parse(this.textBox1.Text)*0.01f;
                this.panelPreView.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("请输入数字！");
            }
            
        }
    }
}
