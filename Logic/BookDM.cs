﻿using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using LogicInfastructure.Interfaces;
using Shared.Interfaces;
using Shared.Services;
using System.Linq;
using ViewModels;
using ViewModels.Enums;

namespace Logic
{
    public class BookDM : BaseDM, IBookDM
    {
        public BookDM(IFactory factory) : base(factory)
        {

        }

        public void Delete(long id)
        {
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                bookRepo.Delete(id);
            }
        }

        public void Save(BookEditVM model)
        {
            var book = MapperService.Map<BookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            using (var scope = new TransactionService())
            {
                long id;

                if (book.Id.HasValue)
                {
                    id = book.Id.Value;
                    bookRepo.Update(book);
                }
                else
                {
                    id = bookRepo.Add(book);
                }

                bookRepo.UpdateAuthors(id, model.AuthorIds);

                scope.Complete();
            }
        }

        public BookVM Get(long? id)
        {
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                if (id.HasValue)
                {
                    var bookEM = bookRepo.Get(id.Value);
                    var bookVM = MapperService.Map<BookVM>(bookEM);
                    return bookVM;
                }
                return null;
            }
        }

        public DataTableResponseVM<BookVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                var responseEM = bookRepo.Get(dataTableEM);
                var responseVM = MapperService.Map<DataTableResponseVM<BookVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }
            
        }
    }
}
