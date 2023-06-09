﻿using NetPlatHF.BLL.Dtos;

namespace NetPlatHF.BLL.Interfaces;




public interface ITemplateService
{
    public IReadOnlyCollection<Dtos.Template> List();
    public IReadOnlyCollection<Dtos.Template> ListSelf(string userApiKey);
    public Dtos.Template GetById(int id, string? userApiKey);
    public Dtos.Template Create(int groupId, int exerciseId, CreateTemplate createTemplate, string userApiKey);
    public Dtos.Template? Update();
    public Dtos.Template? Delete(int id, string userApiKey);
}
