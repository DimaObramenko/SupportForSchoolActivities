using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class RemarkVM
    {
        public Student Student { get; set; }
        public string StudentId { get; set; }
        public List<Remark> Remarks { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
