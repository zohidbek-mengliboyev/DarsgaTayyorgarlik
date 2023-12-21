using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms; // Add this line

namespace Delegates;

public sealed class Program
{
    [DllImport("kernel32.dll")]
    static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;

    internal delegate void FeedBack(Int32 value);
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {

        // redirect console output to parent process;
        // must be before any calls to Console.WriteLine()
        AttachConsole(ATTACH_PARENT_PROCESS);

        // // to demonstrate where the console output is going
        // int argCount = args == null ? 0 : args.Length;
        // Console.WriteLine("nYou specified {0} arguments:", argCount);
        // for (int i = 0; i < argCount; i++)
        // {
        //     Console.WriteLine("  {0}", args[i]);
        // }
        StaticDelegateDemo();
        InstanceDelegateDemo();

        ChainDelegateDemo1(new Program());
        ChainDelegateDemo2(new Program());

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());

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
        fbChain = (FeedBack)Delegate.Combine(fbChain, fb1);
        fbChain = (FeedBack)Delegate.Combine(fbChain, fb2);
        fbChain = (FeedBack)Delegate.Combine(fbChain, fb3);
        Counter(1, 2, fbChain);

        Console.WriteLine();
        fbChain = (FeedBack)Delegate.Remove(fbChain, new FeedBack(FeedbackToMsgBox));
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
                fb(val); // => fb.Invoke(val);
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
