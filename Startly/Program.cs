
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Startly.Domain.DTOs.Atuacao;
using Startly.Domain.DTOs.Base;
using Startly.Domain.DTOs.Login;
using Startly.Domain.DTOs.Startup.Adicionar;
using Startly.Domain.DTOs.Startup.Atualizar;
using Startly.Domain.Entities;
using Startly.Domain.Extensions;
using Startly.Infra.Data.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Startly.Domain.DTOs.AlterarSenha;
using Startly.Domain.DTOs.ResetSenha;
using Startly.Domain.DTOs.Startup.Listar;
using Startly.Domain.DTOs.Startup.Obter;
using Startly.Infra.Email;
using Startly.Domain.Enumerators;

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


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User", "Admin"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

#region Endpoint Atuacao

app.MapGet("atuacao/listar", (StartlyContext context) =>
{
    var listaAtuacaoDto = context.AtuacaoSet.Select(x => new AtuacaoListarDto
    {
        Id = x.Id,
        Descricao = x.Descricao,
    }).OrderBy(p => p.Descricao).ToList();

    return Results.Ok(listaAtuacaoDto);
}).WithTags("Atuação");

app.MapGet("atuacao/obter/{id:guid}", (StartlyContext context, Guid id) =>
{
    var atuacao = context.AtuacaoSet.Find(id);

    if (atuacao == null)
        return Results.NotFound($"Startup com ID {id} não encontrada.");

    var atuacaoDto = new AtuacaoObterDto
    {
        Id = atuacao.Id,
        Descricao = atuacao.Descricao
    };

    return Results.Ok(atuacaoDto);
}).WithTags("Atuação");

app.MapPost("atuacao/adicionar", (StartlyContext context, AtuacaoAdicionarDto atuacaoDto) =>
{
    var resultado = new AtuacaoAdicionarDtoValidator().Validate(atuacaoDto);

    if (!resultado.IsValid)
       return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var atuacao = new Atuacao
    {
        Id = Guid.NewGuid(),
        Descricao = atuacaoDto.Descricao,
    };

    context.AtuacaoSet.Add(atuacao);
    context.SaveChanges();

    return Results.Created("Created", "Atuacao Registrada com Sucesso");
}).RequireAuthorization("Admin").WithTags("Atuação");

app.MapPut("atuacao/atualizar", (StartlyContext context, AtuacaoAtualizarDto atuacaoAtualizarDto) =>
{
    var resultado = new AtuacaoAtualizarDtoValidator().Validate(atuacaoAtualizarDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var atuacao = context.AtuacaoSet.Find(atuacaoAtualizarDto.Id);
    if (atuacao == null)
        return Results.NotFound($"Atucação com ID {atuacaoAtualizarDto.Id} não encontrada.");

    atuacao.Descricao = atuacaoAtualizarDto.Descricao;
    context.AtuacaoSet.Update(atuacao);
    context.SaveChanges();

    return Results.Ok("Atuação atualizada com sucesso");
}).RequireAuthorization("Admin").WithTags("Atuação");

app.MapDelete("atuacao/remover/{id:guid}", (StartlyContext context, Guid id) =>
{
    var atuacao = context.AtuacaoSet.Find(id);

    if (atuacao == null)
        return Results.NotFound($"Atuação com ID {id} não encontrada.");

    context.AtuacaoSet.Remove(atuacao);
    context.SaveChanges();
    return Results.Ok("Atuacao removida com sucesso");
}).RequireAuthorization("Admin").WithTags("Atuação");

#endregion

#region Endpoint Startup

app.MapGet("startup/listar", (StartlyContext context) =>
{
    var listaStartup = context.StartupSet.Select(x => new StartupListarDto
    {
        Id = x.Id,
        Nome = x.Nome,
        Descricao = x.Descricao,
        Logo = x.Logo,
        Atuacoes = x.Atuacoes.Select(a => new StartupAtuacaoListarDto
        {
            Descricao = a.Atuacao.Descricao
        }).ToList(),
        Imagens = x.Imagens.Select(i => new StartupImagemListarDto
        {
            Imagem = i.Imagem
        }).ToList()
    }).ToList();

    return listaStartup.Count == 0 ? Results.NotFound(new BaseResponse("Não há Nenhuma Startup Criada!!!")) : Results.Ok(listaStartup);
}).WithTags("Startup");

app.MapGet("startup/obter/{id:guid}", (StartlyContext context, Guid id) =>
{
    var startup = context.StartupSet
        .Include(p => p.Atuacoes).ThenInclude(atuacoes => atuacoes.Atuacao)
        .Include(p => p.Imagens)
        .Include(p => p.Videos)
        .FirstOrDefault(p => p.Id == id);

    if (startup == null)
        return Results.NotFound($"Startup com ID {id} não encontrada.");

    var startupDto = new StartupObterDto
    {
        Id = startup.Id,
        Nome = startup.Nome,
        Descricao = startup.Descricao,
        Metas = startup.Metas,
        CNPJ = startup.CNPJ,
        EmailPessoal = startup.EmailPessoal,
        EmailCorporativo = startup.EmailCorporativo,
        TelefoneFixo = startup.TelefoneFixo,
        LinkedIn = startup.LinkedIn,
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
        Atuacoes = startup.Atuacoes.Select(a => new StartupAtuacaoObterDto
        {
            Descricao = a.Atuacao.Descricao
        }).ToList(),
        Imagens = startup.Imagens.Select(i => new StartupImagemObterDto
        {
            Imagem = i.Imagem
        }).ToList(),
        Videos = startup.Videos.Select(v => new StartupVideoObterDto
        {
            LinkVideo = v.LinkVideo,
        }).ToList(),
    };

    return Results.Ok(startupDto);
}).WithTags("Startup");

app.MapPost("startup/adicionar", (StartlyContext context, StartupAdicionarDto startupAdicionarDto) =>
{
    var resultado = new StartupAdicionarDtoValidator().Validate(startupAdicionarDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var idStartup = Guid.NewGuid();

    var startup = new Startup
    {
        Id = idStartup,
        Nome = startupAdicionarDto.Nome,
        Descricao = startupAdicionarDto.Descricao,
        Metas = startupAdicionarDto.Metas,
        CNPJ = startupAdicionarDto.CNPJ,
        EmailPessoal = startupAdicionarDto.EmailPessoal,
        EmailCorporativo = startupAdicionarDto.EmailCorporativo,
        TelefoneFixo = startupAdicionarDto.TelefoneFixo,
        LinkedIn = startupAdicionarDto.LinkedIn,
        Cep = startupAdicionarDto.Cep,
        Logradouro = startupAdicionarDto.Logradouro,
        Numero = startupAdicionarDto.Numero,
        Bairro = startupAdicionarDto.Bairro,
        Municipio = startupAdicionarDto.Municipio,
        UF = startupAdicionarDto.UF,
        SiteStartup = startupAdicionarDto.SiteStartup,
        QuantidadeFuncionario = startupAdicionarDto.QuantidadeFuncionario,
        EnumTicket = startupAdicionarDto.TicketMedio,
        EnumTipoDeAtendimento = startupAdicionarDto.TipoAtendimento,
        ResponsavelCadastro = startupAdicionarDto.ResponsavelCadastro,
        Login = startupAdicionarDto.Login,
        Senha = startupAdicionarDto.Senha.EncryptPassword(),
        Logo = startupAdicionarDto.Logo,
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
            Imagem = i.Imagem
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

app.MapPut("startup/atualizar", (StartlyContext context, StartupAtualizarDto startupAtualizarDto) =>
{
    var resultado = new StartupAtualizarDtoValidator().Validate(startupAtualizarDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var startup = context.StartupSet
        .Include(p => p.Atuacoes)
        .Include(p => p.Imagens)
        .Include(p => p.Videos)
        .FirstOrDefault(p => p.Id == startupAtualizarDto.Id);

    if (startup == null)
        return Results.NotFound(new BaseResponse($"Não foi Possível encontrar a Startup de Id: {startupAtualizarDto.Id}."));

    startup.Videos.ToList().ForEach(startupVideo => context.StartupVideoSet.Remove(startupVideo));
    startup.Imagens.ToList().ForEach(startupImagem => context.StartupImagemSet.Remove(startupImagem));
    startup.Atuacoes.ToList().ForEach(startupAtuacao => context.StartupAtuacaoSet.Remove(startupAtuacao));

    //Atualizando dados
    startup.Nome = startupAtualizarDto.Nome;
    startup.Descricao = startupAtualizarDto.Descricao;
    startup.EmailPessoal = startupAtualizarDto.EmailPessoal;
    startup.EmailCorporativo = startupAtualizarDto.EmailCorporativo;
    startup.TelefoneFixo = startupAtualizarDto.TelefoneFixo;
    startup.LinkedIn = startupAtualizarDto.LinkedIn;
    startup.Metas = startupAtualizarDto.Metas;
    startup.QuantidadeFuncionario = startupAtualizarDto.QuantidadeFuncionario;
    startup.EnumTicket = startupAtualizarDto.TicketMedio;
    startup.Logo = startupAtualizarDto.Logo;

    startupAtualizarDto.Atuacoes.ForEach(atuacao =>
    {
        startup.Atuacoes.Add(new StartupAtuacao
        {
            Id = Guid.NewGuid(),
            StartupId = startup.Id,
            AtuacaoId = atuacao.AtuacaoId
        });
    });

    startupAtualizarDto.Imagens.ForEach(imagem =>
    {
        startup.Imagens.Add(new StartupImagem
        {
            Id = Guid.NewGuid(),
            StartupId = startup.Id,
            Imagem = imagem.Imagem
        });
    });

    startupAtualizarDto.Videos.ForEach(video =>
    {
        startup.Videos.Add(new StartupVideo
        {
            Id = Guid.NewGuid(),
            StartupId = startup.Id,
            LinkVideo = video.LinkVideo
        });
    });

    context.StartupSet.Update(startup);
    context.SaveChanges();

    return Results.Ok("Curso atualizado com sucesso");
}).RequireAuthorization().WithTags("Startup");

app.MapDelete("startup/remover/{id:guid}", (StartlyContext context, Guid id) =>
{
    var startup = context.StartupSet.Find(id);

    if (startup == null)
        return Results.NotFound(new BaseResponse($"Startup com ID {id} não encontrada."));

    context.StartupSet.Remove(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Startup deletada com sucesso!!!"));
}).RequireAuthorization().WithTags("Startup");

#endregion

#region EndPoint Autenticar

app.MapPost("autenticar", (StartlyContext context, AutenticarDto autenticarDto) =>
{
    var startup = context.StartupSet.FirstOrDefault(p => p.Login == autenticarDto.Login && p.Senha == autenticarDto.Senha.EncryptPassword());
    if (startup is null)
        return Results.BadRequest(new BaseResponse("Usuário ou Senha Incorretos"));

    var claims = new[]
    {
        new Claim("Id", startup.Id.ToString()),
        new Claim("Nome", startup.Nome),
        new Claim("Senha", startup.Senha),
        new Claim(ClaimTypes.Role, "Admin")
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

}).WithTags("Segurança");

app.MapPost("gerar-chave-reset-senha", (StartlyContext context, GerarResetSenhaDto gerarResetSenhaDto) =>
{
    var resultado = new GerarResetSenhaDtoValidator().Validate(gerarResetSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var startup = context.StartupSet.Include(p => p.Contatos).FirstOrDefault(p => p.Contatos.Any(c => c.Conteudo == gerarResetSenhaDto.Email && c.Contato == EnumTipoContato.Email));

    if (startup is not null)
    {
        startup.ChaveResetSenha = Guid.NewGuid();
        context.StartupSet.Update(startup);
        context.SaveChanges();

        var emailService = new EmailService();
        var enviarEmailResponse = emailService.EnviarEmail(gerarResetSenhaDto.Email, "Reset de Senha", $"https://url-front/reset-senha/{startup.ChaveResetSenha}", true);
        if (!enviarEmailResponse.Sucesso)
            return Results.BadRequest(new BaseResponse("Erro ao enviar o e-mail: " + enviarEmailResponse.Mensagem));
    }

    return Results.Ok(new BaseResponse("Se o e-mail informado estiver correto, você receberá as instruções por e-mail."));
}).WithTags("Segurança");

app.MapPut("resetar-senha", (StartlyContext context, ResetSenhaDto resetSenhaDto) =>
{
    var resultado = new ResetSenhaDtoValidator().Validate(resetSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var startup = context.StartupSet.FirstOrDefault(p => p.ChaveResetSenha == resetSenhaDto.ChaveResetSenha);

    if (startup is null)
        return Results.BadRequest(new BaseResponse("Chave de reset de senha inválida."));

    startup.Senha = resetSenhaDto.NovaSenha.EncryptPassword();
    startup.ChaveResetSenha = null;
    context.StartupSet.Update(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Senha alterada com sucesso."));
}).WithTags("Segurança");

app.MapPut("alterar-senha", (StartlyContext context, ClaimsPrincipal claims, AlterarSenhaDto alterarSenhaDto) =>
{
    var resultado = new AlterarSenhaDtoValidator().Validate(alterarSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var userIdClaim = claims.FindFirst("Id")?.Value;
    if (userIdClaim == null)
        return Results.Unauthorized();

    var userId = Guid.Parse(userIdClaim);
    var startup = context.StartupSet.FirstOrDefault(p => p.Id == userId);
    if (startup == null)
        return Results.NotFound(new BaseResponse("Usuário não encontrado."));

    startup.Senha = alterarSenhaDto.NovaSenha.EncryptPassword();
    context.StartupSet.Update(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Senha alterada com sucesso."));
}).WithTags("Segurança");

#endregion

app.Run();
