# HammingCodeSimulasyon

Bu proje, kullanıcının girdiği 4, 8 veya 16 bitlik veriyi Hamming koduna dönüştüren ve kullanıcının belirttiği bir biti bozmasına izin veren bir Windows Forms uygulamasıdır. Uygulama ayrıca bozuk veriyi analiz ederek hangi bitin hatalı olduğunu belirler ve kullanıcıya bildirir.

- Demonstrasyon videosu
- [Video Linki](https://youtu.be/mpij6ox2iUA)

## Özellikler

- Kullanıcıdan 4, 8 veya 16 bitlik ikili veri girişi alır.
- Girilen veriyi Hamming koduna dönüştürür.
- Kullanıcının belirlediği bir biti bozmasına izin verir.
- Bozuk veriyi analiz eder ve hatalı bitin pozisyonunu belirler.

- ## Kurulum
- Bu projeyi klonlayın:
   ```sh
   git clone https://github.com/YusufBalmumcu/HammingCodeSimulasyon.git
- Projeyi Visual Studio ile açıp derleyip çalıştırın ya da Release klasörünün içindeki executable ı kullanın.

## Kullanım
- Uygulamayı başlatın.
- 4, 8 veya 16 bitlik bir binary veri girin ve Veri Ekle butonuna tıklayın.
- Hamming kodu görüntülenecektir.
- Veriyi Boz butonuna tıklayarak bozulacak bitin pozisyonunu girin.
- Bozulan veri görüntülenecektir.
- Sendrom kelimesini ve hatalı bitin pozisyonunu öğrenin.

## Proje Yapısı
- MainForm.cs: Uygulamanın ana formu ve olay işleyicileri.
- Program.cs: Uygulamanın giriş noktası.
- HammingCodeHesapla: Girilen veriyi Hamming koduna dönüştüren fonksiyon.
- CalculateSyndrome: Bozuk veriyi analiz ederek sendrom kelimesini hesaplayan fonksiyon.
- Diğer Form Elemanları: Veri girişi ve kullanıcı etkileşimi için kullanılan form elemanları (TextBox, Button, LinkLabel vb.).
