﻿using Dapper.FluentMap.Dommel.Mapping;
using DataInfrastructure.Entities;

namespace DataDapper.Maps
{
    public class BookMap : DommelEntityMap<BookEM>
    {
        public BookMap()
        {
            ToTable("Book");
            Map(b => b.Id).IsIdentity();
            Map(b => b.Authors).Ignore();
        }
    }
}
