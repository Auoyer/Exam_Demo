using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class HomePage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CompetitionIntroduction { get; set; }

        [DataMember]
        public string Title1 { get; set; }

        [DataMember]
        public string Title2 { get; set; }

        [DataMember]
        public string Title3 { get; set; }

        [DataMember]
        public string QRCodeImgPath { get; set; }

        [DataMember]
        public string QRCodeIntroduction { get; set; }

        [DataMember]
        public string ComTelConsultation { get; set; }

        [DataMember]
        public string ComPhone { get; set; }

        [DataMember]
        public string ComQQ { get; set; }

        [DataMember]
        public string Step1Description { get; set; }

        [DataMember]
        public string Step2Description { get; set; }

        [DataMember]
        public string Step3Description { get; set; }

        [DataMember]
        public string Step4Description { get; set; }

        [DataMember]
        public int CollegeId { get; set; }
    }
}
