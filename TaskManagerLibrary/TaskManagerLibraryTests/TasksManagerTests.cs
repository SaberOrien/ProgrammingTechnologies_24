using TaskManagerLibrary;

namespace TaskManagerLibraryTests
{
    [TestClass]
    public class TasksManagerTests
    {
        [TestMethod]
        public void AddTask_AddsTaskToList()
        {
            var manager = new TaskManager();
            string description = "Test Task";

            manager.AddTask(description);
            Assert.AreEqual(1, manager.GetTasks().Count(), "The number of added task doesn't match the expected number");
            Assert.AreEqual(description, manager.GetTasks().First().Description, "The returned description doesn't match the expected task description");
        }

        [TestMethod]
        public void AddTask_WithValidDescription_ReturnsTaskIdAndAddsTask()
        {
            var manager = new TaskManager();
            string description = "Valid Task Description";

            Guid taskId = manager.AddTask(description);
            TaskManagerLibrary.Task addedTask = manager.GetTaskById(taskId)!;

            Assert.IsNotNull(addedTask, "Task should be successfully added.");
            Assert.AreEqual(description, addedTask.Description, "Task description should match the input.");
        }

        [TestMethod]
        public void RemoveTask_RemovesTaskToList()
        {
            var manager = new TaskManager();
            manager.AddTask("Test Task");
            var task = manager.GetTasks().First();

            var result = manager.RemoveTask(task.Id);

            Assert.IsTrue(result, "Task didn't remove successfully.");
            Assert.AreEqual(0, manager.GetTasks().Count(), "The task list should be empty after removing a task");
        }

        [TestMethod]
        public void RemoveTask_NonExistentTask_ReturnsFalse()
        {
            var manager = new TaskManager();
            var nonExistentTaskId = Guid.NewGuid();

            var result = manager.RemoveTask(nonExistentTaskId);
            Assert.IsFalse(result, "Attempting to remove a non-existent task should return false");
        }

        [TestMethod]
        public void MarkTaskAsCompleted_SetIsCompletedToTrue()
        {
            var manager = new TaskManager();
            manager.AddTask("Test Task");
            var task = manager.GetTasks().First();

            var result = manager.MarkTaskAsCompleted(task.Id);
            Assert.IsTrue(result, "The task should be marked as completed successfully.");
            Assert.IsTrue(task.IsCompleted, "The IsCompleted property should be true for the completed task,");
        }

        [TestMethod]
        public void GetTasks_ReturnsAllTasks()
        {
            var manager = new TaskManager();
            manager.AddTask("Task 1");
            manager.AddTask("Task 2");
            var tasks = manager.GetTasks();

            Assert.AreEqual(2, tasks.Count(), "Should return all tasks.");
        }

        [TestMethod]
        public void GetTasks_OnlyCompletedTasks_ReturnsCorrectTasks()
        {
            var manager = new TaskManager();
            manager.AddTask("Task 1");
            manager.AddTask("Task 2");
            manager.MarkTaskAsCompleted(manager.GetTasks().Last().Id);

            var completedTasks = manager.GetTasks(true);

            Assert.AreEqual(1, completedTasks.Count(), "Should only return one completed tasks.");
            Assert.IsTrue(completedTasks.All(t => t.IsCompleted), "All returned tasks should be completed.");
        }

        [TestMethod]
        public void GetTaskById_WithExistingId_ReturnsCorrectTask()
        {
            var manager = new TaskManager();
            manager.AddTask("Existing Task");
            var existingTask = manager.GetTasks().First();

            var result = manager.GetTaskById(existingTask.Id);

            Assert.IsNotNull(result, "Should return a task for an existing ID.");
            Assert.AreEqual(existingTask.Id, result.Id, "The returned task should have the same Id as the requested task.");
        }

        [TestMethod]
        public void GetTaskById_WithNonExistingId_ReturnsNull()
        {
            var manager = new TaskManager();
            var nonExistingId = Guid.NewGuid();

            var result = manager.GetTaskById(nonExistingId);

            Assert.IsNull(result, "Should return null for non-existing ID.");
        }
    }
}