# JWT Authentication Eğitimi

## İçindekiler
1. [JWT Nedir?](#jwt-nedir)
2. [Proje Yapısı](#proje-yapısı)
3. [Gerekli NuGet Paketleri](#gerekli-nuget-paketleri)
4. [Konfigürasyon](#konfigürasyon)
5. [Model ve DTO Yapıları](#model-ve-dto-yapıları)
6. [JWT Token Servisi](#jwt-token-servisi)
7. [Authentication Servisi](#authentication-servisi)
8. [API Controller](#api-controller)
9. [Program.cs Konfigürasyonu](#programcs-konfigürasyonu)
10. [Swagger Entegrasyonu](#swagger-entegrasyonu)
11. [Test ve Kullanım](#test-ve-kullanım)

---

## JWT Nedir?

**JWT (JSON Web Token)**, modern web uygulamalarında kullanılan güvenli bir kimlik doğrulama ve bilgi alışverişi standardıdır. 

### JWT'nin Yapısı
JWT üç bölümden oluşur:
1. **Header**: Token tipi ve şifreleme algoritması
2. **Payload**: Kullanıcı bilgileri (Claims)
3. **Signature**: Token'ın doğruluğunu kontrol eden imza

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiVXNlck5hbWUiOiJhZG1pbiJ9.SIGNATURE
```

### JWT'nin Avantajları
- ✅ Stateless (sunucuda oturum saklamaya gerek yok)
- ✅ Ölçeklenebilir (microservice mimarilere uygun)
- ✅ Farklı domain'ler arası kullanılabilir
- ✅ Mobile uygulamalar için ideal

---

## Proje Yapısı

Bu projede **4 katmanlı mimari** kullanılmıştır:

```
OgrenciServis/
├── OgrenciServis.Api/          # API Controller'ları
├── OgrenciServis.Logic/        # İş mantığı ve servisler
├── OgrenciServis.Models/       # Entity ve DTO'lar
└── OgrenciServis.DataAccess/   # Veritabanı işlemleri
```

---

## Gerekli NuGet Paketleri

### OgrenciServis.Api
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
```

### OgrenciServis.Logic
```xml
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.0.0" />
<PackageReference Include="Microsoft.IdentityModel.Tokens" Version="7.0.0" />
```

---

## Konfigürasyon

### appsettings.json

JWT konfigürasyonu için `appsettings.json` dosyasına aşağıdaki ayarları ekliyoruz:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TestDataBase;Username=pass;Password=user"
  },
  "Jwt": {
    "SecretKey": "BuCokGizliVeUzunBirSecretKeyOlmalidirEnAz32Karakter-Ertugrul",
    "Issuer": "OgrenciServis",
    "Audience": "OgrenciServisUsers",
    "ExpirationInHours": 1
  },
  "AllowedHosts": "*"
}
```

### Önemli Notlar:
- **SecretKey**: En az 32 karakter uzunluğunda olmalı
- **Issuer**: Token'ı üreten uygulama adı
- **Audience**: Token'ı kullanacak hedef kitle
- **ExpirationInHours**: Token geçerlilik süresi

⚠️ **Güvenlik Uyarısı**: Production ortamında `SecretKey` değerini **environment variable** veya **Azure Key Vault** gibi güvenli yöntemlerle saklayın!

---

## Model ve DTO Yapıları

### User.cs - Veritabanı Modeli

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciServis.Models
{
    public class User
    {
        [Key]
        [Column(name: "user_id")]
        public int UserId { get; set; }

        [Column(name: "username")]
        public string UserName { get; set; }

        [Column(name: "password")]
        public string Password { get; set; }

        [Column(name: "role")]
        public string Role { get; set; }
    }
}
```

### LoginRequestDto.cs - Giriş İsteği

```csharp
namespace OgrenciServis.Models.DTO
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
```

### LoginResponseDto.cs - Giriş Yanıtı

```csharp
namespace OgrenciServis.Models.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
```

**DTO (Data Transfer Object) Kullanımının Faydaları:**
- 🔒 Hassas bilgileri (şifre) gizleme
- 📦 Sadece gerekli verileri taşıma
- 🎯 API contract'ını belirleme

---

## JWT Token Servisi

### IJwtTokenService.cs - Interface

```csharp
using OgrenciServis.Models;

namespace OgrenciServis.Logic.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
```

### JwtTokenService.cs - Implementasyon

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OgrenciServis.Logic.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // 1. Secret key'i okuyup byte dizisine çevir
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = Encoding.UTF8.GetBytes(secretKey);

            // 2. Token tanımlaması (descriptor) oluştur
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Claim bilgileri ekle (Payload kısmı)
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("Role", user.Role),
                    new Claim("EMail", "ertugrulkra@gmail.com"),
                    new Claim("Phone", "555 623 67 63"),
                    new Claim("CName", "Ertugrul-PCX")
                }),

                // Token süresi
                Expires = DateTime.UtcNow.AddDays(1),

                // İmzalama (Signature kısmı)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // 3. Token oluştur
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 4. Token'ı string olarak döndür
            return tokenHandler.WriteToken(token);
        }
    }
}
```

### Token Oluşturma Adımları:

1. **Secret Key Hazırlığı**: Konfigürasyondan secret key okunur ve byte dizisine çevrilir
2. **Claim'lerin Eklenmesi**: Kullanıcı bilgileri (UserId, UserName, Role vb.) token'a eklenir
3. **Token Süresinin Ayarlanması**: `Expires` özelliği ile token geçerlilik süresi belirlenir
4. **İmzalama**: HMAC-SHA256 algoritması ile token imzalanır
5. **String'e Dönüştürme**: Token string formatına çevrilir ve döndürülür

### Claims Nedir?
**Claims**, token içinde taşınan kullanıcı bilgileridir. Örneğin:
- `UserId`: Kullanıcının benzersiz kimliği
- `Role`: Kullanıcının rolü (Admin, User, vb.)
- `UserName`: Kullanıcı adı

---

## Authentication Servisi

### IAuthService.cs - Interface

```csharp
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Interface
{
    public interface IAuthService
    {
        LoginResponseDto? Login(LoginRequestDto loginRequest);
    }
}
```

### AuthService.cs - Implementasyon

```csharp
using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly OkulContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(OkulContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public LoginResponseDto? Login(LoginRequestDto loginRequest)
        {
            // 1. Kullanıcı adı ile veritabanında user sorgula
            var user = (from u in _context.Users
                        where u.UserName == loginRequest.Username
                        select u).FirstOrDefault();

            // 2. Kullanıcı bulunamazsa null döndür
            if (user == null)
                return null;

            // 3. Şifre kontrolü (production'da hash'lenmiş şifre kontrol edilmeli)
            // if (!PasswordHasher.Verify(loginRequest.Password, user.Password))
            //     return null;

            // 4. Kullanıcı bulunursa JWT token oluştur
            var token = _jwtTokenService.GenerateToken(user);

            // 5. LoginResponseDto oluştur ve döndür
            return new LoginResponseDto
            {
                Token = token,
                Username = user.UserName,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddDays(1)
            };
        }
    }
}
```

### Login İşlem Akışı:

```
Kullanıcı → LoginRequestDto → AuthService
                                    ↓
                        Veritabanı Sorgusu
                                    ↓
                        Kullanıcı Bulundu mu?
                                    ↓
                              JwtTokenService
                                    ↓
                          Token Oluşturuldu
                                    ↓
                            LoginResponseDto
```

⚠️ **Güvenlik Notu**: Production ortamında şifreler **BCrypt** veya **PBKDF2** gibi algoritmalarla hash'lenmelidir!

---

## API Controller

### AuthController.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Models.DTO;

namespace OgrenciServis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequest)
        {
            // 1. Model validasyonu kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Login işlemini yap
            var result = _authService.Login(loginRequest);

            // 3. Sonuç kontrolü
            if (result == null)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

            // 4. Token bilgilerini döndür
            return Ok(result);
        }
    }
}
```

### HTTP Status Kodları:
- **200 OK**: Başarılı login, token döndürülür
- **400 Bad Request**: Model validasyon hatası
- **401 Unauthorized**: Kullanıcı adı veya şifre yanlış

---

## Program.cs Konfigürasyonu

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Logic.Services;
using System.Text;

namespace OgrenciServis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers ekle
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger yapılandırması (JWT desteği ile)
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OgrenciServis API",
                    Version = "v1"
                });

                // JWT Authentication için Swagger yapılandırması
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // DbContext ekle
            builder.Services.AddDbContext<OkulContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            // JWT Authentication Konfigürasyonu
            var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
            var key = Encoding.UTF8.GetBytes(jwtSecretKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Token süresi dolma toleransı
                };
            });

            var app = builder.Build();

            // Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ÖNEMLİ: Sıralama önemli!
            app.UseAuthentication();  // İlk önce authentication
            app.UseAuthorization();   // Sonra authorization

            app.MapControllers();
            app.Run();
        }
    }
}
```

### Token Validation Parametreleri:

| Parametre | Açıklama |
|-----------|----------|
| `ValidateIssuerSigningKey` | Token'ın imzasını doğrula |
| `IssuerSigningKey` | İmzalama için kullanılan key |
| `ValidateIssuer` | Token'ı üreten kaynağı doğrula |
| `ValidateAudience` | Token'ın hedef kitlesini doğrula |
| `ValidateLifetime` | Token'ın süresini doğrula |
| `ClockSkew` | Süre kontrolünde tolerans |

### Middleware Sırası:
```
Request → UseAuthentication() → UseAuthorization() → Controllers
```

⚠️ **Önemli**: `UseAuthentication()` her zaman `UseAuthorization()`'dan önce çağrılmalıdır!

---

## Swagger Entegrasyonu

Swagger'da JWT token kullanmak için yapılandırma:

1. **Authorize Butonu**: Swagger UI'da sağ üstte "Authorize" butonu görünür
2. **Token Girişi**: Login endpoint'inden aldığınız token'ı "Bearer YOUR_TOKEN" formatında girin
3. **Test**: Diğer korumalı endpoint'leri test edin

### Swagger'da Token Kullanımı:

```
1. /api/auth/login endpoint'ini çağır
2. Dönen token'ı kopyala
3. "Authorize" butonuna tıkla
4. "Bearer YOUR_TOKEN_HERE" formatında gir
5. Korumalı endpoint'leri test et
```

---

## Test ve Kullanım

### 1. Login İsteği

**Endpoint**: `POST /api/auth/login`

**Request Body**:
```json
{
  "username": "admin",
  "password": "admin123"
}
```

**Response** (200 OK):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "role": "Admin",
  "expiresAt": "2025-12-05T10:30:00Z"
}
```

### 2. Korumalı Endpoint'e İstek

**Header**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 3. Controller'da Token Bilgisine Erişim

```csharp
[Authorize] // Bu attribute endpoint'i korur
[HttpGet("profile")]
public IActionResult GetProfile()
{
    // Token'dan kullanıcı bilgisini al
    var userId = User.FindFirst("UserId")?.Value;
    var userName = User.FindFirst("UserName")?.Value;
    var role = User.FindFirst("Role")?.Value;

    return Ok(new { userId, userName, role });
}
```

### 4. Role-Based Authorization

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("users/{id}")]
public IActionResult DeleteUser(int id)
{
    // Sadece Admin rolündeki kullanıcılar bu endpoint'e erişebilir
    return Ok("Kullanıcı silindi");
}
```

---

## Özet ve Best Practices

### ✅ Yapılması Gerekenler:

1. **Güvenli Secret Key**: En az 32 karakter, production'da environment variable
2. **HTTPS Kullanımı**: Token'lar her zaman HTTPS üzerinden iletilmeli
3. **Token Süresi**: Kısa süreli token'lar kullanın (1-24 saat)
4. **Refresh Token**: Uzun süreli oturumlar için refresh token mekanizması ekleyin
5. **Şifre Hash'leme**: BCrypt, Argon2 gibi güçlü algoritmalar kullanın
6. **Rate Limiting**: Login endpoint'ine rate limiting ekleyin
7. **Logging**: Başarısız login denemelerini loglayın

### ❌ Yapılmaması Gerekenler:

1. **Plain Text Şifre**: Şifreleri asla plain text olarak saklamayın
2. **Client-Side Secret**: Secret key'i client-side'da kullanmayın
3. **Hassas Bilgi**: Token'da çok hassas bilgiler (şifre, kredi kartı) taşımayın
4. **Uzun Token Süreleri**: 30 günlük token'lar güvenlik riski oluşturur
5. **Global Exception Handler Eksikliği**: Tüm hataları düzgün handle edin

### 🔄 Tam İşlem Akışı:

```
1. Kullanıcı → Username/Password gönderir
2. AuthService → Veritabanında kullanıcıyı arar
3. Kullanıcı bulunursa → JwtTokenService token üretir
4. Token → Client'a döndürülür
5. Client → Her istekte token'ı Authorization header'ında gönderir
6. Middleware → Token'ı validate eder
7. Valid ise → Controller'a erişim sağlanır
8. Invalid ise → 401 Unauthorized döner
```

---

## Ek Kaynaklar

- [JWT.io](https://jwt.io/) - Token decode ve debug için
- [Microsoft JWT Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/)
- [OWASP Authentication Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Authentication_Cheat_Sheet.html)

---

## Gelişmiş Konular

### Refresh Token Implementasyonu

Token süresi dolduğunda kullanıcıyı tekrar login sayfasına yönlendirmek yerine, refresh token ile yeni access token alabilirsiniz:

```csharp
public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
}

[HttpPost("refresh")]
public ActionResult<LoginResponseDto> RefreshToken([FromBody] RefreshTokenDto dto)
{
    // Refresh token'ı validate et
    // Yeni access token üret
    // Döndür
}
```

### Custom Claims

Özel claim'ler ekleyebilirsiniz:

```csharp
new Claim("Department", "IT"),
new Claim("EmployeeId", "12345"),
new Claim(ClaimTypes.Email, user.Email)
```

### Policy-Based Authorization

Daha gelişmiş yetkilendirme için policy kullanabilirsiniz:

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireClaim("Role", "Admin"));
    
    options.AddPolicy("ITDepartment", policy => 
        policy.RequireClaim("Department", "IT"));
});

[Authorize(Policy = "AdminOnly")]
public IActionResult AdminAction() { }
```

---

**Son Güncelleme**: 4 Aralık 2025  
**Yazar**: OgrenciServis Projesi Bazlı Eğitim İçeriği
