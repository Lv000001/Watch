using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Watch.Element;
using Watch.Entity;

namespace Watch.Common
{
    class ConfigWriter
    {
        /// <summary>
        /// 导出文件夹
        /// </summary>
        private string folder = "";

        /// <summary>
        /// 表盘上的元素
        /// </summary>
        private ObservableCollection<WatchElement> watchElements;

        /// <summary>
        /// 图片is  key：图片路径  value:id
        /// </summary>
        Dictionary<string, string> imageIDs = new Dictionary<string, string>();

        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        private int resolution = 0;

        /// <summary>
        /// imageID格式
        /// </summary>
        private const string IMAGE_ID_FORMAT = "A100_{0:D3}.png";

        /// <summary>
        /// 表盘描述信息
        /// </summary>
        private WatchDescription watchDescription;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="folder">导出文件夹</param>
        /// <param name="watchElements">表盘上的元素</param>
        /// <param name="resolution">分辨率  0：454*454  1：390*390</param>
        public ConfigWriter(string folder, ObservableCollection<WatchElement> watchElements, WatchDescription watchDescription)
        {
            this.folder = folder;
            this.watchElements = watchElements;
            this.watchDescription = watchDescription;
            resolution = watchDescription.Screen == "HWHD01" ? 1 : 0;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        public void Write()
        {
            if (!folder.EndsWith("/"))
            {
                folder += "/";
            }
            string watchface = folder + "watchface";
            string watch_description = folder + "description.xml";
            string watch_face_config = watchface + "/watch_face_config.xml";
            string watch_face_info = watchface + "/watch_face_info.xml";
            string res = watchface + "/res/";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!Directory.Exists(watchface))
            {
                Directory.CreateDirectory(watchface);
            }

            if (!Directory.Exists(res))
            {
                Directory.CreateDirectory(res);
            }
            try
            {
                WriteConfig(watch_face_config);
                WriteDescription(watch_description);
                WriteDescription(watch_face_info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        /// <summary>
        /// 写入说明文件
        /// </summary>
        /// <param name="fileName"></param>
        private void WriteDescription(string fileName)
        {
            XmlDocument xml = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(xmldecl);

            //加入一个根元素
            XmlElement xmlRoot = xml.CreateElement("HwTheme");
            xml.AppendChild(xmlRoot);

            foreach (PropertyInfo propertie in watchDescription.GetType().GetProperties())
            {
                XmlElement xmlElement = xml.CreateElement(propertie.Name.ToLower().Replace("_", "-"));
                xmlElement.InnerText = propertie.GetValue(watchDescription).ToString();
                xmlRoot.AppendChild(xmlElement);
            }
            xml.Save(fileName);
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="fileName"></param>
        private void WriteConfig(string fileName)
        {
            XmlDocument xml = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(xmldecl);

            //加入一个根元素
            XmlElement xmlRoot = xml.CreateElement("providers");
            xml.AppendChild(xmlRoot);

            XmlElement xmlTemplate = xml.CreateElement("TemplateWatch");
            xmlTemplate.SetAttribute("dpi", resolution == 0 ? "454" : "390");
            xmlRoot.AppendChild(xmlTemplate);

            foreach (WatchElement item in watchElements)
            {
                XmlElement xmlelem = xml.CreateElement("Widget");
                xmlTemplate.AppendChild(xmlelem);
                if (item.HWElement is HWBox box)
                {
                    xmlelem.SetAttribute("widget_type", "BOX");
                    XmlElement xmlChild = xml.CreateElement("BOX");
                    SetXmlValue(box, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWCircle circel)
                {
                    xmlelem.SetAttribute("widget_type", "CIRCLE");
                    XmlElement xmlChild = xml.CreateElement("Circle");
                    SetXmlValue(circel, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWImage image)
                {
                    xmlelem.SetAttribute("widget_type", "IMAGE");
                    XmlElement xmlChild = xml.CreateElement("Image");
                    SetXmlValue(image, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWLine line)
                {
                    xmlelem.SetAttribute("widget_type", "LINE");
                    XmlElement xmlChild = xml.CreateElement("Line");
                    SetXmlValue(line, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWSelectImage selecteImage)
                {
                    xmlelem.SetAttribute("widget_type", "SELECTIMAGE");
                    XmlElement xmlChild = xml.CreateElement("SelectImage");
                    SetXmlValue(selecteImage, xmlChild);
                    xmlelem.AppendChild(xmlChild);


                    xmlChild = xml.CreateElement("ImagesList");
                    string watchface = folder + "watchface";
                    string res = watchface + "/res/";

                    for (int i = 0; i < 15; i++)
                    {
                        if (i < selecteImage.Res_names.Count)
                        {
                            if (!string.IsNullOrWhiteSpace(selecteImage.Res_names[i]))
                            {
                                if (!imageIDs.ContainsKey(selecteImage.Res_names[i]))
                                {
                                    string imageId = string.Format(IMAGE_ID_FORMAT, imageIDs.Count + 2);
                                    imageIDs.Add(selecteImage.Res_names[i], imageId);
                                    File.Copy(selecteImage.Res_names[i], res + imageId, true);
                                }
                                xmlChild.SetAttribute("res_" + i, imageIDs[selecteImage.Res_names[i]]);
                            }
                            else
                            {
                                xmlChild.SetAttribute("res_" + i, "");
                            }
                        }
                        else
                        {
                            xmlChild.SetAttribute("res_" + i, "");
                        }
                    }

                    xmlelem.AppendChild(xmlChild);

                }

                if (item.HWElement is HWTextareaWithOneWildCard textareaWithOneWildCard)
                {
                    xmlelem.SetAttribute("widget_type", "TEXTAREAWITHONEWILDCARD");
                    XmlElement xmlChild = xml.CreateElement("TextAreaWithOneWildCard");
                    SetXmlValue(textareaWithOneWildCard, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWTextareaWithTwoWildCard textareaWithTwoWildCard)
                {
                    xmlelem.SetAttribute("widget_type", "TEXTAREAWITHTWOWILDCARD");
                    XmlElement xmlChild = xml.CreateElement("TextAreaWithTwoWildCard");
                    SetXmlValue(textareaWithTwoWildCard, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }

                if (item.HWElement is HWTextureMapper textureMapper)
                {
                    xmlelem.SetAttribute("widget_type", "TEXTUREMAPPER");
                    XmlElement xmlChild = xml.CreateElement("TextureMapper");
                    SetXmlValue(textureMapper, xmlChild);
                    xmlelem.AppendChild(xmlChild);
                }
            }

            xml.Save(fileName);
        }

        /// <summary>
        /// 设置Xml值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attributes"></param>
        private void SetXmlValue(Object obj, XmlElement xmlChild)
        {
            string watchface = folder + "watchface";
            string res = watchface + "/res/";
            foreach (PropertyInfo propertie in obj.GetType().GetProperties())
            {
                if (obj is HWSelectImage selecteImage && propertie.Name == "Res_names")
                {
                    continue;
                }

                if (propertie.Name == "Res_name")
                {
                    string value = propertie.GetValue(obj).ToString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (!imageIDs.ContainsKey(value))
                        {
                            string imageId = string.Format(IMAGE_ID_FORMAT, imageIDs.Count + 2);
                            imageIDs.Add(value, imageId);
                            File.Copy(value, res + imageId, true);
                        }
                        xmlChild.SetAttribute("res_name", imageIDs[value]);
                    }
                    else
                    {
                        xmlChild.SetAttribute("res_name", "");
                    }
                    continue;
                }

                if (propertie.Name == "Description" || propertie.Name == "ClassName")
                {
                    continue;
                }
                try
                {
                    xmlChild.SetAttribute(propertie.Name.ToLower(), propertie.GetValue(obj).ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
