using University.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{
    interface IStudentInGroupRepository
    {
        void Add(StudentInGroup studentInGroup);
        List<Student> GetAllByGroupId(int groupId);
        List<StudentInGroup> GetByStudentAndGroupIds();
    }
}
