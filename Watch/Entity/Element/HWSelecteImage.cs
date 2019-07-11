using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Element
{
    /// <summary>
    /// 随着订阅的数据类型的数据改变，显示不同的图片
    /// </summary>
    class HWSelectImage : HWElement
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
        /// 订阅的数据
        /// </summary>
        public string Data_type { set; get; }

        /// <summary>
        /// 序列帧图片ID
        /// 图片列表最大支持15幅，未用到的直接留空。
        /// 使用图片绘制数字时间时，需要提供0 ~9十个数字的图片资源；
        /// 使用图片绘制文字日期时，需提供12个月份、7个星期的对应的图片资源，
        /// 所有图片资源必须按顺序提供，如A100_020、A100_021、A100_022、……
        /// </summary>
        public List<string> Res_names { set; get; } = new List<string> { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        /// <summary>
        /// 类名
        /// </summary>
        public new string ClassName { get; } = "SELECTEIMAGE";

        /// <summary>
        /// 描述用途
        /// </summary>
        public new string Description { set; get; } = "随着订阅的数据类型的数据改变，显示不同的图片";

    }
}
