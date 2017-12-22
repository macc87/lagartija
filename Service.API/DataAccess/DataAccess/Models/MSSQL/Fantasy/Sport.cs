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
    
        public int SportId
        {
            get; set;
        }
    
        public string Name
        {
            get; set;
        }
    
        public int PositionId
        {
    get { return _positionId; }
            set
            {
                if (_positionId != value)
                {
                    if (Position != null && Position.PositionId != value)
                    {
                        Position = null;
                    }
                    _positionId = value;
                }
            }
        }
        private int _positionId;

        #endregion

        #region Navigation Properties
    
        public Position Position
        {
            get { return _position; }
            set
            {
                if (!ReferenceEquals(_position, value))
                {
                    var previousValue = _position;
                    _position = value;
                    FixupPosition(previousValue);
                }
            }
        }
        private Position _position;
    
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

        #endregion

        #region Association Fixup
    
        private void FixupPosition(Position previousValue)
        {
            if (previousValue != null && previousValue.Sports.Contains(this))
            {
                previousValue.Sports.Remove(this);
            }
    
            if (Position != null)
            {
                if (!Position.Sports.Contains(this))
                {
                    Position.Sports.Add(this);
                }
                if (PositionId != Position.PositionId)
                {
                    PositionId = Position.PositionId;
                }
            }
        }
    
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

        #endregion

    }
}
