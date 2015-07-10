using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Utils
{
    public class MessageConstant
    {

        public static string GetMessage(Messages message)
        {
            string messagetext = null;

            switch (message)
            {
                case Messages.Random:
                    messagetext = "random message";
                    break;
                case Messages.RequestSuccess:
                    messagetext = "Your Request has been sucessfully received";
                    break;
                case Messages.Duplicate:
                    messagetext = "Duplicate entries not allowed.";
                    break;
                case Messages.Update:
                    messagetext = "Your Request has been sucessfully updated";
                    break;
            }
            return messagetext;
        }
    }
}