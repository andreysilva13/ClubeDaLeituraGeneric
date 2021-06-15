using ClubeLeitura.ConsoleApp.Dominio;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class Controlador<T> where T : EntidadeBase, IValidavel
    {
        private int ultimoId;
        private List<T> registros;

        public Controlador()
        {
            registros = new List<T>();
        }

        public string Adicionar(T registro)
        {
            string EhPossivelAdicionar = registro.Validar();
            if (EhPossivelAdicionar == "ESTA_VALIDO")
            {
                registro.id = NovoId();
                registros.Add(registro);
            }

            return EhPossivelAdicionar;
        }

        public string Editar(int id, T registro)
        {
            string EhPossivelEditar = registro.Validar();

            if (EhPossivelEditar == "ESTA_VALIDO")
            {
                for (int i = 0; i < registros.Count; i++)
                {
                    if (registros[i].id == id)
                    {
                        registros[i] = registro;
                        registros[i].id = id;
                        break;
                    }
                }
            }
            return EhPossivelEditar;
        }

        public bool Excluir(int id, T Registro)
        {
            bool conseguiuExcluir = false;

            foreach (T item in registros)
            {
                if(id == item.id)
                {
                    registros.Remove(item);
                    conseguiuExcluir = true;
                    break;
                }
            }
            return conseguiuExcluir;
        }

        public List<T> SelecionarTodosRegistros()
        {
            return registros;
        }

        public T SelecionarRegistroPorId(int id)
        {
            T selecionado = default(T);

            foreach (T item in registros)
            {
                if(item.id == id)
                {
                    selecionado = item;
                }
            }
            return selecionado;
        }


        protected int NovoId()
        {
            return ++ultimoId;
        }
    }
}