using System.ComponentModel;

namespace XstreaMonNET8
{
    public class GroupDescriptor : INotifyPropertyChanged
    {
        private string _propertyName;
        private ListSortDirection _direction;
        private string _format = "{0}: {1}";

        public string PropertyName
        {
            get => _propertyName;
            set
            {
                if (_propertyName != value)
                {
                    _propertyName = value;
                    OnPropertyChanged(nameof(PropertyName));
                }
                else
                {
                    // Forzar notificación aunque el valor sea el mismo
                    OnPropertyChanged(nameof(PropertyName));
                }
            }
        }

        public ListSortDirection Direction
        {
            get => _direction;
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged(nameof(Direction));
                }
            }
        }

        public string Format
        {
            get => _format;
            set
            {
                if (_format != value)
                {
                    _format = value;
                    OnPropertyChanged(nameof(Format));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GroupDescriptor(string propertyName, ListSortDirection direction = ListSortDirection.Ascending, string format = "{0}: {1}")
        {
            _propertyName = propertyName;
            _direction = direction;
            _format = format;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{PropertyName} {Direction}";
        }
    }
}
