using AlgoFit.Data.Context;

namespace AlgoFit.Repositories.Manager
{
    public class RepositoryManager
    {
        private UserRepository _userRepository;
        private IngredientRepository _ingredientRepository;
        private MealRepository _mealRepository;
        private DietRepository _dietRepository;
        private BiometricRepository _biometricRepository;
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
        public IngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository(_context);
                }
                return _ingredientRepository;
            }
        }
        public MealRepository MealRepository
        {
            get
            {
                if (_mealRepository == null)
                {
                    _mealRepository = new MealRepository(_context);
                }
                return _mealRepository;
            }
        }
        public DietRepository DietRepository
        {
            get
            {
                if (_dietRepository == null)
                {
                    _dietRepository = new DietRepository(_context);
                }
                return _dietRepository;
            }
        }
        public BiometricRepository BiometricRepository
        {
            get
            {
                if (_biometricRepository == null)
                {
                    _biometricRepository = new BiometricRepository(_context);
                }
                return _biometricRepository;
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
