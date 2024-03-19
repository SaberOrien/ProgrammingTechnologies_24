namespace TaskManagerLibrary
{
    public class TaskManager
    {
        private readonly List<Task> tasks = [];

        public Guid AddTask(string description)
        {
            var task = new Task(description);
            tasks.Add(task);
            return task.Id;
        }

        public bool RemoveTask(Guid taskId)
        {
            var task = tasks.Find(t => t.Id == taskId);
            return task != null && tasks.Remove(task);
        }

        public bool MarkTaskAsCompleted(Guid taskId)
        {
            var task = tasks.Find(t => t.Id == taskId);
            if (task != null)
            {
                task.MarkAsCompleted();
                return true;
            }
            return false;
        }

        public IEnumerable<Task> GetTasks(bool? isCompleted = null)
        {
            if(isCompleted == null)
            {
                return tasks;
            }

            return tasks.Where(t => t.IsCompleted == isCompleted);
        }

        public Task? GetTaskById(Guid taskId)
        {
            return tasks.FirstOrDefault(t => t.Id == taskId);
        }
    }
}
