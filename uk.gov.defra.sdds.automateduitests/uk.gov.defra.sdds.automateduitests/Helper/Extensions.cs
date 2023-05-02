using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace uk.gov.defra.sdds.automateduitests.Helper
{
    internal static class Extensions
    {
        internal static SecureString ToSecureString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            var secureString = new SecureString();
            Array.ForEach(value.ToCharArray(), x => secureString.AppendChar(x));
            return secureString;
        }
    }
}
