using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 背景框，用于显示背景色
    /// </summary>
    class HWBox : HWElement
    {
        /// <summary>
        /// 背景框左上角X坐标
        /// </summary>
        public int Drawable_x { set; get; }
        /// <summary>
        /// 背景框左上角Y坐标
        /// </summary>
        public int Drawable_y { set; get; }
        /// <summary>
        /// 背景框宽度
        /// </summary>
        public int Drawable_width { set; get; }
        /// <summary>
        /// 背景框高度
        /// </summary>
        public int Drawable_height { set; get; }
        /// <summary>
        /// 背景颜色的红色分量值
        /// </summary>
        public byte Color_red { set; get; }
        /// <summary>
        /// 背景颜色的绿色分量值
        /// </summary>
        public byte Color_green { set; get; }
        /// <summary>
        /// 背景颜色的蓝色分量值
        /// </summary>
        public byte Color_blue { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "BOX";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "背景框，用于显示背景色";

    }
}
