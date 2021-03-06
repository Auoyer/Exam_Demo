﻿using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:LifeEducationPlanDetail
    /// </summary>
    public partial class LifeEducationPlanDetailDAL
    {
        public LifeEducationPlanDetailDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LifeEducationPlanDetail where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LifeEducationPlanDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LifeEducationPlanDetail(");
            strSql.Append("ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@EduStage,@EduAge,@EduTime,@Tuition,@EduTuition,@TotalTuition)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@EduStage", model.EduStage, dbType: DbType.Int32);
                param.Add("@EduAge", model.EduAge, dbType: DbType.Int32);
                param.Add("@EduTime", model.EduTime, dbType: DbType.Int32);
                param.Add("@Tuition", model.Tuition, dbType: DbType.Decimal);
                param.Add("@EduTuition", model.EduTuition, dbType: DbType.Decimal);
                param.Add("@TotalTuition", model.TotalTuition, dbType: DbType.Decimal);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LifeEducationPlanDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LifeEducationPlanDetail set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("EduStage=@EduStage,");
            strSql.Append("EduAge=@EduAge,");
            strSql.Append("EduTime=@EduTime,");
            strSql.Append("Tuition=@Tuition,");
            strSql.Append("EduTuition=@EduTuition,");
            strSql.Append("TotalTuition=@TotalTuition");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@EduStage", model.EduStage, dbType: DbType.Int32);
                param.Add("@EduAge", model.EduAge, dbType: DbType.Int32);
                param.Add("@EduTime", model.EduTime, dbType: DbType.Int32);
                param.Add("@Tuition", model.Tuition, dbType: DbType.Decimal);
                param.Add("@EduTuition", model.EduTuition, dbType: DbType.Decimal);
                param.Add("@TotalTuition", model.TotalTuition, dbType: DbType.Decimal);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LifeEducationPlanDetail ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LifeEducationPlanDetail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition from LifeEducationPlanDetail ");
            strSql.Append(" where Id=@Id ");

            LifeEducationPlanDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<LifeEducationPlanDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LifeEducationPlanDetail> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition ");
            strSql.Append(" FROM LifeEducationPlanDetail ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<LifeEducationPlanDetail> list = new List<LifeEducationPlanDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<LifeEducationPlanDetail>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.ProposalId.HasValue)
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetLifeEducationPlanDetailPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "LifeEducationPlanDetail";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

