using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestVM
{ 
    [TestClass]
    public class RevenueStatisticVMTest
    {
        [TestMethod]
        public void CurrentView_Property()
        {
            var revenueStatisticVM = new RevenueStatisticVM();

            var currentView = new object();
            revenueStatisticVM.CurrentView = currentView;

            Assert.AreEqual(currentView, revenueStatisticVM.CurrentView);
        }

        [TestMethod]
        public void IsSaleChecked_Property()
        {
            var revenueStatisticVM = new RevenueStatisticVM();
           
            var isSaleChecked = true;
            revenueStatisticVM.IsSaleChecked = isSaleChecked;

            Assert.AreEqual(isSaleChecked,revenueStatisticVM.IsSaleChecked);
        }

        [TestMethod]
        public void IsTripCheck_Property()
        {
            var revenueStatisticVM = new RevenueStatisticVM();

            var isTripCheck = true;
            revenueStatisticVM.IsTripCheck = isTripCheck;

            Assert.AreEqual(isTripCheck, revenueStatisticVM.IsTripCheck);
        }

        [TestMethod]
        public void TripsStatistics_Property()
        {
            var obj = new object();
            var revenueStatisticVM = new RevenueStatisticVM();

            revenueStatisticVM.TripsStatistics(obj);

            Assert.IsTrue(revenueStatisticVM.CurrentView.GetType() == typeof(TripsStatisticsVM));
        }

        [TestMethod]
        public void SalesStatistics_Property()
        {
            var obj = new object();
            var revenueStatisticVM = new RevenueStatisticVM();

            revenueStatisticVM.SalesStatistics(obj);

            Assert.IsTrue(revenueStatisticVM.CurrentView.GetType() == typeof(SalesStatisticsVM));
        }
    }
}
