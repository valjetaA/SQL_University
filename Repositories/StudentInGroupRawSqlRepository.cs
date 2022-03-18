using University.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{

    public class StudentInGroupRawSqlRepository : IStudentInGroupRepository
    {
        private readonly string _connectionString;

        public StudentInGroupRawSqlRepository(string connectionString)

        {
            _connectionString = connectionString;
        }

        public void Add(StudentInGroup studentInGroup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [StudentInGroup]
                        values
                            (@studentId, @groupsId)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@studentId", SqlDbType.NVarChar).Value = studentInGroup.StudentId;
                    command.Parameters.Add("@groupsId", SqlDbType.Int).Value = studentInGroup.GroupId;

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Student> GetAllByGroupId(int groupsId)
        {
            var result = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [StudentId], [GroupsId] from [StudentInGroup] where [GroupsId] = @groupsId";

                    command.Parameters.Add("@groupsId", SqlDbType.Int).Value = groupsId;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["StudentId"])
                            });
                        }
                    }
                }
            }
            return result;
        }
        public List<StudentInGroup> GetByStudentAndGroupIds()
        {
            var result = new List<StudentInGroup>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [StudentId], [GroupsId] from [StudentInGroup]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentInGroup
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                GroupId = Convert.ToInt32(reader["GroupsId"])
                            });
                        }

                    }
                }
            }
            return result;
        }
    }
}
