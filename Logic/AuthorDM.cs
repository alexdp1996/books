using AutoMapper;
using Data;
using Data.Repositories;
using Entities;
using System.Collections.Generic;
using ViewModels;

namespace Logic
{
    public class AuthorDM
    {
        public IEnumerable<AuthorBaseVM> Get(string term)
        {
            using (var сontext = new DataContext())
            using (var authorRepo = new AuthorRepo(сontext))
            {
                var authorsEM = authorRepo.Get(term);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids)
        {
            using (var сontext = new DataContext())
            using (var authorRepo = new AuthorRepo(сontext))
            {
                var authorsEM = authorRepo.Get(ids);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var dataTableEM = Mapper.Map<DataTableEM>(model);

            using (var context = new DataContext())
            using (var authorRepo = new AuthorRepo(context))
            {
                var authorsEM = authorRepo.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                result.data = authorsVM;
                result.recordsFiltered = recordsFiltered;
                result.recordsTotal = recordsTotal;
            }
            result.draw = model.Draw;

            return result;
        }

        public AuthorVM Get(long authorId)
        {
            using (var context = new DataContext())
            using (var authorRepo = new AuthorRepo(context))
            {
                var authorEM = authorRepo.Get(authorId);
                var authorVM = Mapper.Map<AuthorVM>(authorEM);
                return authorVM;
            }
        }

        public void Add(AuthorVM model)
        {
            var author = Mapper.Map<AuthorEM>(model);
            using (var context = new DataContext())
            using (var authorRepo = new AuthorRepo(context))
            {
                authorRepo.Add(author);
            }
        }

        public void Delete(long id)
        {
            using (var context = new DataContext())
            using (var authorRepo = new AuthorRepo(context))
            {
                authorRepo.Delete(id);
            }
        }

        public void Update(AuthorVM model)
        {
            var author = Mapper.Map<AuthorEM>(model);

            using (var context = new DataContext())
            using (var authorRepo = new AuthorRepo(context))
            {
                authorRepo.Update(author);
            }
        }
    }
}
