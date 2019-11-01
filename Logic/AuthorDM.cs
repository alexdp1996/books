using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using LogicInfastructure.Interfaces;
using Shared.Interfaces;
using Shared.Services;
using System.Collections.Generic;
using ViewModels;
using ViewModels.Enums;

namespace Logic
{
    public class AuthorDM : BaseDM, IAuthorDM
    {
        public AuthorDM(IFactory factory) : base(factory)
        {

        }

        public IEnumerable<AuthorBaseVM> Get(string term)
        {
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                var authorsEM = authorRepo.Get(term);
                var authorsVM = MapperService.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids)
        {
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                var authorsEM = authorRepo.Get(ids);
                var authorsVM = MapperService.Map<IEnumerable<AuthorBaseVM>>(authorsEM);
                return authorsVM;
            }
        }

        public DataTableResponseVM<AuthorBaseVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            var asc = model.Order[0].Dir == "asc";
            var column = (AuthorColumn)model.Order[0].Column;
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                DataTableResponseEM<AuthorEM> responseEM;
                switch (column)
                {
                    default:
                    case AuthorColumn.Name:
                        {
                            if (asc)
                            {
                                responseEM = authorRepo.GetByNameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = authorRepo.GetByNameDesc(dataTableEM);
                            }
                            break;
                        }
                    case AuthorColumn.Surname:
                        {
                            if (asc)
                            {
                                responseEM = authorRepo.GetBySurnameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = authorRepo.GetBySurnameDesc(dataTableEM);
                            }
                            break;
                        }
                    case AuthorColumn.AmountOfBooks:
                        {
                            if (asc)
                            {
                                responseEM = authorRepo.GetByAmountOfBooksAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = authorRepo.GetByAmountOfBooksDesc(dataTableEM);
                            }
                            break;
                        }

                }
                var responseVM = MapperService.Map<DataTableResponseVM<AuthorBaseVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }           
        }

        public AuthorVM Get(long? id)
        {
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                if (id.HasValue)
                {
                    var authorEM = authorRepo.Get(id.Value);
                    var authorVM = MapperService.Map<AuthorVM>(authorEM);
                    return authorVM;
                }
                return null;
            }
        }

        public void Delete(long id)
        {
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                authorRepo.Delete(id);
            }
        }

        public void Save(AuthorVM model)
        {
            var author = MapperService.Map<AuthorEM>(model);

            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                if (author.Id.HasValue)
                {
                    authorRepo.Update(author);
                }
                else
                {
                    authorRepo.Add(author);
                }  
            }
        }
    }
}
