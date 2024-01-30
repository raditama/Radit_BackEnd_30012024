# README: Menjalankan Aplikasi .NET C#

Selamat datang di aplikasi .NET C#! Dalam panduan ini, saya akan memberikan instruksi langkah demi langkah untuk menjalankan aplikasi ini.

## Persyaratan Prasyarat
1. **.NET SDK:** Pastikan Anda telah menginstal [.NET SDK](https://dotnet.microsoft.com/download).

## Langkah-langkah Menjalankan Aplikasi

1. **Unduh Kode Sumber**
   - Gunakan perintah `git clone` untuk mengunduh kode sumber aplikasi:
     ```bash
     git clone https://github.com/raditama/Radit_BackEnd_30012024
     ```
   - Atau unduh dan ekstrak ZIP dari repositori jika tidak menggunakan Git.

2. **Pindah ke Direktori Proyek**
   - Buka terminal atau command prompt, dan pindah ke direktori proyek aplikasi:
     ```bash
     cd path/to/your/project
     ```

3. **Bangun Aplikasi**
   - Gunakan perintah berikut untuk membangun aplikasi:
     ```bash
     dotnet build
     ```

4. **Jalankan Aplikasi**
   - Setelah berhasil membangun, jalankan aplikasi dengan perintah:
     ```bash
     dotnet watch run
     ```
     or
     ```bash
     dotnet run
     ```
   - Ini akan menjalankan aplikasi Anda di lingkungan lokal.

5. **Akses Aplikasi**
   - Buka browser dan kunjungi `http://localhost:5000` untuk melihat aplikasi Anda.
