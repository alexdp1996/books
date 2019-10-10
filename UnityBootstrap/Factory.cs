using DataInfrastructure.Interfaces;
using Logic;
using LogicInfastructure.Interfaces;
using Shared.Interfaces;
using Unity;
using Unity.Injection;

namespace UnityBootstrap
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

            _unit.RegisterType<IBookRepo, DataDapper.Repositories.BookRepo>();
            _unit.RegisterType<IAuthorRepo, DataDapper.Repositories.AuthorRepo>();
        }

        public T GetService<T>()
        {
            return (T)_unit.Resolve(typeof(T));
        }
    }
}
