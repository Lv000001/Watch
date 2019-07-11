using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 圆形进度条，用于步数、卡路里等的目标完成进度显示
    /// </summary>
    class HWCircle : HWElement
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
        /// 图片旋转绘制区域宽度
        /// </summary>
        public int Drawable_width { set; get; }
        /// <summary>
        /// 图片旋转绘制区域高度
        /// </summary>
        public int Drawable_height { set; get; }
        /// <summary>
        /// 圆形进度条的圆心位置X坐标
        /// </summary>
        public int Circle_x { set; get; }
        /// <summary>
        /// 圆形进度条的圆心位置X坐标
        /// </summary>
        public int Circle_y { set; get; }
        /// <summary>
        /// 圆形进度条的半径
        /// </summary>
        public int Circle_r { set; get; }
        /// <summary>
        /// 圆形进度条的宽度
        /// </summary>
        public int Line_width { set; get; }
        /// <summary>
        /// 圆形进度条范围的起始角度
        /// </summary>
        public int Arc_start { set; get; }
        /// <summary>
        /// 圆形进度条范围的结束角度
        /// </summary>
        public int Arc_end { set; get; }
        /// <summary>
        /// 圆形进度条的默认角度
        /// </summary>
        public int Update_arc_start { set; get; }
        /// <summary>
        /// 设置Circle绘制功能的精度。精度是以度为单位的，默认值为5，值越高，圆圈步进越大，但渲染速度越快。
        /// </summary>
        public int Precision { set; get; }
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
        public new string ClassName { get; } = "CIRCLE";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "圆形进度条，用于步数、卡路里等的目标完成进度显示";
    }
}
