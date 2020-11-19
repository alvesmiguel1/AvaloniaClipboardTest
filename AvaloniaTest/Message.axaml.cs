using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTest {

    class Message : Window {

        public Message() {
            AvaloniaXamlLoader.Load(this);
        }

        public static Task<bool> Show(Window parent, bool result) {
            var msgbox = new Message() {
                Title = "Paste Result"
            };
            msgbox.FindControl<TextBlock>("Text").Text = "Clipboard is " + (result ? "empty" : "not empty");

            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");
            var btn = new Button { Content = "Ok" };
            btn.Click += (_, __) => {
                msgbox.Close();
            };
            buttonPanel.Children.Add(btn);

            var tcs = new TaskCompletionSource<bool>();
            msgbox.Closed += delegate { tcs.TrySetResult(true); };
            if (parent != null) {
                msgbox.ShowDialog(parent);
            } else {
                msgbox.Show();
            }
            return tcs.Task;
        }


    }

}