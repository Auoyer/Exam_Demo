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
        /// 获取最新编号
        /// </summary>
        /// <param name="numtype">编号类型</param>
        /// <returns>编号实体</returns>
        public NumberVM GetNumber(int numberType)
        {
            var model = MyService.GetNumber(numberType);
            return model.Map<NumberVM, Number>();
        }

        /// <summary>
        /// 新增编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>新增编号Id</returns>
        public int AddNumber(NumberVM model)
        {
            Number entity = model.Map<Number, NumberVM>();
            int Id = MyService.AddNumber(entity);
            return Id;
        }

        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateNumber(NumberVM model)
        {
            Number entity = model.Map<Number, NumberVM>();
            bool result = MyService.UpdateNumber(entity);
            return result;
        }

    }
}
