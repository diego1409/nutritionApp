﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;

//Clases utilizadas
using nutritionApp.Clases;

namespace nutritionApp
{
    public class ManejoDatos
    {
        private List<Usuario> lista_usuarios;

        public List<Usuario> _lista_usuarios
        {
            get
            {
                return lista_usuarios;
            }
            set
            {
                lista_usuarios = value;
            }
        }

        public List<Usuario> ListaUsuarios(int idUsuario)
        {
            List<Usuario> lista = new List<Usuario>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC ListaUsuarios ?";
            conec.annadir_consulta(consulta);
            conec.annadir_parametro(idUsuario, 1);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                Usuario tmp = new Usuario();
                tmp._Cedula = contenedor["cedula"].ToString();
                tmp._Genero = contenedor["genero"].ToString();
                tmp._FechaNac = Convert.ToDateTime(contenedor["fechaNac"]);
                tmp._Nombre = contenedor["nombre"].ToString();
                tmp._Apellido1 = contenedor["apellido1"].ToString();
                tmp._Apellido2 = contenedor["apellido2"].ToString();
                tmp._Direccion = contenedor["direccion"].ToString();
                tmp._Telefono1 = contenedor["telefono1"].ToString();
                tmp._Estatura = Convert.ToInt16(contenedor["estatura"]);
                tmp._Peso = Convert.ToDecimal(contenedor["peso"]);
                tmp._Proposito = contenedor["proposito"].ToString();
                tmp._Correo = contenedor["correo"].ToString();
                tmp._NomUsuario = contenedor["usuario"].ToString();
                tmp._Contrasena = contenedor["contrasena"].ToString();
                tmp._TipoUsuario = contenedor["tipoUsuario"].ToString();
                lista.Add(tmp);
            }
            return lista;
        }

        public List<ingrediente> ListaIngredientes()
        {
            List<ingrediente> lista = new List<ingrediente>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Select * from ingrediente order by nombre";
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                ingrediente tmp = new ingrediente();
                tmp._IdIngrediente = Convert.ToInt16(contenedor["idIngrediente"]);
                tmp._Nombre = contenedor["nombre"].ToString();
                lista.Add(tmp);
            }
            return lista;
        }

        public List<ingrediente_receta> ListaIngredientesReceta(int idReceta)
        {
            List<ingrediente_receta> lista = new List<ingrediente_receta>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Select ingrediente_receta.*, ingrediente.nombre as nombre_ingrediente from ingrediente_receta inner join ingrediente on ingrediente_receta.idIngrediente = ingrediente.idIngrediente where idReceta="+ idReceta;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                ingrediente_receta tmp = new ingrediente_receta();
                tmp._IdReceta = Convert.ToInt16(contenedor["idReceta"]);
                tmp._IdIngrediente = Convert.ToInt16(contenedor["idIngrediente"]);
                tmp._Cantidad = Convert.ToDecimal(contenedor["cantidad"].ToString());
                tmp._Medida = contenedor["medida"].ToString();
                tmp._Observaciones = contenedor["observaciones"].ToString();
                tmp._Nombre_ingrediente = contenedor["nombre_ingrediente"].ToString();
                lista.Add(tmp);
            }
            return lista;
        }

        //Retorna un unico valor
        public List<ingrediente_receta> RetornaIngredienteReceta(int idIngrediente, int idReceta)
        {
            List<ingrediente_receta> lista = new List<ingrediente_receta>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Select ingrediente_receta.*, ingrediente.nombre as nombre_ingrediente from ingrediente_receta inner join ingrediente on ingrediente_receta.idIngrediente = ingrediente.idIngrediente where ingrediente_receta.idReceta=" + idReceta+ " and ingrediente_receta.idIngrediente=" + idIngrediente;
            //consulta = "Select ingrediente_receta.*, ingrediente.nombre as nombre_ingrediente from ingrediente_receta inner join ingrediente on ingrediente_receta.idIngrediente = ingrediente.idIngrediente where idReceta=" + idReceta+" and idIngrediente="+ idIngrediente;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                ingrediente_receta tmp = new ingrediente_receta();
                tmp._IdReceta = Convert.ToInt16(contenedor["idReceta"]);
                tmp._IdIngrediente = Convert.ToInt16(contenedor["idIngrediente"]);
                tmp._Cantidad = Convert.ToDecimal(contenedor["cantidad"].ToString());
                tmp._Medida = contenedor["medida"].ToString();
                tmp._Observaciones = contenedor["observaciones"].ToString();
                tmp._Nombre_ingrediente = contenedor["nombre_ingrediente"].ToString();
                lista.Add(tmp);
            }
            return lista;
        }

        public List<receta> ListaRecetas()
        {
            List<receta> lista = new List<receta>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "exec ListaRecetas";
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                receta tmp = new receta();
                tmp._idReceta = Convert.ToInt32(contenedor["idReceta"]);
                tmp._Nombre = contenedor["nombre"].ToString();
                tmp._Tiempo = Convert.ToInt32(contenedor["tiempo"]);
                tmp._TiempoComida = contenedor["tiempoComida"].ToString();
                lista.Add(tmp);
            }
            return lista;
        }

        /* == Funcion para Iniciar sesion == */
        public int RetornaUsuarioPass(Usuario user)
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            int idUsuario = 0;

            //Se crea la consulta
            consulta = "EXEC RetornaUsuarioPass ?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(user._NomUsuario, 2);
            conect_local.annadir_parametro(user._Contrasena, 2);

            //Se procede a buscar
            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
                idUsuario = Convert.ToInt32(contenedor["idUsuario"].ToString());
            }
            contenedor.Close();

            //Se retorna el idUsuario para buscarlo y obtener los datos de usuario
            return idUsuario;
        }

        public List<Usuario> RetornaUsuarioCedula(Usuario usuario)
        {
            List<Usuario> usuarioDevolver = new List<Usuario>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Select * from usuario where cedula=" + usuario._Cedula;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                Usuario tmp = new Usuario();
                tmp._Cedula = contenedor["cedula"].ToString();
                tmp._Genero = contenedor["genero"].ToString();
                tmp._FechaNac = Convert.ToDateTime(contenedor["fechaNac"]);
                tmp._Nombre = contenedor["nombre"].ToString();
                tmp._Apellido1 = contenedor["apellido1"].ToString();
                tmp._Apellido2 = contenedor["apellido2"].ToString();
                tmp._Direccion = contenedor["direccion"].ToString();
                tmp._Telefono1 = contenedor["telefono1"].ToString();
                tmp._Estatura = Convert.ToInt16(contenedor["estatura"]);
                tmp._Peso = Convert.ToDecimal(contenedor["peso"]);
                tmp._Proposito = contenedor["proposito"].ToString();
                tmp._Correo = contenedor["correo"].ToString();
                tmp._NomUsuario = contenedor["usuario"].ToString();
                tmp._Contrasena = contenedor["contrasena"].ToString();
                tmp._TipoUsuario = contenedor["tipoUsuario"].ToString();
                usuarioDevolver.Add(tmp);
            }
            return usuarioDevolver;
        }

        public List<receta> RetornaRecetaID(receta Receta)
        {
            List<receta> usuarioDevolver = new List<receta>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Select * from receta where idReceta=" + Receta._idReceta;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                receta tmp = new receta();
                tmp._idReceta = Convert.ToInt16(contenedor["idReceta"]);
                tmp._Nombre = contenedor["nombre"].ToString();
                tmp._Dificultad = Convert.ToChar(contenedor["dificultad"]);
                tmp._Tiempo = Convert.ToInt16(contenedor["tiempo"].ToString());
                tmp._TiempoComida = contenedor["tiempoComida"].ToString();
                tmp._Carbos = Convert.ToDecimal(contenedor["carbos"]);
                tmp._Proteinas = Convert.ToDecimal(contenedor["proteinas"]);
                tmp._Grasas = Convert.ToDecimal(contenedor["grasas"]);
                tmp._Azucares = Convert.ToDecimal(contenedor["azucares"]);
                tmp._Calorias = Convert.ToInt16(contenedor["calorias"]);
                tmp._Pasos= contenedor["pasos"].ToString();

                usuarioDevolver.Add(tmp);
            }
            return usuarioDevolver;
        }

        public void DarDeBajaUsuario(Usuario usuario)
        {
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "Update usuario set tipoUsuario='B' where cedula=" + usuario._Cedula;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
            }
        }

        public void EliminarIngredienteReceta(int idIngrediente, int idReceta)
        {
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "delete from ingrediente_receta where idIngrediente=" + idIngrediente+ " and idReceta="+ idReceta;
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
            }
        }

        /* == Funcion para retornar un usuario == */
        public OleDbDataReader RetornaUsuario(int idUsuario)
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;

            //Se crea la consulta
            consulta = "EXEC RetornaUsuario ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idUsuario, 1);

            //Se procede a buscar
            contenedor = conect_local.busca();

            //Se retorna el contenedor para usar todos sus datos
            return contenedor;
        }

        /* == Funcion para almacenar un usuario == */
        public Usuario almacenarDatosUsuario(OleDbDataReader datos, Usuario user)
        {
            while (datos.Read())
            {
                //Se almacenan los datos de usuario
                user._Foto = Convert.FromBase64String(datos["foto"].ToString());
                user._Cedula = Convert.ToString(datos["cedula"].ToString());
                user._Genero = Convert.ToString(datos["genero"].ToString());
                user._FechaNac = Convert.ToDateTime(datos["fechaNac"].ToString());
                user._Nombre = Convert.ToString(datos["nombre"].ToString());
                user._Apellido1 = Convert.ToString(datos["apellido1"].ToString());
                user._Apellido2 = Convert.ToString(datos["apellido2"].ToString());
                user._Direccion = Convert.ToString(datos["direccion"].ToString());
                user._Telefono1 = Convert.ToString(datos["telefono1"].ToString());
                user._Estatura = Convert.ToInt32(datos["estatura"].ToString());
                user._Peso = Convert.ToDecimal(datos["peso"].ToString());
                user._Proposito = Convert.ToString(datos["proposito"].ToString());
                user._TipoUsuario = Convert.ToString(datos["tipoUsuario"].ToString());
            }

            //Se agrega el usuario como variable de sesion
            //this.Session.Add("Usuario", user);

            return user;
        }

        /* == Funcion para insertar un usuario comun y corriente == */
        public bool insertar_usuario(Usuario insertar)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaUsuario ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(insertar._Cedula, 2);
            conect_local.annadir_parametro(insertar._Genero, 2);
            conect_local.annadir_parametro(insertar._FechaNac, 4);
            conect_local.annadir_parametro(insertar._Nombre, 2);
            conect_local.annadir_parametro(insertar._Apellido1, 2);
            conect_local.annadir_parametro(insertar._Apellido2, 2);
            conect_local.annadir_parametro(insertar._Direccion, 2);
            conect_local.annadir_parametro(insertar._Telefono1, 2);
            conect_local.annadir_parametro(insertar._Estatura, 1);
            conect_local.annadir_parametro(insertar._Peso, 3);
            conect_local.annadir_parametro(insertar._Proposito, 2);
            conect_local.annadir_parametro(insertar._Correo, 2);
            conect_local.annadir_parametro(insertar._NomUsuario, 2);
            conect_local.annadir_parametro(insertar._Contrasena, 2);
            conect_local.annadir_parametro(insertar._TipoUsuario, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool insertar_receta(receta insertar_receta)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaReceta ?,?,?,?,?,?,?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(insertar_receta._Foto, 2);
            conect_local.annadir_parametro(insertar_receta._Nombre, 2);
            conect_local.annadir_parametro(insertar_receta._Dificultad, 2);
            conect_local.annadir_parametro(insertar_receta._Tiempo, 1);
            conect_local.annadir_parametro(insertar_receta._TiempoComida, 2);
            conect_local.annadir_parametro(insertar_receta._Carbos, 3);
            conect_local.annadir_parametro(insertar_receta._Proteinas, 3);
            conect_local.annadir_parametro(insertar_receta._Grasas, 3);
            conect_local.annadir_parametro(insertar_receta._Azucares, 3);
            conect_local.annadir_parametro(insertar_receta._Calorias, 1);
            conect_local.annadir_parametro("", 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool insertar_ingrediente_receta(ingrediente_receta insertar_ingrediente_receta)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaIngredienteReceta ?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(insertar_ingrediente_receta._IdIngrediente, 1);
            conect_local.annadir_parametro(insertar_ingrediente_receta._Cantidad, 3);
            conect_local.annadir_parametro(insertar_ingrediente_receta._Medida, 2);
            conect_local.annadir_parametro(insertar_ingrediente_receta._Observaciones, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool insertar_ingrediente(string ingrediente)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaIngrediente ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(ingrediente, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool modificar_usuario(Usuario modificar)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC ModificaUsuario ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(modificar._Cedula, 2);
            conect_local.annadir_parametro(modificar._Genero, 2);
            conect_local.annadir_parametro(modificar._FechaNac, 4);
            conect_local.annadir_parametro(modificar._Nombre, 2);
            conect_local.annadir_parametro(modificar._Apellido1, 2);
            conect_local.annadir_parametro(modificar._Apellido2, 2);
            conect_local.annadir_parametro(modificar._Direccion, 2);
            conect_local.annadir_parametro(modificar._Telefono1, 2);
            conect_local.annadir_parametro(modificar._Estatura, 1);
            conect_local.annadir_parametro(modificar._Peso, 3);
            conect_local.annadir_parametro(modificar._Proposito, 2);
            conect_local.annadir_parametro(modificar._Correo, 2);
            conect_local.annadir_parametro(modificar._NomUsuario, 2);
            conect_local.annadir_parametro(modificar._Contrasena, 2);
            conect_local.annadir_parametro(modificar._TipoUsuario, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool modificar_receta(receta modificar)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC ModificaReceta ?,?,?,?,?,?,?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(modificar._idReceta, 1);
            conect_local.annadir_parametro(modificar._Nombre, 2);
            conect_local.annadir_parametro(modificar._Dificultad, 2);
            conect_local.annadir_parametro(modificar._Tiempo, 1);
            conect_local.annadir_parametro(modificar._TiempoComida, 2);
            conect_local.annadir_parametro(modificar._Carbos, 3);
            conect_local.annadir_parametro(modificar._Proteinas, 3);
            conect_local.annadir_parametro(modificar._Grasas, 3);
            conect_local.annadir_parametro(modificar._Azucares, 3);
            conect_local.annadir_parametro(modificar._Calorias, 1);
            conect_local.annadir_parametro(modificar._Pasos, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool modificar_ingrediente_receta(ingrediente_receta modificar)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC ModificaIngredienteReceta ?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(modificar._IdIngrediente, 1);
            conect_local.annadir_parametro(modificar._IdReceta, 1);
            conect_local.annadir_parametro(modificar._Cantidad, 3);
            conect_local.annadir_parametro(modificar._Medida, 2);
            conect_local.annadir_parametro(modificar._Observaciones, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        public bool inserta_pasos_receta(string pasos)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaPasosReceta ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(pasos, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para retornar todos los ingredientes == */
        public OleDbDataReader RetornaIngrediente()
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;

            //Se crea la consulta
            consulta = "EXEC RetornaIngredientes";
            conect_local.annadir_consulta(consulta);

            //Se procede a buscar
            contenedor = conect_local.busca();

            //Se retorna el contenedor para usar todos sus datos
            return contenedor;
        }

        /* == Funcion para insertar una alergia == */
        public bool InsertaAlergia(int idUsuario, int idIngrediente)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaAlergia ?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idUsuario, 1);
            conect_local.annadir_parametro(idIngrediente, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para retornar todas los alergias == */
        public OleDbDataReader RetornaAlergias(int idUsuario)
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
           
            //Se crea la consulta
            consulta = "EXEC RetornaAlergias ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idUsuario, 1);
            //Se procede a buscar
            contenedor = conect_local.busca();

            //Se retorna el contenedor para usar todos sus datos
            return contenedor;
        }

        /* == Funcion para eliminar una alergia == */
        public bool EliminaAlergia(int idUsuario, int idIngrediente)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC EliminaAlergia ?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idUsuario, 1);
            conect_local.annadir_parametro(idIngrediente, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para obtener la lista de planes == */
        public List<planNutricional> ListaPlanes(int idUsuario)
        {
            List<planNutricional> lista = new List<planNutricional>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "exec ListaPlanes ?";
            conec.annadir_consulta(consulta);
            conec.annadir_parametro(idUsuario, 1);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                planNutricional tmp = new planNutricional();
                tmp.idPlan = Convert.ToInt32(contenedor["idPlan"]);
                tmp.fecha = Convert.ToDateTime(contenedor["fecha"]);
                tmp.carbos = contenedor["carbos"].ToString();
                tmp.proteinas = contenedor["proteinas"].ToString();
                tmp.grasas = contenedor["grasas"].ToString();
                tmp.azucares = contenedor["azucares"].ToString();
                tmp.calorias = Convert.ToInt32(contenedor["calorias"]);
                lista.Add(tmp);
            }
            return lista;
        }

        /* == Funcion para insertar un plan nutricional == */
        public bool InsertaPlanNutricional(planNutricional plan)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaPlanNutricional ?,?,?,?,?,?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(plan.idUsuario, 1);
            conect_local.annadir_parametro(plan.carbos, 2);
            conect_local.annadir_parametro(plan.proteinas, 2);
            conect_local.annadir_parametro(plan.grasas, 2);
            conect_local.annadir_parametro(plan.azucares, 2);
            conect_local.annadir_parametro(plan.calorias, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para retornar un plan nutricional == */
        public planNutricional RetornaPlanNutricional(int idPlan)
        {
            planNutricional plan = new planNutricional();
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC RetornaPlanNutricional ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idPlan, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
                plan.idPlan = Convert.ToInt32(contenedor["idPlan"]);
                plan.idUsuario = Convert.ToInt32(contenedor["idUsuario"]);
                plan.fecha = Convert.ToDateTime(contenedor["fecha"]);
                plan.carbos = Convert.ToString(contenedor["carbos"]);
                plan.proteinas = Convert.ToString(contenedor["proteinas"]);
                plan.grasas = Convert.ToString(contenedor["grasas"]);
                plan.azucares = Convert.ToString(contenedor["azucares"]);
                plan.cantComidas = Convert.ToInt32(contenedor["cantComidas"]);
                plan.calorias = Convert.ToInt32(contenedor["calorias"]);
            }
            contenedor.Close();
            return plan;
        }

        /* == Funcion para retornar el ultimo plan nutricional creado == */
        public int RetornaUltimoPlan(int idUsuario)
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            int ultimoPlan = 0;

            //Se crea la consulta
            consulta = "Select max(idPlan) as ultimoPlan from planNutricional where idUsuario = " + idUsuario.ToString();
            conect_local.annadir_consulta(consulta);

            //Se procede a buscar
            contenedor = conect_local.busca();
            if (contenedor.HasRows)
            {
                while (contenedor.Read())
                {
                    if (!(contenedor["ultimoPlan"] is DBNull))
                    {
                        ultimoPlan = Convert.ToInt32(contenedor["ultimoPlan"]);
                    }
                }
            }
            
            contenedor.Close();

            //Se retorna el idUsuario para buscarlo y obtener los datos de usuario
            return ultimoPlan;
        }

        /* == Funcion para insertar las comidas para el plan == */
        public bool InsertaComidasPlan(int idUsuario, int idPlan, double caloriasComida, int carbos, double proteinas, double grasas, int azucares)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaComidasPlan ?, ?, ?, ?, ?, ?, ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idUsuario, 1);
            conect_local.annadir_parametro(idPlan, 1);
            conect_local.annadir_parametro(caloriasComida, 3);
            conect_local.annadir_parametro(carbos, 1);
            conect_local.annadir_parametro(proteinas, 3);
            conect_local.annadir_parametro(grasas, 3);
            conect_local.annadir_parametro(azucares, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para insertar una comida para el plan == */
        public bool InsertaComida(int idPlan, string tiempoComida, int idReceta)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaComida ?, ?, ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idPlan, 1);
            conect_local.annadir_parametro(tiempoComida, 2);
            conect_local.annadir_parametro(idReceta, 1);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }

        /* == Funcion para retornar una comida para el plan == */
        public receta RetornaComida(int idPlan, string tiempoComida)
        {
            receta receta = new receta();
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC RetornaComida ?, ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(idPlan, 1);
            conect_local.annadir_parametro(tiempoComida, 2);

            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
                receta._idReceta = Convert.ToInt32(contenedor["idReceta"]);
                receta._Nombre = contenedor["nombre"].ToString();
                receta._Dificultad = Convert.ToChar(contenedor["dificultad"]);
                receta._Tiempo = Convert.ToInt32(contenedor["tiempo"]);
                receta._TiempoComida = Convert.ToString(contenedor["tiempoComida"]);
                receta._Carbos = Convert.ToDecimal(contenedor["carbos"]);
                receta._Proteinas = Convert.ToDecimal(contenedor["proteinas"]);
                receta._Grasas = Convert.ToDecimal(contenedor["grasas"]);
                receta._Azucares = Convert.ToDecimal(contenedor["azucares"]);
                receta._Calorias = Convert.ToInt32(contenedor["calorias"]);
                receta._Pasos = Convert.ToString(contenedor["pasos"]);
            }
            contenedor.Close();
            return receta;
        }

        public List<Medicion> RetornaUltimaMedicion(Medicion medicion)
        {
            List<Medicion> medicionDevolver = new List<Medicion>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "select top 1 *,usuario.estatura as estatura from medicion inner join usuario on medicion.idUsuario = usuario.idUsuario where medicion.idUsuario =" + medicion._IdUsuario + "order by idMedicion desc";
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();

            if (contenedor.HasRows)
            {
                while (contenedor.Read())
                {
                    Medicion tmp = new Medicion();

                    tmp._IdUsuario = Convert.ToInt16(contenedor["IdUsuario"]);
                    tmp._Peso = Convert.ToDecimal(contenedor["peso"]);
                    tmp._Grasa = Convert.ToDecimal(contenedor["grasa"]);
                    tmp._Musculo = Convert.ToDecimal(contenedor["musculo"]);
                    tmp._Agua = Convert.ToDecimal(contenedor["agua"]);
                    tmp._Hueso = Convert.ToDecimal(contenedor["hueso"]);
                    tmp._Observaciones = contenedor["observaciones"].ToString();
                    tmp._Imc = Convert.ToDecimal(contenedor["imc"]);
                    tmp._Fecha = Convert.ToDateTime(contenedor["fecha"]);
                    tmp._Estatura = Convert.ToInt16(contenedor["estatura"]);

                    medicionDevolver.Add(tmp);
                }
            }
            
            return medicionDevolver;
        }

        public List<Medicion> HistorialMedidas(int idUsuario)
        {
            List<Medicion> medicionDevolver = new List<Medicion>();
            Conexion conec = new Conexion();
            conec.parametro("", "", "", "");
            conec.inicializa();
            string consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "select * from medicion where idUsuario="+ idUsuario + " order by idMedicion desc";
            conec.annadir_consulta(consulta);
            contenedor = conec.busca();
            while (contenedor.Read())
            {
                Medicion tmp = new Medicion();

                tmp._Peso = Convert.ToDecimal(contenedor["peso"]);
                tmp._Grasa = Convert.ToDecimal(contenedor["grasa"]);
                tmp._Musculo = Convert.ToDecimal(contenedor["musculo"]);
                tmp._Agua = Convert.ToDecimal(contenedor["agua"]);
                tmp._Hueso = Convert.ToDecimal(contenedor["hueso"]);
                tmp._Observaciones = contenedor["observaciones"].ToString();
                tmp._Imc = Convert.ToDecimal(contenedor["imc"]);
                tmp._Fecha = Convert.ToDateTime(contenedor["fecha"]);

                medicionDevolver.Add(tmp);
            }
            return medicionDevolver;
        }

        public int RetornaUltimaReceta()
        {
            //Declaracion de variables
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            int ultimaReceta = 0;

            //Se crea la consulta
            consulta = "Select max(idReceta) as ultimaReceta from receta";
            conect_local.annadir_consulta(consulta);

            //Se procede a buscar
            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
                ultimaReceta = Convert.ToInt32(contenedor["ultimaReceta"].ToString());
            }
            contenedor.Close();

            //Se retorna el idUsuario para buscarlo y obtener los datos de usuario
            return ultimaReceta;
        }

        public bool insertar_medicion(Medicion medicion)
        {
            Conexion conect_local = new Conexion();
            conect_local.parametro("", "", "", "");
            conect_local.inicializa();
            String consulta;
            System.Data.OleDb.OleDbDataReader contenedor;
            consulta = "EXEC InsertaMedicion ?, ?, ?, ?, ?, ?, ?";
            conect_local.annadir_consulta(consulta);
            conect_local.annadir_parametro(medicion._IdUsuario, 1);
            conect_local.annadir_parametro(medicion._Peso, 3);
            conect_local.annadir_parametro(medicion._Grasa, 3);
            conect_local.annadir_parametro(medicion._Musculo, 3);
            conect_local.annadir_parametro(medicion._Agua, 3);
            conect_local.annadir_parametro(medicion._Hueso, 3);
            conect_local.annadir_parametro(medicion._Observaciones, 2);

            //conect_local.annadir_parametro(medicion._IdUsuario, 1);
            //conect_local.annadir_parametro(medicion._Peso, 3);
            //conect_local.annadir_parametro(medicion._Grasa, 3);
            //conect_local.annadir_parametro(medicion._Musculo, 3);
            //conect_local.annadir_parametro(medicion._Agua, 3);
            //conect_local.annadir_parametro(medicion._Hueso, 3);
            //conect_local.annadir_parametro(medicion._Observaciones, 2);


            contenedor = conect_local.busca();
            while (contenedor.Read())
            {
            }
            contenedor.Close();
            return true;
        }
    }
}