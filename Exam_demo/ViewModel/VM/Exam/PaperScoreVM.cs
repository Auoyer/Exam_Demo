using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///�Ծ�����ṹ
    /// </summary>
    public class PaperScoreVM
    {
        public PaperScoreVM()
        {
            ChapterListId = new List<int>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// �Ծ�ID
        /// </summary>		
        public int PaperID { get; set; }

        /// <summary>
        /// �½�ID
        /// </summary>		
        public string CharpterID { get; set; }

        /// <summary>
        /// ��ǰ�½���Ŀ��
        /// </summary>		
        public int Count { get; set; }

        /// <summary>
        /// ÿ���ֵ
        /// </summary>		
        public decimal Score { get; set; }
        /*--------------------------------��չ�ֶ�-------------------------------------*/
        /// <summary>
        /// �½�����
        /// </summary>
        public string CharpterName { get; set; }
        /// <summary>
        /// ѡ�е��������
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string QuestionsName { get; set; }
        /// <summary>
        /// ��Ӧ�Ķ����ѡ��ѡ��ЩID
        /// </summary>
        public List<int> ChapterListId { get; set; }

        /// <summary>
        /// ��Ӧ���е�����Id
        /// </summary>
        public List<QuestionVM> QuestionsListId { get; set; }

        /// <summary>
        /// ����stringid
        /// </summary>
        public List<string> StrListId { get; set; }

        /// <summary>
        /// �û���������
        /// </summary>
        public List<PaperUserAnswerVM> UserAnswer { get; set; }
        /// <summary>
        /// �û�����÷�
        /// </summary>
        public PaperUserAnswerResultVM UserAnswerResult { get; set; }
        /// <summary>
        /// �Ƿ��е�ѡ��
        /// </summary>
        public bool IsRadio { get; set; }

    }
}