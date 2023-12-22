public class MoneyOperator
{
    /// <summary>
    /// From UZS to USD
    /// </summary>
    /// <param name="money"></param>
    public void Uzs2Usd(Money money)
    {
        money.Currency = "USD";
        money.Amount /= 12400;
    }

    /// <summary>
    /// From USD to UZS
    /// </summary>
    /// <param name="money"></param>
    public void Usd2Uzs(Money money)
    {
        money.Currency = "UZS";
        money.Amount *= 12360;
    }

    public void Send(Money money, string bank, Money.MoneyDelegate moneyDelegate)
    {
        if (moneyDelegate.Target != null)
            moneyDelegate(money);

        Console.WriteLine(money.Amount.ToString("0.00") + " " + money.Currency + " a sum of money " + "has been successfully transferred to " + bank);
    }
}