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
    public class Sport
    {
        #region Primitive Properties
    
        public long SportId
        {
            get; set;
        }
    
        public string Name
        {
            get; set;
        }

        #endregion

        #region Navigation Properties
    
        public ICollection<Team> Teams
        {
            get
            {
                if (_teams == null)
                {
                    var newCollection = new FixupCollection<Team>();
                    newCollection.CollectionChanged += FixupTeams;
                    _teams = newCollection;
                }
                return _teams;
            }
            set
            {
                if (!ReferenceEquals(_teams, value))
                {
                    var previousValue = _teams as FixupCollection<Team>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTeams;
                    }
                    _teams = value;
                    var newValue = value as FixupCollection<Team>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTeams;
                    }
                }
            }
        }
        private ICollection<Team> _teams;
    
        public ICollection<Position> Positions
        {
            get
            {
                if (_positions == null)
                {
                    var newCollection = new FixupCollection<Position>();
                    newCollection.CollectionChanged += FixupPositions;
                    _positions = newCollection;
                }
                return _positions;
            }
            set
            {
                if (!ReferenceEquals(_positions, value))
                {
                    var previousValue = _positions as FixupCollection<Position>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPositions;
                    }
                    _positions = value;
                    var newValue = value as FixupCollection<Position>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPositions;
                    }
                }
            }
        }
        private ICollection<Position> _positions;
    
        public ICollection<Goal> Goals
        {
            get
            {
                if (_goals == null)
                {
                    var newCollection = new FixupCollection<Goal>();
                    newCollection.CollectionChanged += FixupGoals;
                    _goals = newCollection;
                }
                return _goals;
            }
            set
            {
                if (!ReferenceEquals(_goals, value))
                {
                    var previousValue = _goals as FixupCollection<Goal>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGoals;
                    }
                    _goals = value;
                    var newValue = value as FixupCollection<Goal>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGoals;
                    }
                }
            }
        }
        private ICollection<Goal> _goals;

        #endregion

        #region Association Fixup
    
        private void FixupTeams(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Team item in e.NewItems)
                {
                    item.Sport = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Team item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sport, this))
                    {
                        item.Sport = null;
                    }
                }
            }
        }
    
        private void FixupPositions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Position item in e.NewItems)
                {
                    item.Sport = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Position item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sport, this))
                    {
                        item.Sport = null;
                    }
                }
            }
        }
    
        private void FixupGoals(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Goal item in e.NewItems)
                {
                    item.Sport = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Goal item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sport, this))
                    {
                        item.Sport = null;
                    }
                }
            }
        }

        #endregion

    }
}
