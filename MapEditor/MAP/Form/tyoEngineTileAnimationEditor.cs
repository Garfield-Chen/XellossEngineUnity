using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        Dictionary<string, List<tyoAnimationFrameInfo>> _AniListFrameDict = new Dictionary<string, List<tyoAnimationFrameInfo>>();
        Dictionary<string, tyoAnimationJsonFile> _AniListJsonDict = new Dictionary<string, tyoAnimationJsonFile>();

        private void btMapEditor_AddAniFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _dlg = new OpenFileDialog();
            _dlg.Filter = "tyo Engine Animation Data|*.json";
            _dlg.InitialDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _dlg.ShowDialog();

            string _filePath = _dlg.FileName;

            if (_filePath.Length > 0)
            {
                FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
                StreamReader fileReader = new StreamReader(fs);

                tyoAnimationJsonFile _jsonFile = new tyoAnimationJsonFile();

                string _jsonString = fileReader.ReadToEnd();

                _jsonFile = Newtonsoft.Json.JsonConvert.DeserializeObject<tyoAnimationJsonFile>(_jsonString);

                fileReader.Close();
                fs.Close();


                if (_AniListFrameDict.ContainsKey(_jsonFile.AnimationName))
                {
                    _AniListFrameDict.Remove(_jsonFile.AnimationName);
                    _AniListJsonDict.Remove(_jsonFile.AnimationName);
                }


                //aniTextBox_FPS.Text = _jsonFile.AnimationFPS.ToString();
                //aniTextBox_Name.Text = _jsonFile.AnimationName;

                string _texDict = Path.GetDirectoryName(_filePath);
                _texDict += "\\Textures\\";

                int _frameID = 0;
                List<tyoAnimationFrameInfo> _frameList = new List<tyoAnimationFrameInfo>();

                
                foreach (string _texName in _jsonFile.AnimationNameList)
                {
                    string _texFilePath = _texDict + _texName + ".png";

                    tyoAnimationFrameInfo _info = new tyoAnimationFrameInfo(_texFilePath);
                    _info.FrameID = _frameID;

                    _frameID++;

                    _frameList.Add(_info);
                }

                _AniListFrameDict.Add(_jsonFile.AnimationName, _frameList);
                _AniListJsonDict.Add(_jsonFile.AnimationName, _jsonFile);

                if(_jsonFile.AnimationDesName.Length > 0)
                {
                    mapEditorAniListBox_LoadAniList.Items.Add(_jsonFile.AnimationName + "_" + _jsonFile.AnimationDesName);
                }
                else
                {
                    mapEditorAniListBox_LoadAniList.Items.Add(_jsonFile.AnimationName);
                }
            }
        }

        private void btMapEditor_AddAniToMap_Click(object sender, EventArgs e)
        {
            _MapEditWin.SetAnimationSelectPiece(_CurrentSelectAnimation);
        }

        string _CurrentSelectAnimation = "";
        int _CurrentSelectAnimationFrameIdx = -1;

        private void mapEditorAniListBox_LoadAniList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(mapEditorAniListBox_LoadAniList.SelectedIndex >= 0)
            {
                string[] _aniList = mapEditorAniListBox_LoadAniList.Items[mapEditorAniListBox_LoadAniList.SelectedIndex].ToString().Split('_');

                if(_aniList.Length > 1)
                {
                    _CurrentSelectAnimation = _aniList[0];
                }
                else
                {
                    _CurrentSelectAnimation = mapEditorAniListBox_LoadAniList.Items[mapEditorAniListBox_LoadAniList.SelectedIndex].ToString();
                }

                //string[] _aniList = _CurrentSelectAnimation

                if (_AniListJsonDict.ContainsKey(_CurrentSelectAnimation))
                {
                    mapEditorAniTimer_PlayTimer.Stop();
                    mapEditorAniTimer_PlayTimer.Interval = 1000 / _AniListJsonDict[_CurrentSelectAnimation].AnimationFPS;
                    mapEditorAniTimer_PlayTimer.Start();

                    _CurrentSelectAnimationFrameIdx = 0;

                    ShowAniFrameImage();
                }
            }
        }

        private void mapEditorAniTimer_PlayTimer_Tick(object sender, EventArgs e)
        {
            _CurrentSelectAnimationFrameIdx++;

            if (_AniListFrameDict.ContainsKey(_CurrentSelectAnimation))
            {
                if (_CurrentSelectAnimationFrameIdx >= _AniListFrameDict[_CurrentSelectAnimation].Count)
                {
                    _CurrentSelectAnimationFrameIdx = 0;
                }

                ShowAniFrameImage();
            }
        }

        private void ShowAniFrameImage()
        {
            mapEditorAniPictureBox_AniShow.Image = _AniListFrameDict[_CurrentSelectAnimation][_CurrentSelectAnimationFrameIdx].FrameImage;

            if (mapEditorAniPictureBox_AniShow.Image.Width > 256 || mapEditorAniPictureBox_AniShow.Image.Height > 256)
            {
                mapEditorAniPictureBox_AniShow.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                mapEditorAniPictureBox_AniShow.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
    }
}
