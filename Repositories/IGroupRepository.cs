using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{
    interface IGroupRepository
    {
        void Add(Group group);
        Group GetById(int id);
        List<Group> GetAll();

    }
}
