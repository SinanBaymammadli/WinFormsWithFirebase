using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;


namespace WindowsFormsApp2
{
    public partial class Guest : Form
    {
        public Guest()
        {
            InitializeComponent();
        }

        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDLaddthXCTCVVM4RlKBDAuAT_eQM16yIc"));

        private void Form1_Load(object sender, EventArgs e)
        {
            centerPanel(panel1);
            label9.Visible = false;
            label10.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;

            centerPanel(panel2);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;

            centerPanel(panel1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                FirebaseAuthLink authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                openDashboard();
            }
            catch (FirebaseAuthException ex)
            {
                label9.Text = ex.Reason.ToString();
                label9.Visible = true;
            }
        }

        private void openDashboard()
        {
            this.Hide();
            var form2 = new Dashboard();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string email = textBox4.Text;
            string password = textBox3.Text;

            try
            {
                FirebaseAuthLink authLink = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                openDashboard();
            }
            catch (FirebaseAuthException ex)
            {
                label10.Text = ex.Reason.ToString();
                label10.Visible = true;
            }
        }

        private void centerPanel(Panel panel)
        {
            panel.Location = new Point(
                this.ClientSize.Width / 2 - panel.Size.Width / 2,
                this.ClientSize.Height / 2 - panel.Size.Height / 2);
        }
    }
}
