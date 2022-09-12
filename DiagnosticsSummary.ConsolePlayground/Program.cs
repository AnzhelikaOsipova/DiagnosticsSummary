using DiagnosticsSummary.Common;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.ConsolePlayground;
using DiagnosticsSummary.DataLayer;
using DiagnosticsSummary.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;

var parser = new DiagnosticRulesParser();
string rules = File.ReadAllText("test.txt");
parser.Parse(rules);