using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class ActivityImage
    {
        [DataMember]
        public int ActivityImageId { get; set; }

        [DataMember]
        public int HomePageId { get; set; }

        [DataMember]
        public int CollegeId { get; set; }

        [DataMember]
        public string ImageDescription { get; set; }

        [DataMember]
        public string ActivityImagePath { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }
    }
}
