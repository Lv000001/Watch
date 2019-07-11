using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 带连接符的动态文本框，如：XX:XX格式的时间显示，XX/XX格式的日期显示。
    /// </summary>
    class HWTextareaWithTwoWildCard : HWElement
    { /// <summary>
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
        /// 第二个订阅的数据
        /// </summary>
        public string Data2_type { set; get; }
        /// <summary>
        /// 行间距
        /// </summary>
        public int Line_spacing { set; get; }

        /// <summary>
        /// 两个数据之间的连接符
        /// CONN_COLON		// 冒号	“:”
        /// CONN_STUB       // 短线	“-”
        /// CONN_SLASH		// 斜线	“/”
        /// CONN_BACKSLASH	// 反斜线“\”
        /// CONN_POINT		// 点	“.”
        /// CONN_PERCENT		// 百分号“%”
        /// CONN_SPACE		// 空格	“ ”
        /// </summary>
        public string Data_connector_type { set; get; }

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
        public new string ClassName { get; } = "TEXTAREAWITHTWOWILDCARDOX";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "带连接符的动态文本框，如：XX:XX格式的时间显示，XX/XX格式的日期显示";
    }
}
