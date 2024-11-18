curl -o dotnet-installer.exe https://download.visualstudio.microsoft.com/download/pr/965b9b88-5db8-4c80-8ee6-7504db2da2fc/88589.7bff3c43c4ef599233e0f5fcb4c041eb/dotnet-sdk-7.0.100-win-x64.exe

dotnet-installer.exe /quiet /install

setx PATH "%PATH%;C:\Program Files\dotnet"

dotnet --version

dotnet nuget add source http://192.168.56.1:5555/v3/index.json -n Baget
dotnet tool install --global MKoval --version 0.0.1

cd C:\proj

dotnet run --project Lab4 --help
