using System.Data;
using System.Data.SqlClient;
using Dapper;
public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Xprience ; Trusted_Connection=True ;";

    public static List<Plan> ListPlan()
    {

        List<Plan> ListPlan = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Plan";
            ListPlan = db.Query<Plan>(sql).ToList();
        }
        return ListPlan;
    }
    public static User SingUp(string name, string mail, string password)
    {
        User nuevoUser = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO User (name, mail, password) VALUES (@pName, @pMail, @pPassword)";
            int i = db.Execute(sql, new { pName = name, pMail = mail, pPassword = password });

        
            /*if (rowsAffected > 0)
            {
            
                nuevoUser = new User
                {
                    name = name,
                    mail = mail,
                    password = password
                };
            }*/
        }

        return nuevoUser;
    }

    public static User LogIn(string nameMail, string password)
    {
        User NameMail = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM User WHERE name = @pNameMail or mail = @pNameMail AND password = @pPassword";
            NameMail = db.QueryFirstOrDefault<User>(sql, new { pNameMail = nameMail, pPassword = password });
        }
        return NameMail;
    }

    public static bool UserExist (string name, string mail, string password)
    {
        bool exist = false;
        User userExist = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM [User] WHERE name = @pName AND mail = @pMail AND password = @pPassword"; 
            userExist = db.QueryFirstOrDefault<User>(sql, new { pName = name, pMail = mail, pPassword = password });
        }
        if (userExist != null){
            exist = true;
        }

        return exist;
    }


}