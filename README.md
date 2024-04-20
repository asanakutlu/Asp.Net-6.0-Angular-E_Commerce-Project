1-) Onion Architecture Mimari kullanıldı 
Domain katmanı merkezi katmandır.Entites, Value Object, Enumeration,Exception(Entity ile ilgili exceptionlar) bu katmanda tasarlandı.
Repository&Service Interfaces/Core Katmanı
Bu katman, Domain katmanı ile uygulamanın iş/business/service katmanı arasında bir soyutlama katmanıdır. Repository olsun, service olsun tüm arayüzler burada tanımlanır. Amaç veri erişiminde Gevşek Bağlı(Loose Coupling) bir yaklaşım sergilemektir. Domain katmanını referans eder.
Persistence Katmanı
DbContext, migration, Configurations, Seedin, Repository Concrete class ve veritabanı konfigürasyon işlemleri bu katmanda gerçekleştirilir. Ayrıca Application katmanındaki interface’ler burada implemente edilir. En dış katman olduğu için bu katmana herhangi bir katman bağımlılık göstermeyecektir.
Infrastructure Katmanı
Esasında Persistence katmanı bu katmanla bütünleşik olarak kullanılmaktadır.Sadece persistence den farklı olarak veritabanının dışındaki operasyonları/servisleri/işlemleri. yürüttüğümüz katmandır Genellikle sisteme eklenecek dış/external yapılanmalar bu katmanda dahil edilir. Haliyle bu katmanda diğer en dış katman olduğu için herhangi bir katman tarafından bağlılık olmamalıdır.
Presentation Katmanı
Kullanıcının uygulama ile iletişime geçtiği katmandır. WEP APP, WEP API, MVC

2-) Veritabanı olarak Postgresql veritanı kullanıldı.

3-) Repository Design Pattern kullanıldı veritabanı sorumluluğunu üstlenen sınıfı tasarlarken bir standart üzerine oturtmayı hedefleyen ve Entity Framework gibi ORM(Object Relational Mapping) araçlarıyla kombine edilerek sorgusal anlamda az sayıda operatif metotla yüksek seviyede veri erişim imkanı sağlayan bir strateji üzerine kurulu tasarım desenidir.

4-) Frontent tarafnı Angular ile geliştirildi. Admin ve Ui katmanları geliştirildi.

5-) Multiple layout kullanıldı. Single Page Application yapısını aşarak birden fazla şablon üzerinde Multiple Page Application şeklinde nasıl çalışma yapıldı.

6-) Angular Material ve Boostrap kütüphanelei vs kullanıldı.

7-)CORS Politikaları API’ın hangi client’tan istek alıp almayacağını belirlemesi CORS politikaları ayarlama diye nitelendirilebilir. CORS politikaları bir güvenlik önleminden ziyade browserlardaki same-origin policy önlemini hafifletmek için devreye giren yapılardır.

8-) Fluent validation ve ValidationFilter kütüphaneleri kullanıldı.

9-)Table Per Hierarchy : Bu davranışta ise hiyerarşi içindeki tüm entity’ler için tek bir tablo oluşturulmakta ve bu tabloda veriler tutulurken ortak olan yani base class’dan gelen alanlar doldurulmakta, olmayan alanlar için nullable şartı gelmektedir. Yani bir başka deyişle, veritabanındaki bir tablo EF Core tarafında birden fazla entity’nin bütünsel haline karşılık gelmektedir diyebiliriz.

10-) Dosya yüklemerinde Local Stroge ve Azure blob stroge  yapısı kullanıldı. Diğer Cloud servislerini destekliyecek alt yapısı hazırlandı.

11-)CQRS Pattern ve MediatR kütüphanesi kullanıldı.

12-) Identity kütüphanesi kullanıldı.

13-)Authorize ve Authentication kullanıldı.

14-)Guard Yapılanması İle Yetkisiz Erişim Engellemesi

15-)IdentityCheck İle Link Kontrolü

16-)Login işlemlerinde Google, Facebook ve Normal Login işlemleri yapııldı.

17-)Http Interceptor İle Global Http Error Handler

18-)JWT Expiration Ayarı Refresh Token Konfigürasyonu sağlandı

19-)Serilog İle Loglama ve Seq İle Görselleştirme yapıldı

20-)Global Exception Handler Oluşturmalar yapıldı

21-)SignalR kütüphanesi kullanıldı ve tüm clientlara bildirim alınması sağlandı

22-)Mail Servisi kullanıldı

23-)Rol tabanlı yetkilendirmeler verildi. Role-Based-Access-Control(RBAC) mimarisi 

24-) qrCode kullanıldı.

