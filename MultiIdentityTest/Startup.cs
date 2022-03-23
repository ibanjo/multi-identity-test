using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiIdentityTest.Handlers;
using MultiIdentityTest.Helpers;
using MultiIdentityTest.Middlewares;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace MultiIdentityTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSwaggerGen();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = MyAuthenticationSchemes.RegularScheme;
                    options.DefaultChallengeScheme = MyChallengeSchemes.ContentIntegration;
                })
                .AddCookie(MyAuthenticationSchemes.RegularScheme, options =>
                {
                    options.Cookie.Name = "MultiIdentityTest.DefaultIdentity";
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSigningIn = async context =>
                        {
                            var defaultIdentityClaim = new Claim("kleosIdp", MyChallengeSchemes.Kleos);
                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            claimsIdentity.AddClaim(defaultIdentityClaim);
                            await Task.CompletedTask;
                        }
                    };
                })
                .AddCookie(MyAuthenticationSchemes.CiScheme, options =>
                {
                    options.Cookie.Name = "MultiIdentityTest.ContentIntegrationIdentity";
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSigningIn = async context =>
                        {
                            var idpClaim = new Claim("kleosIdp", MyChallengeSchemes.ContentIntegration);
                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            claimsIdentity.AddClaim(idpClaim);
                            await Task.CompletedTask;
                        }
                    };
                })
                .AddJwtBearer("KleosToken", options =>
                {
                    options.Authority = "https://localhost:44325";
                    options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
                })
                .AddOpenIdConnect(MyChallengeSchemes.ContentIntegration, options =>
                {
                    options.SignInScheme = MyAuthenticationSchemes.CiScheme;
                    options.Authority = "https://accounts.google.com";
                    options.ClientId = "823170831580-eiuugao8ii32qvbba21kf3mj898q4315.apps.googleusercontent.com";
                    options.ClientSecret = "kjG0lmwFlju9JUBhBnUyi5Ty";
                    options.CallbackPath = "/Account/SignInGoogle";
                    options.SignedOutCallbackPath = "/Account/SignOutGoogle";
                })
                .AddOpenIdConnect(MyChallengeSchemes.Kleos, options =>
                {
                    options.SignInScheme = MyAuthenticationSchemes.RegularScheme;
                    options.Authority = "https://localhost:44325";
                    options.ClientId = "kleosbrowser";
                    options.ClientSecret = "25693B1C-4342-4A05-B784-688708212F12";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.CallbackPath = "/Account/SignInKleos";
                    options.SignedOutCallbackPath = "/Account/SignOutKleos";
                    "kleosLegal kleosStateful kleosIdentity offline_access openid profile IdentityServerApi"
                        .Split(" ").ToList().ForEach(scope => options.Scope.Add(scope));
                });

            services.AddAuthorization(options =>
            {
                new List<string>
                {
                    "kleosOps",
                    "IdentityServerApi"
                }.ForEach(scope =>
                {
                    options.AddPolicy(scope, builder =>
                    {
                        builder
                            .RequireAuthenticatedUser()
                            .AddAuthenticationSchemes("KleosToken")
                            .Requirements.Add(new ScopeRequirement(scope));
                        builder.Build();
                    });
                });
            });

            services.AddSingleton<IAuthorizationHandler, RequireScopeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            //app.UseMiddleware<AuthSchemeMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
