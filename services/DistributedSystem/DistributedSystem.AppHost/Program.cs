using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

var gateway = builder.AddProject<Projects.ApiGateway>("apigateway");
var accountsapp = builder.AddProject<Projects.AccountsApp>("accountsapp");
var posapp = builder.AddProject<Projects.PosApp>("posapp");
var emailsenderapp = builder.AddProject<Projects.EmailSenderApp>("emailsenderapp");

gateway
.WithReference(emailsenderapp)
.WithReference(accountsapp)
.WithReference(posapp);


builder.Build().Run();
