﻿using Dapper.FluentMap.Dommel.Mapping;
using EntityModels;

namespace AWSLambda.Maps
{
    public class AuthorMap : DommelEntityMap<AuthorEM>
    {
        public AuthorMap()
        {
            ToTable("Author");
            Map(a => a.Id).IsIdentity();
            Map(a => a.Books).Ignore();
        }
    }
}
