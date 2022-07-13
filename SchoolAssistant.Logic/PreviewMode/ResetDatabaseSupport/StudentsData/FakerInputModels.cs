using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport.StudentsData
{
    public record StudentsDataFakerInput(
        StudentDataFakerInput Student,
        ParentDataFakerInput? FirstParent = null,
        SecondParentDataFakerInput? SecondParent = null,
        StudentRegisterRecord? StudentToOverride = null,
        ParentRegisterSubrecord? FirstParentToOverride = null,
        ParentRegisterSubrecord? SecondParentToOverride = null);

    public record StudentDataFakerInput(
        string? FirstName = null,
        string? LastName = null,
        string? SecondName = "");

    public record ParentDataFakerInput(
        string? FirstName = null,
        string? LastName = null,
        bool? AddressLikeChilds = null,
        string? SecondName = "");

    public record SecondParentDataFakerInput(
        string? FirstName = null,
        string? LastName = null,
        bool? AddressLikeChilds = null,
        bool? IsPresent = null,
        string? SecondName = "") : ParentDataFakerInput(FirstName, LastName, AddressLikeChilds, SecondName);
}
