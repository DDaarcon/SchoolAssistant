using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.DAL.Interfaces
{
    public interface IHasUser
    {
        long Id { get; set; }
        User? User { get; set; }
    }
}
