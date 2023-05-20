using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class ScheduleVM : BaseViewModel
    {
        private static TUYEN currTour;
        private string _SoNgayDem;

        public ScheduleVM()
        {
            SoNgayDem = CurrTour.SONGAY + " ngày " + CurrTour.SODEM + " đêm";
        }

        public static TUYEN CurrTour { 
            get => currTour;
            set
            {
                currTour = value;
            }
        }

        public string SoNgayDem { get => _SoNgayDem; set { _SoNgayDem = value; OnPropertyChanged(); }  }
    }
}
