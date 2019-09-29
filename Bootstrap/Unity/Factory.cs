using DataInfrastructure.Interfaces;
using Logic;
using LogicInfastructure.Interfaces;
using Shared.Interfaces;
using Unity;

namespace Bootstrap.Unity
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
            _unit.RegisterType<IBookDM, BookDM>();
            _unit.RegisterType<IAuthorDM, AuthorDM>();

            _unit.RegisterType<IBookRepo, DataDapper.Repositories.BookRepo>();
            _unit.RegisterType<IAuthorRepo, DataDapper.Repositories.AuthorRepo>();
        }

        public IBookDM GetBookDM()
        {
            return _unit.Resolve<IBookDM>();
        }

        public T GetService<T>()
        {
            return (T)_unit.Resolve(typeof(T));
        }
    }
}
