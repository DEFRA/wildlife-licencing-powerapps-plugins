using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Setupuk.gov.defra.sdds.automateduitests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.gov.defra.sdds.automateduitests.Setup;

namespace uk.gov.defra.sdds.automateduitests.Pages
{
    public class HappyPathPage
    {
        Hooks _hooks;
        public HappyPathPage(Hooks hooks)

        {
            _hooks = hooks;
        }  

        public void OpenLicenceApp()
        {
            _hooks._xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);
        }

        public void OpenApplications()
        {
          
            _hooks._xrmApp.Navigation.OpenSubArea("Licence", "Applications");
        }


    }
}
