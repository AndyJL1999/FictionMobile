using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EpubSharp;
using FictionMobile.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class SearchViewModel : BaseViewModel
    {
        private EpubBook _book;
        [ObservableProperty]
        private StoryDisplayModel _story;

        public SearchViewModel()
        {
            Story = new StoryDisplayModel();
        }

        [RelayCommand]
        private void ReturnToMain()
        {
            Shell.Current.GoToAsync("..");
            ClearStoryInfo();
        }

        [RelayCommand]
        private async Task SearchForFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.Android, new[] { "application/epub+zip" } }
                    })

                });

                if (result != null)
                {
                    if (result.FileName.EndsWith("epub", StringComparison.OrdinalIgnoreCase))
                    {
                        var path = result.FullPath;

                        _book = EpubReader.Read(path);

                        if (_book != null)
                        {
                            Story.Title = _book.Title;
                            Story.Author = _book.Authors.FirstOrDefault();
                            Story.Chapters = _book.Resources.Html.Count.ToString();
                            Story.EpubFile = result.FullPath;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ClearStoryInfo()
        {
            Story.Title = string.Empty;
            Story.Author = string.Empty;
            Story.Chapters = string.Empty;
            Story.Summary = string.Empty;
            Story.EpubFile = string.Empty;
        }
    }
}
