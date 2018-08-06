using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Mvvm;

namespace ACWPF
{
    class ModelForMainWin : BindableBase
    {
        private int requestID;
        private string department;
        private string cartridge;
        private string inventaryID;
        private string requestDate;
        private string status;

        public int RequestID
        {
            get { return requestID; }
            set { requestID = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string Cartridge
        {
            get { return cartridge; }
            set { cartridge = value; }
        }
        public string InventaryID
        {
            get { return inventaryID; }
            set { inventaryID = value; }
        }
        public string RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public async void InventaryCheck(string inv)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            InventaryID = inv;
            SqlDataReader sqlReader = null;
            string str = String.Format("SELECT InventaryNumber, Cartridge, DeliveryDate FROM [Cartridges] WHERE InventaryNumber = N'{0}' AND DeliveryDate = (SELECT MAX(DeliveryDate) FROM [Cartridges])", InventaryID);
            SqlCommand command = new SqlCommand(str, sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    string date = Convert.ToString(sqlReader["DeliveryDate"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        public void Issue()
        {

        }

        public void Rezerv()
        {

        }

        public void Purchase()
        {

        }

        public void OrderFromStock()
        {

        }

        public async void ResizeArray()
        {
            //if (label7.Visible)
            //    label7.Visible = false;

            //if (!string.IsNullOrEmpty(txbDepartment.Text) && !string.IsNullOrWhiteSpace(txbDepartment.Text) &&
            //    !string.IsNullOrEmpty(txbRequest.Text) && !string.IsNullOrWhiteSpace(txbRequest.Text) &&
            //    !string.IsNullOrEmpty(txbCartridge.Text) && !string.IsNullOrWhiteSpace(txbCartridge.Text) &&
            //    !string.IsNullOrEmpty(txbQuantity.Text) && !string.IsNullOrWhiteSpace(txbQuantity.Text))
            //{
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("INSERT INTO [Cartridges] (RequestId, InventaryNumber, Department, Cartridge, DeliveryDate, Status)VALUES(@RequestId, @InventaryNumber, @Department, @Cartridge, @DeliveryDate, @Status)", sqlConnection);
            command.Parameters.AddWithValue("RequestId", RequestID);
            command.Parameters.AddWithValue("InventaryNumber", InventaryID);
            command.Parameters.AddWithValue("Department", Department);
            command.Parameters.AddWithValue("Cartridge", Cartridge);
            command.Parameters.AddWithValue("DeliveryDate", RequestDate);
            command.Parameters.AddWithValue("Status", Status);
            command.ExecuteNonQuery();
            //}
            //else
            //{
            //    label7.Visible = true;
            //    label7.Text = "Не все поля заполнены!";
            //}
        }
    }
}
