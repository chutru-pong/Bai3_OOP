using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement
{

    public class ProjectTeam
    {
        public string ProjectCode { get; private set; }
        public string ProjectName { get; private set; }
        
        private Employee leader;
        private List<Employee> members;


        //chưa có trưởng nhóm
        public ProjectTeam(string projectCode, string projectName)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            members = new List<Employee>();
        }

        //Có trưởng nhóm
        public ProjectTeam(string projectCode, string projectName, Employee leader) : this(projectCode, projectName) //tái sử dụng construct 1
        {
            AddMember(leader, true);
        }

        public bool Contains(string employeeId)
        {
            return members.Any(m => m.Id == employeeId);
        }

        //thành viên không là nhóm trưởng
        public bool AddMember(Employee employee)
        {
            if (Contains(employee.Id))
            {
                return false;
            }
            members.Add(employee);
            return true;
        }

        //thành viên là nhóm trưởng
        public bool AddMember(Employee employee, bool makeLeader)
        {
            bool added = true;
            if (!Contains(employee.Id))
            {
                added = AddMember(employee);
            }

            if (added && makeLeader)
            {
                leader = employee;
            }
            return added;
        }

        public bool RemoveMember(string employeeId)
        {
            if (leader != null && leader.Id == employeeId)
            {
                Console.WriteLine("Không thể xóa trưởng nhóm {0}", employeeId);
                return false;
            }

            var emp = members.FirstOrDefault(m => m.Id == employeeId);
            if (emp != null)
            {
                members.Remove(emp);
                Console.WriteLine("Xóa nhân sự {0} khỏi nhóm {1}.", employeeId, ProjectName);
                return true;
            }
            return false;
        }

        public void ChangeLeader(Employee newLeader)
        {
            if (!Contains(newLeader.Id))
            {
                AddMember(newLeader);
            }
            leader = newLeader;
            Console.WriteLine(" {0} thành trưởng nhóm {1}.", newLeader.FullName, ProjectName);
        }

        public double CalculateTotalMonthlyCost()
        {
            return members.Sum(m => m.CalculateMonthlyCost());
        }

        public void DisplayTeam()
        {
            Console.WriteLine("\nDỰ ÁN: {0} ({1})", ProjectName, ProjectCode);
            Console.WriteLine("Trưởng nhóm: {0}", leader != null ? leader.FullName : "Chưa có");
            Console.WriteLine("Số lượng thành viên: {0}", members.Count);
            Console.WriteLine("Danh sách thành viên:");
            foreach (var m in members)
            {
                m.DisplayInfo();
            }
            Console.WriteLine("Tổng chi phí hằng tháng: {0:N0}", CalculateTotalMonthlyCost());
            Console.WriteLine("\n");
        }

        // Destructor hủy cấu trúc danh sách liên kết nội bộ, không hủy các đối tượng Employee
        public void DestroyTeam()
        {
            members.Clear();
            leader = null;
            Console.WriteLine("Đã xóa danh sách liên kết nội bộ của dự án {0}.", ProjectName);
        }
    }
}
