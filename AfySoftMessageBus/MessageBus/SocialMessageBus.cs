using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace AfySoftMessageBus.MessageBus
{
    public class SocialMessageBus: IMessageBus
    {
        public string ConnectionString = "";

        public async Task PublishMessage(object message, string queue_topic_name)
        {
            var serviceBus = new ServiceBusClient(this.ConnectionString);
            var sender = serviceBus.CreateSender(queue_topic_name);

            var jsonMessage=JsonConvert.SerializeObject(message);

            var theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender.SendMessageAsync(theMessage);
            await serviceBus.DisposeAsync();
        }
    }
}