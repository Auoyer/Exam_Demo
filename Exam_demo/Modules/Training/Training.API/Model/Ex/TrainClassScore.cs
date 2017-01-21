using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API

{
    /// <summary>
    /// 统计班级每月得分
    /// </summary>
     [DataContract]
    public class TrainClassScore
    {
         /// <summary>
         /// 班级Id
         /// </summary>
         [DataMember]
         public int ClassId { get; set; }

         /// <summary>
         /// 平均分
         /// </summary>
          [DataMember]
         public decimal Average { get; set; }

         /// <summary>
         /// 结束时间
         /// </summary>
          [DataMember]
         public DateTime EndDate { get; set; }
    }
}
