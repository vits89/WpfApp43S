using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WpfApp43S
{
    public static class Model
    {
        public static List<Student> studentsList { get; set; }

        public static void Init()
        {
            studentsList = new List<Student>();
        }

        public static string[] GetData()
        {
            try
            {
                List<string> studentsStringList = new List<string>();

                XDocument studentsDocument = XDocument.Load("Students.xml");

                foreach (XElement studentElement in studentsDocument.Element("Students").Elements("Student"))
                {
                    Student studentObject = new Student(
                        studentElement.Element("FirstName").Value,
                        studentElement.Element("Last").Value,
                        studentElement.Element("Age").Value,
                        studentElement.Element("Gender").Value
                    );

                    studentsList.Add(studentObject);

                    studentsStringList.Add(studentObject.ToString());
                }

                return studentsStringList.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static string CheckData(params string[] data)
        {
            List<string> errors = new List<string>();

            StringBuilder errorsMessage = new StringBuilder();

            if (data[0] == "")
                errors.Add("Вы не ввели имя.");

            if (data[1] == "")
                errors.Add("Вы не ввели фамилию.");

            if (data[2] != "")
            {
                int ageNumber = int.Parse(data[2]);

                if (!(ageNumber >= 16 && ageNumber <= 100))
                    errors.Add("Возраст должен находиться на отрезке [16; 100].");
            }

            if (data[3] == "")
                errors.Add("Вы не ввели пол.");
            else
            {
                int genderNumber = int.Parse(data[3]);

                if (!(genderNumber == 0 || genderNumber == 1))
                    errors.Add("Пол должен быть равен 0 (м) или 1 (ж).");
            }

            int errorsAmount = errors.Count;

            for (int index = 0; index < errorsAmount; index++)
            {
                errorsMessage.Append(errors[index]);

                if (index != (errorsAmount - 1))
                    errorsMessage.Append("\r\n");
            }

            if (errorsAmount > 0)
                return errorsMessage.ToString();
            else
                return "";
        }

        public static void AddData(Student data)
        {
            studentsList.Add(data);
        }

        public static void EditData(int index, params string[] data)
        {
            studentsList[index].studentFirstName = data[0];
            studentsList[index].studentLastName = data[1];
            studentsList[index].studentAge = data[2];
            studentsList[index].studentGender = data[3];
        }

        public static void DeleteData(int[] indices)
        {
            List<Student> studentsListTemp = new List<Student>();

            for (int index = 0; index < studentsList.Count; index++)
                if (!indices.Contains(index))
                    studentsListTemp.Add(studentsList[index]);

            studentsList = studentsListTemp;
        }

        public static void SaveData()
        {
            var studentsArray = studentsList.Select((data, id) => new { id, data });

            XDocument studentsDocument = new XDocument(
                new XElement(
                    "Students",
                    from studentElement in studentsArray
                    select new XElement(
                        "Student",
                        new XAttribute("Id", studentElement.id),

                        new XElement("FirstName", studentElement.data.studentFirstName),
                        new XElement("Last", studentElement.data.studentLastName),
                        new XElement("Age", studentElement.data.studentAge),
                        new XElement("Gender", studentElement.data.studentGender)
                    )
                )
            );

            studentsDocument.Save("Students.xml");
        }
    }
}
