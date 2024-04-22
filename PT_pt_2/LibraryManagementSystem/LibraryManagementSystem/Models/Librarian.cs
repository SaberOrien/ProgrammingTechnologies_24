namespace LibraryData.Models
{
    public class Librarian : User
    {
        public Librarian(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
        }
    }
}
