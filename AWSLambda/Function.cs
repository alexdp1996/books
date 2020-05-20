using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using EntityModels;
using Newtonsoft.Json;
using System.Linq;
using Dapper.FluentMap;
using AWSLambda.Maps;
using Dapper.FluentMap.Dommel;
using System.Data.SqlClient;
using Dommel;
using System.Diagnostics;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda
{
    public class Function
    {
        private string ConnectionString { get; } = Environment.GetEnvironmentVariable("ConnectionString");

        static Function()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AuthorMap());
                config.AddMap(new BookMap());
                config.ForDommel();
            });
        }

        public void BookHandler(SQSEvent sqsEvent, ILambdaContext context)
        {
            var entities = sqsEvent.Records.Select(r => JsonConvert.DeserializeObject<BookEM>(r.Body));
            using (var connection = new SqlConnection(ConnectionString))
            {
                foreach (var e in entities)
                {
                    var result = connection.Insert(e);
                }
            }
        }

        public void AuthorHandler(SQSEvent sqsEvent, ILambdaContext context)
        {
            var entities = sqsEvent.Records.Select(r => JsonConvert.DeserializeObject<AuthorEM>(r.Body));
            using (var connection = new SqlConnection(ConnectionString))
            {
                foreach (var e in entities)
                {
                    var result = connection.Insert(e);
                }
            }
        }
    }
}
