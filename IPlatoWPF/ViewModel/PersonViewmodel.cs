using IPlatoWPF.Helper;
using IPlatoWPF.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IPlatoWPF.ViewModel
{
    internal class PersonViewmodel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> m_Persons;

        public ObservableCollection<Person> Persons
        {
            get
            {
                return m_Persons;
            }
        }
        private string message { get; set; }
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private Person selectedPerson { get; set; }
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson =value; OnPropertyChanged("SelectedPerson"); }
        }

        private Person personDetails;
        public Person PersonDetails
        {
            get { return personDetails; }
            set { personDetails = value; OnPropertyChanged("PersonDetails"); }
        }
        public ICommand OnSelectionChangedCommand { get; set; }
        public ICommand AddNewPersonCommand { get; set; }
        public ICommand UpdatePersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }
        public ICommand ClearFormCommand { get; set; }
        public PersonViewmodel()
        {
            var Philip = new Person() { UniqueId=1, FirstName = "Philip", LastName = "Pullman", DateOfBirth = new DateTime(1991, 01, 01), Profession="Author" };
            var Neil = new Person() { UniqueId = 2, FirstName = "Neil", LastName = "Gaiman", DateOfBirth = new DateTime(1990, 12, 24) ,Profession = "Author" };
            var Roald = new Person() { UniqueId = 3, FirstName = "Roald", LastName = "Dahl", DateOfBirth = new DateTime(1992, 05, 16) , Profession = "Author" };
            m_Persons = new ObservableCollection<Person>() { Philip, Neil, Roald };

            OnSelectionChangedCommand = new RelayCommand(SelectionChanged);
            AddNewPersonCommand = new RelayCommand(AddNewPerson);
            UpdatePersonCommand = new RelayCommand(UpdatePerson);
            DeletePersonCommand = new RelayCommand(DeletePersonDetails);
            ClearFormCommand = new RelayCommand(ClearForm);
            PersonDetails = new Person();
        }

        private void DeletePersonDetails(object obj)
        {
            Message = string.Empty;
            if (PersonDetails!=null && Persons.Any())
            {
                if(Persons.Any(x=>x.UniqueId == PersonDetails.UniqueId))
                {
                    Persons.Remove(Persons.Where(x=>x.UniqueId ==PersonDetails.UniqueId).FirstOrDefault());
                    Message = "Data deleted successfully.";
                    ClearForm(null);
                }
            }
        }

        private void AddNewPerson(object obj)
        {
            Message = string.Empty;
            if (PersonDetails == null)
            {
                
            }
            //if the person already exists
            if (Persons != null && Persons.Any(x => x.FirstName.Equals(PersonDetails.FirstName, StringComparison.OrdinalIgnoreCase) &&
            x.LastName.Equals(PersonDetails.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                Message = "This person details already exists on the system.";
                return;
            }
                if (AnyPropertyIsnull())
                {
                    Message = "Please fill in all the fields.";
                    return;
                }

                //add to the list
                PersonDetails.UniqueId = Persons.Count + 1;
                Persons.Add(PersonDetails);
                Message = "Details added successfully.";
                ClearForm(null); 
        }

        private bool AnyPropertyIsnull()
        {
                return (string.IsNullOrEmpty(PersonDetails.FirstName)
                    || string.IsNullOrEmpty(PersonDetails.LastName)
                    || string.IsNullOrEmpty(PersonDetails.Profession)
                    || PersonDetails.DateOfBirth == null);
        }

        private void UpdatePerson(object obj)
        {
            Message = string.Empty;
            if (PersonDetails != null && Persons.Any(x=>x.UniqueId == PersonDetails.UniqueId))
            {
                //remove and add items again to trigger collection changed
                Persons.Remove(Persons.Where(x => x.UniqueId == PersonDetails.UniqueId).FirstOrDefault());
                Persons.Add(GetNewPerson(PersonDetails));
                Message = "Details updated successfully.";

            }
        }

        private Person GetNewPerson(Person newDetailsToAdd)
        {
            return new Person()
            {
                UniqueId = newDetailsToAdd.UniqueId,
                FirstName = newDetailsToAdd.FirstName,
                LastName = newDetailsToAdd.LastName,
                Profession = newDetailsToAdd.Profession,
                DateOfBirth = newDetailsToAdd.DateOfBirth,

            };
        }

        private void ClearForm(object obj)
        {
            PersonDetails = new Person();
            SelectedPerson = null;
            message=string.Empty;
        }

        private void SelectionChanged(object obj)
        {
            if (SelectedPerson != null)
            {
                PersonDetails = GetNewPerson(SelectedPerson);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
