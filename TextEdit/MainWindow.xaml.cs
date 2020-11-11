using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace TextEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string pathToFile = $"{Path.GetTempFileName()}{Guid.NewGuid().ToString()}.txt";
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show($"Ваш файл расположен по этому пути: {pathToFile}");
            MessageBox.Show("Перед началом работы нажмите кнопку 'Начать работу'. По завершению работы нажмите кнопку 'Сохранить и выйти'");

            //using (FileStream fsStream = File.OpenRead(pathToFile))
            //{
            //    byte[] array = new byte[fsStream.Length];
            //    fsStream.Read(array, 0, array.Length);
            //    textArea.Text = System.Text.Encoding.Default.GetString(array);
            //}
        }

        public void SaveTimer()
        {
            //Timer timer = new Timer(null, 0, 1, 10000);
            //File.WriteAllText(path, textArea.Text);

            Timer timer = new Timer((state) =>
            {
                File.WriteAllText(pathToFile, textArea.Text);
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));
        }

        private void SaveAndQuit(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(pathToFile, textArea.Text);
            this.Close();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var tast = Task.Run(() =>
            {
                Timer timer = new Timer((state) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        File.WriteAllText(pathToFile, textArea.Text);

                    });
                    //SaveTimer();
                }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

            });
        }
    }
}
