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

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class ClimaConditions
    {
        #region Primitive Properties
    
        public long ClimaConditionsId
        {
            get; set;
        }
    
        public string Condition
        {
            get; set;
        }

        #endregion

        #region Navigation Properties
    
        public ICollection<Game> Games
        {
            get
            {
                if (_games == null)
                {
                    var newCollection = new FixupCollection<Game>();
                    newCollection.CollectionChanged += FixupGames;
                    _games = newCollection;
                }
                return _games;
            }
            set
            {
                if (!ReferenceEquals(_games, value))
                {
                    var previousValue = _games as FixupCollection<Game>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGames;
                    }
                    _games = value;
                    var newValue = value as FixupCollection<Game>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGames;
                    }
                }
            }
        }
        private ICollection<Game> _games;

        #endregion

        #region Association Fixup
    
        private void FixupGames(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Game item in e.NewItems)
                {
                    item.ClimaCondition = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Game item in e.OldItems)
                {
                    if (ReferenceEquals(item.ClimaCondition, this))
                    {
                        item.ClimaCondition = null;
                    }
                }
            }
        }

        #endregion

    }
}
