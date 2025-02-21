import tkinter as tk
from tkinter import filedialog, messagebox
from PIL import Image, ImageTk
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import requests
from io import BytesIO
import numpy as np
import gc
import time
from memory_profiler import profile  # Импортируем профилировщик

# Глобальная переменная для хранения ссылок на изображения, чтобы вызвать утечку памяти
leaked_images = []

# Константы по умолчанию
DEFAULT_IMG_URL = (
    "https://avatars.mds."
    "yandex.net/i?id="
    "86173555bdf1057b63885466"
    "556fe494d35c8655b14eabfd"
    "-5270387-images-thumbs&n=13"
)
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

        tk.Label(self.control_frame, text="URL изображения:").pack()
        self.url_entry = tk.Entry(self.control_frame, width=50)
        self.url_entry.pack()
        self.url_entry.insert(0, DEFAULT_IMG_URL)

        self.load_url_button = tk.Button(
            self.control_frame,
            text="Загрузить по URL",
            command=self.load_image_from_url
        )
        self.load_url_button.pack(pady=5)

        self.load_file_button = tk.Button(
            self.control_frame,
            text="Загрузить с файла",
            command=self.load_image_from_file
        )
        self.load_file_button.pack(pady=5)

        self.hist_button = tk.Button(
            self.control_frame,
            text="Построить гистограмму",
            command=self.plot_histogram
        )
        self.hist_button.pack(fill=tk.X, pady=10)

        tk.Label(
            self.control_frame,
            text="Макс. значение гистограммы (опционально):"
        ).pack()
        self.hist_max_entry = tk.Entry(self.control_frame)
        self.hist_max_entry.pack()
        self.hist_max_entry.insert(0, str(DEFAULT_HIST_MAX))

        self.original_image = None
        self.processed_image = None

        self.load_image_from_url()

    @profile  # Декоратор для профилирования памяти
    def load_image_from_url(self):
        url = self.url_entry.get()
        try:
            response = requests.get(url)
            response.raise_for_status()
            img_data = BytesIO(response.content)
            self.original_image = Image.open(img_data).convert("RGB")
            self.processed_image = self.original_image.copy()
            self.display_image(self.processed_image)

            # Создание утечки памяти: сохраняем изображения в глобальной переменной
            leaked_images.append(self.original_image)

        except Exception as e:
            messagebox.showerror(
                "Ошибка",
                f"Не удалось загрузить изображение по URL:\n{e}"
            )

    @profile  # Декоратор для профилирования памяти
    def load_image_from_file(self):
        file_path = filedialog.askopenfilename(
            title="Выберите изображение",
            filetypes=[
                ("Image Files", "*.png;*.jpg;*.jpeg;*.bmp;*.gif"),
                ("All Files", "*.*")
            ]
        )
        if not file_path:
            return
        try:
            self.original_image = Image.open(file_path).convert("RGB")
            self.processed_image = self.original_image.copy()
            self.display_image(self.processed_image)

            # Создание утечки памяти: сохраняем изображения в глобальной переменной
            leaked_images.append(self.original_image)

        except Exception as e:
            messagebox.showerror(
                "Ошибка",
                f"Не удалось загрузить изображение с файла:\n{e}"
            )

    def display_image(self, img):
        max_size = (500, 500)
        img_thumbnail = img.copy()
        img_thumbnail.thumbnail(max_size, Image.Resampling.LANCZOS)
        self.tk_image = ImageTk.PhotoImage(img_thumbnail)
        self.image_label.config(image=self.tk_image)
        self.image_label.image = self.tk_image

    def plot_histogram(self):
        if self.processed_image is None:
            messagebox.showwarning(
                "Предупреждение",
                "Сначала загрузите изображение."
            )
            return

        np_img = np.array(self.processed_image)
        brightness = (
            0.299 * np_img[:, :, 0] +
            0.587 * np_img[:, :, 1] +
            0.114 * np_img[:, :, 2]
        ).astype(int)
        histogram, _ = np.histogram(brightness, bins=256, range=(0, 255))

        try:
            max_hist = int(self.hist_max_entry.get())
        except ValueError:
            messagebox.showwarning(
                "Предупреждение",
                "Неверное значение максимума гистограммы. "
                "Используется значение по умолчанию."
            )
            max_hist = DEFAULT_HIST_MAX

        histogram = np.clip(histogram, 0, max_hist)

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


def main():
    root = tk.Tk()
    ImageEditor(root)
    root.mainloop()


if __name__ == "__main__":
    # Профилирование памяти
    gc.collect()  # Принудительно вызываем сборку мусора перед запуском
    start_time = time.time()
    main()
    end_time = time.time()
    print(f"Время выполнения: {end_time - start_time:.2f} секунд")
