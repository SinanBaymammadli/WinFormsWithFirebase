using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace WindowsFormsApp2
{
    public partial class ProductEdit : Form
    {
        string productId;

        public ProductEdit(string id)
        {
            productId = id;
            InitializeComponent();
        }

        FirebaseClient firebase = new FirebaseClient("https://csharp-450b3.firebaseio.com/");

        private async void ProductEdit_Load(object sender, EventArgs e)
        {
            var product = await firebase
                    .Child("products")
                    .Child(productId)
                    .OnceSingleAsync<Product>();

            textBox1.Text = product.Title;
            textBox2.Text = product.Price;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            string price = textBox2.Text;

            try
            {
                await firebase
                    .Child("products")
                    .Child(productId)
                    .PutAsync(new Product { Title = title, Price = price });

                textBox1.Text = "";
                textBox2.Text = "";

                this.Hide();
            } catch
            {

            }
            
        }
    }
}
