# Case: İçerik Yönetim Sistemi (CMS) Geliştirme  

**Proje Açıklaması:**  
Bu proje, verilen case çalışması kapsamında yalnızca backend kısmı geliştirilerek hazırlanmıştır. Projenin amacı, temel gereksinimleri karşılayan bir API uygulaması sunmaktır. **.NET Core** ile geliştirilmiş, veritabanı olarak **PostgreSQL** kullanılmış ve cache mekanizması için **Redis** entegre edilmiştir. Uygulama Docker Compose ile kolayca ayağa kaldırılabilir.

---

## Kullanılan Teknolojiler  

- **Backend Framework:** .NET Core 8  
- **ORM:** Entity Framework Core  
- **Veritabanı:** PostgreSQL  
- **Cache:** Redis  
- **Container Orkestrasyonu:** Docker Compose  

---

## Kurulum ve Çalıştırma  

Projeyi Docker Compose kullanarak çalıştırmak için aşağıdaki adımları izleyiniz:

### Gereksinimler  

- **Docker ve Docker Compose**  
  - Docker Desktop veya Docker Engine kurulu olmalıdır.  
  - Docker Compose, Docker Desktop ile birlikte gelir.  
    Eğer manuel kurulum yaptıysanız, Compose’un yüklü olduğundan emin olunuz.

### Adımlar  

1. Depoyu klonlayınız:  
   ```bash
   git clone https://github.com/EmreSeverr/Cms.git
   cd Cms
2. **Docker Compose ile uygulamayı başlatınız:**  
   ```bash
   docker-compose up --build
3. **Uygulamayı aşağıdaki şekilde erişilebilir hale getiriniz:**  
   - **API Uygulaması:** `http://localhost:5000/swagger/index.html`  
   - **PostgreSQL Veritabanı:** `localhost:5432`  
   - **Redis:** `localhost:6379`


## Notlar

- **Authentication ve Authorization:**  
  Proje kapsamı ve süre kısıtlamaları nedeniyle, kimlik doğrulama ve yetkilendirme mekanizmaları uygulanmamıştır.  

- **Endpointlerde Prop Kontrolleri:**  
  Gelen verilerin doğruluğunu kontrol eden mekanizmalar (ör. model validation) göz ardı edilmiştir. Normalde, bu işlemler `FluentValidation` veya **Data Annotations** ile yapılırdı.  

- **Dil Bağımlı Hata Mesajları:**  
  Kullanıcıya döndürülen hata mesajlarının dil bağımlı olması bu projede göz ardı edilmiştir. Normalde dil bilgisi `Accept-Language` HTTP header'ından alınırdı, ancak case gereği `id` üzerinden alınmıştır.

- **Swagger:**  
  Production ortamında test amaçlı Swagger API dokümantasyonu açık bırakılmıştır. Güvenlik nedenleriyle, bu özellik gerçek bir production ortamında devre dışı bırakılmalıdır.
