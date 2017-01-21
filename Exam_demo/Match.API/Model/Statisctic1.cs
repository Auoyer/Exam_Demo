using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
     [DataContract]
    public class Statisctic1
    {
         /// <summary>
         /// 大赛ID
         /// </summary>
         [DataMember]
         public int Id { get; set; }

         /// <summary>
         /// 大赛类型（竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛）
         /// </summary>
         [DataMember]
         public int Type { get; set; }

         /// <summary>
         /// 实训赛总分
         /// </summary>
         [DataMember]
         public decimal AllScore { get; set; }

         /// <summary>
         /// 理论赛总分
         /// </summary>
         [DataMember]
         public decimal TotalScore { get; set; }
    }
}
