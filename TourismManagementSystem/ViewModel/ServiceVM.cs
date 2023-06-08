﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.UserControls;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class ServiceVM : BaseViewModel
    {
        public static bool IsDone = false;
        private bool _IsTbEnable;
        //Thông tin phương tiện
        public static PHUONGTIEN pt; 

        public static String MaPT;
        public static String TenPT;
        public static int SLGhe ;


        private bool _IsDisplay;
        public bool IsTbEnable
        {
            get { return _IsTbEnable; }
            set
            {
                _IsTbEnable = value;
                OnPropertyChanged();
            }
        }
        

        private bool _IsFilter2Enable;
        public bool IsFilter2Enable
        {
            get { return _IsFilter2Enable; }
            set
            {
                _IsFilter2Enable = value;
                OnPropertyChanged();
            }
        }
        private string _SelectedTable;
        public string SelectedTable
        {
            get { return _SelectedTable; }
            set
            {
                _SelectedTable = value;
                OnPropertyChanged();
            }
        }
        private string _Filter1;
        public string Filter1
        {
            get { return _Filter1; }
            set
            {
                _Filter1 = value;

                IsFilter2Enable = !string.IsNullOrEmpty(value);
                UpdateFilter2();
                UpdateSelectedTable();
            }
        }

       

        private string _Filter2;
        public string Filter2
        {
            get { return _Filter2; }
            set
            {
                _Filter2 = value;
                IsTbEnable = !string.IsNullOrEmpty(value);
                
            }
        }

        private String _SearchTerm;
        public String SearchTerm { get { return _SearchTerm; } set { _SearchTerm = value; OnPropertyChanged(); } }

      

        
        private ObservableCollection<string> _FilterItems1;
        public ObservableCollection<string> FilterItems1
        {
            get { return _FilterItems1; }
            set { _FilterItems1 = value; }
        }

        private ObservableCollection<string> _FilterItems2;
        public ObservableCollection<string> FilterItems2
        {
            get { return _FilterItems2; }
            set { _FilterItems2 = value; }
        }

        private ObservableCollection<PHUONGTIEN> _PhuongTien;
        public ObservableCollection<PHUONGTIEN> PhuongTien
        {

            get => _PhuongTien;
            set { _PhuongTien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NHAHANG> _NhaHang;
        public ObservableCollection<NHAHANG> NhaHang
        {

            get => _NhaHang;
            set { _NhaHang = value; OnPropertyChanged(); }
        }
        private ObservableCollection<KHACHSAN> _KhachSan;
        public ObservableCollection<KHACHSAN> KhachSan
        {

            get => _KhachSan;
            set { _KhachSan = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DICHVUKHAC> _DVKhac;
        public ObservableCollection<DICHVUKHAC> DVKhac
        {

            get => _DVKhac;
            set { _DVKhac = value; OnPropertyChanged(); }
        }

        //Danh sách để binding vô datagrid (tính cả tìm kiếm)

        private ObservableCollection<PHUONGTIEN> _SearchResultPT;
        public ObservableCollection<PHUONGTIEN> SearchResultPT
        {

            get => _SearchResultPT;
            set { _SearchResultPT = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NHAHANG> _SearchResultNH;
        public ObservableCollection<NHAHANG> SearchResultNH
        {

            get => _SearchResultNH;
            set { _SearchResultNH = value; OnPropertyChanged(); }
        }
       
        private ObservableCollection<KHACHSAN> _SearchResultKS;
        public ObservableCollection<KHACHSAN> SearchResultKS
        {

            get => _SearchResultKS;
            set { _SearchResultKS = value; OnPropertyChanged(); }
        }

       
        private ObservableCollection<DICHVUKHAC> _SearchResultDVK;
        public ObservableCollection<DICHVUKHAC> SearchResultDVK
        {

            get => _SearchResultDVK;
            set { _SearchResultDVK = value; OnPropertyChanged(); }
        }

        // các dịch vụ được chọn


        private PHUONGTIEN _selectedItemPT;

        public PHUONGTIEN selectedItemPT { get { return _selectedItemPT; } set { _selectedItemPT = value; OnPropertyChanged(); } }


        private NHAHANG _selectedItemNH;

        public NHAHANG selectedItemNH { get { return _selectedItemNH; } set { _selectedItemNH = value; OnPropertyChanged(); } }

        private KHACHSAN _selectedItemKS;

        public KHACHSAN selectedItemKS { get { return _selectedItemKS; } set { _selectedItemKS = value; OnPropertyChanged(); } }

        private DICHVUKHAC _selectedItemDVK;

        public DICHVUKHAC selectedItemDVK { get { return _selectedItemDVK; } set { _selectedItemDVK = value; OnPropertyChanged(); } }

        public ICommand ToAddServiceCommand { get; set; }

        public ICommand DeleteTrafficCommand { get; set; }

        public ICommand ToEditTrafficCommand { get; set; }

        public ICommand DeleteRestaurantCommand { get; set; }
        public ICommand ToEditRestaurantCommand { get; set; }

        public ICommand DeleteHotelCommand { get; set; }
        public ICommand ToEditHotelCommand { get; set; }

        public ICommand DeleteOtherServiceCommand { get; set; }
        public ICommand ToEditOtherServiceCommand { get; set; }

        public ICommand FindServiceCommand { get; set; }
        public ICommand RadioButtonCommand { get; set; }
        public bool IsDisplay { get => _IsDisplay; set { _IsDisplay = value; OnPropertyChanged(); } }

        public ServiceVM() {
            IsDisplay = true;
            FilterItems1 = new ObservableCollection<String>(new List<string> { "Phương tiện", "Khách sạn", "Nhà hàng", "Dịch vụ khác" });


            PhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
            SearchResultPT = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);

            NhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
            SearchResultNH = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);

            KhachSan = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);
            SearchResultKS = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);

            DVKhac = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);
            SearchResultDVK = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);

            //
            ToAddServiceCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                selectedItemPT = null;
                selectedItemNH = null;
                selectedItemKS = null;
                selectedItemDVK = null;

                InforServiceVM.filter = null;

                InforServiceVM.MaPT = null;
                InforServiceVM.TenPT = null;
                InforServiceVM.SLG = 0;

                InforServiceVM.MaKS = null;
                InforServiceVM.TenKS = null;
                InforServiceVM.DcKS = null;
                InforServiceVM.SDTKS = null;
                InforServiceVM.SoSaoKS = 0;
                InforServiceVM.SucChuaKS = 0;

                InforServiceVM.MaNH = null; 
                InforServiceVM.TenNH = null;
                InforServiceVM.MoTaNH = null;
                InforServiceVM.SDTNH = null;

                InforServiceVM.MaDVK = null;
                InforServiceVM.TenDVK = null;
                InforServiceVM.MoTaDVK = null;


                InforServiceVM.IsNew = true;

                AddServiceWindow addService = new AddServiceWindow();

                addService.ShowDialog();

                if (IsDone)
                {
                    switch (InforServiceVM.filter)
                    {
                        case "Phương tiện":

                            PhuongTien.Add(InforServiceVM.newPT);
                            OnPropertyChanged(nameof(PhuongTien));

                            SearchResultPT.Add(InforServiceVM.newPT);
                            OnPropertyChanged(nameof(SearchResultPT));
                            IsDone = false;

                            InforServiceVM.MaPT = null;
                            InforServiceVM.TenPT = null;
                            InforServiceVM.SLG = 0;
                            InforServiceVM.filter = null;
                            selectedItemPT = null;

                            break;

                        case "Nhà hàng":

                            NhaHang.Add(InforServiceVM.newNH);
                            OnPropertyChanged(nameof(NhaHang));

                            SearchResultNH.Add(InforServiceVM.newNH);
                            OnPropertyChanged(nameof(SearchResultNH));


                            IsDone = false;
                            break;


                        case "Khách sạn":

                            KhachSan.Add(InforServiceVM.newKS);
                            OnPropertyChanged(nameof(KhachSan));

                            SearchResultKS.Add(InforServiceVM.newKS);
                            OnPropertyChanged(nameof(SearchResultKS));

                            IsDone = false;
                            break;

                        case "Dịch vụ khác":
                            DVKhac.Add(InforServiceVM.newDVK);
                            OnPropertyChanged(nameof(DVKhac));

                            SearchResultDVK.Add(InforServiceVM.newDVK);
                            OnPropertyChanged(nameof(SearchResultDVK));

                            IsDone = false;
                            break;

                            default: break;
                    }
                }
            });

            FindServiceCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                // Kiểm tra filter 1 và filter 2 rồi kiểm tra text box
                switch (Filter1)
                {
                    case "Phương tiện":
                        SearchResultPT.Clear();
                        if (SearchTerm != null)
                        {
                            List<PHUONGTIEN> temporaryList = new List<PHUONGTIEN>();

                            switch (Filter2)
                            {
                                case "Mã phương tiện":
                                    foreach (PHUONGTIEN item in PhuongTien)
                                    {
                                        if (IsSubstring(item.MAPT.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Tên phương tiện":
                                    foreach (PHUONGTIEN item in PhuongTien)
                                    {
                                        if (IsSubstring(item.TENPT.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Số lượng ghế":
                                    if (int.TryParse(SearchTerm, out int searchTermInt))
                                    {
                                        foreach (PHUONGTIEN item in PhuongTien)
                                        {
                                            if (item.SLGHE == searchTermInt)
                                            {
                                                temporaryList.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số lượng ghế phải là một số");
                                    }
                                    break;
                            }
                            foreach (PHUONGTIEN item in temporaryList)
                            {
                                SearchResultPT.Add(item);
                            }

                            OnPropertyChanged(nameof(SearchResultPT));
                            OnPropertyChanged(nameof(SearchResultPT));
                        }
                        else
                        {
                            MessageBox.Show("Hãy nhập thông tin bạn cần tìm");
                        }
                        break;
                    case "Nhà hàng":
                        SearchResultNH.Clear();
                        if (SearchTerm != null)
                        {
                            List<NHAHANG> temporaryList = new List<NHAHANG>();

                            switch (Filter2)
                            {
                                case "Mã nhà hàng":
                                    foreach (NHAHANG item in NhaHang)
                                    {
                                        if (IsSubstring(item.MANH.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Tên nhà hàng":
                                    foreach (NHAHANG item in NhaHang)
                                    {
                                        if (IsSubstring(item.TENNH.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Số điện thoại":
                                    foreach (NHAHANG item in NhaHang)
                                    {
                                        if (IsSubstring(item.SDT.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Mô tả":
                                    foreach (NHAHANG item in NhaHang)
                                    {
                                        if (IsSubstring(item.MOTA.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                            }
                            foreach (NHAHANG item in temporaryList)
                            {
                                SearchResultNH.Add(item);
                            }

                            OnPropertyChanged(nameof(SearchResultNH));
                            OnPropertyChanged(nameof(SearchResultNH));
                        }
                        else
                        {
                            MessageBox.Show("Hãy nhập thông tin bạn cần tìm");
                        }
                        break;
                    case "Khách sạn":
                        SearchResultKS.Clear();
                        if (SearchTerm != null)
                        {
                            List<KHACHSAN> temporaryList = new List<KHACHSAN>();

                            switch (Filter2)
                            {
                                case "Mã khách sạn":
                                    foreach (KHACHSAN item in KhachSan)
                                    {
                                        if (IsSubstring(item.MAKS.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Tên khách sạn":
                                    foreach (KHACHSAN item in KhachSan)
                                    {
                                        if (IsSubstring(item.TENKS.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Địa chỉ":
                                    foreach (KHACHSAN item in KhachSan)
                                    {
                                        if (IsSubstring(item.DIACHI.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Số điện thoại":
                                    foreach (KHACHSAN item in KhachSan)
                                    {
                                        if (IsSubstring(item.SDT.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;
                                case "Số sao":
                                    if (int.TryParse(SearchTerm, out int searchTermInt))
                                    {
                                        foreach (KHACHSAN item in KhachSan)
                                        {
                                            if (item.SOSAO == searchTermInt)
                                            {
                                                temporaryList.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số sao của nhà hàng phải là một số");
                                    }
                                    break;
                                case "Sức chứa":
                                    if (int.TryParse(SearchTerm, out int searchTermInt1))
                                    {
                                        foreach (KHACHSAN item in KhachSan)
                                        {
                                            if (item.SUCCHUA == searchTermInt1)
                                            {
                                                temporaryList.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Sức chứa của nhà hàng phải là một số");
                                    }
                                    break;
                            }
                            foreach (KHACHSAN item in temporaryList)
                            {
                                SearchResultKS.Add(item);
                            }

                            OnPropertyChanged(nameof(SearchResultKS));
                            OnPropertyChanged(nameof(SearchResultKS));
                        }
                        else
                        {
                            MessageBox.Show("Hãy nhập thông tin bạn cần tìm");
                        }
                        break;

                    case "Dịch vụ khác":
                        SearchResultDVK.Clear();
                        if (SearchTerm != null)
                        {
                            List<DICHVUKHAC> temporaryList = new List<DICHVUKHAC>();

                            switch (Filter2)
                            {
                                case "Mã dịch vụ":
                                    foreach (DICHVUKHAC item in DVKhac)
                                    {
                                        if (IsSubstring(item.MADV.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                                case "Tên dịch vụ":
                                    foreach (DICHVUKHAC item in DVKhac)
                                    {
                                        if (IsSubstring(item.TENDV.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;
                                case "Mô tả":
                                    foreach (DICHVUKHAC item in DVKhac)
                                    {
                                        if (IsSubstring(item.MOTA.ToLower(), SearchTerm.ToLower()))
                                        {
                                            temporaryList.Add(item);
                                        }
                                    }
                                    break;

                            }
                            foreach (DICHVUKHAC item in temporaryList)
                            {
                                SearchResultDVK.Add(item);
                            }

                            OnPropertyChanged(nameof(SearchResultDVK));
                            OnPropertyChanged(nameof(SearchResultDVK));
                        }
                        else
                        {
                            MessageBox.Show("Hãy điền thông tin cần tìm kiếm");
                        }
                        break;

                }

            });

            ToEditTrafficCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforServiceVM.IsNew = false;
                InforServiceVM.filter = "Phương tiện";
                OnPropertyChanged(nameof(InforServiceVM.filter));
                InforServiceVM.MaPT = selectedItemPT.MAPT;
                InforServiceVM.TenPT = selectedItemPT.TENPT;
                InforServiceVM.SLG = (int)selectedItemPT.SLGHE;

                AddServiceWindow Infor = new AddServiceWindow();

                Infor.ShowDialog();

                if (IsDone)
                {
                    OnPropertyChanged(nameof(PhuongTien));
                    OnPropertyChanged(nameof(SearchResultPT));
                    IsDone = false;
                }
            });

            ToEditRestaurantCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforServiceVM.IsNew = false;
                InforServiceVM.filter = "Nhà hàng";
                OnPropertyChanged(nameof(InforServiceVM.filter));
                InforServiceVM.MaNH = selectedItemNH.MANH;
                InforServiceVM.TenNH  = selectedItemNH.TENNH;
                InforServiceVM.MoTaNH = selectedItemNH.MOTA;
                InforServiceVM.SDTNH = selectedItemNH.SDT;

                AddServiceWindow Infor = new AddServiceWindow();

                Infor.ShowDialog();

                if (IsDone)
                {
                    //OnPropertyChanged(nameof(diadiem));
                    //OnPropertyChanged(nameof(SearchResult));
                    IsDone = false;
                }
            });
            ToEditHotelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforServiceVM.IsNew = false;
                InforServiceVM.filter = "Khách sạn";
                OnPropertyChanged(nameof(InforServiceVM.filter));
                InforServiceVM.MaKS = selectedItemKS.MAKS;
                InforServiceVM.TenKS = selectedItemKS.TENKS;
                InforServiceVM.SoSaoKS = (int)selectedItemKS.SOSAO;
                InforServiceVM.SucChuaKS = (int)selectedItemKS.SUCCHUA;
                InforServiceVM.SDTKS = selectedItemKS.SDT;

                AddServiceWindow Infor = new AddServiceWindow();

                Infor.ShowDialog();

                if (IsDone)
                {
                    //OnPropertyChanged(nameof(diadiem));
                    //OnPropertyChanged(nameof(SearchResult));
                    IsDone = false;
                }
            });

            ToEditOtherServiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforServiceVM.IsNew = false;
                InforServiceVM.filter = "Dịch vụ khác";
                OnPropertyChanged(nameof(InforServiceVM.filter));
                InforServiceVM.MaDVK = selectedItemDVK.MADV;
                InforServiceVM.TenDVK = selectedItemDVK.TENDV;
                InforServiceVM.MoTaDVK = selectedItemDVK.MOTA;

                AddServiceWindow Infor = new AddServiceWindow();
                Infor.ShowDialog();

                if (IsDone)
                {
                    //OnPropertyChanged(nameof(diadiem));
                    //OnPropertyChanged(nameof(SearchResult));
                    IsDone = false;
                }
            });

            DeleteTrafficCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (selectedItemPT != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa phương tiện này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // kiểm tra có tham chiếu k

                        if (selectedItemPT.LICHTRINHs.Count > 0)
                        {
                            MessageBox.Show("Phương tiện này k thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        // Xóa
                        DataProvider.Ins.DB.PHUONGTIENs.Remove(selectedItemPT);
                        DataProvider.Ins.DB.SaveChanges();
                        PhuongTien.Remove(selectedItemPT);
                        SearchResultPT.Remove(selectedItemPT);

                        selectedItemPT = null;
                        MessageBox.Show("Đã xóa phương tiện thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;


            });
            DeleteRestaurantCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (selectedItemNH != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa nhà hàng này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // kiểm tra có tham chiếu k

                        if (selectedItemNH.LICHTRINHs.Count > 0)
                        {
                            MessageBox.Show("Nhà hàng này k thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        // Xóa
                        DataProvider.Ins.DB.NHAHANGs.Remove(selectedItemNH);
                        DataProvider.Ins.DB.SaveChanges();
                        NhaHang.Remove(selectedItemNH);
                        SearchResultNH.Remove(selectedItemNH);

                        selectedItemNH = null;
                        MessageBox.Show("Đã xóa nhà hàng thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;


            });
            DeleteHotelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (selectedItemKS != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa khách sạn này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // kiểm tra có tham chiếu k

                        if (selectedItemKS.LICHTRINHs.Count > 0)
                        {
                            MessageBox.Show("Khách sạn này k thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        // Xóa
                        DataProvider.Ins.DB.KHACHSANs.Remove(selectedItemKS);
                        DataProvider.Ins.DB.SaveChanges();
                        KhachSan.Remove(selectedItemKS);
                        SearchResultKS.Remove(selectedItemKS);

                        selectedItemNH = null;
                        MessageBox.Show("Đã xóa khách sạn thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;


            });
            DeleteOtherServiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (selectedItemDVK != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // kiểm tra có tham chiếu k

                        if (selectedItemDVK.LICHTRINHs.Count > 0)
                        {
                            MessageBox.Show("Dịch vụ này không thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        // Xóa
                        DataProvider.Ins.DB.DICHVUKHACs.Remove(selectedItemDVK);
                        DataProvider.Ins.DB.SaveChanges();
                        DVKhac.Remove(selectedItemDVK);
                        SearchResultDVK.Remove(selectedItemDVK);

                        selectedItemDVK = null;
                        MessageBox.Show("Đã xóa dịch vụ thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;


            });
            RadioButtonCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                MainWindow mainWindow = Window.GetWindow(p) as MainWindow;
                if (mainWindow != null)
                {
                    MainVM mainVM = mainWindow.DataContext as MainVM;
                    if (mainVM != null)
                    {
                        mainVM.CurrentView = new LocationVM();
                        mainVM.PageTitle = "Quản lý địa điểm du lịch";
                    }
                }
            });
        }

    
        public void UpdateFilter2()
        {
            // Cập nhật items trong Filter2
            if (Filter1 == "Phương tiện")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã phương tiện", "Tên phương tiện", "Số lượng ghế" };
                
            }
            else if (Filter1 == "Nhà hàng")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã nhà hàng", "Tên nhà hàng", "Số điện thoại", "Mô tả" };
            }
            else if (Filter1 == "Khách sạn")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã khách sạn", "Tên khách sạn", "Địa chỉ", "Số điện thoại", "Số sao", "Sức chứa" };
                
            }
            else if (Filter1 == "Dịch vụ khác")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã dịch vụ", "Tên dịch vụ", "Mô tả" };
            }
            OnPropertyChanged(nameof(FilterItems2));
        }
        public void UpdateSelectedTable()
        {
            // Cập nhật items trong Filter2
            if (Filter1 == "Phương tiện")
            {
                SelectedTable = "dtgridPT";

            }
            else if (Filter1 == "Khách sạn")
            {
                SelectedTable = "dtgridKS";

            }
            else if (Filter1 == "Nhà hàng")
            {
                SelectedTable = "dtgridNH";
            }
            else if (Filter1 == "Dịch vụ khác")
            {
                SelectedTable = "dtgridDVK";
            }
            OnPropertyChanged(nameof(SelectedTable));
            //scroll đến table nếu có thể
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

        public static string GenerateCode(string filter, string previousCode = null)
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

            string temp = null;

            // Tạo mã mới
            switch (filter)
            {
                case "Phương tiện":
                    temp = "PT";
                    break;
                case "Khách sạn":
                    temp = "KS";
                    break;
                case "Nhà hàng":
                    temp = "NH";
                    break;
                case "Dịch vụ khác":
                    temp = "DV";
                    break;
            }

            string newCode = temp + newNumberStr;

            return newCode;
        }
      

    }
}
