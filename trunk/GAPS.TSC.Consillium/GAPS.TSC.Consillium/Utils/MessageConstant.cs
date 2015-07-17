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
                case Messages.AddLeadSuccess:
                    messagetext = "Lead has been saved successfully";
                    break;
                case Messages.ConvertLead:
                    messagetext = "Lead has been converted to expert successfully";
                    break;
                case Messages.DeleteLead:
                    messagetext = "Lead has been deleted successfully.";
                    break;
                case Messages.AddCvSuccess:
                    messagetext = "File has been uploaded successfully";
                    break;
                case Messages.AddNoteSuccess:
                    messagetext = "Note has been saved successfully.";
                    break;
                case Messages.Danger:
                    messagetext = "Please select atleast one Expert";
                    break;
            }
            return messagetext;
        }
    }
}