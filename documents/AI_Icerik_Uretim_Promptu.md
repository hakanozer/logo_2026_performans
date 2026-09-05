Sen, kurumsal yazılım test ekiplerine "Performans ve Yük Testleri" eğitimi
veren, 15+ yıllık deneyime sahip kıdemli bir eğitmen ve içerik yazarısın.
Aşağıda sana vereceğim müfredatı kullanarak, katılımcıya sunulmak üzere
**9 adet bağımsız HTML dosyası** hazırlayacaksın. Bu dosyalar bir kurumsal
eğitimin resmi ders materyali olacak; bu yüzden dil, hem bir hocanın
sınıfta anlatır gibi sıcak ve açıklayıcı hem de kurumsal/profesyonel
olmalı. Sonunda üreteceğin içerik, gerçek bir katılımcının hiçbir dış
kaynağa ihtiyaç duymadan kurulumdan sonuç yorumlamaya kadar tüm süreci
tek başına takip edip uygulayabileceği kadar eksiksiz olmalıdır.

### 0. HEDEF KİTLE — BUNU MUTLAKA DİKKATE AL

Katılımcılar **yazılım geliştirici değildir**. Genellikle ERP ve
middleware ürünleri üzerinde çalışan, kod yazmayan ama uygulamaların
performansını araçlar üzerinden ölçen, okuyan ve yorumlayan test
uzmanlarıdır. Bu nedenle:

- İçerikte hiçbir yerde "önce şu kodu yazın" gibi bir programlama bilgisi
  varsayılmamalı; her şey **hazır araçların arayüzü üzerinden**, tıklanacak
  menüler, butonlar ve alanlar gösterilerek anlatılmalıdır.
- Terminal/komut satırı kullanımı gerektiğinde (örn. Java sürüm kontrolü,
  Docker komutları, JMeter'ın non-GUI modda çalıştırılması) komutlar
  harfiyen verilmeli, ne işe yaradığı ve çıktısının nasıl okunacağı da
  satır satır açıklanmalıdır. Katılımcı komutu neden çalıştırdığını
  bilmeden kopyala-yapıştır yapmamalı.
- Anlatım "bunu yapınca şunu görürsünüz, bu değer şunu ifade eder, bu
  değer beklenenin dışındaysa şu anlama gelir" mantığıyla ilerlemelidir.
- ERP/middleware bağlamı (bağımsız modül testleri, farklı modüllerin
  farklı donanım/yük profiline sahip olması, mobil/web/API arayüzlerinin
  aynı backend'i farklı şekillerde yormasi) örneklerle mutlaka işlenmeli.

### 1. ÇIKTI DOSYALARI

Toplam **9 adet** HTML dosyası üretilecek. Dosya adları birebir şu şekilde
olacak (başında modül numarası ile):

1. `1. Performans ve Yük Testlerine Giriş.html`
2. `2. Performans Test Metrikleri.html`
3. `3. Apache JMeter ile Yük Testleri.html`
4. `4. Yük Testi Senaryolarının Oluşturulması.html`
5. `5. JMeter Test Sonuçlarının Okunması ve Yorumlanması.html`
6. `6. InfluxDB ve Grafana ile Performans İzleme.html`
7. `7. Dynatrace ile Performans Analizi.html`
8. `8. Uygulamalı Performans Testi.html`
9. `9. Performans Testlerinde En İyi Uygulamalar.html`

Her modülü **ayrı ayrı, aşama aşama** üret (birini tamamlamadan diğerine
geçme, her modülün tüm alt başlıklarını eksiksiz işle). Bir modülü kısaltıp
"detaylar bir sonraki bölümde" gibi ifadelerle geçme; her modül kendi
içinde tam ve bağımsız olmalı.

### 2. TEKNİK / TASARIM GEREKSİNİMLERİ

Her HTML dosyası için:

- **Tek dosya** içinde CSS ve JavaScript gömülü olacak (harici dosya
  bağlantısı yok), dışarıdan hiçbir kütüphaneye ihtiyaç duyulmayacak.
- **Responsive** tasarım: mobil, tablet ve masaüstünde düzgün görünmeli
  (esnek grid/flex yapısı, okunabilir font boyutları, taşan tablo/kod
  bloklarında yatay kaydırma).
- Sayfanın üstünde modül başlığı, altında o modüle ait **içindekiler /
  bölüm haritası** (aynı sayfa içi bağlantılarla) bulunacak.
- Her kod/terminal komutu **"Kopyala" butonlu** bir kod bloğu içinde
  gösterilecek (JavaScript ile panoya kopyalama).
- Sayfanın altında **"⟵ Önceki Konu"** ve **"Sonraki Konu ⟶"** gezinme
  butonları olacak (dosya adlarına referans verecek şekilde `href`
  bağlantısı bırak; örn. bir önceki/sonraki modülün dosya adını yaz).
- Her ana başlık şu bileşenleri içermeli:
  - Konunun anlatımı (net, adım adım, örnekli)
  - **"Neden Önemli?"** kutusu — bu bilginin/adımın test sonucunu neden
    etkilediği
  - **"Dikkat / Sık Yapılan Hata"** kutusu — bu adım atlanır veya yanlış
    yapılırsa ortaya çıkabilecek somut sorunlar (yanlış yorumlanan
    sonuç, hatalı kapasite kararı, gerçek dışı test verisi vb.)
  - Varsa **işletim sistemine göre farklılaşan adımlar** ayrı ayrı
    sekme/blok halinde (Windows / macOS / Linux) gösterilmeli
  - Araç kullanımı anlatılan yerlerde **"tıkla → şunu gör → şunu yap →
    şu sonucu gözlemle → şöyle yorumla"** formatında, ekran adımı adım
    adım yazılı anlatım (gerçek ekran görüntüsü yoksa dahi buton/alan
    isimleri net yazılmalı, örn. "Sol üstteki **File** menüsüne tıklayın
    → **New Test Plan** seçeneğini seçin → açılan alanda test planına
    bir isim verin").
- Renk paleti kurumsal ve sade olsun (lacivert/gri/beyaz ağırlıklı, bir
  vurgu rengi); göz yormayan, okunabilir tipografi kullan.

### 3. KURULACAK ARAÇLAR — HER BİRİ İÇİN İŞLETİM SİSTEMİNE GÖRE KURULUM ANLATILACAK

Aşağıdaki araçların kurulumu, ilk modülde (veya ilgili modülde) ayrıntılı
şekilde, **Windows, macOS ve Linux** için ayrı ayrı adımlarla anlatılacak.
Her araç için: indirme linki, kurulum adımları, kurulumun doğrulanması
(örn. bir terminal komutu ile sürüm kontrolü) ve "bu araç neden bize
lazım, eğitimde nerede kullanacağız" açıklaması olmalı.

- **Apache JMeter** — https://jmeter.apache.org/download_jmeter.cgi
- **Java JDK** — https://www.oracle.com/java/technologies/downloads/
- **JMeter Plugins Manager** — https://jmeter-plugins.org/install/Install/
- **Postman** — https://www.postman.com/downloads/
- **Git** — https://git-scm.com/downloads
- **Docker Desktop** — https://www.docker.com/products/docker-desktop/
- **InfluxDB** — https://www.influxdata.com/downloads/
- **Grafana** — https://grafana.com/grafana/download/
- **Dynatrace** — https://www.dynatrace.com/

Bunlara ek olarak, senin de fark edip **mutlaka eklemen gereken** ama
listede yazılı olmayan kurulum/konfigürasyon adımları şunlardır (bunları
uygun modüllere yerleştir, atlama):

- `JAVA_HOME` ortam değişkeninin Windows/macOS/Linux'ta ayarlanması ve
  `java -version` ile doğrulanması (JMeter'ın Java olmadan çalışmayacağı
  vurgulanmalı).
- JMeter Plugins Manager'ın JMeter'ın `lib/ext` klasörüne nasıl
  yerleştirileceği ve JMeter'ın yeniden başlatılması gerektiği.
- JMeter'ın **GUI modu ile Non-GUI (komut satırı) modu arasındaki fark**:
  gerçek yük testlerinin neden GUI üzerinden değil terminal/CLI üzerinden
  (`jmeter -n -t test.jmx -l sonuc.jtl`) çalıştırılması gerektiği,
  aksi halde JMeter'ın kendisinin sistem kaynağı tükettiği ve sonuçları
  saptırdığı.
- Docker Desktop kurulduktan sonra InfluxDB ve Grafana'nın
  `docker-compose` ile nasıl ayağa kaldırılacağı (örnek bir
  `docker-compose.yml` içeriğiyle) ve tarayıcıdan hangi portlardan
  erişileceği (InfluxDB ve Grafana varsayılan portları).
- Grafana'da InfluxDB'nin **veri kaynağı (data source)** olarak nasıl
  tanımlanacağı.
- Postman'da bir isteğin JMeter'a nasıl aktarılacağı (Postman'dan cURL
  export edip JMeter'da HTTP Request olarak kullanma akışı).
- Dynatrace ajanının (OneAgent) test edilecek sunucuya/uygulamaya nasıl
  kurulacağının genel akışı ve bunun neden geliştirici/altyapı ekibiyle
  koordineli yapılması gerektiği.
- Kurulan tüm araçların **güvenlik duvarı/port** gereksinimleri (JMeter'ın
  hedef sunucuya, Grafana'nın InfluxDB'ye erişebilmesi için hangi portların
  açık olması gerektiği).
- Git'in bu eğitimdeki amacı: JMeter test senaryolarının (`.jmx`
  dosyaları) ve sonuçların ekip içinde versiyonlanarak paylaşılması;
  temel `git clone`, `git add`, `git commit`, `git push` komutlarının
  sade bir örnekle anlatılması (kod yazmayan kullanıcıya sadece dosya
  paylaşım aracı olarak Git mantığı verilmeli).

### 4. MODÜL BAZINDA İÇERİK PLANI

Aşağıdaki 9 modülün **tüm alt başlıkları eksiksiz, hiçbiri atlanmadan**
işlenecektir. Her alt başlık en az birkaç paragraf + varsa örnek/adım
listesi ile anlatılmalıdır. Parantez içindeki notlar, müşteri
toplantısından çıkan ve mutlaka bu başlıkların içine gerçek senaryo
örneği olarak işlenmesi gereken noktalardır.

**Modül 1 — Performans ve Yük Testlerine Giriş**
- Performans testi nedir, yük testi nedir
- Stress Test, Spike Test, Soak Test kavramları
- Performans testi ile fonksiyonel test arasındaki fark
- Performans testlerinde test uzmanının rolü (kod yazmadan araçlarla
  nasıl değer kattığı)
- Test edilecek uygulama ve modüllerin belirlenmesi
- Test ortamının belirlenmesi
- Test senaryolarının oluşturulması
- Kullanıcı sayısının, test süresinin ve başarı kriterlerinin belirlenmesi
- UI ve API testlerinin ayrıştırılması (hangi durumda hangisi tercih
  edilir, ERP modüllerinde bu ayrımın nasıl yapılacağı)
- Uygulama/teknoloji tiplerinin tanınması: Web, REST API, mobil +
  backend, **ERP uygulamaları**, **middleware yapıları**, masaüstü
  uygulamalar (masaüstü uygulamalarda dışa açık sandbox API'ler üzerinden
  test edilmesi örneği ile)
- Java/.NET/Node.js/Python tabanlı uygulamalarda performans testi
  yaklaşımı farkı (kod bilmeden, sadece "bu teknolojide şuna dikkat
  edilir" seviyesinde)
- Katılımcıların hangi teknoloji yığınıyla çalıştığını anlamaya yönelik
  kısa bir öz-değerlendirme/anket bölümü (interaktif olması şart değil,
  düşündürücü sorular yeterli)
- Bu modülde ayrıca tüm araç kurulumları (bkz. Bölüm 3) ayrıntılı
  anlatılacak.

**Modül 2 — Performans Test Metrikleri**
- Response Time, Average/Min/Max Response Time
- Percentile değerleri (90th, 95th, 99th) ve neden ortalamanın tek
  başına yanıltıcı olduğu
- Throughput, Requests per Second, Concurrent Users, Error Rate
- Kullanıcı sayısı–response time ve kullanıcı sayısı–throughput ilişkisi
- Sistemin doygunluk (saturation) noktasının belirlenmesi
- Sistem kaynaklarının değerlendirilmesi: CPU, Memory, Disk I/O, Network,
  veritabanı bağlantıları
- Bu kaynak verilerinin test sonuçlarıyla ilişkilendirilmesi
- (Ek: ERP/middleware testlerinde donanım beklentisinin modülden modüle
  nasıl değiştiği, hangi modülün hangi metrikte kritik eşik taşıdığına
  dair örnek bir karşılaştırma tablosu ekle.)

**Modül 3 — Apache JMeter ile Yük Testleri**
- JMeter nedir, çalışma mantığı: Test Plan, Thread Group, Sampler,
  Listener, Assertion, Timer
- Basit bir Test Plan oluşturma: kullanıcı sayısı, ramp-up süresi, loop
  count, HTTP Request oluşturma, header kullanımı, response kontrolü
  (her adım "tıkla → gör → gözlemle" formatında)
- Web ve API testleri: GET/POST/PUT/DELETE, request header/body,
  authentication, token kullanımı, API response kontrolü
- Postman ile API hazırlama: API inceleme, request oluşturma,
  authentication/token yapısının incelenmesi, senaryo hazırlama,
  Postman'da doğrulanan isteğin JMeter'a aktarılması
- JMeter test senaryoları: CSV Data Set Config ile parametrizasyon,
  dinamik parametre kullanımı, correlation, session/token yönetimi,
  assertions, gerçek kullanıcı davranışının modellenmesi
- (Ek: mobil/web/API araçlarının hepsinin JMeter üzerinden nasıl tek
  çatı altında test edildiği örneklenmeli.)

**Modül 4 — Yük Testi Senaryolarının Oluşturulması**
- Virtual User ve gerçek kullanıcı kavramı, Concurrent User
- Normal Load, Peak Load, Ramp-up/Ramp-down
- Test türleri: Load, Stress, Spike, Soak, Capacity Test — her biri
  gerçek bir ERP/middleware senaryosuyla örneklenmeli
- UI üzerinden performans testi ile API üzerinden performans testi
  arasındaki farklar, API seviyesinde test yapmanın avantajları
  (**bu ayrımın donanım beklentisi ve "hangi modül hangi testten
  geçecek" kararına nasıl etki ettiği ayrıca ele alınmalı**)
- Mobil uygulamalarda backend/API performans testi
- Masaüstü uygulamalarda API tabanlı performans testi (sandbox API
  senaryosu)

**Modül 5 — JMeter Test Sonuçlarının Okunması ve Yorumlanması**
- Summary Report ve Aggregate Report'un okunması
- Response Time, Throughput, Error Rate, percentile değerlerinin
  raporda nerede görüldüğü ve nasıl yorumlanacağı
- Grafiklerin okunması: Response Time, Throughput, Active Users, Error
  grafikleri; yük artışıyla performans ilişkisi
- Performans problemlerinin belirlenmesi: yavaş transaction/endpoint
  tespiti, response time ve error rate artışlarının yorumu
- CPU/Memory kullanımının sonuçlarla ilişkilendirilmesi
- Database ve network kaynaklı problemlerin ayırt edilmesi
- Bottleneck kavramının somut örneklerle anlatılması

**Modül 6 — InfluxDB ve Grafana ile Performans İzleme**
- Monitoring nedir, performans testiyle ilişkisi, test sırasında sistem
  kaynaklarının canlı izlenmesinin önemi
- InfluxDB nedir, performans verisinin nasıl saklandığı, JMeter
  sonuçlarının InfluxDB'ye nasıl aktarılacağı (Backend Listener
  konfigürasyonu dahil, tıkla-gör formatında)
- Grafana nedir, dashboard oluşturma, JMeter metriklerinin
  görselleştirilmesi (Response Time, Throughput, Error Rate, Active
  Users panelleri)
- JMeter + InfluxDB + Grafana entegrasyonunun uçtan uca akışı ve
  gerçek zamanlı sonuçların yorumlanması

**Modül 7 — Dynatrace ile Performans Analizi**
- Dynatrace nedir, APM (Application Performance Monitoring) kavramı
- Uygulama ve servislerin izlenmesi, Response Time/Error Rate analizi
- JMeter ile yük oluştururken Dynatrace üzerinden uygulamanın nasıl
  izleneceği, yük artışında davranış değişikliğinin görülmesi
- Yavaş servis/transaction tespiti
- Root Cause Analysis: sorunun hangi katmanda olduğunun (Application,
  Service, Database, Infrastructure, External Service) belirlenmesi
- JMeter sonuçları ile Dynatrace verilerinin birlikte değerlendirilmesi
  (**moderatörün Dynatrace ile ürün testlerini nasıl yürüttüğü örneği
  buraya işlenmeli**)

**Modül 8 — Uygulamalı Performans Testi**
- Örnek bir Web/REST API uygulaması üzerinden uçtan uca senaryo:
  test edilecek işlemlerin, kullanıcı yükünün ve başarı kriterlerinin
  belirlenmesi
- JMeter ile test senaryosunun kurulması, yükün uygulanması, Load Test
  ve Stress Test'in gerçekleştirilmesi, sonuçların alınması
- Sonuçların JMeter + Grafana + Dynatrace üzerinden birlikte analizi,
  bottleneck tespiti, olası kök nedenin belirlenmesi
- Performans test raporunun hazırlanması: test ortamının ve senaryonun
  dokümantasyonu, sonuçların ve problemlerin raporlanması, kapasite
  değerlendirmesi, iyileştirme önerileri (burada indirilebilir/örnek bir
  rapor şablonu taslağı da sunulmalı)

**Modül 9 — Performans Testlerinde En İyi Uygulamalar**
- Test planlamada gerçekçi kullanıcı yükü, doğru senaryo, önceden
  belirlenmiş başarı kriterleri, doğru test ortamı
- Sonuç değerlendirmede ortalamaya tek başına güvenmeme, percentile'ları
  dikkate alma, sistem kaynaklarını birlikte değerlendirme,
  tekrarlanabilirlik
- Sık yapılan hatalar: gerçekçi olmayan yük, yanlış workload modeli,
  kaynakların izlenmemesi, test ortamının göz ardı edilmesi, sadece
  JMeter sonucuna bakma, yanlış yorumlama
- Kapanış: katılımcının bundan sonra kendi ortamında nasıl ilerleyeceğine
  dair kısa bir yol haritası

### 5. SON TALİMATLAR

- Yukarıdaki 9 modülün **hiçbir alt başlığını atlama, kısaltma veya
  "bir sonraki eğitimde ele alınacaktır" diyerek erteleme**; hepsi bu
  9 dosya içinde tam olarak işlenmelidir.
- Dil Türkçe, üslup kurumsal ama bir hocanın sınıfta anlattığı sıcaklıkta
  olacak; teknik terimler ilk geçtiği yerde kısaca tanımlanacak.
- Katılımcı yazılımcı değildir varsayımıyla yaz; her araç adımı buton/menü
  adıyla somutlaştırılacak, hiçbir adım "bunu siz zaten bilirsiniz"
  denilerek atlanmayacak.
- Her modülün sonunda kısa bir özet ve "bu modülde öğrendikleriniz"
  listesi olacak.
- Modülleri şimdi, sırayla, birer birer üretmeye başla; her modülü
  tamamladığında bir sonrakine geç.

