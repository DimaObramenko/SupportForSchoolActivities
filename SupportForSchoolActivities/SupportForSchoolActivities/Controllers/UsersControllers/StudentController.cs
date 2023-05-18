﻿using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> CreateStudent()
        {
            var st = new Student
            {
                FirstName = "a",
                LastName = "b",
                Parent = new Parent()
                {
                    FirstName = "a",
                    LastName = "b",
                }
            };
            if(await _studentService.CreateStudent(st))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteStudent()
        {
            var stu = await _studentService.GetAllStudents();
            var st = stu.FirstOrDefault(s => s.FirstName == "a");
            if(st != null)
            {
                if(await _studentService.DeleteStudent(st.Id))
                {
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> UpdateStudent()
        {
            var stu = await _studentService.GetAllStudents();
            var st = stu.FirstOrDefault(s => s.FirstName == "StudF");
            if(st != null)
            {
                var s = new Student
                {
                    FirstName = "bb",
                    LastName = "cc",
                };
                if(await _studentService.UpdateStudent(st.Id, s))
                {
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}