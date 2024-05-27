namespace Client.Data.Services
{
    internal interface IDialogService
    {
        string OpenFolderDialog(string initPath);
        void ShowErrorMessageBox(string message);
    }
}
