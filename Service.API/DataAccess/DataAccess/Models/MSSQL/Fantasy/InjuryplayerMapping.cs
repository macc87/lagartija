//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Models.MSSQL.Fantasy
{
    using System;
    using System.Collections.Generic;
    
    using System.Data.Entity.ModelConfiguration;
    using DataAccess.Configurations;
    using System.ComponentModel.DataAnnotations.Schema;
    internal sealed class InjuryplayerMapping : EntityTypeConfiguration<InjuryPlayer>
    {
                public InjuryplayerMapping()
                {
                    string dbSchema = DataLayerEnvironment.GetInstance().FantasyMssqlProperties.DbSchema; 
    		            this.HasKey(t => t.PlayerId);	
    		            this.ToTable("InjuryPlayer",dbSchema);
                        this.Property(t => t.PlayerId).HasColumnName("PlayerId").IsRequired().IsUnicode(false).HasMaxLength(36);
    
                }
    }
}