using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Vil_Management
{
    public partial class ToolsControl : UserControl
    {
        public ToolsControl()
        {
            InitializeComponent();
        }

        private void btnEdge_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new EdgeDriver();

            try
            {
                driver.Navigate().GoToUrl("https://cpos4.vodafoneidea.com/");
            }
            catch (Exception ex) { 
            
            }

        }
    }
}
