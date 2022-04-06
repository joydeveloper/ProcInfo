using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcInfo
{
    internal class Logger
    {
        private string _logPath = Environment.CurrentDirectory + @"\Logger.txt";
        private string _onLogCreate = "-----Журнал создан: " + DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Year + "-----";
        public string LogPath { get => _logPath; set { _logPath = value; } }
        public Logger()
        {

        }
        public string CreateLog()
        {
            if (!File.Exists(_logPath))
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(_logPath))
                    {
                        outputFile.WriteLine(_onLogCreate);
                        outputFile.Close();
                    }
                }
                catch (FileNotFoundException e)
                {
                    return e.ToString();
                }
                catch (IOException e)
                {
                    if (e.Source != null)
                        return e.ToString();
                    throw;
                }
            }
            else
            {
                return "Журнал уже создан";
            }
            return "Журнал обновлен";
        }
        public string AddRecord(string record)
        {
            if (File.Exists(_logPath))
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(_logPath, true))
                    {

                        outputFile.WriteLine(record);
                        outputFile.Close();
                        return "Запись добавлена";
                    }
                }
                catch (FileNotFoundException e)
                {
                    return "Фаил не найден " + e;
                }
                catch (IOException e)
                {
                    if (e.Source != null)
                        return "Ошибка ввода/вывода: {0}" + e.Source;
                    throw;
                }
            }
            else
            {
                return "Журнал не найден";
            }
        }
    }
}
