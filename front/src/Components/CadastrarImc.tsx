import { useEffect, useState } from "react";
import { Categoria } from "../../models/Alunos";
import { Produto } from "../../models/Imc";

function CadastrarImc(){

    const [imc, setImc] = useState("");
    const [AlunoId, setAlunoId] = useState(1);
    const [Aluno, setAluno] = useState<Aluno[]>([])
    
    useEffect(() => {
        
        fetch("http://localhost:5246/imc/listar")
        .then(resposta => {return resposta.json()})
        .then(aluno => setAluno(aluno));
    },[])

    function HandleSubmit(e:any){

        e.preventDefault()

        const imc : Imc = {
            imc : imc,
            AlunoIdId : AlunoIdId
        }

        fetch("http://localhost:5246/imc/cadastrar" , {
            method: "POST",
            headers: {
                "Content-Type": "application/json",

            },
            body: JSON.stringify(imc)
        })
        .then(resposta => {return resposta.json()})
        .then(imcCriado => console.log(imcCriado));
    }

    return (
        <div>
            <form onSubmit={HandleSubmit}>
                <div>
                    <label htmlFor="imc">imc:</label>
                    <input type="text" onChange={(e:any) => setNome(e.target.value)}/>
                </div>
                <label htmlFor="aluno">Aluno:</label>
                    <select onChange={(e:any) => setAlunoId(Number(e.target.value))}>
                        {aluno.map(aluno => (
                            <option key={aluno.AlunoId} value={aluno.AlunoId}>{aluno.imc}</option>
                        ))
                        }
                    </select>
                </div>
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    )
}

export default CadastrarImc;