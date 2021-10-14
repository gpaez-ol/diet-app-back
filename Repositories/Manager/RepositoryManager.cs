using AlgoFit.Data.Context;

namespace AlgoFit.Repositories.Manager
{
    public class RepositoryManager
    {
        private UserRepository _userRepository;
        private AlgoFitContext _context;
        public RepositoryManager(AlgoFitContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
