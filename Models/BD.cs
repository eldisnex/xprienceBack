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
    public static User SingUp (string name, string mail, string password)
    {
        User nuevoUser = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO User (name, mail, password) VALUES (@pName, @pMail, @pPassword)"; 
            nuevoUser = db.QueryFirstOrDefault(sql, new { pName = name, pMail = mail, pPassword = password});
        }

        return nuevoUser;
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