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
            string sql = "SELECT TOP 10 * FROM [Plan]";
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

    public static User? GetUserByCookie(string? cookie)
    {
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

    public static bool PlanCreated(string ids, int day, int month, int year, int userId)
    {
        int lines;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC GeneratePlan @pIds, @pDay, @pMonth, @pYear, @pUserId";
            lines = db.Execute(sql, new { pIds = ids, pDay = day, pMonth = month, pYear = year, pUserId = userId });
        }
        return lines == 2;
    }

    public static Plan? GetLastPlan(int userId)
    {
        Plan? p;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @pUserId ORDER BY [Plan].id DESC";
            p = db.QueryFirstOrDefault<Plan>(sql, new { pUserId = userId });
        }
        return p;
    }

    public static List<Plan> GetLatestPlan(int userId)
    {
        List<Plan> p;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT TOP 3 [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @pUserId ORDER BY [Plan].id DESC";
            p = db.Query<Plan>(sql, new { pUserId = userId }).ToList();
        }
        return p;
    }

    public static List<Plan> GetPlans(int userId)
    {
        List<Plan>? ListPlan = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @pUserId AND PlanUser.Created = 1";
            ListPlan = db.Query<Plan>(sql, new { pUserId = userId }).ToList();
        }
        return ListPlan;

    }

    public static List<Plan> GetFolder(int folderId, int userId)
    {
        List<Plan>? ListPlan = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC GetFolder @pIdFolder, @pUserId";
            ListPlan = db.Query<Plan>(sql, new { pIdFolder = folderId, pUserId = userId }).ToList();
        }
        return ListPlan;
    }

    public static List<Folder> GetFolders(int userId)
    {
        List<Folder> listFolders = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Folder.* FROM Folder INNER JOIN FolderPlan ON Folder.id = FolderPlan.idFolder INNER JOIN PlanUser ON PlanUser.id = FolderPlan.idPlan WHERE idUser = @pId GROUP BY Folder.id, Folder.name";
            listFolders = db.Query<Folder>(sql, new { pId = userId }).ToList();
        }
        return listFolders;
    }

    public static Dictionary<Folder, List<Plan>> GetPlansByFolder(List<Folder> f)
    {
        Dictionary<Folder, List<Plan>> r = new Dictionary<Folder, List<Plan>>();
        foreach (Folder folder in f)
        {
            List<Plan> listFolders = null;
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT [Plan].* FROM Folder INNER JOIN FolderPlan ON Folder.id = FolderPlan.idFolder INNER JOIN PlanUser ON PlanUser.id = FolderPlan.idPlan INNER JOIN [Plan] ON [Plan].id = PlanUser.idPlan WHERE Folder.id = @pId";
                listFolders = db.Query<Plan>(sql, new { pId = folder.id }).ToList();
            }
            r.Add(folder, listFolders);
        }
        return r;
    }

    public static Plan GetPlan(int idPlan)
    {
        Plan p;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM [Plan] WHERE id = @pId";
            p = db.QueryFirstOrDefault<Plan>(sql, new { pId = idPlan });
        }
        return p;
    }

    public static void ChangeUsername(string userChanged, string? cookie)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "UPDATE Users SET username = @puserChanged WHERE cookie = @pCookie";
            db.Execute(sql, new { puserChanged = userChanged, pCookie = cookie });
        }
    }

    public static void ChangeMail(string mailChanged, string? cookie)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "UPDATE Users SET mail = @pmailChanged WHERE cookie = @pCookie";
            db.Execute(sql, new { pmailChanged = mailChanged, pCookie = cookie });
        }
    }

    public static void LikePlace(string fsqId, int idUser)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC LikePlace @pIdUser, @pFsqId";
            db.Execute(sql, new { pFsqId = fsqId, pIdUser = idUser });
        }
    }

    public static bool IsChecked(string fsqId, int idUser)
    {
        bool r;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Punctuation WHERE idUsers = @pIdUser AND fsq_id = @pFsqId AND isFav = 1";
            r = db.QueryFirstOrDefault<bool>(sql, new { pFsqId = fsqId, pIdUser = idUser });
        }
        return r;
    }

    public static void ChangePlanName(int id, string name)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "UPDATE [Plan] SET [name] = @pName WHERE id = @pId";
            db.Execute(sql, new { pName = name, pId = id });
        }
    }

    public static bool CreateFolder(int userId, string name, int planId)
    {
        bool r;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC AddFolder @pUserId, @pPlanId, @pName";
            r = db.Execute(sql, new { pName = name, pUserId = userId, pPlanId = planId }) == 2;
        }
        return r;
    }

    public static bool AddToFolder(int folderId, int userId, int planId)
    {
        bool r;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "EXEC AddToFolder @pIdFolder, @pIdUser, @pIdPlan";
            r = db.Execute(sql, new { pIdFolder = folderId, pIdUser = userId, pIdPlan = planId }) == 1;
        }
        return r;
    }

    public static Folder GetFolderName(int folderId)
    {
        if (folderId == -1)
        {
            Folder f = new Folder();
            f.id = -1;
            f.name = "Created";
            return f;
        }
        Folder r;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Folder WHERE id = @pId";
            r = db.QueryFirstOrDefault<Folder>(sql, new { pId = folderId });
        }
        return r;
    }
}