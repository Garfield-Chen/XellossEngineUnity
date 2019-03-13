using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tyoEngineEditor
{
    public partial class tyoEngineTileAttributeDlg : Form
    {

        MapInfos _MapInfos = null;
        public tyoEngineTileAttributeDlg(int _x,int _y,int _layerIndex,string _layerName,Image _image,MapInfos _mapInfos)
        {
            InitializeComponent();

            pictureBox1.Image = _image;

            label1.Text = string.Format("X: {0}, Y: {1}", _x, _y);
            label2.Text = "Layer: " + _layerName;

            MapTileAttribute _attribute = _mapInfos.GetMapTileAttribute(_x, _y, _layerIndex);
            propertyGrid1.SelectedObject = _attribute;

            listBox1.Visible = false;

            _MapInfos = _mapInfos;
        }

        public tyoEngineTileAttributeDlg(MapInfos _mapInfos)
        {
            InitializeComponent();

            listBox1.Visible = true;

            _MapInfos = _mapInfos;

            for ( int i = 0; i < _MapInfos._mapTileAttributeList.Count; ++i)
            {
                string _layerName = _MapInfos._mapLayerInfosByIndex[_MapInfos._mapTileAttributeList[i]._tile_layer].Name;
                string _listNode = string.Format("{0}[{1},{2}]",_layerName, _MapInfos._mapTileAttributeList[i]._tile_x, _MapInfos._mapTileAttributeList[i]._tile_y);

                listBox1.Items.Add(_listNode);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < _MapInfos._mapTileAttributeList.Count)
            {
                MapTileAttribute _attribute = _MapInfos._mapTileAttributeList[listBox1.SelectedIndex];
                int _x = _attribute._tile_x;
                int _y = _attribute._tile_y;
                int _layerIndex = _attribute._tile_layer;

                string _layerName = _MapInfos._mapLayerInfosByIndex[_layerIndex].Name;

                int _index = _MapInfos._mapTile[_layerIndex, _x, _y];

                if (_index != -1 && _index < _MapInfos._mapTileUseInfo.Count)
                {
                    //e.Graphics.DrawImage(_mapInfos._mapTitleUseInfo[index]._image, (x - mapHScrollBar.Value) * _mapInfos.Map_Title_Width, (y - mapVScrollBar.Value) * _mapInfos.Map_Title_Height);

                    if (_MapInfos._mapTileUseInfo[_index]._image._tile != null)
                    {
                        pictureBox1.Image = _MapInfos._mapTileUseInfo[_index]._image._tile;
                    }
                }

                label1.Text = string.Format("X: {0}, Y: {1}", _x, _y);
                label2.Text = "Layer: " + _layerName;

                propertyGrid1.SelectedObject = _attribute;
            }
        }
    }
}
