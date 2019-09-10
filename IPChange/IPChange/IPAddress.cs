using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace IPChange
{
    class IPAddress
    {

        public bool ChangeIPAddress(string sourceDescription, string sourceIPAddress, string sourceSubnetMask, string sourceGateway)
        {
            //Win32_NetworkAdapterConfiguration 네트워크 설정
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            //
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 설정할 네트워크 어탭터 선택
                string description = managementObject["Description"] as string;
                // 설정할 네트워크 어탭터 속성 중에서 iv4 선택
                if (string.Compare(description, sourceDescription, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    try
                    {
                        ManagementBaseObject setGatewaysManagementBaseObject = managementObject.GetMethodParameters("SetGateways");
                        // 기본 게이트 웨이
                        setGatewaysManagementBaseObject["DefaultIPGateway"] = new string[] { sourceGateway };
                        // 우선 순위
                        setGatewaysManagementBaseObject["GatewayCostMetric"] = new int[] { 1 };
                        ManagementBaseObject enableStaticManagementBaseObject = managementObject.GetMethodParameters("EnableStatic");
                        // IP 주소
                        enableStaticManagementBaseObject["IPAddress"] = new string[] { sourceIPAddress };
                        // 서브넷 마스크
                        enableStaticManagementBaseObject["SubnetMask"] = new string[] { sourceSubnetMask };
                        // 고정IP 변경
                        managementObject.InvokeMethod("EnableStatic", enableStaticManagementBaseObject, null);
                        managementObject.InvokeMethod("SetGateways", setGatewaysManagementBaseObject, null);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public List<string> NetworkList(List<string> List,int Check)
        {
            List.Clear();
            NetworkInterface[] networklist = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface network in networklist)
            {
                if (network.Supports(NetworkInterfaceComponent.IPv4) == true && network.Name != "Loopback Pseudo-Interface 1")
                {
                    if (Check == 0)
                    {
                        List.Add(network.Name);
                    }
                    else if(Check == 1)
                    {
                        List.Add(network.Description);
                    }
                    else if(Check == 2)
                    {
                        List.Add(network.OperationalStatus.ToString());
                    }
                }
            }
            return List;
        }
        public string Sum(string S1, string S2, string S3, string S4, string SumAll)
        {
            SumAll = S1 + "." + S2 + "." + S3 + "." + S4;
            return SumAll;
        }
        public void IPAddressCheck(string LanCardName,ref string[] IPAddress,ref string[] SubNetMask,ref string[] GateWay)
        {
            NetworkInterface[] networklist = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface network in networklist)
            {
                if (string.Compare(LanCardName, network.Description, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (network.Supports(NetworkInterfaceComponent.IPv4) == true)
                    {
                        foreach (UnicastIPAddressInformation AdressIP in network.GetIPProperties().UnicastAddresses)
                        {
                            if (AdressIP.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                string [] IP = AdressIP.Address.ToString().Split(new string[] { "." }, StringSplitOptions.None);
                                IPAddress[0] = IP[0];
                                IPAddress[1] = IP[1];
                                IPAddress[2] = IP[2];
                                IPAddress[3] = IP[3];

                                string[] SubNet = AdressIP.IPv4Mask.ToString().Split(new string[] { "." }, StringSplitOptions.None);
                                SubNetMask[0] = SubNet[0];
                                SubNetMask[1] = SubNet[1];
                                SubNetMask[2] = SubNet[2];
                                SubNetMask[3] = SubNet[3];
                            }
                        }

                        foreach (GatewayIPAddressInformation GateWayIP in network.GetIPProperties().GatewayAddresses)
                        {
                            if (GateWayIP.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                string[] Gate = GateWayIP.Address.ToString().Split(new string[] { "." }, StringSplitOptions.None);
                                GateWay[0] = Gate[0];
                                GateWay[1] = Gate[1];
                                GateWay[2] = Gate[2];
                                GateWay[3] = Gate[3];
                            }
                        }
                    }
                }
            }
        }
    }
}
