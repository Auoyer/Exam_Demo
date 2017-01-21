using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Factory;
using VM;
using Utils;

namespace Web
{
    public class UserTimeSummary
    {
        /// <summary>
        /// 获取有效时间
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public static List<UserTimeSummaryVM> GetUserTimeSummary(int userId)
        {
            List<UserTimeSummaryVM> list = null;

            UserTimeSummaryVM model = new UserTimeSummaryVM();

            if (TrainingCaches.CurUserTimeCache().Count(x => x.UserId == userId) == 0)
            {
                //缓存没有时，从数据库加载
                TrainSearch ts = new TrainSearch()
                {
                    UserId = userId
                };
               
                list = SvrFactory.Instance.TrainingSvr.GetUserTimeSummarylist(ts);

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        TrainingCaches.SetUserTimeCache(item.Id, item);
                    }
                }
            }
            else
            {
                list = TrainingCaches.CurUserTimeCache().Where(x => x.UserId == userId).ToList();
            }
            return list;
        }
    }
}