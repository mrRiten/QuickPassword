using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace QiuckPassword
{
    /// <summary>
    /// Класс для загрузки и сохранения конфигурационных данных в файле JSON.
    /// </summary>
    internal class ConfigLoader
    {
        /// <summary>
        /// Путь к файлу конфигурации.
        /// </summary>
        readonly private string pathConfig = "userConfig.json";

        /// <summary>
        /// Получает конфигурационные данные из файла JSON.
        /// </summary>
        /// <returns>Объект, содержащий конфигурационные данные.</returns>
        public PasswordData GetPasswordData()
        {
            string jsonData = File.ReadAllText(pathConfig);
            PasswordData passwordData = JsonConvert.DeserializeObject<PasswordData>(jsonData);

            return passwordData;
        }

        /// <summary>
        /// Устанавливает новые конфигурационные данные и сохраняет их в файле JSON.
        /// </summary>
        /// <param name="passwordLength">Новая длина пароля.</param>
        /// <param name="blackList">Новый черный список символов.</param>
        public void SetPasswordData(int passwordLength, List<string> blackList)
        {
            var passwordData = new PasswordData
            {
                PasswordLength = passwordLength,
                BlackList = blackList
            };

            string jsonData = JsonConvert.SerializeObject(passwordData);

            File.WriteAllText(pathConfig, jsonData);
        }
    }

    /// <summary>
    /// Класс, представляющий конфигурационные данные пароля.
    /// </summary>
    internal class PasswordData
    {
        /// <summary>
        /// Получает или устанавливает длину пароля.
        /// </summary>
        public int PasswordLength { get; set; }

        /// <summary>
        /// Получает или устанавливает черный список символов.
        /// </summary>
        public List<string> BlackList { get; set; }
    }
}