using System.Data;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MyFirstProject.Models;
using System.Runtime.InteropServices;
using System.Data.Common;

namespace MyFirstProject.Models
{
    public class Userdb
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=FirstProject;Integrated Security=True;Pooling=False");

        public string SaveRecord(user users)
        {
            try
            {
                SqlCommand com = new SqlCommand("User_Add", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Username", users.Username);
                com.Parameters.AddWithValue("@Fname", users.Fname);
                com.Parameters.AddWithValue("@Lname", users.Lname);
                com.Parameters.AddWithValue("@Email", users.Email);
                com.Parameters.AddWithValue("@MobileNo", users.MobileNo);
                com.Parameters.AddWithValue("@State", users.State);
                com.Parameters.AddWithValue("@City", users.City);
                com.Parameters.AddWithValue("@DOB", users.DOB);
                com.Parameters.AddWithValue("@Gender", users.Gender);
                com.Parameters.AddWithValue("@Adress", users.Adress);
                com.Parameters.AddWithValue("@Password", users.Password);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("ok");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
return (ex.Message.ToString());
            }

        }
       
        public Userdb()
        {
            var Configuration = GetConfiguration();
            con = new SqlConnection(Configuration.GetSection("Data").GetSection("ConnectionString").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional: true,reloadOnChange: true);
            return builder.Build();
        }
        public DataSet GetRecords()
        {
            SqlCommand com = new SqlCommand("User_Add", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
    }
}
