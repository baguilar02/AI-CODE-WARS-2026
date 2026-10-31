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

        private List<Expediente> Cargar()
        {
            if (!File.Exists(rutaArchivo))
                return new List<Expediente>();

            string json = File.ReadAllText(rutaArchivo);
            return JsonSerializer.Deserialize<List<Expediente>>(json)
                ?? new List<Expediente>();
        }

        private void Guardar()
        {
            string? carpeta = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(carpeta))
                Directory.CreateDirectory(carpeta);

            string json = JsonSerializer.Serialize(
                expedientes,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(rutaArchivo, json);
        }

        private static string Validar(string? valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException($"El campo {campo} es obligatorio.");

            return valor.Trim();
        }

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
                    throw new ArgumentException("Categoría no válida.");
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

        public Expediente? ObtenerExpediente(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            lock (bloqueo)
            {
                var encontrado = expedientes.FirstOrDefault(
                    e => e.Id.Equals(id.Trim(), StringComparison.OrdinalIgnoreCase));
                return encontrado == null ? null : Copiar(encontrado);
            }
        }

        // Cambia únicamente de Abierto a En seguimiento.
        public void IniciarSeguimiento(string id)
        {
            lock (bloqueo)
            {
                var e = BuscarInterno(id);
                if (e.Estado != "Abierto")
                    throw new InvalidOperationException("Solo un expediente abierto puede pasar a seguimiento.");
                e.Estado = "En seguimiento";
                e.Historial.Add(new EventoHistorial
                {
                    FechaHora = DateTime.Now,
                    Tipo = "Cambio de estado",
                    Descripcion = "Abierto → En seguimiento"
                });
                Guardar();
            }
        }

        public void AgregarActividad(string id, string descripcion)
        {
            descripcion = Validar(descripcion, "Descripción de actividad");
            lock (bloqueo)
            {
                var e = BuscarInterno(id);
                if (e.Estado == "Cerrado")
                    throw new InvalidOperationException("Un expediente cerrado no admite actividades.");
                e.Historial.Add(new EventoHistorial
                {
                    FechaHora = DateTime.Now,
                    Tipo = "Actividad",
                    Descripcion = descripcion
                });
                Guardar();
            }
        }

        public void CerrarExpediente(string id, string motivo)
        {
            motivo = Validar(motivo, "Motivo de cierre");
            lock (bloqueo)
            {
                var e = BuscarInterno(id);
                if (e.Estado != "En seguimiento")
                    throw new InvalidOperationException("Solo se puede cerrar un expediente en seguimiento.");

                // Para cierres posteriores a una reapertura, exigir actividad nueva.
                int ultimaReapertura = e.Historial.FindLastIndex(h => h.Tipo == "Reapertura");
                bool existeActividad = e.Historial.Skip(ultimaReapertura + 1)
                    .Any(h => h.Tipo == "Actividad");
                if (!existeActividad)
                    throw new InvalidOperationException("Debe registrarse al menos una actividad antes del cierre.");

                e.Estado = "Cerrado";
                e.Historial.Add(new EventoHistorial
                {
                    FechaHora = DateTime.Now,
                    Tipo = "Cierre",
                    Descripcion = "En seguimiento → Cerrado. Motivo: " + motivo
                });
                Guardar();
            }
        }

        public void ReabrirExpediente(string id, string motivo)
        {
            motivo = Validar(motivo, "Motivo de reapertura");
            lock (bloqueo)
            {
                var e = BuscarInterno(id);
                if (e.Estado != "Cerrado")
                    throw new InvalidOperationException("Solo pueden reabrirse expedientes cerrados.");
                e.Estado = "En seguimiento";
                e.Historial.Add(new EventoHistorial
                {
                    FechaHora = DateTime.Now,
                    Tipo = "Reapertura",
                    Descripcion = "Cerrado → En seguimiento. Motivo: " + motivo
                });
                Guardar();
            }
        }

        // Solo llamar dentro de lock(bloqueo).
        private Expediente BuscarInterno(string id)
        {
            var e = expedientes.FirstOrDefault(x =>
                x.Id.Equals(id ?? "", StringComparison.OrdinalIgnoreCase));
            return e ?? throw new InvalidOperationException("El expediente no existe.");
        }

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
