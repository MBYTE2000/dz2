using Newtonsoft.Json;
using RestSharp;
using System;

namespace Uiguuna_TG
{
    class TelegramAPI
    {
        public class Chat
        {
            public int id { get; set; }
            public string first_name { get; set; }
        }
        public class Message
        {
            public Chat chat { get; set; }
            public string text { get; set; }
        }
        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
        }
        class ApiResult
        {
            public Update[] result { get; set; }
        }

        


        private const string token = "1157376669:AAHOIArGR4iclDprW_EISU76Q2Nw8qBlGxw";
        private string API_URL = "https://api.telegram.org/bot"+token+"/";

        private int last_update_id = 0;


        private RestClient RC = new RestClient();
        public TelegramAPI()
        {

        }

        public Update[] GetUpdates()
        {
            var json = SendApiRequest("getUpdates",$"offset={last_update_id}");
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            foreach (var update in apiResult.result)
            {
                Console.WriteLine($"получен апдейт {update.update_id}\n от {update.message.chat.first_name}\n текс: {update.message.text}\nchat_id:{update.message.chat.id}");
                last_update_id = update.update_id+1;
                //Console.WriteLine($"last_update_id:{last_update_id}");
            }
            return apiResult.result;
        }

        public void SendMessage(string text, int chat_id)
        {
            SendApiRequest($"sendMessage",$"chat_id={chat_id}&text={text}");
        }

        public string SendApiRequest(string ApiMethod, string Params)
        {
            string URL = API_URL + ApiMethod+"?"+Params;
            //Console.WriteLine("URL:"+URL);
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            return Response.Content; 
        }
    }
}
