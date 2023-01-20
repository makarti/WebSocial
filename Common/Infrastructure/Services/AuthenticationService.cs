using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Core.Repositories;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountRepository _accountRep;
        private readonly ISignInManager _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(
            IAccountRepository accountRep, 
            ISignInManager signInManager, 
            IUnitOfWork unitOfWork)
        {
            _accountRep = accountRep;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync(Account account, string password)
        {
            var existingAccount = await _accountRep.GetAsync(account.Login);
            if (existingAccount != null) throw new AuthenticationException($"account {account.Login} already exists");

            await CreateAccountAsync(account, password);

            await _unitOfWork.CommitAsync();
        }

        public async Task CreateAccountAsync(Account account, string password)
        {
            account.Password = new PasswordHasher<User>().HashPassword(account, password);
            account.CreateDate = DateTime.UtcNow;

            await _accountRep.AddAsync(account);
        }

        public async Task<Account> LoginAsync(string login, string password)
        {
            var account = await _accountRep.GetAsync(login);
            if (login == null) throw new AuthenticationException($"Account {login} not found");

            var verificationResult = new PasswordHasher<Account>().VerifyHashedPassword(account, account.Password, password);
            if (verificationResult != PasswordVerificationResult.Success) throw new AuthenticationException("Invalid password");

            await _signInManager.SignInAsync(account);

            return account;
        }
    }
}