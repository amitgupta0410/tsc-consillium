using System.Text;

namespace GAPS.TSC.CONS.Util.Extenders
{
    public static class DecimalExtender
    {
        public static string ToHoursAndMins(this decimal amount)
        {
            if (amount == 0)
            {
                return "0 hrs";
            }

            var hours = (int)amount / 60;
            var minutes = (int)amount % 60;

            var sb = new StringBuilder();
            if (amount < 0)
            {
                sb.Append("- ");
            }

            if (hours > 0)
            {
                sb.AppendFormat("{0} hrs ", hours);
            }
            if (minutes > 0)
            {
                sb.AppendFormat("{0} mins", minutes);
            }

            return sb.ToString();
        }

    }
}
