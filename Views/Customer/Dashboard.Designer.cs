namespace QLBS.Views.Customer
{
    partial class Dashboard
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
            this.btnBuy = new System.Windows.Forms.Button();
            this.flowBooks = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.btnCart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(37, 20);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(97, 32);
            this.btnBuy.TabIndex = 0;
            this.btnBuy.Text = "Sách đã mua";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // flowBooks
            // 
            this.flowBooks.AutoScroll = true;
            this.flowBooks.Location = new System.Drawing.Point(37, 96);
            this.flowBooks.Name = "flowBooks";
            this.flowBooks.Size = new System.Drawing.Size(794, 377);
            this.flowBooks.TabIndex = 1;
            this.flowBooks.Paint += new System.Windows.Forms.PaintEventHandler(this.flowBooks_Paint);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(717, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(114, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(158, 20);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(124, 32);
            this.btnInfo.TabIndex = 3;
            this.btnInfo.Text = "Thông tin cá nhân";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(552, 27);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(121, 21);
            this.cbCategory.TabIndex = 4;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // btnCart
            // 
            this.btnCart.Location = new System.Drawing.Point(312, 20);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(114, 32);
            this.btnCart.TabIndex = 5;
            this.btnCart.Text = "Giỏ hàng";
            this.btnCart.UseVisualStyleBackColor = true;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(866, 494);
            this.Controls.Add(this.btnCart);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.flowBooks);
            this.Controls.Add(this.btnBuy);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.FlowLayoutPanel flowBooks;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Button btnCart;
    }
}