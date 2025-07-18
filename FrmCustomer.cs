using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerProject
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Server=.;Initial Catalog=DbCustomer;Integrated Security=True");
        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName From TblCustomer Inner Join TblCity on TblCity.CityId=TblCustomer.CustomerCity", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblCustomer (CustomerName,CustomerSurname,CustomerCity,CustomerBalance,CustomerStatus) values (@customerName,@CustomerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);
            command.Parameters.AddWithValue("customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("customerBalance", decimal.Parse(txtCustomerBalance.Text));
            if(rdbActive.Checked)
            {
                command.Parameters.AddWithValue("customerStatus", true);
            }
            else
            {
                command.Parameters.AddWithValue("customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Ekleme İşlemi Başarılı.","Uyarı!",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Execute CustomerListWithCity", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From TblCity", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            cmbCity.ValueMember = "CityId";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource = dataTable;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCustomer Where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("customerId", int.Parse(txtCustomerId.Text));
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Silme İşlemi Başarılı.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblCustomer Set CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity,CustomerBalance=@customerBalance,CustomerStatus=@customerStatus Where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("customerBalance", decimal.Parse(txtCustomerBalance.Text)); 
            command.Parameters.AddWithValue("customerId", int.Parse(txtCustomerId.Text));
            command.Parameters.AddWithValue("customerStatus", rdbActive.Checked ? true : false);
            //if(rdbActive.Checked){
            //command.Parameters.AddWithValue("customerStatus", true);
            //}
            //if(rdbPassive.Checked){
            //command.Parameters.AddWithValue("customerStatus", false);
            //}
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Güncelleme İşlemi Başarılı.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM TblCustomer WHERE CustomerName=@customerName", sqlConnection);
            command.Parameters.AddWithValue("customerName", txtCustomerName.Text);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();

        }
    }
}
