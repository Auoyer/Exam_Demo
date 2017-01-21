using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class Award
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CollegeId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int AwardType { get; set; }

        [DataMember]
        public string AwardTypeComment { get; set; }

        [DataMember]
        public string AwardDescription { get; set; }

        [DataMember]
        public bool IsVisible { get; set; }

        //----------------------------扩展字段--------------------------------

        [DataMember]
        public int chbtd { get; set; } //特等奖是否选中
        [DataMember]
        public int hdtd { set; get; }//特等奖的ID
        [DataMember]
        public string tdAwardTypeComment { set; get; }//特等奖的注释
        [DataMember]
        public string tdAwardDescription { set; get; }//特等奖的描述

        [DataMember]
        public int chbyd { get; set; }

        [DataMember]
        public int hdyd { set; get; }

        [DataMember]
        public string ydAwardTypeComment { set; get; }

        [DataMember]
        public string ydAwardDescription { set; get; }
        [DataMember]
        public int chbed { get; set; }
        [DataMember]
        public int hded { set; get; }
        [DataMember]
        public string edAwardTypeComment { set; get; }
        [DataMember]
        public string edAwardDescription { set; get; }
        
        [DataMember]
        public int chbsd { get; set; }
        [DataMember]
        public int hdsd { set; get; }

        [DataMember]
        public string sdAwardTypeComment { set; get; }

        [DataMember]
        public string sdAwardDescription { set; get; }

        [DataMember]
        public int chbyx { get; set; }

        [DataMember]
        public int hdyx { set; get; }

        [DataMember]
        public string yxAwardTypeComment { set; get; }

        [DataMember]
        public string yxAwardDescription { set; get; }

        [DataMember]
        public int chbcs { get; set; }

        [DataMember]
        public int hdcs { set; get; }

        [DataMember]
        public string csAwardTypeComment { set; get; }

        [DataMember]
        public string csAwardDescription { set; get; }
    }
}
