using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp43S
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

        private void ClearTextBoxes()
        {
            textFirstName.Clear();
            textLastName.Clear();
            textAge.Clear();
            textGender.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model.Init();

            string[] studentsArray = Model.GetData();

            if (studentsArray != null)
                foreach (string studentString in studentsArray)
                    listBoxStudents.Items.Add(studentString);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstName = textFirstName.Text;
            string lastName = textLastName.Text;
            string age = textAge.Text;
            string gender = textGender.Text;

            string errorsMessage = Model.CheckData(firstName, lastName, age, gender);

            if (errorsMessage != "")
            {
                MessageBox.Show(errorsMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            Student studentObject = new Student(
                firstName,
                lastName,
                age,
                gender
            );

            Model.AddData(studentObject);

            Model.SaveData();

            ClearTextBoxes();

            listBoxStudents.Items.Add(studentObject.ToString());
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выделили элемент в списке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            string firstName = textFirstName.Text;
            string lastName = textLastName.Text;
            string age = textAge.Text;
            string gender = textGender.Text;

            string errorsMessage = Model.CheckData(firstName, lastName, age, gender);

            if (errorsMessage != "")
            {
                MessageBox.Show(errorsMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            Model.EditData(listBoxStudents.SelectedIndex, firstName, lastName, age, gender);

            Model.SaveData();

            listBoxStudents.Items.Clear();

            foreach (Student studentObject in Model.studentsList)
                listBoxStudents.Items.Add(studentObject.ToString());

            ClearTextBoxes();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxStudents.SelectedItems.Count == 0)
            {
                MessageBox.Show("Вы ничего не выделили!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить выделенные элементы?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                List<int> selectedItemsIndices = new List<int>();

                foreach (object selectedStudent in listBoxStudents.SelectedItems)
                    selectedItemsIndices.Add(listBoxStudents.Items.IndexOf(selectedStudent));

                Model.DeleteData(selectedItemsIndices.ToArray());

                Model.SaveData();

                listBoxStudents.Items.Clear();

                foreach (Student studentObject in Model.studentsList)
                    listBoxStudents.Items.Add(studentObject.ToString());

                ClearTextBoxes();
            }
        }

        private void listBoxStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listBoxStudents.SelectedIndex;

            if (index >= 0)
            {
                textFirstName.Text = Model.studentsList[index].studentFirstName;
                textLastName.Text = Model.studentsList[index].studentLastName;
                textAge.Text = Model.studentsList[index].studentAge;
                textGender.Text = Model.studentsList[index].studentGender;
            }
        }
    }
}
