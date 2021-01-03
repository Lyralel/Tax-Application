using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Tax_Application
{
    public partial class frmMain : Form
    {

        Employee[] employees = new Employee[20];
        int count = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        //Click event handles printing out Employee data onto txtDisplay
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            txtDisplay.Text = "";

            for (int i = 0; i < count; i++)
            {
                txtDisplay.Text += "Name: " + employees[i].FirstName + ", " + employees[i].LastName + "\r\n\tGross Pay: " + (employees[i].grossPay.ToString("C2")) + "\r\n\tNet Pay: " + (employees[i].netPay().ToString("C2")) +
                "\r\n\tSS Withheld: " + employees[i].SSWithheld().ToString("C2") + "\r\n\tMedicare Withheld: " + employees[i].MedicareWithheld().ToString("C2") + "\r\n\tSIT Withheld: " + employees[i].StateInTax().ToString("C2") + "\r\n\tFIT Withheld: " + employees[i].FedInTax().ToString("C2") + "\r\n";
            }

        }

        //Click event handles processing data entered and turning it into a Employee object
        private void btnEnter_Click(object sender, EventArgs e)
        {

            double test;
            int test1;
            //Checks if employee array count has exceeded 20
            if (count <= 19)
            {
                //if statement for making sure correct data is entered
                if (double.TryParse(txtHourlyRate.Text, out test) && int.TryParse(txtHoursWorked.Text, out test1) && !(txtFirstName.Text.Equals("")) && !(txtLastName.Text.Equals("")))
                {

                    lstEmployees.Items.Add(txtFirstName.Text + ", " + txtLastName.Text);
                    employees[count] = new Employee(txtFirstName.Text, txtLastName.Text, double.Parse(txtHourlyRate.Text), int.Parse(txtHoursWorked.Text));
                    ++count;

                    txtFirstName.Text = "";
                    txtHourlyRate.Text = "";
                    txtHoursWorked.Text = "";
                    txtLastName.Text = "";

                }
                else
                {
                    //Outputs message depending on what data was entered incorrectly
                    if (!(double.TryParse(txtHourlyRate.Text, out test)))
                    {
                        MessageBox.Show("Hourly Rate field only excepts number or decimal values");
                    }
                    if (!(int.TryParse(txtHourlyRate.Text, out test1)))
                    {
                        MessageBox.Show("Hours Worked field only excepts whole number values");
                    }
                    if (txtFirstName.Text.Equals(""))
                    {
                        MessageBox.Show("Please enter Employee's first name");
                    }
                    if (txtLastName.Text.Equals(""))
                    {
                        MessageBox.Show("Please enter Employee's last name");
                    }

                }

            }
            else
            {
                //Message for indicating that the Employee array is full
                MessageBox.Show("Maximum number of employees entered");
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }

    public class Employee
    {

        //initializing class variables
        private string firstName;
        private string lastName;
        private double hourlyRate;
        private int hoursWorked;
        public double grossPay;

        //constructor for creating employee object
        public Employee(string firstName, string lastName, double hourlyRate, int hoursWorked)
        {

            this.firstName = firstName;
            this.lastName = lastName;
            this.hourlyRate = hourlyRate;
            this.hoursWorked = hoursWorked;
            grossPay = hoursWorked * hourlyRate;

        }

        //outputs the Net Pay employee has earned
        public double netPay()
        {
            return grossPay - this.SSWithheld() - this.MedicareWithheld() - this.StateInTax() - this.FedInTax();
        }

        //outputs the Sociali Security tax withheld
        public double SSWithheld()
        {
            return grossPay * .062;
        }

        //outputs Medicare tax withheld
        public double MedicareWithheld()
        {
            return grossPay * .0145;
        }

        //Method for returning state income tax for employee depending on amount earned
        public double StateInTax()
        {

            if (0 < grossPay && grossPay <= 500)
            {
                return (grossPay * .02);
            }
            else if (500 <= grossPay && grossPay <= 999.99)
            {
                return grossPay * .04;
            }
            else if (1000 <= grossPay)
            {
                return grossPay * .06;
            }
            else
            {
                return 0;
            }

        }

        //returns federal income tax using a series of if statements selecting the correct percentage to calculate federal income tax 
        public double FedInTax()
        {

            if (0 < grossPay && grossPay < 500)
            {
                return (grossPay * .05);
            }
            else if (500 <= grossPay && grossPay <= 999.99)
            {
                return grossPay * .10;
            }
            else if (1000 <= grossPay && grossPay <= 1499.99)
            {
                return grossPay * .15;
            }
            else if (1500 <= grossPay && grossPay <= 1999.99)
            {
                return (grossPay * .20);
            }
            else if (2000 <= grossPay && grossPay <= 2999.99)
            {
                return (grossPay * .25);
            }
            else if (3000 <= grossPay)
            {
                return (grossPay * .30);
            }
            else
            {
                return 0;
            }

        }

        public string FirstName
        {

            get
            {
                return firstName;
            }

        }

        public string LastName
        {

            get
            {
                return lastName;
            }

        }


    }

}
