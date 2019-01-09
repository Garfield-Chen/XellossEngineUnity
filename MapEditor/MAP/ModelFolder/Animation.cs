using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace tyoEngineEditor
{
    public class Animation
    {
        private static string animationVersion = string.Empty;

        public static Dictionary<string, AnimationInfo> ReadAnimationXML(string path)
        {
            Dictionary<string, AnimationInfo> animationInfos = new Dictionary<string, AnimationInfo>();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(path);
            XmlNode rootNode = myXmlDoc.SelectSingleNode("root");
            animationVersion = rootNode.Attributes["version"].Value;
            switch (animationVersion)
            {
                case "1":
                    LoadNewVerison_1(myXmlDoc, path, animationInfos);
                    break;
                default:
                    LoadNewVerison_1(myXmlDoc, path, animationInfos);
                    break;
            }

            return animationInfos;
        }

        public static AnimationInfo GetSpeicalAnimation(string path, string name, string direction)
        {
            AnimationInfo animationInfo = null;

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(path);
            XmlNode rootNode = myXmlDoc.SelectSingleNode("root");
            animationVersion = rootNode.Attributes["version"].Value;
            switch (animationVersion)
            {
                case "1":
                    animationInfo = GetSpeicalAnimation_1(path, name, direction);
                    break;
                default:
                    break;
            }

            return animationInfo;
        }

        public static string GetAnimationVersion()
        {
            return animationVersion;
        }

        public static string TransDirectionCN2EN(string name)
        {
            string trans = string.Empty;
            switch (name)
            {
                case "0-无":
                    trans = "none";
                    break;
                case "1-上":
                    trans = "up";
                    break;
                case "2-右上":
                    trans = "rightUp";
                    break;
                case "3-右":
                    trans = "right";
                    break;
                case "4-右下":
                    trans = "rightDown";
                    break;
                case "5-下":
                    trans = "down";
                    break;
                case "6-左下":
                    trans = "leftDown";
                    break;
                case "7-左":
                    trans = "left";
                    break;
                case "8-左上":
                    trans = "leftUp";
                    break;
                default:
                    trans = name;
                    break;

            }
            return trans;
        }

        public static string TransDirectionEN2CN(string name)
        {
            string trans = string.Empty;
            switch (name)
            {
                case "none":
                    trans = "0-无";
                    break;
                case "up":
                    trans = "1-上";
                    break;
                case "rightUp":
                    trans = "2-右上";
                    break;
                case "right":
                    trans = "3-右";
                    break;
                case "rightDown":
                    trans = "4-右下";
                    break;
                case "down":
                    trans = "5-下";
                    break;
                case "leftDown":
                    trans = "6-左下";
                    break;
                case "left":
                    trans = "7-左";
                    break;
                case "leftUp":
                    trans = "8-左上";
                    break;

            }
            return trans;
        }

        private static void LoadNewVerison_1(XmlDocument myXmlDoc, string path, Dictionary<string, AnimationInfo> animationInfos)
        {
            DirectoryInfo dir = Directory.GetParent(path);
            string pngPath = string.Empty, key = string.Empty;
            int indexID = 0;

            AnimationInfo animationInfo;
            AnimationImageInfo animationImageInfo;

            Graphics graphics;
            XmlNode rootNode = myXmlDoc.SelectSingleNode("root");

            XmlNodeList firstLevelNodeList = rootNode.ChildNodes;

            int index = 0;

            int firstX = 0, firstY = 0, firstW = 0, firstH = 0;

            foreach (XmlNode node in firstLevelNodeList)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }
                animationInfo = new AnimationInfo();
                animationInfo.xmlPath = path.Substring(path.LastIndexOf('\\'));
                animationInfo.name = node.Name;
                animationInfo.direction = TransDirectionEN2CN(node.Attributes["direction"].Value);

                for (int i = 0; i < node.ChildNodes.Count; i++)
                {

                    pngPath = node.ChildNodes[i].ChildNodes[8].InnerText;
                    using (Bitmap bitmapBig = (Bitmap)Image.FromFile(dir.FullName + "\\" + pngPath))
                    {
                        animationImageInfo = new AnimationImageInfo();
                        animationImageInfo.orderIndex = int.Parse(node.ChildNodes[i].Attributes["value"].Value.ToString());

                        animationImageInfo.xClone = int.Parse(node.ChildNodes[i].ChildNodes[0].InnerText);
                        animationImageInfo.yClone = int.Parse(node.ChildNodes[i].ChildNodes[1].InnerText);
                        animationImageInfo.x = int.Parse(node.ChildNodes[i].ChildNodes[2].InnerText);
                        animationImageInfo.y = int.Parse(node.ChildNodes[i].ChildNodes[3].InnerText);
                        animationImageInfo.w = int.Parse(node.ChildNodes[i].ChildNodes[4].InnerText);
                        animationImageInfo.h = int.Parse(node.ChildNodes[i].ChildNodes[5].InnerText);
                        animationImageInfo.originalW = int.Parse(node.ChildNodes[i].ChildNodes[6].InnerText);
                        animationImageInfo.originalH = int.Parse(node.ChildNodes[i].ChildNodes[7].InnerText);
                        animationImageInfo.path = node.ChildNodes[i].ChildNodes[8].InnerText;
                        //animationImageInfo.name = node.ChildNodes[i].Attributes["value"].Value.ToString() + node.Attributes["direction"].Value;
                        animationImageInfo.name = node.ChildNodes[i].ChildNodes[9].InnerText;

                        Bitmap bitmap = new Bitmap(animationImageInfo.w, animationImageInfo.h);
                        graphics = Graphics.FromImage(bitmap);
                        graphics.DrawImage(bitmapBig, new Rectangle(0, 0, animationImageInfo.w, animationImageInfo.h)
                            , new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone, animationImageInfo.w, animationImageInfo.h),
                            GraphicsUnit.Pixel);

                        animationImageInfo.bitmap = bitmap;


                        bitmap = new Bitmap(animationImageInfo.originalW, animationImageInfo.originalH);
                        graphics = Graphics.FromImage(bitmap);
                        graphics.DrawImage(bitmapBig, new Rectangle(animationImageInfo.x, animationImageInfo.y, animationImageInfo.w, animationImageInfo.h),
                            new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone, animationImageInfo.w, animationImageInfo.h),
                            GraphicsUnit.Pixel);
                        animationImageInfo.originalBitmap = bitmap;
                        animationInfo.images.Add(animationImageInfo);

                    }
                    if (i == 0)
                    {
                        firstX = animationImageInfo.x;
                        firstY = animationImageInfo.y;
                        firstW = animationImageInfo.w;
                        firstH = animationImageInfo.h;
                    }
                    CreateOnpanelBitmap(animationImageInfo, animationInfo, firstW, firstH, firstX, firstY);

                }

                if (key != animationInfo.name && !key.Equals(string.Empty))
                {
                    indexID++;
                }
                key = animationInfo.name;
                animationInfos.Add(indexID.ToString() + animationInfo.direction, animationInfo);
                index++;
            }
        }

        private static void CreateOnpanelBitmap(AnimationImageInfo animationImageInfo,
            AnimationInfo animationInfo, int w, int h, int x, int y)
        {
            Bitmap bitmap = new Bitmap(w, h);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(animationImageInfo.originalBitmap, new Rectangle(0, 0, w, h),
                new Rectangle(x, y, w, h),
                GraphicsUnit.Pixel);
            animationImageInfo.onPanelBitmap = bitmap;
            animationInfo.images.Add(animationImageInfo);
        }

        private static AnimationInfo GetSpeicalAnimation_1(string path, string name, string direction)
        {
            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(path);
            XmlNode rootNode = myXmlDoc.SelectSingleNode("root");

            XmlNodeList firstLevelNodeList = rootNode.ChildNodes;
            AnimationInfo animationInfo = null;
            AnimationImageInfo animationImageInfo = null;
            string pngPath = string.Empty;

            DirectoryInfo dir = Directory.GetParent(path);
            Graphics graphics;

            int index = 0;

            int firstX = 0, firstY = 0, firstW = 0, firstH = 0;

            foreach (XmlNode node in firstLevelNodeList)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }

                if (node.Name == name && node.Attributes["direction"].Value == direction)
                {
                    animationInfo = new AnimationInfo();
                    animationInfo.name = node.Name;
                    animationInfo.direction = TransDirectionEN2CN(node.Attributes["direction"].Value);

                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        pngPath = node.ChildNodes[i].ChildNodes[8].InnerText;
                        using (Bitmap bitmapBig = (Bitmap)Image.FromFile(dir.FullName + "\\" + pngPath))
                        {
                            animationImageInfo = new AnimationImageInfo();
                            animationImageInfo.orderIndex = int.Parse(node.ChildNodes[i].Attributes["value"].Value.ToString());

                            animationImageInfo.xClone = int.Parse(node.ChildNodes[i].ChildNodes[0].InnerText);
                            animationImageInfo.yClone = int.Parse(node.ChildNodes[i].ChildNodes[1].InnerText);
                            animationImageInfo.x = int.Parse(node.ChildNodes[i].ChildNodes[2].InnerText);
                            animationImageInfo.y = int.Parse(node.ChildNodes[i].ChildNodes[3].InnerText);
                            animationImageInfo.w = int.Parse(node.ChildNodes[i].ChildNodes[4].InnerText);
                            animationImageInfo.h = int.Parse(node.ChildNodes[i].ChildNodes[5].InnerText);
                            animationImageInfo.originalW = int.Parse(node.ChildNodes[i].ChildNodes[6].InnerText);
                            animationImageInfo.originalH = int.Parse(node.ChildNodes[i].ChildNodes[7].InnerText);
                            animationImageInfo.path = node.ChildNodes[i].ChildNodes[8].InnerText;
                            //animationImageInfo.name = node.ChildNodes[i].Attributes["value"].Value.ToString() + node.Attributes["direction"].Value;
                            animationImageInfo.name = node.ChildNodes[i].ChildNodes[9].InnerText;

                            Bitmap bitmap = new Bitmap(animationImageInfo.w, animationImageInfo.h);

                            graphics = Graphics.FromImage(bitmap);
                            graphics.DrawImage(bitmapBig, new Rectangle(0, 0, animationImageInfo.w, animationImageInfo.h)
                                , new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone, animationImageInfo.w, animationImageInfo.h),
                                GraphicsUnit.Pixel);

                            animationImageInfo.bitmap = bitmap;


                            bitmap = new Bitmap(animationImageInfo.originalW, animationImageInfo.originalH);

                            graphics = Graphics.FromImage(bitmap);
                            graphics.DrawImage(bitmapBig, new Rectangle(animationImageInfo.x, animationImageInfo.y, animationImageInfo.w, animationImageInfo.h),
                                new Rectangle(animationImageInfo.xClone, animationImageInfo.yClone, animationImageInfo.w, animationImageInfo.h),
                                GraphicsUnit.Pixel);
                            animationImageInfo.originalBitmap = bitmap;

                            
                        }
                        if (i == 0)
                        {
                            firstX = animationImageInfo.x;
                            firstY = animationImageInfo.y;
                            firstW = animationImageInfo.w;
                            firstH = animationImageInfo.h;
                        }
                        CreateOnpanelBitmap(animationImageInfo, animationInfo, firstW, firstH, firstX, firstY);

                        //animationInfo.images.Add(animationImageInfo);
                    }
                }

                index++;
            }
            return animationInfo;
        }
    }
}
