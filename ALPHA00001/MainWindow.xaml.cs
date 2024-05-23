using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ALPHA00001
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра второго окна
            Window1 secondWindow = new Window1();

            // Открытие второго окна
            secondWindow.Show();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var textBox = button.Tag as TextBox;
            var text = textBox.Text;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Document|*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Create(saveFileDialog.FileName, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());
                    DocumentFormat.OpenXml.Wordprocessing.Paragraph para = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
                    DocumentFormat.OpenXml.Wordprocessing.Run run = para.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Run());
                    run.AppendChild(new Text(text));
                }

                MessageBox.Show($"Документ сохранен по пути: {saveFileDialog.FileName}");
            }
        }
       
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word Document|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(openFileDialog.FileName, false))
                {
                    Body body = doc.MainDocumentPart.Document.Body;
                    var text = body.InnerText;

                    var button = sender as Button;
                    var textBox = button.Tag as TextBox;
                    textBox.Text = text;
                }

                MessageBox.Show($"Документ открыт из: {openFileDialog.FileName}");
            }

        }
    }
}
