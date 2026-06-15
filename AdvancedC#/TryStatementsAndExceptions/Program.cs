string[] arguments = args.Length > 0 ? args : [];

try
{
    ProcessUserArgument(arguments); 
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("[HANDLED] Error: Harap masukkan argumen pada aplikasi.");
}
catch (ArgumentException ex) when (ex.Message.Contains("Format"))
{
    Console.WriteLine($"[HANDLED] Error Validasi Format: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[GLOBAL FILTER] Terjadi error tidak terduga: {ex.Message}");
}

Console.WriteLine("\nDEMO 2\n");

try
{
    ExecutionPipeline("invalid_config_data");
}
catch (BusinessValidationException ex)
{
    Console.WriteLine($"[TOP LEVEL CATCH] Menangkap Business Exception: {ex.Message}");
    Console.WriteLine($"-> (InnerException): {ex.InnerException?.GetType().Name}");
    Console.WriteLine($"-> Stack Trace:\n{ex.StackTrace}");
}

Console.WriteLine("\n=== Aplikasi Selesai Berjalan Tanpa Crash ===");


static void ProcessUserArgument(string[] methodArgs)
{
    // Mengakses indeks pertama secara langsung untuk memicu IndexOutOfRangeException jika array kosong
    string firstArg = methodArgs[0];
    Console.WriteLine($"Argumen ditemukan: {firstArg}");
}

static void ExecutionPipeline(string inputData)
{
    // Memastikan objek StringReader otomatis dibebaskan saat keluar dari cakupan metode
    using var fakeMemoryStream = new StringReader(inputData);

    try
    {
        // Melempar exception menggunakan throw expression di dalam operator ternary
        string validatedData = inputData == "invalid_config_data"
            ? throw new FormatException("Struktur data konfigurasi rusak.")
            : inputData;

        Console.WriteLine($"Memproses data: {validatedData}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine("[LOG SYSTEM] Mengamankan log ke teks lokal...");
        
        // Membungkus exception asli ke dalam exception baru untuk mempertahankan InnerException
        throw new BusinessValidationException("Gagal menjalankan pipeline eksekusi sistem.", ex);
    }
    finally
    {
        // Blok ini dijamin tetap dieksekusi setelah terjadi exception sebelum dialirkan ke pemanggil
        Console.WriteLine("[FINALLY PIPELINE] Membersihkan temporary state pipeline.");
    }
}

public class BusinessValidationException : Exception
{
    public BusinessValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}