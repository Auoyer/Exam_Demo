using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class V_UserMatchScore
    {
        [DataMember]
        public int CompetitionId { get; set; }
        [DataMember]
        public decimal SubjectiveResults { get; set; }
        [DataMember]
        public decimal ObjectiveResults { get; set; }
        [DataMember]
        public int TrainExamId { get; set; }
        [DataMember]
        public decimal Score { get; set; }
        [DataMember]
        public decimal TotalScore { get; set; }
        [DataMember]
        public decimal AllScore { get; set; }

        public int GroupId { get; set; }
    }
}
