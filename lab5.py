import tkinter as tk
from tkinter import filedialog, simpledialog, messagebox
from PIL import Image, ImageTk, ImageOps, ImageEnhance
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import requests
from io import BytesIO
import numpy as np

# Константы по умолчанию
DEFAULT_IMG_URL = "https://avatars.mds.yandex.net/i?id=86173555bdf1057b63885466556fe494d35c8655b14eabfd-5270387-images-thumbs&n=13"
DEFAULT_HIST_MAX = 3000

class ImageEditor:
    def __init__(self, root):
        self.root = root
        self.root.title("Редактор изображений")

        # Основная рамка
        self.main_frame = tk.Frame(root)
        self.main_frame.pack(fill=tk.BOTH, expand=True)

        # Левая часть: изображение
        self.image_label = tk.Label(self.main_frame)
        self.image_label.pack(side=tk.LEFT, padx=10, pady=10)

        # Правая часть: управление
        self.control_frame = tk.Frame(self.main_frame)
        self.control_frame.pack(side=tk.RIGHT, fill=tk.Y, padx=10, pady=10)

        # Поле ввода URL
        tk.Label(self.control_frame, text="URL изображения:").pack()
        self.url_entry = tk.Entry(self.control_frame, width=50)
        self.url_entry.pack()
        self.url_entry.insert(0, DEFAULT_IMG_URL)

        self.load_url_button = tk.Button(self.control_frame, text="Загрузить по URL", command=self.load_image_from_url)
        self.load_url_button.pack(pady=5)

        # Кнопка загрузки с файла
        self.load_file_button = tk.Button(self.control_frame, text="Загрузить с файла", command=self.load_image_from_file)
        self.load_file_button.pack(pady=5)

        # Кнопки операций
        self.grayscale_button = tk.Button(self.control_frame, text="Оттенки серого", command=self.grayscale)
        self.grayscale_button.pack(fill=tk.X, pady=2)

        self.negative_button = tk.Button(self.control_frame, text="Негатив", command=self.negative)
        self.negative_button.pack(fill=tk.X, pady=2)

        self.binarization_button = tk.Button(self.control_frame, text="Бинаризация", command=self.binarization)
        self.binarization_button.pack(fill=tk.X, pady=2)

        self.brightness_button = tk.Button(self.control_frame, text="Коррекция яркости", command=self.adjust_brightness)
        self.brightness_button.pack(fill=tk.X, pady=2)

        self.contrast_button = tk.Button(self.control_frame, text="Коррекция контрастности", command=self.adjust_contrast)
        self.contrast_button.pack(fill=tk.X, pady=2)

        # Кнопка построения гистограммы
        self.hist_button = tk.Button(self.control_frame, text="Построить гистограмму", command=self.plot_histogram)
        self.hist_button.pack(fill=tk.X, pady=10)

        # Кнопка возврата к исходному изображению
        self.reset_button = tk.Button(self.control_frame, text="Вернуть исходное изображение", command=self.reset_image)
        self.reset_button.pack(fill=tk.X, pady=10)

        # Поле ввода максимального значения гистограммы (опционально)
        tk.Label(self.control_frame, text="Макс. значение гистограммы (опционально):").pack()
        self.hist_max_entry = tk.Entry(self.control_frame)
        self.hist_max_entry.pack()
        self.hist_max_entry.insert(0, str(DEFAULT_HIST_MAX))

        # Поля для хранения оригинального и обработанного изображений
        self.original_image = None
        self.processed_image = None

        # Загрузка изображения по умолчанию
        self.load_image_from_url()

    def load_image_from_url(self):
        url = self.url_entry.get()
        try:
            response = requests.get(url)
            response.raise_for_status()
            img_data = BytesIO(response.content)
            self.original_image = Image.open(img_data).convert("RGB")
            self.processed_image = self.original_image.copy()
            self.display_image(self.processed_image)
        except Exception as e:
            messagebox.showerror("Ошибка", f"Не удалось загрузить изображение по URL:\n{e}")

    def load_image_from_file(self):
        file_path = filedialog.askopenfilename(
            title="Выберите изображение",
            filetypes=[("Image Files", "*.png;*.jpg;*.jpeg;*.bmp;*.gif"), ("All Files", "*.*")]
        )
        if not file_path:
            return  # Пользователь отменил выбор файла
        try:
            self.original_image = Image.open(file_path).convert("RGB")
            self.processed_image = self.original_image.copy()
            self.display_image(self.processed_image)
        except Exception as e:
            messagebox.showerror("Ошибка", f"Не удалось загрузить изображение с файла:\n{e}")

    def display_image(self, img):
        # В новых версиях Pillow используйте Image.Resampling.LANCZOS вместо ANTIALIAS
        max_size = (500, 500)
        img_thumbnail = img.copy()
        img_thumbnail.thumbnail(max_size, Image.Resampling.LANCZOS)
        self.tk_image = ImageTk.PhotoImage(img_thumbnail)
        self.image_label.config(image=self.tk_image)
        self.image_label.image = self.tk_image  # Сохранение ссылки

    def reset_image(self):
        # Функция для возврата к исходному изображению
        if self.original_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        self.processed_image = self.original_image.copy()
        self.display_image(self.processed_image)

    def plot_histogram(self):
        # Функция для построения гистограммы яркости
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        
        # Рассчитываем яркость каждого пикселя по формуле 0.299R + 0.587G + 0.114B
        np_img = np.array(self.processed_image)
        brightness = (0.299 * np_img[:, :, 0] + 0.587 * np_img[:, :, 1] + 0.114 * np_img[:, :, 2]).astype(int)
        histogram, _ = np.histogram(brightness, bins=256, range=(0, 255))

        try:
            max_hist = int(self.hist_max_entry.get())
        except ValueError:
            messagebox.showwarning("Предупреждение", "Неверное значение максимума гистограммы. Используется значение по умолчанию.")
            max_hist = DEFAULT_HIST_MAX

        # Ограничение максимального значения для гистограммы
        histogram = np.clip(histogram, 0, max_hist)

        # Создание нового окна для отображения гистограммы
        hist_window = tk.Toplevel(self.root)
        hist_window.title("Гистограмма яркости")

        fig, ax = plt.subplots(figsize=(5, 4))
        ax.bar(range(256), histogram, color='orange')
        ax.set_title("Гистограмма яркости")
        ax.set_xlabel("Яркость")
        ax.set_ylabel("Количество пикселей")
        ax.set_xlim(0, 255)

        canvas = FigureCanvasTkAgg(fig, master=hist_window)
        canvas.draw()
        canvas.get_tk_widget().pack()

    def grayscale(self):
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        # Преобразуем изображение в оттенки серого по формуле 0.299R + 0.587G + 0.114B
        np_img = np.array(self.processed_image).astype(float)
        brightness = 0.299 * np_img[:, :, 0] + 0.587 * np_img[:, :, 1] + 0.114 * np_img[:, :, 2]
        brightness = np.clip(brightness, 0, 255).astype(np.uint8)
        grayscale_img = np.stack((brightness, brightness, brightness), axis=-1)
        self.processed_image = Image.fromarray(grayscale_img)
        self.display_image(self.processed_image)

    def negative(self):
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        # Создаем негатив изображения
        np_img = 255 - np.array(self.processed_image)
        self.processed_image = Image.fromarray(np_img)
        self.display_image(self.processed_image)

    def binarization(self):
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        # Запрос порога бинаризации
        threshold = simpledialog.askinteger("Бинаризация", "Введите порог бинаризации (0-765):", minvalue=0, maxvalue=765)
        if threshold is None:
            return
        # Преобразование в черно-белое на основе порога
        np_img = np.array(self.processed_image).astype(int)
        total = np.sum(np_img, axis=2)
        binary = np.where(total > threshold, 255, 0).astype(np.uint8)
        binary_rgb = np.stack((binary, binary, binary), axis=-1)
        self.processed_image = Image.fromarray(binary_rgb)
        self.display_image(self.processed_image)

    def adjust_brightness(self):
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        coeff = simpledialog.askinteger("Яркость", "Введите коэффициент яркости (-255 до 255):", minvalue=-255, maxvalue=255)
        if coeff is None:
            return
        # Добавляем коэффициент к каждому каналу
        np_img = np.array(self.processed_image).astype(int)
        np_img += coeff
        np_img = np.clip(np_img, 0, 255).astype(np.uint8)
        self.processed_image = Image.fromarray(np_img)
        self.display_image(self.processed_image)

    def adjust_contrast(self):
        if self.processed_image is None:
            messagebox.showwarning("Предупреждение", "Сначала загрузите изображение.")
            return
        coeff = simpledialog.askfloat("Контраст", "Введите коэффициент контрастности (например, 1.2 для увеличения):", minvalue=0.0)
        if coeff is None:
            return
        # Используем Pillow для корректировки контраста
        enhancer = ImageEnhance.Contrast(self.processed_image)
        self.processed_image = enhancer.enhance(coeff)
        self.display_image(self.processed_image)

def main():
    root = tk.Tk()
    app = ImageEditor(root)
    root.mainloop()

if __name__ == "__main__":
    main()
