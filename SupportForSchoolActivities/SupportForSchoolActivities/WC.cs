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
        public static DateTime BeginningOfSchoolYear = new DateTime(2023, 9, 1);
        public static DateTime EndOfSchoolYear = new DateTime(2023, 10, 30);
    }
}
