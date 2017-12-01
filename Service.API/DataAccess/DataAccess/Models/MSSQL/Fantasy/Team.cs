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
    public class Team
    {
        #region Primitive Properties
    
        public int TeamId
        {
            get; set;
        }
    
        public string TeamName
        {
            get; set;
        }
    
        public string TeamLogo
        {
            get; set;
        }
    
        public int SportId
        {
    get { return _sportId; }
            set
            {
                if (_sportId != value)
                {
                    if (Sport != null && Sport.SportId != value)
                    {
                        Sport = null;
                    }
                    _sportId = value;
                }
            }
        }
        private int _sportId;

        #endregion

        #region Navigation Properties
    
        public Sport Sport
        {
            get { return _sport; }
            set
            {
                if (!ReferenceEquals(_sport, value))
                {
                    var previousValue = _sport;
                    _sport = value;
                    FixupSport(previousValue);
                }
            }
        }
        private Sport _sport;
    
        public ICollection<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    var newCollection = new FixupCollection<Player>();
                    newCollection.CollectionChanged += FixupPlayers;
                    _players = newCollection;
                }
                return _players;
            }
            set
            {
                if (!ReferenceEquals(_players, value))
                {
                    var previousValue = _players as FixupCollection<Player>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPlayers;
                    }
                    _players = value;
                    var newValue = value as FixupCollection<Player>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPlayers;
                    }
                }
            }
        }
        private ICollection<Player> _players;
    
        public Game AwayGame
        {
            get { return _awayGame; }
            set
            {
                if (!ReferenceEquals(_awayGame, value))
                {
                    var previousValue = _awayGame;
                    _awayGame = value;
                    FixupAwayGame(previousValue);
                }
            }
        }
        private Game _awayGame;
    
        public Game HomeGame
        {
            get { return _homeGame; }
            set
            {
                if (!ReferenceEquals(_homeGame, value))
                {
                    var previousValue = _homeGame;
                    _homeGame = value;
                    FixupHomeGame(previousValue);
                }
            }
        }
        private Game _homeGame;

        #endregion

        #region Association Fixup
    
        private void FixupSport(Sport previousValue)
        {
            if (previousValue != null && previousValue.Teams.Contains(this))
            {
                previousValue.Teams.Remove(this);
            }
    
            if (Sport != null)
            {
                if (!Sport.Teams.Contains(this))
                {
                    Sport.Teams.Add(this);
                }
                if (SportId != Sport.SportId)
                {
                    SportId = Sport.SportId;
                }
            }
        }
    
        private void FixupAwayGame(Game previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.AwayTeam, this))
            {
                previousValue.AwayTeam = null;
            }
    
            if (AwayGame != null)
            {
                AwayGame.AwayTeam = this;
            }
        }
    
        private void FixupHomeGame(Game previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.HomeTeam, this))
            {
                previousValue.HomeTeam = null;
            }
    
            if (HomeGame != null)
            {
                HomeGame.HomeTeam = this;
            }
        }
    
        private void FixupPlayers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Player item in e.NewItems)
                {
                    item.Team = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Player item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team, this))
                    {
                        item.Team = null;
                    }
                }
            }
        }

        #endregion

    }
}