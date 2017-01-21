using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;
using VM;
using ServiceStack.Redis;
using System.Configuration;
using System.Runtime.Caching;
using System.Web.UI;
using ServiceStack.Redis.Generic;
using Newtonsoft.Json;

namespace Web
{
    /// <summary>
    /// 考试管理服务
    /// </summary>
    public static class ExamCaches
    {
        private static ObjectCache localCacheManager = null;
        /// <summary>
        /// 本地缓存管理
        /// </summary>
        private static ObjectCache LocalCacheManager
        {
            get
            {
                return MemoryCache.Default;
                if (null == localCacheManager)
                {
                    localCacheManager = MemoryCache.Default;
                }
                return localCacheManager;
            }
        }

        #region Redis缓存关键字
        /// <summary>
        /// 习题信息缓存key
        /// </summary>
        static string redisQuestionKey = "RedisQuestionInfo";

        /// <summary>
        /// 理论试卷信息缓存key
        /// </summary>
        static string redisPaperKey = "RedisPaperInfo";

        /// <summary>
        /// 用户答卷信息缓存key
        /// </summary>
        static string redisUserSummaryKey = "RedisUserSummaryInfo";

        #endregion

        #region 缓存容器

        /// <summary>
        /// 题目缓存
        /// </summary>
        public static SynchronisedDictionary<int, QuestionVM> QuestionCache;
        /// <summary>
        /// 试卷缓存
        /// </summary>
        public static SynchronisedDictionary<int, PaperVM> PaperCache;
        /// <summary>
        /// 用户答卷缓存
        /// </summary>
        public static SynchronisedDictionary<int, List<PaperUserSummaryVM>> UserSummaryCache;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            QuestionCache = new SynchronisedDictionary<int, QuestionVM>(l => l.Id, LoadQuestionCache());

            PaperCache = new SynchronisedDictionary<int, PaperVM>(l => l.Id, LoadPaperCache());
            LogHelper.Log.WriteInfo("试卷缓存加载完毕：试卷数量" + PaperCache.Count);

            UserSummaryCache = new SynchronisedDictionary<int, List<PaperUserSummaryVM>>(null);
            LoadUserSummaryCache();
        }

        #region 习题信息缓存操作
        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        static List<QuestionVM> LoadQuestionCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisQuestionHash = redisClient.Hashes["QuestionHash"];
            redisQuestionHash.Clear();

            List<QuestionVM> list = SvrFactory.Instance.ExamSvr.GetAllQuestions(null);
            foreach (var item in list)
            {
                redisQuestionHash.Add(item.Id.ToString(), JsonConvert.SerializeObject(item));
            }
            //30分钟过期
            redisClient.Expire(redisQuestionKey, 60*30);
                //redisQuestionVM.StoreAll(list);
            return list;
        }

        private static object objCurQuestionCache = new object();
        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<QuestionVM> CurQuestionCache()
        {
            if (null == LocalCacheManager.Get(redisQuestionKey))
            {
                lock (objCurQuestionCache)
                {
                    if (null == LocalCacheManager.Get(redisQuestionKey))
                    {
                        RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
                        var redisQuestionHash = redisClient.Hashes["QuestionHash"];

                        var result = new List<QuestionVM>();
                        redisQuestionHash.ToList().ForEach(x =>
                        {
                            result.Add(JsonConvert.DeserializeObject<QuestionVM>(x.Value));
                        });
                        if (result == null || result.Count == 0)
                        {
                            result = LoadQuestionCache();
                        }

                        //添加到localcache
                        CacheItemPolicy policy = new CacheItemPolicy();
                        policy.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                        CacheItem cacheItem = new CacheItem(redisQuestionKey, result);
                        LocalCacheManager.Set(cacheItem, policy);
                        return result;
                    }
                    else
                    {
                        return LocalCacheManager.Get(redisQuestionKey) as List<QuestionVM>;
                    }
                }
            }
            else
            {
                return LocalCacheManager.Get(redisQuestionKey) as List<QuestionVM>;
            }

        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static QuestionVM GetQuestionCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisQuestionHash = redisClient.Hashes["QuestionHash"];
            string key = id.ToString();

            var json = redisQuestionHash[key];
            if (json != null)
            {
                var vm = JsonConvert.DeserializeObject<QuestionVM>(json);
                return vm;
            }
            else
            {
                QuestionVM vm = SvrFactory.Instance.ExamSvr.GetQuestion(id);
                if (vm != null)
                {
                    string jsonStr = JsonConvert.SerializeObject(vm);
                    redisQuestionHash.Add(key, jsonStr);
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetQuestionCache(int id, QuestionVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisQuestionHash = redisClient.Hashes["QuestionHash"];
            redisQuestionHash.Add(id.ToString(), JsonConvert.SerializeObject(value));
            LocalCacheManager.Remove(redisQuestionKey);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveQuestionCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisQuestionHash = redisClient.Hashes["QuestionHash"];
            redisQuestionHash.Remove(id.ToString());
            LocalCacheManager.Remove(redisQuestionKey);
        }
        #endregion

        #region 理论试卷信息缓存操作
        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        static List<PaperVM> LoadPaperCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisPaperVM = redisClient.Hashes["PaperVM"];
            //IRedisTypedClient<PaperVM> redisPaperVM = redisClient.As<PaperVM>();
            //IRedisSet<PaperVM> ListPaper = redisPaperVM.Sets[redisPaperKey];
            redisPaperVM.Clear();
            List<PaperVM> list = SvrFactory.Instance.ExamSvr.GetAllPaperList(null).ToList();
            foreach (var item in list)
            {
                redisPaperVM.Add(item.Id.ToString(), JsonConvert.SerializeObject(item));
            }

            //return list;
            //ListPaper.Clear();
            //redisPaperVM.DeleteAll();
            //List<PaperVM> list = SvrFactory.Instance.ExamSvr.GetAllPaperList(null).ToList();
            //if (list != null && list.Count != 0)
            //    redisPaperVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<PaperVM> CurPaperCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisPaperVM = redisClient.Hashes["PaperVM"];
            //IRedisTypedClient<PaperVM> redisPaperVM = redisClient.As<PaperVM>();
            //JsonConvert.DeserializeObject<PaperVM>(redisPaperVM);
            var result = new List<PaperVM>();
            redisPaperVM.ToList().ForEach(x=>
            {
                result.Add(JsonConvert.DeserializeObject<PaperVM>(x.Value));
            });

            if (result == null || result.Count == 0)
            {
                return LoadPaperCache();
            }
            return result;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static PaperVM GetPaperCache(int id)
        {
            string key = id.ToString();
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisPaperVM = redisClient.Hashes["PaperVM"];


            //PaperVM vm
            var json = redisPaperVM[key];
            if (json != null)
            {
                var vm = JsonConvert.DeserializeObject<PaperVM>(json);
                return vm;
            }
            else
            {
                PaperVM vm = SvrFactory.Instance.ExamSvr.GetPaper(id);
                if (vm != null)
                {
                    string jsonStr = JsonConvert.SerializeObject(vm);
                    redisPaperVM.Add(key, jsonStr);
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetPaperCache(int id, PaperVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisPaperVM = redisClient.Hashes["PaperVM"];
            //IRedisTypedClient<PaperVM> redisPaperVM = redisClient.As<PaperVM>();
            redisPaperVM.Add(id.ToString(), JsonConvert.SerializeObject(value));
            //redisPaperVM.DeleteById(id);
            //redisPaperVM.Store(value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemovePaperCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var redisPaperVM = redisClient.Hashes["PaperVM"];
            //IRedisTypedClient<PaperVM> redisPaperVM = redisClient.As<PaperVM>();

            redisPaperVM.Remove(id.ToString());
        }

        #endregion

        #region 用户答卷信息缓存操作
        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        static List<List<PaperUserSummaryVM>> LoadUserSummaryCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<List<PaperUserSummaryVM>> redisUserSummaryVM = redisClient.As<List<PaperUserSummaryVM>>();
            IRedisSet<List<PaperUserSummaryVM>> ListUserSummary = redisUserSummaryVM.Sets[redisUserSummaryKey];

            ListUserSummary.Clear();
            redisUserSummaryVM.DeleteAll();
            List<List<PaperUserSummaryVM>> list = new List<List<PaperUserSummaryVM>>();
            if (list != null && list.Count != 0)
                redisUserSummaryVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static List<PaperUserSummaryVM> GetUserSummaryCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<List<PaperUserSummaryVM>> redisUserSummaryVM = redisClient.As<List<PaperUserSummaryVM>>();

            List<PaperUserSummaryVM> vm = redisUserSummaryVM.GetById(id);
            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.ExamSvr.GetAllUserSummaryByPaperId(id);
                if (vm != null)
                {
                    redisUserSummaryVM.Store(vm);
                }
                return vm;
            }
        }
        #endregion

        #region 用户理论试卷缓存操作
        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        static List<PaperVM> LoadUserPaperCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口

            var userPaperHash = redisClient.Hashes["UserPaperInfo"];
            Dictionary<string, string> stringMap = new Dictionary<string, string>();
            userPaperHash.Clear();
            List<PaperVM> list = new List<PaperVM>();
            foreach (var item in list)
            {
                userPaperHash.Add(item.Id.ToString() + "_" + item.UserSummary[0].UserId, JsonConvert.SerializeObject(item));
            }

            return list;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static PaperVM GetUserPaperCache(int paperId, int userId)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            var userPaperHash = redisClient.Hashes["UserPaperInfo"];

            string idKey = paperId.ToString() + "_" + userId.ToString();
            PaperVM vm = null;
            if (!string.IsNullOrEmpty(userPaperHash[idKey]))
            {
                vm = JsonConvert.DeserializeObject<PaperVM>(userPaperHash[idKey]);
            }

            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.ExamSvr.GetUserPaperProc(paperId, userId);
                if (vm != null)
                {
                    userPaperHash.Add(idKey, JsonConvert.SerializeObject(vm));
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetUserPaperCache(int paperId, int userId, PaperVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            string idKey = paperId.ToString() + "_" + userId.ToString();
            var userPaperHash = redisClient.Hashes["UserPaperInfo"];

            userPaperHash.Add(idKey, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void RemoveUserPaperCache(int paperId, int userId)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            string idKey = paperId.ToString() + "_" + userId.ToString();
            var userPaperHash = redisClient.Hashes["UserPaperInfo"];

            userPaperHash.Remove(idKey);
        }
        #endregion
    }
}