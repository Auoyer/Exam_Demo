using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Utils
{
    public class ListCache<T>
    {
        private List<T> cacheList;
        private ReaderWriterLockSlim readWriteLock;

        public ListCache(List<T> list)
        {
            this.readWriteLock = new ReaderWriterLockSlim();
            this.cacheList = new List<T>();

            if (list != null && list.Count > 0)
            {
                this.cacheList.AddRange(list);
            }
        }

        public void Add(T model)
        {
            try
            {
                this.readWriteLock.EnterWriteLock();
                if (!this.cacheList.Contains(model))
                {
                    this.cacheList.Add(model);
                }
            }
            finally
            {
                this.readWriteLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 基于谓词筛选值序列。
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            IEnumerable<T> result = null;
            try
            {
                this.readWriteLock.EnterReadLock();
                result = this.cacheList.Where(predicate);
            }
            finally
            {
                this.readWriteLock.ExitReadLock();
            }
            return result;
        }

        /// <summary>
        /// 返回序列中满足条件的第一个元素；如果未找到这样的元素，则返回默认值。
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            T result = default(T);
            try
            {
                this.readWriteLock.EnterReadLock();
                result = this.cacheList.FirstOrDefault(predicate);
            }
            finally
            {
                this.readWriteLock.ExitReadLock();
            }
            return result;
        }

        /// <summary>
        /// 移除与指定的谓词所定义的条件相匹配的所有元素。
        /// </summary>
        /// <param name="match"></param>
        public int RemoveAll(Predicate<T> match)
        {
            int result = 0;
            try
            {
                this.readWriteLock.EnterWriteLock();
                result = this.cacheList.RemoveAll(match);
            }
            finally
            {
                this.readWriteLock.ExitWriteLock();
            }
            return result;
        }

        public List<T> GetList()
        {
            return this.cacheList;
        }
    }
}