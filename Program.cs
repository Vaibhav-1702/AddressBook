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


        // Override Equals method to compare contacts by First and Last Name
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Contact other = (Contact)obj;
            return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                   LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
        }

        // Override GetHashCode to ensure that the hash code is consistent with the Equals method
        public override int GetHashCode()
        {
            return (FirstName + LastName).ToLower().GetHashCode();
        }

        // Display contact information in a readable format
        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\nAddress: {Address}\nCity: {City}, State: {State}, Zip: {Zip}\nPhone: {PhoneNumber}\nEmail: {Email}";
        }
    }




    public class AddressBook
    {
        public List<Contact> Contacts { get; private set; }

        public AddressBook()
        {
            Contacts = new List<Contact>();
        }

        // Add a new contact to the Address Book after checking for duplicates
        public void AddContact(Contact contact)
        {
            if (Contacts.Contains(contact))
            {
                Console.WriteLine("A contact with the same name already exists.");
            }
            else
            {
                Contacts.Add(contact);
                Console.WriteLine("Contact added successfully.");
            }
        }

        // Display all contacts in the Address Book
        public void DisplayContacts()
        {
            if (Contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
            }
            else
            {
                foreach (var contact in Contacts)
                {
                    Console.WriteLine(contact);
                    Console.WriteLine("----------------------------");
                }
            }
        }

        // Find a contact by first and last name
        public Contact FindContact(string firstName, string lastName)
        {
            foreach (var contact in Contacts)
            {
                if (contact.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    contact.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    return contact;
                }
            }
            return null;
        }

        // Edit a contact in the Address Book
        public void EditContact(string firstName, string lastName)
        {
            Contact contact = FindContact(firstName, lastName);
            if (contact != null)
            {
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
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        // Delete a contact in the Address Book
        public void DeleteContact(string firstName, string lastName)
        {
            Contact contact = FindContact(firstName, lastName);
            if (contact != null)
            {
                Contacts.Remove(contact);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

    }


    public class AddressBookManager
    {
        private Dictionary<string, AddressBook> addressBooks; // Dictionary to store multiple Address Books by name

        public AddressBookManager()
        {
            addressBooks = new Dictionary<string, AddressBook>(); // Initialize the dictionary
        }

        // Main menu to manage the Address Books and perform operations
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add New Address Book");
                Console.WriteLine("2. Select Address Book and Manage Contacts");
                Console.WriteLine("3. Display All Address Books");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddNewAddressBook();
                        break;
                    case 2:
                        ManageAddressBook();
                        break;
                    case 3:
                        DisplayAllAddressBooks();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

        // Add a new Address Book with a unique name
        public void AddNewAddressBook()
        {
            Console.Write("Enter the name for the new Address Book: ");
            string name = Console.ReadLine();

            if (addressBooks.ContainsKey(name))
            {
                Console.WriteLine("An Address Book with this name already exists.");
            }
            else
            {
                addressBooks[name] = new AddressBook();
                Console.WriteLine($"Address Book '{name}' created successfully.");
            }
        }

        // Display all the available Address Books
        public void DisplayAllAddressBooks()
        {
            if (addressBooks.Count == 0)
            {
                Console.WriteLine("No Address Books available.");
            }
            else
            {
                Console.WriteLine("Available Address Books:");
                foreach (var book in addressBooks.Keys)
                {
                    Console.WriteLine($"- {book}");
                }
            }
        }

        // Select an Address Book to perform actions like adding, editing, and deleting contacts
        public void ManageAddressBook()
        {
            Console.Write("Enter the name of the Address Book to manage: ");
            string name = Console.ReadLine();

            if (addressBooks.ContainsKey(name))
            {
                AddressBook selectedAddressBook = addressBooks[name];
                ShowAddressBookMenu(selectedAddressBook);
            }
            else
            {
                Console.WriteLine("No Address Book found with this name.");
            }
        }

        // Menu to manage contacts within a selected Address Book
        private void ShowAddressBookMenu(AddressBook addressBook)
        {
            while (true)
            {
                Console.WriteLine("\nAddress Book Menu:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Display Contacts");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Go Back to Main Menu");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddContactToAddressBook(addressBook);
                        break;
                    case 2:
                        addressBook.DisplayContacts();
                        break;
                    case 3:
                        EditContactInAddressBook(addressBook);
                        break;
                    case 4:
                        DeleteContactInAddressBook(addressBook);
                        break;
                    case 5:
                        return; // Go back to the main menu
                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

        // Add a new contact to the selected Address Book
        public void AddContactToAddressBook(AddressBook addressBook)
        {
            Contact newContact = GetContactDetailsFromUser();

            // Check if a contact with the same first and last name already exists in the Address Book
            if (addressBook.FindContact(newContact.FirstName, newContact.LastName) == null)
            {
                addressBook.AddContact(newContact);
            }
            else
            {
                Console.WriteLine("A contact with this name already exists.");
            }
        }

        // Edit a contact in the selected Address Book
        public void EditContactInAddressBook(AddressBook addressBook)
        {
            Console.Write("Enter the First Name of the contact to edit: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter the Last Name of the contact to edit: ");
            string lastName = Console.ReadLine();

            addressBook.EditContact(firstName, lastName);
        }

        // Delete a contact in the selected Address Book
        public void DeleteContactInAddressBook(AddressBook addressBook)
        {
            Console.Write("Enter the First Name of the contact to delete: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter the Last Name of the contact to delete: ");
            string lastName = Console.ReadLine();

            addressBook.DeleteContact(firstName, lastName);
        }

        // Helper method to get contact details from the user
        private Contact GetContactDetailsFromUser()
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