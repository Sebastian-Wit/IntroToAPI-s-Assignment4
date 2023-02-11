using System;

interface IPerson
{
	string Name { get; set; }
	int Age { get; set; }
	string Address { get; set; }
	string this[int index] { get; set; }
	event EventHandler AddressChanged;

	void DisplayPersonInfo();
}

class Person : IPerson
{
        private string[] _phoneNumbers = new string[3];
	private string _address;

        public string Name { get; set; }
	public int Age { get; set; }
	public string Address
	{
		get
		{
			return _address;
		}
		set
		{
			_address = value;
			AddressChanged?.Invoke(this, EventArgs.Empty); //If AddressChanged of a created Person object is subscribed to
								       //Person_AddressChanged, invoke the event handler for this object
								       //with no EventArgs (could replace this with the new address, though
								       //if that was to be printed in the error message
        	}
	}


	public string this[int index]
	{
		get
		{
			return _phoneNumbers[index];
		}
		set
		{
			_phoneNumbers[index] = value;
		}
	}

	public event EventHandler AddressChanged;

	public void DisplayPersonInfo()
	{
		Console.WriteLine("Name: " + Name);
		Console.WriteLine("Age: " + Age);
		Console.WriteLine("Address: " + Address);
		Console.WriteLine("Contacts: ");
		for (int i = 0; i < _phoneNumbers.Length; i++)
		{
			Console.WriteLine("Contact " + (i + 1) + ": " + _phoneNumbers[i]);
		}
	}
}

class Program
{
	static void Main(string[] args)
	{
		Person person = new Person();
		person.Name = "John Doe";
		person.Age = 20;
		person.Address = "20 Maple Ave.";
		person[0] = "123-456-7890";
		person[1] = "123-456-7891";
		person[2] = "123-456-7892";

		person.AddressChanged += Person_AddressChanged; //Allows for the error message to apply to the address of "person"

		person.DisplayPersonInfo();

		person.Address = "30 Pine St."; //Change of address displays event handler message on screen

		Console.ReadLine(); //Allows output to stay on screen until the user presses Enter
	}

	//object type sender parameter refers to the object that called this event
	private static void Person_AddressChanged(object sender, EventArgs e) //event handler added at the end of the program for readability
	{
		Console.WriteLine("The address has changed.");
	}
}