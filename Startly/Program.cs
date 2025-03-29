using Microsoft.AspNetCore.Hosting;
using Startly.Domain.DTOs.Atuacao;
using Startly.Domain.DTOs.Base;
using Startly.Domain.DTOs.Startup.Adicionar;
using Startly.Domain.DTOs.Startup.Atualizar;
using Startly.Domain.DTOs.Startup.Pesquisar;
using Startly.Domain.Entities;
using Startly.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StartlyContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//controller Atuação
#region Endpoint Atuacao

app.MapGet("atuacao/listar", (StartlyContext context) =>
{
    var listaAtuacaoDto = context.AtuacaoSet.Select(x => new AtuacaoListarDto
    {
        Id = x.Id,
        Descricao = x.Descricao,
    }).ToList();

    return Results.Ok(listaAtuacaoDto);
});

app.MapGet("atuacao/obter/{Id}", (StartlyContext context, Guid Id) =>
{
    var atuacao = context.AtuacaoSet.Find(Id);

    if (atuacao == null)
        return Results.NotFound($"Startup com ID {Id} não encontrada.");

    var atuacaoDto = new AtuacaoObterDto
    {
        Id = atuacao.Id,
        Descricao = atuacao.Descricao
    };

    return Results.Ok(atuacaoDto);
});

app.MapPost("atuacao/adicionar", (StartlyContext context, AtuacaoAdicionarDto atuacaoDto) =>
{
    var atuacao = new Atuacao
    {
        Id = Guid.NewGuid(),
        Descricao = atuacaoDto.Descricao,
    };

    context.AtuacaoSet.Add(atuacao);
    context.SaveChanges();

    return Results.Created("Created", "Atuacao Registrada com Sucesso");
});

app.MapPut("atuacao/atualizar", (StartlyContext context, AtuacaoAtualizarDto atuacaoAtualizarDto) =>
{
    var atuacao = context.AtuacaoSet.Find(atuacaoAtualizarDto.Id);

    if (atuacao == null)
        return Results.NotFound($"Atucação com ID {atuacaoAtualizarDto.Id} não encontrada.");

    atuacao.Descricao = atuacaoAtualizarDto.Descricao;
    context.AtuacaoSet.Update(atuacao);
    context.SaveChanges();

    return Results.Ok("Atuação atualizada com sucesso");
});

app.MapDelete("atuacao/remover/{Id}", (StartlyContext context, Guid Id) =>
{
    var atuacao = context.AtuacaoSet.Find(Id);

    if (atuacao == null)
        return Results.NotFound($"Atuação com ID {Id} não encontrada.");

    context.AtuacaoSet.Remove(atuacao);
    context.SaveChanges();
    return Results.Ok("Atuacao removida com sucesso");
});

#endregion

//controller Startup
#region Endpoint Startup
app.MapPost("startup/adicionar", (StartlyContext context, StartupAdicionarDto startupAdicionarDto) =>
{
    if (startupAdicionarDto.Senha != startupAdicionarDto.ConfirmarSenha)
        return Results.BadRequest(new BaseResponse("Senhas não Conferem!!!"));



    var idStartup = Guid.NewGuid();

    var startup = new Startup
    {
        Id = idStartup,
        Nome = startupAdicionarDto.Nome,
        Descricao = startupAdicionarDto.Descricao,
        Metas = startupAdicionarDto.Metas,
        CNPJ = startupAdicionarDto.CNPJ,
        Cep = startupAdicionarDto.Cep,
        Logradouro = startupAdicionarDto.Logradouro,
        Numero = startupAdicionarDto.Numero,
        Bairro = startupAdicionarDto.Bairro,
        Municipio = startupAdicionarDto.Municipio,
        UF = startupAdicionarDto.UF,
        SiteStartup = startupAdicionarDto.SiteStartup,
        QuantidadeFuncionario = startupAdicionarDto.QuantidadeFuncionario,
        EnumTicket = startupAdicionarDto.EnumTicket,
        EnumTipoDeAtendimento = startupAdicionarDto.EnumTipoDeAtendimento,
        ResponsavelCadastro = startupAdicionarDto.ResponsavelCadastro,
        Login = startupAdicionarDto.Login,
        Senha = startupAdicionarDto.Senha,
        Atuacoes = startupAdicionarDto.Atuacoes.Select(a => new StartupAtuacao
        {
            Id = Guid.NewGuid(),
            StartupId = idStartup,
            AtuacaoId = a.AtuacaoId
        }).ToList(),
        Imagens = startupAdicionarDto.Imagens.Select(i => new StartupImagem
        {
            Id = Guid.NewGuid(),
            StartupId = idStartup,
            TipoImagem = i.TipoImagem,
            Imagem = i.Imagem
        }).ToList(),
        Contatos = startupAdicionarDto.Contatos.Select(c => new StartupContato
        {
            Id = Guid.NewGuid(),
            StartupId = idStartup,
            Contato = c.Contato,
            Conteudo = c.Conteudo
        }).ToList(),
        Videos = startupAdicionarDto.Videos.Select(v => new StartupVideo
        {
            Id = Guid.NewGuid(),
            StartupId = idStartup,
            LinkVideo = v.LinkVideo
        }).ToList()
    };

    context.StartupSet.Add(startup);
    context.SaveChanges();

    return Results.Created("Created",new BaseResponse("Startup Adicionada com Sucesso!!!"));
});

app.MapGet("startup/listar", (StartlyContext context) =>
{

    var ListaStartup = context.StartupSet.Select(x => new StartupPesquisarDto
    {

        Id = x.Id,
        Nome = x.Nome,
        Descricao = x.Descricao,
        Metas = x.Metas,
        CNPJ = x.CNPJ,
        Cep = x.Cep,
        Logradouro = x.Logradouro,
        Numero = x.Numero,
        Bairro = x.Bairro,
        Municipio = x.Municipio,
        UF = x.UF,
        SiteStartup = x.SiteStartup,
        QuantidadeFuncionario = x.QuantidadeFuncionario,
        EnumTicket = x.EnumTicket,
        EnumTipoDeAtendimento = x.EnumTipoDeAtendimento,
        ResponsavelCadastro = x.ResponsavelCadastro,
        Atuacoes = x.Atuacoes.Select(a => new StartupAtuacaoPesquisarDto
        {
            Id = a.Id,
            StartupId = a.StartupId,
            AtuacaoId = a.AtuacaoId
        }).ToList(),
        Imagens = x.Imagens.Select(i => new StartupImagemPesquisarDto
        {
            id = i.Id,
            Imagem = i.Imagem,
            TipoImagem = i.TipoImagem,
        }).ToList(),
        Contatos = x.Contatos.Select(c => new StartupContatoPesquisarDto 
        {
            Id = c.Id,
            Contato = c.Contato,
            StartupId = c.StartupId
        }).ToList(),
        Videos = x.Videos.Select(v => new StartupVideoPesquisarDto 
        {
            Id = v.Id,
            StartupId = v.StartupId,
            LinkVideo = v.LinkVideo,
        }).ToList(),

    }).ToList();

    if (ListaStartup == null)
        return Results.NotFound(new BaseResponse("Não há Nenhuma Startup Criada!!!"));

        return Results.Ok(ListaStartup);
});

app.MapGet("startup/obter/{id}", (StartlyContext context, Guid id) =>
{
    var startup = context.StartupSet.Find(id);

    if (startup == null)
    {
        return Results.NotFound($"Startup com ID {id} não encontrada.");
    }

    var startupDto = new StartupPesquisarDto
    {
        Id = startup.Id,
        Nome = startup.Nome,
        Descricao = startup.Descricao,
        Metas = startup.Metas,
        CNPJ = startup.CNPJ,
        Cep = startup.Cep,
        Logradouro = startup.Logradouro,
        Numero = startup.Numero,
        Bairro = startup.Bairro,
        Municipio = startup.Municipio,
        UF = startup.UF,
        SiteStartup = startup.SiteStartup,
        QuantidadeFuncionario = startup.QuantidadeFuncionario,
        EnumTicket = startup.EnumTicket,
        EnumTipoDeAtendimento = startup.EnumTipoDeAtendimento,
        ResponsavelCadastro = startup.ResponsavelCadastro,
        Atuacoes = startup.Atuacoes.Select(a => new StartupAtuacaoPesquisarDto
        {
            Id = a.Id,
            StartupId = a.StartupId,
            AtuacaoId = a.AtuacaoId
        }).ToList(),
        Imagens = startup.Imagens.Select(i => new StartupImagemPesquisarDto
        {
            id = i.Id,
            Imagem = i.Imagem,
            TipoImagem = i.TipoImagem,
        }).ToList(),
        Contatos = startup.Contatos.Select(c => new StartupContatoPesquisarDto
        {
            Id = c.Id,
            Contato = c.Contato,
            StartupId = c.StartupId
        }).ToList(),
        Videos = startup.Videos.Select(v => new StartupVideoPesquisarDto
        {
            Id = v.Id,
            StartupId = v.StartupId,
            LinkVideo = v.LinkVideo,
        }).ToList(),

    };

    return Results.Ok(startupDto);
});

app.MapDelete("startup/remover/{id}", (StartlyContext context, Guid Id) =>
{
    var startup = context.StartupSet.Find(Id);

    if (startup == null)
    {
        return Results.NotFound(new BaseResponse($"Startup com ID {Id} não encontrada."));
    }

    context.StartupSet.Remove(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Startup deletada com sucesso!!!"));
});

app.MapPut("startup/atualizar/{id}", (StartlyContext context, StartupAtualizarDto startupAtualizarDto) =>
{

    var startup = context.StartupSet.Find(startupAtualizarDto.id);

    if (startup == null)
    {
        return Results.NotFound(new BaseResponse($"Não foi Possível encontrar a Startup De id {startupAtualizarDto.id}."));
    }

    var startupDto = new StartupAtualizarDto 
    {
        Nome = startup.Nome,
        Descricao = startup.Descricao,
        metas = startup.Metas,
        QuantidadeFuncionario = startup.QuantidadeFuncionario,
        EnumTicket = startup.EnumTicket,
        EnumTipoDeAtendimento = startup.EnumTipoDeAtendimento,
        Atuacoes = startup.Atuacoes.Select(a => new StartupAtuacaoAtualizarDto
        {
            StartupId = a.StartupId,
            AtuacaoId = a.AtuacaoId
        }).ToList(),
        Imagens = startup.Imagens.Select(i => new StartupImagemAtualizarDto
        {
            StartupId = i.StartupId,
            Imagem = i.Imagem,
            TipoImagem = i.TipoImagem,
        }).ToList(),
        Contatos = startup.Contatos.Select(c => new StartupContatoAtualizarDto
        {
            Contato = c.Contato,
            StartupId = c.StartupId
        }).ToList(),
        Videos = startup.Videos.Select(v => new StartupVideoAtualizarDto
        {
            StartupId = v.StartupId,
            LinkVideo = v.LinkVideo,
        }).ToList(),

    };

    context.StartupSet.Update(startup);
    context.SaveChanges();

    return Results.Ok("Curso atualizado com sucesso");
});
#endregion

app.Run();

