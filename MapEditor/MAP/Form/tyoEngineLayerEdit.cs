using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tyoEngineEditor
{
    public partial class tyoEngineLayerEdit : Form
    {
        private int _nowMapLayerInfosSelect = -1;
        private MapInfos _mapInfos;
        private string name = string.Empty;

        public ListBox getListBox() 
        {
            return listBoxMapEditor_MapLayer;
        }

        public tyoEngineLayerEdit(MapInfos mapInfos)
        {
            InitializeComponent();
            _mapInfos = mapInfos;
            foreach (KeyValuePair<int, MapLayerInfos> item in mapInfos._mapLayerInfosByIndex)
            {
                listBoxMapEditor_MapLayer.Items.Insert(item.Key, item.Value);
            }
        }

        private void listBoxMapEditor_MapLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMapEditor_MapLayer.SelectedIndex >= 0)
            {
                _nowMapLayerInfosSelect = listBoxMapEditor_MapLayer.SelectedIndex;
                propertyGridMapEditor_MapLayer.SelectedObject = listBoxMapEditor_MapLayer.Items[_nowMapLayerInfosSelect];
                name = ((MapLayerInfos)propertyGridMapEditor_MapLayer.SelectedObject).Name;
            }
        }

        private void btMAP_AddLayer_Click(object sender, EventArgs e)
        {
            MapLayerInfos layer = new MapLayerInfos();
            layer.opType = "ADD";
            listBoxMapEditor_MapLayer.Items.Add(layer);
        }

        private void btMAP_DelLayer_Click(object sender, EventArgs e)
        {
            if (_nowMapLayerInfosSelect >= 0)
            {
                listBoxMapEditor_MapLayer.Items.RemoveAt(_nowMapLayerInfosSelect);
                propertyGridMapEditor_MapLayer.SelectedObject = null;
            }
        }

        private void propertyGridMapEditor_MapLayer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                listBoxMapEditor_MapLayer.Items.RemoveAt(_nowMapLayerInfosSelect);
                listBoxMapEditor_MapLayer.Items.Insert(_nowMapLayerInfosSelect, (MapLayerInfos)propertyGridMapEditor_MapLayer.SelectedObject);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
