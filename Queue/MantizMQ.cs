using Experimental.System.Messaging;
using Serilog;
using System.Text.RegularExpressions;

namespace MZ_WorkerService.Queue
{
    public class MantizMQ
    {
        private MessageQueue? MQ { get; set; }
        private Message? Message { get; set; }
        private string QueueName { get; set; }
        private string WebHost { get; set; }

        public MantizMQ(bool isResponse = false)
        {
            WebHost = Worker.Configuration!.GetValue<string>("WindowsMQ:Host")!;
            QueueName = Worker.Configuration!.GetValue<string>("WindowsMQ:QueueName")!;
            if (isResponse) QueueName += "_respuesta";

            Connect();
        }

        public MantizMQ(string webHost, string queueName, bool isResponse = false)
        {
            if (isResponse) queueName += "_respuesta";
            WebHost = webHost;
            QueueName = queueName;

            Connect();
        }

        private void Connect()
        {
            try
            {
                if (Regex.IsMatch(WebHost, @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$"))
                {
                    MQ = new MessageQueue("FormatName:Direct=TCP:" + WebHost + "\\private$\\" + QueueName);
                }
                else
                {
                    MQ = new MessageQueue("FormatName:Direct=OS:" + WebHost + "\\private$\\" + QueueName);
                }
                MQ.MessageReadPropertyFilter.CorrelationId = true;
                MQ.MessageReadPropertyFilter.AppSpecific = true;

                Log.Information("Conectado a la Cola Windows: {QueueName} Host: {WebHost}", QueueName, WebHost);
            }
            catch (ArgumentException e)
            {
                Log.Error(e, "Error al conectarse a la Cola Windows: {QueueName} Host: {WebHost}", QueueName, WebHost);
            }
        }

        public string Read()
        {
            try
            {
                Message = MQ!.Receive();
                Message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                var body = Message.Body.ToString();

                return body!;
            }
            catch (MessageQueueException e)
            {
                Log.Error(e, "Error al recibir el mensaje de la Cola Windows: {QueueName}, Host: {WebHost}", QueueName, WebHost);
            }

            return null!;
        }

        public async Task<string> Listen(TimeSpan timeOut, CancellationToken stoppingToken)
        {
            string message = null!;

            while (!stoppingToken.IsCancellationRequested && string.IsNullOrEmpty(message))
            {
                try
                {
                    Message = await Task.Factory.FromAsync(MQ!.BeginReceive(timeOut), MQ!.EndReceive);
                    Message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    message = Message.Body.ToString()!;
                }
                catch (MessageQueueException e)
                {
                    if (e.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                        Log.Error(e, "Error al recibir el mensaje de la Cola Windows: {QueueName}, Host: {WebHost}", QueueName, WebHost);
                }
            }

            return message!;
        }

        public async Task<string> ReadAsync()
        {
            int cantidad = MQ!.GetAllMessages().Length;

            if (cantidad > 0)
            {
                Message = await Task.Factory.FromAsync<Message>(MQ.BeginReceive(), MQ.EndReceive);
                Message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                var body = Message.Body.ToString();

                return body!;
            }

            return null!;
        }

        public bool Write(string xml, string id = null!)
        {
            try
            {
                Message = new Message();
                Message.TimeToReachQueue = Message.InfiniteTimeout;
                Message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                if (id != null) Message.CorrelationId = id;
                Message.Body = xml;

                MQ!.Send(Message);
                return true;
            }
            catch (MessageQueueException e)
            {
                Log.Error(e, "Error al enviar el mensaje de a la Cola Windows: {QueueName}, Host: {WebHost}", QueueName, WebHost);
            }

            return false;
        }

        public string GetMessageId()
        {
            return Message?.Id!;
        }
    }
}
