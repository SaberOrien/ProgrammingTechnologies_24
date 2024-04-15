namespace LibraryManagementSystem.Data

{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfSignUp { get; set; }
        public List<BorrowRecord> BooksBorrowed { get; set; } = new List<BorrowRecord>();
    }
}
