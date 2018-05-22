using System.Threading.Tasks;

namespace ConsoleNanoWallet.Pages
{
    public interface IPage
    {
        Task<object> RunAsync();
    }
    
}