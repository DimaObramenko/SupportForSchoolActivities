using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Models.ViewModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System.Data;

namespace SupportForSchoolActivities.Controllers.EntitiesControllers
{
    [Authorize(Roles = WC.ParentRole)]
    public class RemarkController : Controller
    {
        private readonly IRemarkService _remarkService;
        private readonly IStudentService _studentService;
        private readonly IParentService _parentService;

        public RemarkController(IRemarkService remarkService, IStudentService studentService, IParentService parentService)
        {
            _remarkService = remarkService;
            _studentService = studentService;
            _parentService = parentService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var student = await _studentService.GetStudent(WC.StudentId);
            var remarks = (await _remarkService.GetAllRemarks()).Where(r => r.Student.Id == student?.Id).ToList();

            RemarkVM remarkVM = new RemarkVM()
            {
                Student = student,
                Remarks = remarks
            };
            return View(remarkVM);
        }
    }
}
