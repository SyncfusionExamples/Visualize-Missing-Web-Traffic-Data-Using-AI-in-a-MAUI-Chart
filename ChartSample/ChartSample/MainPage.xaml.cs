using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChartSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            viewModel.IsBusy = true;
            await Task.Delay(2000);
            await viewModel.LoadCleanedDataAsync();
        }
    }

    public class Model
    {
        public DateTime DateTime { get; set; }

        public double Visitors { get; set; }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model> cleanData;
        public ObservableCollection<Model> CleanedData
        {
            get { return cleanData; }
            set
            {
                cleanData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CleanedData"));
            }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
            }
        }
        public ObservableCollection<Brush> PaletteBrushes { get; set; }
        public ObservableCollection<Model> RawData { get; set; }

        public ViewModel()
        {
            IsBusy = false;

            PaletteBrushes = new ObservableCollection<Brush>()
            {
                 new SolidColorBrush(Color.FromArgb("#ffa600")),
                 new SolidColorBrush(Color.FromArgb("#58508d")),
            };

            RawData = new ObservableCollection<Model>()
            {
                new Model{ DateTime = new DateTime(2024, 07, 01, 00, 00, 00), Visitors = 150 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 01, 00, 00), Visitors = 160 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 02, 00, 00), Visitors = 155 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 03, 00, 00), Visitors = double.NaN },
                new Model{ DateTime = new DateTime(2024, 07, 01, 04, 00, 00), Visitors = 170 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 05, 00, 00), Visitors = 175 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 06, 00, 00), Visitors = 145 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 07, 00, 00), Visitors = 180 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 08, 00, 00), Visitors = 190 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 09, 00, 00), Visitors = 185 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 10, 00, 00), Visitors = 200 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 11, 00, 00), Visitors = double.NaN },
                new Model{ DateTime = new DateTime(2024, 07, 01, 12, 00, 00), Visitors = 220 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 13, 00, 00), Visitors = 230 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 14, 00, 00), Visitors = double.NaN },
                new Model{ DateTime = new DateTime(2024, 07, 01, 15, 00, 00), Visitors = 250 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 16, 00, 00), Visitors = 260 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 17, 00, 00), Visitors = 270 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 18, 00, 00), Visitors = double.NaN },
                new Model{ DateTime = new DateTime(2024, 07, 01, 19, 00, 00), Visitors = 280 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 20, 00, 00), Visitors = 290 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 21, 00, 00), Visitors = 300 },
                new Model{ DateTime = new DateTime(2024, 07, 01, 22, 00, 00), Visitors = double.NaN },
                new Model{ DateTime = new DateTime(2024, 07, 01, 23, 00, 00), Visitors = 320 },
            };

            CleanedData = new ObservableCollection<Model>();
        }

        internal async Task LoadCleanedDataAsync()
        {
            var service = new AzureOpenAIService();
            CleanedData = await service.GetCleanedData(RawData);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
