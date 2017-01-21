using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private NumberDAL numberDAL = new NumberDAL();

        /// <summary>
        /// 新增编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>新增编号Id</returns>
        public int AddNumber(Number model)
        {
            int result = 0;
            try
            {
                result = numberDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddNumber方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取最新编号
        /// </summary>
        /// <param name="numtype">编号类型</param>
        /// <returns>编号实体</returns>
        public Number GetNumber(int numtype)
        {
            Number result = null;
            try
            {
                result = numberDAL.GetModel(numtype);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNumber方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateNumber(Number model)
        {
            bool result = false;
            try
            {
                result = numberDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateNumber方法出错", ex);
            }
            return result;
        }



    }
}
