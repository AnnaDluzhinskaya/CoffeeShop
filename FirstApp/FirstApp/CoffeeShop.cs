using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstApp
{
    public partial class CoffeeShop : Form
    {
        public CoffeeShop()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct fr1 = new AddProduct();
            fr1.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddRecipe fr1 = new AddRecipe();
            fr1.Show();
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddIngredient fr1 = new AddIngredient();
            fr1.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddOrder fr1 = new AddOrder();
            fr1.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Statistics fr1 = new Statistics();
            fr1.Show();
            Hide();
        }
    }


}
