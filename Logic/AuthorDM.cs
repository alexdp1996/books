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

        public DataTableResponseVM<AuthorBaseVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = Mapper.Map<DataTableRequestEM>(model);

            var asc = model.Order[0].Dir == "asc";
            var column = (AuthorColumn)model.Order[0].Column;
            using (var unit = new UnitOfWork())
            {
                DataTableResponseEM<AuthorEM> responseEM;
                switch (column)
                {
                    default:
                    case AuthorColumn.Name:
                        {
                            if (asc)
                            {
                                responseEM = unit.Author.GetByNameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Author.GetByNameDesc(dataTableEM);
                            }
                            break;
                        }
                    case AuthorColumn.Surname:
                        {
                            if (asc)
                            {
                                responseEM = unit.Author.GetBySurnameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Author.GetBySurnameDesc(dataTableEM);
                            }
                            break;
                        }
                    case AuthorColumn.AmountOfBooks:
                        {
                            if (asc)
                            {
                                responseEM = unit.Author.GetByAmountOfBooksAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Author.GetByAmountOfBooksDesc(dataTableEM);
                            }
                            break;
                        }

                }
                var responseVM = Mapper.Map<DataTableResponseVM<AuthorBaseVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }           
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
