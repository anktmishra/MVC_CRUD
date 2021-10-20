using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CRUD.Models
{
    public class Departments
    {
        [Key]
        public int id { get; set; }
        public string Department { get; set; }
    }
}
