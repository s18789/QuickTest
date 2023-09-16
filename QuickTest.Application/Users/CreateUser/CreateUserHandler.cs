using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Application.Users.UserRole;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, ResponseDto>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUserRoleRepository roleRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;


        public CreateUserHandler(IMapper mapper,UserManager<User> userManager, IEmailService emailService, IUserRoleRepository roleRepository, 
                ITeacherRepository teacherRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.roleRepository = roleRepository;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this._mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserDto.Email) || string.IsNullOrEmpty(request.UserDto.FirstName) || string.IsNullOrEmpty(request.UserDto.LastName))
            {
                return new ResponseDto { IsSuccess = false, ErrorMessage = "Invalid input." };
            }

            var user = MapToCreateUserDto(request.UserDto);
            var generatedPassword = GenerateRandomPassword();
            var isCreateSuccessful = false;
            TeacherDto addedTeacher = new TeacherDto();
            StudentDto addedStudent = new StudentDto();

            await unitOfWork.BeginTransactionAsync();

            try
            {
                if (user.UserRole.Name == "teacher")
                {
                    var result = await CreateTeacher(user, generatedPassword);
                    isCreateSuccessful = result.Item1;
                    addedTeacher =_mapper.Map<TeacherDto>(result.Item2);
                }
                else if (user.UserRole.Name == "student")
                {
                    var result = await CreateStudent(user, generatedPassword, request.UserDto.Group.Id);
                    isCreateSuccessful = result.Item1;
                    addedStudent = _mapper.Map<StudentDto>(result.Item2);
                }
                else
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return new ResponseDto { IsSuccess = false, ErrorMessage = "Such role was not found, creating user failed." };
                }

                if (isCreateSuccessful)
                {
                    await unitOfWork.CommitTransactionAsync();
                }
                else
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return new ResponseDto { IsSuccess = false, ErrorMessage = "Error creating user" };
                }
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                return new ResponseDto { IsSuccess = false, ErrorMessage = $"Creating user failed. There was an exception: {ex.Message}" };
            }

            var emailResult = await emailService.SendEmailAsync(user.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

            return new ResponseDto { IsEmailSent = emailResult, IsSuccess = true, AddedStudent =addedStudent, AddedTeacher = addedTeacher  };
        }

        private string GenerateRandomPassword()
        {
            Random random = new Random();

            char randomUppercaseLetter = (char)random.Next(65, 91);

            char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '{', '}', '[', ']', '|', '\\', ':', ';', '<', '>', '?', '/', '.' };
            char randomSpecialCharacter = specialCharacters[random.Next(0, specialCharacters.Length)];

            string[] words = { "apple", "banana", "cherry", "keyboard", "elderberry", "cactus", "grape", "honeydew" };
            string randomWord = words[random.Next(0, words.Length)];

            int randomNumber = random.Next(0, 10);

            string password = $"{randomUppercaseLetter}{randomSpecialCharacter}{randomWord}{randomNumber}";

            return password;
        }
        private async Task<Tuple<bool, Teacher>> CreateTeacher(CreateUserDto user, string password)
        {
            var teacherRole = await roleRepository.GetRoleByName("teacher");
            var teacherToUpdate = new Teacher();
            if (await teacherRepository.CheckIfTeacherExists(user.Email))
            {
                teacherToUpdate= await teacherRepository.GetTeacherByEmail(user.Email);
                var result = await userManager.AddPasswordAsync(teacherToUpdate, password);
                if (result.Errors.FirstOrDefault().Description.Contains("already has a password"))
                {
                    return new Tuple<bool, Teacher>(true, teacherToUpdate);
                }
                else
                {
                    return new Tuple<bool, Teacher>(result.Succeeded, teacherToUpdate);
                }
            }
            else
            {
                var newTeacher = MapToTeacher(user, teacherRole);

                //newTeacher.UserRoleId = 2;
                //teacherRepository.ReloadTheEntity(newTeacher);
                await teacherRepository.AddAsync(newTeacher);

                var result = await userManager.AddPasswordAsync(newTeacher, password);
                return new Tuple<bool, Teacher>(result.Succeeded, newTeacher);

            }
            
        }
        private async Task<Tuple<bool, Student>> CreateStudent(CreateUserDto user, string password, int groupId)
        {
            var studentRole = await roleRepository.GetRoleByName("student");
            var newStudent = MapToStudent(user, studentRole, groupId);
            var studentToUpdate = new Student();

            if (await studentRepository.CheckIfStudentExists(user.Email))
            {
                studentToUpdate = await studentRepository.GetStudentByEmail(user.Email);
                var result = await userManager.AddPasswordAsync(studentToUpdate, password);
                if (result.Errors.FirstOrDefault().Description.Contains("already has a password"))
                {
                    return new Tuple<bool, Student>(true, studentToUpdate);
                }
                else
                {
                    return new Tuple<bool, Student>(result.Succeeded, studentToUpdate);
                }
                
            }
            else
            {
                await studentRepository.AddAsync(newStudent);

                var result = await userManager.AddPasswordAsync(newStudent, password);
                return new Tuple<bool, Student>(result.Succeeded, newStudent);

            }
            
           
        }
        private Teacher MapToTeacher(CreateUserDto user, QuickTest.Core.Entities.UserRole teacherRole)
        {
            return new Teacher
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                NormalizedEmail = user.Email.ToUpper(),
                //UserRole = teacherRole,
                UserRoleId = user.UserRole.Id,
            };
        }

        private Student MapToStudent(CreateUserDto user, QuickTest.Core.Entities.UserRole studentRole, int groupId)
        {
            return new Student
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                UserRoleId = user.UserRole.Id,
                GroupId = groupId
            };
        }
        private CreateUserDto MapToCreateUserDto(CreateUserDto sourceUser)
        {
            return new CreateUserDto
            {
                UserName = sourceUser.Email.Split('@')[0],
                Email = sourceUser.Email,
                FirstName = sourceUser.FirstName,
                LastName = sourceUser.LastName,
                UserRole = new UserRoleDto
                {
                    Id = sourceUser.UserRole.Id,
                    Name = sourceUser.UserRole.Name
                }
            };
        }
    }
}
