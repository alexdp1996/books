using Logic;
using Infrastructure.Logic;
using Shared.Interfaces;
using Unity.Injection;
using Infrastructure.Data;
using Data.Repositories;

namespace Unity
{
    public class Factory : IFactory
    {
        private IUnityContainer _unit;
        public Factory()
        {
            _unit = new UnityContainer();
            Register();
        }

        private void Register()
        {
            _unit.RegisterType<IBookDM, BookDM>(new InjectionConstructor(this));
            _unit.RegisterType<IAuthorDM, AuthorDM>(new InjectionConstructor(this));

            _unit.RegisterType<IBookRepo, BookRepo>();
            _unit.RegisterType<IAuthorRepo, AuthorRepo>();
        }

        public T GetService<T>()
        {
            return (T)_unit.Resolve(typeof(T));
        }
    }
}
