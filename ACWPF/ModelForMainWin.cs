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
        private int requestID;
        private string department;
        private string cartridge;
        //private string inventaryID;
        private string requestDate;
        private string status;

        public int RequestID
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
        //public string InventaryID
        //{
        //    get { return inventaryID; }
        //    set
        //    {
        //        inventaryID = value;
        //        OnPropertyChanged("InventaryID");
        //    }
        //}
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

        private string valueInventary;
        public string ValueInventary
        {
            get => valueInventary;  
            set
            {
                valueInventary = value;
                if (PropertyChanged != null)
                    OnPropertyChanged("ValueInventary");
            }
        }
        

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public  void InventaryCheck(string InventaryID)
        {
            SqlConnection sqlConnection;
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kharkovskiy-is\source\repos\Accounting cartridges\ACWPF\ACWPF\CartridgeBase.mdf");
            sqlConnection.Open();

            if (InventaryID != null && InventaryID != "")
            {
                SqlDataReader sqlReader = null;
                string str = String.Format("SELECT InventaryNumber, Cartridge, DeliveryDate FROM [Cartridges] WHERE InventaryNumber = N'{0}' AND DeliveryDate = (SELECT MAX(DeliveryDate) FROM [Cartridges])", InventaryID);
                SqlCommand command = new SqlCommand(str, sqlConnection);

                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        string date = Convert.ToString(sqlReader["DeliveryDate"]);
                        ValueInventary = Convert.ToString(sqlReader["InventaryNumber"]) + " | " + Convert.ToString(sqlReader["Cartridge"]) + " | " + date.Substring(0, 10) + "\n";
                        OnPropertyChanged("ValueInventary");
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
            }
            else
            {
                MessageBox.Show("Поле не заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //public void Issue()
        //{
        //    status = "Выдано";
        //    Insert();
        //}

        //public void Rezerv()
        //{
        //    status = "Резерв";
        //    Insert();
        //}

        //public void Purchase()
        //{
        //    status = "Закупка";
        //    Insert();
        //}

        //public void OrderFromStock()
        //{
        //    status = "Заказ со склада";
        //    Insert();
        //}

        //public async void Insert()
        //{
        //    SqlConnection sqlConnection;
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\Database1.mdf;Integrated Security=True";
        //    sqlConnection = new SqlConnection(connectionString);
        //    await sqlConnection.OpenAsync();
        //    SqlCommand command = new SqlCommand("INSERT INTO [Cartridges] (RequestId, InventaryNumber, Department, Cartridge, DeliveryDate, Status)VALUES(@RequestId, @InventaryNumber, @Department, @Cartridge, @DeliveryDate, @Status)", sqlConnection);
        //    command.Parameters.AddWithValue("RequestId", RequestID);
        //    command.Parameters.AddWithValue("InventaryNumber", InventaryID);
        //    command.Parameters.AddWithValue("Department", Department);
        //    command.Parameters.AddWithValue("Cartridge", Cartridge);
        //    command.Parameters.AddWithValue("DeliveryDate", RequestDate);
        //    command.Parameters.AddWithValue("Status", Status);
        //    command.ExecuteNonQuery();
        //}

        public void AboutApp()
        {
            MessageBox.Show("Accounting cartridges v.1.0\nСоздал: Харьковский Игорь Сергеевичь.\nПо всем вопросам обращяться по почте kharkovskiy-is@mosmetro.ru или his1994@mail.ru", "О программе", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void OpenRequest()
        {
            Request request = new Request();
            request.ShowDialog();
        }

        public void Exit()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
