using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

var accountsapp = builder.AddProject<Projects.AccountsApp>("accountsapp");
var posapp = builder.AddProject<Projects.PosApp>("posapp");
builder.AddProject<Projects.EmailSenderApp>("emailsenderapp")
    .WithReference(accountsapp)
    .WithReference(posapp);

builder.Build().Run();
