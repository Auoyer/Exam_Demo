using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class AssessmentResultsVM
    {

        public AssessmentResultsVM()
        {
            DetailList = new List<AssessmentResultsDetailVM>();
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 实训考核ID
        /// </summary>
        public int TrainExamId { get; set; }
        /// <summary>
        /// 竞赛ID
        /// </summary>
        public int CompetitionId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 主观分
        /// </summary>
        public decimal SubjectiveResults { get; set; }
        /// <summary>
        /// 客观分
        /// </summary>
        public decimal ObjectiveResults { get; set; }
        /// <summary>
        /// 考核状态 1:待评分，2：已评分
        /// </summary>
        public int TrainExamStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 考核明细
        /// </summary>
        public List<AssessmentResultsDetailVM> DetailList { get; set; }

        #region 扩展字段
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string SchoolNum { get; set; }
        /// <summary>
        /// 考核名称
        /// </summary>
        public string TrainExamName { get; set; }
        /// <summary>
        /// 考核状态名称
        /// </summary>
        public string TrainExamStatusName { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 理财类型
        /// </summary>
        public string FinancialTypeName { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        public decimal RealSumScore
        {
            get
            {
                return SubjectiveResults + ObjectiveResults;

            }
        }
        /// <summary>
        /// 达标分
        /// </summary>
        public decimal PassScore
        {
            get
            {
                return TotalScore * 0.6m;
            }
        }
        /// <summary>
        /// 考核日期=开始日期
        /// </summary>
        public string StrStartDate { get; set; }
        /// <summary>
        /// 院系名
        /// </summary>
        public string CollegeName { get; set; }
        #endregion
    }
}
