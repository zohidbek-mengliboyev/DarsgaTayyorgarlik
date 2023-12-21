namespace Delegates
{
    public sealed class Program
    {
        internal delegate void FeedBack(Int32 value);
        public static void Main(string[] args)
        {
            StaticDelegateDemo();
            InstanceDelegateDemo();

            ChainDelegateDemo1(new Program());
            ChainDelegateDemo2(new Program());
        }

        private static void StaticDelegateDemo()
        {
            Console.WriteLine("----- Static Delegate Demo -----");
            Counter(1, 3, null);
            Counter(1, 3, new FeedBack(Program.FeedbackToConsole));
            Counter(1, 3, new FeedBack(FeedbackToMsgBox)); // "Program." is optional
            Console.WriteLine();
        }

        private static void InstanceDelegateDemo()
        {
            Console.WriteLine("----- Instance Delegate Demo -----");
            Program p = new Program();
            Counter(1, 3, new FeedBack(p.FeedbackToFile));
            Console.WriteLine();
        }

        private static void ChainDelegateDemo1(Program p)
        {
            Console.WriteLine("----- Chain Delegate Demo 1 -----");
            FeedBack fb1 = new FeedBack(FeedbackToConsole);
            FeedBack fb2 = new FeedBack(FeedbackToMsgBox);
            FeedBack fb3 = new FeedBack(p.FeedbackToFile);

            FeedBack fbChain = null;
            fbChain = (FeedBack) Delegate.Combine(fbChain, fb1);
            fbChain = (FeedBack) Delegate.Combine(fbChain, fb2);
            fbChain = (FeedBack) Delegate.Combine(fbChain, fb3);
            Counter(1, 2, fbChain);

            Console.WriteLine();
            fbChain = (FeedBack) Delegate.Remove(fbChain, new FeedBack(FeedbackToMsgBox));
            Counter(1, 2, fbChain);
        }

        private static void ChainDelegateDemo2(Program p)
        {
            Console.WriteLine("----- Chain Delegate Demo 2 -----");
            FeedBack fb1 = new FeedBack(FeedbackToConsole);
            FeedBack fb2 = new FeedBack(FeedbackToMsgBox);
            FeedBack fb3 = new FeedBack(p.FeedbackToFile);

            FeedBack fbChain = null;
            fbChain += fb1;
            fbChain += fb2;
            fbChain += fb3;
            Counter(1, 2, fbChain);

            Console.WriteLine();
            fbChain -= new FeedBack(FeedbackToMsgBox);
            Counter(1, 2, fbChain);
        }

        private static void Counter(Int32 from, Int32 to, FeedBack fb)
        {
            for (Int32 val = from; val <= to; val++)
            {
                // If any callbacks are specified, call them
                if (fb != null)
                    fb(val);
            }
        }

        private static void FeedbackToConsole(Int32 value)
        {
            Console.WriteLine("Item = " + value);
        }

        private static void FeedbackToMsgBox(Int32 value)
        {
            MessageBox.Show("Item = " + value);
        }

        private void FeedbackToFile(Int32 value)
        {
            using (StreamWriter sw = new StreamWriter("Status", true))
            {
                sw.WriteLine("Item = " + value);
            }
        }
    }
}