using Microsoft.VisualBasic;

namespace ConsoleApp1;

public class PersonalInfo
{
    public string FirstName;
    public string LastName;
    public CardCredentials CardCredentials;
    public int PinCode;
    public List<TransactionHistory> TransactionHistory;

    public PersonalInfo(string firstName, string lastName, CardCredentials cardCredentials, int pinCode, List<TransactionHistory> transactionHistory)
    {
        FirstName = firstName;
        LastName = lastName;
        CardCredentials = cardCredentials;
        PinCode = pinCode;
        TransactionHistory = transactionHistory;
    }

    public bool Get5Transactions()
    {
        if (TransactionHistory.Count == 0)
        {
            Console.WriteLine("No Transactions at this moment");
            return true;
        }

        for (int i = 0; i < 5 && i < TransactionHistory.Count; i++)
        {
            Console.WriteLine(TransactionHistory[i].TransactiontoString() + "\n-----------------------------");
        }
        Thread.Sleep(10000);
        return true;
    }

    public bool ChangePin()
    {
        try
        {
        Console.WriteLine("insert current pin: ");
        int curpin = int.Parse(Strings.Trim(Console.ReadLine()));
        if (PinCode != curpin) {
            Console.WriteLine("pin is incorrect");
            Thread.Sleep(2000);
            return false;
        }

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("insert new password: ");
            int newpin = int.Parse(Strings.Trim(Console.ReadLine()));
            Console.WriteLine("insert new password again: ");
            int newpin2 = int.Parse(Strings.Trim(Console.ReadLine()));
            if (newpin2 != newpin)
            {
                Console.WriteLine("Pincode is typed incorrectly, try again:");
                Thread.Sleep(2000);
                Console.Clear();
                continue;
            }

            if (newpin.ToString().Length != 4)
            {
                Console.WriteLine("pin must be 4 Integers long");
                continue;
            }
            PinCode = newpin;
            break;
        }

        if (PinCode == curpin)
        {
            Console.WriteLine("Pin code is not changed");
            Thread.Sleep(2000);
            return false;
        }
        TransactionHistory tr = new TransactionHistory(DateTime.Now, "changed pin code",
            TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd,
            TransactionHistory[0].AmountEur);
        TransactionHistory.Insert(0, tr);
        Console.WriteLine("Pin is Changed");
        Thread.Sleep(2000);
        return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool BalanceInquery()
    {
        Console.WriteLine(TransactionHistory[0].AmountToString());
        TransactionHistory tr = new TransactionHistory(DateTime.Now, "balance inquery",
            TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd, TransactionHistory[0].AmountEur);
        TransactionHistory.Insert(0, tr);
        return true;
    }

    public bool GetAmount()
    {
        try
        {
        Console.WriteLine("insert cash amount: ");
        int amount = int.Parse(Strings.Trim(Console.ReadLine()));
        Console.WriteLine("insert currency:\n 1 - GEL \n 2 - USD \n 3 - EUR");
        int currency = int.Parse(Strings.Trim(Console.ReadLine()));
        switch (currency)
        {
            case 1:
                if (TransactionHistory[0].AmountGel < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                TransactionHistory trgel = new TransactionHistory(DateTime.Now, "get amount",
                    TransactionHistory[0].AmountGel - amount, TransactionHistory[0].AmountUsd, TransactionHistory[0].AmountEur);
                    TransactionHistory.Insert(0, trgel);
                break;
            case 2:
                if (TransactionHistory[0].AmountUsd < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                TransactionHistory trusd = new TransactionHistory(DateTime.Now, "get amount",
                    TransactionHistory[0].AmountGel, this.TransactionHistory[0].AmountUsd - amount, TransactionHistory[0].AmountEur);
                    TransactionHistory.Insert(0, trusd);
                break;
                
            case 3: 
                if (TransactionHistory[0].AmountEur < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                TransactionHistory treur = new TransactionHistory(DateTime.Now, "get amount",
                    TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd, TransactionHistory[0].AmountEur - amount);
                    TransactionHistory.Insert(0, treur);
                break;
            default: Console.WriteLine("no such currency exists");
                Thread.Sleep(3000);
                return false;
        }
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
    
    public bool AddAmount()
    {
        try
        {
            Console.WriteLine("insert cash amount: ");
            int amount = int.Parse(Strings.Trim(Console.ReadLine()));
            Console.WriteLine("insert currency:\n 1 - GEL \n 2 - USD \n 3 - EUR");
            int currency = int.Parse(Strings.Trim(Console.ReadLine()));
        switch (currency)
        {
            case 1:
                TransactionHistory trgel = new TransactionHistory(DateTime.Now, "add amount",
                    TransactionHistory[0].AmountGel + amount, TransactionHistory[0].AmountUsd,
                    TransactionHistory[0].AmountEur);
                TransactionHistory.Insert(0, trgel);
                break;
            case 2:
                TransactionHistory trusd = new TransactionHistory(DateTime.Now, "add amount",
                    TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd + amount,
                    TransactionHistory[0].AmountEur);
                TransactionHistory.Insert(0, trusd);
                break;

            case 3:
                TransactionHistory treur = new TransactionHistory(DateTime.Now, "add amount",
                    TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd,
                    TransactionHistory[0].AmountEur + amount);
                TransactionHistory.Insert(0, treur);
                break;
            default: Console.WriteLine("no such currency exists");
                Thread.Sleep(3000);
                return false;
        }
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }

    public bool ChangeAmount()
    {
        try
        {Console.WriteLine("insert cash amount: ");
        int amount = int.Parse(Strings.Trim(Console.ReadLine()));
        Console.WriteLine("insert currency you want change FROM:\n 1 - GEL \n 2 - USD \n 3 - EUR");
        int currencyfrom = int.Parse(Strings.Trim(Console.ReadLine()));
        Console.WriteLine("insert currency you want change TO:\n 1 - GEL \n 2 - USD \n 3 - EUR");
        int currencyto = int.Parse(Strings.Trim(Console.ReadLine()));
        switch (currencyfrom)
        {
            case 1:
                if (TransactionHistory[0].AmountGel < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                switch (currencyto)
                {
                    case 1:
                        break;
                    case 2:
                        TransactionHistory gelToUsd = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel - amount, TransactionHistory[0].AmountUsd + amount * 0.37,
                            TransactionHistory[0].AmountEur);
                        TransactionHistory.Insert(0, gelToUsd);
                        break;
                    case 3:
                        TransactionHistory gelToEur = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel - amount, TransactionHistory[0].AmountUsd,
                            TransactionHistory[0].AmountEur + amount * 0.34);
                        TransactionHistory.Insert(0, gelToEur);
                        break;
                    default: Console.WriteLine("no such currency exists");
                        Thread.Sleep(3000);
                        return false;
                }

                break;
            case 2:
                if (TransactionHistory[0].AmountUsd < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                switch (currencyto)
                {
                    case 1:
                        TransactionHistory usdToGel = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel + amount * 2.64, TransactionHistory[0].AmountUsd - amount,
                            TransactionHistory[0].AmountEur);
                        TransactionHistory.Insert(0, usdToGel);
                        break;
                    case 2:
                        break;
                    case 3:
                        TransactionHistory usdToEur = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd - amount,
                            TransactionHistory[0].AmountEur + amount * 0.91);
                        TransactionHistory.Insert(0, usdToEur);
                        break;
                    default: Console.WriteLine("no such currency exists");
                        Thread.Sleep(3000);
                        return false;
                }
                break;
            case 3:
                if (TransactionHistory[0].AmountEur < amount)
                {
                    Console.WriteLine("You don't have enough money on account");
                    Thread.Sleep(2000);
                    return false;
                }
                switch (currencyto)
                {
                    case 1:
                        TransactionHistory eurToGel = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel * amount * 2.9, TransactionHistory[0].AmountUsd,
                            TransactionHistory[0].AmountEur - amount);
                        TransactionHistory.Insert(0, eurToGel);
                        break;
                    case 2:
                        TransactionHistory eurToUsd = new TransactionHistory(DateTime.Now, "change amount",
                            TransactionHistory[0].AmountGel, TransactionHistory[0].AmountUsd * 1.1,
                            TransactionHistory[0].AmountEur - amount);
                        TransactionHistory.Insert(0, eurToUsd);
                        break;
                    case 3:
                        break;
                    default: Console.WriteLine("no such currency exists");
                        Thread.Sleep(3000);
                        return false;
                }
                break;
            default: Console.WriteLine("no such currency exists");
                Thread.Sleep(3000);
                return false;
        }
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
}