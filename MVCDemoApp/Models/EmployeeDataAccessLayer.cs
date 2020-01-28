using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemoApp.Models
{
    public class EmployeeDataAccessLayer
    {
        readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abc\OneDrive\Documents\CustomerDB.mdf;Integrated Security=True;Connect Timeout=30";


        //To View all employees details  
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
               SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.id = Convert.ToInt32(rdr["id"]);
                    employee.name = rdr["Name"].ToString();
                    employee.email = rdr["Email"].ToString();
                    employee.password = rdr["Password"].ToString();
                    employee.mobile = rdr["Mobile"].ToString();
                    employee.dob = rdr["Dob"].ToString();

                    employee.gender = rdr["Gender"].ToString();
                    employee.department = rdr["Department"].ToString();
                    employee.city = rdr["City"].ToString();

                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        
        //To Add new employee record  
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", employee.name);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                cmd.Parameters.AddWithValue("@Password", employee.password);
                cmd.Parameters.AddWithValue("@Mobile", employee.mobile);
                cmd.Parameters.AddWithValue("@Dob", DateTime.Parse(employee.dob));
                cmd.Parameters.AddWithValue("@Gender", employee.gender);
                cmd.Parameters.AddWithValue("@Department", employee.department);
                cmd.Parameters.AddWithValue("@City", employee.city);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        

        //To Update the records of a particluar employee
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", employee.id);
                cmd.Parameters.AddWithValue("@Name", employee.name);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                cmd.Parameters.AddWithValue("@Password", employee.password);
                cmd.Parameters.AddWithValue("@Mobile", employee.mobile);
                cmd.Parameters.AddWithValue("@Dob", DateTime.Parse(employee.dob));

                cmd.Parameters.AddWithValue("@Gender", employee.gender);
                cmd.Parameters.AddWithValue("@Department", employee.department);
                cmd.Parameters.AddWithValue("@City", employee.city);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee
        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblEmployee WHERE id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.id = Convert.ToInt32(rdr["id"]);
                    employee.name = rdr["Name"].ToString();
                    employee.email = rdr["Email"].ToString();
                    employee.password = rdr["Password"].ToString();
                    employee.mobile = rdr["Mobile"].ToString();
                    employee.dob = rdr["Dob"].ToString();
                    employee.gender = rdr["Gender"].ToString();
                    employee.department = rdr["Department"].ToString();
                    employee.city = rdr["City"].ToString();
                }
            }
            return employee;
        }

        //To Delete the record on a particular employee
        public void DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

       

    }
}
