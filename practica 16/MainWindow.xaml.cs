using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace practica_16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // В классе MainWindow
        private Random random = new Random();

        private void DrawNextShape()
        {
            int index = random.Next(shapes.Count); // Получаем случайный индекс из списка фигур
            Shape shape = shapes[index];
            if (shape != null)
            {
                
                shape.Draw(MainGrid);
            }
        }

        private int currentIndex = 0; // Индекс текущей фигуры
        private List<Shape> shapes = new List<Shape>(); // Список фигур

        public MainWindow()
        {
            InitializeComponent();

            // Создаем экземпляр фабрики
            ShapeFactory factory = new TetrominoFactory();

            // Заполняем список фигур
            for (int i = 0; i < 10; i++)
            {
                shapes.Add(factory.CreateShape());
            }

            // Выводим следующую фигуру
            DrawNextShape();
        }

        // Метод для отрисовки следующей фигуры по порядку
        // В классе MainWindow
        private void MyDrawNextShape()
        {
            if (currentIndex >= shapes.Count)
            {
                currentIndex = 0; // Если достигли конца списка, начинаем сначала
            }

            Shape shape = shapes[currentIndex];
            if (shape != null)
            {
                
                shape.Draw(MainGrid); // Передаем координаты x и y для расположения фигуры
                currentIndex++;
            }
        }

    }

    // Базовый класс для всех фигур
    abstract class Shape
    {
        public abstract void Draw(Grid grid);
    }

    // Конкретная реализация фигуры - Квадрат
    class Square : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing a square...");
            // Добавляем квадрат на форму
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 50;
            rectangle.Height = 50;
            rectangle.Fill = System.Windows.Media.Brushes.Red; // Заполнение красным цветом (можно изменить на другой)
            grid.Children.Add(rectangle);
        }
    }

    // Остальные классы для других фигур (Линия, Форма Т, Форма L, Форма Z, Форма S) аналогично Square
    class Line : Shape
{
    public override void Draw(Grid grid)
    {
        Console.WriteLine("Drawing a line...");
        // Добавляем линию на форму
        System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
        line.X1 = 0;
        line.Y1 = 0;
        line.X2 = 50;
        line.Y2 = 50;
        line.Stroke = System.Windows.Media.Brushes.Green; // Цвет линии (можно изменить на другой)
        line.StrokeThickness = 2; // Толщина линии
        grid.Children.Add(line);
    }
}

    // Класс Форма Т
    class ShapeT : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing a T shape...");
            // Добавляем форму Т на форму
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(50, 0));
            polygon.Points.Add(new Point(25, 50));
            polygon.Fill = System.Windows.Media.Brushes.Blue; // Заполнение синим цветом (можно изменить на другой)
            grid.Children.Add(polygon);
        }
    }

    // Класс Форма L
    class ShapeL : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing an L shape...");
            // Добавляем форму L на форму
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(50, 0));
            polygon.Points.Add(new Point(50, 50));
            polygon.Points.Add(new Point(0, 50));
            polygon.Fill = System.Windows.Media.Brushes.Yellow; // Заполнение желтым цветом (можно изменить на другой)
            grid.Children.Add(polygon);
        }
    }

    // Класс Форма Z
    class ShapeZ : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing a Z shape...");
            // Добавляем форму Z на форму
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(50, 0));
            polygon.Points.Add(new Point(25, 25));
            polygon.Points.Add(new Point(50, 50));
            polygon.Points.Add(new Point(0, 50));
            polygon.Fill = System.Windows.Media.Brushes.Orange; // Заполнение оранжевым цветом (можно изменить на другой)
            grid.Children.Add(polygon);
        }
    }

    // Класс Форма S
    class ShapeS : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing an S shape...");
            // Добавляем форму S на форму
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(25, 0));
            polygon.Points.Add(new Point(50, 25));
            polygon.Points.Add(new Point(25, 50));
            polygon.Points.Add(new Point(0, 50));
            polygon.Fill = System.Windows.Media.Brushes.Purple; // Заполнение фиолетовым цветом (можно изменить на другой)
            grid.Children.Add(polygon);
        }
    }


    // Класс, представляющий супер-фигуру
    class SuperShape : Shape
    {
        public override void Draw(Grid grid)
        {
            Console.WriteLine("Drawing a super shape...");
            // Добавляем супер-фигуру на форму
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 100;
            rectangle.Height = 100;
            rectangle.Fill = System.Windows.Media.Brushes.Blue; // Заполнение синим цветом (можно изменить на другой)
            grid.Children.Add(rectangle);
        }
    }

    // Фабрика фигур
    abstract class ShapeFactory
    {
        public abstract Shape CreateShape();
    }

    // Фабрика тетромино
    class TetrominoFactory : ShapeFactory
    {
        private List<Type> _shapes = new List<Type> { typeof(Square), typeof(Line), /* Добавьте остальные фигуры */ };

        public override Shape CreateShape()
        {
            Random rand = new Random();
            int index = rand.Next(_shapes.Count);
            Type shapeType = _shapes[index];
            if (shapeType == typeof(Square))
            {
                // Предусмотрим появление супер-фигур с вероятностью 1/10
                if (rand.Next(10) == 0)
                    return new SuperShape();
                else
                    return new Square();
            }
            else
            {
                // Вернуть остальные фигуры
                return Activator.CreateInstance(shapeType) as Shape;
            }
        }
    }
}
