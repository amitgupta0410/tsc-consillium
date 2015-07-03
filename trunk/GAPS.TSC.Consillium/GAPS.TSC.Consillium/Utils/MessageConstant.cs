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
                case Messages.UpdateTimesheet:
                    messagetext = "Your Timesheet has been updated successfully.";
                    break;
                case Messages.AddTimesheet:
                    messagetext = "Your Timesheet has been added successfully.";
                    break;
                case Messages.DeleteTimesheet:
                    messagetext = "Your Timesheet has been deleted successfully.";
                    break;
                case Messages.NotMoreThanEndDate:
                    messagetext = "The Expected review date and Interim delivery date cannot be greater than project end date.";
                    break;
                case Messages.ErrorInTimesheet:
                    messagetext = "An error occurred while updating the timesheet, Please try again";
                    break;
                case Messages.ProjectAdded:
                    messagetext = "Project has been added successfuly.";
                    break;
                case Messages.ProjectUpdated:
                    messagetext = "Project has been updated successfully.";
                    break;
            }
            return messagetext;
        }
    }
}