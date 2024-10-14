using API.Reader;
using API.Saver;
using Lab03;

var fileReader = new FileReader(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "INPUT.TXT")));
var fileSaver = new FileSaver(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OUTPUT.TXT")));

fileReader.CreateIfNotExists("5\n....X\n.OOOO\n.....\nOOOO.\n@....");

var task = new Lab3Task(fileSaver)
    .Params(new Lab3Input().Read(fileReader));

task.Handle().WriteResult();