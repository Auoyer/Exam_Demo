using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match.API;
using Utils;
using Dapper;
using System.Data;

namespace Match.Svr
{
    public partial class CompetitionDAL
    {
        #region 模板生成

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Competition where Id=@Id ");

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
        public int Add(Competition model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Competition(");
            strSql.Append("PreliminaryStartTime,PreliminaryEndTime,RematchStartTime,RematchEndTime,PreliminaryResultType,PreliminaryResultTime,RematchResultType,RematchResultTime,ScoreStartTime,ScoreEndTime,CollegeId,Information,FinalistNumber,IsRelease,IsDelete,Name,AddUserId,AddTime,Type,State,RegistrationStartTime,RegistrationEndTime");
            strSql.Append(") values (");
            strSql.Append("@PreliminaryStartTime,@PreliminaryEndTime,@RematchStartTime,@RematchEndTime,@PreliminaryResultType,@PreliminaryResultTime,@RematchResultType,@RematchResultTime,@ScoreStartTime,@ScoreEndTime,@CollegeId,@Information,@FinalistNumber,@IsRelease,@IsDelete,@Name,@AddUserId,@AddTime,@Type,@State,@RegistrationStartTime,@RegistrationEndTime");
            strSql.Append(") ");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@PreliminaryStartTime", model.PreliminaryStartTime, dbType: DbType.DateTime);
                param.Add("@PreliminaryEndTime", model.PreliminaryEndTime, dbType: DbType.DateTime);
                param.Add("@RematchStartTime", model.RematchStartTime, dbType: DbType.DateTime);
                param.Add("@RematchEndTime", model.RematchEndTime, dbType: DbType.DateTime);
                param.Add("@PreliminaryResultType", model.PreliminaryResultType, dbType: DbType.Int32);
                param.Add("@PreliminaryResultTime", model.PreliminaryResultTime, dbType: DbType.DateTime);
                param.Add("@RematchResultType", model.RematchResultType, dbType: DbType.Int32);
                param.Add("@RematchResultTime", model.RematchResultTime, dbType: DbType.DateTime);
                param.Add("@ScoreStartTime", model.ScoreStartTime, dbType: DbType.DateTime);
                param.Add("@ScoreEndTime", model.ScoreEndTime, dbType: DbType.DateTime);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@Information", model.Information, dbType: DbType.String);
                param.Add("@FinalistNumber", model.FinalistNumber, dbType: DbType.Int32);
                param.Add("@IsRelease", model.IsRelease, dbType: DbType.Int32);
                param.Add("@IsDelete", model.IsDelete, dbType: DbType.Int32);
                param.Add("@Name", model.Name, dbType: DbType.String);
                param.Add("@AddUserId", model.AddUserId, dbType: DbType.Int32);
                param.Add("@AddTime", model.AddTime, dbType: DbType.DateTime);
                param.Add("@Type", model.Type, dbType: DbType.Int32);
                param.Add("@State", model.State, dbType: DbType.Int32);
                param.Add("@RegistrationStartTime", model.RegistrationStartTime, dbType: DbType.DateTime);
                param.Add("@RegistrationEndTime", model.RegistrationEndTime, dbType: DbType.DateTime);
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
        public bool Update(Competition model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Competition set ");
            strSql.Append(" PreliminaryStartTime = @PreliminaryStartTime , ");
            strSql.Append(" PreliminaryEndTime = @PreliminaryEndTime , ");
            strSql.Append(" RematchStartTime = @RematchStartTime , ");
            strSql.Append(" RematchEndTime = @RematchEndTime , ");
            strSql.Append(" PreliminaryResultType = @PreliminaryResultType , ");
            strSql.Append(" PreliminaryResultTime = @PreliminaryResultTime , ");
            strSql.Append(" RematchResultType = @RematchResultType , ");
            strSql.Append(" RematchResultTime = @RematchResultTime , ");
            strSql.Append(" ScoreStartTime = @ScoreStartTime , ");
            strSql.Append(" ScoreEndTime = @ScoreEndTime , ");
            strSql.Append(" CollegeId = @CollegeId , ");
            strSql.Append(" Information = @Information , ");
            strSql.Append(" FinalistNumber = @FinalistNumber , ");
            strSql.Append(" IsRelease = @IsRelease , ");
            strSql.Append(" IsDelete = @IsDelete , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" AddUserId = @AddUserId , ");
            strSql.Append(" AddTime = @AddTime , ");
            strSql.Append(" Type = @Type , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" RegistrationStartTime = @RegistrationStartTime , ");
            strSql.Append(" RegistrationEndTime = @RegistrationEndTime  ");

            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@PreliminaryStartTime", model.PreliminaryStartTime, dbType: DbType.DateTime);
                param.Add("@PreliminaryEndTime", model.PreliminaryEndTime, dbType: DbType.DateTime);
                param.Add("@RematchStartTime", model.RematchStartTime, dbType: DbType.DateTime);
                param.Add("@RematchEndTime", model.RematchEndTime, dbType: DbType.DateTime);
                param.Add("@PreliminaryResultType", model.PreliminaryResultType, dbType: DbType.Int32);
                param.Add("@PreliminaryResultTime", model.PreliminaryResultTime, dbType: DbType.DateTime);
                param.Add("@RematchResultType", model.RematchResultType, dbType: DbType.Int32);
                param.Add("@RematchResultTime", model.RematchResultTime, dbType: DbType.DateTime);
                param.Add("@ScoreStartTime", model.ScoreStartTime, dbType: DbType.DateTime);
                param.Add("@ScoreEndTime", model.ScoreEndTime, dbType: DbType.DateTime);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@Information", model.Information, dbType: DbType.String);
                param.Add("@FinalistNumber", model.FinalistNumber, dbType: DbType.Int32);
                param.Add("@IsRelease", model.IsRelease, dbType: DbType.Int32);
                param.Add("@IsDelete", model.IsDelete, dbType: DbType.Int32);
                param.Add("@Name", model.Name, dbType: DbType.String);
                param.Add("@AddUserId", model.AddUserId, dbType: DbType.Int32);
                param.Add("@AddTime", model.AddTime, dbType: DbType.DateTime);
                param.Add("@Type", model.Type, dbType: DbType.Int32);
                param.Add("@State", model.State, dbType: DbType.Int32);
                param.Add("@RegistrationStartTime", model.RegistrationStartTime, dbType: DbType.DateTime);
                param.Add("@RegistrationEndTime", model.RegistrationEndTime, dbType: DbType.DateTime);
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
            strSql.Append("delete from Competition ");
            strSql.Append(" where Id=@Id");

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
        public Competition GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PreliminaryStartTime, PreliminaryEndTime, RematchStartTime, RematchEndTime, PreliminaryResultType, PreliminaryResultTime, RematchResultType, RematchResultTime, ScoreStartTime, ScoreEndTime, CollegeId, Information, FinalistNumber, IsRelease, IsDelete, Name, AddUserId,AddUserName, AddTime, Type, State, RegistrationStartTime, RegistrationEndTime  ");
            strSql.Append("  from Competition ");
            strSql.Append(" where Id=@Id");

            Competition model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Competition>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Competition> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PreliminaryStartTime, PreliminaryEndTime, RematchStartTime, RematchEndTime, PreliminaryResultType, PreliminaryResultTime, RematchResultType, RematchResultTime, ScoreStartTime, ScoreEndTime, CollegeId, Information, FinalistNumber, IsRelease, IsDelete, Name, AddUserId, AddTime, Type, State, RegistrationStartTime, RegistrationEndTime  ");
            strSql.Append("  from Competition where");
            strSql.Append(GetStrWhere(filter));

            List<Competition> list = new List<Competition>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Competition>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();

            // 学校Id
            if (filter.CollegeId.HasValue && filter.CollegeId != 0)
                strSql.Append(" and CollegeId=" + filter.CollegeId);
            // 学校Id
            if (filter.CollegeId2.HasValue)
                strSql.AppendFormat(" and CollegeId in (0,{0})", filter.CollegeId2.Value);

            if (filter.CollegeIdList != null && filter.CollegeIdList.Count > 0)
            {
                string collegeIdStr = "";
                for (int i = 0; i < filter.CollegeIdList.Count; i++)
                {
                    collegeIdStr += (filter.CollegeIdList[i] + ",");
                }
                collegeIdStr = collegeIdStr.Substring(0, collegeIdStr.LastIndexOf(','));
                strSql.AppendFormat(" and CollegeId in ({0})", collegeIdStr);
            }
            if (filter.MatchIdForResult == null && filter.CompetitionId == null)
            {
                // 是否删除
                if (filter.IsDelete == 0)
                {
                    // 只查询正常的比赛
                    strSql.Append(" and IsDelete=" + filter.IsDelete);
                }
                else if (filter.IsDelete == 1)
                {
                    // 超管端，查询正常和被竞赛管理员删除的比赛
                    strSql.Append(" and IsDelete in (0,1)");
                }
            }
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Competition.Id=" + filter.Id);
            }

            // 查询条件，查询竞赛名称、创建人名称、竞赛类型
            if (!string.IsNullOrEmpty(filter.QueryFiled))
            {
                // 判断是否查竞赛类型
                string type = "";
                if (filter.QueryFiled.IndexOf("理论") != -1 || filter.QueryFiled.IndexOf("理") != -1 || filter.QueryFiled.IndexOf("理") != -1)
                    type = " Type=1 ";
                else if (filter.QueryFiled.IndexOf("实训") != -1 || filter.QueryFiled.IndexOf("实") != -1 || filter.QueryFiled.IndexOf("训") != -1)
                    type = " Type=2 ";
                else if (filter.QueryFiled.IndexOf("单项") != -1 || filter.QueryFiled.IndexOf("单") != -1 || filter.QueryFiled.IndexOf("项") != -1)
                    type = " Type<>3 ";
                else if (filter.QueryFiled.IndexOf("复合") != -1 || filter.QueryFiled.IndexOf("综合") != -1 || filter.QueryFiled.IndexOf("复") != -1 || filter.QueryFiled.IndexOf("合") != -1 || filter.QueryFiled.IndexOf("综") != -1)
                    type = " Type=3 ";

                if (string.IsNullOrEmpty(type))
                    strSql.Append(" and (Name like '%" + filter.QueryFiled.Trim() + "%'  or AddUserName like '%" + filter.QueryFiled.Trim() + "%' )");
                else
                    strSql.Append(" and (Name like '%" + filter.QueryFiled.Trim() + "%'  or AddUserName like '%" + filter.QueryFiled.Trim() + "%' or " + type + ")");
            }

            // 发布状态
            if (filter.IsRelease.HasValue)
            {
                if (filter.IsRelease.Value == -1)
                {
                    strSql.Append(" and IsRelease in (0,1)");
                }
                else if (filter.IsRelease.Value == -2)
                {
                    strSql.Append(" and IsRelease in (1,2)");
                }
                else
                {
                    strSql.Append(" and IsRelease=" + filter.IsRelease);
                }
            }

            // 要查询的关键字内容(竞赛成绩用)
            if (!string.IsNullOrEmpty(filter.KeyWordForResult))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWordForResult.Replace("'", "''");
                //客户姓名，学校名称，身份证号
                strSql.AppendFormat(" and (UserName like '%{0}%' or CollegeName like '%{0}%')", key);
            }

            //判断是否参赛的条件
            if (!string.IsNullOrEmpty(filter.KeyWordForInMatch))
            {
                strSql.AppendFormat(filter.KeyWordForInMatch);
            }

            //统计查询用（搜索范围：姓名，省份、城市、学校、院系）
            if (!string.IsNullOrEmpty(filter.KeyWordForStatistic))
            {
                string key = filter.KeyWordForStatistic.Replace("'", "''");
                strSql.AppendFormat(" and (UserName like '%{0}%' or ProvinceCode like '%{0}%' or CityCode like '%{0}%'  or CollegeName like '%{0}%' or DepartmentName like '%{0}%')", key);
            }

            if (filter.MatchIdForResult.HasValue)
            {
                if (filter.MatchIdForResult.Value > 0)
                {
                    strSql.Append(" and CompetitionId=" + filter.MatchIdForResult.Value);
                }
                else
                {
                    if (filter.MatchIdForResult.Value == -1)
                    {
                        // 待评分
                        strSql.Append(" and TrainExamStatus=1 and TrainExamId>0");
                    }
                    else if (filter.MatchIdForResult.Value == -2)
                    {
                        // 已评分
                        strSql.Append(" and TrainExamStatus=2 and TrainExamId>0");
                    }
                    else
                    {
                        // 全评分
                        strSql.Append(" and TrainExamId>0");
                    }
                }
            }

            // 竞赛ID
            if (filter.CompetitionId.HasValue)
                strSql.Append(" and CompetitionId=" + filter.CompetitionId.Value);

            // 用户ID
            if (filter.UserId2.HasValue)
                strSql.Append(" and UserId=" + filter.UserId2.Value);

            // 身份证
            if (filter.IDNum.HasValue)
                strSql.Append(" and IDNum=" + filter.IDNum.Value);

            // 竞赛报名用户
            if (filter.UserIdForApply.HasValue)
                strSql.AppendFormat(" and ApplyUser=(select max(ApplyUser) FROM MatchApply where CompetitionId={0} and UserId={1} and ApplyStatus in (0,1))", filter.CompetitionId.Value, filter.UserIdForApply.Value);

            //是否官网注册，1=官网注册，0=管理员创建
            if (filter.IsPageRegistration.HasValue)
            {
                strSql.Append(" and IsPageRegistration=" + filter.IsPageRegistration);
            }

            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetCompetitionPageParams()
        {
            PageModel model = new PageModel();
            model.Tables = "Competition";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id, PreliminaryStartTime, PreliminaryEndTime, RematchStartTime, RematchEndTime, PreliminaryResultType, PreliminaryResultTime, RematchResultType, RematchResultTime, ScoreStartTime, ScoreEndTime, CollegeId, Information, FinalistNumber, IsRelease, IsDelete, Name, AddUserId, AddTime, Type, State, RegistrationStartTime, RegistrationEndTime ";
            model.Filter = "";
            return model;
        }

        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetUserScoreParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "V_UserScore";
            //model.PKey = "Id";
            model.Sort = "GroupId";
            model.Fields = "[GroupId] ,[UserName],[ProvinceCode],[CityCode] ,[CollegeName] ,[DepartmentName],[SubjectiveResults]   ,[ObjectiveResults]    ,[Score]   ,[CompetitionId], IsPageRegistration,IDCard,Type";
            model.Filter = GetStrWhere(filter);

            //model.Sort = " CreateTime desc";

            return model;
        }

        #endregion

        #endregion


        #region 自定义扩展

        /// <summary>
        /// 验证竞赛名称是否重复
        /// </summary>
        /// <param name="matchName">竞赛名称</param>
        /// <returns>不重复返回true</returns>
        public bool IsMatchNameRepeat(string matchName, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Competition where Name=@matchName and IsDelete=0 and collegeId=@collegeId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@matchName", matchName, dbType: DbType.String);
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0 ? false : true;
        }


        /// <summary>
        /// 创建大赛，关联评委
        /// </summary>
        public bool CreateMatch(Competition model, List<int> listJudgeId)
        {
            var param = new DynamicParameters();
            StringBuilder strSql = new StringBuilder();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    // 创建大赛
                    strSql.Clear();
                    strSql.Append("insert into Competition(");
                    strSql.Append("CollegeId,IsRelease,IsDelete,Name,AddUserId,AddTime,AddUserName,Type,State");
                    strSql.Append(") values (");
                    strSql.Append("@CollegeId,@IsRelease,@IsDelete,@Name,@AddUserId,@AddTime,@AddUserName,@Type,@State");
                    strSql.Append(") ");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                    param.Add("@IsRelease", model.IsRelease, dbType: DbType.Int32);
                    param.Add("@IsDelete", model.IsDelete, dbType: DbType.Int32);
                    param.Add("@Name", model.Name, dbType: DbType.String);
                    param.Add("@AddUserId", model.AddUserId, dbType: DbType.Int32);
                    param.Add("@AddTime", model.AddTime, dbType: DbType.DateTime);
                    param.Add("@AddUserName", model.AddUserName, dbType: DbType.String);
                    param.Add("@Type", model.Type, dbType: DbType.Int32);
                    param.Add("@State", model.State, dbType: DbType.Int32);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute(strSql.ToString(), param, tran);
                    int matchId = param.Get<int>("@returnid");               // 返回新增竞赛ID

                    if (matchId > 0)
                    {
                        // 循环添加竞赛评委关联信息
                        if (listJudgeId != null && listJudgeId.Count > 0)
                        {
                            int result = 0;
                            foreach (var item in listJudgeId)
                            {
                                strSql.Clear();
                                strSql.Append("insert into CompetitionJudges(");
                                strSql.Append("CompetitionId,UserId");
                                strSql.Append(") values (");
                                strSql.Append("@CompetitionId,@UserId");
                                strSql.Append(") ");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                                param.Add("@UserId", item, dbType: DbType.Int32);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                                conn.Execute(strSql.ToString(), param, tran);
                                result = param.Get<int>("@returnid");
                                if (result <= 0)
                                {
                                    tran.Rollback();
                                    return false;
                                }
                            }
                        }
                        // 事务提交
                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }



        }

        /// <summary>
        /// 获取考核成绩列表（用于统计分析）
        /// </summary>
        public List<V_UserMatchScore> GetUserMatchScoreList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from V_UserMatchScore where 1=1 and (SubjectiveResults is not null and ObjectiveResults is not null)");
            strSql.Append(GetStrWhere(filter));

            List<V_UserMatchScore> list = new List<V_UserMatchScore>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<V_UserMatchScore>(strSql.ToString()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>
        /// <param name="listJudgeId">List<评委ID></param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        /// <returns></returns>
        public bool IsJudgeConductMatch(List<int> listJudgeId, int userType, int collegeId)
        {
            string q = "in {" + string.Join(",", listJudgeId) + "}";
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(1) from Competition a, ");

            // 用户类型
            if (userType == 1)
                strSql.Append(" CompetitionJudges b ");
            else
                strSql.Append(" CompetitionUser b ");

            strSql.Append(" where a.id=b.CompetitionId and a.IsRelease<>2 ");
            //strSql.Append(" where a.id=b.CompetitionId and a.IsRelease<>2 and collegeId=" + collegeId);
            strSql.Append("and UserId in (" + string.Join(",", listJudgeId) + ")");


            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                int result = conn.Query<int>(strSql.ToString()).FirstOrDefault();

                if (result == 0)
                    return true;
                else
                    return false;
            }
        }



        /// <summary>
        /// cww-用户分页，关联账号表和用户信息表
        /// </summary>
        //public PageModel GetMatchPage(CustomFilter filter = null)
        //{
        //    PageModel model = new PageModel();
        //    model.Tables = "Competition ";
        //    model.PKey = "Competition.Id";
        //    model.Sort = GetStrSort(filter);
        //    model.Fields = "Competition.*";
        //    model.Filter = GetStrWhere(filter);
        //    return model;
        //}


        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public PageModel GetMatchLsit(CustomFilter filter = null)
        {
            PageModel model = new PageModel();
            model.Tables = "Competition ";
            model.PKey = "Competition.Id";
            model.Sort = GetStrSort(filter);
            model.Fields = "Competition.*";
            model.Filter = GetStrWhere(filter);
            return model;
        }


        /// <summary>
        /// 编辑竞赛信息和评委关联信息
        /// </summary>
        public bool UpdateMatch(Competition model)
        {
            StringBuilder strSql = new StringBuilder();
            int result = 0;
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    #region 拼接Sql

                    strSql.Append("update Competition set ");

                    // 默认单项理论赛
                    strSql.Append(" PreliminaryStartTime = @PreliminaryStartTime , ");
                    strSql.Append(" PreliminaryEndTime = @PreliminaryEndTime , ");
                    strSql.Append(" PreliminaryResultType = @PreliminaryResultType , ");
                    strSql.Append(" PreliminaryResultTime = @PreliminaryResultTime , ");
                    strSql.Append(" CollegeId = @CollegeId , ");
                    strSql.Append(" Information = @Information , ");
                    strSql.Append(" FinalistNumber = @FinalistNumber , ");
                    strSql.Append(" RegistrationStartTime = @RegistrationStartTime , ");
                    strSql.Append(" RegistrationEndTime = @RegistrationEndTime  ");

                    if (model.Type == 2)
                    {
                        // 实训赛需要有评分时间和评委信息
                        strSql.Append(" ,ScoreStartTime = @ScoreStartTime  ");
                        strSql.Append(" ,ScoreEndTime = @ScoreEndTime  ");
                    }
                    else if (model.Type == 3)
                    {
                        // 复合在还需要有复赛相关信息
                        strSql.Append(" ,ScoreStartTime = @ScoreStartTime  ");               // 评分时间
                        strSql.Append(" ,ScoreEndTime = @ScoreEndTime  ");
                        strSql.Append(" ,RematchStartTime = @RematchStartTime  ");           // 复赛时间
                        strSql.Append(" ,RematchEndTime = @RematchEndTime  ");
                        strSql.Append(" ,RematchResultType = @RematchResultType  ");           // 复赛成绩发布类型
                        strSql.Append(" ,RematchResultTime = @RematchResultTime  ");           // 复赛成绩发布时间
                    }

                    strSql.Append(" where Id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@PreliminaryStartTime", model.PreliminaryStartTime.Value, dbType: DbType.DateTime);
                    param.Add("@PreliminaryEndTime", model.PreliminaryEndTime, dbType: DbType.DateTime);
                    param.Add("@PreliminaryResultType", model.PreliminaryResultType, dbType: DbType.Int32);
                    param.Add("@PreliminaryResultTime", model.PreliminaryResultTime, dbType: DbType.DateTime);
                    param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                    param.Add("@Information", model.Information, dbType: DbType.String);
                    param.Add("@FinalistNumber", model.FinalistNumber, dbType: DbType.Int32);
                    param.Add("@RegistrationStartTime", model.RegistrationStartTime, dbType: DbType.DateTime);
                    param.Add("@RegistrationEndTime", model.RegistrationEndTime, dbType: DbType.DateTime);

                    if (model.Type == 2)
                    {
                        param.Add("@ScoreStartTime", model.ScoreStartTime, dbType: DbType.DateTime);
                        param.Add("@ScoreEndTime", model.ScoreEndTime, dbType: DbType.DateTime);
                    }
                    else if (model.Type == 3)
                    {
                        param.Add("@ScoreStartTime", model.ScoreStartTime, dbType: DbType.DateTime);
                        param.Add("@ScoreEndTime", model.ScoreEndTime, dbType: DbType.DateTime);
                        param.Add("@RematchStartTime", model.RematchStartTime, dbType: DbType.DateTime);
                        param.Add("@RematchEndTime", model.RematchEndTime, dbType: DbType.DateTime);
                        param.Add("@RematchResultType", model.RematchResultType, dbType: DbType.Int32);
                        param.Add("@RematchResultTime", model.RematchResultTime, dbType: DbType.DateTime);
                    }
                    #endregion

                    result = conn.Execute(strSql.ToString(), param, tran);

                    #region 关联评委

                    if (model.Type != 1)
                    {
                        // 实训赛和复合赛关联评委信息，先删除原有关联信息，在添加新的关联信息
                        strSql.Clear();
                        strSql.Append("delete CompetitionJudges where CompetitionId=@CompetitionId");
                        param.Add("@CompetitionId", model.Id, dbType: DbType.Int32);
                        result = conn.Execute(strSql.ToString(), param, tran);
                        //if (result <= 0)
                        //{
                        //    tran.Rollback();
                        //    return false;
                        //}

                        // 循环添加评委
                        foreach (var item in model.ListJudgeId)
                        {
                            strSql.Clear();
                            strSql.Append("insert into CompetitionJudges(");
                            strSql.Append("CompetitionId,UserId");
                            strSql.Append(") values (");
                            strSql.Append("@CompetitionId,@UserId");
                            strSql.Append(") ");
                            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                            param.Add("@CompetitionId", model.Id, dbType: DbType.Int32);
                            param.Add("@UserId", item, dbType: DbType.Int32);
                            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            conn.Execute(strSql.ToString(), param, tran);
                            result = param.Get<int>("@returnid");
                            if (result <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }
                        }
                    }

                    #endregion

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }

            return true;



        }

        /// <summary>
        /// 查询竞赛评审Ids
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public List<int> GetMatchJudgeListId(int matchId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select userId from CompetitionJudges where CompetitionId=" + matchId);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                return conn.Query<int>(strSql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<V_MatchUser> GetMatchUser(int CompetitionId)
        {
            string strSql = "select * from V_MatchUser where CompetitionId=" + CompetitionId;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                return conn.Query<V_MatchUser>(strSql).ToList();
            }
        }

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public V_MatchUser GetMatchUser2(int CompetitionId, int UserId)
        {
            string strSql = "select * from V_MatchUser where CompetitionId=" + CompetitionId + " and UserId=" + UserId;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                return conn.Query<V_MatchUser>(strSql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 提交保存分组信息
        /// </summary>
        /// <param name="list">提交分组的用户信息</param>
        /// <param name="groupId">groupId！=0表示只修改该分组用户信息，groupId==0：先删除全部手动分组用户，然后在添加分组用户</param>
        /// <param name="competitionId">竞赛Id</param>
        /// <returns></returns>
        public bool AddGroupUser(List<V_MatchUser> list, int groupId, int competitionId, out string errCode)
        {
            errCode = "";
            var param = new DynamicParameters();
            StringBuilder strSql = new StringBuilder();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    // 判断是否单组用户操作
                    if (groupId != 0)
                    {
                        // 判断新增的用户是否已在其他组
                        strSql.Clear();
                        strSql.Append("exec('select count(1) from CompetitionUser where CompetitionId='+@CompetitionId+' and GroupId<>'+@GroupId+' and UserId in('+@UserIds+')')");
                        param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                        param.Add("@GroupId", groupId, dbType: DbType.Int32);
                        param.Add("@UserIds", string.Join(",", list.Select(m => m.UserId)), dbType: DbType.String);
                        int result = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();          // 提交删除
                        if (result != 0)
                        {
                            errCode = "22001";              // 选择的用户已有分组，无法重新分组
                            tran.Rollback();
                            return false;
                        }

                        // 单组操作，先删除该组用户，然后在添加
                        strSql.Clear();
                        strSql.Append("delete from CompetitionUser where CompetitionId=@CompetitionId and GroupId=@GroupId");
                        param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                        param.Add("@GroupId", groupId, dbType: DbType.Int32);

                        result = conn.Execute(strSql.ToString(), param, tran);          // 提交删除

                        // 该分组号不变，循环添加
                        foreach (var item in list)
                        {
                            strSql.Clear();
                            strSql.Append("insert into CompetitionUser(");
                            strSql.Append("CompetitionId,UserId,GroupId,GroupSouce,IsAudit");
                            strSql.Append(") values (");
                            strSql.Append("@CompetitionId,@UserId,@GroupId,2,1");
                            strSql.Append(") ");
                            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                            param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                            param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                            param.Add("@GroupId", groupId, dbType: DbType.Int32);
                            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            conn.Execute(strSql.ToString(), param, tran);
                            int newId = param.Get<int>("@returnid");               // 返回新增竞赛ID
                            if (newId <= 0)
                            {
                                errCode = "22002";              // 分组提交失败，请联系系统管理员
                                tran.Rollback();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 判断新增的用户是否已在其他组
                        strSql.Clear();
                        strSql.Append("exec('select count(1) from CompetitionUser where CompetitionId='+@CompetitionId+' and GroupSouce<>2 and UserId in('+@UserIds+')')");
                        param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                        param.Add("@UserIds", string.Join(",", list.Select(m => m.UserId)), dbType: DbType.String);
                        int result = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();          // 提交删除
                        if (result != 0)
                        {
                            errCode = "22001";              // 选择的用户已有分组，无法重新分组
                            tran.Rollback();
                            return false;
                        }

                        // 删除全部手动分组用户，然后在添加
                        strSql.Clear();
                        strSql.Append("delete from CompetitionUser where CompetitionId=@CompetitionId and GroupSouce=2");
                        param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);

                        result = conn.Execute(strSql.ToString(), param, tran);          // 提交删除

                        // 该分组号不变，循环添加
                        // 分组
                        List<IGrouping<int, V_MatchUser>> listGroup = list.GroupBy(m => m.GroupId).ToList<IGrouping<int, V_MatchUser>>();
                        foreach (var item in listGroup)
                        {
                            // 查询最大的分组编号
                            strSql.Clear();
                            strSql.Append("select count(GroupId) from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=@CompetitionId");
                            param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                            int temp = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();
                            int maxGroupId = 0;
                            if (temp == 0)
                                maxGroupId = 1;
                            else
                            {
                                // 有数据，查询最大分组号
                                strSql.Clear();
                                strSql.Append("select max(GroupId) from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=@CompetitionId");
                                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                                temp = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();
                                maxGroupId = temp + 1;
                            }

                            //strSql.Append("select max(GroupId) from CompetitionUser where CompetitionId=@CompetitionId");
                            //int maxGroupId = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();          // 提交删除
                            //if (maxGroupId != 0)            // 最大分组号+1，是该分组号
                            //    maxGroupId = maxGroupId + 1;
                            //else
                            //    maxGroupId = 1;

                            foreach (var item2 in list.FindAll(m => m.GroupId == item.Key))
                            {
                                strSql.Clear();
                                strSql.Append("insert into CompetitionUser(");
                                strSql.Append("CompetitionId,UserId,GroupId,GroupSouce,IsAudit");
                                strSql.Append(") values (");
                                strSql.Append("@CompetitionId,@UserId,@GroupId,2,1");
                                strSql.Append(") ");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                                param.Add("@UserId", item2.UserId, dbType: DbType.Int32);
                                param.Add("@GroupId", maxGroupId, dbType: DbType.Int32);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                conn.Execute(strSql.ToString(), param, tran);
                                int newId = param.Get<int>("@returnid");               // 返回新增竞赛ID
                                if (newId <= 0)
                                {
                                    errCode = "22002";              // 分组提交失败，请联系系统管理员
                                    tran.Rollback();
                                    return false;
                                }
                            }
                        }
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    errCode = "22002";              // 分组提交失败，请联系系统管理员
                    tran.Rollback();
                    return false;
                }
            }
        }


        /// <summary>
        /// 删除指定分组下的用户关联信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool DeleteGroupUser(int matchId, int groupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CompetitionUser ");
            strSql.Append(" where CompetitionId=@CompetitionId and GroupId=@GroupId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                param.Add("@GroupId", groupId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 同一竞赛下不可有重复分组用户，判断用户是否已有分组
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public bool IsHaveGroup(int userId, int matchId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CompetitionUser a, Competition b where a.CompetitionId=b.id and UserId=@UserId and CompetitionId=@CompetitionId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);
                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }


        /// <summary>
        /// 删除比赛信息，逻辑删除竞赛信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="collegeId"></param>
        /// <param name="isDelete">更新竞赛信息表字段IsDelete,1=竞赛管理员删除，0=正常，2=超管删除</param>
        /// <returns></returns>
        public bool DeleteMatch(int matchId, int collegeId, int isDelete)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Competition set IsDelete=@IsDelete");
            strSql.Append(" where Id=@Id ");
            if (collegeId != 0)
                strSql.Append(" and collegeId=@CollegeId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", matchId, dbType: DbType.Int32);
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                param.Add("@IsDelete", isDelete, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }


        /// <summary>
        /// 修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool UpdateGroupAudit(int matchId, int groupId, int isAudit)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CompetitionUser set IsAudit=@IsAudit");
            strSql.Append(" where CompetitionId=@CompetitionId and GroupId=@GroupId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@IsAudit", isAudit, dbType: DbType.Int32);
                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                param.Add("@GroupId", groupId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }


        /// <summary>
        /// 复合赛-初赛成绩发布，设置入围人员
        /// </summary>
        /// <param name="vm">修改竞赛信息</param>
        /// <param name="groupIds">入围复赛的分组Ids</param>
        /// <returns></returns>
        public bool SetResult(Competition model, string groupIds)
        {
            var param = new DynamicParameters();
            StringBuilder strSql = new StringBuilder();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    // 修改竞赛信息
                    strSql.Append("update Competition set ");
                    strSql.Append(" State = @State ");
                    strSql.Append(" where Id=@Id ");

                    int result = 0;

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@State", model.State, dbType: DbType.Int32);
                    result = conn.Execute(strSql.ToString(), param, tran);

                    if (result > 0)
                    {
                        // 修改用户入围状态
                        strSql.Clear();
                        strSql.Append("exec('update CompetitionUser set ");
                        strSql.Append(" IsFinal = 1 ");
                        strSql.Append(" where GroupId in('+@GroupIds+') and CompetitionId='+@CompetitionId+'')");

                        param.Add("@GroupIds", groupIds, dbType: DbType.String);
                        param.Add("@CompetitionId", model.Id, dbType: DbType.Int32);
                        result = conn.Execute(strSql.ToString(), param, tran);

                        if (result > 0)
                        {
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }

                tran.Commit();
                return true;
            }
        }

        /// <summary>
        /// 获取报名待审核用户数量，只查询已发布的竞赛
        /// </summary>
        /// <returns></returns>
        public int GetSiginupNotAduitNum(int collegeId)
        {
            // 查询已发布，比赛未开始竞赛下的未审核用户
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Competition a,CompetitionUser b where a.id=b.CompetitionId and GroupSouce=3 and CollegeId=@collegeId and IsAudit=0 and IsRelease=1 and IsDelete=0 ");
            strSql.Append(" and ((a.Type=1 and a.State<103) or (a.Type=2 and a.State<203) or (a.Type=3 and a.State<303))");

            int result;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取报名待审核要跳转的竞赛ID，只查询已发布的竞赛
        /// </summary>
        /// <returns></returns>
        public int GetSiginupNotAduitMatchId(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(CompetitionId) as Id from Competition a,CompetitionUser b where a.id=b.CompetitionId and GroupSouce=3 and CollegeId=@collegeId and IsAudit=0 and IsRelease=1 and IsDelete=0");
            strSql.Append(" and ((a.Type=1 and a.State<103) or (a.Type=2 and a.State<203) or (a.Type=3 and a.State<303))");

            int result;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                //result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();

                Competition match = conn.Query<Competition>(strSql.ToString(), param).FirstOrDefault();
                if (match != null)
                    result = match.Id;
                else
                    result = 0;

                //IEnumerator<object> list = aa.GetEnumerator();
                //if (list.Current != null)
                //    result = Convert.ToInt32(aa.FirstOrDefault());
                //else
                //    result = 0;
            }
            return result;
        }

        #endregion


        #region 私有方法

        private string GetStrSort(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(filter.SortName))
            {
                strSql.Append(filter.SortName);
            }
            else
            {
                strSql.Append("Competition.Id desc");
            }
            if (filter.SortWay.HasValue)
            {
                strSql.Append(filter.SortWay.Value ? " " : " desc");
            }
            return strSql.ToString();
        }


        #endregion

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<V_MatchUser> GetAllMatchUser()
        {
            string strSql = "select * from V_MatchUser ";
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                return conn.Query<V_MatchUser>(strSql).ToList();
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Competition> GetMatchListWithCurUser(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Competition where 1=1");
            strSql.Append(GetStrWhere(filter));

            List<Competition> list = new List<Competition>();
            List<Competition> result = new List<Competition>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Competition>(strSql.ToString()).ToList();
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Competition te = list[i];
                        strSql.Clear();
                        strSql.Append("select * from [GTA_FPBT_Match_V1.5].dbo.[V_MatchUser] where CompetitionId=@CompetitionId ");
                        strSql.Append("and UserId=@UserId ");

                        var param2 = new DynamicParameters();
                        param2.Add("@CompetitionId", te.Id, dbType: DbType.Int32);
                        param2.Add("@UserId", filter.UserId.Value, dbType: DbType.Int32);
                        using (var multi = conn.QueryMultiple(strSql.ToString(), param2))
                        {
                            te.CurUserInfo = multi.Read<V_MatchUser>().LastOrDefault();

                            // 已参加大赛
                            if (filter.JoinIn)
                            {
                                if (te.CurUserInfo != null && te.CurUserInfo.IsAudit == 1)
                                {
                                    #region 获取试卷状态
                                    strSql.Clear();
                                    strSql.Append("select * from [GTA_FPBT_Training_V1.5].dbo.PaperUserSummary where CompetitionId=@CompetitionId ");
                                    strSql.Append("and UserId=@UserId ");

                                    strSql.Append("select * from [GTA_FPBT_Training_V1.5].dbo.AssessmentResults where CompetitionId=@CompetitionId ");
                                    strSql.Append("and UserId=(select MAX(UserId) from [GTA_FPBT_Match_V1.5].dbo.[V_MatchResult] where CompetitionId=@CompetitionId and TrainExamId>0 and GroupId=@GroupId) ");

                                    var param3 = new DynamicParameters();
                                    param3.Add("@CompetitionId", te.Id, dbType: DbType.Int32);
                                    param3.Add("@UserId", filter.UserId.Value, dbType: DbType.Int32);
                                    param3.Add("@GroupId", te.CurUserInfo.GroupId, dbType: DbType.Int32);
                                    using (var multi2 = conn.QueryMultiple(strSql.ToString(), param3))
                                    {
                                        te.CurUserPUResult = multi2.Read<Competition.PaperUserSummary>().LastOrDefault();
                                        te.CurUserARResult = multi2.Read<Competition.AssessmentResults>().LastOrDefault();
                                    }
                                    #endregion


                                    if (te.Type != 3)
                                    {
                                        result.Add(te);
                                    }
                                    else
                                    {
                                        Competition te1 = CommValue.CloneObject(te) as Competition;
                                        Competition te2 = CommValue.CloneObject(te) as Competition;
                                        te1.Name += "(初赛)";
                                        te1.Type = 31;
                                        te2.Name += "(复赛)";
                                        te2.Type = 32;
                                        result.Add(te1);
                                        result.Add(te2);
                                    }
                                }
                            }
                            else
                            {
                                if (te.CurUserInfo == null || te.CurUserInfo.IsAudit != 1)
                                {
                                    result.Add(te);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取分页参数（待报名大赛）
        /// </summary>
        public PageModel GetMatchListNotJoin(int userId, int collegeId, int sortType)
        {
            PageModel model = new PageModel();
            model.Tables = "Competition AS a LEFT JOIN V_MatchUser AS b ON a.Id=b.CompetitionId AND b.UserId=" + userId;
            model.Sort = (sortType == 1) ? "PreliminaryStartTime desc" : "PreliminaryStartTime";
            model.Fields = "a.*,b.IsAudit";

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" and a.CollegeId=" + collegeId);
            strSql.Append(" and a.IsRelease =1");
            strSql.Append(" and a.IsDelete =0");
            strSql.Append(" and ((a.Type=1 and a.State<102) or (a.Type=2 and a.State<202) or (a.Type=3 and a.State<302))");
            strSql.AppendFormat(" and (select count(*) from V_MatchUser where CompetitionId=a.Id and UserId={0} and IsAudit=1) < 1", userId);

            model.Filter = strSql.ToString();

            return model;
        }

        /// <summary>
        /// 待报名大赛数量
        /// </summary>
        public int GetMatchListNotJoinNum(int userId, int collegeId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.AppendFormat("select COUNT(1) from Competition a where IsRelease=1 and IsDelete=0 and CollegeId ={0}", collegeId);
            strSql.Append(" and ((a.Type=1 and a.State<102) or (a.Type=2 and a.State<202) or (a.Type=3 and a.State<302))");
            strSql.AppendFormat(" and (select count(1) from CompetitionUser where CompetitionId=a.Id and UserId={0}) < 1", userId);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 待参加大赛数量
        /// </summary>
        public int GetMatchListHasJoinNum(int userId, int collegeId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.AppendFormat("select COUNT(1) from Competition a where IsRelease=1 and IsDelete=0 and CollegeId ={0}", collegeId);
            strSql.Append(" and ((a.Type=1 and a.State=103) or (a.Type=2 and a.State=203) or (a.Type=3 and (a.State=303 or a.State=306)))");
            strSql.AppendFormat(" and (select count(1) from CompetitionUser where CompetitionId=a.Id and UserId={0} and IsAudit=1) > 0", userId);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<V_MatchResult> GetMatchResultList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from V_MatchResult where 1=1");
            strSql.Append(GetStrWhere(filter));

            List<V_MatchResult> list = new List<V_MatchResult>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<V_MatchResult>(strSql.ToString()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 统计分析用（查询参赛者信息和成绩信息）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<V_UserScore> GetUserScoreList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from V_UserScore where 1=1");
            strSql.Append(GetStrWhere(filter));

            List<V_UserScore> list = new List<V_UserScore>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<V_UserScore>(strSql.ToString()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 获取竞赛报名列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<MatchApply> GetMatchApplyList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from MatchApply where 1=1");
            strSql.Append(GetStrWhere(filter));

            List<MatchApply> list = new List<MatchApply>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<MatchApply>(strSql.ToString()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 增加竞赛报名数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddApply(List<MatchApply> model)
        {
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();
                foreach (var item in model)
                {
                    strSql.Clear();
                    param = new DynamicParameters();

                    strSql.Append("insert into MatchApply(");
                    strSql.Append("UserId,CompetitionId,IDNum,ApplyStatus,ApplyUser");
                    strSql.Append(") values (");
                    strSql.Append("@UserId,@CompetitionId,@IDNum,@ApplyStatus,@ApplyUser");
                    strSql.Append(") ");

                    param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                    param.Add("@CompetitionId", item.CompetitionId, dbType: DbType.Int32);
                    param.Add("@IDNum", item.IDNum, dbType: DbType.String);
                    param.Add("@ApplyStatus", item.ApplyStatus, dbType: DbType.Int32);
                    param.Add("@ApplyUser", item.ApplyUser, dbType: DbType.Int32);

                    conn.Execute(strSql.ToString(), param, tran);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]新增竞赛报名信息出错", ex);
                tran.Rollback();
                return false;
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
            }
            return true;
        }

        /// <summary>
        /// 删除竞赛报名数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DelApply(CustomFilter filter)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MatchApply where 1=1");
            strSql.Append(GetStrWhere(filter));
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString());
            }
            return result > 0;
        }

        /// <summary>
        /// 修改竞赛报名状态
        /// </summary>
        /// <param name="search"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdApplyStatue(CustomFilter filter, List<MatchApply> applyUser, int status)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            bool res = false;
            try
            {
                int temp = 0;
                var param = new DynamicParameters();
                StringBuilder strSql = new StringBuilder();
                // 更新状态
                strSql.AppendFormat("update MatchApply set ApplyStatus={0} where CompetitionId={1} and UserId={2}", status, filter.CompetitionId.Value, filter.UserIdForApply.Value);
                result = conn.Execute(strSql.ToString(), null, tran);
                // 获取成功的数量
                strSql.Clear();
                strSql.Append("select count(*) from MatchApply where ApplyStatus=1");
                strSql.Append(GetStrWhere(filter));

                temp = conn.Query<int>(strSql.ToString(), null, tran).FirstOrDefault();
                // 如果全部同意，删除组队记录，增加大赛人员记录
                if (temp == 3)
                {
                    // 删除组队记录
                    strSql.Clear();
                    strSql.Append("delete from MatchApply where 1=1");
                    strSql.Append(GetStrWhere(filter));
                    result = conn.Execute(strSql.ToString(), null, tran);
                    if (result > 0)
                    {
                        // 获取当前组号
                        strSql.Clear();
                        strSql.Append("select * from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=@CompetitionId order by GroupId desc");
                        param.Add("@CompetitionId", filter.CompetitionId, dbType: DbType.Int32);
                        var vTemp = conn.Query<CompetitionUser>(strSql.ToString(), param, tran);
                        if (vTemp != null && vTemp.Count() > 0)
                        {
                            temp = vTemp.FirstOrDefault().GroupId;
                        }
                        else
                        {
                            temp = 0;
                        }
                        int maxGroupId = temp + 1;
                        int i = 0;

                        foreach (var item in applyUser)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into CompetitionUser(");
                            strSql.Append("CompetitionId,UserId,GroupId,GroupSouce,IsAudit");
                            strSql.Append(") values (");
                            strSql.Append("@CompetitionId,@UserId,@GroupId,3,0");
                            strSql.Append(") ");
                            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                            param.Add("@CompetitionId", item.CompetitionId, dbType: DbType.Int32);
                            param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                            param.Add("@GroupId", maxGroupId, dbType: DbType.Int32);
                            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                            conn.Execute(strSql.ToString(), param, tran);
                            int newId = param.Get<int>("@returnid");               // 返回新增竞赛ID
                            if (newId <= 0)
                            {
                                tran.Rollback();
                                break;
                            }
                            i++;
                        }
                        if (i == 3)
                        {
                            tran.Commit();
                            res = true;
                        }
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Commit();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]新增竞赛报名信息出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
            }

            return res;
        }

        /// <summary>
        /// 进入复赛时，修改其他组员的状态
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="GroupId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool UpdFinalStatus(int MatchId, int GroupId, int UserId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update CompetitionUser set IsFinal=2 where CompetitionId={0} and GroupId={1} and UserId<>{2}", MatchId, GroupId, UserId);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString());
            }
            return result > 0;
        }

        /// <summary>
        /// 批量修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool UpdateBatchGroupAudit(int matchId, List<int> groupId, int isAudit)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CompetitionUser set IsAudit=@IsAudit");
            strSql.Append(" where CompetitionId=@CompetitionId and GroupId in(" + string.Join(",", groupId) + ")");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@IsAudit", isAudit, dbType: DbType.Int32);
                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }



        public List<int> GetIndexNum(int userId, int collegeId)
        {
            List<int> result = new List<int>();
            var param = new DynamicParameters();


            int temp1, temp2, temp3, temp4;

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                param.Add("@userId", userId, dbType: DbType.Int32);
                param.Add("@temp1", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@temp2", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@temp3", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@temp4", dbType: DbType.Int32, direction: ParameterDirection.Output);


                result = conn.Query<int>("Proc_GetIndexNum", param, commandType: CommandType.StoredProcedure).ToList();
                temp1 = param.Get<int>("@temp1");
                temp2 = param.Get<int>("@temp2");
                temp3 = param.Get<int>("@temp3");
                temp4 = param.Get<int>("@temp4");

                result.Add(temp1);
                result.Add(temp2);
                result.Add(temp3);
                result.Add(temp4);
            }

            return result;
        }
    }
}
