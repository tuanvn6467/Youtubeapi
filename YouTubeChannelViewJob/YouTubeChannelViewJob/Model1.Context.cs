﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YouTubeChannelViewJob
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class youtubeEntities : DbContext
    {
        public youtubeEntities()
            : base("name=youtubeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Channel> Channels { get; set; }
    
        public virtual ObjectResult<yt_Get_Channel_Result> yt_Get_Channel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<yt_Get_Channel_Result>("yt_Get_Channel");
        }
    }
}