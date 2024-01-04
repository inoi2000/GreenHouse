using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Exeptions;
using GreenHouse.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GreenHouse.Domain.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IApplicationPasswordHasher _hasher;
        private readonly ILogger<AdminService> _logger;

        public AdminService(
            IAdminRepository accountRepository,
            IApplicationPasswordHasher hasher,
            ILogger<AdminService> logger)
        {
            _adminRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task Register(string login, string password, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть пустым или содержать только пробел.", nameof(login)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password)); }

            var existedAdmin = await _adminRepository.FindAdminByLogin(login, token);
            if (existedAdmin is not null)
            {
                throw new LoginAlreadyExistsExeption();
            }
            Admin admin = new Admin(login, EncryptPassword(password));
            await _adminRepository.Add(admin, token);
        }

        public virtual async Task<Admin> Authorisation(string login, string password, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNullOrEmpty($"\"{nameof(login)}\" не может быть пустым или содержать только пробел.", nameof(login));
            ArgumentNullException.ThrowIfNullOrEmpty($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password));

            var admin = await _adminRepository.FindAdminByLogin(login, token);
            if (admin is null)
            {
                throw new AdminNotFoundExeption("Admin with given login not found");
            }

            var isPasswordValid = _hasher.VerifyHashedPassword(admin.PasswordHash, password, out var rehashNeeded);
            if (!isPasswordValid)
            {
                throw new InvalidPasswordException("Invalid password");
            }

            if (rehashNeeded)
            {
                await RehashPassword(password, admin, token);
            }

            return admin;

        }

        private Task RehashPassword(string password, Admin admin, CancellationToken token)
        {
            admin.PasswordHash = EncryptPassword(password);
            return _adminRepository.Update(admin, token);
        }

        private string EncryptPassword(string password)
        {
            var hashPassword = _hasher.HashPassword(password);
            _logger.LogDebug($"Password hasher {hashPassword}");
            return hashPassword;
        }

        public async Task<Admin> GetAccountById(Guid id, CancellationToken cancellationToken)
        {
            return await _adminRepository.GetById(id, cancellationToken);
        }
    }
}
