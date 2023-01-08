using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(i => i.Photos).ToListAsync();
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _context.Users.Include(i => i.Photos).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _context.Users.Include(i => i.Photos).FirstOrDefaultAsync(s => s.UserName == username);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            //without auto mapper
            //return await _context.Users
            //            .Where(s => s.UserName == username)
            //            .Select(user => new MemberDto
            //            {
            //                Id = user.Id,
            //                UserName = user.UserName,
            //            })
            //            .FirstOrDefaultAsync(); 

            return await _context.Users
                        .Where(s => s.UserName == username)
                        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider) //when using projection we do not need to eagerly load the collection-- more efficient!
                        .FirstOrDefaultAsync();
        }
    }
}
