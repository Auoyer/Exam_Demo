using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class Activities
    {
        public Activities() {
            this.CreateTime = DateTime.Now;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string BackImagePath { get; set; }

        [DataMember]
        public string CompetitionPurpose { get; set; }


        [DataMember]
        public string CompetitionTime { get; set; }

        [DataMember]
        public string Organization { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int CollegeId { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }

        [DataMember]
        public DateTime? ModifyTime { get; set; }
    }
}
