﻿using System;
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
        private string cartridge;
        public string InventaryID
        {
            get { return inventaryID; }
            set
            {
                inventaryID = value;
                OnPropertyChanged("InventaryID");
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

        public void InventaryCheck(string stringInventary)
        {
            SqlConnection sqlConnection;
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kharkovskiy-is\source\repos\Accounting cartridges\ACWPF\ACWPF\CartridgeBase.mdf");
            sqlConnection.Open();

            if (InventaryID != null && InventaryID != "")
            {
                SqlDataReader sqlReader = null;
                string str = String.Format("SELECT InventaryNumber, Cartridge, DeliveryDate FROM [Cartridges] WHERE InventaryNumber = N'{0}' AND DeliveryDate = (SELECT MAX(DeliveryDate) FROM [Cartridges])", stringInventary);
                SqlCommand command = new SqlCommand(str, sqlConnection);

                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        string date = Convert.ToString(sqlReader["DeliveryDate"]);
                        InventaryID = Convert.ToString(sqlReader["InventaryNumber"]);
                        Cartridge =  Convert.ToString(sqlReader["Cartridge"]);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
