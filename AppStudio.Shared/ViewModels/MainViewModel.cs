using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private NoticiasViewModel _noticiasModel;
       private YouTubeViewModel _youTubeModel;
       private SobreViewModel _sobreModel;
        private PrivacyViewModel _privacyModel;
        private RemoveViewModel _removeModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = NoticiasModel;
            _privacyModel = new PrivacyViewModel();
            _removeModel = new RemoveViewModel();
       
        }
 
        public NoticiasViewModel NoticiasModel
        {
            get { return _noticiasModel ?? (_noticiasModel = new NoticiasViewModel()); }
        }
 
        public YouTubeViewModel YouTubeModel
        {
            get { return _youTubeModel ?? (_youTubeModel = new YouTubeViewModel()); }
        }
 
        public SobreViewModel SobreModel
        {
            get { return _sobreModel ?? (_sobreModel = new SobreViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            NoticiasModel.ViewType = viewType;
            YouTubeModel.ViewType = viewType;
            SobreModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                NoticiasModel.LoadItemsAsync(forceRefresh),
                YouTubeModel.LoadItemsAsync(forceRefresh),
                SobreModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
        public ICommand RemoveCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_removeModel.Url);
                });
            }
        }

    }
}
