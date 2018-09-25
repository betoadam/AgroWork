using AgroWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroWork.Dados
{
    public class Dbinitializer
    {

        public static void Initialize(AgroContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any produtors.
            if (context.Produtors.Any())
            {
                return;   // DB has been seeded
            }

            var produtors = new Produtor[]
            {
            new Produtor{Nome="Afonso Gerardt",Endereco="Linha Taquari Baxada do 10", Ativo=true,Telefone="999-999-999",Email="Afonsinho10@bol.com"},
            new Produtor{Nome="Leduvina Samp",Endereco="Linha Palmitar Baxada do 13", Ativo=true,Telefone="999-999-999",Email="Leduvina@bol.com"},
            new Produtor{Nome="Eustacio Klein",Endereco="Linha 3 irmãs", Ativo=true,Telefone="999-999-999",Email="eustacioklein@bol.com"}
            
            };
            foreach (Produtor p in produtors)
            {
                context.Produtors.Add(p);
            }
            context.SaveChanges();
            
            var vacas = new Vaca[]
            {
            new Vaca{Nome="Malhada",Numero=2,Raca="Jersey",Tipo="Novilha",Idade="1",ProdutorId=1},
            new Vaca{Nome="Mimosa",Numero=2,Raca="Holandesa",Tipo="Vaca",Idade="3",ProdutorId=1},
            new Vaca{Nome="Jertrudes",Numero=2,Raca="HGB",Tipo="Vaca",Idade="5",ProdutorId=1}
            };
            foreach (Vaca v in vacas)
            {
                context.Vacas.Add(v);
            }
            context.SaveChanges();

            var inseminadors = new Inseminador[]
            {
            new Inseminador{Nome="Jair",Ativo=true,Carro="Uno 1.0",Email="jair@gmail.com",Endereco="Rua Bahia",Telefone="999-999-999"},
            new Inseminador{Nome="Jose",Ativo=true,Carro="Uno 1.6R",Email="gastao@gmail.com",Endereco="Rua Bahia",Telefone="999-999-999"}
            
            };
            foreach (Inseminador i in inseminadors)
            {
                context.Inseminadors.Add(i);
            }
            context.SaveChanges();

            var inseminacaos = new Inseminacao[]
            {

            new Inseminacao{InseminadorId=1,ProdutorId=1,VacaId=1,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            new Inseminacao{InseminadorId=1,ProdutorId=1,VacaId=2,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            new Inseminacao{InseminadorId=1,ProdutorId=2,VacaId=3,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            new Inseminacao{InseminadorId=1,ProdutorId=3,VacaId=2,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            new Inseminacao{InseminadorId=1,ProdutorId=2,VacaId=3,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            new Inseminacao{InseminadorId=1,ProdutorId=3,VacaId=1,Valor=29.5,Touro="T10",Data=DateTime.Parse("2018-09-01 08:08")},
            
            };
            foreach (Inseminacao i in inseminacaos)
            {
                context.Inseminacaos.Add(i);
            }
            context.SaveChanges();
        }
    }
}
