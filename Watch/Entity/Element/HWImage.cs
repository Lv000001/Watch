using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 静态图，如背景图、图标等
    /// </summary>
    public class HWImage : HWElement
    {
        /// <summary>
        /// 图片左上角X坐标
        /// </summary>
        public int X { set; get; }

        /// <summary>
        /// 图片左上角Y坐标
        /// </summary>
        public int Y { set; get; }

        /// <summary>
        /// 引用的图片名称
        /// </summary>
        public string Res_name { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "IMAGE";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "静态图，如背景图、图标等";
    }
}
