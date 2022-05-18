using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;
using ServerAPI;
using System.Web;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/polls", () =>
{
    StreamReader r = new StreamReader("data.json");
    string jsonString = r.ReadToEnd();
    var myobjList = JsonConvert.DeserializeObject<List<PollsTest>>(jsonString);
    var myObj = myobjList.Where(x => x.id > 0).ToList();

    return (myObj);
})
.WithName("Polls");

object CheckID(int i)
{
    StreamReader r = new StreamReader("data.json");
    string jsonString = r.ReadToEnd();
    var myobjList = JsonConvert.DeserializeObject<List<PollList>>(jsonString);
    var myObj = myobjList.Where(x => x.id == i).ToList();

    return (myObj);
}

app.MapGet("/polls/{id}", ([FromRoute] int id) =>
{
    int i = id;
    var obj = CheckID(i);
    return (obj);
})
.WithName("Poll/:id");


app.MapPost("/polls/{id}/vote/{id2}", ([FromRoute] int id, int id2) =>
{
    int a = id2-1;
    string json = File.ReadAllText("data.json");
    StreamReader r = new StreamReader("data.json");
    string jsonString = r.ReadToEnd();
    var myobjList = JsonConvert.DeserializeObject<List<PollList>>(jsonString);

    foreach (var polls in myobjList)
    {
        if(polls.id == id)
        {
            polls.options[a].votes = polls.options[a].votes + 1;
        }
    }
    string newJson = JsonConvert.SerializeObject(myobjList);
    string output = Newtonsoft.Json.JsonConvert.SerializeObject(myobjList, Newtonsoft.Json.Formatting.Indented);

    r.Close();
    File.WriteAllText("data.json", output);

    return (output);
})
.WithName("polls/id/vote/id");


app.MapPost("/polls/add", ([FromBody] PollList value) =>
{
    string json = File.ReadAllText("data.json");
    var myobjList = JsonConvert.DeserializeObject<List<PollList>>(json);
    var obj = JsonConvert.SerializeObject(value);
    var newobj = new PollList();
    int a = 0;

    foreach (var id in myobjList)
    {
        a++;
    }

    foreach (var id in obj)
    {
        newobj.id = a +1 ;
        newobj.title = value.title;
        newobj.options = value.options;
        if (newobj.options != null && newobj.options.Length != 0)
        {
            for(int i = 0; i < newobj.options.Length; i++) {

                newobj.options[i].id = i + 1;
                newobj.options[i].title = value.options[i].title;
                newobj.options[i].votes = 0;
                
            }
        }
    };    
    myobjList.Add(newobj);    

    string output = Newtonsoft.Json.JsonConvert.SerializeObject(myobjList, Newtonsoft.Json.Formatting.Indented);
    File.WriteAllText("data.json", output);

    return (myobjList);
})
.WithName("polls/add");

app.Run();