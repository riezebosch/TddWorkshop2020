using System.Threading.Tasks;

namespace TddWorkshop
{
    public interface IBankCodeProvider
    {
        Task<string[]> BankCodes();
    }
}