using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProducts.Models
{
    public class SmartPhone : INotifyPropertyChanged
    {
       
        int _Id;
        string _Image;
        string _Titre;
        string _SousTitre;
        string _Description;
        string _Prix;

        public SmartPhone()
        {               
        }
        public SmartPhone(int id , string image, string titre , string soustitre , string description , string prix )
        {
            Id = id;
            Image = image;
            Titre = titre;
            SousTitre = soustitre;
            Description = description;
            Prix = prix;
        }

        
        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }
        
        public string Titre
        {
            get
            {
                return _Titre;
            }

            set
            {
                _Titre = value;

                NotifyPropertyChanged("Titre");
            }
        }
        
        public string SousTitre
        {
            get
            {
                return _SousTitre;
            }

            set
            {
                _SousTitre = value;

                NotifyPropertyChanged("SousTitre");

            }
        }
        
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        
        public string Prix
        {
            get
            {
                return _Prix;
            }

            set
            {
                _Prix = value;

                NotifyPropertyChanged("Prix");
            }
        }

        
        public string Image
        {
            get
            {
                return _Image;
            }

            set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
