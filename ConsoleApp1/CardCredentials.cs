namespace ConsoleApp1;

public class CardCredentials
{
    public string CardNumber;
    public string ExpirationDate;
    public int CVC;

    public CardCredentials(string cardNumber, string expirationDate, int cvc)
    {
        this.CardNumber = cardNumber;
        this.ExpirationDate = expirationDate;
        CVC = cvc;
    }
}