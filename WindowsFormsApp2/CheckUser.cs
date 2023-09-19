namespace WindowsFormsApp2
{
    public class CheckUser
    {
        public string Login { get; set; }
        public bool isAdmin { get; }
        public string Status => isAdmin ? "Admin" : "User";

        public CheckUser(string login, bool isadmin)
        { 
          Login = login.Trim();
          isAdmin = isadmin;
        }


    }
}
