using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace Uiguuna_TG
{
    class Program
    { 
        static void Main(string[] args)
        {
            
            TelegramAPI telegramAPI = new TelegramAPI();

            //telegramAPI.GetUpdates();
            //telegramAPI.SendMessage("привет", 834144726);

            //while (true)
            //{
            //    telegramAPI.GetUpdates();
            //}

            //http://api.openweathermap.org/data/2.5/find?q=Minsk&type=like&APPID=57c0f2b70cc28202723d1fbfb864a951
            //http://api.openweathermap.org/data/2.5/weather?q=Minsk&appid=57c0f2b70cc28202723d1fbfb864a951


            string json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\answers.json");
            Dictionary<string, string> questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            List<string> answer = new List<string>();



            while (true)
            {
                var Updates = telegramAPI.GetUpdates();


                foreach (var update in Updates)
                {
                    string question = update.message.text.ToLower();
                    if (question != null)
                    {
                        foreach (var key in questions.Keys)
                        {
                            if (question.Contains(key))
                            {
                                answer.Add(questions[key]);
                            }
                        }
                        if (question.ToLower().Contains("погода"))
                        {
                            string Weather_json = GetWeather();
                            var weatherAPI = JsonConvert.DeserializeObject<WeatherAPI>(Weather_json);
                            telegramAPI.SendMessage((weatherAPI.message).ToString(), update.message.chat.id);
                        }

                        telegramAPI.SendMessage(String.Join(" ,", answer), update.message.chat.id);
                        answer.Clear();
                    }
                }
                
            }


            static string GetWeather()
            {
                string URL = @"http://api.openweathermap.org/data/2.5/find?q=Minsk&type=like&APPID=57c0f2b70cc28202723d1fbfb864a951";
                var RC = new RestClient();
                var Request = new RestRequest(URL);
                var Response = RC.Get(Request);
                return Response.Content;
            }
        }
    }
}
