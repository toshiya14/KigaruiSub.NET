using NAudio.Wave;
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

namespace AudioSpectogramTester
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var testFile = @"D:\CloudMusic\TWILL - New World.flac";
            var reader = new AudioFileReader(testFile);
            var samples = reader.ToSampleProvider();
            var buffer = new byte[reader.Length];
            var readedCount = reader.Read(buffer, 0, buffer.Length);
            var channels = reader.WaveFormat.Channels;
            var sampleRate = reader.WaveFormat.SampleRate;
            var fftsize = 4096;
            var m = (int)Math.Log(fftsize, 2.0);

            for (var n = 0; n < readedCount; n += channels) {
                
            }
        }
        private void Add(float value) {

        }
    }
}
