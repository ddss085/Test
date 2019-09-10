using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IPChange
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        IPAddress iPAddress = new IPAddress();
        Object objectName = new Object();
        List<ListView> lists = new List<ListView>();
        string[] IPAddress = new string[4];
        string[] SubNetMask = new string[4];
        string[] GateWay = new string[4];
        public MainWindow()
        {
            InitializeComponent();
            Repeat();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(objectName.LanCardName == "")
            {
                MessageBox.Show("변경할 네트워크를 설정해주세요");
            }
            else if(IPAddress4.Text == "0")
            {
                MessageBox.Show("IP주소의 마지막 자리는 0으로 설정할 수 없습니다.");
            }
            else
            {
                objectName.IPAddress = iPAddress.Sum(IPAddress1.Text, IPAddress2.Text, IPAddress3.Text, IPAddress4.Text, objectName.IPAddress);
                objectName.SubNetMask = iPAddress.Sum(SubNetMask1.Text, SubNetMask2.Text, SubNetMask3.Text, SubNetMask4.Text, objectName.SubNetMask);
                objectName.GateWay = iPAddress.Sum(GateWay1.Text, GateWay2.Text, GateWay3.Text, GateWay4.Text, objectName.GateWay);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                iPAddress.ChangeIPAddress(objectName.LanCardName, objectName.IPAddress, objectName.SubNetMask, objectName.GateWay);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                iPAddress.IPAddressCheck(objectName.LanCardName, ref IPAddress, ref SubNetMask, ref GateWay);
                string Num1 = IPAddress[0] + "." + IPAddress[1] + "." + IPAddress[2] + "." + IPAddress[3];
                string Num2 = SubNetMask[0] + "." + SubNetMask[1] + "." + SubNetMask[2] + "." + SubNetMask[3];
                string Num3 = GateWay[0] + "." + GateWay[1] + "." + GateWay[2] + "." + GateWay[3];
                if (objectName.IPAddress != Num1)
                {
                    MessageBox.Show("IP주소가 잘 못 설정되었습니다.");
                }
                else if (objectName.SubNetMask != Num2)
                {
                    MessageBox.Show("SubNetMask주소가 잘 못 설정되었습니다.");
                }
                else if (objectName.GateWay != Num3)
                {
                    MessageBox.Show("GateWay주소가 잘 못 설정되었습니다.");
                }
                else
                {
                    MessageBox.Show("변경되었습니다.");
                }
            }
        }

        private void AdapterListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                objectName.LanCardName = lists[AdapterListView.SelectedIndex].Device;
                iPAddress.IPAddressCheck(objectName.LanCardName, ref IPAddress, ref SubNetMask, ref GateWay);
                IPAddress1.Text = IPAddress[0];
                IPAddress2.Text = IPAddress[1];
                IPAddress3.Text = IPAddress[2];
                IPAddress4.Text = IPAddress[3];
                GateWay1.Text = SubNetMask[0];
                GateWay2.Text = SubNetMask[1];
                GateWay3.Text = SubNetMask[2];
                GateWay4.Text = SubNetMask[3];
                GateWay1.Text = GateWay[0];
                GateWay2.Text = GateWay[1];
                GateWay3.Text = GateWay[2];
                GateWay4.Text = GateWay[3];
                Array.Clear(IPAddress, 0, IPAddress.Length);
                Array.Clear(SubNetMask, 0, SubNetMask.Length);
                Array.Clear(GateWay, 0, GateWay.Length);
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lists.Clear();
            Repeat();
        }

        public void Repeat()
        {
            iPAddress.NetworkList(objectName.ListName, objectName.Check = 0);
            iPAddress.NetworkList(objectName.ListDescription, objectName.Check = 1);
            iPAddress.NetworkList(objectName.ListOperationalStatus, objectName.Check = 2);
            for (int i = 0; i < objectName.ListName.Count; i++)
            {
                if(objectName.ListOperationalStatus[i] == "Up")
                {
                    objectName.ListOperationalStatus[i] = "연결됨";
                }
                else if(objectName.ListOperationalStatus[i] == "Down")
                {
                    objectName.ListOperationalStatus[i] = "연결되지 않음";
                }
                lists.Add(new ListView() { Name = objectName.ListName[i], Device = objectName.ListDescription[i], NetWork = objectName.ListOperationalStatus[i] });
            }
            AdapterListView.ItemsSource = lists;
            AdapterListView.Items.Refresh();
        }
    }
}
