using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Repositories;
using LibraryData.Models;
using System.Linq;

namespace LibraryDataLayerTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserRepository _userRepository;
        private ITestDataGenerator _dataGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            // Toggle this line to switch between Randomized and Scripted TestDataGenerators
            bool useRandomizedData = false; // Set true for Randomized, false for Scripted

            _dataGenerator = useRandomizedData ? (ITestDataGenerator)new RandomizedTestDataGenerator()
                                               : new ScriptedTestDataGenerator();

            var users = _dataGenerator.GenerateUsers(5);
            _userRepository = new UserRepository();
            foreach (var user in users)
            {
                _userRepository.AddUser(user);
            }
        }

        [TestMethod]
        public void GetUser_ValidId_ReturnsUser()
        {
            var user = _userRepository.GetUser(1);
            Assert.IsNotNull(user);
            Assert.IsTrue(user.FirstName.Contains("FirstName 1"), "The user's first name should include 'FirstName 1'");
        }

        [TestMethod]
        public void GetAllUsers_ReturnsAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            Assert.AreEqual(5, users.Count());
        }

        [TestMethod]
        public void UpdateUser_ModifiesUserDetails()
        {
            var userToUpdate = new Librarian(3, "Modified First", "Modified Last");
            _userRepository.UpdateUser(userToUpdate);

            var updatedUser = _userRepository.GetUser(3);
            Assert.AreEqual("Modified First", updatedUser.FirstName);
        }

        [TestMethod]
        public void DeleteUser_RemovesUser()
        {
            var initialCount = _userRepository.GetAllUsers().Count();
            var userToDelete = _userRepository.GetUser(1); 

            _userRepository.DeleteUser(userToDelete);
            var userAfterDeletion = _userRepository.GetUser(1);
            var finalCount = _userRepository.GetAllUsers().Count();

            Assert.IsNull(userAfterDeletion, "User should be null after deletion.");
            Assert.AreEqual(initialCount - 1, finalCount, "Total user count should decrease by one.");
        }
    }
}
