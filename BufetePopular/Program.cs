using AICodeWars;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(
    new OrganizadorExpedientes(
        Path.Combine(
            builder.Environment.ContentRootPath,
            "expedientes.json"
        )
    )
);

var app = builder.Build();

// PÁGINA PRINCIPAL DEL BUFETE POPULAR
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="es">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Bufete Popular | Gestión Jurídica</title>

<style>
* {
    box-sizing: border-box;
}

body {
    margin: 0;
    font-family: 'Segoe UI', Arial, sans-serif;
    background: #f4f6f9;
    color: #263449;
}

header {
    background: #142b43;
    color: white;
    padding: 22px 7%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 20px;
}

.logo {
    font-size: 24px;
    font-weight: bold;
}

.logo span {
    color: #d8b675;
}

nav {
    display: flex;
    gap: 24px;
    flex-wrap: wrap;
}

nav a {
    color: white;
    text-decoration: none;
    font-size: 15px;
}

nav a:hover {
    color: #d8b675;
}

.hero {
    background: linear-gradient(120deg, #142b43, #23536a);
    color: white;
    text-align: center;
    padding: 95px 20px;
}

.hero h1 {
    font-size: clamp(32px, 5vw, 49px);
    margin: 0 0 20px;
}

.hero p {
    font-size: 18px;
    max-width: 680px;
    margin: 0 auto 30px;
    line-height: 1.7;
    color: #d9e4ed;
}

.btn {
    display: inline-block;
    background: #d8b675;
    color: #142b43;
    padding: 14px 24px;
    border-radius: 7px;
    text-decoration: none;
    font-weight: bold;
    margin: 5px;
}

.btn:hover {
    background: #f0d49a;
}

.btn-secondary {
    background: transparent;
    color: white;
    border: 1px solid white;
}

.btn-secondary:hover {
    background: #ffffff20;
}

.container {
    max-width: 1150px;
    margin: auto;
    padding: 48px 20px;
}

h2 {
    text-align: center;
    font-size: 30px;
    color: #142b43;
}

.subtitle {
    text-align: center;
    color: #66768a;
    margin-bottom: 35px;
}

.cards {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 24px;
}

.card {
    background: white;
    padding: 30px;
    border-radius: 12px;
    box-shadow: 0 5px 20px #142b4314;
    border: 1px solid #e5eaf0;
    transition: transform 0.2s;
}

.card:hover {
    transform: translateY(-5px);
}

.card .icon {
    font-size: 38px;
}

.card h3 {
    color: #142b43;
    margin-bottom: 12px;
}

.card p {
    color: #66768a;
    line-height: 1.7;
    min-height: 75px;
}

.card a {
    color: #087f76;
    font-weight: bold;
    text-decoration: none;
}

.stats {
    background: white;
    padding: 30px;
    margin-top: 45px;
    border-radius: 12px;
    border: 1px solid #e5eaf0;
}

.stats-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 20px;
}

.stat {
    background: #f4f6f9;
    padding: 25px;
    text-align: center;
    border-radius: 10px;
}

.stat strong {
    display: block;
    font-size: 37px;
    color: #142b43;
}

.stat p {
    color: #66768a;
    font-weight: 600;
}

.note {
    color: #66768a;
    text-align: center;
    font-size: 13px;
}

footer {
    background: #142b43;
    color: #cbd5df;
    text-align: center;
    padding: 25px;
    margin-top: 45px;
}

@media (max-width: 760px) {
    header {
        flex-direction: column;
        text-align: center;
    }

    nav {
        justify-content: center;
    }

    .cards, .stats-grid {
        grid-template-columns: 1fr;
    }

    .hero {
        padding: 65px 20px;
    }
}
</style>
</head>

<body>

<header>
    <div class="logo">⚖ Bufete <span>Popular</span></div>

    <nav>
        <a href="/">Inicio</a>
        <a href="/expedientes">Expedientes</a>
        <a href="/expedientes/nuevo">Nuevo expediente</a>
        <a href="/historial">Historial</a>
    </nav>
</header>

<section class="hero">
    <h1>Bienvenido al Bufete Popular</h1>

    <p>
        Sistema de gestión de expedientes jurídicos.
        Organiza los casos, registra actividades y
        consulta su estado e historial de forma sencilla,
        segura y ordenada.
    </p>

    <a href="/expedientes/nuevo" class="btn">
        + Crear expediente
    </a>

    <a href="/expedientes" class="btn btn-secondary">
        Consultar expedientes
    </a>
</section>

<main class="container">

    <h2>Gestión Jurídica</h2>
    <p class="subtitle">
        Herramientas para administrar expedientes del Bufete Popular
    </p>

    <section class="cards">

        <article class="card">
            <div class="icon">📁</div>
            <h3>Crear expediente</h3>
            <p>
                Registra nuevos expedientes con los datos
                del solicitante, descripción del asunto
                y responsable asignado.
            </p>
            <a href="/expedientes/nuevo">
                Registrar expediente →
            </a>
        </article>

        <article class="card">
            <div class="icon">🔎</div>
            <h3>Consultar expedientes</h3>
            <p>
                Busca expedientes mediante su identificador
                o nombre del solicitante y consulta
                la información registrada.
            </p>
            <a href="/expedientes">
                Consultar expedientes →
            </a>
        </article>

        <article class="card">
            <div class="icon">📋</div>
            <h3>Historial y seguimiento</h3>
            <p>
                Consulta las actividades realizadas
                y los cambios de estado
                registrados en cada expediente.
            </p>
            <a href="/historial">
                Ver historial →
            </a>
        </article>

    </section>

    <section class="stats">
        <h2>Resumen de expedientes</h2>

        <div class="stats-grid">

            <div class="stat">
                <strong>—</strong>
                <p>Expedientes abiertos</p>
            </div>

            <div class="stat">
                <strong>—</strong>
                <p>En seguimiento</p>
            </div>

            <div class="stat">
                <strong>—</strong>
                <p>Expedientes cerrados</p>
            </div>

        </div>

        <p class="note">
            Las estadísticas se actualizarán al conectar
            el módulo de gestión de expedientes.
        </p>
    </section>

</main>

<footer>
    <p>⚖ Bufete Popular | Sistema de Gestión Jurídica</p>
    <p>Universidad Mariano Gálvez — AI CODE WARS 2026</p>
    <p>Prototipo académico con datos ficticios</p>
</footer>

</body>
</html>
""", "text/html; charset=utf-8"));

// PÁGINAS TEMPORALES PARA INTEGRACIÓN CON LOS DEMÁS MÓDULOS

//
// MÓDULO DE ORGANIZACIÓN DE EXPEDIENTES
//

// Formulario de creación
app.MapGet("/expedientes/nuevo", () =>
{
    string html = """
    <!DOCTYPE html>
    <html lang="es">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Nuevo expediente</title>
        <style>
            body { font-family: Segoe UI, Arial; background:#f4f6f9; margin:0; color:#263449; }
            header { background:#142b43; color:white; padding:20px; text-align:center; }
            main { max-width:600px; margin:35px auto; background:white; padding:30px; border-radius:12px; box-shadow:0 4px 20px #0001; }
            label { display:block; margin-top:18px; font-weight:600; }
            input,textarea,select { width:100%; box-sizing:border-box; padding:12px; margin-top:7px; border:1px solid #cdd5df; border-radius:7px; font:inherit; }
            textarea { min-height:100px; }
            button { background:#142b43; color:white; border:0; padding:14px 22px; border-radius:7px; cursor:pointer; margin-top:25px; font-weight:bold; }
            a { color:#087f76; text-decoration:none; }
        </style>
    </head>
    <body>
        <header><h2>⚖ Bufete Popular</h2></header>
        <main>
            <a href="/">← Volver al inicio</a>
            <h1>Nuevo expediente</h1>
            <p>Complete los datos obligatorios para registrar el caso.</p>

            <form method="post" action="/expedientes/nuevo">
                <label>Nombre ficticio o alias *</label>
                <input name="solicitante" required maxlength="150">

                <label>Descripción del asunto *</label>
                <textarea name="asunto" required maxlength="1000"></textarea>

                <label>Nombre del responsable *</label>
                <input name="responsable" required maxlength="150">

                <label>Categoría (opcional)</label>
                <select name="categoria">
                    <option value="">Sin categoría</option>
                    <option value="Civil">Civil</option>
                    <option value="Familiar">Familiar</option>
                    <option value="Laboral">Laboral</option>
                </select>

                <button type="submit">Guardar expediente</button>
            </form>
        </main>
    </body>
    </html>
    """;

    return Results.Content(html, "text/html; charset=utf-8");
});

// Guardar un expediente nuevo
app.MapPost("/expedientes/nuevo",
    async (HttpRequest request, OrganizadorExpedientes organizador) =>
    {
        var form = await request.ReadFormAsync();

        try
        {
            var expediente = organizador.CrearExpediente(
                form["solicitante"].ToString(),
                form["asunto"].ToString(),
                form["responsable"].ToString(),
                form["categoria"].ToString()
            );

            return Results.Redirect("/expedientes/" + expediente.Id);
        }
        catch (ArgumentException ex)
        {
            return Results.Content(
                PaginaSimple("Error de validación",
                    "<p>" + WebUtility.HtmlEncode(ex.Message) + "</p>" +
                    "<a href='/expedientes/nuevo'>Volver al formulario</a>"),
                "text/html; charset=utf-8",
                statusCode: 400);
        }
    });

// Lista y búsqueda de expedientes
app.MapGet("/expedientes",
    (string? buscar, OrganizadorExpedientes organizador) =>
    {
        var lista = organizador.BuscarExpedientes(buscar);
        var html = new StringBuilder();

        html.Append("""
        <h1>Expedientes registrados</h1>
        <p>Consulte los expedientes del Bufete Popular.</p>
        <a href="/expedientes/nuevo">+ Nuevo expediente</a>

        <form method="get" action="/expedientes" style="margin:25px 0">
            <input name="buscar" placeholder="Buscar por ID o solicitante"
                   style="padding:12px;max-width:350px;width:70%">
            <button type="submit">Buscar</button>
        </form>
        """);

        if (lista.Count == 0)
        {
            html.Append("<p>No se encontraron expedientes.</p>");
        }
        else
        {
            html.Append("""
            <div style="overflow-x:auto">
            <table>
            <tr>
                <th>Identificador</th>
                <th>Solicitante</th>
                <th>Responsable</th>
                <th>Estado</th>
                <th>Detalle</th>
            </tr>
            """);

            foreach (var e in lista)
            {
                html.Append("<tr>");
                html.Append("<td>" + WebUtility.HtmlEncode(e.Id[..8]) + "</td>");
                html.Append("<td>" + WebUtility.HtmlEncode(e.Solicitante) + "</td>");
                html.Append("<td>" + WebUtility.HtmlEncode(e.Responsable) + "</td>");
                html.Append("<td>" + WebUtility.HtmlEncode(e.Estado) + "</td>");
                html.Append("<td><a href='/expedientes/" +
                    Uri.EscapeDataString(e.Id) + "'>Ver detalle</a></td>");
                html.Append("</tr>");
            }

            html.Append("</table></div>");
        }

        return Results.Content(
            PaginaSimple("Consulta de expedientes", html.ToString()),
            "text/html; charset=utf-8");
    });

// Detalle de un expediente e historial
app.MapGet("/expedientes/{id}",
    (string id, OrganizadorExpedientes organizador) =>
    {
        var e = organizador.ObtenerExpediente(id);

        if (e == null)
            return Results.NotFound("Expediente no encontrado.");

        string Seguro(string? valor) => WebUtility.HtmlEncode(valor ?? "");

        var html = new StringBuilder();

        html.Append("<h1>Detalle del expediente</h1>");
        html.Append("<p><b>ID:</b> " + Seguro(e.Id) + "</p>");
        html.Append("<p><b>Solicitante:</b> " + Seguro(e.Solicitante) + "</p>");
        html.Append("<p><b>Asunto:</b> " + Seguro(e.Asunto) + "</p>");
        html.Append("<p><b>Responsable:</b> " + Seguro(e.Responsable) + "</p>");
        html.Append("<p><b>Categoría:</b> " + Seguro(e.Categoria ?? "Sin categoría") + "</p>");
        html.Append("<p><b>Estado:</b> " + Seguro(e.Estado) + "</p>");
        html.Append("<p><b>Apertura:</b> " +
            e.FechaApertura.ToString("dd/MM/yyyy HH:mm") + "</p>");


        html.Append("<h2>Gestión del estado</h2>");

        if (e.Estado == "Abierto")
        {
            html.Append(
                "<p>Este expediente está abierto.</p>" +
                "<form method='post' action='/historial/" +
                Uri.EscapeDataString(e.Id) + "/accion'>" +
                "<input type='hidden' name='accion' value='seguimiento'>" +
                "<button type='submit'>Pasar a seguimiento</button>" +
                "</form>"
            );
        }
        else if (e.Estado == "En seguimiento")
        {
            html.Append(
                "<p>Este expediente está en seguimiento.</p>" +
                "<p>Debes registrar una actividad antes de cerrarlo.</p>" +
                "<form method='post' action='/historial/" +
                Uri.EscapeDataString(e.Id) + "/accion'>" +
                "<input type='hidden' name='accion' value='cerrar'>" +
                "<textarea name='motivo' required " +
                "placeholder='Escribe el motivo de cierre' " +
                "style='width:95%;padding:12px'></textarea>" +
                "<p><button type='submit'>Cerrar expediente</button></p>" +
                "</form>"
            );
        }
        else if (e.Estado == "Cerrado")
        {
            html.Append(
                "<p>Este expediente está cerrado.</p>" +
                "<form method='post' action='/historial/" +
                Uri.EscapeDataString(e.Id) + "/accion'>" +
                "<input type='hidden' name='accion' value='reabrir'>" +
                "<textarea name='motivo' required " +
                "placeholder='Motivo de reapertura' " +
                "style='width:95%;padding:12px'></textarea>" +
                "<p><button type='submit'>Reabrir expediente</button></p>" +
                "</form>"
            );
        }

        html.Append(
            "<p><a href='/historial/" +
            Uri.EscapeDataString(e.Id) +
            "'>Registrar actividades y consultar seguimiento</a></p>"
        );

        html.Append("<h2>Historial</h2>");

        foreach (var evento in e.Historial.OrderBy(h => h.FechaHora))
        {
            html.Append("<div class='evento'>");
            html.Append("<b>" + Seguro(evento.Tipo) + "</b><p>" +
                Seguro(evento.Descripcion) + "</p><small>" +
                evento.FechaHora.ToString("dd/MM/yyyy HH:mm") +
                "</small></div>");
        }
        html.Append(
    "<p><a href='/historial/" +
    Uri.EscapeDataString(e.Id) +
    "'>Administrar historial y seguimiento →</a></p>"
);

        html.Append("<p><a href='/expedientes'>Volver al listado</a></p>");

        return Results.Content(
            PaginaSimple("Detalle del expediente", html.ToString()),
            "text/html; charset=utf-8");
    });

// Acceso al historial desde la página principal

// LISTA DE EXPEDIENTES PARA CONSULTAR HISTORIAL
app.MapGet("/historial", (OrganizadorExpedientes organizador) =>
{
    var html = new StringBuilder();

    html.Append("<h1>Historial y seguimiento</h1>");
    html.Append("<p>Seleccione un expediente para consultar sus movimientos.</p>");

    var lista = organizador.ListarExpedientes();

    if (lista.Count == 0)
    {
        html.Append("<p>No existen expedientes registrados.</p>");
    }
    else
    {
        html.Append("<table><tr><th>Solicitante</th><th>Estado</th><th>Acción</th></tr>");

        foreach (var e in lista)
        {
            html.Append("<tr><td>" +
                WebUtility.HtmlEncode(e.Solicitante) +
                "</td><td>" +
                WebUtility.HtmlEncode(e.Estado) +
                "</td><td><a href='/historial/" +
                Uri.EscapeDataString(e.Id) +
                "'>Ver historial</a></td></tr>");
        }

        html.Append("</table>");
    }

    return Results.Content(
        PaginaSimple("Historial", html.ToString()),
        "text/html; charset=utf-8");
});

// HISTORIAL Y ACCIONES SOBRE UN EXPEDIENTE
app.MapGet("/historial/{id}",
    (string id, OrganizadorExpedientes organizador) =>
    {
        var e = organizador.ObtenerExpediente(id);

        if (e == null)
            return Results.NotFound("Expediente no encontrado.");

        var html = new StringBuilder();

        string Seguro(string? s) => WebUtility.HtmlEncode(s ?? "");

        string ruta = "/historial/" + Uri.EscapeDataString(e.Id) + "/accion";

        html.Append("<h1>Historial del expediente</h1>");
        html.Append("<p><b>Solicitante:</b> " + Seguro(e.Solicitante) + "</p>");
        html.Append("<p><b>Responsable:</b> " + Seguro(e.Responsable) + "</p>");
        html.Append("<p><b>Estado actual:</b> " + Seguro(e.Estado) + "</p>");

        html.Append("<h2>Movimientos registrados</h2>");

        foreach (var h in e.Historial.OrderBy(x => x.FechaHora))
        {
            html.Append("<div class='evento'>");
            html.Append("<b>" + Seguro(h.Tipo) + "</b>");
            html.Append("<p>" + Seguro(h.Descripcion) + "</p>");
            html.Append("<small>" +
                h.FechaHora.ToString("dd/MM/yyyy HH:mm:ss") +
                "</small>");
            html.Append("</div>");
        }

        html.Append("<h2>Administrar expediente</h2>");

        // Registrar actividad: solo si no está cerrado
        if (e.Estado != "Cerrado")
        {
            html.Append($"""
            <form method="post" action="{ruta}">
                <h3>Registrar actividad</h3>
                <input type="hidden" name="accion" value="actividad">
                <textarea name="descripcion" required
                    placeholder="Descripción de la actividad"
                    style="width:95%;padding:12px;min-height:80px"></textarea>
                <p><button type="submit">Guardar actividad</button></p>
            </form>
            """);
        }

        // Abierto -> En seguimiento
        if (e.Estado == "Abierto")
        {
            html.Append($"""
            <form method="post" action="{ruta}">
                <input type="hidden" name="accion" value="seguimiento">
                <button type="submit">Iniciar seguimiento</button>
            </form>
            """);
        }

        // En seguimiento -> Cerrado
        if (e.Estado == "En seguimiento")
        {
            html.Append($"""
            <form method="post" action="{ruta}">
                <h3>Cerrar expediente</h3>
                <input type="hidden" name="accion" value="cerrar">
                <textarea name="motivo" required
                    placeholder="Motivo de cierre"
                    style="width:95%;padding:12px"></textarea>
                <p><button type="submit">Cerrar expediente</button></p>
            </form>
            """);
        }

        // Cerrado -> En seguimiento
        if (e.Estado == "Cerrado")
        {
            html.Append($"""
            <form method="post" action="{ruta}">
                <h3>Reabrir expediente</h3>
                <p>Ingrese el motivo de reapertura.</p>
                <input type="hidden" name="accion" value="reabrir">
                <textarea name="motivo" required
                    placeholder="Motivo de reapertura"
                    style="width:95%;padding:12px"></textarea>
                <p><button type="submit">Reabrir expediente</button></p>
            </form>
            """);
        }

        html.Append("<p><a href='/historial'>← Volver al historial</a></p>");

        return Results.Content(
            PaginaSimple("Historial del expediente", html.ToString()),
            "text/html; charset=utf-8");
    });

// PROCESAR LAS ACCIONES DEL HISTORIAL
app.MapPost("/historial/{id}/accion",
    async (string id, HttpRequest request,
        OrganizadorExpedientes organizador) =>
    {
        var form = await request.ReadFormAsync();

        string accion = form["accion"].ToString();
        string motivo = form["motivo"].ToString();
        string descripcion = form["descripcion"].ToString();

        try
        {
            switch (accion)
            {
                case "seguimiento":
                    organizador.IniciarSeguimiento(id);
                    break;

                case "actividad":
                    organizador.AgregarActividad(id, descripcion);
                    break;

                case "cerrar":
                    organizador.CerrarExpediente(id, motivo);
                    break;

                case "reabrir":
                    organizador.ReabrirExpediente(id, motivo);
                    break;

                default:
                    throw new ArgumentException("Acción no válida.");
            }

            return Results.Redirect(
                "/historial/" + Uri.EscapeDataString(id));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException)
        {
            string mensaje = WebUtility.HtmlEncode(ex.Message);

            return Results.Content(
                PaginaSimple("No se pudo completar la acción",
                    "<h2>Operación rechazada</h2><p>" +
                    mensaje +
                    "</p><a href='/historial/" +
                    Uri.EscapeDataString(id) +
                    "'>Volver al expediente</a>"),
                "text/html; charset=utf-8",
                statusCode: 400);
        }
    });


app.Run();

// Diseño compartido por las páginas del módulo
static string PaginaSimple(string titulo, string contenido)
{
    return """
    <!DOCTYPE html>
    <html lang="es">
    <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <title>Bufete Popular</title>
      <style>
        body { margin:0; font-family:Segoe UI,Arial; background:#f4f6f9; color:#263449; }
        header { padding:22px; background:#142b43; color:white; text-align:center; }
        main { max-width:1000px; margin:30px auto; padding:25px; background:white; border-radius:12px; }
        a { color:#087f76; text-decoration:none; font-weight:600; }
        button { padding:12px; border:0; border-radius:5px; background:#142b43; color:white; cursor:pointer; }
        table { width:100%; border-collapse:collapse; margin-top:20px; }
        th,td { text-align:left; padding:13px; border-bottom:1px solid #dce3ea; }
        th { background:#edf2f6; }
        .evento { border-left:4px solid #d8b675; background:#f4f6f9; padding:15px; margin:12px 0; }
      </style>
    </head>
    <body>
      <header><h2>⚖ Bufete Popular</h2></header>
      <main>
        <p><a href="/">← Inicio</a></p>
    """ + contenido + """
      </main>
    </body>
    </html>
    """;
}
