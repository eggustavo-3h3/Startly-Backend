
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Startly.Domain.DTOs.Atuacao;
using Startly.Domain.DTOs.Base;
using Startly.Domain.DTOs.Login;
using Startly.Domain.DTOs.Startup.Adicionar;
using Startly.Domain.DTOs.Startup.Atualizar;
using Startly.Domain.DTOs.Startup.Pesquisar;
using Startly.Domain.Entities;
using Startly.Domain.Extensions;
using Startly.Infra.Data.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Startly",
        Version = "v1",
        Description = "API para conexao de investidores e startups pela Startly"
    });

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"<b>JWT Autorização</b> <br/> 
                      Digite 'Bearer' [espaço] e em seguida seu token na caixa de texto abaixo.
                      <br/> <br/>
                      <b>Exemplo:</b> 'bearer 123456abcdefg...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
});


builder.Services.AddDbContext<StartlyContext>();
builder.Services.AddCors();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "startly",
            ValidAudience = "startly",
            IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(
                  "{b76ecac1-7f05-455b-a51d-0ef0500c8e4c}"))
        };
    });


builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


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
}).WithTags("Atuação");

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
}).WithTags("Atuação");

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
}).RequireAuthorization().WithTags("Atuação");

app.MapPut("atuacao/atualizar", (StartlyContext context, AtuacaoAtualizarDto atuacaoAtualizarDto) =>
{
    var atuacao = context.AtuacaoSet.Find(atuacaoAtualizarDto.Id);

    if (atuacao == null)
        return Results.NotFound($"Atucação com ID {atuacaoAtualizarDto.Id} não encontrada.");

    atuacao.Descricao = atuacaoAtualizarDto.Descricao;
    context.AtuacaoSet.Update(atuacao);
    context.SaveChanges();

    return Results.Ok("Atuação atualizada com sucesso");
}).RequireAuthorization().WithTags("Atuação");

app.MapDelete("atuacao/remover/{Id}", (StartlyContext context, Guid Id) =>
{
    var atuacao = context.AtuacaoSet.Find(Id);

    if (atuacao == null)
        return Results.NotFound($"Atuação com ID {Id} não encontrada.");

    context.AtuacaoSet.Remove(atuacao);
    context.SaveChanges();
    return Results.Ok("Atuacao removida com sucesso");
}).RequireAuthorization().WithTags("Atuação");

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
        Senha = startupAdicionarDto.Senha.EncryptPassword(),
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

    return Results.Created("Created", new BaseResponse("Startup Adicionada com Sucesso!!!"));
}).WithTags("Startup");

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
            Descricao = a.Atuacao.Descricao,
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
}).WithTags("Startup");

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
}).RequireAuthorization().WithTags("Startup");

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
}).RequireAuthorization().WithTags("Startup");

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
}).RequireAuthorization().WithTags("Startup");
#endregion

#region EndPoint Autentica
app.MapPost("autenticar", (StartlyContext context, autenticarDto autenticarDto) =>
{
    var startup = context.StartupSet.FirstOrDefault(p => p.Login == autenticarDto.Login && p.Senha == autenticarDto.Senha.EncryptPassword());
    if (startup is null)
        return Results.BadRequest(new BaseResponse("Usuário ou Senha Incorretos"));

    var claims = new[]
    {
        new Claim("Nome", startup.Nome),
        new Claim("Senha", startup.Senha)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("" + "{b76ecac1-7f05-455b-a51d-0ef0500c8e4c}"));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "startly",
        audience: "startly",
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds
    );

    return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));

}).WithTags("Autorização");

#endregion

app.Run();

