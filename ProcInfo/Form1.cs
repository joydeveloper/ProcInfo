using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProcInfo
{
    public partial class mainForm : Form
    {
        private int _mainInterval = 1000;
        private string _idProcesses;
        private DataTable _defaultdata;
        private string _someProcProp;
        private List<string> _additionalInfoList = new List<string>();
        private Thread _additionalInfoThread;
        private bool _isStarted = true;
        public delegate void InfoListCallback(List<string> target);
        public mainForm()
        {
            InitializeComponent();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            LabelsSetup();
            TableSetup();
            InitializeTimer();
        }
        private void LabelsSetup()
        {
            debuglabel.BackColor = Color.Red;
            debuglabel.Text = "Data";
            processcounterlab.Text = "Процессов:";
        }
        private void TableSetup()
        {
            _defaultdata = new DataTable();
            _defaultdata.Columns.Add("ID");
            _defaultdata.Columns.Add("Name");
            _defaultdata.Rows.Add("a", "b");
            processGridView.DataSource = _defaultdata;
        }
        private void InitializeTimer()
        {
            maintimer.Interval = _mainInterval;
            maintimer.Enabled = true;
        }
        private void mainForm_Shown(object sender, EventArgs e)
        {
            ThreadSetup();
        }
        private void ThreadSetup()
        {
            _additionalInfoThread = new Thread(new ThreadStart(GetProcessList));
            try
            {
                _additionalInfoThread.IsBackground = true;
            }
            catch (ThreadStateException)
            {
                _additionalInfoThread.Start();
            }
            finally
            {
                _additionalInfoThread.Start();
            }
        }
        private void maintimer_Tick(object sender, EventArgs e)
        {
            if (_idProcesses != ProcessesData.GetIDsString())
            {
                // debuglabel.Text = "Changed";
                _idProcesses = ProcessesData.GetIDsString();
            }
            else
            {
                //debuglabel.Text = "Wait";
            }
            processcounterlab.Text = "Процессов:" + ProcessesData.GetIDsInt().Length;
            infolab.Text = _additionalInfoThread.ThreadState.ToString();
            debuglabel.Text = _additionalInfoList.Count.ToString();
        }
        private void processGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            TableSetup();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                OnStopProcess();
            }
            else
            {
                OnResumeProcess();
            }
        }
        [Obsolete]
        private void OnStopProcess()
        {
            startBut.BackColor = SystemColors.MenuHighlight;
            startBut.Text = "Play";
            _isStarted = false;
            try
            {
                _additionalInfoThread.Suspend();
            }
            catch (ThreadStateException)
            {
                _additionalInfoThread.Resume();
                MessageBox.Show("Thread control exeption");
                throw;
            }
            catch (System.Security.SecurityException s)
            {
                MessageBox.Show(s.Message);
            }
        }
        [Obsolete]
        private void OnResumeProcess()
        {
            maintimer.Enabled = true;
            startBut.BackColor = Color.Firebrick;
            startBut.Text = "Stop";
            _isStarted = true;
            _additionalInfoThread.Resume();
        }
        private void GetProcessList()
        {
            InfoListCallback call = new InfoListCallback(InfoCallBackResult);
            List<string> temp = new List<string>();
            while (_isStarted)
            {
                temp.Clear();
                int i = 0;
                try
                {
                    var procs = ProcessesData.GetProcessList();
                    foreach (var proc in procs)
                    {
                        temp.Add(procs[i].Id.ToString());
                        i++;
                    }
                }
                catch (PlatformNotSupportedException e)
                {
                    _someProcProp = e.ToString();
                }
                catch (NotSupportedException e)
                {
                    _someProcProp = e.ToString();
                }
                catch (Win32Exception e)
                {
                    _someProcProp = e.ToString();
                }
                catch (InvalidOperationException e)
                {
                    _someProcProp = e.ToString();
                }
                call.Invoke(temp);
                Thread.Sleep(500);
            }
        }
        private void InfoCallBackResult(List<string> target)
        {

            _additionalInfoList = target;
        }
        private void GetProcessInfo(int id)
        {

        }

        private void processGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
