using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;
using TourismManagementSystem.UserControls;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace TourismManagementSystem.ViewModel
{
    
    internal class CustomerVM : BaseViewModel
    {

        private ObservableCollection<string> _filter = new ObservableCollection<string>() { "Mã khách hàng", "Tên khách hàng","Số điện thoại" };
        public ObservableCollection<KHACHHANG> KhachHangs { get; set; }
        public ICommand SwitchWindowCommand { get; set; }
        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand BookingButtonCommand { get; set; }


        private KHACHHANG _selectedCustomer;
        public KHACHHANG SelectedCustomer { get => _selectedCustomer; set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));

            }
        }

        
        
        private string _selectedFilter;
        public string selectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(selectedFilter));
            }
        }
        public ObservableCollection<string> filter { get => _filter; set { _filter = value; OnPropertyChanged(nameof(filter)); } }

        private string _SearchText;

        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                if (selectedFilter == filter[0])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.MAKH.Contains(SearchText)));
                }
                else if (selectedFilter == filter[1])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.HOTEN.Contains(SearchText)));
                }
                else if (selectedFilter == filter[2])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.SDT.Contains(SearchText)));
                }
              
                OnPropertyChanged();
            }
        }



        private ObservableCollection<KHACHHANG> _ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        public ObservableCollection<KHACHHANG> ListKhachhang { get => _ListKhachhang; set { _ListKhachhang= value; OnPropertyChanged(nameof(ListKhachhang)); } }

        public CustomerVM()
        {
            selectedFilter = filter[0];
            SwitchWindowCommand = new RelayCommand<object>((p) => {
               
                  return true;
                
            }, (p) =>
            {SwitchWindow(p);});

            KhachHangs = new ObservableCollection<KHACHHANG>();
            


            UpdateDataCommand = new RelayCommand<object>((p) =>
            {
                
                return true;
            },
            (p) => { UpdateCustomer(); });
            DeleteDataCommand = new RelayCommand<object>((p) =>
            {
              
                return true;
            },
            (p) => { DeleteCustomer(); });

            BookingButtonCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BookingDetailVM.SelectedPhieu = p as PHIEUDATCHO;
                BookingDetailsWindow bookingDetailsWindow = new BookingDetailsWindow();
                bookingDetailsWindow.ShowDialog();
            });


        }
        private void SwitchWindow(object parameter)
        {
            try
            {
                AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
                addCustomerWindow.ShowDialog();
                ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            }
            catch(Exception e)
            {
               
                MessageBox.Show(e.Message);

            }


        }

        private void UpdateCustomer()
        {
            if (SelectedCustomer != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var itemToUpdate = DataProvider.Ins.DB.KHACHHANGs.FirstOrDefault(item => item.MAKH == SelectedCustomer.MAKH);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate = SelectedCustomer;
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                    }
                }
            }
        }
        private void DeleteCustomer()
        {
            if(SelectedCustomer != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedCustomer);
                    DataProvider.Ins.DB.SaveChanges();
                    ListKhachhang.Remove(SelectedCustomer);
                }
            }
        }
       




    }
}
