using MaterialDesignColors;
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
        public static bool IsNew = false;

        private bool _CanWrite;
        public bool CanWrite
        {
            get { return _CanWrite; }
            set
            {
                _CanWrite = value;
                OnPropertyChanged();
            }
        }

        private String _Filter;
        public String Filter { get { return _Filter; } set { _Filter = value; OnPropertyChanged(); CanWrite = !string.IsNullOrEmpty(value); } }

        private String _SearchTerm;
        public String SearchTerm { get { return _SearchTerm; } set { _SearchTerm = value; OnPropertyChanged(); } }

        private DIADIEM _selectedItem;
        public DIADIEM selectedItem { get { return _selectedItem; } set { _selectedItem = value; OnPropertyChanged(); } }

        public static DIADIEM SelectedItem;

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
        public ObservableCollection<DIADIEM> diadiem
        {

            get => _diadiem;
            set { _diadiem = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DIADIEM> _SearchResult;
        public ObservableCollection<DIADIEM> SearchResult
        {

            get => _SearchResult;
            set { _SearchResult = value; OnPropertyChanged(); }
        }


        public static String MaDD;
        public static String TenDD;
        public static String DcDD;
        public static String MtDD;

        public LocationVM()
        {

            diadiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            SearchResult = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);

            cmbItems = new ObservableCollection<String>(new List<string> { "Mã địa điểm", "Tên địa điểm", "Địa chỉ", "Mô tả" });


            FindLocationnCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                SearchResult.Clear();

                if (SearchTerm != null)
                {
                    List<DIADIEM> temporaryList = new List<DIADIEM>();

                    switch (Filter)
                    {
                        case "Mã địa điểm":
                            foreach (DIADIEM item in diadiem)
                            {
                                if (IsSubstring(item.MADD.ToLower(), SearchTerm.ToLower()))
                                {
                                    temporaryList.Add(item);
                                }
                            }
                            break;

                        case "Tên địa điểm":
                            foreach (DIADIEM item in diadiem)
                            {
                                if (IsSubstring(item.TENDD.ToLower(), SearchTerm.ToLower()))
                                {
                                    temporaryList.Add(item);
                                }
                            }
                            break;

                        case "Địa chỉ":
                            foreach (DIADIEM item in diadiem)
                            {
                                if (IsSubstring(item.DIACHI.ToLower(), SearchTerm.ToLower()))
                                {
                                    temporaryList.Add(item);
                                }
                            }
                            break;

                        case "Mô tả":
                            foreach (DIADIEM item in diadiem)
                            {
                                if (IsSubstring(item.MOTA.ToLower(), SearchTerm.ToLower()   ))
                                {
                                    temporaryList.Add(item);
                                }
                            }
                            break;
                    }

                    SearchResult.Clear();
                    foreach (DIADIEM item in temporaryList)
                    {
                        SearchResult.Add(item);
                    }
                    OnPropertyChanged(nameof(SearchResult));
                    OnPropertyChanged(nameof(SearchResult)); 
                }
                else
                {
                    MessageBox.Show("Hãy nhập thông tin bạn cần tìm");
                }
           
            });

            ToAddLocationCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                InforLocationVM.IsNew = true;
                if (diadiem.Count() == 0)
                {
                    MaDD = GenerateCode();
                }
                else
                {
                    MaDD = diadiem[diadiem.Count - 1].MADD;
                    MaDD = GenerateCode(MaDD);
                }
                TenDD = DcDD = MtDD = null;

                AddLocationWindow addLocation = new AddLocationWindow();
                addLocation.ShowDialog();




                if (IsDone)
                {
                    diadiem.Add(InforLocationVM.d);
                    OnPropertyChanged(nameof(diadiem));

                    SearchResult.Add(InforLocationVM.d);
                    OnPropertyChanged(nameof(SearchResult));
                    IsDone = false;
                }

            });

            ToEditLocationCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforLocationVM.IsNew = false;
                MaDD = selectedItem.MADD;
                TenDD = selectedItem.TENDD;
                DcDD = selectedItem.DIACHI;
                MtDD = selectedItem.MOTA;
                AddLocationWindow addLocation = new AddLocationWindow();

                addLocation.ShowDialog();

                if (IsDone)
                {
                    OnPropertyChanged(nameof(diadiem));
                    OnPropertyChanged(nameof(SearchResult));
                    IsDone = false;
                }
            });

            DeleteLocationCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (selectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xáo địa điểm này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // kiểm tra có tham chiếu k
                        bool hasReferences = CheckLocationReferences(selectedItem);

                        if (hasReferences)
                        {
                            MessageBox.Show("Địa điểm này k thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        // Xóa
                        DataProvider.Ins.DB.DIADIEMs.Remove(selectedItem);
                        DataProvider.Ins.DB.SaveChanges();
                        diadiem.Remove(selectedItem);
                        SearchResult.Remove(selectedItem);

                        selectedItem = null;
                        MessageBox.Show("Đã xóa địa điểm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;


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


        public void LoadDatagrid()
        {
            diadiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            SearchResult = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);

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

        private bool CheckLocationReferences(DIADIEM location)
        {
            //thỏa một trong 3 điều kiện thì trả về true
            bool hasReferences = location.LICHTRINHs.Count > 0 || location.LICHTRINHs1.Count > 0 || location.TUYENs.Count > 0;

            return hasReferences;
        }

    }
}
