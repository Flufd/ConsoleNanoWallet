using System.Threading.Tasks;

namespace Wallet
{
    public interface IPage
    {
        Task<object> RunAsync();
    }
    
}