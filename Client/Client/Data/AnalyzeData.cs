using System.IO;
using System.Net;
using System.Text;
using Client.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Client.Data
{
    class AnalyzeData
    {
        // Чтение данных из файла
        private static string ReadFileData(string fileName)
        {
            string fileData = string.Empty;

            try
            {
                fileData = File.ReadAllText(fileName);
            }
            catch
            {
                return string.Empty;
            }

            return fileData;
        }

        // Обработка запроса на/от сервера
        private static async Task<string> SendRequset(string fileData, string serverAddress)
        {
            bool waitResult = true;
            string responseResult = string.Empty;

            StringContent httpContent = new StringContent(fileData, Encoding.UTF8, "application/json");

            while (waitResult)
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = await httpClient
                            .PostAsync($"{serverAddress}/api/Home/CheckPalindrome", httpContent);

                        if (response.StatusCode != HttpStatusCode.ServiceUnavailable)
                        {
                            responseResult = await response.Content.ReadAsStringAsync();
                            waitResult = false;
                        }
                        else
                            await Task.Delay(1000);
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }

            return responseResult;
        }

        // Проверка на палиндром
        internal static async Task<string> CheckPalindrome(string fileName, string serverAddress)
        {
            string fileData = ReadFileData(fileName);

            if (fileData != string.Empty)
            {
                string jsonFileData = JsonConvert.SerializeObject(new
                {
                    Text = fileData
                });

                string rawData = await SendRequset(jsonFileData, serverAddress);

                if (rawData != string.Empty)
                {
                    var responseData = JsonConvert.DeserializeObject<ResponseModel>(rawData);

                    if (responseData != null && responseData.IsPalindrome)
                        return $" - {fileName} ---> содержит палиндром\n";

                    return $" - {fileName} ---> не содержит палиндром\n";
                }

                return $" - Ошибка соединения с сервером (файл {fileName})\n";
            }

            return $" - Ошибка чтения файла {fileName}\n";
        }
    }
}