using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDemoApp.Models;


namespace MVCDemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();
        readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abc\OneDrive\Documents\CustomerDB.mdf;Integrated Security=True;Connect Timeout=30";
        List<Employee> lstEmployee = new List<Employee>();

        public IActionResult Index()
        {

            lstEmployee = objemployee.GetAllEmployees().ToList();
            return View(lstEmployee);
        }
        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                objemployee.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Employee employee)
        {
            if (id != employee.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objemployee.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult Delete([Bind] EmployeeDataAccessLayer objemployee,int? id)
        {
            objemployee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
        public JsonResult GetOrganizations(int length, int start)
        {

            var count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                StringBuilder sbSQL = new StringBuilder();

                SqlCommand acn = new SqlCommand("select count(*) from tblEmployee",conn);
                count = Convert.ToInt32(acn.ExecuteScalar());

                sbSQL.AppendFormat("select top({0}) * from (select org.*, row_number() over(order by id ASC) as [row_number] from tblEmployee org) org", length);
                sbSQL.AppendFormat(" where row_number >{0}", start);

                string searchVal = HttpContext.Request.Form["search[value]"];

                if (!string.IsNullOrEmpty(searchVal))
                {
                    sbSQL.AppendFormat(" and Name like '%{0}%' or id like '%{0}%'", searchVal);
                    }

                cmd.CommandText = sbSQL.ToString();
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lstEmployee.Add(new Employee()
                    {
                        id=Convert.ToInt32(reader["id"]),
                        name = reader["Name"].ToString(),
                        email = reader["Email"].ToString(),
                        password = reader["Password"].ToString(),
                        mobile = reader["Mobile"].ToString(),
                        dob = reader["Dob"].ToString().Replace("12:00:00 AM",""),
                        gender = reader["Gender"].ToString(),
                        department = reader["Department"].ToString(),
                        city = reader["City"].ToString()
                    });
                }
                    reader.Close();
                conn.Close();
            }

        var response = new { data = lstEmployee, recordsFiltered = count, recordsTotal = count };
            return Json(response);
    }

        public JsonResult VerifyEmail(string email, int id )
        {
            bool result;
            SqlConnection con = new SqlConnection(connectionString);
            
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblEmployee WHERE Email = '" + email + "' and id !='"+id+"'",con);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
                con.Close();
                return Json(result);

            
        }




        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]

        public IActionResult Login([Bind]LoginViewModel reg)
        {
            string result = "0";
            string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abc\OneDrive\Documents\CustomerDB.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection sql = new SqlConnection(conn);
            sql.Open();
            SqlCommand sqlcomm = new SqlCommand("Validate_User", sql);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddWithValue("@Email", reg.Email);
            sqlcomm.Parameters.AddWithValue("@Password", reg.Password);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    HttpContext.Session.SetInt32("id", Convert.ToInt32(sdr["id"]));
                    HttpContext.Session.SetString("Name", sdr["Name"].ToString());
                    HttpContext.Session.SetString("Email", sdr["Email"].ToString());
                }

                result = "Index";

            }
            else
            {
                result = "Login";
            }
            return View(result);

        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }



    }
}
