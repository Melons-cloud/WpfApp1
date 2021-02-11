using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
            
            foreach (ManagementObject obj in objvide.Get())
            { 
                ListBox1.Items.Add("显卡 - " + obj["Name"]);
           
            }
            
            //创建ManagementObjectSearcher对象
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            String strHardDiskID = null;//存储磁盘序列号
            //调用ManagementObjectSearcher类的Get方法取得硬盘序列号
            foreach (ManagementObject mo in searcher.Get())
            {
                strHardDiskID = mo["SerialNumber"].ToString().Trim();//记录获得的磁盘序列号
                break;
            }
            ListBox1.Items.Add( "硬盘序列号: "+strHardDiskID);//显示硬盘序列号
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moj = mc.GetInstances();
            foreach (ManagementObject m in moj)
            {
                ListBox1.Items.Add("磁盘大小:"+m.Properties["Size"].Value.ToString()) ;
            }

            ManagementClass mc1 = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc1.GetInstances();
            double sizeAll = 0.0;
            foreach (ManagementObject m in moc)
            {
                if (m.Properties["TotalVisibleMemorySize"].Value != null)
                {
                    sizeAll += Convert.ToDouble(m.Properties["TotalVisibleMemorySize"].Value.ToString());
                }
            }
            mc = null;
            moc.Dispose();
            ListBox1.Items.Add("内存大小:" +sizeAll.ToString());
            
           

            //获取CPU序列号代码 
            string cpuInfo = "";//cpu序列号 
            ManagementClass mc2 = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc1 = mc2.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            moc = null;
            mc = null;
            ListBox1.Items.Add("CPU编号:" + cpuInfo);

            //获取网卡硬件地址 
            string mac = "";
            ManagementClass mc3 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc4 = mc3.GetInstances();
            foreach (ManagementObject mo in moc4)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            moc = null;
            mc = null;
            ListBox1.Items.Add("MAC地址:" + mac);
           

            //获取IP地址 
            string st = "";
            ManagementClass mc5 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc3 = mc5.GetInstances();
            foreach (ManagementObject mo in moc3)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    //st=mo["IpAddress"].ToString(); 
                    System.Array ar;
                    ar = (System.Array)(mo.Properties["IpAddress"].Value);
                    st = ar.GetValue(0).ToString();
                    break;
                }
            }
            moc = null;
            mc = null;
            ListBox1.Items.Add("IP地址:"+st);
            //获取硬盘ID 
            String HDid = "";
            ManagementClass mc6 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc5 = mc6.GetInstances();
            foreach (ManagementObject mo in moc5)
            {
                HDid = (string)mo.Properties["Model"].Value;
            }
            moc = null;
            mc = null;
            ListBox1.Items.Add("磁盘名称:"+HDid);

            string st2 = "";
            ManagementClass mc7 = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc6 = mc7.GetInstances();
            foreach (ManagementObject mo in moc6)
            {
                st2 = mo["SystemType"].ToString();
            }
            moc = null;
            mc = null;
            ListBox1.Items.Add("PC类型:"+st2);
            //计算机名
            ListBox1.Items.Add("计算机名:"+System.Environment.GetEnvironmentVariable("ComputerName"));
        }
    }
}
