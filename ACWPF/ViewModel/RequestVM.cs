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


        public RequestVM()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            InscriptionRequest = new ObservableCollection<ModelForRequest>();
            InscriptionRequest.Add(new ModelForRequest(" ", " ", " "));
        }



        public void BildDepartmentReport()
        {
            model.DepartmentRequest(selectedDepartmentRequest.Department, SelectedDepartmentRequest.DateSins, SelectedDepartmentRequest.DateTill);
            {
                for (int i = 0; i < model.data.Length; i++)
                {
                    InscriptionRequest.Add(new ModelForRequest(model.data[0], model.data[1], model.data[2]));
                    //SelectedDepartmentRequest = model;
                    //SelectedDepartmentRequest.Department = model.data[0];
                    //SelectedDepartmentRequest.Cartridge = model.data[1];
                    //SelectedDepartmentRequest.Quantaty = model.data[2];
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
