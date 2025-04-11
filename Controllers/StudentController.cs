// StudentController.cs
using LibraryM.DTO;
using LibraryM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(StudentModel model)
        {
            return await _studentService.SignUp(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string emailId, string password)
        {
            return await _studentService.Login(emailId, password);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent(string uid)
        {
            return await _studentService.GetStudent(uid);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            return await _studentService.GetAllStudents();
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudentModelUpdate model)
        {
            return await _studentService.UpdateStudent(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string uid)
        {
            return await _studentService.DeleteStudent(uid);
        }
    }
}
