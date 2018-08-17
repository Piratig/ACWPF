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
    class RequestVM : INotifyPropertyChanged
    {
        ModelForRequest model = new ModelForRequest();
        private ModelForRequest selectedDepartmentRequest;
        private ObservableCollection<ModelForRequest> inscriptionRequest; 
        public ModelForRequest SelectedDepartmentRequest
        {
            get { return selectedDepartmentRequest; }
            set
            {
                selectedDepartmentRequest = value;
                OnPropertyChanged("SelectedDepartmentRequest");
            }
        }
        public ObservableCollection<ModelForRequest> InscriptionRequest
        {
            get { return inscriptionRequest; }
            set
            {
                inscriptionRequest = value;
                OnPropertyChanged("InscriptionRequest");
            }
        }


        public RequestVM()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            InscriptionRequest = new ObservableCollection<ModelForRequest>();
            inscriptionRequest.Add(new ModelForRequest("aaaaa"));
    }



    public void BildDepartmentReport()
        {
            model.DepartmentRequest(selectedDepartmentRequest.Department, SelectedDepartmentRequest.DateSins, SelectedDepartmentRequest.DateTill);
            {
                for (int i = 0; i < model.data.Length; i++)
                {
                    inscriptionRequest.Add(new ModelForRequest());
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
