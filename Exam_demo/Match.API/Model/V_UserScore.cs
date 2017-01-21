using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class V_UserScore
    {
        [DataMember]
        public int GroupId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int ProvinceCode { get; set; }

        [DataMember]
        public int CityCode { get; set; }

        [DataMember]
        public string CollegeName { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public decimal? SubjectiveResults { get; set; }

        [DataMember]
        public decimal? ObjectiveResults { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public int CompetitionId { get; set; }

        [DataMember]
        public int IsPageRegistration { get; set; }

        [DataMember]
        public string IDCard { get; set; }

        /// <summary>
        /// 大赛的类型（竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛）
        /// </summary>
        /// 
        [DataMember]
        public int Type { get; set; }
    }
}
