using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System.Configuration;

namespace AmazonIntegration
{
    public class SNS
    {
        private AmazonSimpleNotificationServiceClient client { get; }

        public SNS()
        {
            client = new AmazonSimpleNotificationServiceClient(ConfigurationManager.AppSettings["AWSAIMAccess"], ConfigurationManager.AppSettings["AWSAIMSecret"], RegionEndpoint.EUCentral1);
        }

        public string PublishEntity(string topicArn, string message, string subject, string messageType)
        {
            var publishRequest = new PublishRequest(topicArn, message, subject);
            publishRequest.MessageAttributes.Add("MessageType", new MessageAttributeValue { StringValue = messageType, DataType = "String" });

            var response = client.Publish(publishRequest);
            return response.MessageId;
        }
    }
}
