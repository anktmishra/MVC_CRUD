using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _Db;
        public StudentController(StudentContext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
        {
            try
            {
                var stdList = from a in _Db.Student
                              join b in _Db.Department
                              on a.DeptId equals b.Id
                              into Dep
                              from b in Dep.DefaultIfEmpty()

                              select new Student
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Email = a.Email,
                                  Mobile = a.Mobile,
                                  Description = a.Description,
                                  DeptId = a.DeptId,

                                  Department=b==null?"":b.Department
                              };
                return View(stdList);
            }
            catch (Exception ex)
            {

                return View(ex);
            }
            
        }
        
        [HttpGet]
        public IActionResult CreateStudent(Student obj)
        {
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(obj.Id==0)
                    {
                    _Db.Student.Add(obj);
                    await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();
                    }
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var std =await _Db.Student.FindAsync(id);
                if(std!=null)
                {
                    _Db.Student.Remove(std);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }
        private void loadDDL()
        {
            try
            {
                List<Departments> depList = new List<Departments>();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
