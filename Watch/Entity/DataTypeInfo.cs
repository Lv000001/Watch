using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch
{
    class DataTypeInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { set; get; }
        /// <summary>
        /// 取值范围
        /// </summary>
        public string Range { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public int Value { set; get; }
    }
}
