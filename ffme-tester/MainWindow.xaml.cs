using ffme_tester.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ffme_tester
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            // Initialize FFME
            var ffmpegPath = System.IO.Path.Combine(AppPath, "share/ffmpeg");
            if (!System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffmpeg.exe")) ||
                !System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffplay.exe")) ||
                !System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffprobe.exe"))
               )
            {
                MessageBox.Show("缺少ffmpeg相关的可执行文件，请重新解压/安装本程序。\n遇到无法继续的错误，程序将会退出。", "初始化失败", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            Unosquare.FFME.MediaElement.FFmpegDirectory = ffmpegPath;

            // Warm up


            InitializeComponent();
        }

        /// <summary>
        /// Handles the SizeChanged event of the PlayerWrapGrid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
        private void PlayerWrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as Grid;
            var width = grid.ActualWidth;
            var height = grid.ActualHeight - 46;
            if(width / 16 > height / 9)
            {
                var towidth = height / 9 * 16;
                Player.Height = height;
                Player.Width = towidth;
            }
            else
            {
                var toheight = width / 16 * 9;
                Player.Height = toheight;
                Player.Width = width;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            await TaskList.Current.Initialize();
            TaskList.Current.Header = "测试标题";
            TaskList.Current.ShowList = true;
            TaskList.Current.ShowProgress = true;
            TaskList.Current.TaskItems = new System.Collections.Generic.List<TaskListItem> {
                    new TaskListItem{ Description="执行预定义脚本", State= TaskListItemState.Waiting},
                    new TaskListItem{ Description="执行逐行脚本", State= TaskListItemState.Waiting},
                    new TaskListItem{ Description="写入文件", State= TaskListItemState.Waiting}
                };
            await TaskList.Current.ShowPopup();
            TaskList.Current.Active();
            await Task.Delay(2000);
            TaskList.Current.Next("生成每一行的结果(0/10)");
            TaskList.Current.Update("生成每一行的结果(1/10)");
            await Task.Delay(1000);
            TaskList.Current.Update("生成每一行的结果(2/10)");
            await Task.Delay(1000);
            TaskList.Current.Update("生成每一行的结果(3/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(4/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(5/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(6/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(7/10)");
            await Task.Delay(2000);
            TaskList.Current.Update("生成每一行的结果(8/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(9/10)");
            await Task.Delay(100);
            TaskList.Current.Update("生成每一行的结果(10/10)");
            TaskList.Current.Next();
            TaskList.Current.Next();
            await TaskList.Current.Initialize();
        }
    }
}
