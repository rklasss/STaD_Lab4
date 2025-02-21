namespace lab5
{
    partial class Form1
    {
        // (1) Убрали/закомментировали всё, связанное с trackBar_brightness,
        // trackBar_contrast, trackBar_bin и их обработчики
        // (2) Добавили TextBox + Button

        private System.ComponentModel.IContainer components = null;

        // Собственно, сами новые элементы:
        private System.Windows.Forms.TextBox textBoxBrightness;
        private System.Windows.Forms.TextBox textBoxContrast;
        private System.Windows.Forms.TextBox textBoxBin;
        private System.Windows.Forms.Button buttonApplyBrightness;
        private System.Windows.Forms.Button buttonApplyContrast;
        private System.Windows.Forms.Button buttonApplyBin;

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button button_chart;
        private System.Windows.Forms.Button button_grey;
        private System.Windows.Forms.Button button_negative;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;

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

        #region Код, автоматически созданный конструктором форм

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_chart = new System.Windows.Forms.Button();
            this.textBoxBrightness = new System.Windows.Forms.TextBox();
            this.buttonApplyBrightness = new System.Windows.Forms.Button();
            this.textBoxContrast = new System.Windows.Forms.TextBox();
            this.buttonApplyContrast = new System.Windows.Forms.Button();
            this.textBoxBin = new System.Windows.Forms.TextBox();
            this.buttonApplyBin = new System.Windows.Forms.Button();
            this.button_grey = new System.Windows.Forms.Button();
            this.button_negative = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_select = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(607, 12);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(491, 537);
            this.chart.TabIndex = 1;
            this.chart.Text = "chart1";
            this.chart.Click += new System.EventHandler(this.chart_Click);
            // 
            // button_chart
            // 
            this.button_chart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_chart.Location = new System.Drawing.Point(865, 565);
            this.button_chart.Name = "button_chart";
            this.button_chart.Size = new System.Drawing.Size(233, 35);
            this.button_chart.TabIndex = 2;
            this.button_chart.Text = "Построить гистограмму";
            this.button_chart.UseVisualStyleBackColor = true;
            this.button_chart.Click += new System.EventHandler(this.button_chart_click);
            // 
            // textBoxBrightness
            // 
            this.textBoxBrightness.Location = new System.Drawing.Point(12, 598);
            this.textBoxBrightness.Name = "textBoxBrightness";
            this.textBoxBrightness.Size = new System.Drawing.Size(107, 20);
            this.textBoxBrightness.TabIndex = 3;
            this.textBoxBrightness.Text = "0";
            this.textBoxBrightness.TextChanged += new System.EventHandler(this.textBoxBrightness_TextChanged);
            // 
            // buttonApplyBrightness
            // 
            this.buttonApplyBrightness.Location = new System.Drawing.Point(12, 624);
            this.buttonApplyBrightness.Name = "buttonApplyBrightness";
            this.buttonApplyBrightness.Size = new System.Drawing.Size(107, 30);
            this.buttonApplyBrightness.TabIndex = 4;
            this.buttonApplyBrightness.Text = "Применить";
            this.buttonApplyBrightness.UseVisualStyleBackColor = true;
            this.buttonApplyBrightness.Click += new System.EventHandler(this.buttonApplyBrightness_Click);
            // 
            // textBoxContrast
            // 
            this.textBoxContrast.Location = new System.Drawing.Point(149, 598);
            this.textBoxContrast.Name = "textBoxContrast";
            this.textBoxContrast.Size = new System.Drawing.Size(107, 20);
            this.textBoxContrast.TabIndex = 5;
            this.textBoxContrast.Text = "1.0";
            this.textBoxContrast.TextChanged += new System.EventHandler(this.textBoxContrast_TextChanged);
            // 
            // buttonApplyContrast
            // 
            this.buttonApplyContrast.Location = new System.Drawing.Point(149, 624);
            this.buttonApplyContrast.Name = "buttonApplyContrast";
            this.buttonApplyContrast.Size = new System.Drawing.Size(107, 30);
            this.buttonApplyContrast.TabIndex = 6;
            this.buttonApplyContrast.Text = "Применить";
            this.buttonApplyContrast.UseVisualStyleBackColor = true;
            this.buttonApplyContrast.Click += new System.EventHandler(this.buttonApplyContrast_Click);
            // 
            // textBoxBin
            // 
            this.textBoxBin.Location = new System.Drawing.Point(287, 598);
            this.textBoxBin.Name = "textBoxBin";
            this.textBoxBin.Size = new System.Drawing.Size(107, 20);
            this.textBoxBin.TabIndex = 7;
            this.textBoxBin.Text = "125";
            // 
            // buttonApplyBin
            // 
            this.buttonApplyBin.Location = new System.Drawing.Point(287, 624);
            this.buttonApplyBin.Name = "buttonApplyBin";
            this.buttonApplyBin.Size = new System.Drawing.Size(107, 30);
            this.buttonApplyBin.TabIndex = 8;
            this.buttonApplyBin.Text = "Применить";
            this.buttonApplyBin.UseVisualStyleBackColor = true;
            this.buttonApplyBin.Click += new System.EventHandler(this.buttonApplyBin_Click);
            // 
            // button_grey
            // 
            this.button_grey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_grey.Location = new System.Drawing.Point(423, 619);
            this.button_grey.Name = "button_grey";
            this.button_grey.Size = new System.Drawing.Size(178, 35);
            this.button_grey.TabIndex = 9;
            this.button_grey.Text = "Оттенки серого";
            this.button_grey.UseVisualStyleBackColor = true;
            this.button_grey.Click += new System.EventHandler(this.button_grey_Click);
            // 
            // button_negative
            // 
            this.button_negative.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_negative.Location = new System.Drawing.Point(423, 565);
            this.button_negative.Name = "button_negative";
            this.button_negative.Size = new System.Drawing.Size(178, 35);
            this.button_negative.TabIndex = 10;
            this.button_negative.Text = "Негатив";
            this.button_negative.UseVisualStyleBackColor = true;
            this.button_negative.Click += new System.EventHandler(this.button_negative_Click);
            // 
            // button_reset
            // 
            this.button_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_reset.Location = new System.Drawing.Point(607, 619);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(491, 35);
            this.button_reset.TabIndex = 11;
            this.button_reset.Text = "Вернуть исходное изображение";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_select
            // 
            this.button_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_select.Location = new System.Drawing.Point(607, 565);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(233, 35);
            this.button_select.TabIndex = 12;
            this.button_select.Text = "Загрузить файл";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(776, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Гистограмма яркости";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(776, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Гистограмма яркости";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(26, 570);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Яркость";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(285, 570);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Бинаризация";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(135, 570);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Констрастность";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::lab5.Properties.Resources.parrot;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(589, 537);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 661);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBrightness);
            this.Controls.Add(this.buttonApplyBrightness);
            this.Controls.Add(this.textBoxContrast);
            this.Controls.Add(this.buttonApplyContrast);
            this.Controls.Add(this.textBoxBin);
            this.Controls.Add(this.buttonApplyBin);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.button_negative);
            this.Controls.Add(this.button_grey);
            this.Controls.Add(this.button_chart);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(1130, 700);
            this.MinimumSize = new System.Drawing.Size(1130, 700);
            this.Name = "Form1";
            this.Text = "Photo Corrector";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}
