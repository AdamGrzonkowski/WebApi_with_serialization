using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IXmlService
    {
        Task WriteRequestsToFiles(string directoryToSave);
    }
}
