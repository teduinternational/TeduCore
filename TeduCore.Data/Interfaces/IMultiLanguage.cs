using TeduCore.Data.Entities;

namespace TeduCore.Data.Interfaces
{
    public interface IMultiLanguage<T>
    {
        T LanguageId { set; get; }
    }
}