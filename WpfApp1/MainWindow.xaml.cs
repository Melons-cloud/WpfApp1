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
    class HardWareInfo
        {
            #region   基础参数
     
            //本类实例
            public static readonly HardWareInfo _Instacne = new HardWareInfo();
     
            #region  0-获取电脑硬件的编号
            //CPU的参数
            private const string _CPUPara = "Win32_Processor";
            private const string _CPUIDPara = "ProcessorId"; 
     
            //主板参数
            private const string _MainBoardPara = "Win32_BaseBoard";
            private const string _MainBoardIDPara = "SerialNumber";
     
            //硬盘参数
            private const string _HardDiskPara = "Win32_PhysicalMedia";
            private const string _HardDiskIDPara = "SerialNumber";
     
            //BIOS参数
            private const string _BiosPara = "Win32_BIOS";
            private const string _BiosIDPara = "SerialNumber";
     
            #endregion
     
            #region   1-获取到电脑其他信息
     
            //系统名称
            private const string _SystemNamePara = "Win32_OperatingSystem";
            private const string _SystemName = "Name";
     
            //CPU名称
            private const string _CPUName = "Name";
            private const string _CPUAllPara = "Select * from Win32_Processor";
            private const string _CPUCoreNum = "NumberOfCores";
     
            #endregion
     
            #endregion
     
     
     
            #region   公有方法
            //本类实例
            public static HardWareInfo GetInstance
            {
                get { return _Instacne; }
            }
     
            #region  0-获取电脑硬件的编号
            /// <summary>
            /// 获取到CPU编号
            /// </summary>
            /// <returns>返回当前电脑的CPU编号</returns>
            public string GetCPUID()
            {
                string strID = null;
     
                ManagementClass mc = new ManagementClass(_CPUPara);
                ManagementObjectCollection moc = mc.GetInstances();
               
                foreach (ManagementObject mo in moc)
                {
                    strID = mo.Properties[_CPUIDPara].Value.ToString();
                    break;
                }
     
                return strID;
            }
     
            /// <summary>
            /// 获取到主板的编号
            /// </summary>
            /// <returns>返回当前电脑主板的编号</returns>
            public string GetMainBoardID()
            {
                string strID = null;
                ManagementClass mc = new ManagementClass(_MainBoardPara);
                ManagementObjectCollection moc = mc.GetInstances();
               
                foreach (ManagementObject mo in moc)
                {
                    strID = mo.Properties[_MainBoardIDPara].Value.ToString();
                    break;
                }
     
                return strID;
            }
     
            /// <summary>
            /// 获取到硬盘的编号
            /// </summary>
            /// <returns>返回当前电脑硬盘的编号</returns>
            public string GetHardDiskID()
            {
                string strID = null;
     
                ManagementClass mc = new ManagementClass(_HardDiskPara);
                ManagementObjectCollection moc = mc.GetInstances();
              
                foreach (ManagementObject mo in moc)
                {
                    strID = mo.Properties[_HardDiskIDPara].Value.ToString();
                    break;
                }
                return strID;
            }
     
            /// <summary>
            /// 获取到Bios的编号
            /// </summary>
            /// <returns>返回当前电脑Bios的编号</returns>
            public string GetBiosID()
            {
                string strID = null;
     
                ManagementClass mc = new ManagementClass(_BiosPara);
                ManagementObjectCollection moc = mc.GetInstances();
               
                foreach (ManagementObject mo in moc)
                {
                    strID = mo.Properties[_BiosIDPara].Value.ToString();
                    break;
                }
     
                return strID;
            }
     
     
     
            #endregion
     
            #region   1-获取到电脑其他信息
     
            /// <summary>
            /// 获取到系统名称
            /// </summary>
            /// <returns>返回当前电脑系统的名称</returns>
            public string GetSystemName()
            {
                string str = null;
     
                ManagementClass manag = new ManagementClass(_SystemNamePara);
                ManagementObjectCollection managCollection = manag.GetInstances();
                foreach (ManagementObject m in managCollection)
                {
                    str = m[_SystemName].ToString();
                   
                }
     
                return str;
            }
     
            /// <summary>
            /// 获取到CPU的名称
            /// </summary>
            /// <returns>返回当前电脑CPU的名称</returns>
            public string GetCPUName()
            {
                string str = null;
                ManagementClass mcCPU = new ManagementClass(_CPUPara);
                ManagementObjectCollection mocCPU = mcCPU.GetInstances();
                foreach (ManagementObject m in mocCPU)
                {
                    str = m[_CPUName].ToString();
                    
                    break;
                }
     
                return str;
            }
     
            /// <summary>
            /// 获取到当前CPU的核心数量
            /// </summary>
            /// <returns>返回当前电脑CPU的核心数量</returns>
            public int GetCPUNumber()
            {
                int coreCount = 0;
     
                foreach (var item in new System.Management.ManagementObjectSearcher(_CPUAllPara).Get())
                {
                    coreCount += int.Parse(item[_CPUCoreNum].ToString());
                }
     
                return coreCount;
            }
     
            /// <summary>
            /// 获取到系统的内存(GB)
            /// </summary>
            /// <returns>返回当前电脑内存的大小</returns>
            public float GetSystemMemorySizeOfGB()
            {
                float size = 0;
     
                ManagementObjectSearcher searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象
                searcher.Query = new SelectQuery("Win32_PhysicalMemory", "", new string[] { "Capacity" });//设置查询条件
                ManagementObjectCollection collection = searcher.Get();   //获取内存容量 
                ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();
                long capacity = 0;
                while (em.MoveNext())
                {
                    ManagementBaseObject baseObj = em.Current;
                    if (baseObj.Properties["Capacity"].Value != null)
                    {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                }
                size = (capacity / 1024 / 1024 / 1024);
     
                return size;
            }
     
            /// <summary>
            /// 获取到硬盘的存储空间(总空间、剩余空间(GB))
            /// </summary>
            /// <returns>返回当前电脑的硬盘存储空间</returns>
            public float[] GetHardDiskSpace()
            {
                float[] Space = new float[2];
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                long totalFreeSpace = 0;
                long totalDiskSize = 0;
                foreach (var drive in drives)
                {
                    if (drive.IsReady)  //判断代码运行时 磁盘是可操作作态 
                    {
                        totalFreeSpace += drive.AvailableFreeSpace;
                        totalDiskSize += drive.TotalSize;
                    }
                }
     
                //总空间(单位：GB)
                float totalSpace = totalDiskSize / 1024 / 1024 / 1024;
                //剩余空间(单位:GB)
                float surplusSpce = totalFreeSpace / 1024 / 1024 / 1024;
     
                Space[0] = totalSpace;
                Space[1] = surplusSpce;
     
                return Space;
     
            }
     
            
           
     
            /// <summary>
            /// 获取到GPU的名称
            /// </summary>
            /// <returns>返回当前电脑的GPU名称</returns>
            public string GetGPUName()
            {
                string str = null;
                ManagementClass manage = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection manageCollection = manage.GetInstances();
                foreach (ManagementObject m in manageCollection)
                {
                    str = m["VideoProcessor"].ToString().Replace("Family", "");
                    break;
                }
     
                return str;
            }
     
            /// <summary>
            ///获取到GPU的显存大小(单位：GB)
            /// </summary>
            /// <returns>返回当前电脑的GPU显存大小</returns>
            public float GetGPUMemorySize()
            {
                float size = 0;
                ManagementClass manage = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection manageCollection = manage.GetInstances();
                foreach (ManagementObject m in manageCollection)
                {
                    size = (Convert.ToInt64(m["AdapterRAM"]) / 1024 / 1024 / 1024);
                    break;
                }
     
                return size;
            }
     
            #endregion
     
            #endregion
     
     
     
            #region   私有方法
     
            //构造函数
            public HardWareInfo()
            {
     
            }
     
     
            #endregion
     
     
     
     
        }//Class_end
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
           HardWareInfo hardWareInfo =new HardWareInfo();
           
           ListBox1.Items.Add("系统名称:"+hardWareInfo.GetSystemName());
           ListBox1.Items.Add("BiosID:"+hardWareInfo.GetBiosID());
           ListBox1.Items.Add("磁盘总空间:"+hardWareInfo.GetHardDiskSpace()[0]+"GB");
           ListBox1.Items.Add("磁盘剩余空间:"+hardWareInfo.GetHardDiskSpace()[1]+"GB");
           ListBox1.Items.Add( "CPU名称:"+hardWareInfo.GetCPUName());
           ListBox1.Items.Add("CPU核心数量:"+hardWareInfo.GetCPUNumber());
          
          
           ListBox1.Items.Add("主板ID:"+hardWareInfo.GetMainBoardID());
          
           ListBox1.Items.Add("显存大小:"+ hardWareInfo.GetGPUMemorySize()+"GB");
           ListBox1.Items.Add("系统内存:"+hardWareInfo.GetSystemMemorySizeOfGB()+"GB");
           
           
           
           
            
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
            
            foreach (ManagementObject obj in objvide.Get())
            { 
                ListBox1.Items.Add("显卡 - " + obj["Name"]);
           
            }
            
            //创建ManagementObjectSearcher对象
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            //存储磁盘序列号
            //调用ManagementObjectSearcher类的Get方法取得硬盘序列号
            foreach (ManagementObject mo in searcher.Get())
            {
               //记录获得的磁盘序列号
                ListBox1.Items.Add( "硬盘序列号: "+mo["SerialNumber"].ToString().Trim());//显示硬盘序列号
               
            }
            
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
                     ListBox1.Items.Add("内存大小:" +sizeAll.ToString());
                }
            }
           
            
           
            
           

            //获取CPU序列号代码 
           
            ManagementClass mc2 = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc1 = mc2.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
               
                 ListBox1.Items.Add("CPU编号:" + mo.Properties["ProcessorId"].Value.ToString());
            }
          
           

            //获取网卡硬件地址 
            
            ManagementClass mc3 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc4 = mc3.GetInstances();
            foreach (ManagementObject mo in moc4)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                   
                    ListBox1.Items.Add("MAC地址:" + mo["MacAddress"].ToString());
                }
            }
           
            
           

            //获取IP地址 
            
            ManagementClass mc5 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc3 = mc5.GetInstances();
            foreach (ManagementObject mo in moc3)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    //st=mo["IpAddress"].ToString(); 
                    System.Array ar;
                    ar = (System.Array)(mo.Properties["IpAddress"].Value);
                    string st = ar.GetValue(0).ToString();
                    ListBox1.Items.Add("IP地址:" + st);
                    
                }
            }
           
            
            
           
            ManagementClass mc6 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc5 = mc6.GetInstances();
            foreach (ManagementObject mo in moc5)
            {             
                ListBox1.Items.Add("磁盘名称:" + (string)mo.Properties["Model"].Value);
            }
           
           

            
            ManagementClass mc7 = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc6 = mc7.GetInstances();
            foreach (ManagementObject mo in moc6)
            {
                
                
                ListBox1.Items.Add("PC类型:" + mo["SystemType"].ToString());
            }
            
            
            //计算机名
            ListBox1.Items.Add("计算机名:"+System.Environment.GetEnvironmentVariable("ComputerName"));
        }
    }
}
