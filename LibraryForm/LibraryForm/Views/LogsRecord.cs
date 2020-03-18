using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryForm.Views
{
    public partial class LogsRecord : UserControl
    {
        // Singleton Instance
        private static LogsRecord instance;

        public static LogsRecord Instance
        {
            get
            {
                if (instance == null)
                    instance = new LogsRecord();
                return instance;
            }
        }

        public LogsRecord()
        {
            InitializeComponent();
        }
    }
}
