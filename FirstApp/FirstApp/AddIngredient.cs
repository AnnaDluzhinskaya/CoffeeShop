﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FirstApp
{
    public partial class AddIngredient : Form
    {


        private SqlConnection sqlConnection = null;

        private SqlDataAdapter adapter = null;

        private DataTable table = null;

        public AddIngredient()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double s = Convert.ToDouble(textBox2.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Пожалуйста,введите цифрy!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CoffeeShop fr1 = new CoffeeShop();
            fr1.Show();
            Hide();
        }

        private void AddIngredient_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source = ANNADLUZHIN813D; Initial Catalog = CoffeeShop; Integrated Security = True");

            sqlConnection.Open();

            adapter = new SqlDataAdapter("select IngredientName as Name, IngredientPerUnit as Cost from Ingredients", sqlConnection);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Clear();

            String name = textBox1.Text;
            String price = textBox2.Text;

            var sqlCmd = new SqlCommand("AddIngredient", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Name", name);
            sqlCmd.Parameters.AddWithValue("@CostPerUnit", Double.Parse(price));
            sqlCmd.ExecuteNonQuery();

            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }
    }
}
