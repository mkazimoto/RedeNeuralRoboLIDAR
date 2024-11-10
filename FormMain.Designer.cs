namespace RedeNeuralTreinamento
{
  partial class FormMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.pbStatus = new System.Windows.Forms.PictureBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblSaidaUp = new System.Windows.Forms.Label();
      this.lblSaidaRight = new System.Windows.Forms.Label();
      this.lblSaidaLeft = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(32, 81);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(1065, 761);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 10;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // pbStatus
      // 
      this.pbStatus.BackColor = System.Drawing.Color.Black;
      this.pbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pbStatus.Location = new System.Drawing.Point(34, 65);
      this.pbStatus.Name = "pbStatus";
      this.pbStatus.Size = new System.Drawing.Size(28, 13);
      this.pbStatus.TabIndex = 1;
      this.pbStatus.TabStop = false;
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Location = new System.Drawing.Point(68, 65);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(41, 13);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Parado";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(32, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(179, 39);
      this.label1.TabIndex = 3;
      this.label1.Text = "Pressione: \r\n1 - Iniciar gravação   \r\n2 - Iniciar treinamento da rede neural";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(602, 22);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Forward:";
      // 
      // lblSaidaUp
      // 
      this.lblSaidaUp.AutoSize = true;
      this.lblSaidaUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSaidaUp.Location = new System.Drawing.Point(605, 48);
      this.lblSaidaUp.Name = "lblSaidaUp";
      this.lblSaidaUp.Size = new System.Drawing.Size(23, 15);
      this.lblSaidaUp.TabIndex = 5;
      this.lblSaidaUp.Text = "Up";
      // 
      // lblSaidaRight
      // 
      this.lblSaidaRight.AutoSize = true;
      this.lblSaidaRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSaidaRight.Location = new System.Drawing.Point(703, 48);
      this.lblSaidaRight.Name = "lblSaidaRight";
      this.lblSaidaRight.Size = new System.Drawing.Size(34, 15);
      this.lblSaidaRight.TabIndex = 6;
      this.lblSaidaRight.Text = "Right";
      // 
      // lblSaidaLeft
      // 
      this.lblSaidaLeft.AutoSize = true;
      this.lblSaidaLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSaidaLeft.Location = new System.Drawing.Point(802, 48);
      this.lblSaidaLeft.Name = "lblSaidaLeft";
      this.lblSaidaLeft.Size = new System.Drawing.Size(27, 15);
      this.lblSaidaLeft.TabIndex = 7;
      this.lblSaidaLeft.Text = "Left";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(700, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Turn Right:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(799, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Turn Left:";
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1135, 866);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lblSaidaLeft);
      this.Controls.Add(this.lblSaidaRight);
      this.Controls.Add(this.lblSaidaUp);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pbStatus);
      this.Controls.Add(this.pictureBox1);
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Rede Neural do Robô com sensor LIDAR";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.PictureBox pbStatus;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblSaidaUp;
    private System.Windows.Forms.Label lblSaidaRight;
    private System.Windows.Forms.Label lblSaidaLeft;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
  }
}

