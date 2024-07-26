using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;
using System.Data;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ParentController : Controller
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        public async Task<IActionResult> DeleteParent()
        {
            if (await _parentService.DeleteParent("fd2a3623-cb5c-4e7d-a800-ada8a334457b"))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> CreateParent()
        {
            Parent parent = new Parent()
            {
                FirstName = "Test",
                LastName = "Test",
                Students = new List<Student>()
                {
                    new Student()
                    {
                        FirstName= "TestS1",
                        LastName= "TestS1",
                    },
                    new Student()
                    {
                        FirstName= "TestS2",
                        LastName= "TestS2",
                    }
                }
            };
            if(await _parentService.CreateParent(parent))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");

            }
        }
    }
}
