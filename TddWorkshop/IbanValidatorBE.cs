using System.Threading.Tasks;

namespace TddWorkshop
{
    internal class IbanValidatorBE : IIbanValidator
    {
        public Task<bool> Check(string input) => 
            Task.FromResult(true);
    }
}