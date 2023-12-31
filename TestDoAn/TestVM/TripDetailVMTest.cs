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
    public class TripDetailVMTest
    {
        [TestMethod]
        public void SearchText_Property()
        {
            var listKhach = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(listKhach);
            var searchText = "C0000001";

            tripDatailVm.SearchText = searchText;

            Assert.AreEqual(searchText,tripDatailVm.SearchText);
        }

        [TestMethod]
        public void ListKhach_Property()
        {
            var list = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(list);
            var listKhach = new ObservableCollection<HANHKHACH>();

            tripDatailVm.ListKhach = listKhach;

            Assert.AreEqual(listKhach, tripDatailVm.ListKhach);
        }

        [TestMethod]
        public void SelectedFilter_Property()
        {
            var listKhach = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(listKhach);
            var selectedFiler = "selectedFilter";

            tripDatailVm.SelectedFilter = selectedFiler;

            Assert.AreEqual(selectedFiler, tripDatailVm.SelectedFilter);
        }

        [TestMethod]
        public void FilterCbItems_Property()
        {
            var list = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(list);
            var filerCbItems = new ObservableCollection<string>();

            tripDatailVm.FilterCbItems = filerCbItems;

            Assert.AreEqual(filerCbItems, tripDatailVm.FilterCbItems);
        }

        [TestMethod]
        public void IsAdmin_Property()
        {
            var list = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(list);
            var isAdmin = true;

            tripDatailVm.IsAdmin = isAdmin;

            Assert.AreEqual(isAdmin, tripDatailVm.IsAdmin);
        }

        [TestMethod]
        public void Chuyen_Property()
        {
            var list = new ObservableCollection<HANHKHACH>();
            var tripDatailVm = new TripDetailVM(list);
            var chuyen = new CHUYEN();

            tripDatailVm.CHUYEN = chuyen;

            Assert.AreEqual(chuyen, tripDatailVm.CHUYEN);
        }
    }
}
