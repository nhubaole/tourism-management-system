using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismManagementSystem.ViewModel
{
   
    public class InforServiceVM :BaseViewModel
    {
        public static bool IsNew; //xác định là tạo mới hay cập nhập
        public ObservableCollection<String> cmbItems { get; set; }

        public InforServiceVM() {
            cmbItems = new ObservableCollection<String>(new List<string> { "Phương tiện", "Nhà hàng", "Khách sạn", "Dịch vụ khác" });
        }    
    }
}
