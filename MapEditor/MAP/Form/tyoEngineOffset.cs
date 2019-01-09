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
    public partial class tyoEngineOffset : Form
    {
        public int _offsetX = 0;
        public int _offsetY = 0;

        private int initValue = 500;

        public tyoEngineOffset()
        {
            InitializeComponent();
        }

        public tyoEngineOffset(int offsetX,int offsetY)
        {
            InitializeComponent();
            //_offsetX = offsetX;
            //_offsetY = offsetY;
            hScrollBarX.Value = offsetX + initValue;
            hScrollBarY.Value = offsetY + initValue;
        }

        private void hScrollBarX_ValueChanged(object sender, EventArgs e)
        {
            _offsetX = hScrollBarX.Value - initValue;
        }

        private void hScrollBarY_ValueChanged(object sender, EventArgs e)
        {
            _offsetY = hScrollBarY.Value - initValue;
        }
    }
}
