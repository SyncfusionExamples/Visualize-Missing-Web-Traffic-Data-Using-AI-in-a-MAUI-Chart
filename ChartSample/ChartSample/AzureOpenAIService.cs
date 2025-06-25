using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartSample
{
    internal class AzureOpenAIService
    {
        const string endpoint = "";
        const string deploymentName = "";
        string key = "";
        internal IChatClient? Client { get; set; }
        internal string? ChatHistory {  get; set; }
        public AzureOpenAIService()
        {
            GetAzureOpenAI();
        }

        private void GetAzureOpenAI()
        {
            try
            {
                var client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key)).AsChatClient(modelId: deploymentName);
                Client = client;
            }
            catch (Exception) 
            { 
            }
        }

        public async Task<ObservableCollection<Model>> GetCleanedData(ObservableCollection<Model> rawData)
        {
            ObservableCollection<Model> collection = new ObservableCollection<Model>();

            var prompt = $"Clean the following e-commerce website traffic data, resolve outliers and fill missing values:\n{string.Join("\n", rawData.Select(d => $"{d.DateTime:yyyy-MM-dd-HH-m-ss}: {d.Visitors}"))} and the output cleaned data should be in the yyyy-MM-dd-HH-m-ss:Value, not required explanations";

            try
            {
                if(Client != null)
                {
                    ChatHistory = prompt;
                    var response = await Client.CompleteAsync(ChatHistory);
                    return CleanedData(response.ToString(), collection);
                }
            }
            catch (Exception)
            {
                return await Task.FromResult(GetDummyData(collection));
            }

            return collection;
        }

        private ObservableCollection<Model> CleanedData(string json, ObservableCollection<Model> collection)
        {
            if(string.IsNullOrEmpty(json))
            {
                return new ObservableCollection<Model>();
            }

            var lines = json.Split('\n');
            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var parts = line.Split(':');
                if(parts.Length == 2)
                {
                    var date = DateTime.ParseExact(parts[0].Trim(), "yyyy-MM-dd-HH-m-ss", CultureInfo.InvariantCulture);
                    var high = double.Parse(parts[1].Trim());
                    collection.Add(new Model { DateTime = date, Visitors = high });
                }
            }

            return collection;
        }

        private ObservableCollection<Model> GetDummyData(ObservableCollection<Model> collection)
        {
            return new ObservableCollection<Model>()
            {
                new Model { DateTime = new DateTime(2024, 07, 01, 00, 00, 00), Visitors = 150 },
                new Model { DateTime = new DateTime(2024, 07, 01, 01, 00, 00), Visitors = 160 },
                new Model { DateTime = new DateTime(2024, 07, 01, 02, 00, 00), Visitors = 155 },
                new Model { DateTime = new DateTime(2024, 07, 01, 03, 00, 00), Visitors = 162 }, 
                new Model { DateTime = new DateTime(2024, 07, 01, 04, 00, 00), Visitors = 170 },
                new Model { DateTime = new DateTime(2024, 07, 01, 05, 00, 00), Visitors = 175 },
                new Model { DateTime = new DateTime(2024, 07, 01, 06, 00, 00), Visitors = 145 }, 
                new Model { DateTime = new DateTime(2024, 07, 01, 07, 00, 00), Visitors = 180 },
                new Model { DateTime = new DateTime(2024, 07, 01, 08, 00, 00), Visitors = 190 },
                new Model { DateTime = new DateTime(2024, 07, 01, 09, 00, 00), Visitors = 185 },
                new Model { DateTime = new DateTime(2024, 07, 01, 10, 00, 00), Visitors = 200 },
                new Model { DateTime = new DateTime(2024, 07, 01, 11, 00, 00), Visitors = 207 }, // Missing data
                new Model { DateTime = new DateTime(2024, 07, 01, 12, 00, 00), Visitors = 220 },
                new Model { DateTime = new DateTime(2024, 07, 01, 13, 00, 00), Visitors = 230 },
                new Model { DateTime = new DateTime(2024, 07, 01, 14, 00, 00), Visitors = 237 }, // Missing data
                new Model { DateTime = new DateTime(2024, 07, 01, 15, 00, 00), Visitors = 250 },
                new Model { DateTime = new DateTime(2024, 07, 01, 16, 00, 00), Visitors = 260 },
                new Model { DateTime = new DateTime(2024, 07, 01, 17, 00, 00), Visitors = 270 },
                new Model { DateTime = new DateTime(2024, 07, 01, 18, 00, 00), Visitors = 277 }, // Missing data
                new Model { DateTime = new DateTime(2024, 07, 01, 19, 00, 00), Visitors = 280 },
                new Model { DateTime = new DateTime(2024, 07, 01, 20, 00, 00), Visitors = 290 },
                new Model { DateTime = new DateTime(2024, 07, 01, 21, 00, 00), Visitors = 300 },
                new Model { DateTime = new DateTime(2024, 07, 01, 22, 00, 00), Visitors = 307 }, // Missing data
                new Model { DateTime = new DateTime(2024, 07, 01, 23, 00, 00), Visitors = 320 },
            };
        }
    }
}
