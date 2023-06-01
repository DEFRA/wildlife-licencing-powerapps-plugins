﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.ApplicationPriority
{
    public static class ApplicationEnum
    {
        public enum Species
        {
            Badgers = 100000000,
            Raven = 100000001,
            RedKite = 100000002,
            Buzzard = 100000003,
            Beavers = 100000004
        };

        public enum Priority
        {
            one = 100000002,
            two = 100000001,
            three = 100000000,
            four = 100000003
        }

        public enum SettType
        {
            Main_no_alternative_sett = 100000000,
            Main_alternative_sett_available = 100000001,
            Annex_Subsidiary = 100000002,
            Outlier = 100000003,
            All_Sett_Types = 100000004,
            Other = 100000005
        }

        public enum Licensing_Policy
        {
            one = 100000000,
            two = 100000001,
            three = 100000002,
            four = 100000003
        }

        public enum License_Methods
        {
            Obstructing_Sett_Entrances = 100000010
        }

        public enum MonthsOfTheYear
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum ApplicationTypes
        {
            A24 = 452120000,
            Others = 452120001
        }

        public enum SpecieSubjects
        {
            Badgers = 452120000,
            Others = 452120001
        }
    }
}
