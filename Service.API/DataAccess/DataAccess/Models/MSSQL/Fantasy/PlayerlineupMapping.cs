//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    using System;
    using System.Collections.Generic;
    
    using System.Data.Entity.ModelConfiguration;
    using global::Fantasy.API.DataAccess.Configurations;
    using System.ComponentModel.DataAnnotations.Schema;
    internal sealed class PlayerlineupMapping : EntityTypeConfiguration<PlayerLineup>
    {
                public PlayerlineupMapping()
                {
                    string dbSchema = DataLayerEnvironment.GetInstance().FantasyMssqlProperties.DbSchema; 
    		            this.HasKey(t => new {t.PlayerLineupId, t.LineupId, t.PlayerId});	
    		            this.ToTable("PlayerLineup",dbSchema);
                        this.Property(t => t.PlayerLineupId).HasColumnName("PlayerLineupId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                        this.Property(t => t.LineupId).HasColumnName("LineupId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                        this.Property(t => t.PlayerId).HasColumnName("PlayerId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
    
                }
    }
}
