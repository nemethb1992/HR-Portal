﻿using HR_Portal.Source;
using HR_Portal.Source.Model;
using System.Collections.Generic;

namespace HR_Portal.Source
{
    class Session
    {
        private static List<UserSessionData> UserDatas;
        public static List<UserSessionData> UserData { get { return UserDatas; } set { UserDatas = value; } }

        private static List<Projekt_Search_Memory> ProjectSearchMemory;
        public static List<Projekt_Search_Memory> projectSearchMemory { get { return ProjectSearchMemory; } set { ProjectSearchMemory = value; } }

        private static string ActiveDirectoryDomains;
        public static string ActiveDirectoryDomain { get { return ActiveDirectoryDomains; } set { ActiveDirectoryDomains = value; } }

        private static int ApplicantIDs;
        public static int ApplicantID { get { return ApplicantIDs; } set { ApplicantIDs = value; } }

        private static int InterViewIDs;
        public static int InterViewID { get { return InterViewIDs; } set { InterViewIDs = value; } }

        private static int TelefonSzurts;
        public static int TelefonSzurt { get { return TelefonSzurts; } set { TelefonSzurts = value; } }

        private static int ProjektIDs;
        public static int ProjektID { get { return ProjektIDs; } set { ProjektIDs = value; } }

        private static bool isUpdates;
        public static bool isUpdate { get { return isUpdates; } set { isUpdates = value; } }
    }

}
