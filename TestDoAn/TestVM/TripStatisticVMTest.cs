using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class TripStatisticVMTest
    {
        [TestMethod]
        public void CanChoseMonth_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            var canChoseMonth = false;

            tripStatisticVM.CanChoseMonth = canChoseMonth;

            Assert.AreEqual(canChoseMonth,tripStatisticVM.CanChoseMonth);
        }


        [TestMethod]
        public void CanChoseYear_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            var canChoseYear = true;

            tripStatisticVM.CanChoseYear = canChoseYear;

            Assert.AreEqual(canChoseYear, tripStatisticVM.CanChoseYear);
        }


        [TestMethod]
        public void TimeOfChart_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            var timeOfChart = "new";

            tripStatisticVM.TimeOfChart = timeOfChart;

            Assert.AreEqual(timeOfChart, tripStatisticVM.TimeOfChart);
        }

        [TestMethod]
        public void FilterMonth_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            ObservableCollection<int> filterMonth = new ObservableCollection<int>();

            tripStatisticVM.FilterMonth = filterMonth;

            Assert.AreEqual(filterMonth, tripStatisticVM.FilterMonth);
        }

        [TestMethod]
        public void FilterItems1_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            ObservableCollection<string> filterItems1 = new ObservableCollection<string>();

            tripStatisticVM.FilterItems1 = filterItems1;

            Assert.AreEqual(filterItems1, tripStatisticVM.FilterItems1);
        }

        [TestMethod]
        public void FilterYear_Property()
        {
            var tripStatisticVM = new TripsStatisticsVM();
            ObservableCollection<int> filterYear = new ObservableCollection<int>();

            tripStatisticVM.FilterYear = filterYear;

            Assert.AreEqual(filterYear, tripStatisticVM.FilterYear);
        }
    }

}
