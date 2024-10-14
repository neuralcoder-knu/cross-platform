using API.Reader;
using API.Saver;
using Lab01;

var fileReader = new FileReader(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "INPUT.TXT")));
var fileSaver = new FileSaver(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OUTPUT.TXT")));

fileReader.CreateIfNotExists("56");

var task = new LuckyNumbersTask(fileSaver)
    .Params(new LuckyNumbersInput().Read(fileReader))
    .Valid("N must be less than 1032", @params => @params.N is >= 0 and <= 1032);

task.Handle().WriteResult();