using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProgressViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int percent = 0;
            // 方法一
            //Task.Run(() =>
            // {
            //     // 在后台线程中执行Test方法
            //     Test(ref percent);
            // });

            // // 在UI线程中定时检查进度并更新
            // Task.Run(async () =>
            // {
            //     int lastPercent = -1;
            //     while (percent < 100)
            //     {
            //         if (percent != lastPercent)
            //         {
            //             lastPercent = percent;
            //             Dispatcher.Invoke(() => pb_value.Value = percent);
            //             Debug.WriteLine($"{percent}");
            //         }
            //         await Task.Delay(50); // 检查频率
            //     }

            //     // 确保最终更新到100%
            //     Dispatcher.Invoke(() => pb_value.Value = 100);
            //     Debug.WriteLine("100");
            // });
            //// 方法二
            //var timer = new System.Windows.Threading.DispatcherTimer();

            //// 在后台线程执行Test
            //Task.Run(() => Test(ref percent));

            //// 设置定时器更新UI
            //timer.Interval = TimeSpan.FromMilliseconds(50);
            //timer.Tick += (s, args) =>
            //{
            //    pb_value.Value = percent;
            //    Debug.WriteLine($"{percent}");

            //    // 当达到100%时停止定时器
            //    if (percent >= 100)
            //    {
            //        timer.Stop();
            //    }
            //};

            //timer.Start();

        }

        public void Test(ref int percent)
        {
            for (int i = 0; i < 100; i++)
            {
                percent++;
                Thread.Sleep(100); // Simulate some work
            }
        }

    }
}