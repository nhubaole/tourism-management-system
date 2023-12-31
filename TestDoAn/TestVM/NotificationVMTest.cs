using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;
using System.Collections.ObjectModel;
using TourismManagementSystem.Model;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class NotificationVMTest
    {
        [TestMethod]
        public void ListThongBao_Property()
        {
            var notificationVm = new NotificationVM();
            ObservableCollection<THONGBAO> listThongbaos = new ObservableCollection<THONGBAO>();

            notificationVm.ListThongBao = listThongbaos;

            Assert.AreEqual(listThongbaos, notificationVm.ListThongBao);
        }
    }
}
