using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class InforLocationVM:BaseViewModel
    {
        private String MaDD;

        private String _maDD;
        
        public String maDD { get { return _maDD; } set { _maDD = value; OnPropertyChanged(); } }

        private String _tenDD;
        public String tenDD { get { return _tenDD; } set { _tenDD = value; OnPropertyChanged(); } }


        private String _dcDD;
        public String dcDD { get { return _dcDD; } set { _dcDD = value; OnPropertyChanged(); } }

        private String _mtDD;
        public String mtDD { get { return _mtDD; } set { _mtDD = value; OnPropertyChanged(); } }

        public ICommand AddLocationCommand { get; set; }

        public InforLocationVM() {

            if (LocationVM.ketqua.Count() == 0)
            {
                MaDD = GenerateCode();
            }
            else
            {
                MaDD = LocationVM.ketqua[LocationVM.ketqua.Count - 1].MADD;
                MaDD = GenerateCode(MaDD);

            }

            maDD = MaDD;

            AddLocationCommand = new RelayCommand<object>((p) => {
                if (tenDD == null || dcDD == null)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                var temp = new DIADIEM() { MADD = maDD, TENDD = tenDD, DIACHI = dcDD, MOTA = mtDD };
                DataProvider.Ins.DB.DIADIEMs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                LocationVM.ketqua.Add(temp);
                LocationVM.IsDone = true;   
                MessageBox.Show("Đã tạo mới địa điểm thành công");

                //Xóa các thông tin cũ
                MaDD = GenerateCode(MaDD);
                maDD = MaDD;
                tenDD = null;
                dcDD = null;
                mtDD = null;

            });
        }
        public static string GenerateCode(string previousCode = null)
        {
            int newNumber;

            if (previousCode == null)
            {
                newNumber = 1;
            }
            else
            {
                // Lấy số từ mã trước đó
                int previousNumber = int.Parse(previousCode.Substring(2));
                newNumber = previousNumber + 1;
            }

            // Chuyển số mới thành chuỗi và thêm số không vào đầu nếu cần
            string newNumberStr = newNumber.ToString().PadLeft(6, '0');

            // Tạo mã mới
            string newCode = "DD" + newNumberStr;

            return newCode;
        }

    }
}
