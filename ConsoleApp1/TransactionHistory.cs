using System.Text;

namespace ConsoleApp1;

public class TransactionHistory
{
    public TransactionHistory(DateTime transactionDate, string transactionType, double amountGel, double amountUsd, double amountEur)
    {
        this.TransactionDate = transactionDate;
        this.TransactionType = transactionType;
        AmountGel = amountGel;
        AmountUsd = amountUsd;
        AmountEur = amountEur; 
    }

    public DateTime TransactionDate;
    public string TransactionType;
    public double AmountGel;
    public double AmountUsd;
    public double AmountEur;

    public string TransactiontoString()
    {
        return
            $"transaction date: {TransactionDate} \ntransaction type: {TransactionType}";
    }

    public string AmountToString()
    {
        return $" \namount in GEL: {AmountGel} \namount in USD: {AmountUsd} \namount in EUR: {AmountEur}";
    }
}