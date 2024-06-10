namespace MVVM.Model
{
    public class UserModel
    {
        public UserModel(int id, string name, string surname, string email, string userType)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            UserType = userType;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
    }
}
