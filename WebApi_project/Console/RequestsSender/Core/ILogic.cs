using System.Threading.Tasks;

namespace RequestsSender.Core
{
    public interface ILogic
    {
        Task RunAsync(string[] parameter);
    }
}
