using Microsoft.AspNetCore.Identity;
using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTService _jwtService;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, JWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

      

        

        public async Task<AccountResponseDto> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var response = new AccountResponseDto();
            if (user != null)
            {
                if (user.Status != UserStatus.Pending)
                {
                    if (user.Status != UserStatus.Rejected)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                        if (result.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(user);
                            var role = roles[0];
                            string token = await _jwtService.GenerateJwtToken(user, role);
                            response.Status = true;
                            response.Message = "Login Successfully";
                            response.Data = token;
                            return response;
                        }
                        response.Status = false;
                        response.Message = "Password is invalid";
                        return response;
                    }
                    response.Status = false;
                    response.Message = "Your account was rejected by admin.";
                    return response;
                }
                response.Status = false;
                response.Message = "Your account is pending admin approval.";
                return response;
            }
            response.Status = false;
            response.Message = "Email Not Exist";
            return response;
        }

        public async Task<AccountResponseDto> Logout()
        {
            await _signInManager.SignOutAsync();
            var response = new AccountResponseDto()
            {
                Status = true,
                Message = "Logout Successfully"
            };
            return response;
        }

        public async Task<AccountResponseDto> Register(RegisterDto register)
        {
            var user = await _userManager.FindByEmailAsync(register.Email);
            var response = new AccountResponseDto();
            if (user == null)
            {
                user = new User()
                {
                    Name = register.Name,
                    Status = UserStatus.Pending,
                    CreatedDate = DateTime.Now,
                    UserName = register.Email,
                    NormalizedEmail = register.Email.ToUpper(),
                    Email = register.Email,
                };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "customer");
                    response.Status = true;
                    response.Message = "Registration submitted. Please wait for admin approval.";
                    return response;
                }
            }
            response.Status = false;
            response.Message = "Email Already Exist";
            return response;
        }
    }
}
