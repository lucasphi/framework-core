using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mail
{
    /// <summary>
    /// Defines the common interface for mailing services.
    /// </summary>
    public interface IFWMailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that on completion updates the output.</returns>
        Task SendEmailAsync(FWMail mail);
    }
}
