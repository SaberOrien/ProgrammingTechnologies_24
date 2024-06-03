using Logic.DTOs_Abstract;

namespace Logic.DTOs_Implemented
{
    internal class UserDTO : IUserDTO
    {
        public UserDTO(int id, string name, string surname, string email, string userType)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.UserType = userType;
        } 

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
