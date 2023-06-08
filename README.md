# AdvenTour
 Đồ án môn học **Nhập môn công nghệ phần mềm (SE104.N23)**, trường Đại học Công nghệ Thông tin - Đại học Quốc gia Thành phố Hồ Chí Minh.
##	Mô tả
- AdvenTour là một ứng dụng được phát triển để hỗ trợ quản lý các hoạt động và thông tin liên quan đến một công ty du lịch

  - **Đối với khách hàng:** ứng dụng giúp người dùng xem thông tin các tour du lịch hiện có, theo dõi lịch trình các chuyến, thông tin vé đã đăng kí
  - **Đối với công ty:** Ứng dụng cho phép tạo, sửa đổi và xóa các tour du lịch hay quản lý thông tin khách hàng. Người dùng có thể thêm thông tin chi tiết về tour, như địa điểm, ngày khởi hành, thời gian, giá cả và số lượng khách hàng tối đa, thông tin khách hàng đã đăng kí. Người dùng cũng có thể theo dõi tình trạng đặt chỗ và tình trạng thanh toán của khách hàng hay của từng tour.
## Người dùng

 -  Các công ty du lịch hoặc tổ chức trong ngành du lịch.
 -  Khách hàng có nhu cầu tìm hiểu về các tour du lịch.
  
## Mục tiêu
### Ứng dụng thực tế:

-  Ứng dụng giúp công ty du lịch quản lý tour một cách hiệu quả. Nhân viên có thể tạo, cập nhật và theo dõi thông tin chi tiết về tour, bao gồm địa điểm, ngày khởi hành, thời gian, giá cả và số lượng khách hàng tối đa. Qua đó, công ty du lịch có thể quản lý tình trạng đặt chỗ, thanh toán và số lượng chỗ trống cho từng tour, đồng thời tăng cường trải nghiệm cho khách hàng.
-  Ứng dụng giúp công ty du lịch quản lý thông tin khách hàng dễ dàng. Nhân viên có thể thêm, sửa và xóa thông tin khách hàng, bao gồm tên, địa chỉ, số điện thoại, email và các thông tin khác. Điều này giúp công ty tiếp cận thông tin khách hàng nhanh chóng và cung cấp dịch vụ tốt hơn.
-  Ứng dụng giúp khách hàng dễ dàng tìm hiểu các tour du lịch mong muốn, theo dõi tình trạng các tour đã đăng kí với lịch trình được cập nhập liên tục.
### Yêu cầu ứng dụng:
*	Giao diện trực quan, đơn giản nhưng không nhàm chán, thân thiện với người dùng.
*	Phân quyền rõ ràng, tăng tính bảo mật dữ liệu.
*	Quản lý thông tin hợp lý, dễ dàng truy xuất, chỉnh sửa thông tin.
*	Tìm kiếm và lọc dữ liệu nhanh chóng.
*	Thông báo và nhắc nhở những thông tin quan trọng.
*	Báo cáo, thống kê.
*	Hướng dẫn cụ thể, đơn giản, dễ dàng sử dụng
*	Dễ dàng sửa chữa, phát triển các tính năng trong tương lai.
## Chức năng
<details>
  <summary>Chức năng chung</summary>
  
  - Đăng nhập theo phần quyền người dùng.
  - Quản lý tuyến du lịch.
  - Quản lý chuyến du lịch.
  - Quản lý địa điểm.
  - Quản lý dịch vụ.
  - Quản lý thông tin khách hàng.
  - Quản lý phiếu đặt chỗ.
  - Quản lý bán vé.
  - Báo cáo thống kê.
</details>
 <details>
    <summary>Quản lý tuyến</summary>
  
  - Thêm mới tuyến du lịch.
  - Tra cứu tuyến du lịch.
  - Cập nhập tuyến du lịch.
  - Xóa tuyến du lịch
  </details>
   <details>
    <summary>Quản lý chuyến</summary>
  
  - Thêm mới chuyến du lịch.
  - Tra cứu chuyến du lịch.
  - Xóa chuyến du lịch.
  - Cập nhập chuyến du lịch.
  - Cảnh báo hành khách chưa đạt mức số lượng tối thiểu của chuyến
  
  </details>
  <details>
  <summary>Quản lý địa điểm</summary>
  
  - Thêm mới địa điểm du lịch.
  - Tra cứu địa điểm du lịch.
  - Xóa địa điểm du lịch.
  - Cập nhập địa điểm du lịch.
  
  </details>
  <details>
  <summary>Quản lý dịch vụ</summary>
  
  - Thêm mới đdịch vụ.
  - Tra cứu dịch vụ.
  - Xóad ịch vụ.
  - Cập nhập thông tin dịch vụ. 
  </details>
   <details>
  <summary>Quản lý thông tin khách hàng</summary>
  
  - Thêm mới thông tin khách hàng.
  - Tra cứu thông tin khách hàng.
  - Xóa thông tin khách hàng.
  - Cập nhập thông tin khách hàng.

  </details>
  <details>
  <summary>Quản lý phiếu đặt chỗ</summary>
  
  - Thêm mới phiếu đặt chỗ
  - Tra cứu phiếu đặt chỗ
  - Xóa phiếu đặt chỗ
  - Cập nhập phiếu đặt chỗ
  
  </details>
  <details>
  <summary>Quản lý bán vé</summary>
  
  - Thêm mới vé
  - Tra cứu vé
  - Cập nhập vé
  - Gửi vé điện tử
  - In vé

  </details>
   <details>
  <summary>Báo cáo thống kê</summary>
  
  - Báo cáo doanh thu chuyến đi trong năm hoặc tháng.
  - Báo cáo số lượng chuyến đi bị hủy và thành công, số lượng chuyến trong nước và nước ngoài trong năm hoặc tháng.
    
  </details>
  
## Công nghệ
* Ngôn ngữ: C#, TSQL.
* IDE: Visual Studio 2022.
* UI Framework: Windows Presentation Foundation (WPF).
* Database: Microsoft SQL Server.
* UI design: Adobe Illustrator.
* Thư viện: .NET Framework, MaterialDesignXAML, Entity Framework.
* Công cụ quản lý sourcecode: GitHub.

## Thành viên thực hiện
| STT | MSSV     | Họ và tên                                                  | Lớp      | 
| --- | -------- | ---------------------------------------------------------- | -------- | 
| 1   | 21520082 | [Lê Bảo Như](https://github.com/nhubaole)          | KTPM2021 | 
| 2   | 21522423 | [Huỳnh Ngọc Ý Nhi](https://github.com/Nhongnhong-0101)             | KTPM2021 | 
| 4   | 21521335 | [Phù Đức Quân](https://github.com/WuanDuc) | KTPM2021 | 
| 3   | 21522810 | [Huỳnh Thị Nhật Vy](https://github.com/vy-htn?fbclid=IwAR0-RBZlh_zqV2FkZu5jSgomYxCHmAoDJe1fK1YTXQszTBYwqiDPolrm-84) | KTPM2021 | 
*	Sinh viên khoa Công nghệ Phần mềm, trường Đại học Công nghệ Thông tin, Đại học Quốc gia thành phố Hồ Chí Minh.

## Giảng viên hướng dẫn
* Cô **Nguyễn Thị Thanh Trúc**, giảng viên khoa **Công Nghệ Phần Mềm**, trường Đại học Công nghệ Thông tin, Đại học Quốc gia Thành phố Hồ Chí Minh.

## Hướng dẫn cài đặt
- Cài đặt [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/).
- Cài đặt [SQL Sever](https://www.microsoft.com/en-us/sql-server/sql-server-downloads). Hướng dẫn cài đặt [tại đây](https://www.youtube.com/watch?v=c9NQLLboSeg).
- Download phần mềm tại [https://github.com/nhubaole/tourism-management-system.git](https://github.com/nhubaole/tourism-management-system.git)
  - Clone về với git.
  - Hoặc Download ZIP và giải nén.
- Tìm file **TourManagement.sql** và mở trong **Microsoft SQL Server Management Studio**. 
- Chọn tất cả nội dung file. Chọn **Execute** để khởi tạo database trên nhánh **master**.
- Mở file **TourismManagementSystem.sln**. Nhấn **Ctrl+F5** để chạy.
- Tài khoản mặc định:
  - Tên đăng nhập: admin
  - Mật khẩu: admin

## Hướng dẫn sử dụng
 <details>
  <summary>Màn hình đăng nhập</summary>
  
  - Nhập đầy đủ thông tin đăng nhập gồm **tên tài khoản** và **mật khẩu**
  - Ấn nút **ĐĂNG NHẬP** để đăng nhập vào tài khoản của mình
  - Nếu bạn là không có tài khoản, bạn có thể chọn **"Tiếp tục dưới vai trò khách"**
 ![Màn hình đăng nhập](https://i.imgur.com/1SeDk36.png)
 
  </details>
  <details>
  <summary>Màn hình trang chủ</summary>
  
  - Sau khi đăng nhập thành công sẽ được chuyển sang màn hình trang chủ
  - Tranh chủ sẽ hiện thống tin các tuyến, thống kê các chuyến , số vé bán ra, doanh thu theo **Ngày**, **Tháng**, **Năm**. 
  - Bạn có thể chọn các chức năng từ thanh công cụ.
 ![Màn hình trang chủ](https://i.imgur.com/QNNjBA1.png)

 
  </details>
   <details>
  <summary>Màn hình quản lý tuyến</summary>
  
  - Màn hình sẽ hiển thị danh sách các tuyến trong hệ thống
![Màn hình quản lý tuyến](https://i.imgur.com/x3UBA1v.png)
  - Bạn có thể thêm mới một tuyến khi ấn nút ![Nút thêm](https://i.imgur.com/4j2ay5Z.png)
  - Bạn có thể xem thông tin tuyến khi ấn nút ![Nút xem](https://i.imgur.com/tgaJF7D.png), chỉnh sửa ![Nút chỉnh sửa](https://i.imgur.com/ljHcAeV.png) hoặc xóa ![Nút xóa](https://i.imgur.com/YIjQafF.png)
  - **Lưu ý**: Khi bạn đăng nhập dưới vai trò khách, bạn chỉ có thể xem thông tin mà không có quyền chỉnh sử hoặc xóa.

  </details>
  
  <details>
  <summary>Màn hình quản lý chuyến</summary>
  - Màn hình sẽ hiển thị danh sách các chuyến trong hệ thống tương tự như màn hình quản lý tuyến.
  </details>
  
  <details>
  <summary>Màn hình quản lý địa điểm</summary>
  
  - Màn hình sẽ hiển thị danh sách các địa điểm trong hệ thống
![Màn hình quản lý địa điểm](https://i.imgur.com/ll4MVQL.png)
  - Bạn có thể thêm mới một địa điểm khi ấn nút ![Nút thêm](https://i.imgur.com/4j2ay5Z.png)
  - Bạn có thể xem thông tin địa điểm khi ấn nút ![Nút xem](https://i.imgur.com/tgaJF7D.png), chỉnh sửa ![Nút chỉnh sửa](https://i.imgur.com/ljHcAeV.png) hoặc xóa ![Nút xóa](https://i.imgur.com/YIjQafF.png)
  - **Lưu ý**: Khi bạn đăng nhập dưới vai trò khách, bạn chỉ có thể xem thông tin mà không có quyền chỉnh sử hoặc xóa.

  </details>
  
  <details>
  <summary>Màn hình quản lý dịch vụ</summary>
  - Màn hình sẽ hiển thị danh sách các dịch vụ trong hệ thống
![Màn hình quản lý dịch vụ]()
  - Bạn có thể xem thông tin dịch vụ khi ấn nút ![Nút xem](https://i.imgur.com/tgaJF7D.png), chỉnh sửa ![Nút chỉnh sửa](https://i.imgur.com/ljHcAeV.png) hoặc xóa ![Nút xóa](https://i.imgur.com/YIjQafF.png)
  - **Lưu ý**: Khi bạn đăng nhập dưới vai trò khách, bạn chỉ có thể xem thông tin mà không có quyền chỉnh sử hoặc xóa.

  </details>
  
  
  <details>
  <summary>Màn hình quản lý thông tin khách hàng</summary>
  
  
  </details>
  
  
  <details>
  <summary>Màn hình quản lý phiếu đặt chỗ</summary>
  </details>
  
  <details>
  <summary>Màn hình thông báo</summary>
 
 - Bạn có thể xem thông báo khi ấn vào biểu tượng ![Imgur](https://i.imgur.com/fvwRUOn.png)
 - Thông báo bao gồm những thông tin đã bị xóa, các chuyến đi sắp xuất phát nhưng chưa đủ số lượng hành khách quy định.
 ![Màn hình thông báo](https://i.imgur.com/C6Bcfbl.png)
  </details>
   
  <details>
  <summary>Màn hình thống kê doanh thu</summary>
 
 - Màn hình thống kê doanh thu theo **Tháng** hoặc **Năm**, bạn có thể tùy chọn tháng hoặc năm cụ thể.
 - **Lưu ý**: Bạn phải đăng nhập để sử dụng chức năng này.
 ![Màn hình thống kê daonh thu](https://i.imgur.com/TsAFPSK.png)
  </details>
  
  <details>
  <summary>Màn hình thống kê chuyến đi</summary>
 
 - Màn hính thống kê chuyến đi theo **Tháng** hoặc **Năm**, bạn có thể tùy chọn tháng hoặc năm cụ thể.
 - **Lưu ý**: Bạn phải đăng nhập để sử dụng chức năng này.
 ![Màn hình thống kê chuyến đi](https://i.imgur.com/3rLvfig.png)
  </details>
  </details>
  
  ## Phản hồi
* Mọi phản hồi của mọi người sau khi trải nghiệm hãy tạo ở mục Issues. Phản hồi của các bạn sẽ là động lực và lời khuyên quý giá giúp chúng tôi cải thiện ứng dụng tốt hơn. Cảm ơn mọi người đã dành thời gian trải nghiệm ứng dụng của chúng tôi.
