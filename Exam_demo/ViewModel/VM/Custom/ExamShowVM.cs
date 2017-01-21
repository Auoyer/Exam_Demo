using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Custom
{
    /// <summary>
    /// 试卷展示Model
    /// </summary>
    public class ExamShowVM
    {
        public ExamShowVM()
        {
            Flag = true;
            Current = new KeyValue();
            Prev = new KeyValue();
            Next = new KeyValue();
            CostTime = 0;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Flag { get; set; }
        /// <summary>
        /// 问题Model
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 当前问题
        /// 键：题目类型，值：问题ID
        /// </summary>
        public KeyValue Current { get; set; }
        /// <summary>
        /// 上一题
        /// 键：题目类型，值：问题ID
        /// </summary>
        public KeyValue Prev { get; set; }
        /// <summary>
        /// 下一题
        /// 键：题目类型，值：问题ID
        /// </summary>
        public KeyValue Next { get; set; }
        /// <summary>
        /// 用户答案1
        /// </summary>
        public List<int> Answers { get; set; }

        /// <summary>
        /// 用户答案2
        /// </summary>
        public List<string> Answers2 { get; set; }

        /// <summary>
        /// 是否被标记
        /// </summary>
        public bool IsMark { get; set; }
        /// <summary>
        /// 主题干
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 信息(XX题（共X题，每题X分，共X分。） 当前为第 X 题)
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 耗费时间
        /// </summary>
        public double CostTime { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 题型
        /// </summary>
        public int StructType { get; set; }

        /// <summary>
        /// 标准分
        /// </summary>
        public decimal StandardScore { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public decimal RightScore { get; set; }
        /// <summary>
        /// 标准答案
        /// </summary>
        public List<int> answer { get; set; }

        /// <summary>
        /// 评析
        /// </summary>
        public string analyse { get; set; }
    }
}
