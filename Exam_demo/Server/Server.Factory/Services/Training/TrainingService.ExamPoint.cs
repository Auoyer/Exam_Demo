using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 获取全部考核点
        /// </summary>
        /// <returns></returns>
        public List<ExamPointVM> GetExamPointList()
        {
            int totalCount = 0;

            var list = MyService.GetExamPointPage(null, null, null, out totalCount);
            List<ExamPointVM> rtnValue = list.MapList<ExamPointVM, ExamPoint>();

            return rtnValue;
        }
    }
}
