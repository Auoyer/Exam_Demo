using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Server.Factory;
using Utils;
using VM;

namespace Web
{
    public class NumHelper
    {
        private static NumHelper _instance;
        private static object lockObj = new object();
        private static Dictionary<int, NumberVM> numCache;
        private static Timer timer;
        private NumHelper() { }

        public static NumHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new NumHelper();
                            numCache = new Dictionary<int, NumberVM>();
                            timer = new Timer(2 * 60 * 1000);//每5分钟
                            timer.Elapsed += new ElapsedEventHandler((obj, e) =>
                            {
                                lock (lockObj)
                                {
                                    var list = (from x in numCache select x.Value).ToList();
                                    foreach (var item in list)
                                    {
                                        if (item.UsedMaxCode != item.LastUsedMaxCode)
                                        {
                                            //更新数据库
                                            bool flag = SvrFactory.Instance.TrainingSvr.UpdateNumber(item);
                                            if (flag)
                                            {
                                                item.LastUsedMaxCode = item.UsedMaxCode;
                                                numCache[item.NumberType] = item;
                                            }
                                            else
                                            {
                                                LogHelper.Log.WriteError(string.Format("编号类型{0}更新数据库失败", item.NumberType));
                                            }
                                        }
                                    }
                                }
                            });
                            timer.Start();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetNum(NumberType type)
        {
            StringBuilder sb = new StringBuilder();
            string Prefix = string.Empty;
            int Figure = 6;
            
            //前缀
            switch (type)
            {
                case NumberType.Customer:
                    {
                        Prefix = "C";
                        Figure = 5;
                    }
                    break;
                case NumberType.Proposal:
                    {
                        Prefix = "FPR";
                        Figure = 6;
                    }
                    break;
            }


            lock (lockObj)
            {
                if (numCache.ContainsKey((int)type))
                {
                    #region 操作内存
                    var model = numCache[(int)type];
                    //内存有，判断日期是否是今日
                    if (model.CurrentDate.Date == DateTime.Now.Date)
                    {
                        //是否超出最大长度
                        if (model.UsedMaxCode >= Math.Pow(10, model.Figure) - 1)
                        {
                            throw new Exception("所请求的单号类型已达到今日生成上限！");
                        }
                        model.UsedMaxCode += 1;

                        //拼接编号
                        sb.Append(Prefix);
                        sb.Append(DateTime.Now.ToString("yyyyMMdd"));
                        sb.Append(model.UsedMaxCode.ToString().PadLeft(model.Figure, '0'));

                        //更新缓存
                        numCache[(int)type] = model;
                    }
                    else
                    {
                        //新增
                        NumberVM entity = new NumberVM
                        {
                            NumberType = (int)type,
                            Prefix = Prefix,
                            CurrentDate = DateTime.Now,
                            UsedMaxCode = 1,
                            Figure = Figure
                        };
                        entity.Id = SvrFactory.Instance.TrainingSvr.AddNumber(entity);

                        //拼接编号
                        sb.Append(Prefix);
                        sb.Append(DateTime.Now.ToString("yyyyMMdd"));
                        sb.Append(entity.UsedMaxCode.ToString().PadLeft(model.Figure, '0'));

                        //更新缓存
                        numCache[(int)type] = entity;
                    } 
                    #endregion
                }
                else
                {
                    #region 内存无,操作数据库
                    var model = SvrFactory.Instance.TrainingSvr.GetNumber((int)type);
                    if (model != null && model.CurrentDate.Date == DateTime.Now.Date)
                    {
                        //是否超出最大长度
                        if (model.UsedMaxCode >= Math.Pow(10, model.Figure) - 1)
                        {
                            throw new Exception("所请求的单号类型已达到今日生成上限！");
                        }
                        model.UsedMaxCode += 1;

                        //拼接编号
                        sb.Append(Prefix);
                        sb.Append(DateTime.Now.ToString("yyyyMMdd"));
                        sb.Append(model.UsedMaxCode.ToString().PadLeft(model.Figure, '0'));

                        //插入缓存
                        numCache.Add((int)type, model);
                    }
                    else
                    {
                        //新增
                        NumberVM entity = new NumberVM
                        {
                            NumberType = (int)type,
                            Prefix = Prefix,
                            CurrentDate = DateTime.Now,
                            UsedMaxCode = 1,
                            Figure = Figure
                        };
                        entity.Id = SvrFactory.Instance.TrainingSvr.AddNumber(entity);

                        //拼接编号
                        sb.Append(Prefix);
                        sb.Append(DateTime.Now.ToString("yyyyMMdd"));
                        sb.Append(entity.UsedMaxCode.ToString().PadLeft(entity.Figure, '0'));

                        //更新缓存
                        numCache.Add((int)type, entity);
                    }  
                    #endregion
                }
            }

            return sb.ToString();
        }




    }
}