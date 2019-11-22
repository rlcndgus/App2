using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using App2.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;


namespace App2.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public MainPageViewModel()
        {

            RegistCommand = new Command(Regist);
            ModifyCommand = new Command<string>(Modify);
            DeleteCommand = new Command(Delete);

            this.UserID = "test1111222333444"; 
            this.UserName = "TEST";
            this.Email = "TEST@gmail.com";
            this.Telephone = "010-0101-0101";
            this.RegistDate = DateTime.Now;

            GetUserListBind();
        }


        public ICommand RegistCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }


        private async void Regist()
        {
            UserModel user = new UserModel();
            user.UserID = this.UserID;
            user.UserName = this.UserName;
            user.Email = this.Email;
            user.Telephone = this.Telephone;
            user.RegistDate = DateTime.Now;

            string baseApiAddress = "http://mixedcode.com/api/user/";
            HttpClient client = new HttpClient();


            var uri = new Uri(baseApiAddress);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(uri, content);
            var result = response.IsSuccessStatusCode;


            GetUserListBind();
        }


        private void Modify(string commandPrameter)
        {
            var test = commandPrameter;
        }


        private void Delete()
        {


        }


        public void GetUserListBind()
        {
            string baseApi = "http://mixedcode.com/";


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseApi);
            client.MaxResponseContentBufferSize = 256000;


                        var response = client.GetAsync("api/user/").Result;


                       List<UserModel> users = null;


                      if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserModel>>(jsonResult);

                NewMethod(users);
            }

        }

        private void NewMethod(List<UserModel> users)
        {
            UserDataList = new ObservableCollection<UserModel>(users);
        }

        private ObservableCollection<UserModel> _UserDataList;
        public ObservableCollection<UserModel> UserDataList
        {
            get
            {
                return _UserDataList;
            }


            set
            {
                _UserDataList = value;
                OnPropertyChanged("UserDataList");
            }
        }

        private string _UserID;
        public string UserID
        {
            get
            {
                return _UserID;
            }


            set
            {
                if (_UserID == value) return;
                _UserID = value;
                OnPropertyChanged("UserID");
            }
        }


        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }


            set
            {
                if (_UserName == value) return;
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }


            set
            {
                if (_Email == value) return;
                _Email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _Telephone;
        public string Telephone
        {
            get
            {
                return _Telephone;
            }


            set
            {
                if (_Telephone == value) return;
                _Telephone = value;
                OnPropertyChanged("Telephone");
            }
        }


        private DateTime? _RegistDate;
        public DateTime? RegistDate
        {
            get
            {
                return _RegistDate;
            }


            set
            {
                if (_RegistDate == value) return;
                _RegistDate = value;
                OnPropertyChanged("RegistDate");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}