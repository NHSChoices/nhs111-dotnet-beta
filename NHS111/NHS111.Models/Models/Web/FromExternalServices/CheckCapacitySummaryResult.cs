namespace NHS111.Models.Models.Web.FromExternalServices
{
    public class CheckCapacitySummaryResult
    {
        public int IdField;
        public Capacity CapacityField;
        public string NameField;
        public string ContactDetailsField;
        public string AddressField;
        public string PostcodeField;
        public int NorthingsField;
        public bool NorthingsSpecifiedField;
        public int EastingsField;
        public bool EastingsSpecifiedField;
        public string UrlField;
        public string NotesField;
        public bool ObsoleteField;
        public System.DateTime UpdateTimeField;
        public bool OpenAllHoursField;
        public ServiceCareItemRotaSession[] RotaSessionsField;
        public ServiceDetails ServiceTypeField;
        public string OdsCodeField;
        public ServiceDetails RootParentField;
    }

    public enum Capacity
    {

        /// <remarks/>
        High,

        /// <remarks/>
        Low,

        /// <remarks/>
        None,
    }

    public  class ServiceCareItemRotaSession 
    {

        public DayOfWeek StartDayOfWeekField;

        public TimeOfDay StartTimeField;

        public DayOfWeek EndDayOfWeekField;

        public TimeOfDay EndTimeField;

        public string StatusField;
    }

    public enum DayOfWeek
    {

        /// <remarks/>
        Sunday,

        /// <remarks/>
        Monday,

        /// <remarks/>
        Tuesday,

        /// <remarks/>
        Wednesday,

        /// <remarks/>
        Thursday,

        /// <remarks/>
        Friday,

        /// <remarks/>
        Saturday,
    }

    public  class TimeOfDay
    {

        public short HoursField;

        public short MinutesField;
    }

    public class ServiceDetails
    {

        public long IdField;

        public string NameField;
    }
}