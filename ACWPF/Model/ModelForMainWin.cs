using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism;
using Prism.Mvvm;

namespace ACWPF
{
    class ModelForMainWin : INotifyPropertyChanged
    {
        private string inventaryID;
        private string cartridgeForCheck;
        private string dateForCheck;
        private string requestID;
        private string department;
        private string cartridge;
        private string requestDate;
        private string status;
        public string InventaryID
        {
            get { return inventaryID; }
            set
            {
                inventaryID = value;
                OnPropertyChanged("InventaryID");
            }
        }
        public string CartridgeForCheck
        {
            get { return cartridgeForCheck; }
            set
            {
                cartridgeForCheck = value;
                OnPropertyChanged("CartridgeForCheck");
            }
        }
        public string DateForCheck
        {
            get { return dateForCheck; }
            set
            {
                dateForCheck = value;
               OnPropertyChanged("DateForCheck");
            }
        }
        public string RequestID
        {
            get { return requestID; }
            set
            {
                requestID = value;
                OnPropertyChanged("RequestID");
            }
        }
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged("Department");
            }
        }
        public string Cartridge
        {
            get { return cartridge; }
            set
            {
                cartridge = value;
                OnPropertyChanged("Cartridge");
            }
        }
        public string RequestDate
        {
            get { return requestDate; }
            set
            {
                requestDate = value;
                OnPropertyChanged("RequestDate");
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\his19\Documents\GitHub\ACWPF\ACWPF\CartridgeBase.mdf");


        public void InventaryCheck(string stringInventary)
        {
            sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string str = String.Format("SELECT InventaryNumber, Cartridge, DeliveryDate FROM [Cartridges] WHERE InventaryNumber = N'{0}' AND DeliveryDate = (SELECT MAX(DeliveryDate) FROM [Cartridges])", stringInventary);
                SqlCommand command = new SqlCommand(str, sqlConnection);

                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        DateForCheck = Convert.ToString(sqlReader["DeliveryDate"]).Substring(0,10);
                        InventaryID = Convert.ToString(sqlReader["InventaryNumber"]);
                        CartridgeForCheck =  Convert.ToString(sqlReader["Cartridge"]);
                    OnPropertyChanged("SelectedInventaryInscription");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            sqlConnection.Close();
        }

        public void AddRegister(string stringStatus, string id, string dep, string cart, string date)
        {
            sqlConnection.Open();
            Status = stringStatus;
            RequestID = id;
            Department = dep;
            Cartridge = cart;
            RequestDate = date;
            if (RequestID != "" && RequestID != null &&
                InventaryID != "" && InventaryID != null &&
                Department != "" && Department != null &&
                Cartridge != "" && Cartridge != null &&
                RequestDate != "" && RequestDate != null)
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Cartridges] (RequestId, InventaryNumber, Department, Cartridge, DeliveryDate, Status)VALUES(@RequestId, @InventaryNumber, @Department, @Cartridge, @DeliveryDate, @Status)", sqlConnection);
                command.Parameters.AddWithValue("RequestId", RequestID);
                command.Parameters.AddWithValue("InventaryNumber", InventaryID);
                command.Parameters.AddWithValue("Department", Department);
                command.Parameters.AddWithValue("Cartridge", Cartridge);
                command.Parameters.AddWithValue("DeliveryDate", RequestDate);
                command.Parameters.AddWithValue("Status", Status);
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        public void RequestWindowOpen()
        {
            Request req = new Request();
            req.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
