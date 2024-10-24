using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

var gateway = builder.AddProject<Projects.ApiGateway>("apigateway");
var signalRApp = builder.AddProject<Projects.SignalRApi>("signalrapi");
var accountsapp = builder.AddProject<Projects.AccountsApp>("accountsapp");
var posapp = builder.AddProject<Projects.PosApp>("posapp");
var emailsenderapp = builder.AddProject<Projects.EmailSenderApp>("emailsenderapp");

builder.AddNpmApp("react", "../../../frontend")
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
                                        //.WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

gateway
.WithReference(emailsenderapp)
.WithReference(accountsapp)
.WithReference(posapp);




builder.Build().Run();
