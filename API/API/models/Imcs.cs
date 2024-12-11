using System;

namespace API.models;

public class Imcs
{
    public int imcId { get; set; }
    public int imc { get; set; }
    public int AlunoId { get; set; }
    public Alunos? Aluno { get; set; }
}
