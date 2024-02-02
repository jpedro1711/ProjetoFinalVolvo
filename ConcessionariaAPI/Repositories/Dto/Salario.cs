namespace ConcessionariaAPI.Repositories.Dto;

public class Salario{  

    public int? _id {get;set;}
    public string _nome {get;set;}
    public double _salarioCalculado {get;set;}
    public int _mes {get;set;}
    public int _ano {get;set;}

    public Salario(int? ID, string Nome, double SalarioCalculado, int Mes, int Ano)
    {
        _id = ID;
        _nome = Nome;
        _salarioCalculado = SalarioCalculado;
        _mes = Mes;
        _ano = Ano;        
    }
    
}