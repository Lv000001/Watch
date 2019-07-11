using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.Entity
{
    /// <summary>
    /// 表盘描述信息
    /// </summary>
    public class WatchDescription
    {
        /// <summary>
        /// 表盘英文名，最终打包工具会基于这个生成压缩包
        /// </summary>
        public string Title { get; set; } = "Default";

        /// <summary>
        /// 表盘中文名
        /// </summary>
        public string Title_CN { get; set; } = "Default";

        /// <summary>
        /// 表盘开发者名称
        /// </summary>
        public string Author { get; set; } = "Default";

        /// <summary>
        /// 表盘设计师名称
        /// </summary>
        public string Designer { get; set; } = "Default";

        /// <summary>
        /// 表盘分辨率  HWHD01代表390px*390px、HWHD02代表454px*454px
        /// </summary>
        public string Screen { get; set; } = "HWHD02";

        /// <summary>
        /// 主题版本号规则：x.y.z
        /// x：设备GUI框架能力的标识。 运动手表454*454规格（GT、GT活力款）表盘取值为2；运动手表390*390（HONOR、GT雅致款）规格表盘取值为3
        /// y：作为设备表盘框架能力的版本号，用于标识设备上表盘框架能力的迭代更新，从1开始定义（当前版本固定为1）
        /// z：作为表盘资源包版本号，用于表盘版本更新，从1开始定义，由表盘作者自定义
        /// </summary>
        public string Version { get; set; } = "2.1.1";

        /// <summary>
        /// 表盘英文字体
        /// </summary>
        public string Font { get; set; } = "Default";

        /// <summary>
        /// 表盘中文字体
        /// </summary>
        public string Font_CN { get; set; } = "默认";

        /// <summary>
        /// 表盘简介
        /// </summary>
        public string Briefinfo { get; set; } = "Default";
    }
}
