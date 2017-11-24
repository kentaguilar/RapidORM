using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace RapidORM.Helpers
{
    public static class FormHelper
    {
        /// <summary>
        /// Show windows form message box along with your custom message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="hasTitle"></param>
        public static void ShowMessageBox(string message, bool hasTitle = true)
        {
            MessageBox.Show(message, (hasTitle) ? ConfigurationManager.AppSettings["AppName"] : "");
        }
    }
}
 