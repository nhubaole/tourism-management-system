using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.View;
using TourismManagementSystem.Model;
using System.Collections.ObjectModel;

namespace TourismManagementSystem.ViewModel
{
    internal class AddTourVM : BaseViewModel
    {
        private TUYEN newTour;
        public ObservableCollection<DIADIEM> ListDiaDiem { get; set; }
        public ObservableCollection<LOAITUYEN> ListLoaiTuyen { get; set; }
        private string _ToolTipText;

        private static TUYEN _EditSelectedTuyen;
        private static int _IsEdit = 0;

        private string _Title;
        private string _BtnText;

        public AddTourVM()
        {
            ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
            ListDiaDiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            ListLoaiTuyen = new ObservableCollection<LOAITUYEN>(DataProvider.Ins.DB.LOAITUYENs);
            if (IsEdit == 0)
            {
                Title = "Thêm mới tuyến du lịch";
                BtnText = "Thêm";
                newTour = new TUYEN();
                Random random = new Random();
                NewTour.MATUYEN = random.Next(0, 1000).ToString();
            }
            else
            {
                Title = "Cập nhật tuyến du lịch";
                BtnText = "Cập nhật";
                newTour = EditSelectedTuyen;
            }
            
            ScheduleOptionCommand = new RelayCommand<Window>((p) => { 
                if (p == null)
                {
                    return false;
                }

                return true;
            }, (p) => {
                if(NewTour.SODEM > NewTour.SONGAY || NewTour.SONGAY <= 0 || NewTour.SODEM <= 0)
                {
                    MessageBox.Show("Số ngày đêm không hợp lệ");
                    return;
                }
                var addTourWindow = p as Window;
                if (addTourWindow != null)
                {
                    ScheduleVM.CurrTour = NewTour;
                    ScheduleWindow scheduleWindow = new ScheduleWindow();
                    scheduleWindow.ShowDialog();
                }
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NewTour.TENTUYEN) || NewTour.LOAITUYEN == null || NewTour.LOAITUYEN == null || NewTour.SONGAY == null || NewTour.SODEM == null || NewTour.GIADUKIEN == null )
                {
                    ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                    return false;
                }
                return true;
            }, (p) =>
            {
                var addTourWindow = p as Window;
                if(addTourWindow != null)
                {
                    if (NewTour.SODEM > NewTour.SONGAY || NewTour.SONGAY <= 0 || NewTour.SODEM <= 0)
                    {
                        MessageBox.Show("Số ngày đêm không hợp lệ");
                        return;
                    }
                    if (IsEdit == 0)
                    {
                        DataProvider.Ins.DB.TUYENs.Add(NewTour);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Thêm mới tuyến thành công!");
                    }
                    else
                    {
                        var itemToUpdate = DataProvider.Ins.DB.TUYENs.FirstOrDefault(item => item.MATUYEN == NewTour.MATUYEN);
                        if (itemToUpdate != null)
                        {
                            itemToUpdate = NewTour;
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Cập nhật lịch trình thành công!");
                        }
                    }
                    addTourWindow.Close();
                }
            });
        }

        public ICommand ScheduleOptionCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public TUYEN NewTour { get => newTour; set { newTour = value; OnPropertyChanged(); } }

        public string ToolTipText { get => _ToolTipText; set { _ToolTipText = value; OnPropertyChanged(); }  }

        public static TUYEN EditSelectedTuyen { get => _EditSelectedTuyen; set => _EditSelectedTuyen = value; }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string BtnText { get => _BtnText; set => _BtnText = value; }
    }
}
