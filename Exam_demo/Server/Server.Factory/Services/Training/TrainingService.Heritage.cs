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
        /// 新增财产传承
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddHeritage(HeritageVM model)
        {
            Heritage entity = model.Map<Heritage, HeritageVM>();
            int Id = MyService.AddHeritage(entity);
            return Id;
        }

        /// <summary>
        /// 查询财产传承2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public HeritageVM SelectHeritageGetObj(int ProposalId)
        {
            var model = MyService.SelectHeritageGetObj(ProposalId);
            return model.Map<HeritageVM, Heritage>();
        }

       
        /// <summary>
        /// 修改财产传承
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateHeritage(HeritageVM model)
        {

            bool bo = false;
            Heritage entity = model.Map<Heritage, HeritageVM>();
            bo = MyService.UpdateHeritage(entity);
            return bo;
        }
    }
}
