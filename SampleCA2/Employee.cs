using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCA2
{
    public class Employee
    {
        #region properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        private static int _empCount;//counts number of instances of class

        public static int EmpCount
        {
            get { return _empCount; }
        }

        #endregion properties

        #region constructors

        public Employee(string fName, string lName, decimal salary)
        {
            FirstName = fName;
            LastName = lName;
            Salary = salary;

            _empCount++;//static class counter

        }

        public Employee(string fName, string lName) : this(fName, lName, 0) { }

        public Employee() : this("Unknown", "Unknown") { }

        #endregion constructors

        public override string ToString()
        {
            return string.Format("[{3}] {0} {1} Salary: {2:C}",
                FirstName, LastName, Salary, this.GetType().Name.ToUpper());
        }
    }//end of class

    
    public class PartTimer : Employee
    {
        #region properties
        public decimal HourlyRate { get; set; }
        public int Hours { get; set; }

        private static int _partTimerCount;

        public static int PartTimerCount
        {
            get { return _partTimerCount; }

        }
        #endregion properties

        #region constructors
        public PartTimer(string fName, string lName, decimal hourlyRate, int hours)
            : base(fName, lName)
        {
            HourlyRate = hourlyRate;
            Hours = hours;

            _partTimerCount++;
        }

        public PartTimer(string fName, string lName) : this(fName, lName, 0, 0) { }

        public PartTimer() : this("Unknown", "Unknown") { }
        #endregion constructors

        //override has different output to base class
        public override string ToString()
        {
            return string.Format("[{3}] {0} {1} Weekly Wages: {2:C}",
                FirstName, LastName, HourlyRate * Hours, this.GetType().Name.ToUpper());
        }
    }//end of class


    public class Contractor : PartTimer
    {
        #region properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private static int _contractorCount;

        public static int ContractorCount
        {
            get { return _contractorCount; }
        }

        #endregion properties

        #region constructors

        public Contractor(string fName, string lName, decimal rate, int hours, 
            DateTime startDate, DateTime endDate)
            : base(fName, lName, rate, hours)
        {
            StartDate = startDate;
            EndDate = endDate;

            _contractorCount++;

        }

        public Contractor(string fName, string lName, decimal rate, DateTime startDate)
            : this(fName, lName, rate, 0, startDate, DateTime.Now.AddYears(1)) { }

        public Contractor() : this("Unknown", "Unknown", 0, DateTime.Now) { }

        #endregion constructors

        //override for contractor has start and end date
        public override string ToString()
        {
            return base.ToString() 
                + string.Format(" Start Date: {0} End Date: {1}", StartDate.ToShortDateString(), EndDate.ToShortDateString());
        }

    }//end of class

}//end of namespace
