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
    public partial class Statistics : Form
    {
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter adapter = null;

        private DataTable table = null;

        private decimal income;

        private decimal primeCost;
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source = ANNADLUZHIN813D; Initial Catalog = CoffeeShop; Integrated Security = True");

            sqlConnection.Open();

            table = new DataTable();

            DataColumn dc1 = new DataColumn("Count", Type.GetType("System.Int32"));
            DataColumn dc2 = new DataColumn("Product", Type.GetType("System.String"));

            table.Columns.Add(dc1);
            table.Columns.Add(dc2);

            var sqlCmd = new SqlCommand("GetIncome", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    income = dr.GetDecimal(dr.GetOrdinal("Cost"));
                }
            }

            label4.Text = Convert.ToString(income);

            var sqlCmd1 = new SqlCommand("GetPrimeCost", sqlConnection);
            sqlCmd1.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader dr = sqlCmd1.ExecuteReader())
            {
                while (dr.Read())
                {
                    primeCost = dr.GetDecimal(dr.GetOrdinal("Cost"));
                }
            }

            label5.Text = Convert.ToString(primeCost);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CoffeeShop fr1 = new CoffeeShop();
            fr1.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            table.Clear();

            var sqlCmd = new SqlCommand("GetStatistics", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;


            using (SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int count = dr.GetInt32(dr.GetOrdinal("Count"));
                    string NameColumn = dr.GetString(dr.GetOrdinal("ProductName"));
                    DataRow row = table.NewRow();
                    row[1] = NameColumn;
                    row[0] = count;
                    table.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Clear();

            var sqlCmd = new SqlCommand("GetTop3", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;


            using (SqlDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int count = dr.GetInt32(dr.GetOrdinal("Count"));
                    string NameColumn = dr.GetString(dr.GetOrdinal("ProductName"));
                    DataRow row = table.NewRow();
                    row[1] = NameColumn;
                    row[0] = count;
                    table.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = table;
        }
    }
}
