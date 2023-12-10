using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.UserControls;
using static System.Net.Mime.MediaTypeNames;

namespace TourismManagementSystem.ViewModel
{
   
    public class InforServiceVM :BaseViewModel
    {
        public static string filter;
        public static bool iscmbEnable; 
        //Phương tiện
        public static String MaPT;
        public static String TenPT;
        public static int SLG;
        public static PHUONGTIEN newPT;

        //nhà hàng
        public static String MaNH;
        public static String TenNH;
        public static String SDTNH;
        public static String MoTaNH;
        public static NHAHANG newNH;

        //khách sạn
        public static String MaKS;
        public static String TenKS;
        public static String SDTKS;
        public static String DcKS;
        public static int SoSaoKS;
        public static int SucChuaKS;
        public static KHACHSAN newKS;

        //dịch vụ khác
        public static String MaDVK;
        public static String TenDVK;
        public static String MoTaDVK;
        public static DICHVUKHAC newDVK;


        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public static bool IsNew; //xác định là tạo mới hay cập nhập

        private ObservableCollection<string> _FilterItems;
        public ObservableCollection<string> FilterItems
        {
            get { return _FilterItems; }
            set { _FilterItems = value; OnPropertyChanged(); }
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

        private bool _IscmbEnable;
        public bool IscmbEnable
        {
            get { return _IscmbEnable; }
            set
            {
                _IscmbEnable = value;

                OnPropertyChanged();
            }
        }

        private bool _CanSave;
        public bool CanSave
        {
            get { return _CanSave; }
            set
            {
                _CanSave = value;

                OnPropertyChanged();
            }
        }


        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

                OnPropertyChanged();
            }
        }
        public ICommand SaveServiceCommand { get; set; }
        public InforServiceVM() {
            FilterItems = new ObservableCollection<String>(new List<string> { "Phương tiện", "Khách sạn", "Nhà hàng", "Dịch vụ khác" });

            Filter = filter;
            IscmbEnable = true;
            CanSave = true;

            OnPropertyChanged(nameof(IscmbEnable));

            if (Filter != null)// có giá trị truyền từ ServiceVM -> cập nhập
            {
                IscmbEnable = false;
                OnPropertyChanged(nameof(IscmbEnable));

                switch (Filter)
                {
                    case "Phương tiện":
                        Traffic(null);
                        break;
                    case "Nhà hàng":
                        Restaurant(null);
                        break;
                    case "Khách sạn":

                        Hotel(null);
                        break;
                    case "Dịch vụ khác":

                        OTherService(null);
                        break;
                    default:
                        // Xử lý cho trường hợp khác
                        break;
                }
            }   
            
            //load khi thay đỏi Filter
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Filter))
                {
                    if (IsNew)
                    {
                        switch (Filter)
                        {
                            case "Phương tiện":
                                filter = Filter;
                                PhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
                                if (PhuongTien.Count() == 0)
                                {
                                    MaPT = GenerateCode("PT");
                                }
                                else
                                {
                                    MaPT = PhuongTien[PhuongTien.Count - 1].MAPT;
                                    MaPT = GenerateCode("PT", MaPT);
                                }
                                TenPT = null;
                                SLG = 0;

                                Traffic(null);

                                break;
                            case "Nhà hàng":
                                filter = Filter;
                                NhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
                                if (NhaHang.Count() == 0)
                                {
                                    MaNH = GenerateCode("NH");
                                }

                                else
                                {
                                    MaNH = NhaHang[NhaHang.Count - 1].MANH;
                                    MaNH = GenerateCode("NH", MaNH);
                                }
                                TenNH = null;
                                SDTNH = null;
                                MoTaNH = null;
                                Restaurant(null);
                                break;
                            case "Khách sạn":
                                filter = Filter;
                                KhachSan = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);
                                if (KhachSan.Count() == 0)
                                {
                                    MaKS = GenerateCode("KS");
                                }

                                else
                                {
                                    MaKS = KhachSan[KhachSan.Count - 1].MAKS;
                                    MaKS = GenerateCode("KS", MaKS);
                                }
                                TenKS = null;
                                SDTKS = null;
                                DcKS = null;
                                SoSaoKS = SucChuaKS = 0;
                                Hotel(null);
                                break;
                            case "Dịch vụ khác":
                                filter = Filter;
                                DVKhac = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);
                                if (DVKhac.Count() == 0)
                                {
                                    MaDVK = GenerateCodeDVK("DVK");
                                }

                                else
                                {
                                    MaDVK = DVKhac[DVKhac.Count - 1].MADV;
                                    MaDVK = GenerateCodeDVK("DVK", MaDVK);
                                }
                                TenDVK = null;
                                MoTaDVK = null;


                                OTherService(null);
                                break;
                            default:
                                // Xử lý cho trường hợp khác
                                break;
                        }
                    }   

                }
            };


            SaveServiceCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (IsNew)//tạo mới
                {

                    switch (Filter)
                    {
                        case "Phương tiện":
                            //cập nhập dữ liệu từ InforTRafficVm
                            if (TenPT == null || SLG == 0 || IsAllWhitespace(TenPT) || SLG.ToString() == null)
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {

                                NewTraffic();
                            }
                            break;
                        case "Nhà hàng":
                            if (TenNH == null || MoTaNH == null || !IsNumeric(SDTNH) || IsAllWhitespace(TenNH) || IsAllWhitespace(MoTaNH))
                            {
                                MessageBox.Show("Hãy kiểm tra lại các thông tin\nHãy chắc chắn bạn đã điền đủ thông tin rồi.");
                            }
                            else
                            {

                                NewRestaurant();
                            }    

                            break;
                        case "Khách sạn":
                            if (TenKS == null || SDTKS == null || DcKS == null || SoSaoKS == 0 || SucChuaKS == 0 || !IsNumeric(SDTKS) || IsAllWhitespace(TenKS) || IsAllWhitespace(DcKS))
                            {
                                MessageBox.Show("Hãy kiểm tra lại các thông tin\nHãy chắc chắn bạn đã điền đủ thông tin rồi.");
                            }
                            else
                            {
                

                                NewHotel();
                            }
                            break;
                        case "Dịch vụ khác":
                            if (TenDVK == null || MoTaDVK == null || IsAllWhitespace(TenDVK) || IsAllWhitespace(MoTaDVK))
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {
     
                                NewOtherService();
                            }
                            break;
                        default:
                            // Xử lý cho trường hợp khác
                            break;
                    }
                    IscmbEnable = false;
                    OnPropertyChanged(nameof(IscmbEnable));
                }
                else//cập nhập
                {
                    //xem coi cái nào được chọn
                    switch (Filter)
                    {
                        case "Phương tiện":
                            //cập nhập dữ liệu từ InforTRafficVm
                            if (TenPT == null || SLG == 0 || IsAllWhitespace(TenPT) || SLG.ToString() == null)
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {
                                EditInforTraffic();
                            }

                            break;
                        case "Nhà hàng":
                            if (TenNH == null || MoTaNH == null || !IsNumeric(SDTNH) || IsAllWhitespace(TenNH) || IsAllWhitespace(MoTaNH))
                            {
                                MessageBox.Show("Hãy kiểm tra lại các thông tin\nHãy chắc chắn bạn đã điền đủ thông tin rồi.");
                            }
                            else
                            {
                                EditInforRestaurant();
                            }

                            break;
                        case "Khách sạn":
                            if (TenKS == null || SDTKS == null || DcKS == null || SoSaoKS == 0 || SucChuaKS == 0 || !IsNumeric(SDTKS) ||  IsAllWhitespace(TenKS) || IsAllWhitespace(DcKS))
                            {
                                MessageBox.Show("Hãy kiểm tra lại các thông tin\nHãy chắc chắn bạn đã điền đủ thông tin rồi.");
                            }
                            else
                            {
                                EditInforHotel();
                            }
                            break;
                        case "Dịch vụ khác":
                            if (TenDVK == null || MoTaDVK == null || IsAllWhitespace(TenDVK) || IsAllWhitespace(MoTaDVK))
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {
                                EditInforOtherService();
                            }
                            break;
                        default:
                            // Xử lý cho trường hợp khác
                            break;
                    }
                }

            });
        }
        private void Traffic(object obj) => CurrentView = new InforTrafficVm();
        private void Restaurant(object obj) => CurrentView = new InforRestaurantVm();
        private void Hotel(object obj) => CurrentView = new InforHotelVM();
        private void OTherService(object obj) => CurrentView = new InforOTherServiceVM();
        public static string GenerateCode(string filter, string previousCode = null )
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

            string newCode = filter + newNumberStr;

            return newCode;
        }
        public static string GenerateCodeDVK(string filter, string previousCode = null)
        {
            int newNumber;

            if (previousCode == null)
            {
                newNumber = 1;
            }
            else
            {
                // Lấy số từ mã trước đó
                int previousNumber = int.Parse(previousCode.Substring(3));
                newNumber = previousNumber + 1;
            }

            // Chuyển số mới thành chuỗi và thêm số không vào đầu nếu cần
            string newNumberStr = newNumber.ToString().PadLeft(5, '0');

            string newCode = filter + newNumberStr;

            return newCode;
        }
        public static bool IsNumeric(string input)
        {
            double result;
            return double.TryParse(input, out result);
        }
        public static bool IsAllWhitespace(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }
        private void NewTraffic()
        {
            var temp = new PHUONGTIEN {
                TENPT = TenPT,
                MAPT = MaPT,
                SLGHE = SLG
            };

            DataProvider.Ins.DB.PHUONGTIENs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Đã tạo mới phương tiện thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập;
            //báo cho bên ServiceVm đã xong
            ServiceVM.IsDone = true;
            newPT = temp;
        }
        private void NewRestaurant()
        {
            var temp = new NHAHANG
            {
                MANH = MaNH,
                TENNH = TenNH,
                SDT = SDTNH,
                MOTA = MoTaNH,
            };

            DataProvider.Ins.DB.NHAHANGs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã tạo mới nhà hàng thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập;
            //báo cho bên ServiceVm đã xong
            ServiceVM.IsDone = true;
            newNH = temp;
        }
        private void NewHotel()
        {
            var temp = new KHACHSAN
            {
                MAKS = MaKS,
                TENKS = TenKS,
                DIACHI = DcKS,
                SDT = SDTKS,
                SOSAO = SoSaoKS,
                SUCCHUA = SucChuaKS,
            };

            DataProvider.Ins.DB.KHACHSANs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã tạo mới khách sạn thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập;
            //báo cho bên ServiceVm đã xong
            ServiceVM.IsDone = true;
            newKS = temp;
        }

        private void NewOtherService()
        {
            try
            {
                var temp = new DICHVUKHAC
                {
                    MADV = MaDVK,
                    TENDV = TenDVK,
                    MOTA = MoTaDVK,
                };

                DataProvider.Ins.DB.DICHVUKHACs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã tạo mới dịch vụ thành công \nBạn có thể xem lại các thông tin");
                //sau cập nhập;
                //báo cho bên ServiceVm đã xong
                ServiceVM.IsDone = true;
                newDVK = temp;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private void EditInforTraffic()
        {
            var temp = DataProvider.Ins.DB.PHUONGTIENs.Where(x => x.MAPT == MaPT).SingleOrDefault();
            if (temp.TENPT == TenPT && temp.SLGHE == SLG )
            {
                MessageBox.Show("Hãy điền các thông tin cần cập nhập");
            }   
            else
            {
                temp.TENPT = TenPT;
                temp.SLGHE = SLG;
                DataProvider.Ins.DB.SaveChanges();

                ServiceVM.IsDone = true;
                MessageBox.Show("Thông tin phương tiện đã được cập nhập");
            }    
        }
        private void EditInforRestaurant()
        {
            var temp = DataProvider.Ins.DB.NHAHANGs.Where(x => x.MANH == MaNH).SingleOrDefault();
           if (temp.TENNH == TenNH && temp.SDT == SDTNH && temp.MOTA == MoTaNH)
            {
                MessageBox.Show("Hãy nhập các thông tin cần thay đổi");
            }   
           else
            {
                temp.TENNH = TenNH;
                temp.SDT = SDTNH;
                temp.MOTA = MoTaNH;
                DataProvider.Ins.DB.SaveChanges();

                ServiceVM.IsDone = true;
                MessageBox.Show("Thông tin nhà hàng đã được cập nhập");
            }    
        }
        private void EditInforHotel()
        {
            var temp = DataProvider.Ins.DB.KHACHSANs.Where(x => x.MAKS == MaKS).SingleOrDefault();
            if (temp.TENKS == TenKS && temp.DIACHI == DcKS && temp.SDT == SDTKS && temp.SOSAO == SoSaoKS && temp.SUCCHUA == SucChuaKS )
            {
                MessageBox.Show("Hãy nhập các thông tin cần thay đổi");
            }
            else
            {
                temp.TENKS = TenKS;
                temp.DIACHI = DcKS;
                temp.SDT = SDTKS;
                temp.SOSAO = SoSaoKS;
                temp.SUCCHUA = SucChuaKS;
                DataProvider.Ins.DB.SaveChanges();
                ServiceVM.IsDone = true;
                MessageBox.Show("Thông tin khách sạn đã được cập nhập");
            }
        }

        private void EditInforOtherService()
        {
            var temp = DataProvider.Ins.DB.DICHVUKHACs.Where(x => x.MADV == MaDVK).SingleOrDefault();
            if (temp.TENDV == TenDVK && temp.MOTA == MoTaDVK)
            {
                MessageBox.Show("Hãy nhập các thông tin cần thay đổi");
            }
            else
            {

                temp.TENDV = TenDVK;
                temp.MOTA = MoTaDVK;

                DataProvider.Ins.DB.SaveChanges();

                ServiceVM.IsDone = true;
                MessageBox.Show("Thông tin dịch vụ đã được cập nhập");
            }
        }
    }
}
