using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static DAL.NoteEntity;
using System.Collections.Generic;

namespace DALTest
{
    [TestClass]
    public class NotesEntityTest
    {
        [TestMethod]
        public void GetUsersTest()
        {
            List<DAL.NoteEntity> notes = DAL.NotesRepository.GetAll();
            Assert.IsNotNull(notes);
        }

        [TestMethod]
        public void AddUsersTest()
        {
            //int userId = UsersManager.AddUsers(new NoteEntity("a", "b"));
            //Assert.IsTrue(userId > 0, "Неверное ИД");
            //User u = UsersManager.GetUserById(userId);
            //Assert.IsTrue(u.FirstName == "a", "Неверное имя");
            //Assert.IsTrue(u.LastName == "b", "Неверная фамилия");
        }
    }
}
