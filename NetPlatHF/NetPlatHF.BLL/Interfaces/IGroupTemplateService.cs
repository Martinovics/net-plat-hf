using NetPlatHF.BLL.Dtos;


namespace NetPlatHF.BLL.Interfaces;




public interface IGroupTemplateService
{
    public IReadOnlyCollection<Dtos.GroupTemplate> ListTemplates();
    public IReadOnlyCollection<Dtos.GroupTemplate> ListUserTemplates(string userApiKey);
    public Dtos.GroupTemplate Insert(CreateGroupTemplate template, string userApiKey);
    public Dtos.GroupTemplate? Update(int id, UpdateGroupTemplate template, string userApiKey);
    public Dtos.GroupTemplate? Delete(int id, string userApiKey);
    public Dtos.GroupTemplate? GetTemplateById(int id, string? userApiKey);
}
