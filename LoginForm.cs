using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SantoGost
{
    public partial class LoginForm : Form
    {
        public static bool admin = false;
        public static bool stSmen = false;
        public static bool prodavec = false;
        public static int post;
        public string capchaValue = "";
        Database database = new Database();
        public LoginForm()
        {
            InitializeComponent();
            CapchaLoad();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '*';
            pictureBox1.Visible = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var loginUser = textBoxLogin.Text;
            var passwordUser = textBoxPassword.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select Login, Password, Post from Users_Table where Login = '{loginUser}' and Password = '{passwordUser}'";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                post = dataReader.GetInt32(2);
            }
            database.closeConnection();
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(table);

            if(table.Rows.Count == 1 && capchaValue == CapchaTextBox.Text)
            {
                if (post == 1)
                {
                    stSmen = true;
                }
                else if(post == 2)
                {
                    admin = true;
                }
                else if (post == 3)
                {
                    prodavec = true;
                }
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Show();
            }           
            else
            {
                MessageBox.Show("Ошибка, попробуйте еще раз!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            CapchaLoad();
            CapchapictureBox.ResetText();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }

        private void CapchaLoad()
        {
            capchaValue = "";
            var rnd = new Random();
            Bitmap image = new Bitmap(CapchapictureBox.Width, CapchapictureBox.Height);
            var font = new Font("TimesNewRoman", 40, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            CapchapictureBox.Image = image;
            for (int i = 0; i < 6; i++)
            {
                int value = rnd.Next(0, 9);
                capchaValue += value.ToString();
                graphics.DrawString(value.ToString(), font, Brushes.Azure, new Point(4 + i * 40, value * 2 + CapchapictureBox.Height / 5));
                CapchapictureBox.Refresh();
            }

            var colors = new Color[]
            {
                Color.Black,
                Color.Red,
                Color.Green
            };


            for (int n = 0; n < CapchapictureBox.Width; n++)
            {
                for (int j = 0; j < CapchapictureBox.Height; j++)
                {
                    if (rnd.Next() % 8 == 0)
                        image.SetPixel(n, j, colors[j % 3]);
                }
            }
            CapchapictureBox.Refresh();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            CapchaLoad();
        }
    }
}
