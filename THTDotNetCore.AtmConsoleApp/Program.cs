using System;

public class cardHolder
{
    string cardNum;
    int pin;
    string firstName;
    string lastName;
    double balance;

    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public string getCardNum() { return cardNum; }
    public int getPin() { return pin; }
    public string getFullName()
    {
        return $"{firstName} {lastName}";
    }

    public double getBalance()
    {
        return balance;
    }

    public void setCardNum(string newCardNum)
    {
        this.cardNum = newCardNum;
    }

    public void setFullName(string newFirstName, string newLastName)
    {
        this.firstName = newFirstName;
        this.lastName = newLastName;
    }

    public void setBalance(double newBalance)
    {
        this.balance = newBalance;
    }

    public static void Main(string[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose from one of the following options...");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        void deposit(cardHolder currentUser)
        {
            Console.WriteLine("How much $ would you like to deposit: ");
            double deposit = Double.Parse(Console.ReadLine());

            currentUser.setBalance(deposit + currentUser.getBalance());
            Console.WriteLine("Thank you for your $$. Your new balance is " + currentUser.getBalance());
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much $ would you like to withdraw: ");
            double withdrawal = Double.Parse(Console.ReadLine());
            if (currentUser.getBalance() < withdrawal)
            {
                Console.WriteLine("Insufficient balance...");

            }

            double newBalance = currentUser.getBalance() - withdrawal;

            currentUser.setBalance((double)newBalance);
            Console.WriteLine("You're good to go! Thank you");

        }

        void balance(cardHolder currentUser)
        {
            Console.WriteLine("Current balance:" + currentUser.getBalance());
        }

        List<cardHolder> cardHolders = new List<cardHolder>();
        cardHolders.Add(new cardHolder("123455665", 1234, "Jhon", "Jones", 150.41));
        cardHolders.Add(new cardHolder("234565544", 2345, "Jack", "Frost", 546.41));
        cardHolders.Add(new cardHolder("324563343", 2552, "Carl", "Tony", 75.35));
        cardHolders.Add(new cardHolder("665321353", 2525, "Jame", "Karl", 574.94));

        Console.WriteLine("Welcome from simpleATM");
        Console.WriteLine("Please insert your debit card");
        string debitCardnum = "";
        cardHolder currentUser;

        while (true)
        {
            try
            {
                debitCardnum = Console.ReadLine();
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardnum);
                if (currentUser != null) { break; }
                else { Console.WriteLine("Card not recognized. Please try again"); };
            }
            catch
            {
                Console.WriteLine("Card not recognized. Please try again");
            }
        }
        Console.WriteLine("Please enter your pin");
        int userPin = 0;

        while (true)
        {
            try
            {
                userPin = int.Parse(Console.ReadLine());
                if (currentUser.getPin() == userPin) { break; }
                else { Console.WriteLine("Incorrect Pin. Please try again"); };
            }
            catch
            {
                Console.WriteLine("Incorrect Pin. Please try again");
            }
        }

        Console.WriteLine("Welcome " + currentUser.getFullName());
        int option = 0;
        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine());

            }
            catch (Exception)
            {
            }

            if (option == 1) { deposit(currentUser); }
            else if (option == 2) { withdraw(currentUser); }
            else if (option == 3) { balance(currentUser); }
            else if (option == 4) { break; }
            else { option = 0; }

        } while (option != 4);
        Console.WriteLine("Thank you! Have a nice day");
    }
}
