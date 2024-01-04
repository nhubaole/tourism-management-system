using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;
using System.Collections.ObjectModel;


namespace TestDoAn.TestVM
{
    [TestClass]
    public class SalesStatisticsVMTest
    {
        [TestMethod]
        public void FilterMonth_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            ObservableCollection<int> filterMonth = new ObservableCollection<int>();

            salesStatisticsVm.FilterMonth = filterMonth;

            Assert.AreEqual(filterMonth, salesStatisticsVm.FilterMonth);
        }

        [TestMethod]
        public void FilterItems1_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            ObservableCollection<string> filterItems1 = new ObservableCollection<string>();

            salesStatisticsVm.FilterItems1 = filterItems1;

            Assert.AreEqual(filterItems1, salesStatisticsVm.FilterItems1);
        }

        [TestMethod]
        public void FilterYear_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            ObservableCollection<int> filterYear = new ObservableCollection<int>();

            salesStatisticsVm.FilterYear = filterYear;

            Assert.AreEqual(filterYear, salesStatisticsVm.FilterYear);
        }

        [TestMethod]
        public void CanChoseMonth_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            var canChoseMonth = false;

            salesStatisticsVm.CanChoseMonth = canChoseMonth;

            Assert.AreEqual(canChoseMonth, salesStatisticsVm.CanChoseMonth);
        }


        [TestMethod]
        public void CanChoseYear_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            var canChoseYear = true;

            salesStatisticsVm.CanChoseYear = canChoseYear;

            Assert.AreEqual(canChoseYear, salesStatisticsVm.CanChoseYear);
        }

        [TestMethod]
        public void TimeOfChart_Property()
        {
            var salesStatisticsVm = new SalesStatisticsVM();
            var timeOfChart = "new";

            salesStatisticsVm.TimeOfChart = timeOfChart;

            Assert.AreEqual(timeOfChart, salesStatisticsVm.TimeOfChart);
        }
    }
}
