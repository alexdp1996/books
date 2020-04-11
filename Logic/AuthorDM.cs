using EntityModels;
using Infrastructure.Data;
using Infrastructure.Logic;
using Shared.Interfaces;
using Shared.Services;
using System.Collections.Generic;
using ViewModels;

namespace Logic
{
    public class AuthorDM : BaseDM, IAuthorDM
    {
        public AuthorDM(IFactory factory) : base(factory)
        {

        }

        public IEnumerable<AuthorVM> Get(string term)
        {
            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                var authorsEM = authorRepo.Get(term);
                var authorsVM = MapperService.Map<IEnumerable<AuthorVM>>(authorsEM);
                return authorsVM;
            }
        }

        public DataTableResponseVM<AuthorVM> GetList(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            using (var authorRepo = Factory.GetService<IAuthorRepo>())
            {
                var responseEM = authorRepo.GetList(dataTableEM);
                var responseVM = MapperService.Map<DataTableResponseVM<AuthorVM>>(responseEM);
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
