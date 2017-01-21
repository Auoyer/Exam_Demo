using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class Notice
    {
        public Notice()
        { 
        
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int NoticeType { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }

        [DataMember]
        public string _CreateTime { get; set; }

        [DataMember]
        public DateTime? ModifyTime { get; set; }

        [DataMember]
        public string _ModifyTime { get; set; }

        [DataMember]
        public int CollegeId { get; set; }
    }
}
