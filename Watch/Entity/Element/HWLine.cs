using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 线形进度条，用于步数、卡路里等的目标完成进度显示
    /// </summary>
    class HWLine : HWElement
    {
        /// <summary>
        /// 控件左上角X坐标
        /// </summary>
        public int Drawable_x { set; get; }
        /// <summary>
        /// 控件左上角Y坐标
        /// </summary>
        public int Drawable_y { set; get; }
        /// <summary>
        /// 控件宽度
        /// </summary>
        public int Drawable_width { set; get; }
        /// <summary>
        /// 控件高度
        /// </summary>
        public int Drawable_height { set; get; }
        /// <summary>
        /// 订阅的数据
        /// </summary>
        public string Data_type { set; get; }
        /// <summary>
        /// 引用的图片ID
        /// </summary>
        public string Res_name { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "LINE";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "线形进度条，用于步数、卡路里等的目标完成进度显示";
    }
}
