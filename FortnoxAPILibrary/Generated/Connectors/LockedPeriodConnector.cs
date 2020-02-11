using FortnoxAPILibrary;
using FortnoxAPILibrary.Entities;

// ReSharper disable UnusedMember.Global

namespace FortnoxAPILibrary.Connectors
{
    /// <remarks/>
    public class LockedPeriodConnector : EntityConnector<LockedPeriod, EntityWrapper<LockedPeriod>, Sort.By.LockedPeriod?>
	{
	    /// <summary>
        /// Use with Find() to limit the search result
        /// </summary>
        [SearchParameter("filter")]
		public Filter.LockedPeriod? FilterBy { get; set; }


		/// <remarks/>
		public LockedPeriodConnector()
		{
			Resource = "settings/lockedperiod";
		}

        public LockedPeriod Get()
        {
            return BaseFind()?.Entity;
        }
    }
}