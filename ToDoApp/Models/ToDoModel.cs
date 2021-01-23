using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ToDoApp.Models
{
    public class ToDoModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _text;
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "isSelected")]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}