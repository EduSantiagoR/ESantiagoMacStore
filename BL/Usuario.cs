using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    var query = context.UsuarioGetAll().ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = item.IdUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.ApellidoPaterno = item.ApellidoPaterno;
                            usuario.ApellidoMaterno = item.ApellidoMaterno;
                            usuario.Edad = item.Edad.Value;
                            usuario.Email = item.Email;
                            usuario.Password = item.Password;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar los usuarios.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    var query = context.UsuarioGetById(idUsuario).AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Edad = query.Edad.Value;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar el usuario.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    int rowsAffected = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Edad, usuario.Email, usuario.Password);
                    if(rowsAffected > 0 )
                    {
                        result.Correct = true;
                        result.Message = "Usuario agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el usuario.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    int rowsAffected = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Edad, usuario.Email, usuario.Password);
                    if(rowsAffected > 0 )
                    {
                        result.Correct = true;
                        result.Message = "Usuario actualizado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar el usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    int rowsAffected = context.UsuarioDelete(idUsuario);
                    if( rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Login(string email, string password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMacStoreEntities context = new DL.ESantiagoMacStoreEntities())
                {
                    var query = context.UsuarioLogin(email, password).AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;

                        result.Object = usuario;
                        result.Correct = true;
                        result.Message = "Acceso concedido.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Acceso denegado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
