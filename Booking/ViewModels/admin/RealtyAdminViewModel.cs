using Booking.Data;
using Booking.Models;
using Booking.Views;
using System.Windows.Input;

namespace Booking.ViewModels.admin
{
    public class RealtyAdminViewModel : ViewModel
    {
        private string? name;
        private string? description;
        private string? slug;
        private string? imageUrl;
        private decimal price;
        private string? city;
        private string? country;
        private string? group;
        private List<string> imageUrls = new List<string>();

        private string? updateSlug;

        private string? newName;
        private string? newDescription;
        private string? newSlug;
        private string? newImageUrl;
        private decimal newPrice;
        private string? newCity;
        private string? newCountry;
        private string? newGroup;
        private List<string> newImageUrls = new List<string>();

        private string? errorMessageOnCreate = "";
        private string? errorMessageOnUpdate = "";
        private string? errorMessageOnDelete = "";
        private bool isViewVisible = true;

        private string? selectedFilePath = "";
        private string? newSelectedFilePath = "";

        DataContext context = new();
        RealtyModel realtyModel;
        ImageModel imageModel;

        public string? Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string? Description
        {
            get => description;
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
        public string? Slug
        {
            get => slug;
            set { slug = value; OnPropertyChanged(nameof(Slug)); }
        }
        public string? ImageUrl
        {
            get => imageUrl;
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }
        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }
        public string? City
        {
            get => city;
            set { city = value; OnPropertyChanged(nameof(City)); }
        }
        public string? Country
        {
            get => country;
            set { country = value; OnPropertyChanged(nameof(Country)); }
        }
        public string? Group
        {
            get => group;
            set { group = value; OnPropertyChanged(nameof(Group)); }
        }
        public List<string> ImageUrls
        {
            get => imageUrls;
            set { imageUrls = value; OnPropertyChanged(nameof(ImageUrls)); }
        }

        public string? UpdateSlug
        {
            get => updateSlug;
            set
            {
                updateSlug = value;
                OnPropertyChanged(nameof(UpdateSlug));
            }
        }
        public string? NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        public string? NewDescription
        {
            get => newDescription;
            set
            {
                newDescription = value;
                OnPropertyChanged(nameof(NewDescription));
            }
        }
        public string? NewSlug
        {
            get => newSlug;
            set
            {
                newSlug = value;
                OnPropertyChanged(nameof(NewSlug));
            }
        }
        public string? NewImageUrl
        {
            get => newImageUrl;
            set
            {
                newImageUrl = value;
                OnPropertyChanged(nameof(NewImageUrl));
            }
        }
        public decimal NewPrice
        {
            get => newPrice;
            set
            {
                newPrice = value;
                OnPropertyChanged(nameof(NewPrice));
            }
        }
        public string? NewCity
        {
            get => newCity;
            set
            {
                newCity = value;
                OnPropertyChanged(nameof(NewCity));
            }
        }
        public string? NewCountry
        {
            get => newCountry;
            set
            {
                newCountry = value;
                OnPropertyChanged(nameof(NewCountry));
            }
        }
        public string? NewGroup
        {
            get => newGroup;
            set
            {
                newGroup = value;
                OnPropertyChanged(nameof(NewGroup));
            }
        }
        public List<string> NewImageUrls
        {
            get => newImageUrls;
            set
            {
                newImageUrls = value;
                OnPropertyChanged(nameof(NewImageUrls));
            }
        }

        public string? SelectedFilePath
        {
            get => selectedFilePath;
            set
            {
                selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
            }
        }
        public string? NewSelectedFilePath
        {
            get => newSelectedFilePath;
            set
            {
                newSelectedFilePath = value;
                OnPropertyChanged(nameof(NewSelectedFilePath));
            }
        }

        public string? ErrorMessageOnCreate
        {
            get => errorMessageOnCreate;
            set { errorMessageOnCreate = value; OnPropertyChanged(nameof(ErrorMessageOnCreate)); }
        }
        public string? ErrorMessageOnUpdate
        {
            get => errorMessageOnUpdate;
            set { errorMessageOnUpdate = value; OnPropertyChanged(nameof(ErrorMessageOnUpdate)); }
        }
        public string? ErrorMessageOnDelete
        {
            get => errorMessageOnDelete;
            set { errorMessageOnDelete = value; OnPropertyChanged(nameof(ErrorMessageOnDelete)); }
        }
        public bool IsViewVisible
        {
            get => isViewVisible;
            set { isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        public event EventHandler OnRequestClose;
        public event EventHandler OnRequestClearRealtyCreateForm;
        public event EventHandler OnRequestClearRealtyDeleteForm;
        public event EventHandler OnRequestClearRealtyUpdateForm;

        public ICommand MainWindowCommand { get; }
        public ICommand AddImageOnCreateCommand { get; set; }
        public ICommand CreateRealtyCommand { get; }
        public ICommand DeleteRealtyCommand { get; }
        public ICommand UpdateRealtyCommand { get; }

        public RealtyAdminViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = new();
            this.realtyModel = new(context);
            this.imageModel = new(context);
        }

        public RealtyAdminViewModel(DataContext context, RealtyModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = context;
            this.realtyModel = model;
            this.imageModel = new(context);
        }

        private void ExecuteMainWindowCommand(object? obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteCreateRealtyCommand(object? obj)
        {
            ErrorMessageOnCreate = "";

            if (!ValidateCreateInputs())
                return;

            realtyModel ??= new RealtyModel(context);
            imageModel ??= new ImageModel(context);

            bool success = await realtyModel.CreateRealtyAsync(name, description, slug, imageUrl, price, city, country, group);
            if (!success)
            {
                ErrorMessageOnCreate = "Failed to create realty.";
                return;
            }

            var realty = await realtyModel.GetRealtyBySlugAsync(slug);
            if (realty == null)
            {
                ErrorMessageOnCreate = "Realty not found after creation.";
                return;
            }

            bool createMainImg = true;
            if (!string.IsNullOrWhiteSpace(SelectedFilePath))
            {
                bool imgLoaded = await imageModel.LoadImageAsync(slug, SelectedFilePath);
                createMainImg = await imageModel.CreateImageAsync(realty.Id, SelectedFilePath);
                if (!imgLoaded || !createMainImg)
                {
                    ErrorMessageOnCreate = "Main image upload failed.";
                    return;
                }
            }

            bool allImagesCreated = true;

            foreach (var imageUrl in imageUrls)
            {
                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    bool loaded = await imageModel.LoadImageAsync(slug, imageUrl);
                    bool imgCreated = await imageModel.CreateImageAsync(realty.Id, imageUrl);

                    if (!loaded || !imgCreated)
                    {
                        allImagesCreated = false;
                        ErrorMessageOnCreate = $"Failed to upload image: {imageUrl}";
                        break;
                    }
                }
            }


            if (!allImagesCreated)
                return;

            OnRequestClearRealtyCreateForm?.Invoke(this, EventArgs.Empty);
            ClearCreateForm();
        }

        private async void ExecuteDeleteRealtyCommand(object? obj)
        {
            ErrorMessageOnDelete = "";

            if (string.IsNullOrWhiteSpace(slug))
            {
                ErrorMessageOnDelete = "Slug is empty.";
                return;
            }

            realtyModel ??= new RealtyModel(context);

            bool success = await realtyModel.DeleteRealtyAsync(slug);
            if (!success)
            {
                ErrorMessageOnDelete = "Invalid slug or delete failed.";
                return;
            }

            OnRequestClearRealtyDeleteForm?.Invoke(this, EventArgs.Empty);
            ClearDeleteForm();
        }

        private async void ExecuteUpdateRealtyCommand(object? obj)
        {
            ErrorMessageOnUpdate = "";

            if (!ValidateUpdateInputs())
                return;

            realtyModel ??= new RealtyModel(context);
            imageModel ??= new ImageModel(context);

            bool success = await realtyModel.UpdateRealtyAsync(slug, newName, newDescription, newSlug, newImageUrl, newPrice, newCity, newCountry, newGroup);
            if (!success)
            {
                ErrorMessageOnUpdate = "Update failed.";
                return;
            }

            var realty = await realtyModel.GetRealtyBySlugAsync(newSlug);
            if (realty == null)
            {
                ErrorMessageOnUpdate = "Realty not found after update.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(NewSelectedFilePath))
            {
                bool imgLoaded = await imageModel.LoadImageAsync(newSlug, NewSelectedFilePath);
                bool createImg = await imageModel.CreateImageAsync(realty.Id, NewSelectedFilePath);
                if (!imgLoaded || !createImg)
                {
                    ErrorMessageOnUpdate = "Failed to upload new main image.";
                    return;
                }
            }

            foreach (var imgPath in newImageUrls)
            {
                if (!string.IsNullOrWhiteSpace(imgPath))
                {
                    bool loaded = await imageModel.LoadImageAsync(newSlug, imgPath);
                    bool added = await imageModel.CreateImageAsync(realty.Id, imgPath);

                    if (!loaded || !added)
                    {
                        ErrorMessageOnUpdate = $"Failed to add image: {imgPath}";
                        return;
                    }
                }
            }


            OnRequestClearRealtyUpdateForm?.Invoke(this, EventArgs.Empty);
            ClearUpdateForm();
        }

        private bool ValidateCreateInputs()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(slug) || price <= 0)
            {
                ErrorMessageOnCreate = "Required fields (name, slug, price) must be filled.";
                return false;
            }
            return true;
        }

        private bool ValidateUpdateInputs()
        {
            if (string.IsNullOrWhiteSpace(slug) || string.IsNullOrWhiteSpace(newSlug) || newPrice <= 0)
            {
                ErrorMessageOnUpdate = "Required fields (slug, newSlug, price) must be filled.";
                return false;
            }
            return true;
        }

        private void ClearCreateForm()
        {
            Name = Description = Slug = ImageUrl = City = Country = Group = SelectedFilePath = null;
            Price = 0;
            ImageUrls = new List<string>();
        }

        private void ClearDeleteForm()
        {
            Slug = null;
        }

        private void ClearUpdateForm()
        {
            UpdateSlug = NewName = NewDescription = NewSlug = NewImageUrl = NewCity = NewCountry = NewGroup = NewSelectedFilePath = null;
            NewPrice = 0;
            NewImageUrls = new List<string>();
        }

    }
}
