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
    public class PlayerLineup
    {
        #region Primitive Properties
    
        public long PlayerLineupId
        {
            get; set;
        }
    
        public long LineupId
        {
    get { return _lineupId; }
            set
            {
                if (_lineupId != value)
                {
                    if (LineUps != null && LineUps.LineUpId != value)
                    {
                        LineUps = null;
                    }
                    _lineupId = value;
                }
            }
        }
        private long _lineupId;
    
        public long PlayerId
        {
    get { return _playerId; }
            set
            {
                if (_playerId != value)
                {
                    if (Player != null && Player.PlayerId != value)
                    {
                        Player = null;
                    }
                    _playerId = value;
                }
            }
        }
        private long _playerId;

        #endregion

        #region Navigation Properties
    
        public LineUp LineUps
        {
            get { return _lineUps; }
            set
            {
                if (!ReferenceEquals(_lineUps, value))
                {
                    var previousValue = _lineUps;
                    _lineUps = value;
                    FixupLineUps(previousValue);
                }
            }
        }
        private LineUp _lineUps;
    
        public Player Player
        {
            get { return _player; }
            set
            {
                if (!ReferenceEquals(_player, value))
                {
                    var previousValue = _player;
                    _player = value;
                    FixupPlayer(previousValue);
                }
            }
        }
        private Player _player;

        #endregion

        #region Association Fixup
    
        private void FixupLineUps(LineUp previousValue)
        {
            if (previousValue != null && previousValue.PlayerLineup.Contains(this))
            {
                previousValue.PlayerLineup.Remove(this);
            }
    
            if (LineUps != null)
            {
                if (!LineUps.PlayerLineup.Contains(this))
                {
                    LineUps.PlayerLineup.Add(this);
                }
                if (LineupId != LineUps.LineUpId)
                {
                    LineupId = LineUps.LineUpId;
                }
            }
        }
    
        private void FixupPlayer(Player previousValue)
        {
            if (previousValue != null && previousValue.PlayerLineups.Contains(this))
            {
                previousValue.PlayerLineups.Remove(this);
            }
    
            if (Player != null)
            {
                if (!Player.PlayerLineups.Contains(this))
                {
                    Player.PlayerLineups.Add(this);
                }
                if (PlayerId != Player.PlayerId)
                {
                    PlayerId = Player.PlayerId;
                }
            }
        }

        #endregion

    }
}