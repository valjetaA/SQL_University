using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{
    interface IStudentRepository
    {
        void Add(Student student);
        Student GetById(int id);
        List<Student> GetAll();

    }
}
