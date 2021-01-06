using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.DatabaseConnectionUtils;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;

namespace IntegrationTesting
{
    [TestClass]
    public class TestGetFlag
    {
        private const string Server = @"DESKTOP-J211L71\MSSQLSERVER1";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"vfhnjxrf";
        private const int ConnectionTime = 75;

        FlagpoleDatabaseUtils fdu = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        [TestMethod]
        public void Test_Get_Flag_By_Id()
        {
            Assert.IsTrue(fdu.GetFlag(1, out _, out _));
        }

        [TestMethod]
        public void Test_Get_Flag_Correct_String_And_Value()
        {
            string resView;
            bool? resValue;
            fdu.GetFlag(1, out resView, out resValue);
            Assert.AreEqual("TTTF", resView);
            Assert.AreEqual(false, resValue);
        }

        [TestMethod]
        public void Test_Get_Flag_Equal_With_Added_Flag()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(8);
            string resView;
            bool? resValue;
            fdu.AddFlag(mbf.ToString(), true);
            fdu.GetFlag(2, out resView, out resValue);
            Assert.AreEqual(resView, mbf.ToString());
            Assert.AreEqual(resValue, mbf.GetFlag());
        }

        [TestMethod]
        public void Test_Get_Flag_By_Negative_Id()
        {
            Assert.IsFalse(fdu.GetFlag(-1, out _, out _));
        }

        [TestMethod]
        public void Test_Get_Flag_By_Non_Existent_Id()
        {
            Assert.IsFalse(fdu.GetFlag(1000, out _, out _));
        }

        [TestMethod]
        public void Test_Get_Flag_Non_Existent_Connection()
        {
            string FakeServer = @"DESKTOP-J211L71\OTHERSERVER";
            string FakeDatabase = @"IIG.CoSWE.FakeFlagpoleDB";
            FlagpoleDatabaseUtils fake_fdu = new FlagpoleDatabaseUtils(FakeServer, FakeDatabase, IsTrusted, Login, Password, ConnectionTime);
            Assert.IsFalse(fake_fdu.GetFlag(1, out _, out _));
        }
    }
}
