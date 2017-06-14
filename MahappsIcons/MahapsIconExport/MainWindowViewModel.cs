using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using MahApps.Metro.IconPacks;

namespace MahapsIconExport
{
    public class MainWindowViewModel : Screen
    {
        private readonly List<IconViewModel> m_allIcons;
        private static readonly string m_favouriteIconsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MahapsIcons");
        private static readonly string m_favouriteIconsPath = Path.Combine(m_favouriteIconsDir, "FavouriteIcons.txt");

        public MainWindowViewModel()
        {
            m_allIcons = new List<IconViewModel>();
            AddToFavouriteCommand = new RelayCommand(AddToFavourite);
            FilterFavouriteCommand = new RelayCommand(FilterFavourite);
            CopyToClipboardCommand = new RelayCommand(CopyToClipboard);
        }

        public ICommand AddToFavouriteCommand { get; }

        public ICommand FilterFavouriteCommand { get; }

        public ICommand CopyToClipboardCommand { get; }

        public BindableCollection<IconViewModel> Icons { get; set; } = new BindableCollection<IconViewModel>();

        private string m_filterText;
        public string FilterText
        {
            get => m_filterText;
            set
            {
                m_filterText = value;

                NotifyOfPropertyChange(nameof(FilterText));
                NotifyOfPropertyChange(nameof(Icons));
                Filter();
            }
        }

        public void Initialize()
        {
            var favouriteIcons = LoadFavourite();

            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconModernKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconModernKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconMaterialKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconMaterialKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconMaterialLightKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconMaterialLightKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconFontAwesomeKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconFontAwesomeKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconOcticonsKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconOcticonsKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconEntypoKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconEntypoKind), favouriteIcons.Contains(s.ToString()))));
            m_allIcons.AddRange(Enum.GetValues(typeof(PackIconSimpleIconsKind)).Cast<Enum>().Select(s => new IconViewModel(s, nameof(PackIconSimpleIconsKind), favouriteIcons.Contains(s.ToString()))));

            Filter();
        }

        private void Filter()
        {
            Icons.Clear();
            Icons.AddRange(string.IsNullOrEmpty(FilterText) ? m_allIcons : m_allIcons.Where(d => d.IconName.ToLower().Contains(FilterText.ToLower())));
        }

        private void AddToFavourite(object parameter)
        {
            var casted = (IconViewModel)parameter;
            casted.IsFavourite = !casted.IsFavourite;

            var favouriteIcons = m_allIcons.Where(d => d.IsFavourite).Select(d => d.IconKind);

            Save(favouriteIcons.Select(s => s.ToString()));
        }

        private void CopyToClipboard(object parameter)
        {
            var casted = (IconViewModel)parameter;

            Clipboard.SetText($"{casted.IconKindName}.{casted.IconName}");
        }

        private void FilterFavourite()
        {
            Icons.Clear();
            Icons.AddRange(m_allIcons.Where(w => w.IsFavourite));
        }

        private static void Save(IEnumerable<string> iconNames)
        {
            if (!Directory.Exists(m_favouriteIconsPath))
                Directory.CreateDirectory(m_favouriteIconsDir);

            File.WriteAllLines(m_favouriteIconsPath, iconNames);
        }

        private static IList<string> LoadFavourite()
        {
            if(!File.Exists(m_favouriteIconsPath))
                return new List<string>();

            return File.ReadAllLines(m_favouriteIconsPath);
        }
    }
}