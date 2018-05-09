using TeduCore.Data.Entities;

namespace TeduCore.Data.Interfaces
{
    /// <summary>
    /// This interface is used to mark the owner of an object.
    /// </summary>
    public interface IHasOwner<T>
    {
        T OwnerId { set; get; }

        /// <summary>
        /// The Person instance this object belongs to.
        /// </summary>
        AppUser Owner { get; set; }
    }
}