using System;

public class Class1
{
	public Class1()
	{

        using System;
        using System.Collections.Generic;
        using System.IO;
        using System.Linq;
        using System.Text.Json;

namespace AICodeWars
{
    public class EventoHistorial
    {
        public DateTime FechaHora { get; set; }
        public string Tipo { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }

    public class Expediente
    {
        public string Id { get; set; } = "";
        public string Solicitante { get; set; } = "";
        public string Asunto { get; set; } = "";
        public string Responsable { get; set; } = "";
        public string? Categoria { get; set; }
        public DateTime FechaApertura { get; set; }
        public string Estado { get; set; } = "Abierto";

        public List<EventoHistorial> Historial { get; set; } = new();
    }

    public class OrganizadorExpedientes
    {
        private readonly string rutaArchivo;
        private readonly object bloqueo = new();
        private readonly List<Expediente> expedientes;

        public OrganizadorExpedientes(string rutaArchivo = "expedientes.json")
        {
            this.rutaArchivo = Path.GetFullPath(rutaArchivo);
            expedientes = Cargar();
        }

        // Recuperar expedientes previamente almacenados
        private List<Expediente> Cargar()
        {
            if (!File.Exists(rutaArchivo))
                return new List<Expediente>();

            string json = File.ReadAllText(rutaArchivo);

            return JsonSerializer.Deserialize<List<Expediente>>(json)
                   ?? new List<Expediente>();
        }

        // Guardar todos los cambios
        private void Guardar()
        {
            string? carpeta = Path.GetDirectoryName(rutaArchivo);

            if (!string.IsNullOrEmpty(carpeta))
                Directory.CreateDirectory(carpeta);

            string json = JsonSerializer.Serialize(
                expedientes,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(rutaArchivo, json);
        }

        private static string Validar(string? valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException(
                    $"El campo {campo} es obligatorio.");

            return valor.Trim();
        }

        // 1. Crear expediente
        public Expediente CrearExpediente(
            string solicitante,
            string asunto,
            string responsable,
            string? categoria = null)
        {
            solicitante = Validar(solicitante, "Solicitante");
            asunto = Validar(asunto, "Asunto");
            responsable = Validar(responsable, "Responsable");

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                categoria = categoria.Trim();

                if (!new[] { "Civil", "Familiar", "Laboral" }
                    .Contains(categoria, StringComparer.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Categoría no válida.");
                }
            }
            else
            {
                categoria = null;
            }

            lock (bloqueo)
            {
                DateTime ahora = DateTime.Now;

                Expediente nuevo = new Expediente
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Solicitante = solicitante,
                    Asunto = asunto,
                    Responsable = responsable,
                    Categoria = categoria,
                    FechaApertura = ahora,
                    Estado = "Abierto",
                    Historial = new List<EventoHistorial>
                    {
                        new EventoHistorial
                        {
                            FechaHora = ahora,
                            Tipo = "Apertura",
                            Descripcion = "Expediente creado en estado Abierto."
                        }
                    }
                };

                expedientes.Add(nuevo);
                Guardar();

                return Copiar(nuevo);
            }
        }

        // 2. Consultar todos los expedientes
        public List<Expediente> ListarExpedientes()
        {
            lock (bloqueo)
            {
                return expedientes
                    .OrderByDescending(e => e.FechaApertura)
                    .Select(Copiar)
                    .ToList();
            }
        }

        // 3. Buscar por identificador o nombre/alias
        public List<Expediente> BuscarExpedientes(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return ListarExpedientes();

            texto = texto.Trim();

            lock (bloqueo)
            {
                return expedientes
                    .Where(e =>
                        e.Id.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                        e.Solicitante.Contains(texto, StringComparison.OrdinalIgnoreCase))
                    .Select(Copiar)
                    .ToList();
            }
        }

        // 4. Consultar detalle e historial
        public Expediente? ObtenerExpediente(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            lock (bloqueo)
            {
                var encontrado = expedientes.FirstOrDefault(
                    e => e.Id.Equals(id.Trim(),
                        StringComparison.OrdinalIgnoreCase));

                return encontrado == null ? null : Copiar(encontrado);
            }
        }

        // 5. Generar copia para proteger datos internos
        private static Expediente Copiar(Expediente original)
        {
            return new Expediente
            {
                Id = original.Id,
                Solicitante = original.Solicitante,
                Asunto = original.Asunto,
                Responsable = original.Responsable,
                Categoria = original.Categoria,
                FechaApertura = original.FechaApertura,
                Estado = original.Estado,
                Historial = original.Historial
                    .Select(h => new EventoHistorial
                    {
                        FechaHora = h.FechaHora,
                        Tipo = h.Tipo,
                        Descripcion = h.Descripcion
                    })
                    .ToList()
            };
        }
    }
}

	}
}
