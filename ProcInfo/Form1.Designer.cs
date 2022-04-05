namespace ProcInfo
{
    partial class mainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.debuglabel = new System.Windows.Forms.Label();
            this.maintimer = new System.Windows.Forms.Timer(this.components);
            this.processcounterlab = new System.Windows.Forms.Label();
            this.processGridView = new System.Windows.Forms.DataGridView();
            this.startBut = new System.Windows.Forms.Button();
            this.infolab = new System.Windows.Forms.Label();
            this.recordscountlab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.processGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // debuglabel
            // 
            this.debuglabel.AutoSize = true;
            this.debuglabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.debuglabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.debuglabel.Location = new System.Drawing.Point(124, 504);
            this.debuglabel.Name = "debuglabel";
            this.debuglabel.Size = new System.Drawing.Size(51, 20);
            this.debuglabel.TabIndex = 0;
            this.debuglabel.Text = "label1";
            // 
            // maintimer
            // 
            this.maintimer.Tick += new System.EventHandler(this.maintimer_Tick);
            // 
            // processcounterlab
            // 
            this.processcounterlab.AutoSize = true;
            this.processcounterlab.Location = new System.Drawing.Point(365, 479);
            this.processcounterlab.Name = "processcounterlab";
            this.processcounterlab.Size = new System.Drawing.Size(35, 13);
            this.processcounterlab.TabIndex = 1;
            this.processcounterlab.Text = "label1";
            // 
            // processGridView
            // 
            this.processGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.processGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.processGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.processGridView.Location = new System.Drawing.Point(1, 0);
            this.processGridView.Name = "processGridView";
            this.processGridView.Size = new System.Drawing.Size(457, 473);
            this.processGridView.TabIndex = 2;
            this.processGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.processGridView_CellDoubleClick);
            // 
            // startBut
            // 
            this.startBut.BackColor = System.Drawing.Color.LightCoral;
            this.startBut.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startBut.Location = new System.Drawing.Point(12, 479);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(75, 45);
            this.startBut.TabIndex = 3;
            this.startBut.Text = "Stop";
            this.startBut.UseVisualStyleBackColor = false;
          
            this.startBut.Click += new System.EventHandler(this.button1_Click);
            // 
            // infolab
            // 
            this.infolab.AutoSize = true;
            this.infolab.Location = new System.Drawing.Point(125, 479);
            this.infolab.Name = "infolab";
            this.infolab.Size = new System.Drawing.Size(35, 13);
            this.infolab.TabIndex = 4;
            this.infolab.Text = "label1";
            // 
            // recordscountlab
            // 
            this.recordscountlab.AutoSize = true;
            this.recordscountlab.Location = new System.Drawing.Point(368, 510);
            this.recordscountlab.Name = "recordscountlab";
            this.recordscountlab.Size = new System.Drawing.Size(35, 13);
            this.recordscountlab.TabIndex = 5;
            this.recordscountlab.Text = "label1";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(459, 536);
            this.Controls.Add(this.recordscountlab);
            this.Controls.Add(this.infolab);
            this.Controls.Add(this.startBut);
            this.Controls.Add(this.processGridView);
            this.Controls.Add(this.processcounterlab);
            this.Controls.Add(this.debuglabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProcViewer";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.processGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debuglabel;
        private System.Windows.Forms.Timer maintimer;
        private System.Windows.Forms.Label processcounterlab;
        private System.Windows.Forms.DataGridView processGridView;
        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Label infolab;
        private System.Windows.Forms.Label recordscountlab;
    }
}

