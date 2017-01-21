using Server.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utils;
using VM;

namespace Web.Controllers
{
    public class QuestionResController : Controller
    {
        #region 超管端-习题资源

        #region 章节题库页面 ActionResult Questions()
        /// <summary>
        /// 章节题库页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Questions()
        {
            return View();
        }
        #endregion

        #region 获取章节列表 ActionResult GetCharpterList(int pageIndex, int pageSize)
        /// <summary>
        /// 获取章节列表
        /// </summary>
        public ActionResult GetCharpterList(int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int curCollegeId = MvcHelper.User.CollegeId;

            var list = (from x in TrainingCaches.CurChapterCache()
                        where (x.CollegeId == curCollegeId || x.CollegeId == 0)
                        orderby x.CollegeId, x.CreateDate
                        select x
                        ).Page(pageIndex, pageSize, out totalCount);
            PagedList<TheoryChapterVM> result = new PagedList<TheoryChapterVM>(list, pageIndex, pageSize, totalCount);
            return Json(new JsonModel(true, string.Empty, result), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节下的题型列表 GetQuestionTypeList(int charpterId)
        /// <summary>
        /// 获取指定章节下的题型列表
        /// </summary>
        /// <param name="charpterId"></param>
        /// <returns></returns>
        public ActionResult GetQuestionTypeList(int charpterId)
        {
            int curCollegeId = MvcHelper.User.CollegeId;

            //教师列表分页
            List<TheoryQuestionTypeVM> result = new List<TheoryQuestionTypeVM>();

            var res = (from x in TrainingCaches.CurQueTypeCache()
                       where x.TheoryChapterId == charpterId
                       orderby x.CreateDate
                       select x);
            if (res.Count() > 0)
            {
                result.AddRange(res);
            }
            var allQue = ExamCaches.CurQuestionCache();
            if (result.Count > 0)
            {
                Parallel.ForEach(result, l =>
                {
                    l.CurQuestionCount = allQue.Count(m => m.CharpterID == l.Id && m.Status == (int)QuestionStauts.Open && (m.CollegeId == curCollegeId || m.CollegeId == 0));
                });
            }

            return Json(new JsonModel(true, string.Empty, result)); //返回结果
        }
        #endregion

        #region 题目列表页面 ActionResult QuestionList()
        /// <summary>
        /// 题目列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionList()
        {
            return View();
        }
        #endregion

        #region 获取理论考试题目列表 ActionResult GetTheoryQuestionList(int pageIndex, int pageSize, int typeid, string keyword, List<int> Listtypeid, int CharpterId)
        /// <summary>
        /// 获取理论考试题目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTheoryQuestionList(int pageIndex, int pageSize, int CharpterId, int structType)
        {
            int total = 0;

            QuestionSearch ts = new QuestionSearch()
            {
                QuestionTypeId = structType,
                Status = (int)QuestionStauts.Open,
                BySchool = true
            };

            var questionTypeCacheList = TrainingCaches.CurQueTypeCache();
            var theoryChapterCacheList = TrainingCaches.CurChapterCache();

            if (CharpterId != 0)
            {
                int typeid = questionTypeCacheList.Where(x => x.TheoryChapterId == CharpterId && x.TypeName == (structType == 1 ? "单选题" : (structType == 2 ? "多选题" : "判断题"))).Select(x => x.Id).FirstOrDefault();
                ts.CharpterID = typeid;
            }
            var list = SvrFactory.Instance.ExamSvr.GetAllQuestions3(ts, pageIndex, pageSize, out total);

            Parallel.ForEach(list, l =>
            {
                l.StrStructType = questionTypeCacheList.Where(x => x.Id == l.CharpterID).Select(x => x.TypeName).FirstOrDefault();
                l.TheoryCharpter = questionTypeCacheList.Where(x => x.Id == l.CharpterID).Select(x => x.TheoryChapterId).FirstOrDefault();
                l.strTheoryCharpter = theoryChapterCacheList.Where(x => x.Id == l.TheoryCharpter).Select(x => x.ChapterName).FirstOrDefault();
                l.strCollege = "系统内置";
            });
            PagedList<QuestionVM> result = new PagedList<QuestionVM>(list, pageIndex, pageSize, total);
            return Json(new JsonModel(true, "", result));

        }
        #endregion

        #region 判断题目来源 ActionResult CheckQuestionSource(int Id)
        /// <summary>
        /// 判断题目来源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult CheckQuestionSource(int Id)
        {
            var mo = ExamCaches.GetQuestionCache(Id);
            if (mo != null)
            {
                if (mo.CollegeId == 0)
                {
                    return Json(new JsonModel(true, "", true));
                }
            }

            return Json(new JsonModel(true, "", false));
        }
        #endregion

        #region 单选题页面 ActionResult QuestionDetail1()
        /// <summary>
        /// 单选题页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionDetail1(int? QuestionId)
        {
            if (QuestionId.HasValue && QuestionId.Value > 0)
            {
                SvrFactory.Instance.ExamSvr.ChangeQuestionViewStatus(QuestionId.Value);
            }
            return View();
        }
        #endregion

        #region 多选题页面 ActionResult QuestionDetail2()
        /// <summary>
        /// 多选题页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionDetail2(int? QuestionId)
        {
            if (QuestionId.HasValue && QuestionId.Value > 0)
            {
                SvrFactory.Instance.ExamSvr.ChangeQuestionViewStatus(QuestionId.Value);
            }
            return View();
        }
        #endregion

        #region 判断题页面 ActionResult QuestionDetail3()
        /// <summary>
        /// 判断题页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionDetail3(int? QuestionId)
        {
            if (QuestionId.HasValue && QuestionId.Value > 0)
            {
                SvrFactory.Instance.ExamSvr.ChangeQuestionViewStatus(QuestionId.Value);
            }
            return View();
        }
        #endregion

        #region 获取对应的题目类型的数量 ActionResult GetTitleCount(string StructTypeName, int CharpterID)
        /// <summary>
        /// 获取对应的题目类型的数量
        /// </summary>
        /// <param name="CharpterID"></param>
        /// <returns></returns>
        public ActionResult GetTitleCount(int CharpterID)
        {
            int count = 0;
            var mvcUser = MvcHelper.User;
            count = ExamCaches.CurQuestionCache().Count(m => m.CharpterID == CharpterID && (m.CollegeId == mvcUser.CollegeId || m.CollegeId == 0) && m.Status == (int)QuestionStauts.Open);

            return Json(new JsonModel(true, "", new { Count = count }));
        }
        #endregion

        #region 获取题目相关信息 ActionResult GetQuestionObj(int QuestionId)
        /// <summary>
        /// 获取题目相关信息
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuestionObj(int QuestionId)
        {
            var model = SvrFactory.Instance.ExamSvr.GetQuestion(QuestionId);
            return Json(new JsonModel(true, "", new { TE = model }));
        }
        #endregion

        #region 上传习题附件 ActionResult UploadFile(string resourceName)
        /// <summary>
        /// 上传习题附件
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string resourceName)
        {
            string FilePath = string.Empty;
            string ErrorMessage = string.Empty;
            //路径
            string msgPath = string.Empty;

            //先上传文件至服务器
            try
            {
                if (!Upload(resourceName, out msgPath))
                {
                    if (msgPath.Equals("上传不能大于500KB!"))
                    {
                        msgPath = "您上传的资源超过500KB，请重新上传";
                        return Content("{\"result\":false,\"error\": \"" + msgPath + "\"}", "text/html");
                    }
                }
                FileUpload FileUpload = new FileUpload();
                FileUpload.UploadPath = FileUpLoadKeys.UploadPath;
                FileUpload.MaxLength = 204800;
                FileUpload.Type = "jpg|gif|jpeg|png|doc|txt|ppt|pptx|xls|xlsx|docx|pdf";
                FileUpload.Save();
                FilePath = string.Format("{0}{1}/{2}", FileUpLoadKeys.UploadPath, FileUpload.SavePath, FileUpload.SaveName);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Content("{\"result\":false,\"error\": \"" + ErrorMessage + "\"}", "text/html");
            }

            return Content("{\"result\":true,\"error\": \"" + FilePath + "\"}", "text/html");
        }
        #endregion

        #region 文件上传 bool Upload(string filename, out string path)
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="filename">要上传的文件名字</param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Upload(string filename, out string path)
        {
            path = string.Empty;
            //循环是考虑多个文件上传
            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 1024 * 0.5 * 1024)
                {
                    path = "上传不能大于500KB!";
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 新增/修改习题 AddUpdateQuestion(QuestionVM model)
        /// <summary>
        /// 新增/修改习题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUpdateQuestion(QuestionVM model)
        {
            var mvcUser = MvcHelper.User;
            bool isSuccess = false;
            string errCode = string.Empty;
            model.UserId = mvcUser.Id;
            model.CollegeId = mvcUser.CollegeId;
            model.CreatedTime = DateTime.Now;

            int ID = model.Id;
            if (model.Id == 0)
            {
                #region 新增试题
                QuestionSearch search = new QuestionSearch()
                {
                    Context = model.Context,
                    CollegeId = mvcUser.CollegeId,
                    CharpterID = model.CharpterID,
                    QuestionTypeId = model.StructType,
                    Status = -1
                };
                var list = SvrFactory.Instance.ExamSvr.GetSomeQuestions(search);
                if (list.Count > 0)
                {
                    return Json(new JsonModel(false, "21026", null)); // 您添加的此题在服务器中已存在！
                }

                int count = ExamCaches.CurQuestionCache().Count(m => m.CharpterID == model.CharpterID && m.StructType == model.StructType && m.Status != 3);
                if (model.StructType == (int)StructType.SelectRadio || model.StructType == (int)StructType.SelectCheckBox || model.StructType == (int)StructType.Determine)
                {
                    if (count >= 5000)
                    {
                        return Json(new JsonModel(false, "21027", null)); // 该章节下的该题型已超过5000题，请删除后添加！
                    }
                }

                var result = SvrFactory.Instance.ExamSvr.AddQuestion(model);
                if (result > 0)
                {
                    //成功后添加到缓存
                    model.Id = result;
                    ExamCaches.SetQuestionCache(result, model);

                    //获取本题在list中的位置
                    var index = ExamCaches.CurQuestionCache().Where(x => x.CharpterID == model.CharpterID && (x.UserId == mvcUser.Id || x.UserId == 0) && x.Status != 3).ToList().IndexOf(model);

                    isSuccess = true;
                    return Json(new JsonModel(isSuccess, errCode, new { index = index, result = result }));
                }
                else
                {
                    isSuccess = false;
                    errCode = "20006";//"20006": "新增失败!请联系管理员!",
                }
                #endregion
            }
            else
            {
                #region 添加试题
                QuestionSearch search = new QuestionSearch()
                {
                    Id = model.Id,
                    Context = model.Context,
                    CollegeId = mvcUser.CollegeId,
                    CharpterID = model.CharpterID,
                    QuestionTypeId = model.StructType,
                    isBool = true,
                    Status = -1
                };
                var list = SvrFactory.Instance.ExamSvr.GetSomeQuestions(search);
                if (list.Count > 0)
                {
                    return Json(new JsonModel(false, "21026", null));//您添加的此题在服务器中已存在！
                }
                // 未避免修改或删除的题目已存在与大赛试题中，修改操作实际是将原题逻辑删除，并重新添加试题
                var bo = SvrFactory.Instance.ExamSvr.EditQuestionStatus(model.Id, (int)QuestionStauts.Delete);
                var mo = SvrFactory.Instance.ExamSvr.GetQuestion(model.Id);
                model.CharpterID = mo.CharpterID;
                var result = SvrFactory.Instance.ExamSvr.AddQuestion(model);
                if (result > 0)
                {
                    // 修改缓存缓存
                    mo.Status = (int)QuestionStauts.Delete;
                    ExamCaches.SetQuestionCache(ID, mo);
                    // 成功后添加到缓存    
                    model.Id = result;
                    model.Status = (int)QuestionStauts.Open;
                    ExamCaches.SetQuestionCache(result, model);

                    QuestionVM q = new QuestionVM();
                    q.Id = ID;
                    // 获取本题在list中的位置
                    var index = ExamCaches.CurQuestionCache().Where(x => x.CharpterID == model.CharpterID && x.Status == (int)QuestionStauts.Open && (x.CollegeId == mvcUser.CollegeId || x.CollegeId == 0)).ToList().IndexOf(model);

                    isSuccess = true;
                    return Json(new JsonModel(isSuccess, errCode, new { index = index, result = result }));
                }
                else
                {
                    errCode = "20007";//"20007": "修改失败!请联系管理员!",
                }
                #endregion
            }
            return Json(new JsonModel(isSuccess, errCode, null));
        }
        #endregion

        #region 删除习题 ActionResult DelQuestion(int Id)
        /// <summary>
        /// 删除习题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelQuestion(int Id)
        {
            string errCode = string.Empty;
            var result = false;
            var boo = SvrFactory.Instance.ExamSvr.GetPaperDetail(Id);
            if (boo)
            {
                // 假删
                result = SvrFactory.Instance.ExamSvr.EditQuestionStatus(Id, (int)QuestionStauts.Delete);
                if (result)
                {
                    //修改到缓存
                    var model = SvrFactory.Instance.ExamSvr.GetQuestion(Id);
                    model.Status = (int)QuestionStauts.Delete;
                    ExamCaches.SetQuestionCache(model.Id, model);
                }
            }
            else
            {
                // 真删
                var model = SvrFactory.Instance.ExamSvr.GetQuestion(Id);
                result = SvrFactory.Instance.ExamSvr.DeleteQuestion(Id);
                if (result)
                {
                    //修改到缓存
                    model.Status = (int)QuestionStauts.Delete;
                    ExamCaches.SetQuestionCache(model.Id, model);
                }
            }

            if (result)
            {
                errCode = "21019";//21019 "删除成功!"

            }
            else
            {
                errCode = "20005";//删除失败！
            }
            return Json(new JsonModel(result, errCode, null));
        }
        #endregion

        #region 批量删除习题 DelQuestionBluk(QuestionVM model)
        /// <summary>
        /// 批量删除习题  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DelQuestionBluk(QuestionVM model)
        {
            string errCode = string.Empty;
            // 假删数据Id
            List<int> list1 = new List<int>();

            for (int i = 0; i < model.ListID.Count; i++)
            {
                //获取假删的数据Id
                list1.Add(model.ListID[i]);
            }
            //批量假删
            var result = SvrFactory.Instance.ExamSvr.EditQuestionStatus2(list1);

            if (result)
            {
                for (int i = 0; i < model.ListID.Count; i++)
                {
                    //修改到缓存
                    var models = SvrFactory.Instance.ExamSvr.GetQuestion(model.ListID[i]);
                    models.Status = (int)QuestionStauts.Delete;
                    ExamCaches.SetQuestionCache(models.Id, models);
                }

                errCode = "21019";//删除成功！
            }
            else
            {
                errCode = "20005";//删除失败！
            }
            return Json(new JsonModel(result, errCode, null));
        }
        #endregion

        #region 批量导入习题
        /// <summary>
        /// 批量导入习题
        /// </summary>
        /// <param name="TheoryChapterId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import(int? TheoryChapterId)
        {
            var mvcUser = MvcHelper.User;

            #region 上传文件至服务器
            string FilePath = string.Empty;
            string ErrorMessage = string.Empty;
            // 先上传文件至服务器
            try
            {
                FileUpload FileUpload = new FileUpload("xls");
                FileUpload.UploadPath = FileUpLoadKeys.UploadPath;
                FileUpload.MaxLength = 5 * 1024;//上传大小
                FileUpload.Save();
                FilePath = string.Format("{0}{1}/{2}", FileUpLoadKeys.UploadPath, FileUpload.SavePath, FileUpload.SaveName);
                FilePath = Server.MapPath("~" + FilePath);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Content("{\"result\":false,\"ErrMsg\": \"" + ErrorMessage + "\"}", "text/html");
            }
            #endregion

            #region 从Excel导出到Datatable
            // 开始导入数据库
            int colCount = 0;
            DataTable tb = new DataTable();
            try
            {
                tb = ExcelHelper.ImportExcelFile(FilePath);

                colCount = tb.Columns.Count;
                if (colCount < 9)
                {
                    return Content("{\"result\":false,\"ErrMsg\": \"列数不对！\"}", "text/html");
                }
                else
                {
                    colCount = 9;
                }
            }
            catch (Exception)
            {
                return Content("{\"result\":false,\"ErrMsg\": \"读取文件发生错误！\"}", "text/html");
            }
            #endregion

            List<QuestionVM> QuestionList = new List<QuestionVM>();
            if (colCount == 9)
            {
                // 章节Id
                int ChapterId = 0;

                int typeId_Dan = 0;
                int typeId_Duo = 0;
                int typeId_Pan = 0;
                TrainSearch ts = new TrainSearch()
                {
                    ChapterId = TheoryChapterId
                };
                var list = SvrFactory.Instance.TrainingSvr.GetTheoryQuestionTypelist(ts);

                for (int a = 0; a < list.Count; a++)
                {
                    if (list[a].TypeName == "单选题")
                    {
                        typeId_Dan = list[a].Id;
                    }
                    else if (list[a].TypeName == "多选题")
                    {
                        typeId_Duo = list[a].Id;
                    }
                    else if (list[a].TypeName == "判断题")
                    {
                        typeId_Pan = list[a].Id;
                    }
                }
                var allQue = ExamCaches.CurQuestionCache();
                #region 循环遍历DataTable
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    int j = i + 2;
                    QuestionVM question = new QuestionVM();
                    List<QuestionOptionVM> options = new List<QuestionOptionVM>();
                    List<QuestionAnswerVM> answers = new List<QuestionAnswerVM>();
                    //检测主要的试题题干、题型、正确答案
                    if (tb.Rows[i]["A"] == null || tb.Rows[i]["B"] == null)
                    {
                        ErrorMessage = "第" + j + "行数据不完整！";
                        break;
                    }
                    if (string.IsNullOrEmpty(tb.Rows[i]["A"].ToString().Trim()) || string.IsNullOrEmpty(tb.Rows[i]["B"].ToString().Trim()))
                    {
                        ErrorMessage = "第" + j + "行数据不完整！";
                        break;
                    }
                    //根据题型进行具体检测
                    int questionType = 0;

                    string type = tb.Rows[i]["B"].ToString().Trim();
                    if (type.Equals("单选"))
                    {
                        #region 单选
                        questionType = (int)StructType.SelectRadio;
                        ChapterId = typeId_Dan;
                        #region 选项A、B、C、D是否有值
                        if (tb.Rows[i]["E"] == null
                            || tb.Rows[i]["F"] == null
                            || tb.Rows[i]["G"] == null
                            || tb.Rows[i]["C"] == null)
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        if (string.IsNullOrEmpty(tb.Rows[i]["E"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["F"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["G"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["C"].ToString().Trim()))
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        //选项A
                        string optionA = tb.Rows[i]["E"].ToString().Trim();
                        if (optionA.Length > 80)
                        {
                            ErrorMessage = "第" + j + "行的[选项A]数据不符合要求！";
                            break;
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionA, Sort = 0 });
                        //选项B
                        string optionB = tb.Rows[i]["F"].ToString().Trim();
                        if (optionB.Length > 80 || optionA == optionB)
                        {
                            ErrorMessage = "第" + j + "行的[选项B]数据不符合要求！";
                            break;
                        }

                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionB, Sort = 1 });
                        //选项C
                        string optionC = string.Empty;
                        if (tb.Rows[i]["G"] != null)
                        {
                            optionC = tb.Rows[i]["G"].ToString();
                            if (optionC.Length > 80 || optionA == optionC || optionB == optionC)
                            {
                                ErrorMessage = "第" + j + "行的[选项C]数据不符合要求！";
                                break;
                            }
                        }

                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionC, Sort = 2 });
                        //选项D
                        string optionD = string.Empty;
                        if (tb.Rows[i]["H"] != null)
                        {
                            optionD = tb.Rows[i]["H"].ToString();
                            if (optionD.Length > 80 || optionA == optionD || optionB == optionD || optionC == optionD)
                            {
                                ErrorMessage = "第" + j + "行的[选项D]数据不符合要求！";
                                break;
                            }
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionD, Sort = 3 });
                        //选项E

                        string optionE = string.Empty;
                        if (tb.Rows[i]["I"] != null && !string.IsNullOrEmpty(tb.Rows[i]["I"].ToString()))
                        {
                            optionE = tb.Rows[i]["I"].ToString();
                            if (optionE.Length > 80 || optionA == optionE || optionB == optionE || optionC == optionE || optionD == optionE)
                            {
                                ErrorMessage = "第" + j + "行的[选项E]数据不符合要求！";
                                break;
                            }
                            else { options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionE, Sort = 4 }); }
                        }

                        #endregion

                        #region 答案是否符合规范
                        string answer = tb.Rows[i]["C"].ToString().Trim().ToUpper();
                        //int max = 0;
                        if (answer.Length > 1)
                        {
                            ErrorMessage = "第" + j + "行单选题答案必须唯一！";
                            break;
                        }

                        int _answer = GetAnswer(Convert.ToChar(answer));
                        if (_answer == -1)
                        {
                            ErrorMessage = "第" + j + "行单选题答案不存在！";
                            break;
                        }
                        answers.Add(new QuestionAnswerVM { QuestionId = question.Id, Answer = _answer });
                        #endregion
                        #endregion
                    }
                    else if (type.Equals("多选"))
                    {
                        #region 多选
                        questionType = (int)StructType.SelectCheckBox;
                        ChapterId = typeId_Duo;
                        #region 选项A、B、C、D是否有值
                        if (tb.Rows[i]["E"] == null
                            || tb.Rows[i]["F"] == null
                            || tb.Rows[i]["G"] == null
                            || tb.Rows[i]["H"] == null || tb.Rows[i]["C"] == null)
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        if (string.IsNullOrEmpty(tb.Rows[i]["E"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["F"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["G"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["H"].ToString().Trim()) || string.IsNullOrEmpty(tb.Rows[i]["C"].ToString().Trim()))
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        //选项A
                        string optionA = tb.Rows[i]["E"].ToString().Trim();
                        if (optionA.Length > 80)
                        {
                            ErrorMessage = "第" + j + "行的[选项A]数据不符合要求！";
                            break;
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionA, Sort = 0 });
                        //选项B
                        string optionB = tb.Rows[i]["F"].ToString().Trim();
                        if (optionB.Length > 80 || optionA == optionB)
                        {
                            ErrorMessage = "第" + j + "行的[选项B]数据不符合要求！";
                            break;
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionB, Sort = 1 });
                        //选项C
                        string optionC = string.Empty;
                        if (tb.Rows[i]["G"] != null)
                        {
                            optionC = tb.Rows[i]["G"].ToString().Trim();
                            if (optionC.Length > 80 || optionA == optionC || optionB == optionC)
                            {
                                ErrorMessage = "第" + j + "行的[选项C]数据不符合要求！";
                                break;
                            }
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionC, Sort = 2 });
                        //选项D
                        string optionD = string.Empty;
                        if (tb.Rows[i]["H"] != null)
                        {
                            optionD = tb.Rows[i]["H"].ToString().Trim();
                            if (optionD.Length > 80 || optionA == optionD || optionB == optionD || optionC == optionD)
                            {
                                ErrorMessage = "第" + j + "行的[选项D]数据不符合要求！";
                                break;
                            }
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionD, Sort = 3 });

                        //选项E

                        string optionE = string.Empty;
                        if (tb.Rows[i]["I"] != null && !string.IsNullOrEmpty(tb.Rows[i]["I"].ToString()))
                        {
                            optionE = tb.Rows[i]["I"].ToString();
                            if (optionE.Length > 80 || optionA == optionE || optionB == optionE || optionC == optionE || optionD == optionE)
                            {
                                ErrorMessage = "第" + j + "行的[选项E]数据不符合要求！";
                                break;
                            }
                            else { options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionE, Sort = 4 }); }
                        }



                        #endregion

                        #region 答案是否符合规范
                        string answer = tb.Rows[i]["C"].ToString().Trim().ToUpper();

                        int nu = 0;
                        foreach (char c in answer)
                        {
                            var num = answer.Split(Convert.ToChar(c));
                            if (num.Length >= 3)
                            {
                                ErrorMessage = "第" + j + "行多选题答案存在相同选项！";
                                nu = nu + 1;
                            }
                            int _answer = GetAnswer(c);
                            if (_answer == -1)
                            {
                                ErrorMessage = "第" + j + "行多选题答案不存在！";
                                nu = nu + 1;
                            }

                            answers.Add(new QuestionAnswerVM { QuestionId = question.Id, Answer = _answer });
                        }

                        if (nu > 0)
                        {
                            break;
                        }
                        #endregion




                        #endregion
                    }
                    else if (type.Equals("判断"))
                    {
                        #region 判断
                        questionType = (int)StructType.Determine;
                        ChapterId = typeId_Pan;
                        #region 选项A、B是否有值
                        if (tb.Rows[i]["E"] == null
                            || tb.Rows[i]["F"] == null || tb.Rows[i]["C"] == null)
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        if (string.IsNullOrEmpty(tb.Rows[i]["E"].ToString().Trim())
                            || string.IsNullOrEmpty(tb.Rows[i]["F"].ToString().Trim()) || string.IsNullOrEmpty(tb.Rows[i]["C"].ToString().Trim()))
                        {
                            ErrorMessage = "第" + j + "行数据不完整！";
                            break;
                        }
                        //选项A
                        string optionA = tb.Rows[i]["E"].ToString().Trim();
                        if (optionA.Length > 2)
                        {
                            ErrorMessage = "第" + j + "行的[选项A]数据不符合要求！";
                            break;
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionA, Sort = 0 });
                        //选项B
                        string optionB = tb.Rows[i]["F"].ToString().Trim();
                        if (optionB.Length > 2)
                        {
                            ErrorMessage = "第" + j + "行的[选项B]数据不符合要求！";
                            break;
                        }
                        options.Add(new QuestionOptionVM { QuestionId = question.Id, OptionName = optionB, Sort = 1 });
                        if (optionA == optionB)
                        {
                            ErrorMessage = "第" + j + "行的[选项A]和[选项B]一样";
                            break;
                        }
                        #endregion

                        #region 答案是否符合规范
                        string answer = tb.Rows[i]["C"].ToString().Trim().ToUpper();
                        if (answer.Length > 1)
                        {
                            ErrorMessage = "第" + j + "行判断题答案必须唯一！";
                            break;
                        }
                        int _answer = GetAnswer(answer[0]);
                        if (_answer != 0 && _answer != 1)
                        {
                            ErrorMessage = "第" + j + "行判断题答案只能是A或B！";
                            break;
                        }
                        answers.Add(new QuestionAnswerVM { QuestionId = question.Id, Answer = _answer });
                        #endregion


                        #endregion
                    }
                    else
                    {
                        ErrorMessage = "第" + j + "行题型不正确！";
                        break;
                    }

                    // 试题题干
                    string context = tb.Rows[i]["A"].ToString().Trim();
                    if (context.Length > 300)
                    {
                        ErrorMessage = "第" + j + "行的[试题题干]数据不符合要求！";
                        break;
                    }

                    // 检查重复题目
                    if (QuestionList.Count(x => x.Context.Equals(context) && x.StructType == questionType) > 0)
                    {
                        ErrorMessage = "Excel中第" + j + "行的试题重复！";
                        break;
                    }

                    //解析
                    string analysis = string.Empty;
                    if (tb.Rows[i]["D"] != null)
                    {
                        analysis = tb.Rows[i]["D"].ToString().Trim();
                    }
                    if (analysis.Length > 300)
                    {
                        ErrorMessage = "第" + j + "行的[试题解析]数据不符合要求！";
                        break;
                    }

                    //判断题型的数量是否超过上限
                    int count = allQue.Count(m => m.CharpterID == ChapterId && m.StructType == questionType && m.Status == (int)QuestionStauts.Open);
                    if (questionType == (int)StructType.SelectRadio || questionType == (int)StructType.SelectCheckBox || questionType == (int)StructType.Determine)
                    {
                        if (count >= 5000)
                        {
                            ErrorMessage = "第" + j + "行以下已超过了该题型5000条上限，请删除后重试！";
                            break;
                        }
                    }
                    else
                    {
                        if (count >= 1000)
                        {
                            ErrorMessage = "第" + j + "行以下已超过了该题型1000条上限，请删除后重试！";
                            break;
                        }
                    }


                    question.Context = context;
                    question.StructType = questionType;
                    question.CharpterID = ChapterId;
                    question.Analysis = analysis;
                    question.Status = (int)QuestionStauts.Open;
                    question.UserId = mvcUser.Id;
                    question.CollegeId = mvcUser.CollegeId;
                    question.CreatedTime = DateTime.Now;
                    question.AnswerList = answers;
                    question.OptionList = options;
                    QuestionList.Add(question);

                    //判断重复
                    var result = allQue.Count(m => m.StructType == questionType && m.CharpterID == ChapterId && m.Context == context && m.Status == (int)QuestionStauts.Open && (m.CollegeId == mvcUser.CollegeId || m.CollegeId == 0));
                    if (result > 0)
                    {
                        ErrorMessage = "第" + j + "行的试题在题库中已存在！";
                        return Content("{\"result\":false,\"ErrMsg\": \"" + ErrorMessage + "\"}", "text/html"); ;
                    }
                }

                #endregion
            }

            //提示导入失败的失败信息
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return Content("{\"result\":false,\"ErrMsg\": \"" + ErrorMessage + "\"}", "text/html");
            }
            if (QuestionList.Count > 5000)
            {
                return Content("{\"result\":false,\"ErrMsg\": \"请确认上传文件的题量少于5000道！\"}", "text/html");
            }

            //批量新增试题           
            var lists = SvrFactory.Instance.ExamSvr.BatchAddQuestion(QuestionList);
            if (lists.Count > 0)
            {
                int co = lists.Count;
                for (int i = 0; i < co; i++)
                {
                    //添加到缓存
                    ExamCaches.SetQuestionCache(lists[i].Id, lists[i]);
                }
            }
            else
            {
                return Content("{\"result\":false,\"ErrMsg\": \"批量新增出现异常,请联系管理员！\"}", "text/html");
            }

            return Content("{\"result\":true,\"ErrMsg\": \"" + ErrorMessage + "\"}", "text/html");
        }

        /// <summary>
        /// 获取答案(int)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private int GetAnswer(char c)
        {
            int result = -1;
            switch (c)
            {
                case 'A':
                    result = (int)QuestionAnswer.A;
                    break;
                case 'B':
                    result = (int)QuestionAnswer.B;
                    break;
                case 'C':
                    result = (int)QuestionAnswer.C;
                    break;
                case 'D':
                    result = (int)QuestionAnswer.D;
                    break;
                case 'E':
                    result = (int)QuestionAnswer.E;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取答案(string)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetAnswer(int i)
        {
            string result = string.Empty;
            switch (i)
            {
                case (int)QuestionAnswer.A:
                    result = "A";
                    break;
                case (int)QuestionAnswer.B:
                    result = "B";
                    break;
                case (int)QuestionAnswer.C:
                    result = "C";
                    break;
                case (int)QuestionAnswer.D:
                    result = "D";
                    break;
                case (int)QuestionAnswer.E:
                    result = "E";
                    break;

            }
            return result;
        }
        #endregion

        #region 上一题/下一题
        /// <summary>
        /// 上一题/下一题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuestionId(int index, int collegeId, int structType)
        {
            var QuestionId = 0;
            QuestionId = GetKeyValueByIndex(index, collegeId, structType);
            var model = SvrFactory.Instance.ExamSvr.GetQuestion(QuestionId);
            if (model != null)
            {
                SvrFactory.Instance.ExamSvr.ChangeQuestionViewStatus(QuestionId);
                model.StrStructType = TrainingCaches.GetQueTypeCache(model.CharpterID).TypeName;
            }
            return Json(new JsonModel(true, "", new { model = model, style = QuestionId }));

        }
        #endregion

        #region 根据当前问题位置，获取问题 int GetKeyValueByIndex(int index, int CharpterID, int PaperId)
        /// <summary>
        /// 根据当前问题位置，获取问题(问题类型，问题ID)
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>问题(问题类型，问题ID)</returns>
        public int GetKeyValueByIndex(int index, int collegeId, int structType)
        {
            int mum = 0;

            //从题库管理入口进入
            var model = ExamCaches.CurQuestionCache().Where(x => x.CollegeId == collegeId && x.Status == (int)QuestionStauts.Open && x.StructType == structType).OrderBy(x => x.Id).ToList();
            if (model.Count > 0 && model.Count > index)
            {
                mum = model[index].Id;
            }
            else
            {
                mum = -1;
            }

            return mum;
        }
        #endregion

        /// <summary>
        /// 批量导出习题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportQuestion(int StructType, int CollegeId, int Count)
        {
            int totalCount = 0;
            QuestionSearch ts = new QuestionSearch()
            {
                QuestionTypeId = StructType,
                CollegeId = CollegeId,
                BySchool = true,
                Status = -1
            };
            var questionList = SvrFactory.Instance.ExamSvr.GetAllQuestions3(ts, 1, Count, out totalCount);

            List<QuestionLibVM> questionLibList = new List<QuestionLibVM>();
            if (questionList != null && questionList.Count > 0)
            {
                questionList = questionList.FindAll(x => (x.CollegeId == CollegeId) && (x.StructType == StructType));
            }

            string strStructType = EnumHelper.GetAllEnumDesc((StructType)StructType);

            try
            {
                string sheetName = strStructType;
                DataTable dt = new DataTable(sheetName);

                dt.Columns.Add("试题题干", typeof(string));
                dt.Columns.Add("题型", typeof(string));
                dt.Columns.Add("正确选项", typeof(string));
                dt.Columns.Add("题目解析（可选）", typeof(string));
                dt.Columns.Add("选项A", typeof(string));
                dt.Columns.Add("选项B", typeof(string));
                dt.Columns.Add("选项C", typeof(string));
                dt.Columns.Add("选项D（可选）", typeof(string));
                dt.Columns.Add("选项E（可选）", typeof(string));

                foreach (var item in questionList)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Context;
                    dr[1] = strStructType;
                    string answer = string.Empty;
                    item.AnswerList.ForEach(x =>
                    {
                        answer += GetAnswer(x.Answer);
                    });

                    dr[2] = answer;
                    dr[3] = item.Analysis;
                    int ii = 4;
                    foreach (var ol in item.OptionList)
                    {
                        dr[ii] = ol.OptionName;
                        ii++;
                    }
                    dt.Rows.Add(dr);
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "files\\";
                string newFileName = DateTime.Now.Ticks.ToString() + ".xls";
                ExcelHelper.OutFileToDisk(dt, sheetName, path + newFileName);
                return Json(new JsonModel(true, "", "/files/" + newFileName));

            }
            catch (Exception ex)
            {
                return Json(new JsonModel(false, "导出失败"));
            }
        }

        /// <summary>
        /// 批量导出习题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DownloadQuestion(List<int> QuestionList)
        {
            List<QuestionVM> questions = new List<QuestionVM>();
            foreach (int i in QuestionList)
            {
                var qq = ExamCaches.GetQuestionCache(i);
                if (qq != null)
                {
                    qq.StrStructType = EnumHelper.GetAllEnumDesc((StructType)qq.StructType);
                    questions.Add(qq);
                }
            }

            try
            {
                string sheetName = "Questions";
                DataTable dt = new DataTable(sheetName);

                dt.Columns.Add("试题题干", typeof(string));
                dt.Columns.Add("题型", typeof(string));
                dt.Columns.Add("正确选项", typeof(string));
                dt.Columns.Add("题目解析（可选）", typeof(string));
                dt.Columns.Add("选项A", typeof(string));
                dt.Columns.Add("选项B", typeof(string));
                dt.Columns.Add("选项C", typeof(string));
                dt.Columns.Add("选项D（可选）", typeof(string));
                dt.Columns.Add("选项E（可选）", typeof(string));

                foreach (var item in questions)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Context;
                    dr[1] = item.StrStructType;
                    string answer = string.Empty;
                    item.AnswerList.ForEach(x =>
                    {
                        answer += GetAnswer(x.Answer);
                    });

                    dr[2] = answer;
                    dr[3] = item.Analysis;
                    int ii = 4;
                    foreach (var ol in item.OptionList)
                    {
                        dr[ii] = ol.OptionName;
                        ii++;
                    }
                    dt.Rows.Add(dr);
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "files\\";
                string newFileName = DateTime.Now.Ticks.ToString() + ".xls";
                ExcelHelper.OutFileToDisk(dt, sheetName, path + newFileName);
                return Json(new JsonModel(true, "", "/files/" + newFileName));

            }
            catch (Exception ex)
            {
                return Json(new JsonModel(false, "导出失败"));
            }
        }

        private Dictionary<string, byte[]> downDic = new Dictionary<string, byte[]>();

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">文件所在路径</param>
        /// <param name="name">文件名称(下载时显示的名称)</param>
        /// <returns></returns>
        public ActionResult DownloadFile(string path, string name)
        {
            string file = Path.GetFileName(path);
            string extension = Path.GetExtension(path).ToLower();

            //文件路径
            string fileUploadAddress = ConfigurationManager.AppSettings["FileUploadAddress"];

            string filePath = Path.Combine(fileUploadAddress, file);

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            switch (extension)
            {
                case ".jpg":
                    Response.ContentType = "application/x-jpg";
                    break;
                case ".bmp":
                    Response.ContentType = "application/x-bmp";
                    break;
                case ".gif":
                    Response.ContentType = "image/gif";
                    break;
                case ".png":
                    Response.ContentType = "application/x-png";
                    break;
                case ".pdf":
                    Response.ContentType = "application/pdf";
                    break;
                case ".ppt":
                    Response.ContentType = "application/vnd.ms-powerpoint";
                    break;
                case ".swf":
                    Response.ContentType = "application/x-shockwave-flash";
                    break;
                case ".txt":
                    Response.ContentType = "text/plain";
                    break;
                default:
                    Response.ContentType = "application/octet-stream";
                    break;
            }
            //解决文件名乱码问题  
            string filename = string.Format("{0}{1}", name, extension);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));

            #region 缓存

            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            downDic.Add(file, bytes);
            Response.BinaryWrite(downDic[file]);
            #endregion

            Response.Flush();
            Response.End();
            return new EmptyResult();
        }
        #endregion
	}
}