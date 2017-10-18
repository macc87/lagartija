//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DataAccess.Models.MSSQL.Fantasy
{
    public class League
    {
        #region Primitive Properties
    
        public string LeagueId
        {
            get; set;
        }
    
        public string Name
        {
            get; set;
        }
    
        public string Alias
        {
            get; set;
        }

        #endregion

        #region Navigation Properties
    
        public ICollection<InjuryTeam> InjuryTeams
        {
            get
            {
                if (_injuryTeams == null)
                {
                    var newCollection = new FixupCollection<InjuryTeam>();
                    newCollection.CollectionChanged += FixupInjuryTeams;
                    _injuryTeams = newCollection;
                }
                return _injuryTeams;
            }
            set
            {
                if (!ReferenceEquals(_injuryTeams, value))
                {
                    var previousValue = _injuryTeams as FixupCollection<InjuryTeam>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupInjuryTeams;
                    }
                    _injuryTeams = value;
                    var newValue = value as FixupCollection<InjuryTeam>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupInjuryTeams;
                    }
                }
            }
        }
        private ICollection<InjuryTeam> _injuryTeams;

        #endregion

        #region Association Fixup
    
        private void FixupInjuryTeams(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (InjuryTeam item in e.NewItems)
                {
                    item.League = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (InjuryTeam item in e.OldItems)
                {
                    if (ReferenceEquals(item.League, this))
                    {
                        item.League = null;
                    }
                }
            }
        }

        #endregion

    }
}