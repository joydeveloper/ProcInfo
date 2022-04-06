using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProcInfo
{
    public partial class mainForm : Form
    {
        private int _mainInterval = 1000;
        private int _threadInterval = 300;
        private DataTable _defaultdata;
        private string _someProcProp;
        private List<string> _additionalInfoList = new List<string>();
        private Thread _additionalInfoThread;
        private bool _isStarted = true;
        public delegate void InfoListCallback(List<string> target);
        private Form infoForm;
        private Logger logger = new Logger();
        enum LoggerRecordType
        {
            UserAction,
            ThreadEvent,
            DataEvent,
            Exeption,
            UI
        }
        public mainForm()
        {
            InitializeComponent();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            LoggerSetup();
            LabelsSetup();
            TableSetup();
            GridSetup();
            OnLoggedEvent("FormLoad " + DateTime.Now.ToString(), LoggerRecordType.UI);
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
            _defaultdata.Columns.Add("ID",Type.GetType("System.Int32"));
            _defaultdata.Columns.Add("Name");
            string[] values = new string[ProcessesData.GetProcessStringList().Count];
            foreach (string s in ProcessesData.GetProcessStringList())
            {
                values = s.Split('*');
                object[] rows = new object[] { values[0], values[1] };
                _defaultdata.Rows.Add(rows);
            }
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = _defaultdata.Columns["ID"];
            _defaultdata.PrimaryKey = PrimaryKeyColumns;
        }
        private void TableUpdate()
        {
            int[] indexes = new int[ProcessesData.GetIDsInt().Length];
            int indexcounter = 0;
            List<DataRow> deletedlist = new List<DataRow>();
            foreach (int idx in ProcessesData.GetIDsInt())
            {
                indexes[indexcounter] = idx;
                indexcounter++;
            }
            indexcounter = 0;
            if (_defaultdata.Rows.Count > indexes.Length)
            {
                for(int i=0;i<_defaultdata.Rows.Count;i++) 
                {
                  if(!indexes.Contains(_defaultdata.Rows[i].Field<int>("ID")))
                    {
                        OnLoggedEvent("Delete process"+ _defaultdata.Rows[i].Field<string>("Name"), LoggerRecordType.DataEvent);
                        _defaultdata.Rows.Remove(_defaultdata.Rows[i]);
                    }
                }
            }
            else if (_defaultdata.Rows.Count < indexes.Length)
            {
                indexcounter = 0;
                string[] values = new string[2];
                foreach (int idx in indexes)
                {
                    if (!_defaultdata.Rows.Contains(idx))
                    {
                        values = ProcessesData.GetProcessStringList()[indexcounter].Split('*');
                        object[] rows = new object[] { values[0], values[1] };
                        try
                        {
                            _defaultdata.Rows.Add(rows);
                            OnLoggedEvent("Add process Name " + rows[1], LoggerRecordType.DataEvent);
                        }
                        catch (ArgumentException)
                        {
                            TableSetup();
                        }
                        catch (ConstraintException)
                        {
                            TableSetup();
                        }
                        catch (NoNullAllowedException)
                        {
                            TableSetup();
                        }
                    }
                    indexcounter++;
                }
            }
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
                    OnLoggedEvent("ArgumentExcption", LoggerRecordType.Exeption);
                }
                catch (ConstraintException)
                {
                    TableSetup();
                    OnLoggedEvent("ConstraintException", LoggerRecordType.Exeption);
                }
                catch (NoNullAllowedException)
                {
                    TableSetup();
                    OnLoggedEvent("NoNullAlowedException", LoggerRecordType.Exeption);
                }
            }
        }
        private void OnLoggedEvent(string text, LoggerRecordType type)
        {
            switch (type)
            {
                case LoggerRecordType.UserAction:
                    logger.AddRecord("User Action");
                    logger.AddRecord(text);
                    logger.AddRecord("___________________");
                    break;
                case LoggerRecordType.ThreadEvent:
                    logger.AddRecord("Thread");
                    logger.AddRecord(text);
                    logger.AddRecord("___________________");
                    break;
                case LoggerRecordType.DataEvent:
                    logger.AddRecord("DataChanged");
                    logger.AddRecord(text);
                    logger.AddRecord("___________________");
                    break;
                case LoggerRecordType.Exeption:
                    logger.AddRecord("Exception");
                    logger.AddRecord(text);
                    logger.AddRecord("___________________");
                    break;
                case LoggerRecordType.UI:
                    logger.AddRecord("UI");
                    logger.AddRecord(text);
                    logger.AddRecord("___________________");
                    break;
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
            InitializeTimer();
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
                OnLoggedEvent("ThreadStateException", LoggerRecordType.Exeption);
            }
            finally
            {
                _additionalInfoThread.Start();
                OnLoggedEvent(_additionalInfoThread.ThreadState.ToString(), LoggerRecordType.ThreadEvent);
            }
        }
        private void LoggerSetup()
        {
            logger.CreateLog();
        }
        private void maintimer_Tick(object sender, EventArgs e)
        {
            if (_defaultdata.Rows.Count != ProcessesData.GetIDsInt().Length)
            {
                TableUpdate();
            }
            processcounterlab.Text = "Процессов:" + ProcessesData.GetIDsInt().Length;
            infolab.Text = _additionalInfoThread.ThreadState.ToString();
            debuglabel.Text = _additionalInfoList.Count.ToString();
            recordscountlab.Text = "Записей" + _defaultdata.Rows.Count;
        }
        private void processGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            TableRefresh();
            OnLoggedEvent("DataError", LoggerRecordType.Exeption);
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
                OnLoggedEvent("Thread control exсeption", LoggerRecordType.Exeption);
            }
            catch (System.Security.SecurityException s)
            {
                MessageBox.Show(s.Message);
            }
            OnLoggedEvent("Thread Stopped", LoggerRecordType.ThreadEvent);
        }
        [Obsolete]
        private void OnResumeProcess()
        {
            maintimer.Enabled = true;
            startBut.BackColor = Color.Firebrick;
            startBut.Text = "Stop";
            _isStarted = true;
            try
            {
                _additionalInfoThread.Resume();
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("Thread control exсeption");
                _additionalInfoThread.Start();
                OnLoggedEvent("Thread control exсeption", LoggerRecordType.Exeption);
            }
            catch (System.Security.SecurityException s)
            {
                MessageBox.Show(s.Message);
            }
          
            OnLoggedEvent("Thread Resumed", LoggerRecordType.ThreadEvent);
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
                        temp.Add(procs[i].Id.ToString() + "*" + procs[i].StartTime.ToString() + "*" + procs[i].Threads.Count.ToString());
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
            OnLoggedEvent("GridDClick", LoggerRecordType.UserAction);
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
                infoForm.Close();
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
            infoForm.DesktopBounds = new Rectangle(Location.X, Location.Y - infoForm.Height + ypadding, Width, infoForm.Height);
            DataGridView additionalgrid = new DataGridView();
            additionalgrid.ReadOnly = true;
            additionalgrid.Width = infoForm.Width;
            additionalgrid.Height = infoForm.Height - 50;
            additionalgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            additionalgrid.Columns.Add("StartTime", "Start Time");
            additionalgrid.Columns.Add("ThreadsCount", "ThreadsCount");
            infoForm.Controls.Add(additionalgrid);
        }
        private void infoForm_Show(object sender, EventArgs e)
        {
            DataGridView dgt = (DataGridView)infoForm.Controls[0];
            string[] values = new string[2];
            int i = 0;
            foreach (var p in _additionalInfoList)
            {
                values = p.Split('*');
                if (values[0] == infoForm.Name)
                {
                    break;
                }
                i++;
            }
            values = _additionalInfoList[i].Split('*');
            object[] rows = new object[] { values[1], values[2] };
            dgt.Rows.Add(rows);
        }
    }
}
