using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class LocationVM : BaseViewModel

    {
      public static bool IsDone = false;

        private String _tbLocation;
        public String tbLocation { get { return _tbLocation; }  set { _tbLocation = value; OnPropertyChanged(); } }
        private String _cmbLocation;
        public String cmbLocation { get { return _cmbLocation; } set { _cmbLocation = value; OnPropertyChanged(); } }

        private DIADIEM _selectedItem;
        public DIADIEM selectedItem { get { return _selectedItem; } set { _selectedItem = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand FindLocationnCommand { get; set; }

        public ICommand ChangecmbLocation { get; set; }
        public ICommand ToAddLocationCommand { get; set; }
      
        public ICommand ToEditLocationCommand { get; set; }

        public ICommand DeleteLocationCommand { get; set; }

        public ICommand ResetCommand { get; set; }




        public ObservableCollection<String> cmbItems { get; set; }

        private ObservableCollection<DIADIEM> _diadiem;
        public  ObservableCollection<DIADIEM> diadiem {

            get => _diadiem; 
            set  { _diadiem = value; OnPropertyChanged(); }
        }

        public static ObservableCollection<DIADIEM> ketqua = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);

        public LocationVM()
        {
            diadiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            cmbItems = new ObservableCollection<String>();
            foreach (DIADIEM item in diadiem)
            {
                cmbItems.Add(item.MADD);
            }    

            FindLocationnCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                //2 ô không được điền
                if (tbLocation == null && cmbLocation == null)
                {
                    MessageBox.Show("Vui lòng điền thông tin cần tìm kiếm");
                }
                else
                {
                    //2 ô đều được điền nhưng khác nhau
                    if (tbLocation != cmbLocation)
                    {
                        MessageBox.Show("Thông tin bạn đưa vào không trùng nhau, chỉ cần nhập một trong hai để tìm kiếm \nHãy xóa thông tin không cần thiết");
                    }
                    else
                    {
                        //một trong 2 ô được điền
                        bool IsFinded = false;
                        if (tbLocation != null)
                        {


                            //tìm kiếm theo mã

                            if (!IsFinded)
                            {
                                foreach (DIADIEM item in diadiem)
                                {
                                    if (IsSubstring(item.MADD, tbLocation))
                                    {
                                        IsFinded = true;
                                        ketqua.Add(item);
                                    }
                                }
                            }

                            //kiếm theo tên địa điểm

                            if (!IsFinded)
                            {
                                foreach (DIADIEM item in diadiem)
                                {
                                    if ( IsSubstring(item.TENDD, tbLocation))
                                    {
                                        IsFinded = true;
                                        ketqua.Add(item);
                                    }
                                }
                            }

                           //kiếm theo địa chỉ
                            if (!IsFinded)
                            {
                                foreach (DIADIEM item in diadiem)
                                {
                                    if (IsSubstring(item.DIACHI, tbLocation))
                                    {
                                        IsFinded = true;
                                        ketqua.Add(item);
                                    }
                                }
                            }

                            //kiếm theo mô tả

                            if (!IsFinded)
                            {
                                foreach (DIADIEM item in diadiem)
                                {
                                    if (IsSubstring(item.MOTA, tbLocation))
                                    {
                                        IsFinded = true;
                                        ketqua.Add(item);
                                    }
                                }
                            }
                        }
                           
                    }
                }
            })  ;

            ToAddLocationCommand  = new RelayCommand<object> ((p) => { return p == null ? false : true; },(p) =>
            {

                AddLocationWindow addLocation = new AddLocationWindow();
                addLocation.ShowDialog();
                if (IsDone)
                {
                    LoadDatagrid();
                    IsDone = false;
                }

            }) ;
            
            ToEditLocationCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Show thông tin
                var a = selectedItem;
                AddLocationWindow editLocation = new AddLocationWindow();
                editLocation.Show();
                
            });

            ResetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadDatagrid();
            });

        }

        public bool IsSubstring(string str, string substr)
        {
            if (str == null || substr == null)
                return false;

            int strLength = str.Length;
            int substrLength = substr.Length;

            if (substrLength > strLength)
                return false;

            for (int i = 0; i <= strLength - substrLength; i++)
            {
                int j;

                for (j = 0; j < substrLength; j++)
                {
                    if (str[i + j] != substr[j])
                        break;
                }

                if (j == substrLength)
                    return true;
            }

            return false;
        }


        public  void LoadDatagrid ()
        {
            diadiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
        }

    }


}
