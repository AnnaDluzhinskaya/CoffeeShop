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
    public partial class AddOrder : Form
    {
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter adapter = null;

        private DataTable table = null;

        private int index;

        public AddOrder()
        {
            InitializeComponent();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coffeeShopDataSet.Products". При необходимости она может быть перемещена или удалена.
            this.productsTableAdapter.Fill(this.coffeeShopDataSet.Products);

            sqlConnection = new SqlConnection(@"Data Source = ANNADLUZHIN813D; Initial Catalog = CoffeeShop; Integrated Security = True");

            sqlConnection.Open();

            table = new DataTable();

            DataColumn dc1 = new DataColumn("Product", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("Price", Type.GetType("System.Double"));

            table.Columns.Add(dc1);
            table.Columns.Add(dc2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            table.Clear();

            var sqlCmd = new SqlCommand("ZeroOrder", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;


            using (SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    index = dr.GetInt32(dr.GetOrdinal("OrderId"));
                }
            }
                dataGridView1.DataSource = table;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CoffeeShop fr1 = new CoffeeShop();
            fr1.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Clear();
            string id = comboBox1.SelectedValue.ToString();

            var sqlCmd = new SqlCommand("AddOrder", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ProductId", Int32.Parse(id));
            sqlCmd.Parameters.AddWithValue("@OrderId", index);
            sqlCmd.ExecuteNonQuery();

            var sqlCmd1 = new SqlCommand("ShowOrder", sqlConnection);
            sqlCmd1.CommandType = CommandType.StoredProcedure;
            sqlCmd1.Parameters.AddWithValue("@OrderId", index);

            using (SqlDataReader dr = sqlCmd1.ExecuteReader())
            {
                while (dr.Read())
                {
                    string NameColumn = dr.GetString(dr.GetOrdinal("ProductName"));
                    decimal Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                    DataRow row = table.NewRow();
                    row[0] = NameColumn;
                    row[1] = Convert.ToDouble(Price);
                    table.Rows.Add(row);
                }
            }
                dataGridView1.DataSource = table;
            
        }
    }
}
