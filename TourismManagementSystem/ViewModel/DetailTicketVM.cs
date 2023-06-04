using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class DetailTicketVM : BaseViewModel
    {
        public static VE SelectedTicket { get; set; }

        private ObservableCollection<VE> _Ticket;
        private ObservableCollection<HANHKHACH> _Passenger;
        private ObservableCollection<CHUYEN> _Trip;

        public DetailTicketVM()
        {
            Ticket = new ObservableCollection<VE>();
            Passenger = new ObservableCollection<HANHKHACH>();
            Trip = new ObservableCollection<CHUYEN>();
            Ticket.Add(SelectedTicket);
            if (_Ticket.First().HANHKHACH != null)
                Passenger.Add(_Ticket.First().HANHKHACH);
            if (_Ticket.First().PHIEUDATCHO != null)
                Trip.Add(_Ticket.First().PHIEUDATCHO.CHUYEN);
            PrintTicketCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    Grid ticket = p as Grid;
                    if (ticket != null)
                    {
                        PrintDialog printDialog = new PrintDialog();
                        if (printDialog.ShowDialog() == true)
                        {
                            printDialog.PrintVisual(ticket, "Ticket");
                            MessageBox.Show("Đã in vé thành công, vui lòng kiểm tra lại!", "Thông báo");
                        }
                    }

                }
            );
            SendTicket = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    Grid ticket = p as Grid;
                    if (ticket != null)
                    {
                        string pdfPath = AppDomain.CurrentDomain.BaseDirectory + "Ticket\\" + SelectedTicket.MAVE + ".pdf";
                        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Ticket"))
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ticket");
                        if (!File.Exists(pdfPath))
                        {
                            using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4.Rotate());
                                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                                document.Open();

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    RenderTargetBitmap bmp = new RenderTargetBitmap((int)(ticket.ActualWidth * 300 / 96), (int)(ticket.ActualHeight * 300 / 96), 300, 300, PixelFormats.Pbgra32);
                                    bmp.Render(ticket);

                                    BitmapEncoder encoder = new PngBitmapEncoder();
                                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                                    encoder.Save(ms);

                                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms.GetBuffer());
                                    image.SetAbsolutePosition(0, 0);
                                    image.ScaleAbsolute(document.PageSize.Width, document.PageSize.Height);

                                    document.Add(image);
                                }
                                document.Close();
                            }
                        }

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("bookings.adventour@gmail.com");
                        string receiveEmail = SelectedTicket.PHIEUDATCHO.KHACHHANG.EMAIL;
                        mail.To.Add(receiveEmail);
                        mail.Subject = "[ Xác nhận ] Vé du lịch điện tử - Công ty du lịch AdvenTour";
                        mail.Body = "Chào anh/chị,\n\nCông ty du lịch AdvenTour xin được gửi tới anh/chị vé tham quan du lịch điện tử.\n\nVui lòng kiểm tra thông tin trên vé (file pdf được đính kèm trong email này) để đảm bảo tính chính xác. Nếu có bất kỳ thay đổi hoặc câu hỏi nào liên quan đến vé du lịch, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc dưới đây:\nĐịa chỉ email: bookings.adventour@gmail.com\nSố điện thoại: 0823.306.992 - 0943.229.560\n\nChúng tôi mong rằng anh/chị sẽ có một chuyến đi thú vị và trọn vẹn cùng AdvenTour. Xin cảm ơn anh/chị đã tin tưởng và sử dụng dịch vụ của chúng tôi.\n\nTrân trọng,\nCông ty du lịch AdvenTour";

                        Attachment attachment = new Attachment(pdfPath);
                        mail.Attachments.Add(attachment);

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("bookings.adventour@gmail.com", "lhxaedaeawdcdzft");
                        smtpClient.Send(mail);
                        MessageBox.Show("Đã gửi vé thành công!", "Thông báo");
                    }

                }
            );
        }

        public ICommand PrintTicketCommand { get; set; }
        public ICommand SendTicket { get; set; }
        public ObservableCollection<VE> Ticket
        {
            get => _Ticket;
            set
            {
                _Ticket = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<HANHKHACH> Passenger { get => _Passenger; set { _Passenger = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUYEN> Trip { get => _Trip; set { _Trip = value; OnPropertyChanged(); } }
    }
}
