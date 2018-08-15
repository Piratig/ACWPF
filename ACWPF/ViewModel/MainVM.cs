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
    class MainVM : INotifyPropertyChanged
    {
        ModelForMainWin model = new ModelForMainWin();
        private ModelForMainWin selectedInventaryInscription;
        public ObservableCollection<ModelForMainWin> Inscription { get; set; }
        public ModelForMainWin SelectedInventaryInscription
        {
            get { return selectedInventaryInscription; }
            set
            {
                selectedInventaryInscription = value;
                OnPropertyChanged("SelectedInventaryInscription");
            }
        }
        public MainVM()
        {
            Inscription = new ObservableCollection<ModelForMainWin>
            {
                new ModelForMainWin { InventaryID = "Инвентарный номер", CartridgeForCheck = "Картридж", DateForCheck = "Дата"}
            };
        }

        public void CheckInventary()
        {
            if (selectedInventaryInscription.InventaryID != null && selectedInventaryInscription.InventaryID != "")
            {
                model.InventaryCheck(selectedInventaryInscription.InventaryID);
                selectedInventaryInscription.CartridgeForCheck = model.CartridgeForCheck;
                selectedInventaryInscription.DateForCheck = model.DateForCheck;
            }
            else
            {
                MessageBox.Show("Поле не заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddRegister(string stat)
        {
            if (selectedInventaryInscription.RequestID != null && selectedInventaryInscription.RequestID != "" &&
                selectedInventaryInscription.Department != null && selectedInventaryInscription.Department != "" &&
                selectedInventaryInscription.Cartridge != null && selectedInventaryInscription.Cartridge != "" )
            {
                model.AddRegister(stat, selectedInventaryInscription.RequestID, selectedInventaryInscription.Department, selectedInventaryInscription.Cartridge, selectedInventaryInscription.RequestDate);
            }
            else
            {
                MessageBox.Show("Поля не заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenRequest()
        {
            model.RequestWindowOpen();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
