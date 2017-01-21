using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 显示已选择数量
    /// </summary>
    public class SelNumVM
    {
        /// <summary>
        /// 章节ID
        /// </summary>
        public int CharpterID { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string CharpterName { get; set; }
        /// <summary>
        /// 已选择数量
        /// </summary>
        public int Num { get; set; }
    }
}
