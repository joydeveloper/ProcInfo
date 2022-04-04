using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProcInfo
{
    public partial class mainForm : Form
    {
        private int _mainInterval = 1000;
        private string _idProcesses;
        public mainForm()
        {
            InitializeComponent();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            debuglabel.BackColor = Color.Red;
            InitializeTimer();
            debuglabel.Text = "Data";
            processcounterlab.Text = "Процессов:";
        }
        private void mainForm_Shown(object sender, EventArgs e)
        {
            _idProcesses = ProcessesData.GetIDsString();
        }
        private void maintimer_Tick(object sender, EventArgs e)
        {


           
            if (_idProcesses!= ProcessesData.GetIDsString())
            {
                debuglabel.Text = "Wait";
                _idProcesses = ProcessesData.GetIDsString();
            }
            else
            {
                debuglabel.Text = "Wait";
            }
           // debuglabel.Text =new System.Text.RegularExpressions.Regex(@"\*").Replace(ProcessesData.GetIDsString()," ");
            processcounterlab.Text = "Процессов:" + ProcessesData.GetIDsInt().Length;
        }
        private void InitializeTimer()
        {
            maintimer.Interval = _mainInterval;
            maintimer.Enabled = true;
        }
    }
}
