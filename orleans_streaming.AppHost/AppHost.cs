var builder = DistributedApplication.CreateBuilder(args);

var nats = builder.AddNats("nats")
                  .WithJetStream();

var redis = builder.AddRedis("redis");

var orleans = builder.AddOrleans("orleans")
                     .WithClustering(redis)
                     .WithGrainStorage("storage", redis)
                     .WithStreaming(nats);

var apiService = builder.AddProject<Projects.orleans_streaming_ApiService>("apiservice")
                        .WithReference(orleans)
                        .WithHttpHealthCheck("/health")
                        .WithReference(nats)
                        .WaitFor(redis);

builder.AddProject<Projects.orleans_streaming_Web>("webfrontend")
       .WithExternalHttpEndpoints()
       .WithHttpHealthCheck("/health")
       .WithReference(apiService)
       .WithReference(orleans)
       .WaitFor(apiService);

builder.Build().Run();
