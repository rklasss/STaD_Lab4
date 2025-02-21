using lab5.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace lab5
{
    public partial class Form1 : Form
    {
        Image workingImage;
        Image originalImage;


        public Form1()
        {
            InitializeComponent();
            originalImage = new Bitmap(Resources.parrot);
            workingImage = new Bitmap(Resources.parrot);
            calculateChart();
        }

        /// <summary>
        /// Для работы с отдельными пикселями изображения с шарпе есть фукнции getPixel и setPixel
        /// Проблема в том что они супер медленные, поэтому использую библиотеку BmpPixelSnoop
        /// Она оборачивает BitMap объект в свой класс, предоставляя более быстрые getPixel и setPixel
        /// </summary>

        //Гистограмма
        private void calculateChart()
        {
            int[] array = new int[256];
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }

            Bitmap image = (Bitmap)pictureBox.Image;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);

                    int R = pixel.R;
                    int G = pixel.G;
                    int B = pixel.B;

                    //Формула для яркости из лекции
                    int brightness = (int)Math.Round(0.299 * R + 0.587 * G + 0.114 * B);

                    array[brightness]++;
                }
            }

            //График: значение яркости по горизонтальной оси (от 0 до 255) и количество пикселей
            //с такой яркостью по вертикальной оси
            chart.Series[chart.Series.Count - 1].Points.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                chart.Series[chart.Series.Count - 1].Points.Add(array[i]);
            }
        }

        //Бинаризация
        private void binarization(int threshold)
        {
            Bitmap image = (Bitmap)workingImage.Clone();

            using (var bitmap = new BmpPixelSnoop(image))
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);

                        int R = pixel.R;
                        int G = pixel.G;
                        int B = pixel.B;

                        int average = (R + G + B) / 3;

                        bitmap.SetPixel(i, j, average < threshold ? Color.Black : Color.White);
                    }
                }
            }
            pictureBox.Image = image;
        }

        //Негатив
        private void negative()
        {
            Bitmap image = (Bitmap)workingImage.Clone();

            using (var bitmap = new BmpPixelSnoop(image))
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);

                        int R = pixel.R;
                        int G = pixel.G;
                        int B = pixel.B;

                        R = 255 - R;
                        G = 255 - G;
                        B = 255 - B;

                        bitmap.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
            }
            pictureBox.Image = image;
        }

        //Оттенки серого
        private void greyTones()
        {
            Bitmap image = (Bitmap)workingImage.Clone();

            using (var bitmap = new BmpPixelSnoop(image))
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);

                        int R = pixel.R;
                        int G = pixel.G;
                        int B = pixel.B;

                        int brightness = (int)Math.Round(0.299 * R + 0.587 * G + 0.114 * B);

                        R = brightness;
                        G = brightness;
                        B = brightness;

                        if (R < 0) R = 0; if (R > 255) R = 255;
                        if (G < 0) G = 0; if (G > 255) G = 255;
                        if (B < 0) B = 0; if (B > 255) B = 255;

                        bitmap.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
            }
            pictureBox.Image = image;
        }

        //Изменение контраста
        private void adjustContrast(double value)
        {
            Bitmap image = (Bitmap)workingImage.Clone();
            using (var bitmap = new BmpPixelSnoop(image))
            {
                int avgR = 0;
                int avgG = 0;
                int avgB = 0;
                //Считаем среднее значение по каналам
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        avgR += pixel.R;
                        avgG += pixel.G;
                        avgB += pixel.B;
                    }
                }
                avgR = avgR / bitmap.Width / bitmap.Height;
                avgG = avgG / bitmap.Width / bitmap.Height;
                avgB = avgB / bitmap.Width / bitmap.Height;
                //Изменение контраста
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        int R = pixel.R;
                        int G = pixel.G;
                        int B = pixel.B;
                        //Заменяем цвет пикселя по формуле из лекции
                        R = (int)Math.Round(value * (R - avgR) + avgR);
                        if (R < 0) R = 0;
                        if (R > 255) R = 255;
                        G = (int)Math.Round(value * (G - avgG) + avgG);
                        if (G < 0) G = 0;
                        if (G > 255) G = 255;
                        B = (int)Math.Round(value * (B - avgB) + avgB);
                        if (B < 0) B = 0;
                        if (B > 255) B = 255;
                        bitmap.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
            }
            pictureBox.Image = image;
        }


        //Изменение яркости
        private void adjustBrightness(int value)
        {
            Bitmap image = (Bitmap)workingImage.Clone();
            using (var bitmap = new BmpPixelSnoop(image))
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        int R = pixel.R;
                        int G = pixel.G;
                        int B = pixel.B;

                        //Чтобы изменить яркость к цвету прибавляем (или отнимаем) определенное значение
                        R += value;
                        if (R < 0) R = 0;
                        if (R > 255) R = 255;
                        G += value;
                        if (G < 0) G = 0;
                        if (G > 255) G = 255;
                        B += value;
                        if (B < 0) B = 0;
                        if (B > 255) B = 255;
                        bitmap.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
            }
            pictureBox.Image = image;
        }



        // Обработчики кнопки «Построить гистограмму»
        private void button_chart_click(object sender, EventArgs e)
        {
            calculateChart();
        }

        // === Ниже ТРИ новых обработчика кнопок (вместо TrackBar) ===

        private void buttonApplyBrightness_Click(object sender, EventArgs e)
        {
            int value;
            // Пытаемся считать int из TextBox
            if (int.TryParse(textBoxBrightness.Text, out value))
            {
                // Меняем яркость
                adjustBrightness(value);
                // Обновляем workingImage
                workingImage = pictureBox.Image;
            }
            else
            {
                MessageBox.Show("Некорректное значение яркости!");
            }
        }

        private void buttonApplyContrast_Click(object sender, EventArgs e)
        {
            double val;
            // Пытаемся считать double из TextBox
            if (double.TryParse(textBoxContrast.Text, out val))
            {
                // Меняем контраст
                adjustContrast(val);
                // Или adjustContrast(val/100.0) — если нужно, чтобы ввод «100» означал 1.0
                workingImage = pictureBox.Image;
            }
            else
            {
                MessageBox.Show("Некорректное значение контрастности!");
            }
        }

        private void buttonApplyBin_Click(object sender, EventArgs e)
        {
            int thr;
            // Пытаемся считать int из TextBox
            if (int.TryParse(textBoxBin.Text, out thr))
            {
                binarization(thr);
                workingImage = pictureBox.Image;
            }
            else
            {
                MessageBox.Show("Некорректное значение порога бинаризации!");
            }
        }

        // Кнопка «Оттенки серого»
        private void button_grey_Click(object sender, EventArgs e)
        {
            greyTones();
            workingImage = pictureBox.Image;
        }

        // Кнопка «Негатив»
        private void button_negative_Click(object sender, EventArgs e)
        {
            negative();
            workingImage = pictureBox.Image;
        }

        // Кнопка «Вернуть исходное изображение»
        private void button_reset_Click(object sender, EventArgs e)
        {
            pictureBox.Image = originalImage;
            workingImage = originalImage;

            // Сбросим значения в текстовых полях
            textBoxBrightness.Text = "0";
            textBoxContrast.Text = "1.0";
            textBoxBin.Text = "125";
        }

        // Загрузка файла
        private void button_select_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "png files (*.png)|*.png|bmp files (*.bmp)|*.bmp";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = openFileDialog1.OpenFile())
                {
                    originalImage = new Bitmap(fileStream);
                    workingImage = new Bitmap(originalImage);
                }

                pictureBox.Image = originalImage;

                // Сбросим поля и график
                textBoxBrightness.Text = "0";
                textBoxContrast.Text = "1.0";
                textBoxBin.Text = "125";
                calculateChart();
            }
        }

        private void chart_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxBrightness_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxContrast_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
