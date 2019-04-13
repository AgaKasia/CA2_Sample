using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleCA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Lists to store employees
        List<Employee> employees = new List<Employee>();
        List<Employee> filteredList = new List<Employee>(); //filter used to show sub set


        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            filteredList.Clear();
            
            //determine which radio button was checked
            RadioButton selectedRadioButton = sender as RadioButton;
            string selected = selectedRadioButton.Tag.ToString();//will get one of the tags - All, Emp, PT, Cont

            //filter based on this
            if (selected.Equals("All"))
                lbxEmployees.ItemsSource = employees;
            else
                lbxEmployees.ItemsSource = 
                    employees.Where(emp => emp.GetType().Name.Equals(selected));  //this in Lambda syntax using LINQ which is not expected in test

            
            //other way to write this
            //else
            //{
            //    if (selected.Equals("Employee"))
            //    {
            //        foreach (Employee emp in employees)
            //        {
            //            if (emp is Employee && !(emp is PartTimer) && !(emp is Contractor))
            //                filteredList.Add(emp);
            //        }

            //    }

            //    else if (selected.Equals("PartTimer"))
            //    {
            //        foreach (Employee emp in employees)
            //        {
            //            if (emp is PartTimer && !(emp is Contractor))
            //                filteredList.Add(emp);
            //        }
            //        lbxEmployees.ItemsSource = filteredList;
            //    }

            //    else if (selected.Equals("Contractor"))
            //    {
            //        foreach (Employee emp in employees)
            //        {
            //            if (emp is Contractor)
            //                filteredList.Add(emp);
            //        }
            //        lbxEmployees.ItemsSource = filteredList;
            //    }

            //    lbxEmployees.ItemsSource = null;
            //    lbxEmployees.ItemsSource = filteredList;

            //}//end of else

        }//end of method

        //method to load data and populate listbox
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetRandomEmployees();
            lbxEmployees.ItemsSource = employees;
            SetEmployeeCount();
        }

        //updates display for counting employees
        private void SetEmployeeCount()
        {
            tblkContractorCount.Text = Contractor.ContractorCount.ToString();
            tblkPartTimeCount.Text = (PartTimer.PartTimerCount - Contractor.ContractorCount).ToString();
            tblkEmployeeCount.Text = (Employee.EmpCount - PartTimer.PartTimerCount).ToString();
            //tblkEmployeeCount.Text = Employee.EmpCount.ToString();

        }

        //generate 20 employees randomly
        private void GetRandomEmployees()
        {
            //setup
            string[] firstNames = { "MARY", "PATRICIA", "LINDA", "BARBARA", "ELIZABETH", "JENNIFER", "MARIA", "SUSAN", "MARGARET", "DOROTHY", "LISA", "JAMES", "JOHN", "ROBERT", "MICHAEL", "WILLIAM", "DAVID", "RICHARD", "CHARLES", "JOSEPH", "THOMAS", };
            string[] lastNames = { "SMITH", "JOHNSON", "WILLIAMS", "JONES", "BROWN", "DAVIS", "MILLER", "WILSON", "MOORE", "TAYLOR", "ANDERSON", "THOMAS", "JACKSON", "WHITE", "HARRIS", "MARTIN", "THOMPSON", "ROBINSON", "CLARK", "LEWIS", "LEE", };

            //Create a random object
            Random rand = new Random();

            //Loop to create 20 Employees
            for (int i = 0; i < 20; i++)
            {
                //Randomly create 1 of 3 types of employee
                int type = rand.Next(0, 3);

                //Take names from array
                string fName = firstNames[rand.Next(0, firstNames.Length)];
                string lName = lastNames[rand.Next(0, lastNames.Length)];

                switch (type)
                {
                    case 0:
                        Employee e = new Employee()
                        {
                            FirstName = fName,
                            LastName = lName,
                            Salary = Convert.ToDecimal(rand.Next(20000, 40001))
                        };

                        employees.Add(e);
                        break;

                    //Create part time object
                    case 1:
                        PartTimer p = new PartTimer()
                        {
                            FirstName = fName,
                            LastName = lName,
                            HourlyRate = Convert.ToDecimal(rand.Next(10, 41)),
                            Hours = rand.Next(10, 21)
                        };
                        
                        employees.Add(p);
                        break;

                    case 2:
                        Contractor c = new Contractor()
                        {
                            FirstName = fName,
                            LastName = lName,
                            HourlyRate = Convert.ToDecimal(rand.Next(10, 41)),
                            Hours = rand.Next(10, 21),
                            StartDate = DateTime.Now.AddMonths(-rand.Next(13)),//past 12 months
                            EndDate = DateTime.Now.AddDays(rand.Next(13))//next 12 months

                        };
                        employees.Add(c);
                        break;

                }//end switch




            }//end for
        }//end method
    }//end class
}//end namespace
