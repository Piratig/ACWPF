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
//using ACWPF.Model;
using Prism;
using Prism.Mvvm;

namespace ACWPF
{
    class RequestVM : INotifyPropertyChanged
    {
        ModelForRequest model = new ModelForRequest();
        private ModelForRequest selectedDepartmentRequest;
        public ObservableCollection<ModelForRequest> InscriptionRequest { get; set; }
        public ModelForRequest SelectedDepartmentRequest
        {
            get { return selectedDepartmentRequest; }
            set
            {
                selectedDepartmentRequest = value;
                OnPropertyChanged("SelectedDepartmentRequest");
            }
        }

        public void BildDepartmentReport()
        {
            model.DepartmentRequest(selectedDepartmentRequest.Department);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
