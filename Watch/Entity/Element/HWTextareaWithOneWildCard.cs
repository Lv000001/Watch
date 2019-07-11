using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 动态文本框，用于显示变化的文字，如步数、心率等的数值
    /// </summary>
    class HWTextareaWithOneWildCard : HWElement
    {
        /// <summary>
        /// 文本框左上角X坐标
        /// </summary>
        public int Drawable_x { set; get; }
        /// <summary>
        /// 文本框左上角Y坐标
        /// </summary>
        public int Drawable_y { set; get; }
        /// <summary>
        /// 文本框宽度
        /// </summary>
        public int Drawable_width { set; get; }
        /// <summary>
        /// 文本框高度
        /// </summary>
        public int Drawable_height { set; get; }
        /// <summary>
        /// 文本颜色的红色分量值
        /// </summary>
        public byte Color_red { set; get; }
        /// <summary>
        /// 文本颜色的绿色分量值
        /// </summary>
        public byte Color_green { set; get; }
        /// <summary>
        /// 文本颜色的蓝色分量值
        /// </summary>
        public byte Color_blue { set; get; }
        /// <summary>
        /// 订阅的数据
        /// </summary>
        public string Data_type { set; get; }
        /// <summary>
        /// 行间距
        /// </summary>
        public int Line_spacing { set; get; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string Alignment_type { set; get; }
        /// <summary>
        /// 字体字号
        /// </summary>
        public string Font_type { set; get; }
        /// <summary>
        /// 文本的透明度值
        /// </summary>
        public byte Alpha { set; get; } = 255;

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "TEXTAREAWITHONEWILDCARDOX";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "动态文本框，用于显示变化的文字，如步数、心率等的数值";
    }
}
