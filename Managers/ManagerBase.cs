using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Managers
{
    public abstract class ManagerBase
    {
        public virtual IUser CurrentUser { get; private set; }

        internal ManagerBase(IUser user)
        {
            CurrentUser = user;
        }

        internal void ClearUser()
        {
            CurrentUser = null;
        }

        internal void SetUser(IUser user)
        {
            CurrentUser = user;
        }
    }
}
