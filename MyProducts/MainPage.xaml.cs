using MyProducts.Models;
using Newtonsoft.Json;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MyProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SQLiteConnection conn;
        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                conn = App.conn;
                string req = @"Create Table if not exists  
                                                User (Id integer primary key autoincrement not null,
                                                      Login varchar(50),
                                                      Passeword varchar(50)
                                                      )";
                using (var Statement = conn.Prepare(req))
                {
                    Statement.Step();
                }

                req = @"select Login from User where Login='Admin'";
                using (var Statement = conn.Prepare(req))
                {
                    //if (SQLiteResult.DONE == Statement.Step())
                    //{
                        Statement.Step();
                        if (Statement.DataCount == 0)
                        {
                            req = @"insert into User(Login,Passeword) values('Admin','Admin')";
                            using (var Statement1 = conn.Prepare(req))
                            {
                                Statement1.Step();
                            }
                        }
                    //}
                }

              
            }
            catch (Exception ex)
            {
                MessageDialog Msg = new MessageDialog(ex.ToString());
                await Msg.ShowAsync();

            }
        }

        private async void Connection_Click(object sender, RoutedEventArgs e)
        {
            //GetObject();
           // string req = @"select count(*) from User where Login='?' and password='?'";
            try
            {
               
                using (var Statement = conn.Prepare("select count(*) from User where Login='"+ Login.Text + "' and Passeword='"+ Passeword.Password + "'"))
                {
                    //Statement.Bind(0, Login.Text);
                    //Statement.Bind(1, Passeword.Text);
                 
                    Statement.Step();
                    int Nb_User_Admin = Int32.Parse(Statement[0].ToString());
                        if (Nb_User_Admin == 1)
                        {
                            this.Frame.Navigate(typeof(Page_Choice));
                        }
                        else
                        {
                            MessageDialog Msg = new MessageDialog("Login Or Passeword not correct. Please Try again !");
                            await Msg.ShowAsync();
                        }
                

                }

            }
            catch (Exception ex)
            {
                MessageDialog Msg = new MessageDialog(ex.ToString());
                await Msg.ShowAsync();
            }

           
        }

        //public async Task<List<SmartPhone>> GetObject()
        //{
        //    List<SmartPhone> o = new List<SmartPhone>();

        //    using (HttpClient client = new HttpClient())
        //    {
        //        using (HttpResponseMessage response = await client.GetAsync("http://localhost:2240/WebService.asmx?op=GetSmartPhones"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string content = await response.Content.ReadAsStringAsync();
        //                o = await JsonConvert.DeserializeObjectAsync<List<SmartPhone>>(content);
        //                foreach (SmartPhone s in o)
        //                {
        //                    MessageDialog ms = new MessageDialog(s.Titre);
        //                    await ms.ShowAsync();
        //                }

        //            }
        //        }
        //    }
        //    return null;
        //}


       
    }
    }
