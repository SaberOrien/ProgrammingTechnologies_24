namespace LibraryData.Models
{
    public class Magazine : Item
    {
        public int IssueNumber { get; set; }
        public string Month { get; set; }

        public Magazine(int id, string title, string publisher, bool isAvailable, int issueNumber, string month)
            : base(id, title, publisher, isAvailable)
        {
            IssueNumber = issueNumber;
            Month = month;
        }
    }
}
