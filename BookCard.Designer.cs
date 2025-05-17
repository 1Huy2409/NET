namespace QLBS
{
    partial class BookCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgBook = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.btnAddCart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgBook)).BeginInit();
            this.SuspendLayout();
            // 
            // imgBook
            // 
            this.imgBook.Location = new System.Drawing.Point(26, 0);
            this.imgBook.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.imgBook.Name = "imgBook";
            this.imgBook.Size = new System.Drawing.Size(146, 154);
            this.imgBook.TabIndex = 0;
            this.imgBook.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(3, 157);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(0, 13);
            this.lbTitle.TabIndex = 1;
            // 
            // lbAuthor
            // 
            this.lbAuthor.AutoSize = true;
            this.lbAuthor.Location = new System.Drawing.Point(3, 173);
            this.lbAuthor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAuthor.Name = "lbAuthor";
            this.lbAuthor.Size = new System.Drawing.Size(0, 13);
            this.lbAuthor.TabIndex = 2;
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Location = new System.Drawing.Point(115, 193);
            this.lbPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(0, 13);
            this.lbPrice.TabIndex = 3;
            // 
            // btnAddCart
            // 
            this.btnAddCart.Location = new System.Drawing.Point(89, 254);
            this.btnAddCart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddCart.Name = "btnAddCart";
            this.btnAddCart.Size = new System.Drawing.Size(83, 27);
            this.btnAddCart.TabIndex = 5;
            this.btnAddCart.Text = "Add To Cart";
            this.btnAddCart.UseVisualStyleBackColor = true;
            this.btnAddCart.Click += new System.EventHandler(this.btnAddCart_Click);
            // 
            // BookCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.lbPrice);
            this.Controls.Add(this.lbAuthor);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.imgBook);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BookCard";
            this.Size = new System.Drawing.Size(202, 283);
            ((System.ComponentModel.ISupportInitialize)(this.imgBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgBook;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Button btnAddCart;
    }
}
