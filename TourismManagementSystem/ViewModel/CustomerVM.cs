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

namespace TourismManagementSystem.ViewModel
{
    
    internal class CustomerVM : BaseViewModel
    {
        

        public ObservableCollection<KHACHHANG> KhachHangs { get; set; }
        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }

        

        private string maKH;
        public string MAKH
        {
            get => maKH;
            set
            {
                maKH = value;
                OnPropertyChanged(nameof(MAKH));
            }
        }

        private string hoTen;
        public string HOTEN
        {
            get => hoTen;
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(HOTEN));
            }
        }

        private string cccd;
        public string CCCD
        {
            get => cccd;
            set
            {
                cccd = value;
                OnPropertyChanged(nameof(CCCD));
            }
        }

        private string sdt;
        public string SDT
        {
            get => sdt;
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(SDT));
            }
        }

        private string email;
        public string EMAIL
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(EMAIL));
            }
        }

        private string diaChi;
        public string DIACHI
        {
            get => diaChi;
            set
            {
                diaChi = value;
                OnPropertyChanged(nameof(DIACHI));
            }
        } 
        private ObservableCollection<KHACHHANG> _ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        public ObservableCollection<KHACHHANG> ListKhachhang { get => _ListKhachhang; set { _ListKhachhang= value; OnPropertyChanged(nameof(DataProvider.Ins.DB.KHACHHANGs)); } }
    
        public CustomerVM()
        {
            KhachHangs = new ObservableCollection<KHACHHANG>();
            AddDataCommand = new RelayCommand<object>((p) => {
                if (maKH== null || hoTen == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                try
                {
                    var temp = new KHACHHANG()
                    {
                        MAKH = MAKH,
                        HOTEN = HOTEN,
                        CCCD = CCCD,
                        SDT = SDT,
                        EMAIL = EMAIL,
                        DIACHI = DIACHI
                    };

                    DataProvider.Ins.DB.KHACHHANGs.Add(temp);
                    DataProvider.Ins.DB.SaveChanges();
                    ListKhachhang.Add(temp);
                    LoadDataGrid();

                    MessageBox.Show(temp.ToString());


                    MessageBox.Show("Đã tạo mới khách hàng thành công");


                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately (e.g., log, display an error message)
                    MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                }
              
               
            }); 
            

        }
        private void LoadDataGrid()
        {
          ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        }

      
    }
}
