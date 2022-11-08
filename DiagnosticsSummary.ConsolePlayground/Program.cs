using DiagnosticsSummary.ConsolePlayground;
using Newtonsoft;
using Newtonsoft.Json;
//var parser = new DiagnosticRulesParser();
//string rules = File.ReadAllText("test.txt");
//parser.Parse(rules);

//IRepository<SomeInfo> rep = new EfRepository<SomeInfo>(new DataContext(new DbContextOptionsBuilder()
//                                                .UseSqlite("Data Source=DiagnosticsSummary.db").Options));
//await rep.CreateAsync(new SomeInfo() { InfoInt = 1, InfoString ="str", InfoInt2 = 2});
//await rep.CreateAsync(new SomeInfo() { InfoInt = 11, InfoString = "str55", InfoInt2 = 55 });
//(await rep.ReadAll()).Match(
//    (e) => Console.WriteLine(e.Message),
//    (infos) => infos.ForEach(info => Console.WriteLine(info.InfoInt + " " +
//    info.InfoString + " " + info.InfoInt2))
//    );

//using System.Text.Json.Nodes;

//Dictionary<(int start, int end), string> ValueInterpreter = new Dictionary<(int start, int end), string>()
//{
//    {(0, 3), "Низкая" },
//    {(4, 5), "Средняя" },
//    {(6, 10), "Высокая" }
//};

//var res = JsonConvert.SerializeObject(ValueInterpreter, new DictionaryJsonConverter());
//Console.WriteLine(res);
//var res2 = JsonConvert.DeserializeObject<Dictionary<(int start, int end), string>>(res, new DictionaryJsonConverter());
//Console.WriteLine();
var res = "".Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
foreach(var r in res)
{
    Console.WriteLine(r);
}
Console.WriteLine();