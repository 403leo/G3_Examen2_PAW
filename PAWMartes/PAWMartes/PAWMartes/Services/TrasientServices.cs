
namespace PAWMartes.Services
{
    public class TrasientServices : ITrasientServices
    {
        Guid ID;

        public TrasientServices()
        {
            ID = Guid.NewGuid();
        }

        public Guid ObtenerID()
        {
            return ID;
        }
    }
}
