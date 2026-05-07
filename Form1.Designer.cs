namespace BirthdayReminderW
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelSystemCheck = new Label();
            labelName = new Label();
            buttonClose = new Button();
            SuspendLayout();
            // 
            // labelSystemCheck
            // 
            labelSystemCheck.AutoSize = true;
            labelSystemCheck.Font = new Font("Courier New", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSystemCheck.Location = new Point(50, 27);
            labelSystemCheck.Name = "labelSystemCheck";
            labelSystemCheck.Size = new Size(205, 30);
            labelSystemCheck.TabIndex = 0;
            labelSystemCheck.Text = "System Check";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Courier New", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelName.Location = new Point(111, 88);
            labelName.Name = "labelName";
            labelName.Size = new Size(171, 39);
            labelName.TabIndex = 1;
            labelName.Text = "Message";
            // 
            // buttonClose
            // 
            buttonClose.Font = new Font("Courier New", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonClose.Location = new Point(509, 184);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(126, 53);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 261);
            Controls.Add(buttonClose);
            Controls.Add(labelName);
            Controls.Add(labelSystemCheck);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Birthday Reminder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSystemCheck;
        private Label labelName;
        private Button buttonClose;
    }
}
