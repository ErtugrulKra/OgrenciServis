# DERS 2: Exception Handling ve Hata Yönetimi

## 📚 Ders İçeriği

Bu derste, API'lerde hata yönetimi nasıl yapılır, exception'lar nasıl yakalanır ve kullanıcıya nasıl gösterilir öğreneceğiz.

---

## 1. Exception (Hata) Nedir?

**Exception**, program çalışırken beklenmeyen bir durum oluştuğunda ortaya çıkan hata nesnesidir.

### Ne Zaman Exception Oluşur?

- Veritabanı bağlantısı koparsa
- Olmayan bir kayıt silinmeye çalışılırsa
- Null bir değer üzerinde işlem yapılırsa
- Geçersiz veri girilirse
- Dosya bulunamazsa

### Basit Örnek

```csharp
// Bu kod bir exception fırlatabilir
int sayi = int.Parse("abc"); // FormatException fırlatır
```

---

## 2. Try-Catch Blokları Nedir?

**Try-Catch**, exception'ları yakalamak ve yönetmek için kullanılan bir yapıdır.

### Temel Yapı

```csharp
try
{
    // Hata oluşabilecek kod buraya yazılır
    // Örnek: Veritabanı işlemi
}
catch (Exception ex)
{
    // Hata oluşursa buraya gelir
    // Hata ile ilgili işlemler yapılır
}
```

### Mevcut Kodunuzdaki Örnek

Şu anda `OgrenciServisImpl.cs` dosyanızda try-catch blokları var:

```18:37:OgrenciServis.Logic/Services/OgrenciServisImpl.cs
try
{
    var sonuc = from ogrenci in _context.Ogrenciler
                join sinif in _context.Siniflar on ogrenci.SinifId equals sinif.SinifId
                select new OgrenciDto
                {
                    OgrenciId = ogrenci.OgrenciId,
                    Adi = ogrenci.Adi,
                    Soyadi = ogrenci.Soyadi,
                    DogumTarihi = ogrenci.DogumTarihi,
                    Sube = sinif.Sube,
                    SinifNo = sinif.SinifNo
                };

    return sonuc.ToList();
}
catch (Exception)
{
    throw;
}
```

**Sorun:** Bu kod sadece exception'ı tekrar fırlatıyor, özel bir işlem yapmıyor.

---

## 3. Mevcut Kodunuzdaki Hata Yönetimi

### Controller'larda Null Kontrolü

Şu anda `OgrenciController.cs` dosyanızda şöyle bir kontrol var:

```32:37:OgrenciServis.Api/Controllers/OgrenciController.cs
var ogrenciDto = this.ogrenci.OgrenciGetirById(id);

if (ogrenciDto == null)
{
    return NotFound($"Öğrenci ID {id} bulunamadı.");
}
```

Bu yaklaşım çalışıyor ama her controller'da tekrar tekrar yazmak zorunda kalıyoruz.

### Daha İyi Bir Yaklaşım: Custom Exception

Custom exception kullanarak kodu daha temiz ve merkezi hale getirebiliriz.

---

## 4. Custom Exception Oluşturma

### Adım 1: NotFoundException Sınıfı Oluşturma

Önce bir `Exceptions` klasörü oluşturalım ve içine `NotFoundException.cs` ekleyelim.

**Dosya Yolu:** `OgrenciServis.Models/Exceptions/NotFoundException.cs`

```csharp
namespace OgrenciServis.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key) 
            : base($"{name} ({key}) bulunamadı.")
        {
        }
    }
}
```

**Açıklama:**
- `Exception` sınıfından türetiyoruz
- İki constructor var: biri mesaj alıyor, diğeri entity adı ve key alıyor
- Örnek: `new NotFoundException("Öğrenci", 5)` → "Öğrenci (5) bulunamadı."

### Adım 2: Service'te Exception Fırlatma

Şimdi `OgrenciServisImpl.cs` dosyasını güncelleyelim:

```csharp
using OgrenciServis.Models.Exceptions;

public OgrenciDto? OgrenciGetirById(int id)
{
    try
    {
        var sonuc = (from ogrenci in _context.Ogrenciler
                     join sinif in _context.Siniflar on ogrenci.SinifId equals sinif.SinifId
                     where ogrenci.OgrenciId == id
                     select new OgrenciDto
                     {
                         OgrenciId = ogrenci.OgrenciId,
                         Adi = ogrenci.Adi,
                         Soyadi = ogrenci.Soyadi,
                         DogumTarihi = ogrenci.DogumTarihi,
                         Sube = sinif.Sube,
                         SinifNo = sinif.SinifNo
                     }).FirstOrDefault();

        if (sonuc == null)
        {
            throw new NotFoundException("Öğrenci", id);
        }

        return sonuc;
    }
    catch (NotFoundException)
    {
        throw; // NotFoundException'ı tekrar fırlat
    }
    catch (Exception ex)
    {
        // Beklenmeyen hatalar için loglama yapılabilir
        throw new Exception("Bir hata oluştu.", ex);
    }
}
```

### Adım 3: Controller'da Try-Catch Kullanma

`OgrenciController.cs` dosyasını güncelleyelim:

```csharp
using OgrenciServis.Models.Exceptions;

[HttpGet("{id}")]
public ActionResult<OgrenciDto> GetOgrenci(int id)
{
    try
    {
        var ogrenciDto = this.ogrenci.OgrenciGetirById(id);
        return Ok(ogrenciDto);
    }
    catch (NotFoundException ex)
    {
        return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Bir hata oluştu.");
    }
}
```

**Sorun:** Her controller metodunda try-catch yazmak çok tekrarlı. Daha iyi bir çözüm: **Global Exception Handler**.

---

## 5. Global Exception Handler Middleware

**Middleware**, HTTP request'leri işlemeden önce veya sonra çalışan bir yapıdır. Global exception handler, tüm exception'ları tek bir yerde yakalar.

### Adım 1: ErrorResponse DTO Oluşturma

**Dosya Yolu:** `OgrenciServis.Models/DTO/ErrorResponseDto.cs`

```csharp
namespace OgrenciServis.Models.DTO
{
    public class ErrorResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? Details { get; set; }
    }
}
```

### Adım 2: GlobalExceptionHandlerMiddleware Oluşturma

**Dosya Yolu:** `OgrenciServis.Api/Middleware/GlobalExceptionHandlerMiddleware.cs`

```csharp
using OgrenciServis.Models.DTO;
using OgrenciServis.Models.Exceptions;
using System.Net;
using System.Text.Json;

namespace OgrenciServis.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next, 
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir hata oluştu: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponseDto();

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = notFoundEx.Message;
                    break;

                case ArgumentException argEx:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = argEx.Message;
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Bir hata oluştu.";
                    errorResponse.Details = exception.Message; // Sadece development'ta göster
                    break;
            }

            response.StatusCode = errorResponse.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(jsonResponse);
        }
    }
}
```

**Açıklama:**
- `RequestDelegate _next`: Bir sonraki middleware'i çağırmak için
- `InvokeAsync`: Her request'te çalışır, exception'ları yakalar
- `HandleExceptionAsync`: Exception tipine göre uygun HTTP status code ve mesaj döner

### Adım 3: Program.cs'de Middleware Kaydetme

`Program.cs` dosyasını güncelleyelim:

```csharp
using OgrenciServis.Api.Middleware;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handler Middleware'i ekle
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

**Önemli:** Middleware'i `UseAuthentication()` ve `UseAuthorization()` öncesine koymalıyız ki tüm exception'lar yakalanabilsin.

### Adım 4: Controller'ları Temizleme

Artık controller'larda try-catch yazmaya gerek yok:

```csharp
[HttpGet("{id}")]
public ActionResult<OgrenciDto> GetOgrenci(int id)
{
    var ogrenciDto = this.ogrenci.OgrenciGetirById(id);
    return Ok(ogrenciDto);
}
```

Eğer `OgrenciGetirById` null dönerse veya exception fırlatırsa, middleware otomatik olarak yakalayıp uygun response'u dönecek.

---

## 6. Hata Response Formatı

### Başarılı Response (200 OK)

```json
{
  "ogrenciId": 1,
  "adi": "Ahmet",
  "soyadi": "Yılmaz"
}
```

### Hata Response (404 Not Found)

```json
{
  "message": "Öğrenci (5) bulunamadı.",
  "statusCode": 404,
  "details": null
}
```

### Hata Response (500 Internal Server Error)

```json
{
  "message": "Bir hata oluştu.",
  "statusCode": 500,
  "details": "Veritabanı bağlantı hatası..."
}
```

---

## 7. Pratik Uygulama Adımları

### Adım 1: NotFoundException Sınıfı Oluştur

1. `OgrenciServis.Models` projesinde `Exceptions` klasörü oluştur
2. `NotFoundException.cs` dosyası ekle
3. Yukarıdaki kodu yapıştır

### Adım 2: ErrorResponseDto Oluştur

1. `OgrenciServis.Models/DTO` klasörüne `ErrorResponseDto.cs` ekle
2. Yukarıdaki kodu yapıştır

### Adım 3: GlobalExceptionHandlerMiddleware Oluştur

1. `OgrenciServis.Api` projesinde `Middleware` klasörü oluştur
2. `GlobalExceptionHandlerMiddleware.cs` dosyası ekle
3. Yukarıdaki kodu yapıştır

### Adım 4: Program.cs'i Güncelle

1. `Program.cs` dosyasını aç
2. `using OgrenciServis.Api.Middleware;` ekle
3. `app.UseMiddleware<GlobalExceptionHandlerMiddleware>();` satırını ekle

### Adım 5: Service'i Güncelle

1. `OgrenciServisImpl.cs` dosyasını aç
2. `OgrenciGetirById` metodunu güncelle
3. Null kontrolünde `NotFoundException` fırlat

### Adım 6: Controller'ı Temizle

1. `OgrenciController.cs` dosyasını aç
2. `GetOgrenci` metodundan try-catch'i kaldır
3. Sadece service çağrısı ve `Ok()` döndür

### Adım 7: Test Et

1. API'yi çalıştır
2. Swagger'da olmayan bir ID ile istek at (örn: `/api/Ogrenci/999`)
3. 404 Not Found response'unu kontrol et

---

## 8. Örnek Senaryolar

### Senaryo 1: Olmayan Öğrenci Getirme

**Request:**
```
GET /api/Ogrenci/999
```

**Response (404):**
```json
{
  "message": "Öğrenci (999) bulunamadı.",
  "statusCode": 404,
  "details": null
}
```

### Senaryo 2: Veritabanı Bağlantı Hatası

**Request:**
```
GET /api/Ogrenci
```

**Response (500):**
```json
{
  "message": "Bir hata oluştu.",
  "statusCode": 500,
  "details": "Connection string hatası..."
}
```

---

## 9. İleri Seviye: Daha Fazla Custom Exception

### BadRequestException

```csharp
namespace OgrenciServis.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
```

### UnauthorizedException

```csharp
namespace OgrenciServis.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
```

Middleware'de bu exception'ları da handle edebilirsiniz:

```csharp
case BadRequestException badRequestEx:
    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
    errorResponse.Message = badRequestEx.Message;
    break;

case UnauthorizedException unauthorizedEx:
    errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
    errorResponse.Message = unauthorizedEx.Message;
    break;
```

---

## 10. Özet

### Öğrendiklerimiz

1. ✅ **Exception nedir?** - Program çalışırken oluşan hatalar
2. ✅ **Try-Catch nedir?** - Exception'ları yakalamak için kullanılan yapı
3. ✅ **Custom Exception** - Kendi exception sınıflarımızı oluşturma
4. ✅ **Global Exception Handler** - Tüm exception'ları tek yerde yakalama
5. ✅ **Hata Response Formatı** - Standart hata mesajları döndürme

### Avantajlar

- ✅ Kod tekrarı azalır
- ✅ Merkezi hata yönetimi
- ✅ Tutarlı hata mesajları
- ✅ Daha temiz controller kodları
- ✅ Kolay bakım ve geliştirme

### Sonraki Adımlar

- [ ] NotFoundException sınıfını oluştur
- [ ] GlobalExceptionHandlerMiddleware'i ekle
- [ ] Program.cs'i güncelle
- [ ] Service'lerde exception fırlat
- [ ] Controller'ları temizle
- [ ] Test et

---

## 📝 Notlar

- Middleware sırası önemlidir - Exception handler'ı en üste koyun
- Development'ta detaylı hata mesajları gösterin, production'da gizleyin
- Logging ekleyerek hataları kaydedin (bir sonraki ders: Logging)
- Her exception tipi için uygun HTTP status code kullanın

---

## 🎯 DERS SONU ÖDEV

### Ödev Konusu: BadRequestException ve DersController'da Exception Handling

Bu ödevde, öğrendiklerinizi pekiştirmek için `BadRequestException` oluşturacak ve `DersController`'da kullanacaksınız.

### Ödev Adımları

#### 1. BadRequestException Sınıfı Oluştur

- `OgrenciServis.Models/Exceptions` klasörüne `BadRequestException.cs` dosyası ekleyin
- `NotFoundException` örneğini referans alarak benzer bir yapı oluşturun
- İki constructor ekleyin:
  - Biri sadece mesaj alan
  - Diğeri entity adı ve açıklama alan

**Beklenen Kod Yapısı:**
```csharp
namespace OgrenciServis.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        // Constructor'ları buraya yazın
    }
}
```

#### 2. GlobalExceptionHandlerMiddleware'i Güncelle

- `GlobalExceptionHandlerMiddleware.cs` dosyasını açın
- `HandleExceptionAsync` metodundaki switch-case yapısına `BadRequestException` case'ini ekleyin
- HTTP Status Code olarak `400 BadRequest` kullanın

**İpucu:** `NotFoundException` case'ini örnek alabilirsiniz.

#### 3. DersController'da Validation Kontrolü

- `DersController.cs` dosyasındaki `PostDers` metodunu bulun
- `ModelState.IsValid` kontrolünden sonra, eğer geçersizse `BadRequestException` fırlatın
- Controller'daki try-catch bloklarını kaldırın (Global Exception Handler zaten var)

**Örnek Senaryo:**
```csharp
[HttpPost]
[Authorize(Roles = "Admin")]
public ActionResult<Ders> PostDers([FromBody] Ders ders)
{
    if (!ModelState.IsValid)
    {
        throw new BadRequestException("Ders bilgileri geçersiz.");
    }
    
    // Devamı...
}
```

#### 4. DersServis'te Exception Fırlatma

- `DersServis.cs` dosyasını açın
- `DersGetirById` metodunu bulun
- Eğer ders bulunamazsa `NotFoundException` fırlatın
- `DersGuncelle` metodunda da benzer kontrol ekleyin

**İpucu:** `OgrenciServisImpl.cs` dosyasındaki `OgrenciGetirById` metodunu örnek alabilirsiniz.

#### 5. Test Etme

Aşağıdaki senaryoları Swagger üzerinden test edin:

**Test 1: Olmayan Ders Getirme**
- `GET /api/Ders/999` isteği atın
- 404 Not Found response'unu kontrol edin
- Response body'de `message` alanının "Ders (999) bulunamadı." olduğunu doğrulayın

**Test 2: Geçersiz Ders Ekleme**
- `POST /api/Ders` isteği atın
- Body'de geçersiz veri gönderin (örn: DersAdi boş)
- 400 Bad Request response'unu kontrol edin
- Response body'de `message` alanının "Ders bilgileri geçersiz." olduğunu doğrulayın

**Test 3: Olmayan Ders Güncelleme**
- `PUT /api/Ders/999` isteği atın
- 404 Not Found response'unu kontrol edin

### Ödev Kontrol Listesi

- [ ] `BadRequestException.cs` dosyası oluşturuldu
- [ ] `GlobalExceptionHandlerMiddleware`'e `BadRequestException` case'i eklendi
- [ ] `DersController.PostDers` metodunda `BadRequestException` kullanıldı
- [ ] `DersServis.DersGetirById` metodunda `NotFoundException` fırlatılıyor
- [ ] `DersServis.DersGuncelle` metodunda `NotFoundException` fırlatılıyor
- [ ] Controller'lardaki gereksiz try-catch blokları kaldırıldı
- [ ] Tüm test senaryoları başarıyla çalışıyor

### Ödev Teslimi

Ödevi tamamladıktan sonra:

1. Kodunuzu çalıştırın ve hata olmadığından emin olun
2. Swagger'da tüm test senaryolarını çalıştırın
3. Response'ların doğru HTTP status code ve mesaj içerdiğini kontrol edin
4. Kodunuzu gözden geçirin ve temiz olduğundan emin olun

### Ekstra Zorluk (Opsiyonel)

Eğer ödevi kolayca tamamladıysanız:

- `SinifController` için de aynı exception handling'i uygulayın
- `UnauthorizedException` sınıfı oluşturun ve `AuthController`'da kullanın
- `ErrorResponseDto`'ya `timestamp` (zaman damgası) alanı ekleyin

### Yardım İçin

- `NotFoundException` örneğini referans alabilirsiniz
- `OgrenciServisImpl.cs` ve `OgrenciController.cs` dosyalarındaki implementasyonları inceleyebilirsiniz
- Ders içeriğindeki "9. İleri Seviye" bölümündeki örnekleri kullanabilirsiniz

**Ödev Süresi:** Yaklaşık 30-45 dakika  
**Zorluk Seviyesi:** Başlangıç  
**Puan:** Bu ödev, bir sonraki derse geçmeden önce yapılması önerilir.

---

**Ders Süresi:** Yaklaşık 1.5 saat  
**Zorluk Seviyesi:** Başlangıç  
**Önkoşul:** JWT Authentication dersini tamamlamış olmak

