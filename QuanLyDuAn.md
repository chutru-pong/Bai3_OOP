# Quản lý nhóm dự án và nhân sự

## 1. Mô tả nghiệp vụ
Một đơn vị cần quản lý các nhóm dự án:
* Mỗi nhân sự có thể tồn tại độc lập với dự án và có thể được phân công vào nhiều dự án khác nhau.
* Có hai loại nhân sự: Nhân sự thông thường (`Employee`) và Kỹ sư phần mềm (`SoftwareEngineer` - có thêm ngôn ngữ lập trình chính và phụ cấp kỹ thuật).
* Mỗi nhóm có một trưởng nhóm và danh sách thành viên.
* Trưởng nhóm cũng là một nhân sự, nhưng không được xuất hiện lần thứ hai trong danh sách thành viên.


## 2. Bất biến của mô hình
Hệ thống cần duy trì ít nhất 5 bất biến:
1. Mã nhân sự không rỗng.
2. Lương cơ bản không âm.
3. Không có hai thành viên cùng mã trong một nhóm.
4. Trưởng nhóm phải thuộc danh sách nhân sự được phân công cho dự án.
5. Một nhân sự không xuất hiện hai lần trong cùng nhóm.


## 3. Phân tích và thiết kế chi tiết

### 3.1. Lớp `Employee`

**Thuộc tính:**
* `id`: Mã nhân sự.
* `fullName`: Họ tên.
* `baseSalary`: Lương cơ bản.

**Constructor:**
Cài đặt ba constructor với các giá trị mặc định (`id = "UNKNOWN"`, `fullName = "Unnamed employee"`, `baseSalary = 0`):
* `Employee()`
* `Employee(const std::string& id, const std::string& fullName)`
* `Employee(const std::string& id, const std::string& fullName, double baseSalary)`

**Ràng buộc:** Mã và họ tên không rỗng. Lương cơ bản không âm.

**Phương thức:**
* Nạp chồng phương thức tăng lương:
    * `void increaseSalary(double amount)`: Phiên bản thứ nhất tăng một số tiền cố định.
    * `void increaseSalary(double value, bool byPercentage)`: 
        * Nếu `byPercentage == true`: Tăng theo tỷ lệ phần trăm.
        * Nếu `byPercentage == false`: Tăng theo số tiền cố định.
        * Giá trị tăng phải dương.
* Các getter cần thiết.
* `virtual double calculateMonthlyCost()`: Mặc định bằng lương cơ bản.
* `virtual void displayInfo()`: Hiển thị thông tin chi tiết nhân sự.
* Destructor `virtual`: In thông báo quan sát vòng đời đối tượng.



### 3.2. Lớp `SoftwareEngineer`
*Kế thừa công khai từ `Employee`.*

**Thuộc tính bổ sung:**
* `primaryLanguage`: Ngôn ngữ lập trình chính.
* `technicalAllowance`: Phụ cấp kỹ thuật.

**Constructor:**
* `SoftwareEngineer(const std::string& id, const std::string& fullName, const std::string& primaryLanguage)`
* `SoftwareEngineer(const std::string& id, const std::string& fullName, double baseSalary, const std::string& primaryLanguage, double technicalAllowance)`

> **Ràng buộc:** Ngôn ngữ chính không rỗng. Phụ cấp không âm.

**Phương thức:**
* Ghi đè `calculateMonthlyCost()`: Trả về tổng lương cơ bản và phụ cấp kỹ thuật.
* Ghi đè `displayInfo()`: In thông tin chi tiết kỹ sư phần mềm.
* Destructor `virtual`: In thông báo quan sát vòng đời.

---

### 3.3. Lớp `ProjectTeam`

**Thuộc tính:**
* `projectCode`: Mã dự án.
* `projectName`: Tên dự án.
* Liên kết **không sở hữu** đến trưởng nhóm (`Employee*` / Aggregation).
* Danh sách liên kết **không sở hữu** đến các thành viên.

**Nạp chồng constructor:**
* `ProjectTeam(const std::string& projectCode, const std::string& projectName)`: Tạo nhóm chưa có trưởng nhóm.
* `ProjectTeam(const std::string& projectCode, const std::string& projectName, Employee& leader)`: Thiết lập trưởng nhóm và tự động đưa trưởng nhóm vào danh sách thành viên.

**Nạp chồng phương thức `addMember`:**
* `bool addMember(Employee& employee)`: Thêm thành viên (không thêm trùng nhân sự).
* `bool addMember(Employee& employee, bool makeLeader)`: 
    * Nếu `makeLeader == true`: Nhân sự được thêm và trở thành trưởng nhóm. Trưởng nhóm cũ vẫn là thành viên nếu đã có trong nhóm.

**Phương thức khác:**
* `bool removeMember(const std::string& employeeId)`
* `void changeLeader(Employee& employee)`
* `bool contains(const std::string& employeeId)`
* `double calculateTotalMonthlyCost()`
* `void displayTeam()`

> **Ràng buộc nghiệp vụ quan trọng:** 
> * Không được xóa trưởng nhóm khi chưa chọn trưởng nhóm thay thế.
> * Trưởng nhóm mới phải được thêm vào nhóm nếu chưa phải thành viên.
> * Destructor của `ProjectTeam` **chỉ hủy cấu trúc danh sách liên kết nội bộ**, không được hủy các đối tượng `Employee`.

## 4. Kịch bản kiểm thử (15 ca kiểm thử)
1. **Test 1**: Tạo 2 `Employee` bằng 2 constructor khác nhau.
2. **Test 2**: Tạo 2 `SoftwareEngineer` bằng 2 constructor khác nhau.
3. **Test 3**: Tăng lương cố định qua `IncreaseSalary(amount)`.
4. **Test 4**: Tăng lương theo phần trăm qua `IncreaseSalary(value, true)`.
5. **Test 5**: Khởi tạo nhóm dự án chưa có trưởng nhóm.
6. **Test 6**: Thêm nhân sự vào nhóm qua `AddMember(employee)`.
7. **Test 7**: Thêm nhân sự và gán làm trưởng nhóm qua `AddMember(employee, true)`.
8. **Test 8**: Kiểm tra không cho phép thêm thành viên đã tồn tại vào nhóm.
9. **Test 9**: Hiển thị danh sách nhân sự trong nhóm thông qua lời gọi đa hình (`DisplayTeam()`).
10. **Test 10**: Tính tổng chi phí nhân sự hàng tháng (`CalculateTotalMonthlyCost()`).
11. **Test 11**: Ràng buộc không thể xóa trưởng nhóm khi chưa đổi trưởng nhóm khác.
12. **Test 12**: Đổi trưởng nhóm và xóa người từng là trưởng nhóm.
13. **Test 13**: Phân công cùng một nhân sự vào nhiều dự án khác nhau.
14. **Test 14**: Hủy cấu trúc liên kết của nhóm bằng `DestroyTeam()`.
15. **Test 15**: Xác nhận đối tượng nhân sự vẫn tồn tại sau khi nhóm bị hủy (quan hệ Aggregation).


## 5. Hướng dẫn biên dịch và chạy chương trình

### Cách 1:
* Nhấn tổ hợp phím **`Ctrl + Shift + B`** .
* Chọn task **`Main Test`**. Hệ thống sẽ tự động biên dịch các file `.cs` thành `Program.exe` và thực thi ngay trên Terminal.

### Cách 2: Sử dụng dòng lệnh PowerShell / Command Prompt
Mở Terminal tại thư mục dự án (`d:\VS Code\OOP`) và chạy các lệnh sau:

1. **Biên dịch mã nguồn:**
   ```powershell
   C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /nologo /out:Program.exe Employee.cs SoftwareEngineer.cs ProjectTeam.cs test.cs
   ```
   *(Nếu `csc` đã có sẵn trong biến môi trường `PATH`, có thể dùng lệnh ngắn: `csc /nologo /out:Program.exe *.cs`)*

2. **Chạy file thực thi:**
   ```powershell
   .\Program.exe
   ```
