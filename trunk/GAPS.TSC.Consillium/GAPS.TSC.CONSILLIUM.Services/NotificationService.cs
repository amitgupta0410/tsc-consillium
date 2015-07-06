using System.Collections.Generic;
using Hangfire;
using Postal;

namespace GAPS.TSC.CONS.Services{
    public class NotificationService{
        public static string SendEmail(BaseEmail email) {
            return SendActualEmail(email);
        }

        public static string SendActualEmail(BaseEmail email) {
            var service = new EmailService();
            var message = service.CreateMailMessage(email);
            var content = message.Body;
            var subject = message.Subject;

            BackgroundJob.Enqueue(() =>
                RestService.Post("/notifications", new Dictionary<string, string> {
                    {"To", email.ToId.ToString()},
                    {"ToEmail", email.ToEmail ?? string.Empty},
                    {"From", email.FromId.ToString()},
                    {"Subject", subject},
                    {"Body", content}
                }));
            return content;
        }
    }


    public class BaseEmail : Email{
        public int ToId { get; set; }
        public int FromId { get; set; }
        public string ToEmail { get; set; }

        public BaseEmail(int fromId, int toId, string toEmail) {
            this.FromId = fromId;
            this.ToId = toId;
            this.ToEmail = toEmail;
        }

        public BaseEmail(int fromId, int toId) {
            this.FromId = fromId;
            this.ToId = toId;
            this.ToEmail = string.Empty;
        }
    }
}