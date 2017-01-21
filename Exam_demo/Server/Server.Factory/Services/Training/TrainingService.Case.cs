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
        /// 获取案例列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public PagedList<CaseVM> GetCasePage(TrainSearch search, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                CaseStatus = search.CaseStatus,
                FinancialTypeId = search.FinancialTypeId,
                KeyWords = search.KeyWords,//客户姓名，身份证号
                CollegeId = search.CollegeId,
                BySchool = search.BySchool,
            };
            var list = new List<Case>();
            try
            {
                 list = MyService.GetCasePage(filter, pageIndex, pageSize, out totalCount);
            }
            catch (Exception ex)
            { }

            List<CaseVM> rtnValue = list.MapList<CaseVM, Case>();
            PagedList<CaseVM> result = new PagedList<CaseVM>(rtnValue, pageIndex, pageSize, totalCount);

            return result;
        }

        /// <summary>
        /// 获取案例列表全部
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<CaseVM> GetCasePage(TrainSearch search)
        {
            int totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                CaseStatus = search.CaseStatus,
                FinancialTypeId = search.FinancialTypeId,
                KeyWords = search.KeyWords,//客户姓名，身份证号
                CollegeId = search.CollegeId
            };

            var list = MyService.GetCasePage(filter,null,null, out totalCount);

            List<CaseVM> rtnValue = list.MapList<CaseVM, Case>();

            return rtnValue;
        }

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        public CaseVM GetCase(int Id)
        {
            var model = MyService.GetCase(Id);
            return model.Map<CaseVM, Case>();
        }

        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>案例Id</returns>
        public int AddCase(CaseVM model)
        {
            Case entity = model.Map<Case, CaseVM>();
            int Id = MyService.AddCase(entity);
            return Id;
        }

        /// <summary>
        /// 更新案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateCase(CaseVM model)
        {
            Case entity = model.Map<Case, CaseVM>();
            bool result = MyService.UpdateCase(entity);
            return result;
        }

        /// <summary>
        /// 校验是否重复
        /// </summary>
        /// <param name="CaseId">案例主键</param>
        /// <param name="IDNum">身份证号</param>
        /// <returns>返回值为true:不存在重复</returns>
        public bool CheckRepeat(int CaseId, string IDNum)
        {
            return MyService.CheckRepeat(CaseId, IDNum);
        }

        /// <summary>
        /// 检查自定义案列是否被用在竞赛中（0，未发布；1：已发布；2：已结束；-2：未结束）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public bool CheckCaseInMatch(int status, int caseId)
        {
            bool result = false;
            result = MyService.CheckCaseInMatch(status, caseId) > 0;
            return result;
        }

        /// <summary>
        /// 修改案例状态（屏蔽，发布，删除）
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <param name="type">操作类型</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseStatus(int caseId, int type)
        {
            var result = MyService.ChangeCaseStatus(caseId, type);
            return result;
        }

        /// <summary>
        /// 修改案例查看状态
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseViewStatus(int caseId)
        {
            var result = MyService.ChangeCaseViewStatus(caseId);
            return result;
        }
    }
}
