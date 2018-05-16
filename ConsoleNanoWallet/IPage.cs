using System.Threading.Tasks;

namespace ConsoleNanoWallet
{
    public interface IPage
    {
        Task<object> RunAsync();
    }
    
}