using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Views;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Media;

namespace Booking.ViewModels.admin
{
    public class RealtyAdminViewModel : ViewModel
    {
        UserAccess access;

        private string? name;
        private string? description;
        private string? slug;
        private string? imageUrl;
        private decimal price;
        private string? city;
        private string? country;
        private string? group;
        private ObservableCollection<string> imageUrls = new ObservableCollection<string>();
        private ObservableCollection<ImageSource?> imageSources = new ObservableCollection<ImageSource?>();

        private string? updateSlug;

        private string? newName;
        private string? newDescription;
        private string? newSlug;
        private string? newImageUrl;
        private decimal newPrice;
        private string? newCity;
        private string? newCountry;
        private string? newGroup;
        private ObservableCollection<string> newImageUrls = new ObservableCollection<string>();
        private ObservableCollection<ImageSource?> newImageSources = new ObservableCollection<ImageSource?>();

        private string? errorMessageOnCreate = "";
        private string? errorMessageOnUpdate = "";
        private string? errorMessageOnDelete = "";
        private bool isViewVisible = true;

        private string? selectedFilePath = "";
        private ImageSource? imageSource;
        private string? newSelectedFilePath = "";
        private ImageSource? updateImageSource;

        DataContext context = new();
        RealtyModel realtyModel;
        ImageModel imageModel;

        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>
        {
            "Hotels",
            "Apartments",
            "Villas",
            "Houses"
        };

        private string? _selectedGroup;
        public string? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

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
        public ObservableCollection<string> ImageUrls
        {
            get => imageUrls;
            set { imageUrls = value; OnPropertyChanged(nameof(ImageUrls)); }
        }
        public ObservableCollection<ImageSource?> ImageSources
        {
            get => imageSources;
            set
            {
                imageSources = value;
                OnPropertyChanged(nameof(ImageSources));
            }
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
        public ObservableCollection<string> NewImageUrls
        {
            get => newImageUrls;
            set
            {
                newImageUrls = value;
                OnPropertyChanged(nameof(NewImageUrls));
            }
        }
        public ObservableCollection<ImageSource?> NewImageSources
        {
            get => newImageSources;
            set
            {
                newImageSources = value;
                OnPropertyChanged(nameof(NewImageSources));
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
        public ImageSource? ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public ImageSource? UpdateImageSource
        {
            get => updateImageSource;
            set
            {
                updateImageSource = value;
                OnPropertyChanged(nameof(UpdateImageSource));
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
        public ICommand AddPrimaryImageCommand {get;set;}
        public ICommand AddSecondaryImageCommand { get; set; }

        public ICommand UpdatePrimaryImageCommand { get; set; }
        public ICommand UpdateSecondaryImageCommand { get; set; }
        public ICommand CreateRealtyCommand { get; }
        public ICommand DeleteRealtyCommand { get; }
        public ICommand UpdateRealtyCommand { get; }

        public RealtyAdminViewModel(UserAccess access)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            AddPrimaryImageCommand = new RelayCommand(ExecuteAddPrimaryImageCommand);
            AddSecondaryImageCommand = new RelayCommand(ExecuteAddSecondaryImageCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            UpdatePrimaryImageCommand = new RelayCommand(ExecuteUpdatePrimaryImageCommand);
            UpdateSecondaryImageCommand = new RelayCommand(ExecuteUpdateSecondaryImageCommand);
            this.context = new();
            this.realtyModel = new(context);
            this.imageModel = new(context);
            this.access = access;
        }

        public RealtyAdminViewModel(DataContext context, RealtyModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            AddPrimaryImageCommand = new RelayCommand(ExecuteAddPrimaryImageCommand);
            AddSecondaryImageCommand = new RelayCommand(ExecuteAddSecondaryImageCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            UpdatePrimaryImageCommand = new RelayCommand(ExecuteUpdatePrimaryImageCommand);
            UpdateSecondaryImageCommand = new RelayCommand(ExecuteUpdateSecondaryImageCommand);
            this.context = context;
            this.realtyModel = model;
            this.imageModel = new(context);
        }

        private void ExecuteAddPrimaryImageCommand(object? obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                SelectedFilePath = selectedPath;
                ImageSource = new ImageSourceConverter().ConvertFromString(selectedPath) as ImageSource;
            }
        }

        private void ExecuteAddSecondaryImageCommand(object? obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                ImageUrls.Add(selectedPath);
                ImageSources.Add(new ImageSourceConverter().ConvertFromString(selectedPath) as ImageSource);
            }
        }

        private void ExecuteUpdatePrimaryImageCommand(object? obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                NewSelectedFilePath = selectedPath;
                UpdateImageSource = new ImageSourceConverter().ConvertFromString(selectedPath) as ImageSource;
            }
        }

        private void ExecuteUpdateSecondaryImageCommand(object? obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                NewImageUrls.Add(selectedPath);
                NewImageSources.Add(new ImageSourceConverter().ConvertFromString(selectedPath) as ImageSource);
            }
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
            var canCreate = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanCreate)
                .FirstOrDefaultAsync();

            if(!canCreate)
            {
                ErrorMessageOnCreate = "You do not have permission to create realty.";
                return;
            }

            ErrorMessageOnCreate = "";

            if (!ValidateCreateInputs())
                return;

            realtyModel ??= new RealtyModel(context);
            imageModel ??= new ImageModel(context);

            bool success = await realtyModel.CreateRealtyAsync(name, description, slug, selectedFilePath, price, city, country, group);
            if (!success)
            {
                ErrorMessageOnCreate = "Failed to create realty.";
                return;
            }

            var realty = await realtyModel.GetRealtyBySlugAsync(slug!);
            if (realty == null)
            {
                ErrorMessageOnCreate = "Realty not found after creation.";
                return;
            }

            bool createMainImg = true;
            if (SelectedFilePath != null)
            {
                bool imgLoaded = await imageModel.LoadImageAsync(slug!, SelectedFilePath);
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
                    bool loaded = await imageModel.LoadImageAsync(slug!, imageUrl);
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
            var canDelete = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanDelete)
                .FirstOrDefaultAsync();

            if (!canDelete)
            {
                ErrorMessageOnDelete = "You do not have permission to create realty.";
                return;
            }

            ErrorMessageOnDelete = "";

            if (string.IsNullOrWhiteSpace(updateSlug))
            {
                ErrorMessageOnDelete = "Slug is empty.";
                return;
            }

            realtyModel ??= new RealtyModel(context);

            bool success = await realtyModel.DeleteRealtyAsync(updateSlug);
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
            var canUpdate = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanUpdate)
                .FirstOrDefaultAsync();

            if (!canUpdate)
            {
                ErrorMessageOnUpdate = "You do not have permission to update realty.";
                return;
            }

            ErrorMessageOnUpdate = "";

            if (!ValidateUpdateInputs())
                return;

            realtyModel ??= new RealtyModel(context);
            imageModel ??= new ImageModel(context);

            bool success = await realtyModel.UpdateRealtyAsync(updateSlug!, newName, newDescription, newSlug, newSelectedFilePath, newPrice, newCity, newCountry, newGroup);
            if (!success)
            {
                ErrorMessageOnUpdate = "Update failed.";
                return;
            }

            var realty = await realtyModel.GetRealtyBySlugAsync(newSlug!);
            if (realty == null)
            {
                ErrorMessageOnUpdate = "Realty not found after update.";
                return;
            }

            var oldImages = await realtyModel.GetImagesAsync(newSlug!);
            foreach (var image in oldImages)
            {
                bool deleted = await imageModel.DeleteImagesByItemIdAsync(realty.Id);
                if (!deleted)
                {
                    ErrorMessageOnUpdate = $"Failed to delete old image: {image.ImageUrl}";
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(NewSelectedFilePath))
            {
                bool imgLoaded = await imageModel.LoadImageAsync(newSlug!, NewSelectedFilePath);
                bool createImg = await imageModel.CreateImageAsync(realty.Id, NewSelectedFilePath);
                if (!imgLoaded || !createImg)
                {
                    ErrorMessageOnUpdate = "Failed to upload new main image.";
                    return;
                }
            }

            foreach (var imgPath in NewImageUrls)
            {
                if (!string.IsNullOrWhiteSpace(imgPath))
                {
                    bool loaded = await imageModel.LoadImageAsync(newSlug!, imgPath);
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
            if (string.IsNullOrWhiteSpace(updateSlug) || string.IsNullOrWhiteSpace(newSlug) || newPrice <= 0)
            {
                ErrorMessageOnUpdate = "Required fields (slug, newSlug, price) must be filled.";
                return false;
            }
            return true;
        }

        private void ClearCreateForm()
        {
            Name = Description = Slug = ImageUrl = City = Country = Group = null;
            SelectedFilePath = null;
            Price = 0;
            ImageUrls = new ObservableCollection<string>();
        }

        private void ClearDeleteForm()
        {
            Slug = null;
        }

        private void ClearUpdateForm()
        {
            UpdateSlug = NewName = NewDescription = NewSlug = NewImageUrl = NewCity = NewCountry = NewGroup = NewSelectedFilePath = null;
            NewPrice = 0;
            NewImageUrls = new ObservableCollection<string>();
        }

    }
}
