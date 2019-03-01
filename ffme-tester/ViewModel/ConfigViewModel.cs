using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ffme_tester.ViewModel
{
    public class Config : ViewModelBase
    {
        #region Const Values
        private readonly string[] ExportProps = {
            "Common_VideoContainerHeight",
            "Common_VideoContainerWidth",
            "Video_UseFrameUnit",
            "Video_UseTimeUnit"
        };
        #endregion

        #region Notification Props
        private long common_videoContainerHeight;
        [Description("播放器容器高度")]
        public long Common_VideoContainerHeight {
            get { return common_videoContainerHeight; }
            set { Set(ref common_videoContainerHeight, value); }
        }

        private long common_videoContainerWidth;
        [Description("播放器容器宽度")]
        public long Common_VideoContainerWidth {
            get { return common_videoContainerWidth; }
            set { Set(ref common_videoContainerWidth, value); }
        }

        private bool video_useFrameUnit;
        public bool Video_UseFrameUnit {
            get { return video_useFrameUnit; }
            set {
                Set(ref video_useFrameUnit, value);
                RaisePropertyChanged("Video_UseTimeUnit");
                RaisePropertyChanged("Video_FrameVisibility");
                RaisePropertyChanged("Video_TimeVisibility");
            }
        }
        public bool Video_UseTimeUnit {
            get { return !video_useFrameUnit; }
            set {
                Set(ref video_useFrameUnit, !value);
                RaisePropertyChanged("Video_UseFrameUnit");
                RaisePropertyChanged("Video_FrameVisibility");
                RaisePropertyChanged("Video_TimeVisibility");
            }
        }
        public Visibility Video_FrameVisibility {
            get { return Video_UseFrameUnit ? Visibility.Visible : Visibility.Collapsed; }
        }
        public Visibility Video_TimeVisibility {
            get { return Video_UseTimeUnit ? Visibility.Visible : Visibility.Collapsed; }
        }
        public string DumpConfig()
        {
            List<Tuple<string, string, object, string>> dict = new List<Tuple<string, string, object, string>>();
            foreach(var key in ExportProps)
            {
                dict.Add(GetMeta(key));
            }

            var sb = new StringBuilder();
            var groups = new HashSet<string>(from e in dict select e.Item1);
            foreach(var g in groups)
            {
                sb.AppendLine($"[{g}]");
                var cols = from e in dict where e.Item1.Equals(g) select e;
                foreach(var opt in cols)
                {
                    if (!string.IsNullOrWhiteSpace(opt.Item4)) {
                        foreach(var line in opt.Item4.Split('\n'))
                        {
                            sb.AppendLine($"# {line}");
                        }
                    }
                    sb.AppendLine($"{opt.Item2}={opt.Item3.ToString()}\n");
                }
            }
            return sb.ToString();
        }
        private Tuple<string, string, object, string> GetMeta(string PropName)
        {
            var type = this.GetType();
            var prop = type.GetProperty(PropName);
            var attr = prop.GetCustomAttributes(true);
            var result = from DescriptionAttribute e in attr where e != null select e.Description;
            var desc = result.FirstOrDefault();
            var val = prop.GetValue(this);
            var parts = PropName.Split(new[] { '_' }, 2);
            var group = parts[0];
            var okey = parts[1];
            return new Tuple<string, string, object, string>(group, okey, val, desc);
        }

        #endregion
    }
}
