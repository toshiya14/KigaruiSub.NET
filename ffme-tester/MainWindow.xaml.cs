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

            InitializeComponent();
        }

        private void PlayerWrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as Grid;
            var width = grid.ActualWidth;
            var height = grid.ActualHeight;
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

        private void MenuOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            var openfile = new OpenFileDialog()
            {
                Title = "打开视频",
                Filter = "常用视频文件(*.mp4,*.mkv,*.264,*.webm,*.flv,*.vob,*.avi,*.mpg,*.ts,*.m2ts,*.mov,*.wmv,*.rm,*.rmvb,*.m4v,*.mpeg,*.3gp,*.f4v)|*.mp4;*.mkv;*.264;*.webm;*.flv;*.vob;*.avi;*.mpg;*.ts;*.m2ts;*.mov;*.wmv;*.rm;*.rmvb;*.m4v;*.mpeg;*.3gp;*.f4v;*.MP4;*.MKV;*.264;*.WEBM;*.FLV;*.VOB;*.AVI;*.MPG;*.TS;*.M2TS;*.MOV;*.WMV;*.RM;*.RMVB;*.M4V;*.MPEG;*.3GP;*.F4V|常用音频格式(*.aac,*.ape,*.flac,*.m4a,*.mp3,*.ogg,*.wav,*.wma)|*.AAC;*.APE;*.FLAC;*.M4A;*.MP3;*.OGG;*.WAV;*.WMA;*.aac;*.ape;*.flac;*.m4a;*.mp3;*.ogg;*.wav;*.wma|其他格式(*.*)|*.*",
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            };
            if(openfile.ShowDialog() == true)
            {
                var file = openfile.FileName;
                Player.Source = new Uri(file);
            }
        }

        private void CtrlBtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (Player.IsPaused)
            {
                Player.Play();
            }
            else
            {
                Player.Pause();
            }
        }
    }
}
