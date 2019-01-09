using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace tyoEngineEditor
{
    public partial class tyoEngineAnimation : Form
    {
        private Dictionary<string, AnimationInfo> _animationInfos;
        int frameIndex = 0;

        private List<AnimationImageInfo> animationImageInfos = null;
        private AnimationInfo animationInfo = null;

        private tyoEngineTitleMapEditWindow _mainDlg;

        public int _TitleMousePointW = 0;
        public int _TitleMousePointH = 0;
        public int _TitleMousePointX = 0;
        public int _TitleMousePointY = 0;

        Point _TitleMousePointStart = new Point();
        Point _TitleMousePointEnd = new Point();

        bool _StartSelectTitle = false;

        public tyoEngineAnimation(string path, tyoEngineTitleMapEditWindow mainDlg)
        {
            InitializeComponent();
            _mainDlg = mainDlg;
            _animationInfos = Animation.ReadAnimationXML(path);
            InitData();

            DirectoryInfo d = new DirectoryInfo(Directory.GetParent(path).FullName + Parameters.PICFILE);

            foreach (FileInfo item in d.GetFiles())
            {
                if (!mainDlg.GetMapInfos().animationPNGpath.Contains(item.FullName))
                {
                    mainDlg.GetMapInfos().animationPNGpath.Add(item.FullName);
                }
            }

            if (path.ToUpper().EndsWith("XML")&&!mainDlg.GetMapInfos().animationXML.Contains(path))
            {
                mainDlg.GetMapInfos().animationXML.Add(path);
            }
        }

        #region 初始化数据

        private void InitData()
        {
            if (_animationInfos != null)
            {
                int rowI = 0;
                string name = string.Empty;

                foreach (KeyValuePair<string, AnimationInfo> kv in _animationInfos)
                {
                    if (name != kv.Value.name)
                    {
                        rowI = dgvAnimation.Rows.Add();
                        dgvAnimation.Rows[rowI].Cells[0].Value = kv.Value.name;
                        dgvAnimation.Rows[rowI].Cells[1].Value = "0-无";
                    }
                    name = kv.Value.name;
                }
            }
        }


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TitleMousePointW = 0;
            _TitleMousePointH = 0;
            _TitleMousePointX = 0;
            _TitleMousePointY = 0;

            _TitleMousePointStart = new Point();
            _TitleMousePointEnd = new Point();

            _StartSelectTitle = false;
        }
        #endregion

        #region 用户选择后执行动画
        private void dgvAnimation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string key = e.RowIndex.ToString() + dgvAnimation.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (_animationInfos.ContainsKey(key))
                {
                    animationImageInfos = _animationInfos[key].images;
                    animationInfo = _animationInfos[key];
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (animationImageInfos != null)
            {
                frameIndex = frameIndex == animationImageInfos.Count - 1 ? 0 : frameIndex + 1;
                panelAnimation.Refresh();
                panelOriginal.Refresh();
            }
        }

        private void cbPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlay.Checked)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        private void panelAnimation_Paint(object sender, PaintEventArgs e)
        {
            if (animationImageInfos != null)
            {
                e.Graphics.DrawImage(animationImageInfos[frameIndex].originalBitmap,
                    0, 0,
                    animationImageInfos[frameIndex].originalW, animationImageInfos[frameIndex].originalH);
            }
        }
        #endregion

        #region 设置图片大小

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), _TitleMousePointX, _TitleMousePointY,
                _TitleMousePointW, _TitleMousePointH);
            e.Graphics.DrawRectangle(_mainDlg.GetBlackPen(), _TitleMousePointX + 1, _TitleMousePointY + 1,
                _TitleMousePointW - 2, _TitleMousePointH - 2);
            e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), _TitleMousePointX + 2, _TitleMousePointY + 2,
                _TitleMousePointW - 4, _TitleMousePointH - 4);
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _TitleMousePointStart.X = e.X;
                _TitleMousePointStart.Y = e.Y;

                _TitleMousePointX = (e.X / _mainDlg.GetMapTitleWidthInScale()) * _mainDlg.GetMapTitleWidthInScale();
                _TitleMousePointY = (e.Y / _mainDlg.GetMapTitleHeightInScale()) * _mainDlg.GetMapTitleHeightInScale();

                _StartSelectTitle = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (animationImageInfos == null)
                {
                    MessageBox.Show("请先选择动画！");
                    return;
                }
                _mainDlg.AddAnimationPiece(animationImageInfos, animationInfo);
                //_mainDlg.AddAnimationPiece(animationImageInfos, _TitleMousePointW, _TitleMousePointH);
            }
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_StartSelectTitle)
            {
                _TitleMousePointEnd.X = e.X;
                _TitleMousePointEnd.Y = e.Y;

                _TitleMousePointW = (_TitleMousePointEnd.X / _mainDlg.GetMapTitleWidthInScale() + 1 -
                _TitleMousePointStart.X / _mainDlg.GetMapTitleWidthInScale()) * _mainDlg.GetMapTitleWidthInScale();

                _TitleMousePointH = ((_TitleMousePointEnd.Y / _mainDlg.GetMapTitleHeightInScale() + 1)
            - _TitleMousePointStart.Y / _mainDlg.GetMapTitleHeightInScale()) * _mainDlg.GetMapTitleHeightInScale();
            }
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _TitleMousePointEnd.X = e.X;
                _TitleMousePointEnd.Y = e.Y;

                _TitleMousePointW = (_TitleMousePointEnd.X / _mainDlg.GetMapTitleWidthInScale() + 1 -
                    _TitleMousePointStart.X / _mainDlg.GetMapTitleWidthInScale()) * _mainDlg.GetMapTitleWidthInScale();

                _TitleMousePointH = ((_TitleMousePointEnd.Y / _mainDlg.GetMapTitleHeightInScale() + 1) -
                    _TitleMousePointStart.Y / _mainDlg.GetMapTitleHeightInScale()) * _mainDlg.GetMapTitleHeightInScale();

                _StartSelectTitle = false;
            }
        }


        private void timerDraw_Tick(object sender, EventArgs e)
        {
            panel.Refresh();
        }
        #endregion

        #region 原图切块
        private void panelOriginal_Paint(object sender, PaintEventArgs e)
        {
            if (tabControl.SelectedIndex == 0 && animationImageInfos != null) 
            {
                e.Graphics.DrawImage(animationImageInfos[frameIndex].onPanelBitmap,
                    0, 0,
                    animationImageInfos[frameIndex].onPanelBitmap.Width, animationImageInfos[frameIndex].onPanelBitmap.Height);

                e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), 0, 0,
                animationImageInfos[frameIndex].onPanelBitmap.Width, animationImageInfos[frameIndex].onPanelBitmap.Height);
                e.Graphics.DrawRectangle(_mainDlg.GetBlackPen(), 1, 1,
                    animationImageInfos[frameIndex].onPanelBitmap.Width - 2, animationImageInfos[frameIndex].onPanelBitmap.Height - 2);
                e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), 2, 2,
                    animationImageInfos[frameIndex].onPanelBitmap.Width - 4, animationImageInfos[frameIndex].onPanelBitmap.Height - 4);

                //e.Graphics.DrawImage(animationImageInfos[frameIndex].originalBitmap,
                //    0, 0,
                //    animationImageInfos[frameIndex].originalBitmap.Width, animationImageInfos[frameIndex].originalBitmap.Height);

                //e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), 0, 0,
                //animationImageInfos[frameIndex].originalBitmap.Width, animationImageInfos[frameIndex].originalBitmap.Height);
                //e.Graphics.DrawRectangle(_mainDlg.GetBlackPen(), 1, 1,
                //    animationImageInfos[frameIndex].originalBitmap.Width - 2, animationImageInfos[frameIndex].originalBitmap.Height - 2);
                //e.Graphics.DrawRectangle(_mainDlg.GetWhilePen(), 2, 2,
                //    animationImageInfos[frameIndex].originalBitmap.Width - 4, animationImageInfos[frameIndex].originalBitmap.Height - 4);
            }
        }
        private void panelOriginal_MouseDown(object sender, MouseEventArgs e)
        {
            if (animationImageInfos == null)
            {
                return;
            }

            _mainDlg.AddAnimationPiece(animationImageInfos ,animationInfo);
            _mainDlg.GetMapInfos()._IsClickAnimation = true;
            //_mainDlg.AddAnimationPiece(animationImageInfos, animationImageInfos[0].originalW,
                //animationImageInfos[0].originalH);
        }

        #endregion

    }
}
