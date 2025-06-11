using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
namespace WpfApp2
{
    

    public class ProgressViewModel : ViewModelBase
    {
        private int _progressValue;
        private bool _isRunning;
        private string _statusText;

        public ProgressViewModel()
        {
            StartCommand = new RelayCommand(async () => await ExecuteStartCommandAsync(), () => !IsRunning);// 统筹法
            StatusText = "准备开始...";
        }

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (SetProperty(ref _isRunning, value))
                {
                    // 通知Command状态改变
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public ICommand StartCommand { get; }
        // async 这里有需要等待的操作
        private async Task ExecuteStartCommandAsync()
        {
            IsRunning = true;
            ProgressValue = 0;
            StatusText = "正在执行...";

            try
            {
                int percent = 0;

                // 在后台线程执行Test方法，不阻塞UI线程
                var task = Task.Run(() => Test(ref percent));

                // 在UI线程中持续更新进度
                while (!task.IsCompleted) 
                {
                    ProgressValue = percent;
                    StatusText = $"进度: {percent}%";
                    Debug.WriteLine($"{percent}");
                    await Task.Delay(50); // 更新频率 await 在这里等下，但不要阻塞整个程序
                }

                // 确保最终显示100%
                ProgressValue = percent;
                StatusText = $"完成! 最终进度: {percent}%";
                Debug.WriteLine($"完成: {percent}");
            }
            catch (Exception ex)
            {
                StatusText = $"错误: {ex.Message}";
                Debug.WriteLine($"错误: {ex.Message}");
            }
            finally
            {
                IsRunning = false;
            }
        }

        // 保持你的原始Test方法不变
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
