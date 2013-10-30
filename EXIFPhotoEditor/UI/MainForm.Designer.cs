namespace EXIFPhotoEditor
{
    partial class MainForm
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
            this.b_ChangeDateTime = new System.Windows.Forms.Button();
            this.tb_Minutes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.b_ChangeGeoData = new System.Windows.Forms.Button();
            this.il_Photos = new System.Windows.Forms.ImageList(this.components);
            this.b_LoadPhoto = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ilb_files = new Controls.Development.ImageListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_timeZone = new System.Windows.Forms.TextBox();
            this.tb_pathToGpxFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.b_openTrackFile = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ChangeDateTime
            // 
            this.b_ChangeDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ChangeDateTime.Location = new System.Drawing.Point(12, 115);
            this.b_ChangeDateTime.Name = "b_ChangeDateTime";
            this.b_ChangeDateTime.Size = new System.Drawing.Size(365, 23);
            this.b_ChangeDateTime.TabIndex = 0;
            this.b_ChangeDateTime.Text = "Изменить время";
            this.b_ChangeDateTime.UseVisualStyleBackColor = true;
            this.b_ChangeDateTime.Click += new System.EventHandler(this.bChangeDateTime_Click);
            // 
            // tb_Minutes
            // 
            this.tb_Minutes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Minutes.Location = new System.Drawing.Point(282, 22);
            this.tb_Minutes.Name = "tb_Minutes";
            this.tb_Minutes.Size = new System.Drawing.Size(89, 20);
            this.tb_Minutes.TabIndex = 1;
            this.tb_Minutes.Text = "60";
            this.tb_Minutes.Validating += new System.ComponentModel.CancelEventHandler(this.tb_Minutes_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Число МИНУТ (положительное или отрицательное)";
            // 
            // b_ChangeGeoData
            // 
            this.b_ChangeGeoData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ChangeGeoData.Location = new System.Drawing.Point(6, 115);
            this.b_ChangeGeoData.Name = "b_ChangeGeoData";
            this.b_ChangeGeoData.Size = new System.Drawing.Size(369, 23);
            this.b_ChangeGeoData.TabIndex = 3;
            this.b_ChangeGeoData.Text = "Изменить гео-данные";
            this.b_ChangeGeoData.UseVisualStyleBackColor = true;
            this.b_ChangeGeoData.Click += new System.EventHandler(this.bChangeGeoData_Click);
            // 
            // il_Photos
            // 
            this.il_Photos.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.il_Photos.ImageSize = new System.Drawing.Size(24, 24);
            this.il_Photos.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // b_LoadPhoto
            // 
            this.b_LoadPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_LoadPhoto.Location = new System.Drawing.Point(12, 11);
            this.b_LoadPhoto.Name = "b_LoadPhoto";
            this.b_LoadPhoto.Size = new System.Drawing.Size(758, 23);
            this.b_LoadPhoto.TabIndex = 5;
            this.b_LoadPhoto.Text = "Выбрать фотографии";
            this.b_LoadPhoto.UseVisualStyleBackColor = true;
            this.b_LoadPhoto.Click += new System.EventHandler(this.b_LoadPhoto_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_Minutes);
            this.groupBox1.Controls.Add(this.b_ChangeDateTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 147);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изменение времени создания фотографии";
            // 
            // ilb_files
            // 
            this.ilb_files.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ilb_files.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ilb_files.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ilb_files.FormattingEnabled = true;
            this.ilb_files.ImageList = this.il_Photos;
            this.ilb_files.ItemHeight = 26;
            this.ilb_files.Location = new System.Drawing.Point(12, 40);
            this.ilb_files.Name = "ilb_files";
            this.ilb_files.Size = new System.Drawing.Size(758, 108);
            this.ilb_files.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_timeZone);
            this.groupBox2.Controls.Add(this.tb_pathToGpxFile);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.b_openTrackFile);
            this.groupBox2.Controls.Add(this.b_ChangeGeoData);
            this.groupBox2.Location = new System.Drawing.Point(395, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 147);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Простановка Гео-тегов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Часовой пояс (отличия в часах от GMT)";
            // 
            // tb_timeZone
            // 
            this.tb_timeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_timeZone.Location = new System.Drawing.Point(9, 89);
            this.tb_timeZone.Name = "tb_timeZone";
            this.tb_timeZone.Size = new System.Drawing.Size(318, 20);
            this.tb_timeZone.TabIndex = 4;
            this.tb_timeZone.Text = "3";
            this.tb_timeZone.Validating += new System.ComponentModel.CancelEventHandler(this.tb_timeZone_Validating);
            // 
            // tb_pathToGpxFile
            // 
            this.tb_pathToGpxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_pathToGpxFile.Location = new System.Drawing.Point(9, 41);
            this.tb_pathToGpxFile.Name = "tb_pathToGpxFile";
            this.tb_pathToGpxFile.Size = new System.Drawing.Size(318, 20);
            this.tb_pathToGpxFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Файл трека в формате GPX";
            // 
            // b_openTrackFile
            // 
            this.b_openTrackFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_openTrackFile.Location = new System.Drawing.Point(333, 38);
            this.b_openTrackFile.Name = "b_openTrackFile";
            this.b_openTrackFile.Size = new System.Drawing.Size(36, 23);
            this.b_openTrackFile.TabIndex = 0;
            this.b_openTrackFile.Text = "...";
            this.b_openTrackFile.UseVisualStyleBackColor = true;
            this.b_openTrackFile.Click += new System.EventHandler(this.b_openTrackFile_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(5, 306);
            this.progressBar1.Minimum = 1;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(773, 17);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Value = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 325);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ilb_files);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.b_LoadPhoto);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор EXIF информации";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_ChangeDateTime;
        private System.Windows.Forms.TextBox tb_Minutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button b_ChangeGeoData;
        private System.Windows.Forms.ImageList il_Photos;
        private System.Windows.Forms.Button b_LoadPhoto;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.Development.ImageListBox ilb_files;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_timeZone;
        private System.Windows.Forms.TextBox tb_pathToGpxFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_openTrackFile;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

