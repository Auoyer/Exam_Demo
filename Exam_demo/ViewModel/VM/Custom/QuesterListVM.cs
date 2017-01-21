using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
   public class QuesterListVM
    {
       public QuesterListVM()
       {

       }

       /// <summary>
       /// 对应的题目/单选多选这些
       /// </summary>
      public string ChapterName { get; set; }
       /// <summary>
       /// 单选多选这些所对应的id
       /// </summary>
      public string chapterId { get; set; }

      /// <summary>
      /// 用户答题内容
      /// </summary>
      public List<PaperUserAnswerVM> UserAnswer { get; set; }
      /// <summary>
      /// 用户答题得分
      /// </summary>
      public PaperUserAnswerResultVM UserAnswerResult { get; set; }
       /// <summary>
       /// 相应题目
       /// </summary>
      public QuestionVM QuestionResult { get; set; }
       /// <summary>
       /// 题目分数表
       /// </summary>
      public decimal SubjectScoreResult { get; set; }
     
    }
}
