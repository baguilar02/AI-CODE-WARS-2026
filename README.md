# AI-CODE-WARS-2026
Sistema Web de gestión y organización juridica.
# AI CODE WARS 2026
## Sistema de Gestión de Expedientes — Bufete Popular

### 1. Análisis del problema
El Bufete Popular necesita una aplicación web que permita organizar expedientes jurídicos ficticios, registrar las actividades realizadas y consultar su estado e historial.

El sistema facilitará el seguimiento de cada expediente mediante una interfaz clara, evitando pérdidas de información y cambios de estado incorrectos.

### 2. Usuarios
Personal responsable de registrar, consultar y dar seguimiento a expedientes del Bufete Popular.

### 3. Prioridades
1. Registrar expedientes con información obligatoria validada.
2. Consultar y buscar expedientes.
3. Registrar actividades con fecha y hora automáticas.
4. Administrar correctamente los estados.
5. Conservar los datos al cerrar y volver a abrir la aplicación.
6. Ofrecer una interfaz sencilla y comprensible.

### 4. Datos principales
- Identificador único del expediente.
- Nombre ficticio o alias del solicitante.
- Descripción del asunto.
- Nombre del responsable.
- Fecha y hora de apertura.
- Estado actual.
- Actividades e historial.
- Fecha, hora y motivo de cierre cuando corresponda.

### 5. Reglas de negocio
- Los campos obligatorios no pueden estar vacíos ni contener solamente espacios.
- Todo expediente inicia en estado Abierto.
- Las únicas transiciones permitidas son Abierto → En seguimiento → Cerrado.
- Para cerrar un expediente debe existir al menos una actividad y un motivo de cierre válido.
- Un expediente cerrado permanece disponible para consulta, pero no admite nuevas actividades ni cambios de estado.
- La apertura y cada cambio de estado deben registrarse automáticamente en el historial.

### 6. Supuestos técnicos
Se utilizará ASP.NET Core MVC con C#, HTML, CSS y JavaScript. El almacenamiento se realizará inicialmente mediante localStorage, sin requerir una base de datos externa.

Se utilizarán exclusivamente datos ficticios. La aplicación no ofrecerá asesoría jurídica.

### 7. Organización del equipo
| Integrante | Responsabilidad |
|---|---|
| Integrante 1 | Diseño visual, navegación y formularios. |
| Integrante 2 | Registro, validación, búsqueda y almacenamiento de expedientes. |
| Integrante 3 | Actividades, historial, seguimiento y cierre. |
| Integrante 4 | Integración, pruebas, documentación y GitHub. |

El equipo utilizará ramas independientes y solicitudes de integración para coordinar los cambios.

### 8. Prioridad de desarrollo
Se implementarán primero los requisitos funcionales obligatorios. Las mejoras visuales y características opcionales se realizarán únicamente si no comprometen las pruebas y la entrega.

### 9. Uso de inteligencia artificial
La IA apoyará el análisis, la programación, la revisión de reglas y las pruebas. El equipo revisará sus propuestas y documentará tres decisiones relevantes, sus razones y cómo fueron comprobadas.

### 10. Pruebas y resultados
Esta sección se completará después de ejecutar los tres recorridos exigidos por la competencia.

### 11. Limitaciones e instrucciones de ejecución
Se documentarán conforme avance la implementación.