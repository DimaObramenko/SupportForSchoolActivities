using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities
{
    public static class WC
    {
        public const string AdminRole = "Admin";
        public const string StudentRole = "Student";
        public const string TeacherRole = "Teacher";
        public const string ParentRole = "Parent";
        public const int NumberOfLessons = 8;
        public static List<Schedule> Schedules = new List<Schedule>();
        public static List<Student> Students = new List<Student>();
        public static DateTime BeginningOfSchoolYear = new DateTime(2023, 9, 4);
        public static DateTime EndOfSchoolYear = new DateTime(2023, 12, 31);
        public static DateTime DateForEditMark;
        public static int ClassNumberForJournal;
        public static DateTime DatetForHomework;
        public static DateTime WeekStartDate = new DateTime(2023, 9, 4);
        public static DateTime WeekEndDate = new DateTime(2023, 9, 10);
    }
}
