# 🎓 Öğrenci Servis API

Modern ve ölçeklenebilir bir okul yönetim sistemi için geliştirilmiş RESTful Web API projesi. .NET 8.0 ve PostgreSQL kullanarak öğrenci ve öğretmen yönetimi için temel endpoint'ler sunmaktadır.

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknolojiler](#-teknolojiler)
- [Proje Yapısı](#-proje-yapısı)
- [Kurulum](#-kurulum)
- [Yapılandırma](#-yapılandırma)
- [API Endpoint'leri](#-api-endpointleri)
- [Veritabanı Yapısı](#-veritabanı-yapısı)
- [Mimari](#-mimari)

## ✨ Özellikler

- ✅ **Clean Architecture** yaklaşımıyla modüler yapı
- ✅ **RESTful API** tasarımı
- ✅ **Dependency Injection** ile gevşek bağlılık
- ✅ **DTO Pattern** ile veri transferi
- ✅ **Entity Framework Core** ile ORM
- ✅ **PostgreSQL** veritabanı desteği
- ✅ **Swagger/OpenAPI** dokümantasyonu
- ✅ **Repository Pattern** ile veri erişim katmanı

## 🛠️ Teknolojiler

- **.NET 8.0** - Framework
- **ASP.NET Core Web API** - Web framework
- **Entity Framework Core** - ORM
- **PostgreSQL** - Veritabanı
- **Swagger/OpenAPI** - API dokümantasyonu
- **C#** - Programlama dili

## 📁 Proje Yapısı

```
OgrenciServis/
│
├── OgrenciServis.Api/           # API katmanı (Controllers, Program.cs)
│   ├── Controllers/
│   │   ├── OgrenciController.cs
│   │   └── OgretmenController.cs
│   └── Program.cs
│
├── OgrenciServis.Logic/          # İş mantığı katmanı
│   ├── Interface/
│   │   ├── IOgrenci.cs
│   │   └── IOgretmen.cs
│   └── Services/
│       ├── OgrenciServisImpl.cs
│       └── OgretmenServis.cs
│
├── OgrenciServis.DataAccess/     # Veri erişim katmanı
│   └── OkulContext.cs
│
└── OgrenciServis.Models/          # Model katmanı
    ├── Ogrenci.cs
    ├── Ogretmen.cs
    ├── Ders.cs
    ├── Sinav.cs
    ├── Sinif.cs
    └── DTO/
        ├── OgrenciDto.cs
        └── OgretmenDto.cs
```

## 🚀 Kurulum

### Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (v12 veya üzeri)
- [Visual Studio](https://visualstudio.microsoft.com/) veya [VS Code](https://code.visualstudio.com/)

### Adımlar

1. **Repository'yi klonlayın**
   ```bash
   git clone <repository-url>
   cd OgrenciServis
   ```

2. **Veritabanını oluşturun**
   ```sql
   CREATE DATABASE TestDataBase;
   ```

3. **Veritabanı tablolarını oluşturun**
   ```sql
   -- ogrenciler tablosu
   CREATE TABLE public.ogrenciler (
       ogrenci_id SERIAL PRIMARY KEY,
       adi VARCHAR(255),
       soyadi VARCHAR(255),
       dogum_tarihi DATE,
       sinif_id INTEGER
   );

   -- ogretmenler tablosu
   CREATE TABLE public.ogretmenler (
       ogretmen_id SERIAL PRIMARY KEY,
       ogretmen_adi VARCHAR(255),
       ogretmen_soyadi VARCHAR(255),
       brans VARCHAR(255),
       sinif INTEGER
   );

   -- siniflar tablosu
   CREATE TABLE public.siniflar (
       sinif_id SERIAL PRIMARY KEY,
       sube VARCHAR(50),
       sinif INTEGER
   );

   -- dersler tablosu
   CREATE TABLE public.dersler (
       ders_id SERIAL PRIMARY KEY,
       ders_adi VARCHAR(255),
       ders_suresi INTEGER
   );

   -- sinavlar tablosu
   CREATE TABLE public.sinavlar (
       sinav_id SERIAL PRIMARY KEY,
       ders_id INTEGER,
       ogrenci_id INTEGER,
       ogretmen_id INTEGER,
       not INTEGER
   );
   ```

4. **Projeyi çalıştırın**
   ```bash
   cd OgrenciServis.Api
   dotnet restore
   dotnet run
   ```

5. **Swagger UI'ya erişin**
   ```
   https://localhost:5001/swagger
   ```

## ⚙️ Yapılandırma

`appsettings.json` dosyasında veritabanı bağlantı string'ini güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TestDataBase;Username=kullanici_adi;Password=sifre"
  }
}
```

## 📡 API Endpoint'leri

### Öğrenci İşlemleri

#### Tüm Öğrencileri Listele
```
GET /api/Ogrenci
```

**Response:**
```json
[
  {
    "ogrenciId": 1,
    "adi": "Ahmet",
    "soyadi": "Yılmaz",
    "dogumTarihi": "2010-05-15T00:00:00",
    "sube": "A",
    "sinifNo": 5
  }
]
```

### Öğretmen İşlemleri

#### Tüm Öğretmenleri Listele
```
GET /api/Ogretmen
```

**Response:**
```json
[
  {
    "ogretmenId": 1,
    "adi": "Mehmet",
    "soyadi": "Demir",
    "brans": "Matematik",
    "sube": "A",
    "sinifNo": 5
  }
]
```

## 🗄️ Veritabanı Yapısı

### Tablolar

- **ogrenciler** - Öğrenci bilgileri
- **ogretmenler** - Öğretmen bilgileri
- **siniflar** - Sınıf bilgileri
- **dersler** - Ders bilgileri
- **sinavlar** - Sınav ve not bilgileri

### İlişkiler

- Öğrenci ↔ Sınıf (Many-to-One)
- Öğretmen ↔ Sınıf (Many-to-One)
- Sınav ↔ Öğrenci (Many-to-One)
- Sınav ↔ Öğretmen (Many-to-One)
- Sınav ↔ Ders (Many-to-One)

## 🏗️ Mimari

Bu proje **Clean Architecture** prensiplerine uygun olarak geliştirilmiştir:

1. **API Katmanı (OgrenciServis.Api)**
   - HTTP isteklerini yönetir
   - Controller'lar ile endpoint'leri tanımlar
   - Swagger konfigürasyonu

2. **İş Mantığı Katmanı (OgrenciServis.Logic)**
   - Business logic'i içerir
   - Interface'ler ile soyutlama sağlar
   - Service sınıfları ile iş kurallarını uygular

3. **Veri Erişim Katmanı (OgrenciServis.DataAccess)**
   - Entity Framework Core context
   - Veritabanı işlemleri
   - DbSet tanımlamaları

4. **Model Katmanı (OgrenciServis.Models)**
   - Entity sınıfları
   - DTO sınıfları
   - Veri transfer nesneleri

### Tasarım Desenleri

- **Repository Pattern** - Veri erişim soyutlaması
- **DTO Pattern** - Veri transfer nesneleri
- **Dependency Injection** - Bağımlılık yönetimi
- **Interface Segregation** - Arayüz ayrımı

## 📝 Notlar

- Development ortamında Swagger otomatik olarak aktifleştirilir
- PostgreSQL tarih formatı uyumluluğu için `Npgsql.EnableLegacyTimestampBehavior` ayarı kullanılmaktadır
- Tüm veritabanı tabloları `public` schema'sında bulunmaktadır

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---

**Geliştirici:** [Ertuğrul Kara]  
**Tarih:** 2025  
**Versiyon:** 1.0.0
