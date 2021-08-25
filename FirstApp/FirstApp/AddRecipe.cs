using System;
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
    public partial class AddRecipe : Form
    {
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter adapter = null;

        private DataTable table = null;

        public AddRecipe()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CoffeeShop fr1 = new CoffeeShop();
            fr1.Show();
            Hide();
        }

        private void AddRecipe_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coffeeShopDataSet.Ingredients". При необходимости она может быть перемещена или удалена.
            this.ingredientsTableAdapter.Fill(this.coffeeShopDataSet.Ingredients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coffeeShopDataSet.Products". При необходимости она может быть перемещена или удалена.
            this.productsTableAdapter.Fill(this.coffeeShopDataSet.Products);

            sqlConnection = new SqlConnection(@"Data Source = ANNADLUZHIN813D; Initial Catalog = CoffeeShop; Integrated Security = True");

            sqlConnection.Open();

            table = new DataTable();

            DataColumn dc1 = new DataColumn("Ingredient", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("Quantity", Type.GetType("System.Int32"));

            table.Columns.Add(dc1);
            table.Columns.Add(dc2);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            table.Clear();
            String index = comboBox1.SelectedValue.ToString();

            var sqlCmd = new SqlCommand("GetRecipe", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ProductId", Int32.Parse(index));


            using(SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string NameColumn = dr.GetString(dr.GetOrdinal("Ingredient"));
                    int Quantity = dr.GetInt32(dr.GetOrdinal("Quantity"));
                    DataRow row = table.NewRow();
                    row[0] = NameColumn;
                    row[1] = Quantity;
                    table.Rows.Add(row);
                }
            }
            
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Clear();
            String index1 = comboBox1.SelectedValue.ToString();
            String index2 = comboBox2.SelectedValue.ToString();
            String qua = textBox2.Text;

            var sqlCmd1 = new SqlCommand("AddRecipe", sqlConnection);
            sqlCmd1.CommandType = CommandType.StoredProcedure;
            sqlCmd1.Parameters.AddWithValue("@ProductId", Int32.Parse(index1));
            sqlCmd1.Parameters.AddWithValue("@IngredientId", Int32.Parse(index2));
            sqlCmd1.Parameters.AddWithValue("@Quantity", Double.Parse(qua));
            sqlCmd1.ExecuteNonQuery();


            var sqlCmd = new SqlCommand("GetRecipe", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ProductId", Int32.Parse(index1));


            using (SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string NameColumn = dr.GetString(dr.GetOrdinal("Ingredient"));
                    int Quantity = dr.GetInt32(dr.GetOrdinal("Quantity"));
                    DataRow row = table.NewRow();
                    row[0] = NameColumn;
                    row[1] = Quantity;
                    table.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = table;
        }
    }
}
