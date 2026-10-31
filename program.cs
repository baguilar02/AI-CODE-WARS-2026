using System;

public class Class1
{
	public Class1()
	{
		
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ÁGINA PRINCIPAL DEL BUFETE POPULAR
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
app.MapGet("/expedientes", () => Results.Content(
    "<h1>Consulta de expedientes</h1><p>Módulo pendiente de integración.</p><a href='/'>Volver</a>",
    "text/html; charset=utf-8"));

app.MapGet("/expedientes/nuevo", () => Results.Content(
    "<h1>Nuevo expediente</h1><p>Formulario pendiente de integración.</p><a href='/'>Volver</a>",
    "text/html; charset=utf-8"));

app.MapGet("/historial", () => Results.Content(
    "<h1>Historial de actividades</h1><p>Módulo pendiente de integración.</p><a href='/'>Volver</a>",
    "text/html; charset=utf-8"));

app.Run();

	}
}
