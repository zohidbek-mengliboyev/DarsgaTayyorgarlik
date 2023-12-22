namespace DelegateFromUzbek
{
    public class Program
    {
        // public delegate int ExampleDelegate(string s, bool b);
        public static void Main(string[] args)
        {
            // Program p = new Program();
            // ExampleDelegate ed = new ExampleDelegate(p.ExampleFunction);

            // int i = ed("Any text example", true);

            Money money = new Money(10000000);
            MoneyOperator moneyOperator = new MoneyOperator();
            Money.MoneyDelegate moneyDelegate = new Money.MoneyDelegate(moneyOperator.Uzs2Usd);
            moneyOperator.Send(money, "KapitalBank", moneyDelegate);
            Console.WriteLine(moneyDelegate.Method.Name);
            Console.ReadLine();
        }

        // private int ExampleFunction(string str, bool bln)
        // {
        //     int natija = 0;
        //     return natija;
        // }
    }
}