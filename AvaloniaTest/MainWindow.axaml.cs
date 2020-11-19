using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaTest {

    public class MainWindow : Window {

        private const string RandomFormat = "Random Format";

        public MainWindow() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnCopyButtonClick(object sender, RoutedEventArgs e) {
            var bArr = new byte[20];
            new Random().NextBytes(bArr);

            var dataObj = new DataObject();
            dataObj.Set(RandomFormat, bArr);

            Application.Current.Clipboard.SetDataObjectAsync(dataObj).ContinueWith(t => {
                Console.WriteLine("Copy task completed");
                return t.IsCompleted;
            });
        }

        private async void OnPasteButtonClick(object sender, RoutedEventArgs e) {
            var result = Application.Current.Clipboard.GetDataAsync(RandomFormat).Result as byte[];

            await Message.Show(this, result == null);
        }
    }
}
