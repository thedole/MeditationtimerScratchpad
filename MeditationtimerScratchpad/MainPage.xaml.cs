using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Meditationtimer.Core;
using Windows.UI.Core;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MeditationtimerScratchpad
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var text = input.Text;
            int seconds = 0;
            if (!Int32.TryParse(text, out seconds))
            {
                input.BorderBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                output.Text = "Invalid input";
                return;
            }

            input.BorderBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

            var timer = new Timer(new TimeSpan(0, 0, seconds));
            timer.Started += Timer_Started;
            timer.Updated += Timer_Updated;
            timer.Completed += Timer_Completed;
            timer.Start();
            
        }

        async private void Timer_Started(object sender)
        {
            await output.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    output.Text = "Timer started";
                }
            );
        }

        async private void Timer_Updated(object sender, TimerUpdatedEventArgs args)
        {
            var text = $"{args.TimeLeft.Seconds:d}";
            
            await input.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () =>
                {
                    input.Text = text;
                }
            );
        }

        async private void Timer_Completed(object sender)
        {
            await output.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    output.Text = "Completed";
                }
            );
        }
    }
}
