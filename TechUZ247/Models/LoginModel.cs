namespace TechUZ247.Models
{
    public class LoginQueryModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginReponsiveModel
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
    public class SignUpQueryModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class SignUpQueryAppModel
    {
        public bool IsState { get; set; }
    }
    public class SignUpRequestModel
    {
        public string UserName { get; set; }
        public string NumberSecurity { get; set; }
    }
    public class GetAllMember
    {
        public string UserName { get; set; }    
        public string Name { get; set; }
        public string Password { get; set; }
        public string NumberAccount { get; set; }
    }
    public class EditNumberAccount
    {
        public string Before { get; set; }  
        public string After { get; set; }    
    }
    public class PageSize
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
