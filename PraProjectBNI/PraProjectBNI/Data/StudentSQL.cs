using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PraProjectBNI.Models;
using SampleAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class StudentDataSQL : IStudent
    {

        private IConfiguration _config;
        public StudentDataSQL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"delete from Student where ID_Student=@ID_Student";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID_Student", id);
                try
                {
                    var student = GetById(id);
                    if (student != null)
                    {
                        conn.Open();
                        var result = cmd.ExecuteNonQuery();
                        if (result != 1)
                            throw new Exception("Gagal delete data student");
                    }
                    else
                    {
                        throw new Exception($"Data student id: {id} tidak ditemukan");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.InnerException.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Student> GetAll()
        {
            List<Student> lstStudents = new List<Student>();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Student 
                                  order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstStudents.Add(new Student
                        {
                            ID_Student = Convert.ToInt32(dr["ID"]),
                            Nama = dr["Nama"].ToString(),
                            Domisili = dr["Domisili"].ToString(),
                            Angkatan = Convert.ToInt32(dr["Angkatan"]),
                            Jenis_Kelamin = dr["Jenis Kelamin"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstStudents;
        }

        public Student GetById(string id)
        {
            Student student = new Student();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Student
                                  where ID_Student=@ID_Student";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    student = new Student
                    {
                        ID_Student = Convert.ToInt32(dr["ID"]),
                        Nama = dr["Nama"].ToString(),
                        Domisili = dr["Domisili"].ToString(),
                        Angkatan = Convert.ToInt32(dr["Angkatan"]),
                        Jenis_Kelamin = dr["Jenis Kelamin"].ToString()
                    };
                }
                else
                {
                    throw new Exception($"Data Student ID: {id} tidak ditemukan");
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return student;
        }

        public IEnumerable<Student> GetByName(string studentName)
        {
            List<Student> lstStudents = new List<Student>();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Student where Nama like @Nama
                                  order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", "%" + studentName + "%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstStudents.Add(new Student
                        {
                            ID_Student = Convert.ToInt32(dr["ID"]),
                            Nama = dr["Nama"].ToString(),
                            Domisili = dr["Domisili"].ToString(),
                            Angkatan = Convert.ToInt32(dr["Angkatan"]),
                            Jenis_Kelamin = dr["Jenis Kelamin"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstStudents;
            }
        }

        public void Insert(Student obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"insert into Student(Nama, Domisili, Angkatan, Jenis_Kelamin) 
                                  values(@Nama,@Domisili,@Jenis_Kelamin)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", obj.Nama);
                cmd.Parameters.AddWithValue("@Domisili", obj.Domisili);
                cmd.Parameters.AddWithValue("@Angkatan", obj.Angkatan);
                cmd.Parameters.AddWithValue("@Jenis_Kelamin", obj.Jenis_Kelamin);
                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Gagal untuk menambahkan data student");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.InnerException.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(string id, Student obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"update Students set Name=@Name,Domisili=@Domisili,
                                  Angkatan=@Angkatan, Jenis_Kelamin=@Jenis_Kelamin, where ID_Student=@ID_Student";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", obj.Nama);
                cmd.Parameters.AddWithValue("@Domisili", obj.Domisili);
                cmd.Parameters.AddWithValue("@Angkatan", obj.Angkatan);
                cmd.Parameters.AddWithValue("@Jenis_Kelamin", obj.Jenis_Kelamin);
                try
                {
                    var student = GetById(id);
                    if (student != null)
                    {
                        conn.Open();
                        var result = cmd.ExecuteNonQuery();
                        if (result != 1)
                            throw new Exception("Gagal update data");
                    }
                    else
                    {
                        throw new Exception($"Data Student ID {id} tidak ditemukan");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.InnerException.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public int InsertWithIndentity(Student obj)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"insert into StudentsStudents set Name=@Name,Domisili=@Domisili,
                                  Angkatan=@Angkatan, Jenis_Kelamin=@Jenis_Kelamin, where ID_Student=@ID_Student
                                  select @@identity";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", obj.Nama);
                cmd.Parameters.AddWithValue("@Domisili", obj.Domisili);
                cmd.Parameters.AddWithValue("@Angkatan", obj.Angkatan);
                cmd.Parameters.AddWithValue("@Jenis_Kelamin", obj.Jenis_Kelamin);
                try
                {
                    conn.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.InnerException.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        IEnumerable<Student> IStudent.GetByName(string Student)
        {
            throw new NotImplementedException();
        }

        Student IStudent.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IStudent.Insert(Student student)
        {
            throw new NotImplementedException();
        }

        void IStudent.Update(int id, Student student)
        {
            throw new NotImplementedException();
        }

        void IStudent.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Student> IStudent.GetByName()
        {
            throw new NotImplementedException();
        }

        object IStudent.InsertWithIndentity(Student student)
        {
            throw new NotImplementedException();
        }

        void IStudent.Update(string v, Student student) => throw new NotImplementedException();

        void ICrud<Student>.Insert(string id)
        {
            throw new NotImplementedException();
        }
    }
}