# Öğrenci Servis API - Eğitim Yol Haritası

## 📚 İŞLENEN KONULAR (Tamamlanan Dersler)

### ✅ 1. Proje Yapısı ve Temel Kurulum
- [x] .NET 8.0 Web API projesi oluşturma
- [x] Solution ve proje yapısı (N-tier Architecture)
- [x] Katmanlı mimari: API, Logic, DataAccess, Models
- [x] Proje referansları ve bağımlılıklar

### ✅ 2. Veritabanı Entegrasyonu
- [x] Entity Framework Core kurulumu
- [x] PostgreSQL veritabanı bağlantısı (Npgsql)
- [x] DbContext yapılandırması (OkulContext)
- [x] Connection String yapılandırması (appsettings.json)
- [x] PostgreSQL timestamp davranışı ayarları

### ✅ 3. Entity Framework Core - Model Tanımlamaları
- [x] Entity sınıfları oluşturma (Ogrenci, Ogretmen, Ders, Sinif, Sinav)
- [x] Data Annotations kullanımı
  - [Key] attribute
  - [Column] attribute ile column mapping
- [x] OnModelCreating ile model yapılandırması
- [x] Primary Key tanımlamaları
- [x] Table ve schema mapping

### ✅ 4. DTO (Data Transfer Object) Pattern
- [x] DTO sınıfları oluşturma (OgrenciDto, OgretmenDto)
- [x] Entity'den DTO'ya dönüşüm mantığı
- [x] DTO kullanım amaçları (veri transferi, güvenlik, performans)

### ✅ 5. Service Layer ve Interface Pattern
- [x] Interface tanımlamaları (IOgrenci, IOgretmen)
- [x] Service implementasyonları (OgrenciServisImpl, OgretmenServis)
- [x] Dependency Injection ile servis kayıtları
- [x] Service lifetime (Scoped)

### ✅ 6. LINQ ve Veritabanı Sorguları
- [x] LINQ Query Syntax kullanımı
- [x] Join işlemleri (Inner Join)
- [x] Left Join (GroupJoin + DefaultIfEmpty)
- [x] FirstOrDefault() kullanımı
- [x] ToList() ile materialization

### ✅ 7. RESTful API Controller'lar
- [x] ControllerBase sınıfından türetme
- [x] Route attribute kullanımı ([Route("api/[controller]")]
- [x] HTTP Verb attributes ([HttpGet], [HttpPost], [HttpPut], [HttpDelete])
- [x] CRUD operasyonları (Create, Read, Update, Delete)
- [x] ActionResult ve ActionResult<T> kullanımı
- [x] [FromBody] attribute

### ✅ 8. HTTP Status Codes ve Response Handling
- [x] Ok (200) - Başarılı işlem
- [x] CreatedAtAction (201) - Yeni kayıt oluşturma
- [x] NotFound (404) - Kayıt bulunamadı
- [x] BadRequest (400) - Geçersiz istek
- [x] NoContent (204) - Başarılı silme işlemi

### ✅ 9. Model Validation
- [x] ModelState.IsValid kontrolü
- [x] Validation hata mesajları

### ✅ 10. Dependency Injection
- [x] Constructor Injection pattern
- [x] Service registration (AddScoped)
- [x] DbContext injection

### ✅ 11. Swagger/OpenAPI
- [x] Swagger entegrasyonu
- [x] SwaggerUI yapılandırması
- [x] API dokümantasyonu

### ✅ 12. JWT Authentication ve Authorization
- [x] JWT (JSON Web Token) nedir? Basit açıklama
- [x] Authentication (Kimlik Doğrulama) nedir?
- [x] Authorization (Yetkilendirme) nedir? Basit fark
- [x] JWT paketlerinin kurulumu (Microsoft.AspNetCore.Authentication.JwtBearer)
- [x] Basit User/Login model oluşturma
- [x] Authentication Controller (Login endpoint) - Adım adım
- [x] JWT Token oluşturma servisi - Basit implementasyon
- [x] appsettings.json'da JWT ayarları (Secret Key)
- [x] Program.cs'de JWT yapılandırması - Kopyala-yapıştır örnek
- [x] [Authorize] attribute ile endpoint koruma - Basit kullanım

---

## 🎯 DEVAM EDİLECEK KONULAR (Sırayla İşlenecek Dersler)

### ✅ DERS 1: JWT Authentication ve Authorization
**Öncelik: YÜKSEK** ⭐ (Zorunlu)
**Seviye:** Başlangıç
**Durum:** Tamamlandı

#### Alt Konular:
- [x] JWT (JSON Web Token) nedir? Basit açıklama
- [x] Authentication (Kimlik Doğrulama) nedir?
- [x] Authorization (Yetkilendirme) nedir? Basit fark
- [x] JWT paketlerinin kurulumu (Microsoft.AspNetCore.Authentication.JwtBearer)
- [x] Basit User/Login model oluşturma
- [x] Authentication Controller (Login endpoint) - Adım adım
- [x] JWT Token oluşturma servisi - Basit implementasyon
- [x] appsettings.json'da JWT ayarları (Secret Key)
- [x] Program.cs'de JWT yapılandırması - Kopyala-yapıştır örnek
- [x] [Authorize] attribute ile endpoint koruma - Basit kullanım

**Pratik Uygulama:**
- [x] Login endpoint'i oluşturma (adım adım)
- [x] Bir endpoint'i [Authorize] ile koruma
- [x] Swagger'da token ile test etme

---

### 📍 DERS 2: Exception Handling ve Hata Yönetimi
**Öncelik: YÜKSEK**
**Seviye:** Başlangıç

#### Alt Konular:
- [X] Try-Catch blokları nedir? Basit örnekler
- [X] Exception (Hata) nedir? Ne zaman oluşur?
- [X] Basit Custom Exception sınıfı oluşturma (NotFoundException)
- [X] Global Exception Handler Middleware - Basit implementasyon
- [X] Hata mesajlarını kullanıcıya gösterme
- [X] Basit hata response formatı

**Pratik Uygulama:**
- Bir NotFoundException sınıfı oluşturma
- Basit global exception handler
- Hata durumlarında standart mesaj döndürme

---

### 📍 DERS 3: Logging (Günlük Kayıtları)
**Öncelik: ORTA**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] Logging nedir? Neden önemlidir?
- [ ] ILogger nedir? Nasıl kullanılır?
- [ ] Log seviyeleri (Information, Warning, Error) - Basit açıklama
- [ ] ILogger'ı servise inject etme
- [ ] Basit log mesajları yazma

**Pratik Uygulama:**
- Service sınıfına ILogger ekleme
- Başarılı işlemlerde Information log
- Hata durumlarında Error log
- Console'da log çıktılarını görme

---

### 📍 DERS 4: Validation (Doğrulama)
**Öncelik: ORTA**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] Validation nedir? Neden gerekli?
- [ ] Data Annotations ile basit validation ([Required], [MaxLength])
- [ ] FluentValidation kütüphanesi - Basit kurulum
- [ ] Basit validation kuralları (Adi boş olamaz, Soyadi boş olamaz)
- [ ] Validation hata mesajlarını gösterme

**Pratik Uygulama:**
- FluentValidation paketi kurulumu
- Ogrenci için basit validator (Adi, Soyadi zorunlu)
- Validation hatalarını test etme

---

### 📍 DERS 5: AutoMapper (Otomatik Dönüşüm)
**Öncelik: ORTA**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] AutoMapper nedir? Neden kullanılır? (Basit örnek)
- [ ] Manuel mapping vs AutoMapper karşılaştırması
- [ ] AutoMapper paketi kurulumu
- [ ] Basit Mapping Profile oluşturma
- [ ] Entity'den DTO'ya otomatik dönüşüm
- [ ] Service'te AutoMapper kullanımı

**Pratik Uygulama:**
- AutoMapper kurulumu
- Ogrenci → OgrenciDto mapping profile'ı
- Mevcut manuel mapping kodunu AutoMapper ile değiştirme

---

### 📍 DERS 6: Repository Pattern (Depo Deseni)
**Öncelik: ORTA**
**Seviye:** Orta-Başlangıç

#### Alt Konular:
- [ ] Repository Pattern nedir? Basit açıklama
- [ ] Neden Repository Pattern kullanılır?
- [ ] Basit Repository interface oluşturma
- [ ] Repository implementasyonu
- [ ] Service'te Repository kullanımı
- [ ] Generic Repository kavramı (basit örnek)

**Pratik Uygulama:**
- IOgrenciRepository interface oluşturma
- OgrenciRepository implementasyonu
- Service'te Repository kullanımına geçiş

---

### 📍 DERS 7: Sayfalama (Pagination) ve Arama
**Öncelik: ORTA**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] Pagination (Sayfalama) nedir? Neden gerekli?
- [ ] Basit pagination mantığı (sayfa numarası, sayfa boyutu)
- [ ] Query parameters (page, pageSize) alma
- [ ] Basit PagedResult sınıfı oluşturma
- [ ] LINQ ile sayfalama (Skip, Take)
- [ ] Basit arama (isim ile filtreleme)

**Pratik Uygulama:**
- Pagination DTO'ları (PagedRequest, PagedResponse)
- GetOgrenciler endpoint'ine sayfalama ekleme
- İsim ile arama özelliği ekleme

---

### 📍 DERS 8: Caching (Önbellekleme)
**Öncelik: DÜŞÜK**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] Caching nedir? Basit örnek (sık kullanılan verileri hafızada tutma)
- [ ] Neden cache kullanılır? (Performans artışı)
- [ ] Memory Cache nedir?
- [ ] IMemoryCache kullanımı - Basit örnek
- [ ] Cache'e veri ekleme ve okuma
- [ ] Cache süresi ayarlama

**Pratik Uygulama:**
- Memory cache kurulumu
- Listeleme endpoint'inde cache kullanımı
- Cache'in çalıştığını test etme

---

### 📍 DERS 9: Unit Testing (Birim Testleri)
**Öncelik: YÜKSEK**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] Unit Testing nedir? Basit açıklama
- [ ] Neden test yazılır? (Hataları erken bulma)
- [ ] xUnit test framework - Basit kurulum
- [ ] Test projesi oluşturma
- [ ] Basit test yazma (Bir fonksiyonun doğru çalıştığını test etme)
- [ ] Test çalıştırma ve sonuçları görme

**Pratik Uygulama:**
- Test projesi oluşturma
- OgrenciServisImpl için basit bir test (OgrenciEkle testi)
- Test sonuçlarını görme

---

### 📍 DERS 10: File Upload/Download (Dosya Yükleme/İndirme)
**Öncelik: ORTA**
**Seviye:** Başlangıç

#### Alt Konular:
- [ ] File upload nedir? Basit açıklama
- [ ] IFormFile nedir? Nasıl kullanılır?
- [ ] Basit file upload endpoint'i oluşturma
- [ ] Dosya boyutu kontrolü (basit)
- [ ] Dosya kaydetme (wwwroot klasörüne)
- [ ] Basit file download endpoint'i

**Pratik Uygulama:**
- Öğrenci fotoğrafı upload endpoint'i
- Fotoğraf indirme endpoint'i
- Swagger'da dosya yükleme testi

---

### 📍 DERS 11: SignalR (Gerçek Zamanlı İletişim)
**Öncelik: DÜŞÜK**
**Seviye:** Orta (Opsiyonel)

#### Alt Konular:
- [ ] SignalR nedir? Basit açıklama
- [ ] Hub nedir? Basit örnek
- [ ] Basit SignalR kurulumu

**Pratik Uygulama:**
- Basit bir Hub oluşturma
- Basit real-time mesajlaşma örneği

---

### 📍 DERS 12: Docker (Konteynerleştirme)
**Öncelik: ORTA**
**Seviye:** Orta

#### Alt Konular:
- [ ] Docker nedir? Basit açıklama
- [ ] Dockerfile nedir?
- [ ] Basit Dockerfile oluşturma
- [ ] Docker image oluşturma
- [ ] Container çalıştırma

**Pratik Uygulama:**
- API için basit Dockerfile
- Docker image build etme
- Container'ı çalıştırma

---

## 📊 ÖNCELİK SIRASI (Önerilen İşleme Sırası)

### Faz 1: Temel Güvenlik ve Stabilite (Hemen - İlk 2 Hafta)
1. ✅ **DERS 1: JWT Authentication** ⭐ (Zorunlu) - **TAMAMLANDI**
2. ⏳ **DERS 2: Exception Handling** - **SIRADAKİ KONU**

### Faz 2: Kod Kalitesi ve Temel Özellikler (2-4 Hafta)
3. ✅ **DERS 3: Logging**
4. ✅ **DERS 4: Validation (FluentValidation)**
5. ✅ **DERS 9: Unit Testing**

### Faz 3: Mimari İyileştirmeler (4-6 Hafta)
6. ✅ **DERS 5: AutoMapper**
7. ✅ **DERS 6: Repository Pattern**
8. ✅ **DERS 7: Pagination ve Arama**

### Faz 4: Ek Özellikler (6-8 Hafta)
9. ✅ **DERS 8: Caching**
10. ✅ **DERS 10: File Upload/Download**

### Faz 5: Opsiyonel/İleri Konular (8+ Hafta)
11. ✅ **DERS 11: SignalR** (Opsiyonel)
12. ✅ **DERS 12: Docker** (Opsiyonel)

---

## 🎓 ÖĞRENME ÇIKTILARI

Bu eğitim setini tamamladıktan sonra öğrenciler:

- ✅ Modern .NET 8.0 Web API geliştirme temelleri
- ✅ N-tier architecture kavramı
- ✅ Entity Framework Core ile veritabanı işlemleri
- ✅ JWT Authentication ve Authorization (temel seviye)
- ✅ RESTful API tasarımı
- ✅ Dependency Injection kavramı ve kullanımı
- ✅ Unit testing temelleri
- ✅ Error handling ve logging
- ✅ API documentation ve Swagger
- ✅ Temel seviye production-ready API geliştirme

---

## 📝 NOTLAR

- Her ders için yaklaşık 1-2 saat süre ayrılması önerilir
- Pratik uygulamalar her dersin sonunda mutlaka yapılmalıdır
- Her ders sonunda kod review yapılması önerilir
- JWT Authentication mutlaka ilk sırada işlenmelidir
- Yeni başlayanlar için konular basitleştirilmiştir
- Karmaşık konular ileri seviye eğitime bırakılmıştır
- Öğrenciler adım adım ilerlemelidir, acele edilmemelidir

