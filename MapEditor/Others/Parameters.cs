using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace tyoEngineEditor
{
    public class Parameters
    {
        /// <summary>
        /// 鼠标滚动刻度
        /// </summary>
        public const int mouseWheelScale = 55;

        /// <summary>
        /// 加载地图需要10步
        /// </summary>
        public const int loadMapCount = 11;

        /// <summary>
        /// 加载怪兽的路径
        /// </summary>
        public static string monsterPath = System.Environment.CurrentDirectory
            + "\\MAPResource\\AllProps\\Monsters";

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public const string connectionStr = "Database='mapeditor';Data Source='192.168.20.145';"
        + "User Id='root';Password='1';charset='utf8';pooling=true";

        public const string MODIFYTYPE = "modifyType";

        public const string PICFILE = "\\PIC";

        public enum OPERATE
        {
            ADD,
            DELETE,
            UPDATE
        }

        public enum MAGICTYPE 
        {
            WARRIOR,
            WARLOCK,
            MASTER
        }


        public const string ITEMS = "ITEMS";
        public const string MONSTOR = "MONSTOR";
        public const string MAGIC = "MAGIC";

        /// <summary>
        /// 系统使用，用于判断在编辑地图时，是否加载图层和图块及图块文件夹
        /// </summary>
        public const string SYSYTEMUSE = "SYSTEM";

        public const string FILTERSTR = "所有文件 (*.*)|*.*|BMP (*.bmp)|*.bmp|PNG (*.png)|*.png";
        public const string FILTERXML = "XML文件 |*.xml|所有文件 (*.*)|*.*";

        public const string SUCCESS = "SUCCESS";

        public const int PICSIZE = 2048;

        public const int HEIGHTSPACING = 50;

        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
    }
}
