using System.Data;
using System.Data.SqlClient;
using Dapper;
public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Xprience ; Trusted_Connection=True ;";

    public static List<Plan> ListPlan()
    {

        List<Plan>? ListPlan = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM [Plan]";
            ListPlan = db.Query<Plan>(sql).ToList();
        }
        return ListPlan;
    }
    public static User? SignUp(string name, string mail, string password)
    {
        User? user;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC SignUp @pName, @pMail, @pPassword";
            // string sql = "INSERT INTO Users (username, mail, [password]) VALUES (@pName, @pMail, @pPassword)";
            user = db.QueryFirstOrDefault<User>(sql, new { pName = name, pMail = mail, pPassword = password });
        }
        return user;
    }

    public static User? LogIn(string nameMail, string password)
    {
        User? user = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Users WHERE (username = @pNameMail OR mail = @pNameMail) AND [password] = @pPassword";
            user = db.QueryFirstOrDefault<User>(sql, new { pNameMail = nameMail, pPassword = password });
        }
        return user;
    }

    public static bool UserExist(string name, string mail, string password)
    {
        User? userExist = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Users WHERE username = @pName OR mail = @pMail";
            userExist = db.QueryFirstOrDefault<User>(sql, new { pName = name, pMail = mail, pPassword = password });
        }
        return userExist != null;
    }

    public static string? GetCookie(int id)
    {
        string? cookie = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC [LogIn] @pId";
            cookie = db.QueryFirstOrDefault<string>(sql, new { pId = id });
        }
        return cookie;
    }

    public static User? GetUserByCookie(string? cookie){
        if (cookie == null)
            return null;
        User? user = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Users WHERE cookie = @pCookie";
            user = db.QueryFirstOrDefault<User>(sql, new { pCookie = cookie });
        }
        return user;
    }
}