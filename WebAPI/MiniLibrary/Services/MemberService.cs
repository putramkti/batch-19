using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<List<MemberDTO>>> GetAllAsync()
    {
        List<Member> members = await _memberRepository.GetAllAsync();
        List<MemberDTO> dtos = _mapper.Map<List<MemberDTO>>(members);
        return ApiResponseDto<List<MemberDTO>>.Success(dtos);
    }

    public async Task<ApiResponseDto<MemberDTO>> CreateAsync(MemberCreateDTO createDto)
    {
        Member newMember = _mapper.Map<Member>(createDto);

        await _memberRepository.AddAsync(newMember);
        await _memberRepository.SaveChangesAsync();

        MemberDTO dto = _mapper.Map<MemberDTO>(newMember);
        return ApiResponseDto<MemberDTO>.Success(dto);
    }
}