using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ManageYourTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TasksCollection tasksCollection = new TasksCollection();

        public DateTime TodayDate;

        public MainWindow()
        {
            InitializeComponent();

            TodayDate = DateTime.Today;
            this.Calendarz.DisplayDateStart = this.TodayDate;

            GridPanel.DataContext = tasksCollection;
        }

        
        
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            var TaskTitle = this.InputText;

            var ComboBoxItem = this.ComboBoxDropdown.SelectionBoxItem;


            if (TaskTitle.Text == "")
            {
                MessageBox.Show("dodaj tytuł zadania");
                return;
            }
            else if (string.IsNullOrWhiteSpace(TaskTitle.Text))
            {
                MessageBox.Show("podaj poprawną nazwę");
                return;
            }
            else if (!this.Calendarz.SelectedDate.HasValue)
            {
                MessageBox.Show("dodaj datę");
                return;
            }
            else
            {
                tasksCollection.ListwithTasks.Add(new Task(
                        tasksCollection.ListwithTasks.Count + 1,
                        TaskTitle.Text,
                        ComboBoxItem.ToString(),
                        this.Calendarz.SelectedDate.Value.ToString("yyyy/MM/dd")
                    )
                 );

                this.Calendarz.SelectedDate = null;

                TaskTitle.Text = "";
            }
        }
    }
}
