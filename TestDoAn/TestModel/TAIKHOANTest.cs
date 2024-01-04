using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class TAIKHOANTest
    {
        [TestMethod]
        public void TestTENTKProperty()
        {
            var tk = new TAIKHOAN();
            string ten = "USER0001";
            tk.TENTK = ten;

            Assert.AreEqual(ten, tk.TENTK);
        }

        [TestMethod]
        public void TestMATKHAUProperty()
        {
            var tk = new TAIKHOAN();
            string mk = "12345678";
            tk.MATKHAU = mk;

            Assert.AreEqual(mk, tk.MATKHAU);
        }
    }
}
