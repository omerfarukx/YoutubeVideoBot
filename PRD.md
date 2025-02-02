# Hikaye Canlandırma Platformu PRD (Product Requirements Document)

## 1. Proje Özeti
Kullanıcıların yazdığı hikayeleri yapay zeka ile görselleştiren, seslendiren ve video haline getiren web tabanlı bir platform.

## 2. Hedef Kitle
- Hikaye yazarları
- İçerik üreticileri
- Eğitimciler
- Çocuk kitabı yazarları
- Sosyal medya içerik üreticileri

## 3. Problem Tanımı
- İnsanların hikayelerini görselleştirmek için profesyonel çizerlere ihtiyaç duyması
- Görsel içerik üretiminin zaman alıcı ve maliyetli olması
- Seslendirme için ayrı ekiplere ihtiyaç duyulması

## 4. Teknik Altyapı

### Backend
- ASP.NET Core 6.0 MVC
- Clean Architecture
  - Core Layer (Entities, Interfaces)
  - Application Layer (DTOs, Services)
  - Infrastructure Layer (DB, External Services)
  - Presentation Layer (MVC)

### Frontend
- Bootstrap 5
- jQuery
- AJAX

### Yapay Zeka Servisleri
- Görsel Üretimi: DALL-E veya Stable Diffusion API
- Metin-Ses Dönüşümü: Azure Text-to-Speech
- Video Oluşturma: FFmpeg

### Database
- SQL Server (MSSQL)
- Entity Framework Core

### Veri Modeli
#### Ana Tablolar
- Users (dbo.Users)
  - Id (int)
  - Username (nvarchar(100))
  - Email (nvarchar(256))
  - CreatedAt (datetime2)
  - UpdatedAt (datetime2)

- Stories (dbo.Stories)
  - Id (int)
  - UserId (int, FK)
  - Title (nvarchar(200))
  - Content (nvarchar(max))
  - Status (nvarchar(50))
  - CreatedAt (datetime2)
  - UpdatedAt (datetime2)

- StoryImages (dbo.StoryImages)
  - Id (int)
  - StoryId (int, FK)
  - ImageUrl (nvarchar(1000))
  - Order (int)
  - CreatedAt (datetime2)

- StoryAudios (dbo.StoryAudios)
  - Id (int)
  - StoryId (int, FK)
  - AudioUrl (nvarchar(1000))
  - VoiceCharacter (nvarchar(100))
  - CreatedAt (datetime2)

- StoryVideos (dbo.StoryVideos)
  - Id (int)
  - StoryId (int, FK)
  - VideoUrl (nvarchar(1000))
  - Format (nvarchar(50))
  - CreatedAt (datetime2)

## 5. Temel Özellikler (MVP)

### Kullanıcı Yönetimi
- Kayıt/Giriş sistemi (ASP.NET Core Identity)
- Profil yönetimi
- Hikaye geçmişi

### Hikaye Modülü
- Hikaye yazma editörü
- Hikaye kaydetme
- Hikaye düzenleme
- Durum takibi (draft, processing, completed)

### AI Görsel Üretimi
- Hikayeden anahtar kelimeleri çıkarma
- Yapay zeka ile görsellerin oluşturulması
- Görsel düzenleme seçenekleri
- Sıralama ve organizasyon

### Seslendirme
- Farklı ses karakterleri seçimi
- Metin-ses dönüşümü
- Ses hızı/tonu ayarlama
- Durum takibi

### Video Oluşturma
- Görselleri sıralama
- Sesi görsellerle senkronize etme
- Video export seçenekleri
- İşlem durumu takibi

## 6. Sprint Planı

### Sprint 1 (2 Hafta)
- MSSQL veritabanı kurulumu ve veri modeli oluşturma
- Clean Architecture implementasyonu
- Temel auth sistemi
- Hikaye yazma modülü

### Sprint 2 (2 Hafta)
- AI görsel üretim entegrasyonu
- Görsel yönetim sistemi
- Temel UI/UX
- Durum yönetimi

### Sprint 3 (2 Hafta)
- Seslendirme sistemi entegrasyonu
- Ses yönetimi
- Video oluşturma altyapısı
- Asenkron işlem yönetimi

### Sprint 4 (1 Hafta)
- Video export sistemi
- Test ve optimizasyon
- Deployment
- Kullanıcı geri bildirimleri

## 7. Riskler ve Çözümler
- AI servis maliyetleri → Kullanım limitleri ve ücretli üyelik sistemi
- Video işleme süresi → Async işlem ve kuyruk sistemi
- Depolama maliyetleri → Azure Blob Storage ve otomatik temizleme
- Servis kesintileri → Fallback mekanizmaları

## 8. Ölçüm Metrikleri
- Kullanıcı kayıt/aktiflik oranı
- Hikaye üretim sayısı
- Video oluşturma başarı oranı
- Sistem performans metrikleri
- Servis maliyetleri

## 9. Gelecek Özellikler
- Hikaye şablonları
- Sosyal medya entegrasyonu
- Toplu hikaye işleme
- Özel AI model eğitimi
- Premium üyelik sistemi

## 10. Güvenlik ve Uyumluluk
- SQL Server Row-Level Security (RLS)
- GDPR uyumluluğu
- Veri yedekleme stratejisi
- Rate limiting