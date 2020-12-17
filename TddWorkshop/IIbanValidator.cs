using System.Threading.Tasks;

namespace TddWorkshop
{
    internal interface IIbanValidator
    {
        Task<bool> Check(string input);
    }
}