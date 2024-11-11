public class User
{
    public int id { get; set; }
    public string? username { get; set; }
    public string? mail { get; set; }
    public string? password { get; private set; }
    public string? cookie { get; set; }
}