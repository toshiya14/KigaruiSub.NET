using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unosquare.FFME;

namespace RMEGo.Kigaruisub.ViewModel
{
    public class PlayBackInfo : ViewModelBase
    {
        #region Const Values
        public readonly int FastForwardOrRewindStepMilliseconds = 500;
        #endregion

        #region Notification Props
        private string videoSource;
        public string VideoSource
        {
            get { return videoSource; }
            set { Set(ref videoSource, value); }
        }

        private TimeSpan totalTimes;
        public TimeSpan TotalTimes
        {
            get { return totalTimes; }
            set { Set(ref totalTimes, value); RaisePropertyChanged("TotalSteps"); }
        }

        private TimeSpan currentTime;
        public TimeSpan CurrentTime
        {
            get { return currentTime; }
            set { Set(ref currentTime, value); RaisePropertyChanged("CurrentStep"); }
        }

        private bool timesCalculated;
        public bool TimeCalculated
        {
            get { return timesCalculated; }
            set { Set(ref timesCalculated, value); }
        }

        public long TotalSteps
        {
            get { return (long)(totalTimes.TotalSeconds * FrameRate) + 1; }
        }

        public long CurrentStep
        {
            get { return (long)(currentTime.TotalSeconds * FrameRate); }
            set {
                if(value == TotalSteps)
                {
                    CurrentTime = TotalTimes;
                    return;
                }
                var seconds = value / FrameRate;
                var gotoPos = TimeSpan.FromSeconds(seconds);
                if(gotoPos >= TotalTimes)
                {
                    CurrentTime = TotalTimes;
                }
                else
                {
                    CurrentTime = gotoPos;
                }
            }
        }

        private double frameRate;
        public double FrameRate
        {
            get { return frameRate; }
            set { Set(ref frameRate, value); }
        }

        private int videoHeight;
        public int VideoHeight {
            get { return videoHeight; }
            set { Set(ref videoHeight, value); }
        }

        private int videoWidth;
        public int VideoWidth {
            get { return videoWidth; }
            set { Set(ref videoWidth, value); }
        }

        private int playerHeight;
        public int PlayerHeight {
            get { return playerHeight; }
            set { Set(ref playerHeight, value); }
        }

        private int playerWidth;
        public int PlayerWidth {
            get { return playerWidth; }
            set { Set(ref playerWidth, value); }
        }

        #endregion

        #region Command Functions Definations
        private void LoadVideo(MediaElement Player)
        {
            TotalTimes = default(TimeSpan);
            CurrentTime = default(TimeSpan);
            TimeCalculated = false;

            // Create open file dialog
            var openfile = new OpenFileDialog()
            {
                Title = "打开视频",
                Filter = "常用视频文件(*.mp4,*.mkv,*.264,*.webm,*.flv,*.vob,*.avi,*.mpg,*.ts,*.m2ts,*.mov,*.wmv,*.rm,*.rmvb,*.m4v,*.mpeg,*.3gp,*.f4v)|*.mp4;*.mkv;*.264;*.webm;*.flv;*.vob;*.avi;*.mpg;*.ts;*.m2ts;*.mov;*.wmv;*.rm;*.rmvb;*.m4v;*.mpeg;*.3gp;*.f4v;*.MP4;*.MKV;*.264;*.WEBM;*.FLV;*.VOB;*.AVI;*.MPG;*.TS;*.M2TS;*.MOV;*.WMV;*.RM;*.RMVB;*.M4V;*.MPEG;*.3GP;*.F4V|常用音频格式(*.aac,*.ape,*.flac,*.m4a,*.mp3,*.ogg,*.wav,*.wma)|*.AAC;*.APE;*.FLAC;*.M4A;*.MP3;*.OGG;*.WAV;*.WMA;*.aac;*.ape;*.flac;*.m4a;*.mp3;*.ogg;*.wav;*.wma|其他格式(*.*)|*.*",
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            };
            if (openfile.ShowDialog() == true)
            {
                var file = openfile.FileName;
                VideoSource = file;
            }

            // Calculate Time after open
            EventHandler action = null;
            action = (sender, args) => {
                MeasureVideo(Player);
                Player.MediaReady -= action;
            };
            Player.MediaReady += action;
        }

        private void FastForward(MediaElement Player) {
            if (PlayerIsOpen(Player))
            {
                CurrentTime = CurrentTime.Add(new TimeSpan(0, 0, 0, 0, FastForwardOrRewindStepMilliseconds));
            }
        }

        private void Rewind(MediaElement Player) {
            if (PlayerIsOpen(Player))
            {
                CurrentTime = CurrentTime.Add(new TimeSpan(0, 0, 0, 0, -FastForwardOrRewindStepMilliseconds));
            }
        }

        private void PauseOrPlay(MediaElement Player)
        {
            if (PlayerIsOpen(Player))
            {
                if (Player.CanPause && Player.IsPlaying)
                {
                    Player.Pause();
                }
                else if (Player.IsPaused)
                {
                    Player.Play();
                }
            }
        }

        private bool PlayerIsOpen(MediaElement Player)
        {
            if (Player != null)
            {
                return Player.IsOpen;
            }
            return false;
        }
        #endregion

        #region Command Props
        public RelayCommand<MediaElement> LoadVideoCommand { get; set; }
        public RelayCommand<MediaElement> FastForwardCommand { get; set; }
        public RelayCommand<MediaElement> RewindCommand { get; set; }
        public RelayCommand<MediaElement> PauseOrPlayCommand { get; set; }
        #endregion

        #region Functions
        
        private void MeasureVideo(MediaElement Player)
        {
            // Pause if the video is playing.
            if (Player.IsPlaying)
            {
                Player.Pause();
            }

            // if player has natural duration timespan, use it as the total time.
            if (Player.NaturalDuration.HasValue)
            {
                FrameRate = Player.VideoFrameRate;
                VideoHeight = Player.NaturalVideoHeight;
                VideoWidth = Player.NaturalVideoWidth;
                TotalTimes = Player.NaturalDuration.Value;
                timesCalculated = true;
            }
        }

        private void MeasureAudio(MediaElement Player) {
        }

        #endregion

        public PlayBackInfo()
        {
            LoadVideoCommand = new RelayCommand<MediaElement>(LoadVideo);
            FastForwardCommand = new RelayCommand<MediaElement>(FastForward);
            RewindCommand = new RelayCommand<MediaElement>(Rewind);
            PauseOrPlayCommand = new RelayCommand<MediaElement>(PauseOrPlay);
        }
    }
}
