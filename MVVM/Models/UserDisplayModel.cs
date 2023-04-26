using System.ComponentModel;

namespace FictionMobile.MVVM.Models
{
    public class UserDisplayModel : INotifyPropertyChanged
    {
        private string _username;
        private string _email;

        public int Id { get; set; }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                INotifyPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                INotifyPropertyChanged(nameof(Email));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
