namespace Data.AbstractInterfaces
{
    public interface IUser
    {
        int Id { get; set; } 
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        string UserType { get; set; }
    }
}
