using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace Management.Class
{
    class Function
    {
        public static SqlConnection Con;
        public static void Connect()
        {
            Con = new SqlConnection();
            Con.ConnectionString = "Data Source=LAPTOP-JU140V24;Initial Catalog=DoAnCuoiKii;User ID=sa;Password=123";

            if (Con.State != ConnectionState.Open)
            {
                Con.Open();
                MessageBox.Show("Connect Successfull");
            }
            else
            {
                MessageBox.Show("Connect fail");
            }
            
        }
        public static void Disconnect()
        {

            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
                Con.Dispose();
                Con = null;

            }

        }
        public static DataTable GetDataToTable(string sql)
        {
            DataTable table = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sql,Con);
            dap.Fill(table);
            return table;

        }
        public static void RunSQL(string sql)
        {
            SqlCommand cmd; 
            cmd = new SqlCommand();
            cmd.Connection = Con; 
            cmd.CommandText = sql; 
            try
            {
                cmd.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql,Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql,Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
        

    }
}
