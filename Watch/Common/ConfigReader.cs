using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Watch.Element;

namespace Watch
{
    class ConfigReader
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private string configFile;

        /// <summary>
        /// 资源文件夹路径
        /// </summary>
        private string resFolder;

        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        private int resolution = 0;

        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        public int Resolution { set { resolution = value; } get { return resolution; } }

        public ConfigReader(string configFile, string resFolder)
        {
            this.configFile = configFile;
            this.resFolder = resFolder;
        }

        public List<HWElement> Read()
        {
            List<HWElement> hWElements = new List<HWElement>();
            XmlDocument xml = new XmlDocument();
            //导入指定xml文件
            xml.Load(configFile);

            //指定一个节点
            XmlNode root = xml.SelectSingleNode("providers");
            XmlNode template = root.SelectSingleNode("TemplateWatch");
            var dpi = template.Attributes["dpi"].Value;
            if (string.IsNullOrWhiteSpace(dpi))
            {
                Resolution = dpi == "454" ? 0 : 1;
            }
            XmlNodeList nodelist = template.SelectNodes("Widget");

            foreach (XmlNode node in nodelist)
            {
                var ss = node.FirstChild;
                switch (ss.Name.ToLower())
                {
                    case "image": hWElements.Add(ConvertImage(ss)); break;
                    case "texturemapper": hWElements.Add(ConvertTextureMapper(ss)); break;
                    case "circle": hWElements.Add(ConvertCircle(ss)); break;
                    case "line": hWElements.Add(ConvertLine(ss)); break;
                    case "textareawithonewildcard": hWElements.Add(ConverTextareaWithOneWildCard(ss)); break;
                    case "box": hWElements.Add(ConvertBox(ss)); break;
                    case "selectimage": hWElements.Add(ConvertSelecteImage(ss)); break;
                    case "textareawithtwowildcard": hWElements.Add(ConvertTextareaWithTwoWildCard(ss)); break;
                }
            }
            return hWElements;
        }

        private HWBox ConvertBox(XmlNode node)
        {
            HWBox box = new HWBox();
            SetValue(box, node.Attributes);
            return box;
        }

        private HWCircle ConvertCircle(XmlNode node)
        {
            HWCircle box = new HWCircle();
            SetValue(box, node.Attributes);
            return box;
        }

        private HWImage ConvertImage(XmlNode node)
        {
            HWImage box = new HWImage();
            SetValue(box, node.Attributes);
            return box;
        }

        private HWLine ConvertLine(XmlNode node)
        {
            HWLine box = new HWLine();
            SetValue(box, node.Attributes);
            return box;
        }

        private HWSelectImage ConvertSelecteImage(XmlNode node)
        {
            HWSelectImage box = new HWSelectImage();
            SetValue(box, node.Attributes);
            XmlNode imageList = node.NextSibling;
            box.Res_names = new List<string>();
            foreach (XmlAttribute item in imageList.Attributes)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    box.Res_names.Add("");
                }
                else
                {
                    box.Res_names.Add(resFolder + item.Value);
                }
            }
            return box;
        }

        private HWTextareaWithOneWildCard ConverTextareaWithOneWildCard(XmlNode node)
        {
            HWTextareaWithOneWildCard box = new HWTextareaWithOneWildCard();
            SetValue(box, node.Attributes);
            return box;
        }
        private HWTextareaWithTwoWildCard ConvertTextareaWithTwoWildCard(XmlNode node)
        {
            HWTextareaWithTwoWildCard box = new HWTextareaWithTwoWildCard();
            SetValue(box, node.Attributes);
            return box;
        }
        private HWTextureMapper ConvertTextureMapper(XmlNode node)
        {
            HWTextureMapper box = new HWTextureMapper();
            SetValue(box, node.Attributes);
            return box;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attributes"></param>
        private void SetValue(Object obj, XmlAttributeCollection attributes)
        {
            foreach (PropertyInfo propertie in obj.GetType().GetProperties())
            {
                if (propertie.Name == "Description" || propertie.Name == "ClassName" || (obj is HWSelectImage && propertie.Name.ToLower() == "res_names"))
                {
                    continue;
                }
                try
                {
                    string value = attributes[propertie.Name.ToLower()].Value;
                    if (propertie.Name == "Res_name")
                    {
                        propertie.SetValue(obj, (string.IsNullOrWhiteSpace(value) ? "" : resFolder) + Convert.ChangeType(value, propertie.PropertyType), null);
                    }
                    else
                    {
                        propertie.SetValue(obj, Convert.ChangeType(value, propertie.PropertyType), null);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
