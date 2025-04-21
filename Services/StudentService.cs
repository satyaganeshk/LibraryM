using LibraryM.DTO;
using LibraryM.Entity;
using LibraryM.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace LibraryM.Services
{
    public class StudentService : IStudentService
    {
        private readonly Container _container;

        public StudentService(CosmosClient client)
        {
            _container = client.GetDatabase("library").GetContainer("library");
        }

        public async Task<IActionResult> SignUp(StudentModel studentModel)
        {
            string newId = Guid.NewGuid().ToString();
            var student = new Student
            {
                StudentName = studentModel.StudentName,
                PrnNumber = studentModel.PrnNumber,
                ContactNumber = studentModel.ContactNumber,
                StudentEmail = studentModel.StudentEmail.ToLower(),
                BranchName = studentModel.BranchName,
                GraduationYear = studentModel.GraduationYear,
                StudentAddress = studentModel.StudentAddress,
                StudentPassword = studentModel.StudentPassword,
                Id = newId,
                UId = newId,
                DocumentType = "student",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Version = 1,
                Active = true,
                Archieved = false
            };
            await _container.CreateItemAsync(student, new PartitionKey("student"));
            return new OkObjectResult(student);
        }

        public async Task<IActionResult> Login(string emailId, string password)
        {
            emailId = emailId.ToLower();

            Student student = _container.GetItemLinqQueryable<Student>(true).Where(s => s.DocumentType == "student" && s.StudentEmail == emailId && s.StudentPassword == password).AsEnumerable().FirstOrDefault();
            if (student != null)
            {
                return new OkObjectResult(student);
            }
            else
            {
                return new BadRequestObjectResult("Invalid Credentails");
            }
        }

        public async Task<IActionResult> GetStudent(string Uid)
        {
            var student = _container.GetItemLinqQueryable<Student>(true).Where(s => s.DocumentType == "student" && s.UId == Uid).AsEnumerable().FirstOrDefault();
            if (student != null)
            {
                return new OkObjectResult(student);
            }
            else
            {
                return new NotFoundObjectResult("Student not found");
            }
        }

        public async Task<IActionResult> GetAllStudents()
        {
            var students = _container.GetItemLinqQueryable<Student>(true).Where(s => s.DocumentType == "student").AsEnumerable().ToList();
            if (students != null)
            {
                return new OkObjectResult(students);
            }
            else
            {
                return new NotFoundObjectResult("No Students Found");
            }
        }

        public async Task<IActionResult> UpdateStudent(StudentModelUpdate studentModel)
        {
            var student = _container.GetItemLinqQueryable<Student>(true).Where(s => s.DocumentType == "student" && s.UId == studentModel.UId).AsEnumerable().FirstOrDefault();
            if (student != null)
            {
                student.StudentName = studentModel.StudentName;
                student.PrnNumber = studentModel.PrnNumber;
                student.ContactNumber = studentModel.ContactNumber;
                student.StudentEmail = studentModel.StudentEmail.ToLower();
                student.BranchName = studentModel.BranchName;
                student.GraduationYear = studentModel.GraduationYear;
                student.StudentAddress = studentModel.StudentAddress;
                student.StudentPassword = studentModel.StudentPassword;
                student.Version++;
                await _container.ReplaceItemAsync(student, student.UId, new PartitionKey("student"));
                return new OkObjectResult(student);
            }
            else
            {
                return new NotFoundObjectResult("Student not found");
            }
        }
        public async Task<IActionResult> DeleteStudent(string Uid)
        {
            var student = _container.GetItemLinqQueryable<Student>(true).Where(s => s.DocumentType == "student" && s.UId == Uid).AsEnumerable().FirstOrDefault();
            if (student != null)
            {
                await _container.DeleteItemAsync<Student>(student.UId, new PartitionKey("student"));
                return new OkObjectResult("Student Deleted Successfully");
            }
            else
            {
                return new NotFoundObjectResult("Student not found");
            }
        }



    }
}
