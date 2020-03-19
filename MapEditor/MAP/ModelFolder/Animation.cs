using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace tyoEngineEditor
{
    public class tyoAnimationFrameInfo
    {
        public tyoAnimationFrameInfo(string _path)
        {
            _frameImage = Image.FromFile(_path);
        }

        [Description("帧序ID"), Category("帧信息")]
        public int FrameID
        {
            set
            {
                _frameID = value;
            }

            get
            {
                return _frameID;
            }

        }

        [Description("帧图像"), Category("帧信息")]
        public Image FrameImage
        {
            set
            {
                _frameImage = value;
            }

            get
            {
                return _frameImage;
            }

        }

        int _frameID = 0;
        Image _frameImage = null;
    }

    public class tyoAnimationJsonFile
    {
        private string _animationFile = "";
        public string AnimationName
        {
            get { return _animationFile; }
            set { _animationFile = value; }
        }

        private int _animationFPS = 60;
        public int AnimationFPS
        {
            get { return _animationFPS; }
            set { _animationFPS = value; }
        }

        private List<string> _animationNameList = new List<string>();
        public List<string> AnimationNameList
        {
            get { return _animationNameList; }
        }


    }
       
}
