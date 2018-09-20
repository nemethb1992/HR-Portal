using HR_Portal.Source;
using System.Collections.Generic;

namespace HR_Portal.Source
{
    public struct UserSessionData
    {
        public int id;
        public string username;
        public string name;
        public string email;
        public int kategoria;
        public int jogosultsag;
        public int validitas;
        public string belepve;
        public string reg_datum;
    }
    class Session
    {
        private static List<UserSessionData> UserDatas;
        public List<UserSessionData> UserData { get { return UserDatas; } set { UserDatas = value; } }

        private static List<Projekt_Search_Memory> ProjectSearchMemory;
        public List<Projekt_Search_Memory> projectSearchMemory { get { return ProjectSearchMemory; } set { ProjectSearchMemory = value; } }

        private static string ActiveDirectoryDomains;
        public string ActiveDirectoryDomain { get { return ActiveDirectoryDomains; } set { ActiveDirectoryDomains = value; } }

        private static int ApplicantIDs;
        public int ApplicantID { get { return ApplicantIDs; } set { ApplicantIDs = value; } }

        private static int InterViewIDs;
        public int InterViewID { get { return InterViewIDs; } set { InterViewIDs = value; } }

        private static int TelefonSzurts;
        public int TelefonSzurt { get { return TelefonSzurts; } set { TelefonSzurts = value; } }

        private static int ProjektIDs;
        public int ProjektID { get { return ProjektIDs; } set { ProjektIDs = value; } }

        private static bool isUpdates;
        public bool isUpdate { get { return isUpdates; } set { isUpdates = value; } }
    }

}
