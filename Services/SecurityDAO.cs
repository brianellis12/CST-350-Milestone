using Milestone.Models;
using System.Data.SqlClient;

namespace Milestone.Services
{
    public class SecurityDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Milestone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return success;
        }

        public bool AddUser(UserModel user)
        {
            string sqlStatement = "INSERT INTO dbo.Users (FirstName, LastName, Sex, Age, Email, Username, Password, State) VALUES (@FirstName, @LastName, @Sex, @Age, @Email, @Username, @Password, @State)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar, 50).Value = user.FirstName;

                command.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar, 50).Value = user.LastName;

                command.Parameters.Add("@Sex", System.Data.SqlDbType.VarChar, 7).Value = user.Sex;

                command.Parameters.Add("@Age", System.Data.SqlDbType.SmallInt).Value = user.Age;

                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 50).Value = user.Email;

                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;

                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                command.Parameters.Add("@State", System.Data.SqlDbType.VarChar, 40).Value = user.State;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }
    }

}
