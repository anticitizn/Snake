namespace SnakeForms
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.scorePoints = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.slowBtn = new System.Windows.Forms.Button();
            this.medBtn = new System.Windows.Forms.Button();
            this.fastBtn = new System.Windows.Forms.Button();
            this.playerSnakeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 65;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // scorePoints
            // 
            this.scorePoints.AutoSize = true;
            this.scorePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.scorePoints.Location = new System.Drawing.Point(61, 9);
            this.scorePoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scorePoints.Name = "scorePoints";
            this.scorePoints.Size = new System.Drawing.Size(45, 20);
            this.scorePoints.TabIndex = 3;
            this.scorePoints.Text = "0000";
            // 
            // restartButton
            // 
            this.restartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.restartButton.Location = new System.Drawing.Point(623, 5);
            this.restartButton.Margin = new System.Windows.Forms.Padding(2);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(100, 28);
            this.restartButton.TabIndex = 1;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pauseButton.Location = new System.Drawing.Point(519, 5);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(2);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(100, 28);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.scoreLabel.Location = new System.Drawing.Point(11, 9);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(55, 20);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "Score:";
            // 
            // slowBtn
            // 
            this.slowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.slowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.slowBtn.Location = new System.Drawing.Point(315, 5);
            this.slowBtn.Margin = new System.Windows.Forms.Padding(2);
            this.slowBtn.Name = "slowBtn";
            this.slowBtn.Size = new System.Drawing.Size(51, 28);
            this.slowBtn.TabIndex = 5;
            this.slowBtn.Text = "Slow";
            this.slowBtn.UseVisualStyleBackColor = true;
            this.slowBtn.Click += new System.EventHandler(this.slowBtn_Click);
            // 
            // medBtn
            // 
            this.medBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.medBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.medBtn.Location = new System.Drawing.Point(370, 5);
            this.medBtn.Margin = new System.Windows.Forms.Padding(2);
            this.medBtn.Name = "medBtn";
            this.medBtn.Size = new System.Drawing.Size(62, 28);
            this.medBtn.TabIndex = 6;
            this.medBtn.Text = "Med";
            this.medBtn.UseVisualStyleBackColor = true;
            this.medBtn.Click += new System.EventHandler(this.medBtn_Click);
            // 
            // fastBtn
            // 
            this.fastBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fastBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.fastBtn.Location = new System.Drawing.Point(436, 5);
            this.fastBtn.Margin = new System.Windows.Forms.Padding(2);
            this.fastBtn.Name = "fastBtn";
            this.fastBtn.Size = new System.Drawing.Size(62, 28);
            this.fastBtn.TabIndex = 7;
            this.fastBtn.Text = "Fast";
            this.fastBtn.UseVisualStyleBackColor = true;
            this.fastBtn.Click += new System.EventHandler(this.fastBtn_Click);
            // 
            // playerSnakeBtn
            // 
            this.playerSnakeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playerSnakeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.playerSnakeBtn.Location = new System.Drawing.Point(174, 5);
            this.playerSnakeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.playerSnakeBtn.Name = "playerSnakeBtn";
            this.playerSnakeBtn.Size = new System.Drawing.Size(117, 28);
            this.playerSnakeBtn.TabIndex = 8;
            this.playerSnakeBtn.Text = "Player ON/OFF";
            this.playerSnakeBtn.UseVisualStyleBackColor = true;
            this.playerSnakeBtn.Click += new System.EventHandler(this.playerSnakeBtn_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 711);
            this.Controls.Add(this.playerSnakeBtn);
            this.Controls.Add(this.fastBtn);
            this.Controls.Add(this.medBtn);
            this.Controls.Add(this.slowBtn);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.scorePoints);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Window";
            this.Text = "Snake";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Window_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label scorePoints;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Button slowBtn;
        private System.Windows.Forms.Button medBtn;
        private System.Windows.Forms.Button fastBtn;
        private System.Windows.Forms.Button playerSnakeBtn;
    }
}

