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
    internal sealed class SportMapping : EntityTypeConfiguration<Sport>
    {
                public SportMapping()
                {
                    string dbSchema = DataLayerEnvironment.GetInstance().FantasyMssqlProperties.DbSchema; 
    		            this.HasKey(t => t.SportId);	
    		            this.ToTable("Sport",dbSchema);
                        this.Property(t => t.SportId).HasColumnName("SportId");
    
                }
    }
}
