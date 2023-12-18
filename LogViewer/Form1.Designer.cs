namespace LogViewer
{
    partial class Form1
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
            this.materialExpansionPanel1 = new MaterialSkin.Controls.MaterialExpansionPanel();
            this.materialCheckedListBox1 = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.materialExpansionPanel2 = new MaterialSkin.Controls.MaterialExpansionPanel();
            this.materialCheckedListBox2 = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.materialListView2 = new MaterialSkin.Controls.MaterialListView();
            this.materialListView1 = new MaterialSkin.Controls.MaterialListView();
            this.Messages = new System.Windows.Forms.ColumnHeader();
            this.materialExpansionPanel1.SuspendLayout();
            this.materialExpansionPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialExpansionPanel1
            // 
            this.materialExpansionPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialExpansionPanel1.Controls.Add(this.materialCheckedListBox1);
            this.materialExpansionPanel1.Depth = 0;
            this.materialExpansionPanel1.Description = "";
            this.materialExpansionPanel1.ExpandHeight = 392;
            this.materialExpansionPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialExpansionPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialExpansionPanel1.Location = new System.Drawing.Point(6, 80);
            this.materialExpansionPanel1.Margin = new System.Windows.Forms.Padding(3, 16, 3, 16);
            this.materialExpansionPanel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialExpansionPanel1.Name = "materialExpansionPanel1";
            this.materialExpansionPanel1.Padding = new System.Windows.Forms.Padding(24, 64, 24, 16);
            this.materialExpansionPanel1.Size = new System.Drawing.Size(460, 392);
            this.materialExpansionPanel1.TabIndex = 0;
            this.materialExpansionPanel1.Title = "Seleziona primo user";
            this.materialExpansionPanel1.UseAccentColor = true;
            this.materialExpansionPanel1.ValidationButtonEnable = true;
            this.materialExpansionPanel1.SaveClick += new System.EventHandler(this.materialExpansionPanel1_SaveClick);
            this.materialExpansionPanel1.CancelClick += new System.EventHandler(this.materialExpansionPanel1_CancelClick);
            // 
            // materialCheckedListBox1
            // 
            this.materialCheckedListBox1.AutoScroll = true;
            this.materialCheckedListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCheckedListBox1.Depth = 0;
            this.materialCheckedListBox1.Location = new System.Drawing.Point(27, 67);
            this.materialCheckedListBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckedListBox1.Name = "materialCheckedListBox1";
            this.materialCheckedListBox1.Size = new System.Drawing.Size(406, 244);
            this.materialCheckedListBox1.Striped = false;
            this.materialCheckedListBox1.StripeDarkColor = System.Drawing.Color.Empty;
            this.materialCheckedListBox1.TabIndex = 3;
            // 
            // materialExpansionPanel2
            // 
            this.materialExpansionPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialExpansionPanel2.Controls.Add(this.materialCheckedListBox2);
            this.materialExpansionPanel2.Controls.Add(this.materialListView2);
            this.materialExpansionPanel2.Depth = 0;
            this.materialExpansionPanel2.Description = "";
            this.materialExpansionPanel2.ExpandHeight = 392;
            this.materialExpansionPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialExpansionPanel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialExpansionPanel2.Location = new System.Drawing.Point(490, 80);
            this.materialExpansionPanel2.Margin = new System.Windows.Forms.Padding(3, 16, 3, 16);
            this.materialExpansionPanel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialExpansionPanel2.Name = "materialExpansionPanel2";
            this.materialExpansionPanel2.Padding = new System.Windows.Forms.Padding(24, 64, 24, 16);
            this.materialExpansionPanel2.Size = new System.Drawing.Size(461, 392);
            this.materialExpansionPanel2.TabIndex = 2;
            this.materialExpansionPanel2.Title = "Seleziona secondo user";
            this.materialExpansionPanel2.UseAccentColor = true;
            this.materialExpansionPanel2.ValidationButtonEnable = true;
            this.materialExpansionPanel2.SaveClick += new System.EventHandler(this.materialExpansionPanel2_SaveClick);
            this.materialExpansionPanel2.CancelClick += new System.EventHandler(this.materialExpansionPanel1_CancelClick);
            // 
            // materialCheckedListBox2
            // 
            this.materialCheckedListBox2.AutoScroll = true;
            this.materialCheckedListBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCheckedListBox2.Depth = 0;
            this.materialCheckedListBox2.Location = new System.Drawing.Point(27, 67);
            this.materialCheckedListBox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckedListBox2.Name = "materialCheckedListBox2";
            this.materialCheckedListBox2.Size = new System.Drawing.Size(407, 244);
            this.materialCheckedListBox2.Striped = false;
            this.materialCheckedListBox2.StripeDarkColor = System.Drawing.Color.Empty;
            this.materialCheckedListBox2.TabIndex = 3;
            // 
            // materialListView2
            // 
            this.materialListView2.AutoSizeTable = false;
            this.materialListView2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialListView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView2.Depth = 0;
            this.materialListView2.FullRowSelect = true;
            this.materialListView2.HideSelection = false;
            this.materialListView2.Location = new System.Drawing.Point(27, 67);
            this.materialListView2.MinimumSize = new System.Drawing.Size(200, 100);
            this.materialListView2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView2.Name = "materialListView2";
            this.materialListView2.OwnerDraw = true;
            this.materialListView2.Size = new System.Drawing.Size(407, 244);
            this.materialListView2.TabIndex = 2;
            this.materialListView2.UseCompatibleStateImageBehavior = false;
            this.materialListView2.View = System.Windows.Forms.View.Details;
            // 
            // materialListView1
            // 
            this.materialListView1.AutoSizeTable = false;
            this.materialListView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.Messages });
            this.materialListView1.Depth = 0;
            this.materialListView1.FullRowSelect = true;
            this.materialListView1.HideSelection = false;
            this.materialListView1.Location = new System.Drawing.Point(178, 491);
            this.materialListView1.MinimumSize = new System.Drawing.Size(200, 100);
            this.materialListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView1.Name = "materialListView1";
            this.materialListView1.OwnerDraw = true;
            this.materialListView1.Size = new System.Drawing.Size(591, 400);
            this.materialListView1.TabIndex = 3;
            this.materialListView1.UseCompatibleStateImageBehavior = false;
            this.materialListView1.View = System.Windows.Forms.View.Details;
            this.materialListView1.Visible = false;
            // 
            // Messages
            // 
            this.Messages.Text = "Messages";
            this.Messages.Width = 591;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 958);
            this.Controls.Add(this.materialListView1);
            this.Controls.Add(this.materialExpansionPanel2);
            this.Controls.Add(this.materialExpansionPanel1);
            this.DrawerAutoShow = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.materialExpansionPanel1.ResumeLayout(false);
            this.materialExpansionPanel1.PerformLayout();
            this.materialExpansionPanel2.ResumeLayout(false);
            this.materialExpansionPanel2.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ColumnHeader Messages;

        private MaterialSkin.Controls.MaterialListView materialListView1;

        private MaterialSkin.Controls.MaterialCheckedListBox materialCheckedListBox2;

        private MaterialSkin.Controls.MaterialCheckedListBox materialCheckedListBox1;

        private MaterialSkin.Controls.MaterialListView materialListView2;

        private MaterialSkin.Controls.MaterialExpansionPanel materialExpansionPanel2;

        private MaterialSkin.Controls.MaterialExpansionPanel materialExpansionPanel1;

        #endregion
    }
}