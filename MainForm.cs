using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;

namespace SantoGost
{
    public partial class MainForm : Form
    {
        Database database = new Database();
        public MainForm()
        {
            InitializeComponent();
            Dostup();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {           
            database.openConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();

            string querystring = $"select * from Service_Table";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());


            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(table);
            dataGridView1.DataSource = table;


            querystring = $"select * from Order_Table";
            command = new SqlCommand(querystring, database.GetConnection());

            database.closeConnection();
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(table2);

            dataGridView2.DataSource = table2;
            database.closeConnection();
        }

        private void Dostup()
        {
            OtchetButton.Visible = false;
            ControlButton.Visible = false;

            if (LoginForm.admin == true)
            {
                PostLabel.Text = "Ваша дожность: Администратор";
                OtchetButton.Visible = true;
                ControlButton.Visible = true;
            }
            else if(LoginForm.stSmen == true)
            {
                PostLabel.Text = "Ваша дожность: Старший смены";
            }
            else if (LoginForm.prodavec == true)
            {
                PostLabel.Text = "Ваша дожность: Продавец";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddOrderForm addOrderForm = new AddOrderForm();
            this.Hide();
            addOrderForm.ShowDialog();
            this.Show();
        }
    }
}
