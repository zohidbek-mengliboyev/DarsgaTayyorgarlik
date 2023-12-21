using System.Reflection;
using System.Text;

namespace DelegateChainInvocation
{
    internal sealed class Light
    {
        public String SwitchPosition()
        {
            return "The light is off.";
        }
    }

    internal sealed class Fan
    {
        public String Speed()
        {
            throw new InvalidOperationException("The fan broke due to overheating.");
        }
    }

    internal sealed class Speaker
    {
        public String Volume()
        {
            return "The volume is loud.";
        }
    }

    public sealed class Program
    {
        private delegate String GetStatus();
        public static void Main()
        {
            GetStatus getStatus = null;

            getStatus += new GetStatus(new Light().SwitchPosition);
            getStatus += new GetStatus(new Fan().Speed);
            getStatus += new GetStatus(new Speaker().Volume);

            Console.WriteLine(GetComponentStatusReport(getStatus));
        }

        private static string GetComponentStatusReport(GetStatus status)
        {
            if (status == null) return null;

            StringBuilder report = new StringBuilder();

            Delegate[] arrayOfDelegates = status.GetInvocationList();

            foreach (GetStatus getStatus in arrayOfDelegates)
            {
                try
                {
                    report.AppendFormat("{0}{1}{1}", getStatus(), Environment.NewLine);
                }
                catch (InvalidOperationException e)
                {
                    Object component = getStatus.Target;
                    report.AppendFormat(
                        "Failed to get status from {1}{2}{0} Error: {3}{0}{0}",
                        Environment.NewLine,
                        ((component == null) ? "" : component.GetType() + "."),
                        getStatus.GetMethodInfo().Name,
                        e.Message
                    );    
                }
            }

            return report.ToString();
        }
    }
}