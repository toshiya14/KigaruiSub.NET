using GalaSoft.MvvmLight;

namespace ffme_tester.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public PlayBackInfo MainVideoInfo { get; set; }

        public Config CurrentConfig { get { return Config.Current; } }

        public TaskList CurrentTaskList { get { return TaskList.Current; } }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MainVideoInfo = new PlayBackInfo();
            if (IsInDesignMode)
            {
                // code runs in blend --> create design time data.
                CurrentTaskList.Header = "测试标题";
                CurrentTaskList.ShowList = true;
                CurrentTaskList.ShowProgress = true;
                CurrentTaskList.TaskItems = new System.Collections.Generic.List<TaskListItem> {
                    new TaskListItem{ Description="第一个任务", State= TaskListItemState.Waiting},
                    new TaskListItem{ Description="第二个任务", State= TaskListItemState.Waiting},
                    new TaskListItem{ Description="第三个任务", State= TaskListItemState.Waiting},
                    new TaskListItem{ Description="第四个任务", State= TaskListItemState.Waiting}
                };
            }
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}