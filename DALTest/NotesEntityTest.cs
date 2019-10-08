using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class NotesEntityTest
    {
        private NotesRepository dal;

        [TestInitialize]
        public void SetupContext()
        {
            dal = new NotesRepository();
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var notes = dal.GetAll();
            Assert.IsNotNull(notes);
        }
    }
}