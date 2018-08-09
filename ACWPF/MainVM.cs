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
                new ModelForMainWin { InventaryID = "Инвентарный номер", Cartridge = "Картридж"}
            };
        }

        public void CheckInventary()
        {
            ModelForMainWin model = new ModelForMainWin();
            if (selectedInventaryInscription.InventaryID != null && selectedInventaryInscription.InventaryID != "")
            {
                model.InventaryCheck(selectedInventaryInscription.InventaryID);
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
