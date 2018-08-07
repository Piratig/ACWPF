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
        private string inventaryID;
        private ModelForMainWin modelMain;
        public ObservableCollection<ModelForMainWin> Requests { get; set; }
        public ModelForMainWin SelectedRequest
        {
            get { return modelMain; }
            set
            {
                modelMain = value;
                OnPropertyChanged("SelectedRequest");
            }
        }
        //public string SelectedInventary
        //{
        //    get { return modelMain.ValueInventary; }
        //    set
        //    {
        //        modelMain.ValueInventary = value;
        //        OnPropertyChanged("SelectedInventary");
        //    }
        //}

        public string InventaryID
        {
            get { return inventaryID; }
            set
            {
                inventaryID = value;
                OnPropertyChanged("InventaryID");
            }
        }

        public void InventaryIdCheck()
        {
            ModelForMainWin model = new ModelForMainWin();
            model.InventaryCheck(InventaryID);

        }

        public ObservableCollection<string> MyValue => modelMain.ValueInventary;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
