using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 图片旋转，如时分秒针等
    /// </summary>
    class HWTextureMapper : HWElement
    {
        /// <summary>
        /// 图片旋转绘制区域左上角X坐标
        /// </summary>
        public int Drawable_x { set; get; }
        /// <summary>
        /// 图片旋转绘制区域左上角Y坐标
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
        /// 旋转中心在图片上的x坐标
        /// </summary>
        public int Rotation_center_x { set; get; }
        /// <summary>
        /// 旋转中心在图片上的y坐标
        /// </summary>
        public int Rotation_center_y { set; get; }
        /// <summary>
        /// 旋转起始角度
        /// </summary>
        public float Begin_arc { set; get; }
        /// <summary>
        /// 旋转终止角度
        /// </summary>
        public float End_arc { set; get; }
        /// <summary>
        /// 订阅的数据
        /// </summary>
        public string Data_type { set; get; }
        /// <summary>
        /// 引用的图片名称
        /// </summary>
        public string Res_name { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "TEXTUREMAPPER";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "图片旋转，如时分秒针等";
    }
}
