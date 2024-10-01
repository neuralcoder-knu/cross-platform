using System;
using System.IO;
using API.Reader;
using API.Saver;
using Lab02;

var fileReader = new FileReader(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "INPUT.TXT")));
var fileSaver = new FileSaver(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OUTPUT.TXT")));

fileReader.CreateIfNotExists("4 6");

var task = new TileTask(fileSaver)
    .StatParams(new TileTaskInput().Read(fileReader))
    .Valid("M must be more than 2", @params => @params.M >= 2)
    .Valid("M must be less than N", @params => @params.M <= @params.N)
    .Valid("N must be more less 50", @params => @params.N <= 50);

task.Handle().WriteResult();