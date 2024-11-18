sudo apt-get update
sudo apt-get upgrade -y

sudo apt-get install -y wget apt-transport-https software-properties-common

wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

dotnet --version

dotnet nuget add source http://192.168.56.1:5555/v3/index.json -n Baget
dotnet tool install --global MKoval --version 0.0.1

cd /vagrant/Lab4

dotnet run version