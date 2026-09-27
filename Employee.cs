using System;

namespace ProjectManagement
{
    public class Employee
    {
        //Thuộc tính
        public string Id { get; protected set; }
        public string FullName { get; protected set; }
        public double BaseSalary { get; protected set; }

        // Constructor
        public Employee() : this("UNKNOWN", "Unnamed employee", 0) 
        { 
        }

        public Employee(string id, string fullName) : this(id, fullName, 0) 
        { 
        }

        public Employee(string id, string fullName, double baseSalary)
        {
            if (id == null || id == "")
                throw new ArgumentException("Mã không rỗng"); // truyền thông báo
            if (fullName == null || fullName == "")
                throw new ArgumentException("Họ tên không rỗng");
            if (baseSalary < 0)
                throw new ArgumentException("Lương cơ bản không âm");

            Id = id;
            FullName = fullName;
            BaseSalary = baseSalary;
        }


        //tăng lương 1
        public void IncreaseSalary(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền tăng là dương");
            BaseSalary += amount;
        }

        //tăng lương 2
        public void IncreaseSalary(double value, bool byPercentage)
        {
            if (value <= 0)
                throw new ArgumentException("Giá trị tăng là dương");

            if (byPercentage == true)
                BaseSalary += BaseSalary * (value * 1.0 / 100);
            else
                BaseSalary += value;
        }


        public virtual double CalculateMonthlyCost()
        {
            return BaseSalary;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("ID: {0}, Tên: {1}, Lương CB: {2:N0}", Id, FullName, BaseSalary);
        }

        // Destructor
        ~Employee()
        {
        }
    }

    
}

