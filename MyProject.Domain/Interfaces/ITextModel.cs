using System.Threading.Tasks;

namespace MyProject.Domain.Interfaces
{
    public interface ITextModel
    {
        Task<string> GenerateAsync(string prompt);
    }
}
