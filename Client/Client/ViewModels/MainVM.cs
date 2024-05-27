using System.IO;
using Client.Data;
using System.Text;
using System.Windows.Input;
using Client.Data.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ViewModels
{
    internal class MainVM : INotifyPropertyChanged
    {
        private string _result;
        private bool _isAnalyze;
        private string _folderPath;
        private string _serverAddress;
        private IDialogService _dialog;

        public MainVM()
        {
            _isAnalyze = false;
            _result = string.Empty;
            _folderPath = string.Empty;
            _dialog = new DialogService();
            _serverAddress = "http://localhost:5126";
        }

        // Запуск проверки на палиндромы (кнопка)
        private ICommand? analyzeCommand;
        public ICommand AnalyzeCommand
        {
            get
            {
                return analyzeCommand ??= new RelayCommand(async _ =>
                {
                    if (Directory.Exists(_folderPath))
                    {
                        if (!_serverAddress.Equals(string.Empty))
                        {
                            IsAnalyze = true;

                            string[] fileNames = Array.Empty<string>();

                            try
                            {
                                fileNames = Directory.GetFiles(_folderPath, "*.*");
                            }
                            catch
                            {
                                _dialog.ShowErrorMessageBox("Нет доступа к выбранной папке");
                            }

                            Task<string>[] tasks = new Task<string>[fileNames.Length];

                            for (int fileIndex = 0; fileIndex < fileNames.Length; fileIndex++)
                            {
                                int i = fileIndex;
                                tasks[i] = AnalyzeData.CheckPalindrome(fileNames[i], ServerAddress);
                            }

                            await Task.WhenAll(tasks);

                            StringBuilder result = new StringBuilder();

                            foreach (var task in tasks)
                                result.Append(task.Result);

                            Result = result.ToString();

                            IsAnalyze = false;
                        }
                        else
                            _dialog.ShowErrorMessageBox("Не указан адрес сервера");
                    }
                    else
                        _dialog.ShowErrorMessageBox("Выбранная папка не найдена");
                });
            }
        }

        // Выбор папки с файлами для проверки
        private ICommand? selectFolderCommand;
        public ICommand SelectFolderCommand
        {
            get
            {
                return selectFolderCommand ??= new RelayCommand(_ =>
                    FolderPath = _dialog.OpenFolderDialog(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public bool IsAnalyze
        {
            get => _isAnalyze;
            set
            {
                _isAnalyze = value;
                OnPropertyChanged("IsAnalyze");
            }
        }

        public string FolderPath
        {
            get => _folderPath;
            set
            {
                _folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }

        public string ServerAddress
        {
            get => _serverAddress;
            set
            {
                _serverAddress = value;
                OnPropertyChanged("ServerAddress");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}