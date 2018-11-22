using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace WindowsFormsApp2
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        FirebaseClient firebase = new FirebaseClient("https://csharp-450b3.firebaseio.com/");

        private void Dashboard_Load(object sender, EventArgs e)
        {
            refreshGrid();
        }

        private void refreshGrid()
        {
            firebase
                .Child("products")
                .AsObservable<Product>()
                .Subscribe(product => Invoke(new Action(() =>
                    {
                        if(product.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                        {
                            // insert
                            dataGridView1.Rows.Add(product.Key, product.Object.Title, product.Object.Price);


                            /*foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    if (row.Cells["Id"].Value.ToString() == product.Key)
                                    {
                                        // update
                                        row.Cells["Title"].Value = product.Object.Title;
                                        row.Cells["Price"].Value = product.Object.Price;
                                    } else
                                    {
                                        // insert
                                        dataGridView1.Rows.Add(product.Key, product.Object.Title, product.Object.Price);
                                    }
                                }*/
                            
                        } else
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        }
                    }
                )));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openProductCreate();
        }

        private void openProductCreate()
        {
            //this.Hide();
            var form = new ProductCreate();
            //form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                string id = row.Cells["id"].Value.ToString();

                await firebase
                    .Child("products")
                    .Child(id)
                    .DeleteAsync();
            } else
            {
                MessageBox.Show("You have to select 1 row.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                string id = row.Cells["id"].Value.ToString();
                //this.Hide();
                var form = new ProductEdit(id);
                //form.Closed += (s, args) => this.Close();
                form.Show();
            }
            else
            {
                MessageBox.Show("You have to select 1 row.");
            }
        }
    }
}
