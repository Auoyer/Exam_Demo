using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:RiskIndex
    /// </summary>
    public partial class RiskIndexDAL
    {
        public RiskIndexDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RiskIndex where Id=@Id ");

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
        public int Add(RiskIndex model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RiskIndex(");
            strSql.Append("ProposalId,AgeScore,JobScore,FamilyScore,HouseScore,EXPScore,KnowledgeScore,RCIScore,TolerateScore,ConsiderationScore,LossScore,MentalityScore,CharacterScore,AvoidScore,UpdateDate,RAIScore)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@AgeScore,@JobScore,@FamilyScore,@HouseScore,@EXPScore,@KnowledgeScore,@RCIScore,@TolerateScore,@ConsiderationScore,@LossScore,@MentalityScore,@CharacterScore,@AvoidScore,@UpdateDate,@RAIScore)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@AgeScore", model.AgeScore, dbType: DbType.Int32);
                param.Add("@JobScore", model.JobScore, dbType: DbType.Int32);
                param.Add("@FamilyScore", model.FamilyScore, dbType: DbType.Int32);
                param.Add("@HouseScore", model.HouseScore, dbType: DbType.Int32);
                param.Add("@EXPScore", model.EXPScore, dbType: DbType.Int32);
                param.Add("@KnowledgeScore", model.KnowledgeScore, dbType: DbType.Int32);
                param.Add("@RCIScore", model.RCIScore, dbType: DbType.Decimal);
                param.Add("@TolerateScore", model.TolerateScore, dbType: DbType.Int32);
                param.Add("@ConsiderationScore", model.ConsiderationScore, dbType: DbType.Int32);
                param.Add("@LossScore", model.LossScore, dbType: DbType.Int32);
                param.Add("@MentalityScore", model.MentalityScore, dbType: DbType.Int32);
                param.Add("@CharacterScore", model.CharacterScore, dbType: DbType.Int32);
                param.Add("@AvoidScore", model.AvoidScore, dbType: DbType.Int32);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@RAIScore", model.RAIScore, dbType: DbType.Decimal);
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
        public bool Update(RiskIndex model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RiskIndex set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("AgeScore=@AgeScore,");
            strSql.Append("JobScore=@JobScore,");
            strSql.Append("FamilyScore=@FamilyScore,");
            strSql.Append("HouseScore=@HouseScore,");
            strSql.Append("EXPScore=@EXPScore,");
            strSql.Append("KnowledgeScore=@KnowledgeScore,");
            strSql.Append("RCIScore=@RCIScore,");
            strSql.Append("TolerateScore=@TolerateScore,");
            strSql.Append("ConsiderationScore=@ConsiderationScore,");
            strSql.Append("LossScore=@LossScore,");
            strSql.Append("MentalityScore=@MentalityScore,");
            strSql.Append("CharacterScore=@CharacterScore,");
            strSql.Append("AvoidScore=@AvoidScore,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("RAIScore=@RAIScore");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@AgeScore", model.AgeScore, dbType: DbType.Int32);
                param.Add("@JobScore", model.JobScore, dbType: DbType.Int32);
                param.Add("@FamilyScore", model.FamilyScore, dbType: DbType.Int32);
                param.Add("@HouseScore", model.HouseScore, dbType: DbType.Int32);
                param.Add("@EXPScore", model.EXPScore, dbType: DbType.Int32);
                param.Add("@KnowledgeScore", model.KnowledgeScore, dbType: DbType.Int32);
                param.Add("@RCIScore", model.RCIScore, dbType: DbType.Decimal);
                param.Add("@TolerateScore", model.TolerateScore, dbType: DbType.Int32);
                param.Add("@ConsiderationScore", model.ConsiderationScore, dbType: DbType.Int32);
                param.Add("@LossScore", model.LossScore, dbType: DbType.Int32);
                param.Add("@MentalityScore", model.MentalityScore, dbType: DbType.Int32);
                param.Add("@CharacterScore", model.CharacterScore, dbType: DbType.Int32);
                param.Add("@AvoidScore", model.AvoidScore, dbType: DbType.Int32);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@RAIScore", model.RAIScore, dbType: DbType.Decimal);
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
            strSql.Append("delete from RiskIndex ");
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
        public RiskIndex GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,AgeScore,JobScore,FamilyScore,HouseScore,EXPScore,KnowledgeScore,RCIScore,TolerateScore,ConsiderationScore,LossScore,MentalityScore,CharacterScore,AvoidScore,UpdateDate,RAIScore from RiskIndex ");
            strSql.Append(" where Id=@Id ");

            RiskIndex model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<RiskIndex>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RiskIndex> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,AgeScore,JobScore,FamilyScore,HouseScore,EXPScore,KnowledgeScore,RCIScore,TolerateScore,ConsiderationScore,LossScore,MentalityScore,CharacterScore,AvoidScore,UpdateDate,RAIScore ");
            strSql.Append(" FROM RiskIndex ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<RiskIndex> list = new List<RiskIndex>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<RiskIndex>(strSql.ToString()).ToList();
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
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetRiskIndexPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "RiskIndex";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,AgeScore,JobScore,FamilyScore,HouseScore,EXPScore,KnowledgeScore,RCIScore,TolerateScore,ConsiderationScore,LossScore,MentalityScore,CharacterScore,AvoidScore,UpdateDate,RAIScore";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /// <summary>
        /// 根据建议号获取风险评测信息
        /// </summary>
        /// <param name="proposalId">建议号</param>
        /// <returns></returns>
        public RiskIndex GetRiskEvaluationInfo(int proposalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,AgeScore,JobScore,FamilyScore,HouseScore,EXPScore,KnowledgeScore,RCIScore,TolerateScore,ConsiderationScore,LossScore,MentalityScore,CharacterScore,AvoidScore,UpdateDate,RAIScore ");
            strSql.Append(" from RiskIndex ");
            strSql.Append(" where ProposalId=@ProposalId ");
            RiskIndex model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", proposalId, dbType: DbType.Int32);
                model = conn.Query<RiskIndex>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }


    }
}

