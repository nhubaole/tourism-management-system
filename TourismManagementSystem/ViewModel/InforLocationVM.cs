using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public static bool IsNew ; //xác định là tạo mới hay cập nhập

        public static DIADIEM d = null;

        private String _maDD;
        
        public String maDD { get { return _maDD; } set { _maDD = value; OnPropertyChanged(); } }

        private String _tenDD;
        public String tenDD { get { return _tenDD; } set { _tenDD = value; OnPropertyChanged(); } }


        private String _dcDD;
        public String dcDD { get { return _dcDD; } set { _dcDD = value; OnPropertyChanged(); } }

        private String _mtDD;
        public String mtDD { get { return _mtDD; } set { _mtDD = value; OnPropertyChanged(); } }

        public ICommand SaveLocationCommand { get; set; }


        public InforLocationVM() {
            maDD = LocationVM.MaDD;
            tenDD = LocationVM.TenDD;
            dcDD = LocationVM.DcDD;
            mtDD = LocationVM.MtDD;
           //tạo mới 
            SaveLocationCommand = new RelayCommand<object>((p) => {
               if (IsNew)//them mới k nhập tên với địa chỉ thì sai
                {
                    if (tenDD == null || dcDD == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
               else//cập nhâp
                {
                    if (dcDD == LocationVM.DcDD && tenDD == LocationVM.TenDD && mtDD == LocationVM.MtDD)
                    { 
                        return false; 
                    }
                    else 
                    {
                        if (tenDD == null || dcDD == null || tenDD == "" || dcDD == "")
                        {
                            return false;
                        }
                    }
                    return true;
                }
                    
            }, (p) =>
            {
                if (IsNew)//tạo mới
                {

                    //mtDD = null;
                    NewLocation();
                    IsNew = false;

                }
                else//cập nhập
                {
                   
                    EditLocation();
                }

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
        public void EditLocation()
        {
            var temp = DataProvider.Ins.DB.DIADIEMs.Where(x => x.MADD == maDD).SingleOrDefault();
            if (tenDD != null && dcDD != null ) {
                temp.TENDD = tenDD;
                temp.DIACHI = dcDD;
                temp.MOTA = mtDD;
                DataProvider.Ins.DB.SaveChanges();
                LocationVM.IsDone = true;
                MessageBox.Show("Thông tin địa điểm đã được cập nhập");
                //sau cập nhập 
                LocationVM.TenDD = tenDD;
                LocationVM.DcDD = dcDD;
                LocationVM.MtDD = mtDD;
            }
            else
            {
                MessageBox.Show("Hãy kiểm tra các thông tin cần cập nhập");
            }


            
        }
        public void NewLocation ()
        {
            var temp = new DIADIEM()
            {
                MADD = maDD,
                TENDD = tenDD,
                DIACHI = dcDD,
                MOTA = mtDD
            };
            d = temp;

            DataProvider.Ins.DB.DIADIEMs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();

            LocationVM.IsDone = true;
            MessageBox.Show("Đã tạo mới địa điểm thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập 
            LocationVM.TenDD = tenDD;
            LocationVM.DcDD = dcDD;
            LocationVM.MtDD = mtDD;



            //Xóa các thông tin cũ
            //maDD = GenerateCode(maDD);
            //LocationVM.MaDD = maDD;
            //tenDD = null;
            //dcDD = null;
        }

    }
   
}
