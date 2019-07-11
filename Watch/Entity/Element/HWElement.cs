using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Watch.Element
{
    public class HWElement
    {
        /// <summary>
        /// 用于描述这个元素具体是什么元素
        /// </summary>
        public string Label { set; get; }

        /// <summary>
        /// 描述控件是静态还是动态，运动手表中固定为static
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        
    }
}
