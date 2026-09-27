using System;

namespace ProjectManagement
{
    public class SoftwareEngineer : Employee
    {
        public string PrimaryLanguage { get; private set; }
        public double TechnicalAllowance { get; private set; }


        // Constructor 
        public SoftwareEngineer(string id, string fullName, string primaryLanguage) 
            : this(id, fullName, 0, primaryLanguage, 0) //(id, name,baseSalary, primaryLanguage, technicalAllowance)
        {
        }
        public SoftwareEngineer(string id, string fullName, double baseSalary, string primaryLanguage, double technicalAllowance) 
            : base(id, fullName, baseSalary) //base gọi đến constructor lớp cha
        {
            if (primaryLanguage == null || primaryLanguage == "")
                throw new ArgumentException("Ngôn ngữ lập trình không rỗng");
            if (technicalAllowance < 0)
                throw new ArgumentException("Phụ cấp kỹ thuật không âm");

            PrimaryLanguage = primaryLanguage;
            TechnicalAllowance = technicalAllowance;
        }

        // Ghi đè
        public override double CalculateMonthlyCost()
        {
            return BaseSalary + TechnicalAllowance;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("Software Engineer ID: {0}, Tên: {1}, Lương CB: {2:N0}, Ngôn ngữ: {3}, Phụ cấp: {4:N0}, Tổng chi phí: {5:N0}",
                Id, FullName, BaseSalary, PrimaryLanguage, TechnicalAllowance, CalculateMonthlyCost());
        }

        ~SoftwareEngineer()
        {     
        }
    }
}
