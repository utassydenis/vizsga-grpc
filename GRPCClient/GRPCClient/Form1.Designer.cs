namespace GRPCClient
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
            this.loginBtn = new System.Windows.Forms.Button();
            this.userTxtBx = new System.Windows.Forms.TextBox();
            this.passTxtBx = new System.Windows.Forms.TextBox();
            this.connStringLbl = new System.Windows.Forms.Label();
            this.listBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codeTxtBx = new System.Windows.Forms.TextBox();
            this.nameTxtBx = new System.Windows.Forms.TextBox();
            this.priceTxtBx = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.resultLbl = new System.Windows.Forms.Label();
            this.bidBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.registerBtn = new System.Windows.Forms.Button();
            this.registerUserTxtBx = new System.Windows.Forms.TextBox();
            this.registerPswrdTxtBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(12, 22);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 0;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // userTxtBx
            // 
            this.userTxtBx.Location = new System.Drawing.Point(117, 22);
            this.userTxtBx.Name = "userTxtBx";
            this.userTxtBx.Size = new System.Drawing.Size(100, 23);
            this.userTxtBx.TabIndex = 2;
            // 
            // passTxtBx
            // 
            this.passTxtBx.Location = new System.Drawing.Point(117, 65);
            this.passTxtBx.Name = "passTxtBx";
            this.passTxtBx.PasswordChar = '*';
            this.passTxtBx.Size = new System.Drawing.Size(100, 23);
            this.passTxtBx.TabIndex = 3;
            // 
            // connStringLbl
            // 
            this.connStringLbl.AutoSize = true;
            this.connStringLbl.Location = new System.Drawing.Point(12, 426);
            this.connStringLbl.Name = "connStringLbl";
            this.connStringLbl.Size = new System.Drawing.Size(46, 15);
            this.connStringLbl.TabIndex = 4;
            this.connStringLbl.Text = "Session";
            // 
            // listBtn
            // 
            this.listBtn.Location = new System.Drawing.Point(676, 349);
            this.listBtn.Name = "listBtn";
            this.listBtn.Size = new System.Drawing.Size(75, 23);
            this.listBtn.TabIndex = 5;
            this.listBtn.Text = "Lista";
            this.listBtn.UseVisualStyleBackColor = true;
            this.listBtn.Click += new System.EventHandler(this.listBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(495, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(444, 280);
            this.dataGridView1.TabIndex = 6;
            // 
            // codeTxtBx
            // 
            this.codeTxtBx.Location = new System.Drawing.Point(361, 42);
            this.codeTxtBx.Name = "codeTxtBx";
            this.codeTxtBx.Size = new System.Drawing.Size(100, 23);
            this.codeTxtBx.TabIndex = 7;
            // 
            // nameTxtBx
            // 
            this.nameTxtBx.Location = new System.Drawing.Point(361, 81);
            this.nameTxtBx.Name = "nameTxtBx";
            this.nameTxtBx.Size = new System.Drawing.Size(100, 23);
            this.nameTxtBx.TabIndex = 8;
            // 
            // priceTxtBx
            // 
            this.priceTxtBx.Location = new System.Drawing.Point(361, 127);
            this.priceTxtBx.Name = "priceTxtBx";
            this.priceTxtBx.Size = new System.Drawing.Size(100, 23);
            this.priceTxtBx.TabIndex = 9;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(377, 173);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 10;
            this.addBtn.Text = "Hozzáad";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // resultLbl
            // 
            this.resultLbl.AutoSize = true;
            this.resultLbl.Location = new System.Drawing.Point(663, 426);
            this.resultLbl.Name = "resultLbl";
            this.resultLbl.Size = new System.Drawing.Size(39, 15);
            this.resultLbl.TabIndex = 11;
            this.resultLbl.Text = "Result";
            // 
            // bidBtn
            // 
            this.bidBtn.Location = new System.Drawing.Point(377, 215);
            this.bidBtn.Name = "bidBtn";
            this.bidBtn.Size = new System.Drawing.Size(75, 23);
            this.bidBtn.TabIndex = 12;
            this.bidBtn.Text = "Bid";
            this.bidBtn.UseVisualStyleBackColor = true;
            this.bidBtn.Click += new System.EventHandler(this.bidBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Location = new System.Drawing.Point(12, 65);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(75, 23);
            this.logoutBtn.TabIndex = 13;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // modifyBtn
            // 
            this.modifyBtn.Location = new System.Drawing.Point(377, 254);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(75, 23);
            this.modifyBtn.TabIndex = 14;
            this.modifyBtn.Text = "Modify";
            this.modifyBtn.UseVisualStyleBackColor = true;
            this.modifyBtn.Click += new System.EventHandler(this.modifyBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(377, 299);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 15;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // registerBtn
            // 
            this.registerBtn.Location = new System.Drawing.Point(12, 309);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(88, 23);
            this.registerBtn.TabIndex = 16;
            this.registerBtn.Text = "Regisztráció";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // registerUserTxtBx
            // 
            this.registerUserTxtBx.Location = new System.Drawing.Point(117, 284);
            this.registerUserTxtBx.Name = "registerUserTxtBx";
            this.registerUserTxtBx.Size = new System.Drawing.Size(100, 23);
            this.registerUserTxtBx.TabIndex = 17;
            // 
            // registerPswrdTxtBx
            // 
            this.registerPswrdTxtBx.Location = new System.Drawing.Point(117, 328);
            this.registerPswrdTxtBx.Name = "registerPswrdTxtBx";
            this.registerPswrdTxtBx.Size = new System.Drawing.Size(100, 23);
            this.registerPswrdTxtBx.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Price";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.registerPswrdTxtBx);
            this.Controls.Add(this.registerUserTxtBx);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.modifyBtn);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.bidBtn);
            this.Controls.Add(this.resultLbl);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.priceTxtBx);
            this.Controls.Add(this.nameTxtBx);
            this.Controls.Add(this.codeTxtBx);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBtn);
            this.Controls.Add(this.connStringLbl);
            this.Controls.Add(this.passTxtBx);
            this.Controls.Add(this.userTxtBx);
            this.Controls.Add(this.loginBtn);
            this.Name = "Form1";
            this.Text = "GRPC Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button loginBtn;
        private TextBox userTxtBx;
        private TextBox passTxtBx;
        private Label connStringLbl;
        private Button listBtn;
        private DataGridView dataGridView1;
        private TextBox codeTxtBx;
        private TextBox nameTxtBx;
        private TextBox priceTxtBx;
        private Button addBtn;
        private Label resultLbl;
        private Button bidBtn;
        private Button logoutBtn;
        private Button modifyBtn;
        private Button deleteBtn;
        private Button registerBtn;
        private TextBox registerUserTxtBx;
        private TextBox registerPswrdTxtBx;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}