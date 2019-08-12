using AutoMapper;
using Data;
using Data.Repositories;
using Entities;
using Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Logic
{
    public class AuthorDM
    {
        private DataContext DataContext { get; }
        private AuthorRepo AuthorRepo { get; }

        public AuthorDM()
        {
            DataContext = new DataContext();
            AuthorRepo  = new AuthorRepo(DataContext);
        }

        public IEnumerable<AuthorBaseVM> Get(string term)
        {
            var authors = AuthorRepo.Get(term);
            var result = new List<AuthorBaseVM>();
            foreach (var author in authors)
            {
                result.Add(new AuthorBaseVM
                {
                    Id = author.Id,
                    Name = author.Name,
                    Surname = author.Surname
                });
            }
            return result;
        }

        public IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids)
        {
            return Mapper.Map<IEnumerable<AuthorBaseVM>>(AuthorRepo.Get(ids));
        }

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var dataTableEM = Mapper.Map<DataTableEM>(model);

            var authors = AuthorRepo.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);

            result.data = Mapper.Map<IEnumerable<AuthorBaseVM>>(authors);            
            result.recordsFiltered = recordsFiltered;
            result.recordsTotal = recordsTotal;
            result.draw = model.Draw;

            return result;
        }

        public AuthorVM Get(long authorId)
        {
            return Mapper.Map<AuthorVM>(AuthorRepo.Get(authorId));
        }

        public void Add(AuthorVM model)
        {
            var author = Mapper.Map<AuthorEM>(model);
            AuthorRepo.Add(author);
        }

        public void Delete(long authorId)
        {
            AuthorRepo.Delete(authorId);
        }

        public void Update(AuthorVM model)
        {
            var author = Mapper.Map<AuthorEM>(model);
            AuthorRepo.Update(author);
        }
    }
}
