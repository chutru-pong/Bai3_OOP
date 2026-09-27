/*
MSSV: 202418964
Họ tên: Ngô Trường Phúc
*/

using System;

namespace ProjectManagement
{
    class Program
    {
        static void Check(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[PASS] ");
                Console.ResetColor();
                Console.WriteLine(message);
                Console.WriteLine("\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine(message);
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //1
            Employee emp1 = new Employee("E01", "Ngô Trường Phúc", 25000000);
            Employee emp2 = new Employee("E02", "Lâm Quý Đô");
            

            Check(emp1.Id =="E01" && emp1.FullName == "Ngô Trường Phúc" &&emp1.BaseSalary == 25000000 
            && emp2.Id =="E02" && emp2.FullName == "Lâm Quý Đô" && emp2.BaseSalary == 0, 
            "Test 1: Tạo hai Employee bằng hai constructor khác nhau");

            //2
            SoftwareEngineer dev1 = new SoftwareEngineer("SE01", "Hoàng Vũ Tùng", "C#");
            SoftwareEngineer dev2 = new SoftwareEngineer("SE02", "Nguyễn Thiện Nhân", 20000000, "C++", 5000000);
            
            Check(dev1.BaseSalary == 0 && dev1.TechnicalAllowance == 0 && dev2.BaseSalary == 20000000 && dev2.TechnicalAllowance == 5000000, 
            "Test 2: Tạo hai SoftwareEngineer bằng hai constructor khác nhau");

            // 3
            emp1.IncreaseSalary(200); 
            Check(emp1.BaseSalary == 25000200, "Test 3: Tăng lương cố định");

            // 4
            dev2.IncreaseSalary(10, true);
            Check(dev2.BaseSalary == 22000000, "Test 4: Tăng lương theo %");

            // 5
            ProjectTeam teamA = new ProjectTeam("P01", "Dự án Phóng mặt trăng lên tàu hỏa");
            Check(teamA != null && teamA.ProjectCode == "P01", "Test 5: Khởi tạo nhóm dự án không có nhóm trưởng");

            // 6
            bool addedDev2 = teamA.AddMember(dev2);
            Check(addedDev2 == true, "Test 6: Thêm nhân sự vào nhóm bằng addMember(employee)");

            // 7
            bool addedEmp1AsLeader = teamA.AddMember(emp1, true);
            Check(addedEmp1AsLeader == true, "Test 7: Thêm nhân sự làm trưởng nhóm bằng addMember(employee,true)");

            // 8
            bool reAddDev2 = teamA.AddMember(dev2);
            Check(reAddDev2 == false, "Test 8: Thêm thành viên đã tồn tại");

            // 9
            Console.WriteLine("Test 9: Hiển thị danh sách bằng lời gọi đa hình");
            teamA.DisplayTeam();

            //10
            double totalCost = teamA.CalculateTotalMonthlyCost();
            Check(totalCost == 52000200, "Test 10: Tính tổng chi phí nhân sự hàng tháng");

            // 11
            Console.WriteLine("\n[Test 11]");
            Check(teamA.RemoveMember("E01") == false, "Test 11: Không thể xóa trưởng nhóm");

            // 12
            teamA.ChangeLeader(dev1);
            bool removeOldLeaderSuccess = teamA.RemoveMember("E01");
            teamA.DisplayTeam();
            Check(removeOldLeaderSuccess == true, "Test 12: Đổi trưởng nhóm rồi xóa người từng là trưởng nhóm");

            // 13
            Console.WriteLine("\n[Test 13]");
            ProjectTeam teamB = new ProjectTeam("P02", "Dự án Chế tạo mặt trời");
            bool addedEmp2ToB = teamB.AddMember(emp2);
            Check(addedEmp2ToB == true, "Test 13: Thêm một nhân sự Lâm Quý Đô đã có ở nhóm thứ nhất thành công");

            // 14
            Console.WriteLine("\n[Test 14]");
            teamB.DisplayTeam();
            teamB.DestroyTeam();
            Check(teamB.Contains("E02") == false, "Test 14: Hủy nhóm thứ hai bằng DestroyTeam()");

            // 15
            Console.WriteLine("\n[Test 15]");
            emp2.DisplayInfo();
            Check(emp2 != null && emp2.BaseSalary == 0, "Test 15: nhân sự của nhóm thứ hai vẫn tồn tại sau khi nhóm bị hủy");
        }
    } 
}