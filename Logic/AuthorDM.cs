using AutoMapper;
using DataInfrastructure.Entities;
using System.Collections.Generic;
using ViewModels;
using ViewModels.Enums;

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

            var asc = model.Order[0].Dir == "asc";
            var column = (AuthorColumn)model.Order[0].Column;
            using (var unit = new UnitOfWork())
            {
                IEnumerable<AuthorEM> authorsEM;
                int recordsTotal;
                int recordsFiltered;
                switch (column)
                {
                    default:
                    case AuthorColumn.Name:
                        {
                            if (asc)
                            {
                                authorsEM = unit.Author.GetByNameAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                authorsEM = unit.Author.GetByNameDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                    case AuthorColumn.Surname:
                        {
                            if (asc)
                            {
                                authorsEM = unit.Author.GetBySurnameAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                authorsEM = unit.Author.GetBySurnameDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                    case AuthorColumn.AmountOfBooks:
                        {
                            if (asc)
                            {
                                authorsEM = unit.Author.GetByAmountOfBooksAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                authorsEM = unit.Author.GetByAmountOfBooksDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }

                }
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
