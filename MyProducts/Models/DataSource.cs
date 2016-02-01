using MyProducts.Models;
using MyProducts.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MyProducts.Data
{
    class DataSource: INotifyPropertyChanged
    {
        private DataServices REST_Services = new DataServices();
        private SmartPhone _Smartphone_Details = new SmartPhone();
        private  bool _Progress_ring_bool = true;
        private bool _Edit_Icon_bool = false;
        private Visibility  _Edit_Icon /*= Visibility.Collapsed*/  ;
        public DataSource()
        {
        }       
        
        private static ObservableCollection<SmartPhone> _Smartphones = new ObservableCollection<SmartPhone>();
        

        public static ObservableCollection<SmartPhone> Smartphones
        {
            get
            {
                
                return _Smartphones;
            }

            set
            {
                _Smartphones = value;
            }
        }

        public  SmartPhone Smartphone_Details
        {
            get
            {
                return _Smartphone_Details;
            }

            set
            {
                _Smartphone_Details = value;
                NotifyPropertyChanged("Smartphone_Details");
            }
        }

        public  bool Progress_ring_bool
        {
            get
            {
                return _Progress_ring_bool;
            }

            set
            {
                _Progress_ring_bool = value;
                NotifyPropertyChanged("Progress_ring_bool");
            }
        }

        public Visibility Edit_Icon
        {
            get
            {
                if (Edit_Icon_bool)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            set
            {
                _Edit_Icon = value;              
                NotifyPropertyChanged("Edit_Icon");
            }
        }

        public bool Edit_Icon_bool
        {
            get
            {
                return _Edit_Icon_bool;
            }

            set
            {
                _Edit_Icon_bool = value;
                NotifyPropertyChanged("Edit_Icon_bool");
            }
        }

        public void SetEditMode()
        {
            this.Edit_Icon_bool = true;
            this.RaiseVisibilityChanges();
        }

        void RaiseVisibilityChanges()
        {
            //base.OnPropertyChanged("Edit_Icon_bool");
        }
        public async void GetData()
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Smartphones.Add(new SmartPhone(i, "http://www.evertek.com.tn/Produits/AndroidSeries/EverShinePlus/Photos/WebBrut/Noir/MiniSite/xImage1DeFaceAvecOmbreMiniSitebg1.png.pagespeed.ic.VHN7-i6CFy.png", "EVERTEK EVERSHINE II", "Référence  EVER-SHINE-II-GOLD", "Ecran:  5.5\" ips HD - Résolution: 1280 x 720 px - Processeur MediaTek MT6735 Quad - Core, Cortex - A53 1.3GHz - Mémoire RAM 1 Go - Stockage 16 Go Extensible Jusqu'à 32 Go Via Micro SD - Systéme d'exploitation: Android 5.1 Lollipop - 2x Appareils Photo 13 Mégapixels(Arrière), 5 Mégapixels(Frontale) - Wifi - 4G - Bluetooth - GPS - Double SIM - Garantie 1 an - Facilite sur 6 mois 73DT, 9 mois 51DT et 12 mois 40DT", "389,00 DT"));

            //}
            try
            {
                var Response = await REST_Services.GetSmartPhones();
                foreach (SmartPhone s in Response)
                {
                    Smartphones.Add(s);
                }
            }
            catch(Exception ex )
            {
                MessageDialog msg = new MessageDialog(ex.ToString());
                await msg.ShowAsync();
            }
          
        }


        public SmartPhone GetItem(string uniqueId)
        {
            
            var result = Smartphones.Where(x => x.Id == Int32.Parse(uniqueId));
            if (result.Count() == 1)
            foreach (SmartPhone s in result)
            {
                Smartphone_Details = s;
            }
            return Smartphone_Details;
        }

        //public async Task<List<SmartPhone>> GetObject()
        public async Task<string> GetObject()
        {


            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://localhost:7824/Smartphones");



            foreach (SmartPhone s in await JsonConvert.DeserializeObjectAsync<List<SmartPhone>>(result))
                {
                    MessageDialog ms = new MessageDialog(s.Titre);
                    await ms.ShowAsync();
                }
            // return result_list;
            return "true";
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
