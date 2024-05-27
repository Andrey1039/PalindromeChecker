using Microsoft.Win32;
using System.Windows;

namespace Client.Data.Services
{
    internal class DialogService : IDialogService
    {
        public string OpenFolderDialog(string initPath)
        {
            OpenFolderDialog openFileDialog = new OpenFolderDialog();

            openFileDialog.Title = "Выберите папку";
            openFileDialog.InitialDirectory = initPath;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FolderName;

            return string.Empty;
        }

        public void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}