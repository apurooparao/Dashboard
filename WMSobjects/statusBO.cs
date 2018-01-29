using System;


namespace WMSobjects
{
   public class statusBO
    {
        public int wmsId { get; set; }
        public int status { get; set; }
        public int assignedTo { get; set; }
        public string comment { get; set; }
        public string materialsUsed { get; set; }
        public string teamMembers { get; set; }
        public DateTime? timeIn { get; set; }
        public DateTime? timeOut { get; set; }

        public int closureFlag { get; set; }
    }
}
