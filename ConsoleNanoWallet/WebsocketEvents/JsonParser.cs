using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class JsonParser
    {
        class OverrideNamingStrategy : SnakeCaseNamingStrategy
        {
            public string GetPropertyName(string name) => this.ResolvePropertyName(name);
        }

        private OverrideNamingStrategy overrideNamingStrategy;
        private Dictionary<string, Type> models = new Dictionary<string, Type>();

        public JsonParser()
        {
            overrideNamingStrategy = new OverrideNamingStrategy();

            // Register event models
            AddModel<ExchangeRateEvent>();
            AddModel<AccountSummaryEvent>();
            AddModel<AccountHistoryEvent>();
            AddModel<AccountHistoryWithPreviousEvent>();
            AddModel<WorkEvent>();
        }

        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };


        private void AddModel<T>()
        {
            models.Add(GetPropertyNames(typeof(T)), typeof(T));
        }

        private string GetPropertyNames(Type type)
        {
            return String.Join("", type.GetProperties().Select(a => overrideNamingStrategy.GetPropertyName(a.Name)).OrderBy(a => a));
        }

        public LightWalletEvent ParseEvent(string json)
        {
            var jsonObject = JObject.Parse(json);

            // What kind of object do we have?
            var typeSignature = String.Join("", jsonObject.Properties().Select(a => a.Name).OrderBy(a => a));

            if (models.TryGetValue(typeSignature, out Type type))
            {
                var model = JsonConvert.DeserializeObject(json, type, jsonSerializerSettings);
                return (LightWalletEvent)model;
            }
            else
            {
                throw new Exception("Could not parse websocket message : " + json);
            }
        }

    }
}
