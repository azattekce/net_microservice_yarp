# MicroYarpDemo

## Proje Amacı
Bu proje, mikroservis mimarisi ile .NET üzerinde YARP (Yet Another Reverse Proxy) kullanarak bir API Gateway ve iki temel servis (CatalogService, OrderService) örneği sunar. Amaç, servisler arası yönlendirme ve merkezi API yönetimini göstermek ve Docker ile container ortamında çalıştırılabilir bir demo sağlamaktır.

## Kullanılan Teknolojiler
- .NET 8
- YARP (Reverse Proxy)
- ASP.NET Core Web API
- Docker & Docker Compose
- Swagger (API dokümantasyonu)

## Servisler
- **Gateway**: ReverseProxy ile gelen istekleri ilgili servislere yönlendirir.
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
3. Swagger arayüzleri:
   - Gateway: `/swagger`
   - CatalogService: `/swagger`
   - OrderService: `/swagger`

## ReverseProxy Ayarları
- Gateway'de `appsettings.Development.json` dosyasında ReverseProxy route ve cluster tanımları yapılır.
- Docker ortamında servisler arası erişim için `localhost` yerine servis adı (`catalog-api`, `order-api`) ve container portu (`80`) kullanılır.
- Host makinedeki servislere erişmek için `host.docker.internal:<port>` adresi kullanılabilir.

## Sık Karşılaşılan Sorunlar ve Çözümleri
- **502 Bad Gateway / Connection Refused**: Gateway container'ı içinden diğer servislere erişmek için `localhost` yerine servis adını kullanmalısın. Örnek:
  ```json
  "Address": "http://catalog-api:80"
  ```
- **Büyük/Küçük Harf Duyarlılığı**: Linux tabanlı Docker ortamında route ve endpoint'lerde büyük/küçük harf uyumuna dikkat et. Örnek: `/api/catalog` ve `/api/Catalog` farklıdır.
- **Swagger'a Erişim Sorunu**: Container port mapping ve uygulama portu uyumlu olmalı. Ortam değişkeni ile port mapping aynı olmalı.
- **ReverseProxy Route Çakışması**: Gateway'de aynı route'u kullanan bir controller varsa, ReverseProxy devreye girmez. Sadece proxy ile yönlendirme yapmak istiyorsan controller'ı kaldır.

## Ek Notlar
- Proje, mikroservis mimarisi ve YARP ile API Gateway uygulamalarını öğrenmek ve test etmek için hazırlanmıştır.
- Docker Compose ile kolayca tüm servisleri ayağa kaldırabilir, ReverseProxy yönlendirmelerini test edebilirsin.
- Sorun yaşarsan logları ve ayarları kontrol et, yukarıdaki notları uygula.
