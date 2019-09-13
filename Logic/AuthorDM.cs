using AutoMapper;
using Data;
using Data.Repositories;
using DataInfrastructure.Entities;
using System.Collections.Generic;
using ViewModels;

namespace Logic
{
    public class AuthorDM
    {
        public IEnumerable<AuthorBaseVM> Get(string term)
        {
            using (var unit = new UnitOfWork())
            {
                var authorsEM = unit.Author.Get(term);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids)
        {
            using (var unit = new UnitOfWork())
            {
                var authorsEM = unit.Author.Get(ids);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public DataTableResponseVM Get(DataTableRequestVM model)
        {
            var result = new DataTableResponseVM();

            var dataTableEM = Mapper.Map<DataTableRequestEM>(model);

            using (var unit = new UnitOfWork())
            {
                var authorsEM = unit.Author.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);
                var authorsVM = Mapper.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                result.data = authorsVM;
                result.recordsFiltered = recordsFiltered;
                result.recordsTotal = recordsTotal;
            }
            result.draw = model.Draw;

            return result;
        }

        public AuthorVM Get(long id)
        {
            using (var unit = new UnitOfWork())
            {
                var authorEM = unit.Author.Get(id);
                var authorVM = Mapper.Map<AuthorVM>(authorEM);
                return authorVM;
            }
        }

        public void Delete(long id)
        {
            using (var unit = new UnitOfWork())
            {
                unit.Author.Delete(id);
            }
        }

        public void Save(AuthorVM model)
        {
            var author = Mapper.Map<AuthorEM>(model);

            using (var unit = new UnitOfWork())
            {
                unit.Author.Save(author);
            }
        }
    }
}
