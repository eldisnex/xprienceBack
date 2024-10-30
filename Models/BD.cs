using System.Data;
using System.Data.SqlClient;
using Dapper;
public class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Xprience ; Trusted_Connection=True ;";

    public static void SignUp (User user)
    {

        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO User (idUser, name, mail, password) VALUES (@pIdUser, @pName, @pMail, @pPassword)";
            db.Execute(sql, new { pIdUser = user.idUser, pName = user.name, pMail = user.mail, pPassword = user.password});
        }
    }

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
    public static Void SingUp (string name, string mail, string password)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO User (name, mail, password) VALUES (@pName, @pMail, @pPassword)"; 
            db.Execute(sql, new { pName = name, pMail = mail, pPassword = password});
        }
    }

    public static User LogIn (string nameMail, string password)
    {
        User NameMail = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM User WHERE name = @pNameMail or mail = @pNameMail AND password = @pPassword"; 
            NameMail = db.QueryFirstOrDefault<User>(sql, new { pNameMail = nameMail, pPassword = password });
        }
        return NameMail;
    }

}