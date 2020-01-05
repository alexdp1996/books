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

            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                var responseEM = authorRepo.Get(dataTableEM);
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

        public void Update(AuthorVM model)
        {
            var author = MapperService.Map<AuthorEM>(model);

            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                authorRepo.Update(author);
            }
        }

        public void Create(AuthorVM model)
        {
            var author = MapperService.Map<AuthorEM>(model);

            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                authorRepo.Create(author);
            }
        }
    }
}
