using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapPost("/aluno/cadastrar" , ([FromBody] Aluno aluno, [FromServices] AppDataContext ctx) =>
{
    ctx.TabelaAluno?.Add(Aluno);
    ctx.SaveChanges();
    return Results.Created("", Aluno);
});

app.MapPost("/imc/cadastrar" , ([FromBody] Imcs imcs, [FromServices] AppDataContext ctx) =>
{
    var Aluno = ctx.TabelaAluno?.Find(imcs.AlunoId);
    if(Aluno == null){
        return Results.NotFound();
    }

    ctx.TabelaImcs?.Add(imcs);
    imcs.Aluno = Aluno;
    ctx.SaveChanges();
    return Results.Created("", imcs);
});

app.MapGet("/imc/listarporaluno", ([FromServices] AppDataContext ctx) => 
{
    return Results.Ok(ctx.TabelaAluno?.ToList());
});

app.MapGet("/imc/listar", ([FromServices] AppDataContext ctx) => 
{
    return Results.Ok(ctx.TabelaImcs?.Include(x => x.Aluno).ToList());
});

app.MapPut("/imc/alterar/{id}", ([FromRoute] string id, [FromBody] Imcs imcsAlterado, [FromServices] AppDataContext ctx) =>
{
    Imcs? imcs = ctx.TabelaImcs?.Find(id);
    if(imcs == null){
        return Results.NotFound();
    }
    Aluno? aluno = ctx.TabelaAluno?.Find(imcsAlterado.AlunoId);
    if(aluno == null){
        return Results.NotFound();
    }

    imcs.imcs = imcsAlterado.Nome;
    imcs.Aluno = aluno;
    imcs.AlunoId = imcsAlterado.AlunoId;

    ctx.TabelaImcs?.Update(imcs);
    ctx.SaveChanges();
    return Results.Ok(imcs);
});

app.UseCors("Acesso Total");

app.Run();