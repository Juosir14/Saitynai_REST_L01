using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynai_REST_L01.Data;
using Saitynai_REST_L01.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using AutoMapper;
using Newtonsoft.Json.Serialization;

using System.IdentityModel.Tokens.Jwt;
namespace Saitynai_REST_L01.Controllers
{
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "aaasssdddfffggghhhjjjkkklll";

        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));
        //role pridet body admin/user arpns
            
        [HttpGet]
        [Route("api/token/{username}/{password}")]
        public IActionResult Get(string username, string password)
        {
            //tikrint hash password ir username ar atitinka, tada generuot tokena, kuriam butu to user'io role
            //ciklas eina per visus user name, tikrina su duotu, jei sutampa tikrina paasswordus, jei tas sutampa, paima role
            //tada generuoja tokena su ta paimta role
            if(username == password)
                return new ObjectResult(GenerateToken(username));
            else
                return BadRequest();
        }

        private string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                //idet kazkur viduj Role
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)               
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}