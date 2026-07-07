using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<List<BookDTO>>> GetAllAsync()
    {
        List<Book> books = await _bookRepository.GetAllAsync();
        List<BookDTO> dtos = _mapper.Map<List<BookDTO>>(books);
        return ApiResponseDto<List<BookDTO>>.Success(dtos);
    }

    public async Task<ApiResponseDto<BookDTO>> GetByIdAsync(int id)
    {
        Book? book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return ApiResponseDto<BookDTO>.Failure("Buku tidak ditemukan.");
        }

        BookDTO dto = _mapper.Map<BookDTO>(book);
        return ApiResponseDto<BookDTO>.Success(dto);
    }

    public async Task<ApiResponseDto<BookDTO>> CreateAsync(BookCreateDTO createDto)
    {
        List<Category> matchedCategories = await _categoryRepository.GetByIdsAsync(createDto.CategoryIds);
        if (matchedCategories.Count == 0)
        {
            return ApiResponseDto<BookDTO>.Failure("Tidak ada kategori valid yang ditemukan.");
        }

        Book newBook = _mapper.Map<Book>(createDto);
        newBook.Categories = matchedCategories;

        await _bookRepository.AddAsync(newBook);
        await _bookRepository.SaveChangesAsync();

        BookDTO dto = _mapper.Map<BookDTO>(newBook);
        return ApiResponseDto<BookDTO>.Success(dto);
    }

    public async Task<ApiResponseDto<BookDTO>> UpdateAsync(int id, BookCreateDTO updateDto)
    {
        Book? existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook is null)
        {
            return ApiResponseDto<BookDTO>.Failure("Buku tidak ditemukan.");
        }

        List<Category> matchedCategories = await _categoryRepository.GetByIdsAsync(updateDto.CategoryIds);
        if (matchedCategories.Count == 0)
        {
            return ApiResponseDto<BookDTO>.Failure("Tidak ada kategori valid yang ditemukan.");
        }

        existingBook.Title = updateDto.Title;
        existingBook.Stock = updateDto.Stock;

        existingBook.Categories.Clear();
        foreach (Category category in matchedCategories)
        {
            existingBook.Categories.Add(category);
        }

        await _bookRepository.SaveChangesAsync();

        BookDTO dto = _mapper.Map<BookDTO>(existingBook);
        return ApiResponseDto<BookDTO>.Success(dto);
    }

    public async Task<ApiResponseDto<string>> DeleteAsync(int id)
    {
        Book? book = await _bookRepository.GetByIdWithLoansAsync(id);
        if (book is null)
        {
            return ApiResponseDto<string>.Failure("Buku tidak ditemukan.");
        }

        if (book.Loans.Count > 0)
        {
            return ApiResponseDto<string>.Failure("Buku tidak bisa dihapus karena masih punya riwayat peminjaman.");
        }

        _bookRepository.Remove(book);
        await _bookRepository.SaveChangesAsync();

        return ApiResponseDto<string>.Success("Buku berhasil dihapus.");
    }
}