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
    public partial class tyoEngineTileAnimationEditor : Form
    {

        tyoEngineTileMapEditWindow _MapEditWin = null;
        public tyoEngineTileAnimationEditor()
        {
            InitializeComponent();
        }

        public void InitData(tyoEngineTileMapEditWindow _dlg)
        {
            _MapEditWin = _dlg;
        }
    }
}
