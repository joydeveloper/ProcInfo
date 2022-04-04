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
            this.SuspendLayout();
            // 
            // debuglabel
            // 
            this.debuglabel.AutoSize = true;
            this.debuglabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.debuglabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.debuglabel.Location = new System.Drawing.Point(311, 382);
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
            this.processcounterlab.Location = new System.Drawing.Point(12, 382);
            this.processcounterlab.Name = "processcounterlab";
            this.processcounterlab.Size = new System.Drawing.Size(35, 13);
            this.processcounterlab.TabIndex = 1;
            this.processcounterlab.Text = "label1";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.processcounterlab);
            this.Controls.Add(this.debuglabel);
            this.Name = "mainForm";
            this.Text = "ProcViewer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debuglabel;
        private System.Windows.Forms.Timer maintimer;
        private System.Windows.Forms.Label processcounterlab;
    }
}

