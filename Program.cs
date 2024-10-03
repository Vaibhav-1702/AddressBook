using System;
using System.Collections.Generic;
using System.Linq;

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
        private Dictionary<string, AddressBook> addressBooks;

        public AddressBookManager()
        {
            addressBooks = new Dictionary<string, AddressBook>();
        }

        // Add a new Address Book to the system
        public void AddAddressBook(string bookName)
        {
            if (!addressBooks.ContainsKey(bookName))
            {
                addressBooks[bookName] = new AddressBook();
                Console.WriteLine($"Address Book '{bookName}' created successfully.");
            }
            else
            {
                Console.WriteLine($"Address Book '{bookName}' already exists.");
            }
        }

        // Get an Address Book by its name
        public AddressBook GetAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
            {
                return addressBooks[bookName];
            }
            else
            {
                Console.WriteLine($"Address Book '{bookName}' not found.");
                return null;
            }
        }

        // Search for contacts by city across all Address Books
        public void SearchContactsByCity(string city)
        {
            var contactsInCity = new List<Contact>();

            foreach (var addressBook in addressBooks.Values)
            {
                var contacts = addressBook.Contacts.Where(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
                contactsInCity.AddRange(contacts);
            }

            DisplaySearchResults(contactsInCity, $"City: {city}");
        }

        // Search for contacts by state across all Address Books
        public void SearchContactsByState(string state)
        {
            var contactsInState = new List<Contact>();

            foreach (var addressBook in addressBooks.Values)
            {
                var contacts = addressBook.Contacts.Where(c => c.State.Equals(state, StringComparison.OrdinalIgnoreCase)).ToList();
                contactsInState.AddRange(contacts);
            }

            DisplaySearchResults(contactsInState, $"State: {state}");
        }

        // Display search results
        private void DisplaySearchResults(List<Contact> contacts, string criteria)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine($"No contacts found for {criteria}.");
            }
            else
            {
                Console.WriteLine($"\nContacts found for {criteria}:");
                foreach (var contact in contacts)
                {
                    Console.WriteLine(contact);
                    Console.WriteLine("----------------------------");
                }
            }
        }

        // Add Contact to a specific Address Book
        public void AddContactToAddressBook(string bookName)
        {
            AddressBook addressBook = GetAddressBook(bookName);
            if (addressBook != null)
            {
                Contact newContact = GetContactDetailsFromUser();

                // Check for duplicates in the specific Address Book
                if (addressBook.FindContact(newContact.FirstName, newContact.LastName) == null)
                {
                    addressBook.AddContact(newContact);
                }
                else
                {
                    Console.WriteLine("A contact with this name already exists in the Address Book.");
                }
            }
        }

        // Get contact details from user
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
            AddressBookManager manager = new AddressBookManager();

            while (true)
            {
                Console.WriteLine("\nAddress Book System Menu:");
                Console.WriteLine("1. Add New Address Book");
                Console.WriteLine("2. Add Contact to Address Book");
                Console.WriteLine("3. Search Contacts by City");
                Console.WriteLine("4. Search Contacts by State");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter Address Book Name: ");
                        string bookName = Console.ReadLine();
                        manager.AddAddressBook(bookName);
                        break;

                    case 2:
                        Console.Write("Enter Address Book Name: ");
                        bookName = Console.ReadLine();
                        manager.AddContactToAddressBook(bookName);
                        break;

                    case 3:
                        Console.Write("Enter City Name: ");
                        string city = Console.ReadLine();
                        manager.SearchContactsByCity(city);
                        break;

                    case 4:
                        Console.Write("Enter State Name: ");
                        string state = Console.ReadLine();
                        manager.SearchContactsByState(state);
                        break;

                    case 5:
                        Console.WriteLine("Exiting program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}