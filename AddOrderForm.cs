using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SantoGost
{
    public partial class AddOrderForm : Form
    {
        Database database = new Database();
        public AddOrderForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

        }

        private void AddOrder()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != ""  && textBox7.Text != "")
            {
            database.openConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            string querystring = $"INSERT INTO Order_Table(Code_Order, Date_Order_Open, Time_Order, Code_Client, Service_Id, Status_Id, Date_Order_Close, Time_Rent) VALUES ({textBox8.Text}, {textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text}, {textBox5.Text}, {textBox6.Text}, {textBox7.Text})";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());

            database.closeConnection();
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(table);

            database.closeConnection();
            }
        }
    }
}
