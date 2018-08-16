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
    class ModelForRequest : INotifyPropertyChanged
    {
        private string inventaryID;
        private string dateSins;
        private string dateTill;
        private string requestID;
        private string department;
        private string cartridge;
        private string requestDate;
        private string status;
        private string quataty;
        public string InventaryID
        {
            get { return inventaryID; }
            set
            {
                inventaryID = value;
                OnPropertyChanged("InventaryID");
            }
        }
        public string DateSins
        {
            get { return dateSins; }
            set
            {
                dateSins = value;
                OnPropertyChanged("DateSins");
            }
        }
        public string DateTill
        {
            get { return dateTill; }
            set
            {
                dateTill = value;
                OnPropertyChanged("DateTill");
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
        public string Quantaty
        {
            get { return quataty; }
            set
            {
                quataty = value;
                OnPropertyChanged("Quantaty");
            }
        }
        public string[] data = new string[3];


        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kharkovskiy-is\source\repos\Accounting cartridges\ACWPF\ACWPF\CartridgeBase.mdf");
        private string v;

        public ModelForRequest(string v, string v1, string v2)
        {
            this.v = v;
        }

        public ModelForRequest(string v)
        {
        }

        public ModelForRequest()
        {
        }

        public void DepartmentRequest(string dep, string sins, string till)
        {
            Department = dep;
            DateSins = sins;
            DateTill = till;
            SqlDataReader sqlReader = null;
            string str = String.Format("SELECT [Department], [Cartridge], COUNT(*) FROM [Cartridges] WHERE [Department] = N'{0}' AND [DeliveryDate] BETWEEN @DateSins AND @DateTill GROUP BY [Cartridge], [Department]", Department);
            SqlCommand command = new SqlCommand(str, sqlConnection);
            command.Parameters.AddWithValue("Department", Department);
            command.Parameters.AddWithValue("DateSins", DateSins);
            command.Parameters.AddWithValue("DateTill", DateTill);
            command.ExecuteNonQueryAsync();

            try
            {
                sqlReader =  command.ExecuteReader();
                while ( sqlReader.Read())
                {
                    data[0] = sqlReader[0].ToString();
                    data[1] = sqlReader[1].ToString();
                    data[2] = sqlReader[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
