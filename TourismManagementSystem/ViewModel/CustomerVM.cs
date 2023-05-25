using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string maKH;
        public string MaKH
        {
            get { return maKH; }
            set
            {
                maKH = value;
                OnPropertyChanged(nameof(MaKH));
            }
        }

        private string hoTen;
        public string HoTen
        {
            get { return hoTen; }
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }

        private string cccd;
        public string CCCD
        {
            get { return cccd; }
            set
            {
                cccd = value;
                OnPropertyChanged(nameof(CCCD));
            }
        }

        private string sdt;
        public string SDT
        {
            get { return sdt; }
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(SDT));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string diaChi;
        public string DiaChi
        {
            get { return diaChi; }
            set
            {
                diaChi = value;
                OnPropertyChanged(nameof(DiaChi));
            }
        }

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
                
                var temp = new KHACHHANG()
                {
                    MAKH = MaKH,
                    HOTEN = HoTen,
                    CCCD = CCCD,
                    SDT = SDT,
                    EMAIL = Email,
                    DIACHI = DiaChi};
                DataProvider.Ins.DB.KHACHHANGs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã tạo mới địa điểm thành công");
                //Xóa các thông tin cũ
               
            }); 

        }

      
    }
}
