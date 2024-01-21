using System;
using System.Collections.Generic;
using System.Linq;

namespace QiuckPassword
{
    /// <summary>
    /// Класс для генерации случайных паролей с учетом конфигурационных данных.
    /// </summary>
    internal class PasswordGenerator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PasswordGenerator"/>.
        /// </summary>
        public PasswordGenerator()
        {
            PasswordLength = 0;
            BlackListChars = new List<string>();
        }

        /// <summary>
        /// Получает или устанавливает длину генерируемого пароля.
        /// </summary>
        private int PasswordLength { get; set; }

        /// <summary>
        /// Получает или устанавливает черный список символов, которые не должны входить в пароль.
        /// </summary>
        private List<string> BlackListChars { get; set; }

        /// <summary>
        /// Обновляет данные для генерации пароля из конфигурационного файла.
        /// </summary>
        private void UpdateData()
        {
            var config = new ConfigLoader();

            PasswordLength = config.GetPasswordData().PasswordLength;

            foreach (var item in config.GetPasswordData().BlackList)
            {
                BlackListChars.Add(item);
            }
        }

        /// <summary>
        /// Генерирует пароль в соответствии с текущими настройками.
        /// </summary>
        /// <returns>Сгенерированный пароль.</returns>
        public string GeneratePassword()
        {
            UpdateData();

            List<string> smallChar = new List<string>()
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
            };
            List<string> upChar = new List<string>()
            {
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
            List<string> specChar = new List<string>()
            {
                "!","\"","#","$","%","&","'","(",")","*","+",",","-",".","/",":",";","<","=",">","?","@","[","\\","]",
                "|","^","_","`","{","|","}","~"
            };
            List<string> numberChar = new List<string>()
            {
                "0","1","2","3","4","5","6","7","8","9"
            };

            List<List<string>> allChars = new List<List<string>>() { smallChar, upChar, specChar, numberChar };


            List<string> validChars = allChars.SelectMany(chars => chars.Except(BlackListChars)).ToList();

            Random random = new Random();
            List<string> passwordChars = new List<string>();

            foreach (var charList in allChars)
            {
                var validChar = charList.Except(BlackListChars).FirstOrDefault();
                if (validChar != null)
                {
                    passwordChars.Add(validChar);
                }
            }

            for (int i = passwordChars.Count; i < PasswordLength; i++)
            {
                int randomIndex = random.Next(validChars.Count);
                passwordChars.Add(validChars[randomIndex]);
            }

            passwordChars = passwordChars.OrderBy(_ => random.Next()).ToList();

            string generatedPassword = string.Join("", passwordChars);

            return generatedPassword;
        }
    }
}
