# MicroYarpDemo

## Proje Amacı
Bu proje, mikroservis mimarisi ile .NET üzerinde YARP (Yet Another Reverse Proxy) kullanarak bir API Gateway ve iki temel servis (CatalogService, OrderService) örneği sunar. Amaç, servisler arası yönlendirme, merkezi API yönetimi ve gelişmiş loglama ile izlenebilirlik sağlamaktır.

## Kullanılan Teknolojiler
- .NET 8
- YARP (Reverse Proxy)
- ASP.NET Core Web API
- Docker & Docker Compose
- Swagger (API dokümantasyonu)
- Serilog & Seq (loglama ve merkezi dashboard)

## Servisler
- **Gateway**: ReverseProxy ile gelen istekleri ilgili servislere yönlendirir ve tüm istek/yanıtları loglar.
- **CatalogService**: Ürün katalog API'si.
- **OrderService**: Sipariş API'si.

## Çalıştırma
1. Tüm servisleri Docker ile başlatmak için:
   ```
   docker-compose up --build
   ```
2. Servis portları:
   - Gateway: `http://localhost:5000`
   - CatalogService: `http://localhost:5001`
   - OrderService: `http://localhost:5002`
   - Seq Dashboard: `http://localhost:5341`
3. Swagger arayüzleri:
   - Gateway: `/swagger`
   - CatalogService: `/swagger`
   - OrderService: `/swagger`

## Loglama ve İzlenebilirlik
- Gateway projesinde Serilog ve Seq ile merkezi loglama aktif.
- Tüm controller istekleri ve yanıtları, `RequestResponseLoggingFilter` ile otomatik olarak loglanır.
- Loglar hem console'a hem de Seq dashboard'a iletilir.
- Log seviyeleri ve sink ayarları `appsettings.Development.json` dosyasında yönetilir.

## ReverseProxy Ayarları
- Gateway'de `appsettings.Development.json` dosyasında ReverseProxy route ve cluster tanımları yapılır.
- Docker ortamında servisler arası erişim için `host.docker.internal:<port>` adresi kullanılır.

## Sık Karşılaşılan Sorunlar ve Çözümleri
- **502 Bad Gateway / Connection Refused**: Gateway container'ı içinden diğer servislere erişmek için `localhost` yerine servis adını veya `host.docker.internal` kullanmalısın.
- **Büyük/Küçük Harf Duyarlılığı**: Linux tabanlı Docker ortamında route ve endpoint'lerde büyük/küçük harf uyumuna dikkat et. Örnek: `/api/catalog` ve `/api/Catalog` farklıdır.
- **Swagger'a Erişim Sorunu**: Container port mapping ve uygulama portu uyumlu olmalı. Ortam değişkeni ile port mapping aynı olmalı.
- **ReverseProxy Route Çakışması**: Gateway'de aynı route'u kullanan bir controller varsa, ReverseProxy devreye girmez. Sadece proxy ile yönlendirme yapmak istiyorsan controller'ı kaldır.
- **Seq Dashboard'da log görünmüyor**: Serilog ayarlarını ve sink tanımlarını kontrol et. `Program.cs`'de sadece `ReadFrom.Configuration` ile Serilog'u başlat.

# Son Yapılan Çalışmalar

## JWT Authentication ve Authorization
- Gateway servisine JWT doğrulama ve token üretimi eklendi.
- Gateway'de `/api/auth/login` endpointi ile kullanıcıya JWT token döndürülüyor.
- CatalogService ve Gateway'de JWT doğrulama için aynı secret key, issuer ve audience ayarları kullanıldı.
- CatalogService'de Swagger arayüzüne JWT ile yetkilendirme desteği eklendi (Authorize butonu ile token girilebilir).
- CatalogController'da gelen JWT token okunup doğrulanıyor, eksik/geçersizse 401 dönüyor.

## Güvenli Servis Erişimi
- Gateway üzerinden alınan JWT token ile CatalogService'e güvenli şekilde erişim sağlanıyor.
- Tüm yetkili endpointler için `[Authorize]` attribute'u kullanıldı.

## Hata ve Sorunlar
- JWT anahtar uzunluğu ve eşleşmesi ile ilgili hatalar giderildi.
- Swagger'da token ile yetkilendirme ve test imkanı sağlandı.
- Docker ve reverse proxy ayarlarında servis adresleri ve portlar kontrol edildi.

---
Yukarıdaki adımlar ile mikroservisler arası güvenli iletişim ve merkezi kimlik doğrulama sağlanmıştır.
