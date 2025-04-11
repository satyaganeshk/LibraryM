// IStudentService.cs
using LibraryM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryM.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IActionResult> SignUp(StudentModel studentModel);
        Task<IActionResult> Login(string emailId, string password);
        Task<IActionResult> GetStudent(string Uid);
        Task<IActionResult> GetAllStudents();
        Task<IActionResult> UpdateStudent(StudentModelUpdate studentModel);
        Task<IActionResult> DeleteStudent(string Uid);
    }
}