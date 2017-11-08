using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fantasy.API.Host.Models
{
    public class LogBrowserModel
    {
        #region Constructor
        public LogBrowserModel()
        {
            FillApplicationLogList();
        }
        #endregion

        #region DataMembers
        public SelectList ApplicationNameListItem = null;
        
        [Required]
        [Display(Name = "Date From")]
        public String DateFrom;

        [Required]
        [Display(Name = "Date To")]
        public String DateTo;

        public String Search;

        #endregion

        #region Private Methods
        private void FillApplicationLogList()
        {
            ApplicationNameListItem = EnumHelper.SelectListFor(LogType.Fantasy);
        }
        #endregion
    }
}