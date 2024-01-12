using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace ConsoleApp1;

public class Data
{
    public List<PersonalInfo> PersonalInfos;

    public Data(string filepath)
    {
        try
        {
        string jsonData = File.ReadAllText(filepath);
        PersonalInfos = JsonConvert.DeserializeObject<List<PersonalInfo>>(jsonData);
        }
        catch (Exception)
        {
            PersonalInfos = null;
            Console.WriteLine("Database is unavailable, sorry for inconvenience, you can come back later");
        }
    }

    public PersonalInfo CardValidator()
    {
        Console.WriteLine("Please insert Card Credentials");
        Console.WriteLine("card Number: ");
        string usercreds = Strings.Trim(Console.ReadLine());
        Console.WriteLine("Expiration Date: ");
        string userexp = Strings.Trim(Console.ReadLine());
        Console.WriteLine("CVC: ");
        try
        {
            int usercvc = int.Parse(Strings.Trim(Console.ReadLine()));
            foreach (var person in PersonalInfos)
            {
                if (person.CardCredentials.CardNumber.Equals(usercreds)
                    && person.CardCredentials.ExpirationDate.Equals(userexp)
                    && person.CardCredentials.CVC == usercvc)
                {
                    Console.Clear();
                    return person;
                }
            }
            Console.Clear();
            return null;
        }
        catch (Exception)
        {   
            Console.Clear();
            return null;
        }
    
        
    }

}