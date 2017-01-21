using System;
using System.Runtime.Serialization;

namespace Match.API
{
		/// <summary>
 	///荣誉榜
	/// </summary>
		[DataContract]
	public class HonorRoll
	{
		public HonorRoll()
		{
			
		}
		
      	/// <summary>
		/// Id
        /// </summary>		
        [DataMember]
		public int Id { get; set; }    
		    
		/// <summary>
		/// HomePageId
        /// </summary>		
        [DataMember]
		public int HomePageId { get; set; }    
		    
		/// <summary>
		/// CollegeId
        /// </summary>		
        [DataMember]
		public int CollegeId { get; set; }    
		    
		/// <summary>
		/// 大赛名称
        /// </summary>		
        [DataMember]
		public string CompetitionName { get; set; }    
		    
		/// <summary>
		/// CompetitionId
        /// </summary>		
        [DataMember]
		public int CompetitionId { get; set; }    
		    
			}
}