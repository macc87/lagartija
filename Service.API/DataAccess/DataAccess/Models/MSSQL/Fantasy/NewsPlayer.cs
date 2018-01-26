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
    public class NewsPlayer
    {
        #region Primitive Properties
    
        public long NewsPlayerId
        {
            get; set;
        }
    
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
    
        public long NewsId
        {
    get { return _newsId; }
            set
            {
                if (_newsId != value)
                {
                    if (News != null && News.NewsId != value)
                    {
                        News = null;
                    }
                    _newsId = value;
                }
            }
        }
        private long _newsId;

        #endregion

        #region Navigation Properties
    
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
    
        public News News
        {
            get { return _news; }
            set
            {
                if (!ReferenceEquals(_news, value))
                {
                    var previousValue = _news;
                    _news = value;
                    FixupNews(previousValue);
                }
            }
        }
        private News _news;

        #endregion

        #region Association Fixup
    
        private void FixupPlayer(Player previousValue)
        {
            if (previousValue != null && previousValue.NewsPlayers.Contains(this))
            {
                previousValue.NewsPlayers.Remove(this);
            }
    
            if (Player != null)
            {
                if (!Player.NewsPlayers.Contains(this))
                {
                    Player.NewsPlayers.Add(this);
                }
                if (PlayerId != Player.PlayerId)
                {
                    PlayerId = Player.PlayerId;
                }
            }
        }
    
        private void FixupNews(News previousValue)
        {
            if (previousValue != null && previousValue.NewsPlayers.Contains(this))
            {
                previousValue.NewsPlayers.Remove(this);
            }
    
            if (News != null)
            {
                if (!News.NewsPlayers.Contains(this))
                {
                    News.NewsPlayers.Add(this);
                }
                if (NewsId != News.NewsId)
                {
                    NewsId = News.NewsId;
                }
            }
        }

        #endregion

    }
}
