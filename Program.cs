using System;

namespace AddressBookApp
{


    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Contact(string firstName, string lastName, string address, string city, string state, string zip, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // Format in which output will be displayed
        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\nAddress: {Address}, {City}, {State} - {Zip}\nPhone: {PhoneNumber}, Email: {Email}";
        }
    }




    public class AddressBook
    {
        public List<Contact> Contacts { get; set; }

        public AddressBook()
        {
            Contacts = new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);// adds the deatils passed from below
            Console.WriteLine("Contact added successfully.");
        }

        public void DisplayContacts()
        {
            if (Contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                return;
            }

            foreach (var contact in Contacts)
            {
                Console.WriteLine(contact);
                Console.WriteLine("-------------------------");
            }
        }

       
        // first it will search for the contact and then change the values 
        public void EditContact(string firstName, string lastName)
        {
            Contact contact = FindContact(firstName, lastName);
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.Write("Enter new Address: ");
            contact.Address = Console.ReadLine();
            Console.Write("Enter new City: ");
            contact.City = Console.ReadLine();
            Console.Write("Enter new State: ");
            contact.State = Console.ReadLine();
            Console.Write("Enter new Zip: ");
            contact.Zip = Console.ReadLine();
            Console.Write("Enter new Phone Number: ");
            contact.PhoneNumber = Console.ReadLine();
            Console.Write("Enter new Email: ");
            contact.Email = Console.ReadLine();

            Console.WriteLine("Contact updated successfully.");
        }



        public void DeleteContact(string firstName, string lastName)
        {
            Contact contact = FindContact(firstName, lastName);
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Contacts.Remove(contact);
            Console.WriteLine("Contact deleted successfully.");
        }



        //This is used to find contact if conatct if user doesn't exits then it will pass null to EditContact() above
        public Contact FindContact(string firstName, string lastName)
        {
            foreach (var contact in Contacts)
            {
                if (contact.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    contact.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    return contact;  // Return the contact if found
                }
            }
            return null;
        }

    }


    public class AddressBookManager
    {
        private AddressBook addressBook;

        public AddressBookManager()
        {
            addressBook = new AddressBook();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Display Contacts");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddNewContact();
                        break;
                    case 2:
                        addressBook.DisplayContacts();
                        break;
                    case 3:
                        EditContact();// Will jump to EditContact() present below
                        break;
                    case 4:
                        DeleteContact();// Will jump to DeleteContact() present below
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }


        //For adding new Contact
        public void AddNewContact()
        {
            var contact = GetContactDetailsFromUser(); // stores all the deatils of user 
            addressBook.AddContact(contact); // passed to the AddContact() in AddressBook Class
        }


        //Code to get information from the user
        public Contact GetContactDetailsFromUser()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            Console.Write("Enter State: ");
            string state = Console.ReadLine();
            Console.Write("Enter Zip: ");
            string zip = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            return new Contact(firstName, lastName, address, city, state, zip, phoneNumber, email);
        }

        //This part will take the existing or non existing user name and pass the value to EditContact() present in AddressBook Class
        public void EditContact()
        {
            Console.Write("Enter First Name of the Contact to edit: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name of the Contact to edit: ");
            string lastName = Console.ReadLine();

            addressBook.EditContact(firstName, lastName);
        }

        public void DeleteContact()
        {
            Console.Write("Enter First Name of the Contact to delete: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name of the Contact to delete: ");
            string lastName = Console.ReadLine();

            addressBook.DeleteContact(firstName, lastName);
        }



    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");

            AddressBookManager addressBookManager = new AddressBookManager();
            addressBookManager.ShowMenu();
        }
    }
}