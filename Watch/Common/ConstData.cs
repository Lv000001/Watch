using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch
{
    class ConstData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public static Dictionary<string, int> Datas = new Dictionary<string, int>();

        /// <summary>
        /// DataType描述(数据类型)
        /// </summary>
        public static Dictionary<string, string> DataTypeDescriptions = new Dictionary<string, string>();

        /// <summary>
        /// DataType描述（百分比类型）
        /// </summary>
        public static Dictionary<string, string> RatioDataTypeDescriptions = new Dictionary<string, string>();

        /// <summary>
        /// 字体
        /// </summary>
        public static List<string> Fonts = new List<string>
        {
            "F_DINCONDENSED_BOLD",
            "F_DINNEXTFORHUAWEI_BOLD",
            "F_DIN_BLACKITALIC",
            "F_DIN_BLACKALTERNATE",
            "F_DINNEXTLTPRO_MEDIUM",
            "F_DINNEXTLTPRO_REGULAR",
            "F_EUROSTILELT_DEMI",
            "F_EUROSTILELT_BOLD_EXTENDED2",
            "F_HYQIHEI_60S",
            "F_ROBOTOCONDENSED_REGULAR"
        };

        /// <summary>
        /// 对齐方式
        /// </summary>
        public static Dictionary<string, string> Alignments = new Dictionary<string, string>
        {
            { "左对齐","LEFT"},
            { "居中","CENTER"},
            { "右对齐","RIGHT"},
        };

        /// <summary>
        /// 两个订阅类型的连接字符串
        /// </summary>
        public static Dictionary<string, string> Connector = new Dictionary<string, string>
        {
            { "冒号","CONN_COLON"},
            { "短线","CONN_STUB"},
            { "斜线 /","CONN_SLASH"},
            { "反斜线 \\","CONN_BACKSLASH"},
            { "点","CONN_POINT"},
            { "百分号","CONN_PERCENT"},
            { "空格","CONN_SPACE"},
        };

        /// <summary>
        /// 454分辨率对应的字体尺寸
        /// </summary>
        public static Dictionary<string, List<int>> FontSize454 = new Dictionary<string, List<int>>
        {
           { "F_DINCONDENSED_BOLD",new List<int>{38,42,44,50,60,70,82,90,106,116} }
           ,{ "F_DINNEXTFORHUAWEI_BOLD",new List<int>{16,22,24,28,40,80} }
           ,{ "F_DIN_BLACKITALIC",new List<int>{32,84} }
           ,{ "F_DIN_BLACKALTERNATE",new List<int>{52} }
           ,{ "F_DINNEXTLTPRO_MEDIUM",new List<int>{32} }
           ,{ "F_DINNEXTLTPRO_REGULAR",new List<int>{24,46} }
           ,{ "F_EUROSTILELT_DEMI",new List<int>{14,20,32,34} }
           ,{ "F_EUROSTILELT_BOLD_EXTENDED2",new List<int>{42} }
           ,{ "F_HYQIHEI_60S",new List<int>{20,23,26,28,30,38,48} }
           ,{ "F_ROBOTOCONDENSED_REGULAR",new List<int>{ 20, 23, 26, 28, 30, 38, 48 } }
        };


        /// <summary>
        /// 390分辨率对应的字体尺寸
        /// </summary>
        public static Dictionary<string, List<int>> FontSize390 = new Dictionary<string, List<int>>
        {
           { "F_DINCONDENSED_BOLD",new List<int>{ 32, 36, 44, 54, 60, 68, 70, 76, 91, 100 } }
           ,{ "F_DINNEXTFORHUAWEI_BOLD",new List<int>{24,34,68} }
           ,{ "F_DIN_BLACKITALIC",new List<int>{18,22,28,72} }
           ,{ "F_DIN_BLACKALTERNATE",new List<int>{44} }
           ,{ "F_DINNEXTLTPRO_MEDIUM",new List<int>{28} }
           ,{ "F_DINNEXTLTPRO_REGULAR",new List<int>{22,40} }
           ,{ "F_EUROSTILELT_DEMI",new List<int>{ 14, 20, 22, 26, 28, 32, 34 } }
           ,{ "F_HYQIHEI_60S",new List<int>{ 18, 20, 22, 24, 26, 32, 36, 42 } }
           ,{ "F_ROBOTOCONDENSED_REGULAR",new List<int>{ 18, 20, 22, 24, 26, 32, 36, 42 } }
        };

        public const string DEFAULT_VALUE = "步数\tDATA_STEPS\t[0-65535]\t5442\n" +
                "卡路里\tDATA_CALORIE\t[0-65535]\t5445\n" +
                "心率\tDATA_HEARTRATE\t(0-255]\t55\n" +
                "中高强度时间\tDATA_STRENTHTIME\t[0-65535]\t8544\n" +
                "气温\tDATA_TEMPERATURE\t[-32678- 32678]\t21\n" +
                "PM2.5\tDATA_PM25\t[0-500]\t85\n" +
                "AQI\tDATA_AQI\t[0-500]\t58\n" +
                "气压\tDATA_PRESSURE\t[0-65535]\t4689\n" +
                "海拔\tDATA_AILTITUDE\t[-32678- 32678]\t4568\n" +
                "电量\tDATA_POWER\t[0-100]\t94\n" +
                "时钟24\tDATA_HOUR24\t[0-23]\t4\n" +
                "时钟12\tDATA_HOUR12\t[0-11]\t6\n" +
                "时钟\tDATA_HOUR\t[0-23]\t5\n" +
                "分钟\tDATA_MINITE\t[0-59]\t50\n" +
                "秒钟\tDATA_SECOND\t[0-59]\t6\n" +
                "站立次数\tDATA_STANDUPTIMES\t(0-255]\t78\n" +
                "最大摄氧量\tDATA_VO2MAX\t(0-80]\t36\n" +
                "日期\tDATA_DATE\t[1-31]\t12\n" +
                "最大心率\tDATA_HEARTRATE_MAX\t(0-255]\t58\n" +
                "最小心率\tDATA_HEARTRATE_MIN\t(0-255]\t25\n" +
                "上午下午\tDATA_AMPM\t[0-1]\t1\n" +
                "月\tDATA_MONTH\t[1-12]\t6\n" +
                "星期数据\tDATA_WEEK\t[1-7]\t4\n" +
                "天气类型\tDATA_WEATHERTYPE\t[0-11]\t11\n" +
                "电量枚举\tDATA_POWER_ENUM\t[0-10]\t5\n" +
                "时钟高位12\tDATA_HOUR12_HIGH\t[0-9]\t3\n" +
                "时钟低位12\tDATA_HOUR12_LOW\t[0-9]\t4\n" +
                "时钟高位24\tDATA_HOUR24_HIGH\t[0-9]\t5\n" +
                "时钟低位24\tDATA_HOUR24_LOW\t[0-9]\t2\n" +
                "时钟高位\tDATA_HOUR_HIGH\t[0-9]\t1\n" +
                "时钟低位\tDATA_HOUR_LOW\t[0-9]\t2\n" +
                "分钟高位\tDATA_MINITE_HIGH\t[0-9]\t7\n" +
                "分钟低位\tDATA_MINITE_LOW\t[0-9]\t5\n" +
                "秒钟高位\tDATA_SECOND_HIGH\t[0-9]\t1\n" +
                "秒钟低位\tDATA_SECOND_LOW\t[0-9]\t5\n" +
                "步数个位\tDATA_STEPS_ONE\t[0-9]\t7\n" +
                "步数十位\tDATA_STEPS_TWO\t[0-9]\t8\n" +
                "步数百位\tDATA_STEPS_THREE\t[0-9]\t7\n" +
                "步数千位\tDATA_STEPS_FOUR\t[0-9]\t5\n" +
                "步数万位\tDATA_STEPS_FIVE\t[0-9]\t2\n" +
                "日期高位\tDATA_DATE_HIGH\t[0-3]\t6\n" +
                "日期低位\tDATA_DATE_LOW\t[0-9]\t5\n" +
                "未读消息\tDATA_UNREADMSG_STATE\t[0-1]\t0\n" +
                "温度类型\tDATA_TEMPERATURE_TYPE\t[0-1]\t0\n" +
                "时钟比例12\tDATA_HOUR12_RATIO\t[00.00%-100.00%]\t25\n" +
                "时钟比例24\tDATA_HOUR24_RATIO\t[00.00%-100.00%]\t41\n" +
                "时钟比例\tDATA_HOUR_RATIO\t[00.00%-100.00%]\t52\n" +
                "分钟比例\tDATA_MINITE_RATIO\t[00.00%-100.00%]\t52\n" +
                "秒钟比例\tDATA_SECOND_RATIO\t[00.00%-100.00%]\t72\n" +
                "日期比例\tDATA_DATE_RATIO\t[00.00%-100.00%]\t32\n" +
                "星期比例\tDATA_WEEK_RATIO\t[00.00%-100.00%]\t41\n" +
                "电量比例\tDATA_POWER_RATIO\t[00.00%-100.00%]\t52\n" +
                "心率比例\tDATA_HEARTRATE_RATIO\t[00.00%-100.00%]\t82\n" +
                "卡路里比例\tDATA_CALORIE_RATIO\t[00.00%-100.00%]\t2\n" +
                "站立次数比例\tDATA_STANDUPTIMES_RATIO\t[00.00%-100.00%]\t58\n" +
                "中高强度时间比例\tDATA_STRENTHTIME_RATIO\t[00.00%-100.00%]\t71\n" +
                "步数目标达成度\tDATA_STEPS_RATIO\t[00.00%-100.00%]\t51\n" +
                "最大摄氧量比例\tDATA_VO2MAX_RATIO\t[00.00%-100.00%]\t52\n" +
                "无数据\tDATA_NULL\t无数据\t0";

    }
}
