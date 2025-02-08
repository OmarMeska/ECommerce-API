using EcommerceProject.DTOs;
using EcommerceProject.Models;
using EcommerceProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        private readonly IConfiguration config;

        private UserManager<ApplicationUser> userManager { get; }
        public AccountController( IProductRepository productRepo,UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.productRepo = productRepo;
            this.userManager = userManager;
            this.config = config;
        }


        [HttpPost("Register")]
        public async Task <IActionResult> Register(RegisterDto UserRequist)
        {
            if (ModelState.IsValid) {
                // save to the database 
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserRequist.UserName;
                user.Email = UserRequist.Email;

                var result = await userManager.CreateAsync(user, UserRequist.Password);
            if (result.Succeeded)
                {
                    return Ok("Created Succesuflly");
                }
                foreach (var item in result.Errors) {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto UserRequest)
        {
            if (ModelState.IsValid)
            {
                
                var userDb = await userManager.FindByNameAsync(UserRequest.UserName);
                if (userDb!= null)
                {
                  var result =   await userManager.CheckPasswordAsync(userDb,UserRequest.Password);
                  
                    if (result == true)
                    {
                        // create JWT Token 

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier , userDb.Id));
                        claims.Add(new Claim(ClaimTypes.Name, userDb.UserName));
                        claims.Add(new Claim( JwtRegisteredClaimNames.Jti  , Guid.NewGuid().ToString() ));
                        claims .Add(new Claim(JwtRegisteredClaimNames.Iss, config["JWT:IssuerIP"]));
                        claims .Add(new Claim(JwtRegisteredClaimNames.Aud, config["JWT:AudienceIP"]));

                        var roles = await userManager.GetRolesAsync(userDb);

                        foreach (var role in roles)
                            claims.Add(new Claim(ClaimTypes.Role, role));


                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));
                        SigningCredentials signing = new SigningCredentials(key:key, algorithm:SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                          issuer: config["JWT:IssuerIP"],
                          //audience: config["JWT:AudienceIP"],

                          expires: DateTime.Now.AddHours(1),

                           claims: claims,

                           signingCredentials: signing
                           );

                           return Ok(new
                           {
                               Token = new JwtSecurityTokenHandler().WriteToken(token),
                               Expire= DateTime.Now.AddHours(1)
                           });
                          
                    }
                   
                 }
                ModelState.AddModelError("Wrong", "Password Or UserName is Wrong");
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProfile()
        {
            UserInfo  user = new UserInfo();
            List<Product> userProducts = productRepo.GetAll(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            user.Name = User.FindFirst(ClaimTypes.Name).Value;
            List<ProductInfo> productInfo = new List<ProductInfo>();    

            foreach(var  item in userProducts )
            { 
                ProductInfo product = new ProductInfo();
                 product.Name = item.Name;
                 product.Description = item.Description;
                 product.Price = item.Price;
                 product.Id = item.Id;
                productInfo.Add(product);

            }
                   user.products = productInfo;

            return Ok(user);
        }
    }
}
