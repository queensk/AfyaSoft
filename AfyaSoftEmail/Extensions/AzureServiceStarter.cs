using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftEmail.Messaging.IMessaging;

namespace AfyaSoftEmail.Extensions
{
    public static class AzureServiceStarter
    {
        public static IAzureMessageBusConsumer ServiceBusConsumerInstance { get; set; }
        public static  IApplicationBuilder useAzure(this IApplicationBuilder app)
        {
            ServiceBusConsumerInstance = app.ApplicationServices.GetService<IAzureMessageBusConsumer>();

            var HostLifetime= app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLifetime.ApplicationStarted.Register(OnStart);
            HostLifetime.ApplicationStopping.Register(OnStop);
            return app;
        }

        private static void OnStop()
        {
           ServiceBusConsumerInstance.Stop();
        }

        private static void OnStart()
        {
            ServiceBusConsumerInstance.Start();
        }
    }
}