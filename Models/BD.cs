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
    /// <summary>
    /// Retorna la cantidad de filas afectadas.
    /// </summary>
    public static int SignUp(string name, string mail, string password)
    {
        int i;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO User (name, mail, password) VALUES (@pName, @pMail, @pPassword)";
            i = db.Execute(sql, new { pName = name, pMail = mail, pPassword = password });
        }
        return i;
    }

    public static User LogIn(string nameMail, string password)
    {
        User user = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM User WHERE (name = @pNameMail OR mail = @pNameMail) AND password = @pPassword";
            user = db.QueryFirstOrDefault<User>(sql, new { pNameMail = nameMail, pPassword = password });
        }
        return user;
    }

    public static bool UserExist(string name, string mail, string password)
    {
        User userExist = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM [User] WHERE name = @pName OR mail = @pMail";
            userExist = db.QueryFirstOrDefault<User>(sql, new { pName = name, pMail = mail, pPassword = password });
        }
        return userExist != null;
    }


}