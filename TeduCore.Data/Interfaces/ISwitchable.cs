using TeduCore.Infrastructure.Enums;

namespace TeduCore.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}