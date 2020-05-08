using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Threading;

namespace WpfControlNetCore
{
    public class MyButton : Button
    {
        public static readonly DependencyProperty DependencyPropertyTriggerProperty = DependencyProperty.Register(
  "DependencyPropertyTrigger", typeof(string), typeof(MyButton), new PropertyMetadata("", DependencyProperyTriggerChangedCallback));

        private static void DependencyProperyTriggerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyButton myButton = d as MyButton;
            myButton.MoreInfo = "DependencyProperyTriggerChangedCallback test";
        }

        public static readonly DependencyProperty MoreInfoProperty = DependencyProperty.Register(
"MoreInfo", typeof(string), typeof(MyButton), new PropertyMetadata(""));


        static MyButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyButton), new FrameworkPropertyMetadata(typeof(MyButton)));
        }

        public string MoreInfo
        {
            get
            {
                return (string)this.GetValue(MoreInfoProperty);
            }
            set
            {
                this.SetValue(MoreInfoProperty, value);
            }
        }

        public string DependencyPropertyTrigger
        {
            get 
            {
                return (string)this.GetValue(DependencyPropertyTriggerProperty);
            }
            set 
            {
                this.SetValue(DependencyPropertyTriggerProperty, value);
                MoreInfo = "test123";
                if (value.Contains("New Process"))
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.UseShellExecute = false;
                    start.CreateNoWindow = false;
                    string exeFile = System.Reflection.Assembly.GetAssembly(this.GetType()).Location;
                    exeFile = new System.IO.DirectoryInfo(exeFile).Parent.FullName + @"\Design\WPFControlNetCore.ConsoleApp.exe";
                    start.FileName = exeFile;
                    this.SetValue(MoreInfoProperty, exeFile);

                    start.EnvironmentVariables["MyButtonText"] = this.Content as string;
                    start.RedirectStandardOutput = true; // set to true to read console app StandardOutput below

                    using (Process process = Process.Start(start))
                    {
                        // Read resulting text from the NetCore console app process with the StreamReader
                        using (System.IO.StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd().TrimEnd('\r', '\n');
                        }
                    }
                }
                else if (value.Contains("New Thread"))
                {
                    Thread t = new Thread(() => 
                    {
                        var netCoreWPFWindow = new NetCoreWPFWindow();
                        netCoreWPFWindow.ShowDialog();
                    });
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                    t.Join();
                }
                else if (value.Contains("New Application"))
                {
                    var netCoreWPFWindow = new NetCoreWPFWindow();
                    Application app = new Application();
                    app.Run(netCoreWPFWindow);
                    this.Content = netCoreWPFWindow.MyButtonText;
                }
                else
                {
                    var netCoreWPFWindow = new NetCoreWPFWindow();
                    netCoreWPFWindow.ShowDialog();
                    //Just try to launch browser window
                    //string url = @"https://www.cnn.com";
                    ////Process.Start(url); This doesn't work in .Net Core
                    //Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
            }
        }
    }
}
