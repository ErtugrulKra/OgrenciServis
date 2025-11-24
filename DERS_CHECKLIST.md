# 📋 Öğrenci Servis API - Ders İlerleme Checklist

Bu dosya, eğitim sürecindeki ilerlemeyi takip etmek için kullanılır. Her ders tamamlandığında ilgili checkbox'ı işaretleyin.

---

## ✅ TAMAMLANAN DERSLER

### 1. Proje Yapısı ve Temel Kurulum
- [X] .NET 8.0 Web API projesi oluşturuldu
- [X] Solution yapısı anlaşıldı
- [ ] N-tier architecture kavramı öğrenildi
- [ ] Proje referansları yapılandırıldı

### 2. Veritabanı Entegrasyonu
- [ ] Entity Framework Core kuruldu
- [ ] PostgreSQL bağlantısı yapılandırıldı
- [ ] DbContext sınıfı oluşturuldu
- [ ] Connection string ayarlandı

### 3. Entity Framework Core - Model Tanımlamaları
- [ ] Entity sınıfları oluşturuldu (Ogrenci, Ogretmen, Ders, Sinif, Sinav)
- [ ] Data Annotations kullanıldı
- [ ] OnModelCreating yapılandırıldı
- [ ] Primary Key'ler tanımlandı

### 4. DTO (Data Transfer Object) Pattern
- [ ] DTO sınıfları oluşturuldu
- [ ] Entity'den DTO'ya dönüşüm yapıldı
- [ ] DTO kullanım amaçları anlaşıldı

### 5. Service Layer ve Interface Pattern
- [ ] Interface'ler tanımlandı (IOgrenci, IOgretmen)
- [ ] Service implementasyonları yapıldı
- [ ] Dependency Injection kayıtları yapıldı

### 6. LINQ ve Veritabanı Sorguları
- [ ] LINQ Query Syntax öğrenildi
- [ ] Join işlemleri yapıldı
- [ ] Left Join kullanıldı

### 7. RESTful API Controller'lar
- [ ] Controller'lar oluşturuldu
- [ ] CRUD operasyonları implement edildi
- [ ] HTTP verb attributes kullanıldı

### 8. HTTP Status Codes ve Response Handling
- [ ] Status code'lar öğrenildi
- [ ] Doğru status code'lar kullanıldı
- [ ] Response handling yapıldı

### 9. Model Validation
- [ ] ModelState.IsValid kullanıldı
- [ ] Validation hata mesajları gösterildi

### 10. Dependency Injection
- [ ] Constructor Injection öğrenildi
- [ ] Service registration yapıldı
- [ ] DI kavramı anlaşıldı

### 11. Swagger/OpenAPI
- [ ] Swagger kuruldu
- [ ] API dokümantasyonu görüntülendi
- [ ] SwaggerUI kullanıldı

---

## 🎯 DEVAM EDİLECEK DERSLER

### 📍 DERS 1: JWT Authentication ve Authorization ⭐ ZORUNLU
**Tahmini Süre:** 2-3 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] JWT nedir? Basit açıklama (10 dk)
- [ ] Authentication (Kimlik Doğrulama) nedir? (10 dk)
- [ ] Authorization (Yetkilendirme) nedir? (10 dk)
- [ ] Token-based authentication - Basit örnek (10 dk)

#### Pratik Uygulama
- [ ] JWT paketlerinin kurulumu (Microsoft.AspNetCore.Authentication.JwtBearer)
- [ ] Basit User/Login model oluşturma
- [ ] Authentication Controller oluşturma (adım adım)
- [ ] Basit JWT Token oluşturma servisi
- [ ] appsettings.json'da JWT ayarları (Secret Key)
- [ ] Program.cs'de JWT yapılandırması (kopyala-yapıştır örnek)
- [ ] [Authorize] attribute ile bir endpoint koruma
- [ ] Login endpoint testi (Swagger)
- [ ] Token ile korumalı endpoint testi

#### Kontrol Listesi
- [ ] Login endpoint çalışıyor mu?
- [ ] Token oluşturuluyor mu?
- [ ] Token ile korumalı endpoint'e erişilebiliyor mu?
- [ ] Token olmadan erişim engelleniyor mu?

**Notlar:**
- JWT Secret Key appsettings.json'da saklanıyor (basit başlangıç için)
- Token expiration süresi ayarlanmalı

---

### 📍 DERS 2: Exception Handling ve Hata Yönetimi
**Tahmini Süre:** 1.5 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Exception (Hata) nedir? Ne zaman oluşur? (10 dk)
- [ ] Try-Catch blokları nedir? Basit örnek (10 dk)
- [ ] Custom exception nedir? (10 dk)

#### Pratik Uygulama
- [ ] Basit NotFoundException sınıfı oluşturma
- [ ] Basit Global Exception Handler Middleware oluşturma
- [ ] Basit hata response formatı
- [ ] Test: Hatalı isteklerde mesaj gösterilmesi

#### Kontrol Listesi
- [ ] NotFoundException oluşturuldu mu?
- [ ] Global exception handler çalışıyor mu?
- [ ] Hata durumlarında mesaj gösteriliyor mu?

---

### 📍 DERS 3: Logging (Günlük Kayıtları)
**Tahmini Süre:** 1 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Logging nedir? Neden önemlidir? (10 dk)
- [ ] ILogger nedir? (5 dk)
- [ ] Log seviyeleri (Information, Warning, Error) - Basit açıklama (10 dk)

#### Pratik Uygulama
- [ ] Service sınıfına ILogger ekleme
- [ ] Başarılı işlemlerde Information log yazma
- [ ] Hata durumlarında Error log yazma
- [ ] Console'da log çıktılarını görme

#### Kontrol Listesi
- [ ] ILogger inject edildi mi?
- [ ] Loglar yazılıyor mu?
- [ ] Console'da log çıktıları görünüyor mu?

---

### 📍 DERS 4: Validation (Doğrulama)
**Tahmini Süre:** 1.5 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Validation nedir? Neden gerekli? (10 dk)
- [ ] Data Annotations ile basit validation ([Required], [MaxLength]) (10 dk)
- [ ] FluentValidation nedir? (10 dk)

#### Pratik Uygulama
- [ ] FluentValidation paketi kurulumu
- [ ] Ogrenci için basit Validator (Adi, Soyadi zorunlu)
- [ ] Validation hata mesajlarını test etme

#### Kontrol Listesi
- [ ] FluentValidation kuruldu mu?
- [ ] Validator oluşturuldu mu?
- [ ] Validation çalışıyor mu?
- [ ] Hata mesajları gösteriliyor mu?

---

### 📍 DERS 5: AutoMapper (Otomatik Dönüşüm)
**Tahmini Süre:** 1 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] AutoMapper nedir? Neden kullanılır? Basit örnek (10 dk)
- [ ] Manuel mapping vs AutoMapper karşılaştırması (10 dk)

#### Pratik Uygulama
- [ ] AutoMapper paketi kurulumu
- [ ] Basit Mapping Profile oluşturma (Ogrenci → OgrenciDto)
- [ ] Service'te AutoMapper kullanımı
- [ ] Mevcut manuel mapping kodunu değiştirme

#### Kontrol Listesi
- [ ] AutoMapper kuruldu mu?
- [ ] Profile oluşturuldu mu?
- [ ] Mapping çalışıyor mu?
- [ ] Manuel mapping kodu temizlendi mi?

---

### 📍 DERS 6: Repository Pattern (Depo Deseni)
**Tahmini Süre:** 2 saat
**Seviye:** Orta-Başlangıç

#### Teorik Kısım
- [ ] Repository Pattern nedir? Basit açıklama (15 dk)
- [ ] Neden Repository Pattern kullanılır? (10 dk)
- [ ] Generic Repository kavramı (basit örnek) (10 dk)

#### Pratik Uygulama
- [ ] IOgrenciRepository interface oluşturma
- [ ] OgrenciRepository implementasyonu
- [ ] Service'te Repository kullanımına geçiş

#### Kontrol Listesi
- [ ] Repository interface oluşturuldu mu?
- [ ] Repository implement edildi mi?
- [ ] Service'te Repository kullanılıyor mu?

---

### 📍 DERS 7: Sayfalama (Pagination) ve Arama
**Tahmini Süre:** 1.5 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Pagination (Sayfalama) nedir? Neden gerekli? (10 dk)
- [ ] Basit pagination mantığı (sayfa numarası, sayfa boyutu) (10 dk)

#### Pratik Uygulama
- [ ] Basit PagedResult sınıfı oluşturma
- [ ] Pagination DTO'ları (PagedRequest, PagedResponse)
- [ ] GetOgrenciler endpoint'ine sayfalama ekleme
- [ ] İsim ile basit arama özelliği

#### Kontrol Listesi
- [ ] Pagination çalışıyor mu?
- [ ] Arama çalışıyor mu?
- [ ] Query parameters doğru alınıyor mu?

---

### 📍 DERS 8: Caching (Önbellekleme)
**Tahmini Süre:** 1 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Caching nedir? Basit örnek (10 dk)
- [ ] Neden cache kullanılır? (Performans) (5 dk)

#### Pratik Uygulama
- [ ] IMemoryCache injection
- [ ] Memory cache kurulumu
- [ ] Listeleme endpoint'inde cache kullanımı
- [ ] Cache'in çalıştığını test etme

#### Kontrol Listesi
- [ ] Cache kuruldu mu?
- [ ] Cache çalışıyor mu?
- [ ] Performans artışı görülüyor mu?

---

### 📍 DERS 9: Unit Testing (Birim Testleri)
**Tahmini Süre:** 2 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] Unit Testing nedir? Basit açıklama (10 dk)
- [ ] Neden test yazılır? (Hataları erken bulma) (10 dk)
- [ ] xUnit test framework (10 dk)

#### Pratik Uygulama
- [ ] Test projesi oluşturma
- [ ] xUnit kurulumu
- [ ] OgrenciServisImpl için basit bir test (OgrenciEkle testi)
- [ ] Test çalıştırma ve sonuçları görme

#### Kontrol Listesi
- [ ] Test projesi oluşturuldu mu?
- [ ] Basit test yazıldı mı?
- [ ] Test geçiyor mu?

---

### 📍 DERS 10: File Upload/Download (Dosya Yükleme/İndirme)
**Tahmini Süre:** 1.5 saat
**Seviye:** Başlangıç

#### Teorik Kısım
- [ ] File upload nedir? Basit açıklama (10 dk)
- [ ] IFormFile nedir? (5 dk)

#### Pratik Uygulama
- [ ] Basit file upload endpoint'i oluşturma
- [ ] Dosya boyutu kontrolü (basit)
- [ ] Dosya kaydetme (wwwroot klasörüne)
- [ ] Basit file download endpoint'i
- [ ] Swagger'da dosya yükleme testi

#### Kontrol Listesi
- [ ] Upload endpoint çalışıyor mu?
- [ ] Download endpoint çalışıyor mu?
- [ ] Dosya kaydediliyor mu?

---

### 📍 DERS 11: SignalR (Gerçek Zamanlı İletişim)
**Tahmini Süre:** 1.5 saat
**Seviye:** Orta (Opsiyonel)

#### Teorik Kısım
- [ ] SignalR nedir? Basit açıklama (10 dk)
- [ ] Hub nedir? (5 dk)

#### Pratik Uygulama
- [ ] SignalR kurulumu
- [ ] Basit bir Hub oluşturma
- [ ] Basit real-time mesajlaşma örneği

#### Kontrol Listesi
- [ ] SignalR kuruldu mu?
- [ ] Hub çalışıyor mu?

---

### 📍 DERS 12: Docker (Konteynerleştirme)
**Tahmini Süre:** 1.5 saat
**Seviye:** Orta

#### Teorik Kısım
- [ ] Docker nedir? Basit açıklama (10 dk)
- [ ] Dockerfile nedir? (5 dk)

#### Pratik Uygulama
- [ ] Basit Dockerfile oluşturma
- [ ] Docker image build etme
- [ ] Container çalıştırma

#### Kontrol Listesi
- [ ] Dockerfile oluşturuldu mu?
- [ ] Container çalışıyor mu?



