using API.Entities;
using System.Threading.Tasks;

namespace API
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser user);
	}
}
