using MiniLibrary.Data;
using MiniLibrary.Models;
using MiniLibrary.Services;

using LibraryDbContext context = new LibraryDbContext();
LibraryService service = new LibraryService(context);

bool isRunning = true;

while (isRunning)
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("===== MINI LIBRARY =====");
    Console.WriteLine("1. Tambah author");
    Console.WriteLine("2. Tambah buku");
    Console.WriteLine("3. Edit buku");
    Console.WriteLine("4. Hapus buku");
    Console.WriteLine("5. Tambah member");
    Console.WriteLine("6. Lihat daftar buku");
    Console.WriteLine("7. Pinjam buku");
    Console.WriteLine("8. Kembalikan buku");
    Console.WriteLine("9. Lihat pinjaman aktif");
    Console.WriteLine("0. Keluar");
    Console.Write("Pilih menu: ");

    string choice = Console.ReadLine() ?? string.Empty;
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddAuthorMenu();
            break;
        case "2":
            AddBookMenu();
            break;
        case "3":
            UpdateBookMenu();
            break;
        case "4":
            DeleteBookMenu();
            break;
        case "5":
            AddMemberMenu();
            break;
        case "6":
            ShowBooks();
            break;
        case "7":
            BorrowBookMenu();
            break;
        case "8":
            ReturnBookMenu();
            break;
        case "9":
            ShowActiveLoans();
            break;
        case "0":
            isRunning = false;
            continue;
        default:
            Console.WriteLine("Pilihan tidak valid.");
            break;
    }

    Pause();
}

void Pause()
{
    Console.WriteLine();
    Console.Write("Tekan Enter untuk kembali ke menu...");
    Console.ReadLine();
}

void AddAuthorMenu()
{
    Console.Write("Nama author: ");
    string name = Console.ReadLine() ?? string.Empty;

    Author newAuthor = service.AddAuthor(name);
    Console.WriteLine($"Author '{newAuthor.Name}' ditambahkan dengan ID {newAuthor.Id}.");
}

void AddBookMenu()
{
    Console.WriteLine("--- Tambah Buku ---");

    List<Author> authors = service.GetAllAuthors();

    if (authors.Count == 0)
    {
        Console.WriteLine("Belum ada author. Tambahkan author dulu (menu 1).");
        return;
    }

    Console.Write("Judul buku: ");
    string title = Console.ReadLine() ?? string.Empty;

    Console.Write("Jumlah stok: ");
    string stockInput = Console.ReadLine() ?? string.Empty;
    bool isStockValid = int.TryParse(stockInput, out int stock);

    if (!isStockValid)
    {
        Console.WriteLine("Stok harus berupa angka. Dibatalkan.");
        return;
    }

    Console.WriteLine("Daftar author:");
    foreach (Author author in authors)
    {
        Console.WriteLine($"  [{author.Id}] {author.Name}");
    }

    Console.Write("Masukkan ID author (pisahkan koma jika lebih dari satu): ");
    string authorInput = Console.ReadLine() ?? string.Empty;

    string[] rawIds = authorInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    List<int> authorIds = new List<int>();

    foreach (string rawId in rawIds)
    {
        bool isIdValid = int.TryParse(rawId, out int authorId);
        if (isIdValid)
        {
            authorIds.Add(authorId);
        }
    }

    if (authorIds.Count == 0)
    {
        Console.WriteLine("Tidak ada ID author yang valid. Dibatalkan.");
        return;
    }

    Book newBook = service.AddBook(title, stock, authorIds);
    Console.WriteLine($"Buku '{newBook.Title}' ditambahkan dengan ID {newBook.Id}.");
}

void UpdateBookMenu()
{
    Console.WriteLine("--- Edit Buku ---");
    ShowBooks();

    Console.Write("ID buku yang diedit: ");
    string bookIdInput = Console.ReadLine() ?? string.Empty;
    bool isBookIdValid = int.TryParse(bookIdInput, out int bookId);

    if (!isBookIdValid)
    {
        Console.WriteLine("ID buku harus berupa angka. Dibatalkan.");
        return;
    }

    Book? existingBook = service.GetBookById(bookId);
    if (existingBook is null)
    {
        Console.WriteLine("Buku tidak ditemukan.");
        return;
    }

    Console.WriteLine($"Judul saat ini: {existingBook.Title}");
    Console.Write("Judul baru: ");
    string title = Console.ReadLine() ?? string.Empty;

    Console.WriteLine($"Stok saat ini: {existingBook.Stock}");
    Console.Write("Stok baru: ");
    string stockInput = Console.ReadLine() ?? string.Empty;
    bool isStockValid = int.TryParse(stockInput, out int stock);

    if (!isStockValid)
    {
        Console.WriteLine("Stok harus berupa angka. Dibatalkan.");
        return;
    }

    List<Author> authors = service.GetAllAuthors();
    Console.WriteLine("Daftar author:");
    foreach (Author author in authors)
    {
        Console.WriteLine($"  [{author.Id}] {author.Name}");
    }

    string currentAuthorNames = string.Join(", ", existingBook.Authors.Select(a => a.Name));
    Console.WriteLine($"Author saat ini: {currentAuthorNames}");
    Console.Write("ID author baru (pisahkan koma, akan menggantikan yang lama): ");
    string authorInput = Console.ReadLine() ?? string.Empty;

    string[] rawIds = authorInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    List<int> authorIds = new List<int>();

    foreach (string rawId in rawIds)
    {
        bool isIdValid = int.TryParse(rawId, out int authorId);
        if (isIdValid)
        {
            authorIds.Add(authorId);
        }
    }

    if (authorIds.Count == 0)
    {
        Console.WriteLine("Tidak ada ID author yang valid. Dibatalkan.");
        return;
    }

    string result = service.UpdateBook(bookId, title, stock, authorIds);
    Console.WriteLine(result);
}

void DeleteBookMenu()
{
    Console.WriteLine("--- Hapus Buku ---");
    ShowBooks();

    Console.Write("ID buku yang dihapus: ");
    string bookIdInput = Console.ReadLine() ?? string.Empty;
    bool isBookIdValid = int.TryParse(bookIdInput, out int bookId);

    if (!isBookIdValid)
    {
        Console.WriteLine("ID buku harus berupa angka. Dibatalkan.");
        return;
    }

    Console.Write("Yakin ingin menghapus buku ini? (y/n): ");
    string confirmation = Console.ReadLine() ?? string.Empty;

    if (!confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Dibatalkan.");
        return;
    }

    string result = service.DeleteBook(bookId);
    Console.WriteLine(result);
}

void AddMemberMenu()
{
    Console.WriteLine("--- Tambah Member ---");
    Console.Write("Nama member: ");
    string name = Console.ReadLine() ?? string.Empty;

    Member newMember = service.AddMember(name);
    Console.WriteLine($"Member '{newMember.Name}' ditambahkan dengan ID {newMember.Id}.");
}

void ShowBooks()
{
    Console.WriteLine("--- Daftar Buku ---");

    List<Book> books = service.GetAllBooks();

    if (books.Count == 0)
    {
        Console.WriteLine("(belum ada buku)");
        return;
    }

    foreach (Book book in books)
    {
        string authorNames = string.Join(", ", book.Authors.Select(author => author.Name));
        Console.WriteLine($"[{book.Id}] {book.Title} — {authorNames} (stok: {book.Stock})");
    }
}

void BorrowBookMenu()
{
    Console.WriteLine("--- Pinjam Buku ---");
    ShowBooks();

    Console.Write("ID buku yang dipinjam: ");
    string bookIdInput = Console.ReadLine() ?? string.Empty;
    bool isBookIdValid = int.TryParse(bookIdInput, out int bookId);

    if (!isBookIdValid)
    {
        Console.WriteLine("ID buku harus berupa angka. Dibatalkan.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("--- Daftar Member ---");
    List<Member> members = service.GetAllMembers();
    foreach (Member member in members)
    {
        Console.WriteLine($"[{member.Id}] {member.Name}");
    }

    Console.Write("ID member peminjam: ");
    string memberIdInput = Console.ReadLine() ?? string.Empty;
    bool isMemberIdValid = int.TryParse(memberIdInput, out int memberId);

    if (!isMemberIdValid)
    {
        Console.WriteLine("ID member harus berupa angka. Dibatalkan.");
        return;
    }

    string result = service.BorrowBook(bookId, memberId);
    Console.WriteLine(result);
}

void ReturnBookMenu()
{
    Console.WriteLine("--- Kembalikan Buku ---");
    ShowActiveLoans();
    
    List<Loan> loans = service.GetActiveLoans();
    if(loans.Count == 0)
    {
        Console.WriteLine("Tidak ada pinjaman aktif. Dibatalkan.");
        return;
    }
    Console.Write("ID pinjaman yang dikembalikan: ");
    string loanIdInput = Console.ReadLine() ?? string.Empty;
    bool isLoanIdValid = int.TryParse(loanIdInput, out int loanId);

    if (!isLoanIdValid)
    {
        Console.WriteLine("ID pinjaman harus berupa angka. Dibatalkan.");
        return;
    }

    string result = service.ReturnBook(loanId);
    Console.WriteLine(result);
}

void ShowActiveLoans()
{
    Console.WriteLine("--- Pinjaman Aktif ---");

    List<Loan> loans = service.GetActiveLoans();

    if (loans.Count == 0)
    {
        Console.WriteLine("(tidak ada pinjaman aktif)");
        return;
    }

    foreach (Loan loan in loans)
    {
        bool isOverdue = loan.DueDate < DateTime.Now;
        string overdueMark = isOverdue ? " [TERLAMBAT]" : string.Empty;
        string dueDateText = loan.DueDate.ToString("dd MMM yyyy");

        Console.WriteLine($"[{loan.Id}] {loan.Book.Title} — {loan.Member.Name} (jatuh tempo: {dueDateText}){overdueMark}");
    }
}