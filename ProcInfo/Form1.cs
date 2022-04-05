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
        private int _threadInterval =300;
        private string _idProcesses;
        private DataTable _defaultdata;
        private string _someProcProp;
        private List<string> _additionalInfoList = new List<string>();
        private Thread _additionalInfoThread;
        private bool _isStarted = true;
        public delegate void InfoListCallback(List<string> target);
        private Form infoForm;

        public mainForm()
        {
            InitializeComponent();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            LabelsSetup();
            TableSetup();
            GridSetup();
            InitializeTimer();
        }
        private void LabelsSetup()
        {
            debuglabel.BackColor = Color.Red;
            debuglabel.Text = "Data";
            processcounterlab.Text = "Процессов:";
        }
        private void GridSetup()
        {
            processGridView.GridColor = Color.Aqua;
            processGridView.DataSource = _defaultdata;
        }
        private void TableSetup()
        {
            _defaultdata = new DataTable();
            _defaultdata.Columns.Add("ID");
            _defaultdata.Columns.Add("Name");
            string[] values = new string[ProcessesData.GetProcessStringList().Count];
            foreach (string s in ProcessesData.GetProcessStringList())
            {
                values = s.Split('*');
                object[] rows = new object[] { values[0], values[1]};
                _defaultdata.Rows.Add(rows);
            }
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = _defaultdata.Columns["ID"];
            _defaultdata.PrimaryKey = PrimaryKeyColumns;
        }
        private void TableUpdate()
        {

          
        }
        private void TableRefresh()
        {
            _defaultdata.Clear();
            string[] values = new string[ProcessesData.GetProcessStringList().Count];
            foreach (string s in ProcessesData.GetProcessStringList())
            {
                values = s.Split('*');
                object[] rows = new object[] { values[0], values[1] };
                try
                {
                    _defaultdata.Rows.Add(rows);
                }
                catch (ArgumentException)
                {
                    TableSetup();
                }
                catch (ConstraintException)
                {
                    TableSetup();
                }
                catch(NoNullAllowedException)
                {
                    TableSetup();
                }
            }
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
                TableRefresh();
                _idProcesses = ProcessesData.GetIDsString();
            }
            else
            {
                //debuglabel.Text = "Wait";
            }
            processcounterlab.Text = "Процессов:" + ProcessesData.GetIDsInt().Length;
            infolab.Text = _additionalInfoThread.ThreadState.ToString();
            debuglabel.Text = _additionalInfoList.Count.ToString();
            recordscountlab.Text = "Записей" + _defaultdata.Rows.Count;
        }
        private void processGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            TableRefresh();
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
                MessageBox.Show("Thread control exсeption");
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
                            temp.Add(procs[i].Id.ToString()+"*"+procs[i].StartTime.ToString()+"*"+procs[i].Threads.Count.ToString());
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
                    temp.Add(e.ToString() + "*" + e.ToString());
                    _someProcProp = e.ToString();
                }
                catch (InvalidOperationException e)
                {
                    _someProcProp = e.ToString();
                }
                call.Invoke(temp);
                Thread.Sleep(_threadInterval);
            }
        }
        private void InfoCallBackResult(List<string> target)
        {
            _additionalInfoList.Clear();
            foreach (var tar in target)
            {
                _additionalInfoList.Add(tar);
            }
        }
        private void processGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowInfolForm(Convert.ToInt32(processGridView[0, e.RowIndex].Value.ToString()));
        }
        private void ShowInfolForm(int id)
        {
            
            if (infoForm == null)
            {
                infoForm_Init();
                infoForm.Name = id.ToString();
                infoForm.Text = "Процесс " + id;
            }
            else
            {
                infoForm = null;
                infoForm_Init();
                infoForm.Name = id.ToString();
                infoForm.Text = "Процесс " + id;
            }
            infoForm.Show();
        }
        private void infoForm_Init()
        {
            infoForm = new Form();
            infoForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            infoForm.TopLevel = true;
            infoForm.Load += new EventHandler(infoForm_Load);
            infoForm.Shown += new EventHandler(infoForm_Show);
        }
        private void infoForm_Load(object sender, EventArgs e)
        {
            infoForm.Height = 150;
            int ypadding = 10;
            infoForm.DesktopBounds = new Rectangle(Location.X, Location.Y- infoForm.Height+ ypadding, Width, infoForm.Height);
            DataGridView additionalgrid = new DataGridView();
            additionalgrid.ReadOnly=true;
            additionalgrid.Width = infoForm.Width;
            additionalgrid.Height = infoForm.Height - 50;
            additionalgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            additionalgrid.Columns.Add("StartTime", "Start Time");
            additionalgrid.Columns.Add("ThreadsCount","ThreadsCount");
            infoForm.Controls.Add(additionalgrid);
        }
        private void infoForm_Show(object sender, EventArgs e)
        {
            DataGridView dgt = (DataGridView)infoForm.Controls[0];
            string[] values = new string[2];
            int i = 0;


            foreach (var p in _additionalInfoList)
            {
                values=p.Split('*');
                if(values[0]== infoForm.Name)
                {
                    break;
                }
                i++;
            }
            values = _additionalInfoList[i].Split('*');
            object[] rows = new object[] { values[1],values[2] };
            MessageBox.Show(values[0]);
            dgt.Rows.Add(rows);
        }
    }
}
