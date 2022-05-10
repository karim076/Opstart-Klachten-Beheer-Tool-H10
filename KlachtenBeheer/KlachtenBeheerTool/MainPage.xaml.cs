using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KlachtenBeheerTool
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".csv");

            var counting = 1;// count start met 1 want het begint altijd met 0 dus doen we alvast +1 erbij
            float revieuwCount = 0;
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                tbFileStatus.Text = "Geen geldig bestand gekozen";
            }
            else
            {
                tbFileStatus.Text = file.Path;
            }
            using (var fileAcces = await file.OpenReadAsync())
            {
                using (var stream = fileAcces.AsStreamForRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<Klacht>().ToList();
                            lvKlacht.ItemsSource = records;
                            counting = records.Count;

                            foreach(var record in records)
                            {
                                revieuwCount = + float.Parse(record.ReviewScore);
                            }
                            // count.Text = "Aantal Klachten: " + Convert.ToString(numComplaints);
                        }
                    }
                }
            }
            float means =  counting / revieuwCount;
            count.Text = "Aantal Klachten: " + Convert.ToString(counting);
            mean.Text = "Gemiddelde revieuw: " + means.ToString("0.0");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
        }
    }
}
