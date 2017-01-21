using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
  public  class ResourceSearch
    {

      public ResourceSearch()
      {
          
      }

        /// <summary>
        /// 章节Id
        /// </summary>
        public Nullable<int> ChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Nullable<int> UserId { get; set; }
    }
}
