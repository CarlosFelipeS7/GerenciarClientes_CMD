using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gestor_de_Clientes_no_CMD
{
    internal class Program
    {

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 } //Numerando para o enum começar contar os itens a partir do 1



        [System.Serializable]
        struct Cliente{     
            public string nome;
            public string email;
            public string cpf;
        }

        //Variavel Global
        static List<Cliente> clientes = new List<Cliente>();



        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp; //convertendo numero digitado pelo usuario atraves de cast para o item correspondente no menu

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();

                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                       
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();

            }







            Console.ReadLine();


        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluído, aperte enter para sair.");
            Console.ReadLine();



        }

        static void Listagem()
        {

            if(clientes.Count > 0)
            {

                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("--------------------------------");
                    i++;

                }
            } else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }

            Console.WriteLine("Aperte enter para sair");
            Console.ReadLine();
        }


        static void Salvar()
        {
            // Abre (ou cria) um arquivo chamado "clients.dat" para escrita/leitura.
            // FileMode.OpenOrCreate → se o arquivo não existir, ele cria.
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            // Cria um objeto BinaryFormatter, que serve para transformar a lista em bytes.
            BinaryFormatter encoder = new BinaryFormatter();

            // Serializa (converte) a lista "clientes" para bytes e grava dentro do arquivo.
            encoder.Serialize(stream, clientes);

            // Fecha o arquivo depois de salvar, liberando o recurso.
            stream.Close();
        }

        static void Carregar()
        {

            // Abre (ou cria) o arquivo "clients.dat"
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try // tenta executar esse bloco
            {
               

                // Cria o mesmo BinaryFormatter para ler os dados salvos
                BinaryFormatter encoder = new BinaryFormatter();

                // Desserializa → lê os bytes do arquivo e converte de volta para List<Cliente>
                clientes = (List<Cliente>)encoder.Deserialize(stream);

                // Se por acaso o retorno for nulo, cria uma lista nova vazia para evitar erros
                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                // Fecha o arquivo
                
            }
            catch (Exception e) // se der erro (ex: arquivo vazio ou corrompido)
            {
                // Apenas cria uma lista vazia para o programa continuar funcionando
                clientes = new List<Cliente>();
            }

            stream.Close();
        }



        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que voce deseja remover: ");
           int id = int.Parse(Console.ReadLine());
            if (id >=0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
                Console.WriteLine("Cliente removido");
            }
            else
            {
                Console.WriteLine("ID digitado é invalido, tente novamente");
            }
            


        }




    }
}
