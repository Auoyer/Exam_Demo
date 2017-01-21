using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class TheoryErrorRate
    {
        /// <summary>
        /// 题干
        /// </summary>
        public string QuestionsName { get; set; }

        /// <summary>
        /// 题型ID
        /// </summary>
        public int StructType { get; set; }

        /// <summary>
        /// 题型
        /// </summary>
        public string QuestionsTypeName { get; set; }
        /// <summary>
        /// 错误率
        /// </summary>
        public  string ErrorRate { get; set; }
    }
}
