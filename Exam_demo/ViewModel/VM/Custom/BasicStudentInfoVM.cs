using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
   public class BasicStudentInfoVM
    {
       public BasicStudentInfoVM()
       {

       }
       /// <summary>
       /// 学生姓名
       /// </summary>
     
       public string UserName { get; set; }
       /// <summary>
       /// 学号
       /// </summary>
     
       public string IDNum { get; set; }
       /// <summary>
       /// 班级
       /// </summary>
     
       public string ClassName { get; set; }
       /// <summary>
       /// 院系
       /// </summary>
     
       public string CollegeName { get; set; }
       /// <summary>
       ///实训考核完成次数
       /// </summary>
     
       public int CalcTrainExamNum { get; set; }
       /// <summary>
       /// 理论考核完成次数
       /// </summary>
     
       public int CalcTheoryExamNum { get; set; }
       /// <summary>
       /// 自主新增完成次数
       /// </summary>
     
       public int CalcSelfTrain { get; set; }
       /// <summary>
       /// 学生工号
       /// </summary>
       public string SchoolNum { get; set; }
    }
}
