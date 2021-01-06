using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.FileWorker;
using IIG.PasswordHashingUtils;

namespace IntegrationTesting
{
    [TestClass]
    public class TestFilePasswordHasher
    {
        [TestMethod]
        public void Test_Write_Hash_To_File()
        {
            string password = "password";
            string salt = "salt";
            Assert.IsTrue(BaseFileWorker.Write(PasswordHasher.GetHash(password, salt), "C:\\TestFiles\\hashes.txt"));
        }

        [TestMethod]
        public void Test_Read_Hash_From_File()
        {
            string password = "password";
            string salt = "salt";
            Assert.IsNotNull(BaseFileWorker.ReadAll("C:\\TestFiles\\hashes.txt"));
            Assert.AreEqual(PasswordHasher.GetHash(password, salt), BaseFileWorker.ReadAll("C:\\TestFiles\\hashes.txt"));
        }

        [TestMethod]
        public void Test_Write_Read_Hash_Equal()
        {
            string password = "test_password";
            string salt = "test_salt";
            string passHash = PasswordHasher.GetHash(password, salt);
            BaseFileWorker.Write(passHash, "C:\\TestFiles\\hashes.txt");
            Assert.AreEqual(passHash, BaseFileWorker.ReadAll("C:\\TestFiles\\hashes.txt"));
        }
    }
}
