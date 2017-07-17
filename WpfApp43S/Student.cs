using System;

namespace WpfApp43S
{
    public class Student
    {
        public string studentFirstName { get; set; }
        public string studentLastName { get; set; }
        public string studentAge { get; set; }
        public string studentGender { get; set; }

        public Student(string firstName, string lastName, string age, string gender)
        {
            studentFirstName = firstName;
            studentLastName = lastName;
            studentAge = age;
            studentGender = gender;
        }
        public Student() { }

        public override string ToString()
        {
            string gender = studentGender == "0" ? "мужчина" : "женщина";

            if (studentAge != "")
            {
                int ageLastDigit = int.Parse(studentAge) % 10;
                string age;

                if (ageLastDigit == 1)
                    age = studentAge + " год";
                else if (Array.Exists(new int[] { 2, 3, 4 }, element => element == ageLastDigit))
                    age = studentAge + " года";
                else
                    age = studentAge + " лет";

                return string.Format("{0} {1}, {2}, {3}", studentFirstName, studentLastName, age, gender);
            }

            return string.Format("{0} {1}, {2}", studentFirstName, studentLastName, gender);
        }
    }
}
