// See https://aka.ms/new-console-template for more information
// Specify the path to your JSON file
using ConsoleApp1;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
string filePath = "C:\\Users\\bendi\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\personal_info.json";
Data data = new Data(filePath);
string logpath = "C:\\Users\\bendi\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\logs.txt";
StreamWriter writer1 = new StreamWriter(logpath, true);
if (data.PersonalInfos == null)
{
    Environment.Exit(-1);
    writer1.WriteLine($"{DateTime.Now}: data file not found \n----------------");
}

writer1.WriteLine($"{DateTime.Now}: data file opened");
writer1.Close();
while (true)
{
    StreamWriter writer = new StreamWriter(logpath, true);
    PersonalInfo personal = data.CardValidator();
    if (personal == null)
    {
        Console.WriteLine("credentials Incorrect");
        Thread.Sleep(3000);
        Console.Clear();
        writer.WriteLine($"{DateTime.Now}: inserted credentials not found \n----------------");
        writer.Close();
        continue;
    }
    writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: inserted credentials found");

    Console.Clear();
    try
    {
    Console.WriteLine($"{personal.FirstName}, Your credentials are verified \nplease insert pin code:");
    int pincode = int.Parse(Strings.Trim(Console.ReadLine()));
    if (pincode != personal.PinCode)
    {
        Console.WriteLine($"{personal.FirstName}, Pin code is incorrect");
        Thread.Sleep(3000);
        Console.Clear();
        writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: pincode not validated \n----------------");
        writer.Close();
        continue;
    }
    }
    catch (FormatException)
    {
        Console.WriteLine($"{personal.FirstName}, Pin code is incorrect");
        writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: pincode not validated \n----------------");
        writer.Close();
        continue;
    }
    writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: pincode validated");

    Console.Clear();
    Console.WriteLine(
        "Pin is correct, Choose one of the transactions: \n 1 - check deposit \n 2 - get amount \n 3 - Get last 5 transactions \n 4 - add amount \n 5 - change PIN \n 6 - change amount");
    int trans = int.Parse(Console.ReadLine()); 
    if (personal.TransactionHistory.Count == 0)
    {
        TransactionHistory initial = new TransactionHistory(DateTime.Now, "initial transaction",
            0, 0, 0);
        personal.TransactionHistory.Insert(0, initial);
        string updatedJson0 = JsonConvert.SerializeObject(data.PersonalInfos, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson0);
        writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: initial transaction");
    }
    Console.Clear();

    bool success;
    switch (trans)
    {
        case 1:
            success = personal.BalanceInquery();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: balance inquery transaction, successful={success}");
            break;
        case 2:
            success = personal.GetAmount();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: get amount transaction, successful={success}");
            break;
        case 3:
            success = personal.Get5Transactions();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: get last 5 transactions, successful={success}");
            break;
        case 4:
            success = personal.AddAmount();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: add amount transaction, successful={success}");
            break;
        case 5:
            success = personal.ChangePin();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: change pin transaction, successful={success}");
            break;
        case 6:
            success = personal.ChangeAmount();
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: change amount transaction, successful={success}");
            break;
        default:
            Console.WriteLine("no such transaction type exists");
            success = false;
            writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: transaction type not validated \n----------------");
            writer.Close();
            break;
    }

    if (success)
    {
        string updatedJson = JsonConvert.SerializeObject(data.PersonalInfos, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);
        Console.WriteLine("Transaction was handled successfully, Thank you for choosing us!");
        Thread.Sleep(2000);
        Console.Clear();
    }
    else
    {
        Console.WriteLine("Transaction was unsuccessful");
        Thread.Sleep(2000);
        Console.Clear();
    }
    writer.WriteLine($"{DateTime.Now}: {personal.FirstName} {personal.LastName}: end of transaction \n----------------");
    writer.Close();
}
