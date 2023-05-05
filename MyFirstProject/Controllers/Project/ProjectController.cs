using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyFirstProject.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models.ViewModel;
using Azure.Identity;

namespace MyFirstProject.Controllers.Project
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext appdbcontext;

        public ProjectController(AppDbContext appdbcontext)
        {
            this.appdbcontext = appdbcontext;
        }
        public async Task<IActionResult> Index(string Search="")
        {
            var Users = await appdbcontext.users.ToListAsync();
            if (Search != null)
            {
                Users = appdbcontext.users.Where(x => x.Username.Contains(Search)).ToList();
            }
            else
            Users=appdbcontext.users.ToList();
            return View(Users);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (string Username,string Password)
        {
            
               var users = await appdbcontext.users.FirstOrDefaultAsync(x => x.Username==Username && x.Password==Password);
            
            return View(users);
        }

        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(SignUpLoginViewModel SignupUsers)
        {

            var user = new user()
            {
                Username = SignupUsers.Username,
                Fname = SignupUsers.Fname,
                Lname = SignupUsers.Lname,
                Email = SignupUsers.Email,
                MobileNo = SignupUsers.MobileNo,
                State = SignupUsers.State,
                City = SignupUsers.City,
                DOB = SignupUsers.DOB,
                Gender = SignupUsers.Gender,
                Adress = SignupUsers.Adress,
                Password = SignupUsers.Password,
            };
           await appdbcontext.users.AddAsync(user);
            await appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task <IActionResult> Edit(string Username)
        { 
            var User = await appdbcontext.users.FirstOrDefaultAsync(x => x.Username==Username);

            if (User != null)
            {
                var viewmodel = new SignUpLoginViewModel()
                {
                    Username = User.Username,
                    Fname = User.Fname,
                    Lname = User.Lname,
                    Email = User.Email,
                    MobileNo = User.MobileNo,
                    State = User.State,
                    City = User.City,
                    DOB = User.DOB,
                    Gender = User.Gender,
                    Adress = User.Adress,
                    Password = User.Password
                };
                return View(viewmodel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Edit(SignUpLoginViewModel model)
        {
            var User = await appdbcontext.users.FindAsync(model.Username);
            if (User != null)
            {
                User.Fname = model.Fname;
                User.Lname = model.Lname;
                User.Email= model.Email;
                User.MobileNo= model.MobileNo;
                User.State = model.State;
                User.City = model.City;
                User.DOB = model.DOB;
                User.Gender = model.Gender;
                User.Adress = model.Adress;
                User.Password = model.Password;

                await appdbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Delete(SignUpLoginViewModel model)
        {
           
            var User = await appdbcontext.users.FindAsync(model.Username);
            if (User != null)
            {
                appdbcontext.users.Remove(User);
                await appdbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
    }
}




