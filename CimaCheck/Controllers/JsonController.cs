using CimaCheck.Models;
using CimaCheck.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace CimaCheck.Controllers
{
    /// <summary>
    /// This Class Contains the methods to get the data from the database
    /// and create tables to administrate the data locally
    /// </summary>
    public class JsonController
    {

        public static async void UpdateAllJsons()
        {
            UpdateAlumnosJson();
            UpdateSchoolsJson();
            UpdateCareerJson();
            UpdateFacultiesJson();
            UpdateOrgJson();
            MessageBox.Show($"datos cargados"); 
        }


        /// <summary>
        /// Serializes the list of students to a JSON file and displays status messages to the user.
        /// </summary>
        /// <remarks>This method retrieves the list of students asynchronously, serializes the data to a
        /// file named "Alumnos.json" in the application's working directory, and displays message boxes to indicate
        /// progress or errors. The method is asynchronous but returns void, so exceptions may not be propagated to the
        /// caller. Use caution when calling from non-UI threads.</remarks>
        public static async void UpdateAlumnosJson()
        {
            try
            {
                List<Alumno> lsAlumnos = await DataManager.ConseguirListaAlumnos();

                string json = JsonSerializer.Serialize(lsAlumnos, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("Alumnos.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron guardar los datos en json{ex}");
            }
        }

        /// <summary>
        /// Serializes the list of schools to a JSON file named "Alumnos.json" asynchronously.
        /// </summary>
        /// <remarks>If an error occurs during the serialization or file writing process, a message box is
        /// displayed with the error details. This method is asynchronous but returns void, so exceptions cannot be
        /// awaited or caught by the caller.</remarks>
        public static async void UpdateSchoolsJson()
        {
            try
            {
                List<Escuela> lsEscuelas = await DataManager.ConseguirEscuelas();

                string json = JsonSerializer.Serialize(lsEscuelas, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("Escuelas.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron guardar los datos en el json {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves the list of careers and writes it to a JSON file named "Carreras.json".
        /// </summary>
        /// <remarks>This method displays a message box if an error occurs while saving the data. Because
        /// this method is asynchronous and returns void, exceptions cannot be awaited or caught by the caller. Use
        /// caution when calling from non-UI code.</remarks>
        public static async void UpdateCareerJson()
        {
            try
            {
                List<Carrera> lsCarreras = await DataManager.ConseguirCarreras();

                string json = JsonSerializer.Serialize(lsCarreras, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("Carreras.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron guardar los datos en el json {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves the list of faculties and updates the "Facultades.json" file with the latest data
        /// in JSON format.
        /// </summary>
        /// <remarks>This method displays a message box if an error occurs while retrieving or saving the
        /// data. Because the method is asynchronous and returns void, exceptions cannot be awaited or caught by the
        /// caller. Use with caution in production code, as this pattern is generally recommended only for event
        /// handlers.</remarks>
        public static async void UpdateFacultiesJson()
        {
            try
            {
                List<Facultad> lsFacultades = await DataManager.ConseguirFacultades();
                String json = JsonSerializer.Serialize(lsFacultades, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText("Facultades.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron guardar los datos en el json {ex.Message}");
            }
        }


        public static async void UpdateOrgJson() 
        {
            try 
            {
                List<Company> lsOrg = await DataManager.ConseguirOrg();
                String json = JsonSerializer.Serialize(lsOrg, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText("Organizaciones.json", json);

                MessageBox.Show($"Organizaciones actualizadas");
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron guardar los datos en el json {ex.Message}");
            }
        }


        /// <summary>
        /// Asynchronously deserializes a JSON file into a list of objects of type T.
        /// </summary>
        /// <remarks>If the file does not exist, is empty, or deserialization fails, a message box is
        /// displayed and an empty list is returned. This method is intended for use in applications with a user
        /// interface, as it displays error messages using message boxes.</remarks>
        /// <typeparam name="T">The type of objects to deserialize from the JSON file.</typeparam>
        /// <param name="fileName">The path to the JSON file to read and deserialize. Cannot be null or empty.</param>
        /// <returns>A list of objects of type T deserialized from the JSON file. Returns an empty list if the file does not
        /// exist, is empty, or cannot be deserialized.</returns>
        public static async Task<List<T>> JsonDeserializer<T>(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show($"No existe el archivo: {fileName}");
                    return new List<T>();
                }

                string jsonString = await File.ReadAllTextAsync(fileName);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return new List<T>();
                }

                List<T>? lista = JsonSerializer.Deserialize<List<T>>(jsonString);

                if (lista == null)
                {
                    MessageBox.Show("No se pudieron cargar los datos del json");
                    return new List<T>();
                }
                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar {fileName}: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
