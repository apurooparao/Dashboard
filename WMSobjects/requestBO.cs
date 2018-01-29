using System;

namespace WMSobjects
{
  public  class requestBO
    {
        public int wmsId { get; set; }
        public int branchId { get; set; }
        public int priorityId { get; set; }
        public string affectOperation { get; set; }
        public string scope { get; set; }
        public int floor { get; set; }
        public int sectionId { get; set; }
        public string otherSection { get; set; }
        public string categoryId { get; set; }
        public string remarks { get; set; }
        public string requestor { get; set; }
        public DateTime createdDate { get; set; }
        public int statusId { get; set; }
        public int assignedTo { get; set; }
        public DateTime modifiedDate { get; set; }
        public int modifiedBy { get; set; }

        public int InsUpdFlag { get; set; }
    }
}
