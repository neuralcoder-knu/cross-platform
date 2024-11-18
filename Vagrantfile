Vagrant.configure("2") do |config|
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "bento/ubuntu-22.04-arm64"
    ubuntu.vm.box_version = "202401.31.0"
    ubuntu.vm.hostname = "ubuntu-vm"
    ubuntu.vm.network "forwarded_port", guest: 5555, host: 6666
    ubuntu.vm.network "private_network", type: "static", ip: "192.168.56.10"
    ubuntu.vm.provider :vmware_desktop do |vmware|
    	vmware.gui = true
    	vmware.cpus = 2
    	vmware.vmx["ethernet0.virtualdev"] = "vmxnet3"
    	vmware.ssh_info_public = true
    	vmware.linked_clone = false 
    end
    ubuntu.vm.provision "shell", path: "ubuntu-provision.sh"
  end

  config.vm.define "windows" do |windows|
    windows.vm.box = "pipegz/Windows11ARM"
    windows.vm.box_version = "1.0.1"
    windows.vm.hostname = "windows-vm"
    windows.vm.network "forwarded_port", guest: 5555, host: 7777
    windows.vm.network "private_network", type: "static", ip: "192.168.56.20"
    windows.vm.provider :vmware_desktop do |vmware|
      vmware.gui = true
      vmware.cpus = 4
      vmware.vmx["ethernet0.virtualdev"] = "vmxnet3"
      vmware.ssh_info_public = true
      vmware.linked_clone = false
    end
    windows.vm.synced_folder ".", "C:/proj"
    windows.vm.provision "shell", path: "windows-provision.sh"
  end
end
