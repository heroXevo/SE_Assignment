using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Data_Access;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;



namespace TraderPlaceApp.Tests.Controllers
{
    [TestClass]
    public class RoleControllerTest
    {
        
        
        SE_DatabaseEntities enitity = new SE_DatabaseEntities();        
        List<Role> allRoles = new List<Role>();
        List<Role> updateRoleList = new List<Role>();
        RoleRepository rs = new RoleRepository();
        TransactionScope ts;
        

        [TestInitialize]
        public void TestInitialize()
        {
           

            ts = new TransactionScope();

            Role r = new Role();
            r.Role_Name = "MethodTest";

            Role role = new Role();
            role.Role_Name = "Edit Test";

            Role rupdate = new Role();
            rupdate.Role_Name = "Same";

            enitity.Roles.AddObject(r);
            enitity.Roles.AddObject(role);
            enitity.Roles.AddObject(rupdate);
            enitity.SaveChanges();

            allRoles = enitity.Roles.ToList();
            updateRoleList = enitity.Roles.ToList();

        }

        [TestMethod]
        public void AddRoleTest()
        {

            Role r = new Role();
            r.Role_Name = "testRole";            

            allRoles.Add(r);
            rs.CreateRole(r);
            
            AreListsEqual(allRoles, enitity.Roles.ToList());
            
            List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
        
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddRoleTestWithNull()
        {
            try
            {
                Role r = new Role();
                r = null;

                allRoles.Add(r);
                rs.CreateRole(r);

                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRoleTestWithEmptyField()
        {
            try
            {

                Role r = new Role();
                r.Role_Name = "";

                allRoles.Add(r);
                new RoleRepository().CreateRole(r);

                //change here 
                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddRoleTestWithNonAsciiChar()
        {
            try
            {

                Role r = new Role();
                r.Role_Name = "testRole££$";

                allRoles.Add(r);
                rs.CreateRole(r);

                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddRoleTestWithNameLong()
        {
            try
            {

                Role r = new Role();
                r.Role_Name = "aaaaaaaaaaaaaaaaaaaaaaaaaa";

                allRoles.Add(r);
                rs.CreateRole(r);

                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddRoleTestWithNameShort()
        {
            try
            {

                Role r = new Role();
                r.Role_Name = "aa";

                allRoles.Add(r);
                rs.CreateRole(r);

                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddRoleTestWithNumbers()
        {
            try
            {

                Role r = new Role();
                r.Role_Name = "1234";

                allRoles.Add(r);
                rs.CreateRole(r);

                AreListsEqual(allRoles, enitity.Roles.ToList());

                List<Role> AllRoleList = new RoleRepository().GetRoles().ToList();

            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        public void GetAllRoles()
        {

            List<Role> methodRoles = new RoleRepository().GetRoles().ToList();

            Assert.IsTrue(allRoles.Count == methodRoles.Count, "Not equivalent");

        }

        [TestMethod]
        public void DeleteRoleTest()
        {

            List<Role> RoleList = new RoleRepository().GetRoles().ToList();

            foreach (Role r in RoleList)
            {

                if (r.Role_Name == "MethodTest")
                {

                    new RoleRepository().DeleteRole(r.RoleID);

                }
            }

            bool roleChec = false;
            foreach (Role r in enitity.Roles.ToList())
            {

                if (r.Role_Name == "MethodTest")
                {
                    roleChec = true;
                }


            }

            Assert.IsFalse(roleChec, "Role Not deleted");


        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteRoleTestWithNonExistingID()
        {
            try
            {


                new RoleRepository().DeleteRole(90);



            }
            catch
            {
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteRoleTestWithIndexOutOfRange()
        {
            try
            {


                new RoleRepository().DeleteRole(-1);



            }
            catch
            {
                throw;
            }
        }

        [TestMethod]
        public void UpdateRoleTest()
        {

            foreach (Role r in updateRoleList)
            {

                if (r.Role_Name == "Edit Test")
                {

                    r.Role_Name = "updateTest";                    
                    rs.UpdateRole(r);

                }

            }

            AreListsEqual(updateRoleList, enitity.Roles.ToList());

        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRoleWithEmptyString()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = "";
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }


        [TestMethod]
        [ExpectedException(typeof(ConstraintException))]
        public void UpdateRoleWithNull()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = null;
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateRoleWithNonAsciiChar()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = "newUpdatedTest%%%";
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateRoleWithLongChar()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateRoleWithShortChar()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = "aa";
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateRoleWithNumbers()
        {
            try
            {

                foreach (Role r in enitity.Roles.ToList())
                {

                    if (r.Role_Name == "Same")
                    {

                        r.Role_Name = "1234";
                        rs.UpdateRole(r);

                    }

                }
            }
            catch
            {
                throw;
            }

        }


        //done till here 
        [TestMethod]
        public void GetRoleByNameTest()
        {

            Role repRole = rs.GetRoleByName("Same");
            Role entRole = enitity.Roles.SingleOrDefault(x => x.Role_Name == "Same");

            Assert.AreEqual(entRole.Role_Name, repRole.Role_Name, "Objects are not equal");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRoleByNameEmptyStringTest()
        {

            try
            {
                rs.GetRoleByName("");
            }
            catch
            {
                throw;
            }
                

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRoleByNameTestWithNull()
        {

            try
            {
                rs.GetRoleByName(null);
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoleByNameTestWithNonAsciiChar()
        {

            try
            {
                rs.GetRoleByName("testFind^^");
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoleByNameTestWithLongChar()
        {

            try
            {
                rs.GetRoleByName("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoleByNameTestWithShortChar()
        {

            try
            {
                rs.GetRoleByName("hh");
            }
            catch
            {
                throw;
            }


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoleByNameTestWithNumbers()
        {

            try
            {
                rs.GetRoleByName("12315");
            }
            catch
            {
                throw;
            }


        }

        [TestCleanup]
        public void TestCleanUp()
        {
            
            ts.Dispose();

        }

        public void  AreListsEqual<T>(List<T> expectedOutPut, List<T> actualOutPut)
        {
            
            //if (actualOutPut.Count != expectedOutPut.Count)
            //{

            //    Assert.Fail("Expected and actual list are not the same size");

            //}
            for (int i = 0; i == actualOutPut.Count - 1; i++)
            {

                Assert.AreEqual(expectedOutPut.ElementAt(i), actualOutPut.ElementAt(i),"They Are not equal");

            }

        }
    }
}
