using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class News
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string _Content { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        private DateTime? _ReleaseTime;

        [DataMember]
        public string ReleaseTime
        {
            get
            {
                if (!_ReleaseTime.HasValue)
                {
                    return DateTime.MaxValue.ToString("yyyy/MM/dd");
                }
                return _ReleaseTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
            set
            {
                _ReleaseTime = DateTime.Parse(value);
            }
        }

        [DataMember]
        public int CollegeId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public bool IsHidden { get; set; }

        [DataMember]
        public int Num { get; set; }

        [DataMember]
        public string Image { get; set; }
    }
}
