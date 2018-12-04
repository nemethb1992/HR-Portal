using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using System.Collections.Generic;

namespace HR_Portal.Source
{
    class Session
    {
        private static ModelUserData UserDatas;
        public static ModelUserData UserData { get { return UserDatas; } set { UserDatas = value; } }

        private static List<ModelApplicantSearchBar> ApplicantSearchValues;
        public static List<ModelApplicantSearchBar> ApplicantSearchValue { get { return ApplicantSearchValues; } set { ApplicantSearchValues = value; } }

        private static List<ModelProjectSearchBar> ProjectSearchValues;
        public static List<ModelProjectSearchBar> ProjectSearchValue { get { return ProjectSearchValues; } set { ProjectSearchValues = value; } }

        private static int ProjectStatus;
        public static int ProjectStatusz { get { return ProjectStatus; } set { ProjectStatus = value; } }

        private static int ApplicantStatus;
        public static int ApplicantStatusz { get { return ApplicantStatus; } set { ApplicantStatus = value; } }

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

        private static CommonUtility.Views lastPages;
        public static CommonUtility.Views lastPage { get { return lastPages; } set { lastPages = value; } }

    }

}
