using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ffme_tester.ViewModel
{
    public class TaskList : ViewModelBase
    {
        #region Single Instance
        private static TaskList _instance;
        private static object SILock = new object();
        public static TaskList Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (SILock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TaskList();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Notification Props
        private string header;
        public string Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }

        private List<TaskListItem> taskitems = new List<TaskListItem>();
        public List<TaskListItem> TaskItems
        {
            get { return taskitems; }
            set {
                Set(ref taskitems, value);
                RaisePropertyChanged("ProgressMaximum");
                RaisePropertyChanged("ProgressValue");
                RaisePropertyChanged("CurrentRunningTask");
            }
        }

        private int cursorIndex = 0;
        public int CursorIndex
        {
            get { return cursorIndex; }
            set {
                Set(ref cursorIndex, value);
                RaisePropertyChanged("CurrentRunningTask");
            }
        }

        private TaskListItem CurrentRunningTask {
            get {
                try
                {
                    return TaskItems[CursorIndex];
                }
                catch
                {
                    return null;
                }
            }
        }

        private bool showProgress = true;
        public bool ShowProgress
        {
            get { return showProgress; }
            set { Set(ref showProgress, value); }
        }

        private bool showList = false;
        public bool ShowList
        {
            get { return showList; }
            set {
                Set(ref showList, value);
                RaisePropertyChanged("ListVisibility");
            }
        }
        public Visibility ListVisibility
        {
            get { return showList ? Visibility.Visible : Visibility.Collapsed; }
        }
        #endregion

        #region Public Functions
        public async Task Initialize() {
            ShowList = false;
            await Task.Delay(Config.Current.Popup_AnimationTime);
            Header = "";
            TaskItems = new List<TaskListItem>();
            CursorIndex = 0;
            ShowProgress = false;
        }
        public void Update(string note)
        {
            var task = TaskItems[CursorIndex];
            if (note != null)
            {
                task.Description = note;
            }
        }
        public void Next(string note = null) {
            var task = TaskItems[CursorIndex];
            if (task.State == TaskListItemState.Pending)
            {
                task.State = TaskListItemState.Finished;
                RaisePropertyChanged("ProgressValue");
            }
            do {
                CursorIndex++;
                if (CursorIndex >= taskitems.Count)
                {
                    return;
                }
            } while (taskitems[CursorIndex].State != TaskListItemState.Waiting);

            if (CursorIndex == TaskItems.Count) {
                return;
            }

            task = TaskItems[CursorIndex];
            if (note != null)
            {
                task.Description = note;
            }
            if (task.State == TaskListItemState.Waiting) {
                task.State = TaskListItemState.Pending;
                RaisePropertyChanged("ProgressValue");
            }
        }
        public void AddTask(string note, int index = -1) {
            var task = new TaskListItem() {
                Description = note,
                State = TaskListItemState.Waiting
            };
            if (index < 0)
            {
                TaskItems.Add(task);
            }
            else
            {
                TaskItems.Insert(index, task);
                if (CursorIndex > index) {
                    CursorIndex++;
                }
            }
        }
        public void Also(int index, string note = null) {
            var task = TaskItems[CursorIndex];
            if (note != null){
                task.Description = note;
            }
            task.State = TaskListItemState.Pending;
            RaisePropertyChanged("ProgressValue");
        }
        public void Active() {
            var task = TaskItems[CursorIndex];
            task.State = TaskListItemState.Pending;
            RaisePropertyChanged("ProgressValue");
        }
        public async Task ShowPopup() {
            ShowList = true;
            await Task.Delay(Config.Current.Popup_AnimationTime);
        }
        public void Disable(int index, string note = null) {
            if (index < 0)
            {
                return;
            }

            var task = TaskItems[index];
            if (note != null)
            {
                task.Description = note;
            }
            task.State = TaskListItemState.NotAvailable;
            RaisePropertyChanged("ProgressValue");
            RaisePropertyChanged("ProgressMaximum");
            Next();
        }
        public void SetProgress(int val, int max = 0)
        {
            if (max > 0)
            {
                CurrentRunningTask.ProgressMaximum = max;
            }
            CurrentRunningTask.ProgressValue = val;
        }
        #endregion

    }

    public class TaskListItem : ViewModelBase {
        private TaskListItemState state;
        public TaskListItemState State
        {
            get { return state; }
            set { Set(ref state, value); }
        }

        private string description;
        public string Description {
            get { return description; }
            set { Set(ref description, value); }
        }

        private int progressMaximum;
        public int ProgressMaximum {
            get { return progressMaximum; }
            set { Set(ref progressMaximum, value); }
        }

        private int progressValue;
        public int ProgressValue {
            get { return progressValue; }
            set { Set(ref progressValue, value); }
        }
    }

    public enum TaskListItemState
    {
        NotAvailable = 0,
        Waiting = 1,
        Pending = 2,
        Finished = 3
    }
}
