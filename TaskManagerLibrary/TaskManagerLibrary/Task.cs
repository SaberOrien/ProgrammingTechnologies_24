namespace TaskManagerLibrary
{
    public class Task
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }

        public Task(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or whitespace.", nameof(description));
            Description = description;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
}