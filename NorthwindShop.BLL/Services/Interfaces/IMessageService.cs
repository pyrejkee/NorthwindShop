using System.Threading.Tasks;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}
