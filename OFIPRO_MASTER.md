# OFIPRO MASTER DOCUMENT

Versión: 1.7

Fecha de creación: 2026-06-08

Última actualización: 2026-06-24

Estado: En desarrollo

---

# 1. VISIÓN DEL PROYECTO

## Nombre temporal

OfiPro

## Descripción

Plataforma web y móvil para conectar personas que necesitan trabajos de construcción, plomería, electricidad y otros oficios con contratistas y empresas que puedan realizarlos.

La plataforma busca convertirse en una red de confianza para contratación de oficios mediante reputación, historial y evidencia verificable.

---

# 2. PROBLEMA QUE RESUELVE

Actualmente la contratación de oficios ocurre mediante:

* Recomendaciones
* Facebook
* WhatsApp
* Amigos y familiares

Problemas detectados:

* Falta de confianza
* No existe historial verificable
* No hay evidencias estructuradas
* Difícil comparar proveedores
* Difícil encontrar trabajadores confiables

---

# 3. PROPUESTA DE VALOR

Permitir:

* Publicar proyectos
* Recibir propuestas
* Comparar contratistas
* Contratar servicios
* Evaluar resultados
* Construir reputación profesional

---

# 4. REGLAS DEL PROYECTO

## Regla 1

No aumentar alcance de una versión.

Toda idea nueva debe enviarse al backlog.

---

## Regla 2

Una versión termina cuando cumple sus objetivos.

No cuando está perfecta.

---

## Regla 3

Primero terminar V1.

Después mejorar V1.

---

## Regla 4

No discutir funcionalidades de V2 mientras V1 no esté terminada.

---

## Regla 5

Todo cambio importante debe documentarse.

---

## Regla 6

Al finalizar una sesión de trabajo se debe generar:

* Reporte de avance
* Bloque para SESSION_LOG.md
* Actualización para OFIPRO_MASTER.md
* Próximo objetivo
* Riesgos actuales
* Evaluación de velocidad

Sin importar la frase utilizada por el usuario.

Ejemplos:

* La sesión de hoy terminó
* Dejemos la sesión de hoy
* Por hoy se acabó
* Hasta aquí dejémoslo por hoy
* Mañana seguimos

---

# 5. ARQUITECTURA

## Arquitectura elegida

Clean Architecture

---

## Tipo

Monolito modular

---

## Razón

Reduce complejidad.

Permite terminar la V1 más rápido.

Facilita una futura migración a microservicios.

---

# 6. TECNOLOGÍAS

Backend

* .NET 8
* ASP.NET Core Web API

Frontend

* Angular

Base de datos

* SQL Server

ORM

* Entity Framework Core 8.0.27

Repositorio

* GitHub

---

# 7. ESTRUCTURA DE LA SOLUCIÓN

OfiPro.Api

OfiPro.Application

OfiPro.Domain

OfiPro.Infrastructure

Docs

---

# 8. MODELO DE NEGOCIO

Marketplace de oficios.

Usuarios objetivo:

* Personas
* Contratistas
* Empresas pequeñas

Oficios iniciales:

* Albañilería
* Plomería
* Electricidad

---

# 9. CONCEPTOS IMPORTANTES

## No existen clientes y contratistas separados

Todos son:

User

---

Un usuario puede:

* Publicar proyectos
* Contratar servicios

---

Si activa:

ProfessionalProfile

también puede:

* Enviar propuestas
* Aparecer en búsquedas
* Construir reputación

---

# 10. V1

Objetivo:

Permitir que un usuario:

1. Publique un proyecto.
2. Reciba propuestas.
3. Seleccione una propuesta.
4. Finalice un trabajo.
5. Cliente y contratista se califiquen mutuamente al finalizar una contratación.

---

# 11. NO ENTRA EN V1

* IA
* Chat
* Pagos
* Ferreterías
* App móvil
* Empresas avanzadas
* Geolocalización avanzada
* Contratos digitales
* Facturación
* Múltiples especialidades
* Microservicios

---

# 12. ENTIDADES DOMAIN

* User
* UserRole
* ProfessionalProfile
* Category
* Subcategory
* Project
* ProjectRequirement
* Proposal
* Contract
* Evidence
* Notification
* Rating
* Invitation

---

# 13. ENUMS

UserRoleType

* Cliente
* Contratista
* Administrador

ProjectStatus

* Publicado
* Asignado
* EnProceso
* PendienteConfirmacion
* Finalizado
* Cancelado
* Expirado

ProposalStatus

* Pendiente
* Aceptada
* Rechazada
* Retirada

UrgencyLevel

* Flexible
* EstaSemana
* Urgente

EvidenceType

* Antes
* Durante
* Despues

NotificationType

* General
* ProposalReceived
* ProposalAccepted
* ProposalRejected
* ContractCreated
* ContractStarted
* ContractFinished
* ContractCancelled
* EvidenceUploaded
* ContractPendingConfirmation
* RatingReceived
* ProjectExpired

---

# 14. DECISIONES IMPORTANTES

## D001

No usar microservicios.

---

## D002

Todos los usuarios pueden crear proyectos.

---

## D003

No existirán tablas Cliente y Contratista.

---

## D004

ProfessionalProfile tendrá una sola especialidad.

---

## D005

Project soportará múltiples necesidades.

Resultado:

Se crea ProjectRequirement.

---

## D006

Pensar en Soft Delete desde el diseño.

Resultado:

Se agrega DeletedAt en entidades principales.

Implementación completa pendiente.

---

## D007

Documentación obligatoria.

Resultado:

OFIPRO_MASTER.md y SESSION_LOG.md.

---

## D008

Antes de ejecutar comandos técnicos se debe validar primero.

Orden correcto:

1. Validar requisitos.
2. Ejecutar.
3. Verificar resultado.

---

## D009

Cada bloque debe cerrarse formalmente.

---

## D010

Los cierres de sesión entregarán:

* SESSION_LOG completo de la sesión.
* Actualizaciones para OFIPRO_MASTER.

---

## D011

Todo bloque debe tener:

* Objetivo definido.
* Alcance definido.
* Criterio de terminado.

---

## D012

No iniciar Angular hasta tener Auth funcional.

---

## D013

El primer administrador del sistema se crea mediante seed automático al iniciar la aplicación.

Usuario:

[admin@ofipro.com](mailto:admin@ofipro.com)

Razón:

* Facilita pruebas.
* Evita inserciones manuales.
* Servirá como patrón para futuros catálogos.

---

## D014

ProjectRequirements se gestionan mediante endpoint independiente.

Endpoints:

PUT /api/projects/{id}

PUT /api/projects/{id}/requirements

Razón:

Separación de responsabilidades entre Project y ProjectRequirement.

---

## D015

Un contratista solo puede tener una propuesta activa por ProjectRequirement.

Resultado:

La combinación:

* ProjectRequirementId
* ContractorUserId

debe ser única a nivel de negocio.

Si el contratista desea modificar una propuesta deberá actualizar la existente.

No podrá crear una segunda propuesta activa para el mismo requerimiento.

---

---

## D016

Al aceptar una propuesta todas las demás propuestas del mismo ProjectRequirement se rechazan automáticamente.

Resultado:

Cuando una propuesta cambia a:

* Aceptada

todas las demás propuestas pendientes asociadas al mismo ProjectRequirement cambian a:

* Rechazada

Razón:

Evitar dejar contratistas esperando una respuesta indefinidamente.

---

## D017
No se aceptarán soluciones temporales que generen retrabajo conocido.

Si existe una solución correcta y escalable que pueda implementarse dentro del alcance actual, se elegirá esa solución.

---

## D018

Los secretos no deben guardarse en appsettings.json ni en el repositorio.

Resultado:

La clave JWT se mueve a User Secrets en entorno local.

Razón:

Evitar exponer credenciales o claves sensibles en GitHub.

---

## D019

Aceptar o rechazar propuestas requiere validar al propietario del proyecto.

Resultado:

Solo el usuario que creó el proyecto puede aceptar o rechazar propuestas asociadas a sus requerimientos.

Razón:

Evitar que usuarios autenticados manipulen propuestas ajenas.

---

## D020

Las pruebas que requieran IDs se apoyarán en consultas directas a base de datos.

Resultado:

Cuando se necesite ProjectId, ProjectRequirementId o ProposalId, se consultará SQL Server para evitar confusión entre identificadores.

Razón:

El flujo Project → Requirement → Proposal maneja varios Guid y puede provocar errores durante pruebas manuales.

---

## D021

Las excepciones de negocio no deben lanzarse con Exception genérica.

Resultado:

Se implementan excepciones específicas:

* BadRequestException
* ForbiddenException
* NotFoundException

Razón:

Permitir respuestas HTTP correctas y consistentes.

---

## D022

Los controladores no deben manejar errores de negocio manualmente.

Resultado:

AuthController elimina try/catch redundantes y delega el manejo de errores al ExceptionMiddleware.

Razón:

Mantener consistencia en respuestas y evitar duplicación de lógica.

---

## D023

Todos los DTOs de actualización deben tener validaciones.

Resultado:

Se agregan DataAnnotations a DTOs Update para evitar modificaciones inválidas.

Razón:

Proteger integridad de datos tanto en creación como actualización.

---

## D024

Los DTOs de respuesta deben usar identificadores descriptivos.

Resultado:

En respuestas públicas de API se usará:

* ProjectId
* ProjectRequirementId
* ProposalId
* ContractId
* UserId

en lugar de exponer únicamente Id cuando pueda generar confusión.

Razón:

Facilitar pruebas, depuración y consumo desde frontend.

---

## D025

Un usuario puede tener más de un rol.

Resultado:

Un usuario registrado inicialmente como Cliente puede activar también el rol Contratista.

Razón:

En OfiPro no existen usuarios completamente separados entre clientes y contratistas. Un mismo usuario puede publicar proyectos y también enviar propuestas si activa su perfil profesional.

---

## D026

Solo usuarios con rol Contratista pueden enviar propuestas.

Resultado:

El endpoint de creación de propuestas valida que el usuario autenticado tenga el rol Contratista.

Razón:

Evitar que cualquier usuario registrado como Cliente pueda enviar propuestas sin haber activado su perfil profesional.

---

## D027

Las interfaces de Application deben separarse por tipo.

Resultado:

Las interfaces se organizan en:

* Interfaces/Repositories
* Interfaces/Services

Razón:

Evitar mezclar contratos de repositorios con contratos de servicios y mantener la arquitectura limpia conforme el proyecto crezca.

---

## D028

Las propuestas asociadas a un requerimiento solo pueden ser consultadas por el dueño del proyecto.

Resultado:

El endpoint GET /api/proposals/requirement/{projectRequirementId} valida que el usuario autenticado sea el propietario del proyecto al que pertenece el requerimiento.

Razón:

Evitar que un contratista pueda ver propuestas de otros contratistas, incluyendo precios, tiempos, garantías o descripciones, antes de enviar su propia propuesta.

Impacto:

Se protege la competencia entre contratistas y se evita manipulación de precios u ofertas.

---

## D029

La validación de soft delete debe hacerse preferentemente desde repositorio.

Resultado:

Se eliminó el filtro redundante DeletedAt == null en UserService.GetAllAsync, ya que UserRepository.GetAllAsync ya devuelve únicamente usuarios no eliminados.

Razón:

Evitar código duplicado, reducir confusión y mantener la responsabilidad de filtrado de datos en la capa de repositorio.

---

## D030

OfiPro será una plataforma mobile-first, pero no tendrá una etapa PWA.

Resultado:

La estrategia de producto queda definida así:

* Backend/API sólido como base central.
* Web responsiva necesaria para operación inicial y administración.
* App móvil real en una etapa posterior.
* No se desarrollará PWA como paso intermedio.

Razón:

OfiPro apunta a usuarios que probablemente operarán desde celular, especialmente contratistas en campo y clientes que requieren seguimiento rápido. Sin embargo, una PWA se considera un paso intermedio que consumiría tiempo y recursos sin aportar el valor suficiente frente a una app móvil real.

Impacto:

El backend debe mantenerse preparado para ser consumido tanto por web como por app móvil, con DTOs claros, rutas simples, errores consistentes y seguridad bien definida.

---

## D031

Las evidencias estarán asociadas a una contratación.

Resultado:

La entidad Evidence se relaciona directamente con Contract.

Flujo conceptual:

Project → ProjectRequirement → Proposal aceptada → Contract → Evidence

Razón:

Una evidencia no pertenece solamente a un proyecto ni a una propuesta. La evidencia sirve para comprobar avances o actividades dentro de un trabajo ya contratado.

---

## D032

La primera versión de Evidencias usará FileUrl como texto.

Resultado:

No se implementa todavía carga real de archivos. La evidencia almacena una URL de archivo mediante el campo FileUrl.

Razón:

Subir archivos reales implica un bloque adicional de seguridad y almacenamiento:

* Validar tamaño.
* Validar extensión.
* Definir ubicación física o nube.
* Generar URL pública.
* Proteger archivos.
* Limpiar archivos eliminados.
* Controlar posibles abusos.

Para V1 se prioriza validar la lógica de negocio antes de implementar almacenamiento real.

---

## D033

Solo el contratista de la contratación puede subir evidencias.

Resultado:

El servicio de evidencias valida que el usuario autenticado sea el ContractorUserId del contrato.

Razón:

Evitar que clientes, otros contratistas o usuarios ajenos puedan registrar evidencias dentro de una contratación que no les corresponde.

---

## D034

Cliente y contratista pueden consultar evidencias de una contratación.

Resultado:

Las evidencias de un contrato pueden ser consultadas por:

* El cliente dueño de la contratación.
* El contratista asignado a la contratación.

Cualquier otro usuario recibe acceso denegado.

Razón:

Las evidencias contienen información del avance del trabajo y solo deben estar disponibles para las partes involucradas.

---

## D035

Las evidencias se eliminan mediante soft delete.

Resultado:

Al eliminar una evidencia se asigna DeletedAt, pero no se elimina físicamente de la base de datos.

Razón:

Mantener trazabilidad de las evidencias registradas, especialmente porque pueden formar parte del historial del trabajo, aclaraciones o revisiones posteriores.

---

## D036

Las evidencias deben clasificarse por fase del trabajo.

Resultado:

Se utiliza el enum EvidenceType dentro de la entidad Evidence.

Valores definidos:

* Antes = 1
* Durante = 2
* Despues = 3

Razón:

Una evidencia no debe ser solamente un archivo o URL aislada. Para OfiPro es importante saber si la evidencia corresponde al estado inicial del trabajo, a un avance o al resultado final.

Impacto:

Las evidencias podrán organizarse mejor en frontend, app móvil, historial del contrato, validaciones de cierre y posibles aclaraciones entre cliente y contratista.

---

## D037

FileType de evidencias debe validarse contra tipos permitidos.

Resultado:

CreateEvidenceDto valida que FileType solo acepte:

* image/jpeg
* image/png
* application/pdf

Razón:

Evitar que el sistema acepte valores libres como ejecutable, script u otros tipos no esperados.

Impacto:

Aunque en esta etapa FileUrl sigue siendo texto, la validación deja preparado el módulo para una futura carga real de archivos con mayor seguridad.

---

## D038

OfiPro debe considerar la cultura informática y la comodidad del usuario final.

Resultado:

Se reconoce que la app móvil real tendrá un papel central en la operación diaria de OfiPro, especialmente para contratistas como albañiles, plomeros, pintores, electricistas y trabajadores de campo.

Razón:

Aunque una web responsiva es necesaria, el usuario operativo difícilmente tendrá el hábito de abrir el navegador, buscar la página, iniciar sesión y revisar constantemente si tiene nuevas oportunidades o actualizaciones.

La experiencia más natural para este tipo de usuario será recibir una notificación, abrir la app y responder de forma rápida.

Impacto:

La web seguirá existiendo, pero no debe ser vista como el canal principal de operación diaria del contratista.

Distribución estratégica:

* Web: presencia formal, administración, operación básica y paneles.
* App móvil real: operación diaria, notificaciones, evidencias, seguimiento y respuesta rápida.
* API: centro del sistema para web y móvil.

---

## D039

Antes de avanzar a nuevos módulos grandes, se debe preparar el backend para notificaciones internas.

Resultado:

Se define como próximo bloque el módulo base de notificaciones internas.

Razón:

Si los contratistas no se enteran de nuevas oportunidades, propuestas aceptadas, contratos iniciados o evidencias solicitadas, el marketplace puede sentirse inactivo para clientes y contratistas.

Las notificaciones internas permitirán registrar eventos importantes aunque todavía no exista push notification real.

Eventos futuros importantes:

* Nueva propuesta recibida.
* Propuesta aceptada.
* Propuesta rechazada.
* Contrato creado.
* Contrato iniciado.
* Contrato pendiente de confirmación.
* Evidencia subida.
* Contrato finalizado.

Impacto:

El backend quedará preparado para que, más adelante, una app móvil real pueda consumir notificaciones y posteriormente integrarse con push notifications.

---

## D040

Las notificaciones internas se almacenan en base de datos.

Resultado:

Se crea la entidad Notification para registrar avisos internos asociados a usuarios.

Las notificaciones incluyen:

* Usuario destinatario.
* Tipo de notificación.
* Título.
* Mensaje.
* Entidad relacionada.
* Id de entidad relacionada.
* Estado de lectura.
* Fecha de creación.
* Fecha de lectura.
* Eliminación lógica mediante DeletedAt.

Razón:

Antes de implementar push notifications reales, OfiPro necesita registrar eventos importantes dentro del backend para que web y app móvil puedan consultarlos.

Impacto:

El sistema ya puede mostrar una bandeja o campana de notificaciones internas aunque todavía no exista una app móvil real ni integración con Firebase, APNs u otro proveedor de push notifications.

---

## D041

Las notificaciones deben generarse desde los servicios de negocio.

Resultado:

Las notificaciones no se crean manualmente desde un endpoint público.

Se generan desde los servicios correspondientes cuando ocurre un evento real del sistema.

Eventos implementados:

* Contratista envía propuesta → se notifica al cliente.
* Cliente acepta propuesta → se notifica al contratista aceptado.
* Cliente rechaza propuesta → se notifica al contratista.
* Cliente acepta una propuesta y otras quedan rechazadas automáticamente → se notifica a los contratistas rechazados.
* Contratista sube evidencia → se notifica al cliente.
* Contratista inicia contratación → se notifica al cliente.
* Contratista marca contratación como pendiente de confirmación → se notifica al cliente.
* Cliente finaliza contratación → se notifica al contratista.
* Cliente o contratista cancela contratación → se notifica a la otra parte.

Razón:

La notificación es consecuencia de una acción de negocio, no una acción aislada del usuario.

Impacto:

Si en el futuro una acción se ejecuta desde web, app móvil o cualquier otro cliente, la notificación seguirá generándose porque vive en el servicio y no en el controlador.

---

## D042

Las fechas del backend se guardan en UTC.

Resultado:

CreatedAt, ReadAt y demás fechas internas se guardan usando UTC.

Razón:

El backend debe ser consistente para web, app móvil y posibles usuarios en diferentes zonas horarias.

Impacto:

SQL Server puede mostrar una hora aparentemente diferente a la hora local de México, pero eso es correcto. La conversión a hora local debe hacerse en frontend o app móvil al mostrar la información al usuario.

---

## D043

Las calificaciones deben ser bidireccionales.

Resultado:

La entidad Rating se relaciona con Contract y permite registrar calificaciones en dos direcciones:

* Cliente califica al contratista.
* Contratista califica al cliente.

Campos principales:

* ContractId
* RaterUserId
* RatedUserId
* Score
* Comment
* CreatedAt
* DeletedAt

Razón:

La confianza en OfiPro no debe depender solamente del comportamiento del contratista. El cliente también debe construir reputación, ya que su claridad, comunicación, respeto y cumplimiento afectan la experiencia del trabajo.

Impacto:

El sistema podrá construir reputación para ambas partes del marketplace.

---

## D044

Una contratación solo puede calificarse cuando está finalizada.

Resultado:

El servicio de calificaciones valida que Contract.Status sea Finalizado antes de permitir crear un Rating.

Razón:

Una calificación antes de finalizar el trabajo podría ser injusta, incompleta o manipulable.

Impacto:

La reputación se construye únicamente a partir de trabajos terminados.

---

## D045

Solo cliente y contratista de una contratación pueden calificarla.

Resultado:

El servicio valida que el usuario autenticado sea ClientUserId o ContractorUserId del contrato.

Reglas aplicadas:

* Usuarios ajenos no pueden calificar la contratación.
* Un usuario no puede calificarse a sí mismo.
* Cliente califica automáticamente al contratista.
* Contratista califica automáticamente al cliente.

Razón:

Evitar calificaciones externas, falsas o manipuladas.

Impacto:

La reputación queda ligada a interacciones reales dentro de la plataforma.

---

## D046

Solo puede existir una calificación por dirección en cada contratación.

Resultado:

Se configura una restricción única para evitar duplicados por:

* ContractId
* RaterUserId
* RatedUserId

Razón:

En una contratación solo deben existir como máximo dos calificaciones reales:

* Cliente → Contratista
* Contratista → Cliente

Impacto:

Evita duplicar calificaciones y protege la integridad de la reputación.

---

## D047

La reputación pública no debe exponer datos internos de la contratación.

Resultado:

Se separan endpoints internos y públicos para ratings.

Vista interna:

* Puede incluir RatingId.
* Puede incluir ContractId.
* Puede incluir RaterUserId.
* Puede incluir RatedUserId.

Vista pública:

* Muestra nombre de quien calificó.
* Muestra Score.
* Muestra Comment.
* Muestra CreatedAt.
* No expone ContractId ni IDs internos de usuarios.

Razón:

El perfil público debe ser útil para generar confianza, pero no debe revelar información interna innecesaria.

Impacto:

La API queda mejor preparada para perfiles públicos en web y app móvil.

---

## D048

La app móvil debe poder consultar reputación pública en una sola llamada.

Resultado:

Se crea un endpoint público de reputación completa por usuario que devuelve:

* UserId
* UserName
* AverageScore
* TotalRatings
* LastRatingAt
* Ratings públicas recibidas

Razón:

Para mobile-first conviene reducir llamadas innecesarias desde la app móvil y entregar datos listos para pintar una pantalla de perfil.

Impacto:

La futura app móvil y la web responsiva podrán mostrar perfiles con reputación de forma más simple y eficiente.

---

## D049

Una calificación recibida debe generar notificación interna.

Resultado:

Se agrega NotificationType.RatingReceived.

Cuando un usuario recibe una calificación, el sistema genera una notificación interna para el usuario calificado.

Evento implementado:

* Cliente califica contratista → contratista recibe notificación.
* Contratista califica cliente → cliente recibe notificación.

Razón:

El módulo de notificaciones ya cubría eventos importantes como propuestas, contratos y evidencias. Faltaba cubrir el cierre natural del ciclo de confianza: recibir una calificación.

Impacto:

La reputación queda mejor integrada al comportamiento mobile-first, permitiendo que web y futura app móvil muestren avisos cuando un usuario recibe una nueva calificación.

---

## D050

Los dashboards deben organizarse por modo de operación.

Resultado:

Se crean endpoints separados para:

* Dashboard de cliente.
* Dashboard de contratista.
* Dashboard de administrador.

Regla:

* Un usuario con rol Cliente puede consultar el dashboard de cliente.
* Un usuario con rol Contratista puede consultar el dashboard de contratista.
* Un usuario con rol Administrador puede consultar el dashboard administrativo.
* Un usuario multirol puede consultar los dashboards correspondientes a sus roles.

Razón:

En OfiPro un mismo usuario puede tener más de un rol. Por eso el dashboard no debe interpretarse como usuario exclusivo, sino como modo de operación.

Impacto:

La web y futura app móvil podrán mostrar la experiencia correcta según el modo activo del usuario.

---

## D051

El backend debe exponer los modos disponibles del usuario autenticado.

Resultado:

Se crea el endpoint:

GET /api/dashboard/modes

Este endpoint devuelve:

* UserId.
* CanUseClientMode.
* CanUseContractorMode.
* CanUseAdminMode.
* AvailableModes.
* DefaultMode.

Razón:

El frontend o app móvil no debe adivinar qué pantallas mostrar. El backend debe informar claramente qué modos puede usar el usuario autenticado.

Impacto:

Se reduce complejidad en frontend y se prepara mejor la experiencia mobile-first.

---

## D052

El dashboard debe tener un endpoint de contexto del usuario autenticado.

Resultado:

Se crea el endpoint:

GET /api/dashboard/me

Este endpoint devuelve:

* UserId.
* Name.
* LastName.
* FullName.
* Email.
* Modes.

Razón:

Después del login, la web o app móvil necesita saber rápidamente quién es el usuario y qué modos puede usar.

Impacto:

La aplicación cliente podrá inicializar la pantalla principal con menos llamadas y menos lógica duplicada.

---

## D053

Los usuarios de prueba deben separarse por escenario.

Resultado:

Para evitar confusiones en pruebas manuales, se define la siguiente estrategia:

* [cliente@ofipro.com](mailto:cliente@ofipro.com) → Cliente puro.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) → Contratista puro.
* [admin@ofipro.com](mailto:admin@ofipro.com) → Usuario multirol para pruebas administrativas e híbridas.

Razón:

Un usuario multirol responde correctamente a más de un modo, pero no sirve para probar restricciones negativas de rol.

Impacto:

Las pruebas de autorización quedan más claras y se evitan falsos diagnósticos.

---

## D054

DashboardRepository puede consultar directamente ApplicationDbContext para lecturas agregadas.

Resultado:

El módulo de dashboard utiliza consultas directas sobre ApplicationDbContext para construir resúmenes de cliente, contratista y administrador.

Razón:

Los dashboards necesitan combinar información de varios módulos en una sola respuesta:

* Projects
* Proposals
* Contracts
* Notifications
* Ratings
* UserRoles

Usar repositorios individuales para cada conteo o resumen generaría más llamadas internas, más complejidad y menor eficiencia.

Regla:

Este patrón queda permitido únicamente para endpoints de lectura agregada o read-models de dashboard.

No debe usarse para modificar reglas de negocio ni para saltarse servicios existentes en operaciones de escritura.

Impacto:

El dashboard puede optimizar consultas transversales, pero queda documentado que depende del schema de varias tablas y debe revisarse cuando cambien entidades relacionadas.

---

## D055

OfiPro no debe lanzarse al mercado solo con web.

Resultado:

La estrategia de salida a mercado queda ajustada:

* Primero terminar backend/API.
* Después construir web responsiva suficiente.
* Después construir app móvil real.
* Lanzar al mercado cuando web y app móvil estén listas para un flujo usable.

Razón:

OfiPro depende mucho de usuarios en campo, especialmente contratistas que necesitan recibir oportunidades, notificaciones, seguimiento y carga de evidencias desde celular.

Lanzar solo con web podría generar fricción porque el usuario operativo no necesariamente tendrá el hábito de abrir navegador, iniciar sesión y revisar manualmente la plataforma.

Impacto:

Elementos como refresh tokens, upload real de archivos, FCM Token y push notifications pasan a ser preparación pre-lanzamiento móvil, no mejoras post-lanzamiento.

---

## D056

ProfessionalProfile será la base del descubrimiento de contratistas.

Resultado:

El módulo ProfessionalProfile permite que un usuario con rol Contratista registre información profesional visible para clientes.

El perfil incluye:

Especialidad principal.
Descripción profesional.
Años de experiencia.
Estado activo/inactivo.
Relación con el usuario.

Razón:

OfiPro no puede depender únicamente de que el cliente publique un proyecto y espere propuestas. También debe permitir que el cliente descubra contratistas de forma activa.

Impacto:

ProfessionalProfile se convierte en la base para búsqueda, comparación, reputación pública y futura visibilidad del contratista dentro de la plataforma.

---

## D057

La búsqueda básica de contratistas entra en V1.

Resultado:

Se crea un endpoint de búsqueda básica de contratistas:

GET /api/contractors

Filtros soportados en V1:

specialty
state
city

Razón:

El marketplace necesita cerrar el ciclo de discovery. El cliente debe poder buscar contratistas disponibles sin depender únicamente de propuestas recibidas.

Regla:

La búsqueda solo devuelve perfiles profesionales activos, usuarios activos y usuarios con rol Contratista.

Impacto:

OfiPro ya permite que el cliente encuentre contratistas por especialidad y ubicación usando consultas simples con EF Core, sin necesidad de ElasticSearch en V1.

## D058

El dashboard del contratista debe mostrar el estado de su perfil profesional.

Resultado:

El endpoint de dashboard de contratista incluye información del perfil profesional:

HasProfessionalProfile.
ProfessionalProfileId.
IsProfessionalProfileActive.
MainSpecialty.

Razón:

La web y futura app móvil deben poder indicar al contratista si ya tiene perfil profesional o si necesita completarlo para aparecer en búsquedas.

Impacto:

El flujo mobile-first queda mejor preparado porque el home del contratista puede mostrar acciones útiles como completar, activar o revisar su perfil profesional.

---

## D059

Los proyectos publicados antiguos deben expirar automáticamente.

Resultado:

Se implementa un proceso automático que marca como Expirado los proyectos publicados que superan el límite de antigüedad configurado.

Configuración agregada:

ProjectExpiration
ProjectExpiration

Razón:

Evitar que proyectos viejos sigan apareciendo como oportunidades disponibles para contratistas.

Impacto:

El feed general de proyectos queda más limpio y evita mostrar proyectos fantasma. Esto mejora la experiencia del contratista, especialmente en una futura app móvil donde el usuario esperará oportunidades vigentes y accionables.

Regla:

Solo los proyectos con estado Publicado pueden expirar automáticamente. Los proyectos eliminados lógicamente no se procesan.

---

## D060

Los listados críticos deben devolver respuestas paginadas.

Resultado:

Se implementa paginación y ordenamiento básico en endpoints que devuelven listas relevantes para web responsiva y futura app móvil real.

Endpoints actualizados:

* GET /api/projects
* GET /api/contractors
* GET /api/notifications
* GET /api/contracts/mine
* GET /api/proposals/my-proposals
* GET /api/projects/my-projects

Razón:

Evitar que los clientes consuman listas completas sin control, reducir carga innecesaria en API y base de datos, y preparar mejor la experiencia para pantallas móviles.

Impacto:

Los endpoints paginados devuelven una estructura consistente con:

Items
PageNumber
PageSize
TotalItems
TotalPages
HasPreviousPage
HasNextPage

Regla:

Los listados críticos no deben crecer sin paginación. Nuevos endpoints de lista deberán considerar paginación desde el diseño.

---

## D061

Los perfiles públicos de contratistas deben poder consultarse sin autenticación.

Resultado:

Los endpoints públicos de contratistas permiten consulta sin token:

* GET /api/contractors
* GET /api/contractors/{userId}

Razón:

El perfil público de un contratista puede compartirse por canales externos como WhatsApp, redes sociales o enlaces directos. Si el usuario que recibe el enlace no ha iniciado sesión, no debe recibir 401 Unauthorized.

Impacto:

La búsqueda y visualización pública de contratistas queda alineada con el objetivo de discovery del marketplace.

Regla:

Solo los endpoints públicos de búsqueda y perfil de contratista permiten acceso anónimo. La creación, consulta propia y actualización de perfil profesional siguen protegidas con JWT.

---

## D062

La expiración automática de proyectos debe notificar al dueño del proyecto.

Resultado:

Cuando un proyecto publicado expira automáticamente, el sistema genera una notificación interna para el usuario que creó el proyecto.

Tipo de notificación agregado:

* ProjectExpired

Razón:

Si un proyecto desaparece del feed por expiración y el cliente no recibe aviso, puede interpretar el comportamiento como error del sistema.

Impacto:

El cliente mantiene visibilidad sobre el estado de sus publicaciones y la experiencia queda mejor preparada para web responsiva y app móvil real.

---


## HITO 8.2

Correcciones de diagnóstico de Ratings y reputación completadas.

Incluye:

* Se agregó NotificationType.RatingReceived.
* Se inyectó INotificationService en RatingService.
* Se genera notificación interna cuando un usuario recibe una calificación.
* Se probó notificación al contratista cuando el cliente lo califica.
* Se probó notificación al cliente cuando el contratista lo califica.
* Se refactorizó lógica duplicada de reputación.
* Se agregó método privado para obtener usuario activo.
* Se agregó método privado para calcular estadísticas de reputación.
* Se agregó método privado para mapear ratings públicos.

Pruebas realizadas:

* Cliente califica contratista → 200 OK.
* Contratista recibe notificación RatingReceived → 200 OK.
* Contratista califica cliente → 200 OK.
* Cliente recibe notificación RatingReceived → 200 OK.
* GET /api/users/{userId}/reputation → 200 OK.
* GET /api/users/{userId}/ratings/public → 200 OK.
* GET /api/users/{userId}/reputation/public → 200 OK.

Resultado:

El módulo de Ratings y reputación queda corregido, notificado y con menor duplicación interna.

---


## HITO 9

Dashboard mínimo / Resúmenes para móvil y web completado.

Incluye:

* Creación de DTOs de dashboard.
* Creación de IDashboardRepository.
* Creación de DashboardRepository.
* Creación de IDashboardService.
* Creación de DashboardService.
* Creación de DashboardController.
* Resumen de cliente.
* Resumen de contratista.
* Resumen de administrador.
* Actividad reciente para cliente.
* Actividad reciente para contratista.
* Notificaciones recientes.
* Contratos recientes.
* Propuestas pendientes para cliente.
* Proyectos disponibles para contratista.
* Modos disponibles del usuario.
* Contexto del usuario autenticado.
* Validación de roles por dashboard.

Endpoints creados:

* GET /api/dashboard/client/summary
* GET /api/dashboard/contractor/summary
* GET /api/dashboard/admin/summary
* GET /api/dashboard/modes
* GET /api/dashboard/me

Reglas implementadas:

* Solo usuarios con rol Cliente pueden consultar el dashboard de cliente.
* Solo usuarios con rol Contratista pueden consultar el dashboard de contratista.
* Solo usuarios con rol Administrador pueden consultar el dashboard administrativo.
* Un usuario multirol puede consultar los dashboards correspondientes a sus roles.
* El dashboard de contratista muestra proyectos disponibles y actividad reciente.
* El dashboard de cliente muestra propuestas pendientes y actividad reciente.
* El dashboard administrativo muestra métricas generales del sistema.
* El endpoint de modos informa qué vistas puede usar el usuario autenticado.

Pruebas realizadas:

* [cliente@ofipro.com](mailto:cliente@ofipro.com) consulta client summary → 200 OK.
* [cliente@ofipro.com](mailto:cliente@ofipro.com) consulta contractor summary → 403 Forbidden.
* [cliente@ofipro.com](mailto:cliente@ofipro.com) consulta admin summary → 403 Forbidden.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) consulta contractor summary → 200 OK.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) consulta client summary → 403 Forbidden.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) consulta admin summary → 403 Forbidden.
* [admin@ofipro.com](mailto:admin@ofipro.com) consulta client summary → 200 OK.
* [admin@ofipro.com](mailto:admin@ofipro.com) consulta contractor summary → 200 OK.
* [admin@ofipro.com](mailto:admin@ofipro.com) consulta admin summary → 200 OK.
* GET /api/dashboard/modes probado correctamente.
* GET /api/dashboard/me probado correctamente.

Resultado:

El backend queda preparado para construir pantallas principales de cliente, contratista y administrador sin hacer múltiples llamadas separadas.

Impacto:

OfiPro queda mejor preparado para web responsiva y futura app móvil real con enfoque mobile-first.

---

## HITO 10

ProfessionalProfile y búsqueda básica de contratistas completado.

Incluye:

Creación de DTOs de ProfessionalProfile.
Creación de DTO de búsqueda de contratistas.
Creación de IProfessionalProfileRepository.
Implementación de ProfessionalProfileRepository.
Creación de IProfessionalProfileService.
Implementación de ProfessionalProfileService.
Registro de dependencias en Program.cs.
Creación de ProfessionalProfilesController.
Creación de perfil profesional.
Consulta de perfil profesional propio.
Actualización de perfil profesional propio.
Búsqueda básica de contratistas.
Consulta pública de perfil de contratista por UserId.
Validación de rol Contratista para administrar perfil profesional.
Validación para evitar perfiles profesionales duplicados.
Filtro para devolver solo perfiles activos.
Filtro para devolver solo usuarios activos.
Filtro para devolver solo usuarios con rol Contratista.
Integración del estado del perfil profesional en el dashboard del contratista.

Endpoints creados:

POST /api/professional-profiles
GET /api/professional-profiles/me
PUT /api/professional-profiles/me
GET /api/contractors
GET /api/contractors/{userId}

Endpoints actualizados:

GET /api/dashboard/contractor/summary

Reglas implementadas:

Solo usuarios con rol Contratista pueden crear perfil profesional.
Solo usuarios con rol Contratista pueden consultar y actualizar su perfil profesional.
Un usuario solo puede tener un perfil profesional activo registrado.
Un perfil profesional inactivo no aparece en búsqueda pública.
La búsqueda pública solo muestra usuarios activos y no eliminados.
La búsqueda pública solo muestra usuarios con rol Contratista.
El dashboard del contratista indica si el perfil profesional existe y si está activo.

Pruebas realizadas:

Cliente intentando crear perfil profesional → 403 Forbidden.
Contratista creando perfil profesional → 200 OK.
Contratista consultando su perfil profesional → 200 OK.
Contratista intentando crear perfil duplicado → 400 Bad Request.
Contratista actualizando perfil profesional → 200 OK.
GET /api/contractors sin filtros → 200 OK.
GET /api/contractors con filtro por especialidad → 200 OK.
GET /api/contractors con filtro por ciudad → 200 OK.
GET /api/contractors/{userId} → 200 OK.
Perfil profesional inactivo deja de aparecer en búsqueda.
Perfil profesional reactivado vuelve a aparecer en búsqueda.
Dashboard de contratista muestra HasProfessionalProfile, ProfessionalProfileId, IsProfessionalProfileActive y MainSpecialty.

Resultado:

OfiPro ya cuenta con perfil profesional de contratista y búsqueda básica de contratistas, cerrando el loop inicial de discovery del marketplace.

Impacto:

La plataforma ya no depende únicamente de que el contratista encuentre proyectos. Ahora el cliente también puede buscar contratistas activos por especialidad y ubicación.

---

## HITO 10.1

Corrección de diagnóstico de ProfessionalProfile y búsqueda de contratistas.

Incluye:

* Corrección de N+1 queries en SearchContractorsAsync.
* Creación de método agregado para estadísticas de reputación por lista de usuarios.
* Implementación de GetReputationStatsByUserIdsAsync en RatingRepository.
* Uso de GroupBy por RatedUserId para obtener AverageScore y TotalRatings.
* Actualización de SearchContractorsAsync para evitar consultas de ratings dentro del foreach.
* Verificación de snapshot de EF Core para ProfessionalProfiles.
* Confirmación de que ProfessionalProfiles ya existía desde InitialCreate.
* Confirmación de que no se requiere migración AddProfessionalProfiles.

Pruebas realizadas:

* GET /api/contractors → 200 OK.
* GET /api/contractors?specialty=plomería → 200 OK.
* GET /api/contractors/{userId} → 200 OK.
* Verificación de cambios pendientes de EF Core → sin cambios pendientes.

Resultado:

El Bloque 10 queda endurecido a nivel de rendimiento básico y consistencia de migraciones.

Impacto:

La búsqueda de contratistas queda más limpia, más eficiente y sin deuda inmediata relacionada con ProfessionalProfiles.

---

## HITO 11

Expiración automática de proyectos completada.

Incluye:

Validación de existencia previa de ProjectStatus.Expirado.
Confirmación de que Project.CreatedAt ya existía.
Confirmación de que no se requería migración.
Creación de método ExpirePublishedProjectsAsync en IProjectRepository.
Implementación de expiración masiva en ProjectRepository.
Creación de ProjectExpirationBackgroundService.
Registro del HostedService en Program.cs.
Configuración de ProjectExpiration en appsettings.json.
Ajuste del feed general de proyectos para devolver solo proyectos publicados activos.

Configuración agregada:

ProjectExpiration
ProjectExpiration

Reglas implementadas:

Solo proyectos con estado Publicado pueden expirar automáticamente.
Los proyectos eliminados lógicamente no se procesan.
Los proyectos expirados no aparecen en el feed general.
La expiración se ejecuta al iniciar la API.
La expiración se ejecuta periódicamente según configuración.
No se requiere migración porque los campos necesarios ya existían.

Pruebas realizadas:

Se localizó un proyecto publicado desde SQL Server.
Se forzó su CreatedAt a una fecha antigua.
Se reinició la API.
El BackgroundService ejecutó la expiración.
El proyecto cambió de Status = 1 a Status = 7.
Se inició sesión con contratista@ofipro.com.
GET /api/projects dejó de mostrar el proyecto expirado.

Resultado:

OfiPro ya cuenta con expiración automática de proyectos publicados antiguos.

Impacto:

El feed de proyectos queda más confiable para contratistas y mejor preparado para una experiencia mobile-first.

---

## HITO 12

Paginación y ordenamiento básico en listados críticos completado.
Incluye:

* Creación de DTOs comunes de paginación.
* Creación de PaginationQueryDto.
* Creación de PaginatedResponseDto.
* Organización de DTOs comunes dentro de carpeta Pagination.
* Paginación en GET /api/projects.
* Paginación en GET /api/contractors.
* Paginación en GET /api/notifications.
* Paginación en GET /api/contracts/mine.
* Paginación en GET /api/proposals/my-proposals.
* Paginación en GET /api/projects/my-projects.
* Ordenamiento básico por campos permitidos en proyectos.
* Ordenamiento básico por campos permitidos en contratistas.
* Ordenamiento básico por campos permitidos en notificaciones.
* Ordenamiento básico por campos permitidos en contratos.
* Ordenamiento básico por campos permitidos en propuestas.
* Conteo total de registros para metadata de paginación.
* Corrección de diagnóstico post-Bloque 11.
* Acceso anónimo a endpoints públicos de contratistas.
* Notificación interna cuando un proyecto expira automáticamente.

Endpoints actualizados:

* GET /api/projects
* GET /api/contractors
* GET /api/notifications
* GET /api/contracts/mine
* GET /api/proposals/my-proposals
* GET /api/projects/my-projects
* GET /api/contractors sin token → 200 OK.
* GET /api/contractors/{userId} sin token → 200 OK.
* Proyecto expirado automáticamente genera notificación ProjectExpired → 200 OK.
* Cliente consulta notificaciones y recibe aviso de proyecto expirado → 200 OK.


Estructura de respuesta implementada:

* Items
* PageNumber
* PageSize
* TotalItems
* TotalPages
* HasPreviousPage
* HasNextPage

Reglas implementadas:

* PageNumber inicia en 1.
* PageSize debe estar dentro del rango permitido.
* SortBy se controla por campos permitidos.
* SortDirection permite ordenar ascendente o descendente.
* Los endpoints conservan sus reglas de seguridad existentes.
* Los endpoints conservan filtros de soft delete y estados activos donde aplica.

Pruebas realizadas:

* GET /api/projects paginado → 200 OK.
* GET /api/projects con ordenamiento → 200 OK.
* GET /api/contractors paginado → 200 OK.
* GET /api/contractors con filtro y ordenamiento → 200 OK.
* GET /api/notifications paginado → 200 OK.
* GET /api/notifications con ordenamiento por IsRead → 200 OK.
* GET /api/contracts/mine paginado como cliente → 200 OK.
* GET /api/contracts/mine paginado como contratista → 200 OK.
* GET /api/proposals/my-proposals paginado → 200 OK.
* GET /api/proposals/my-proposals con ordenamiento por Status → 200 OK.
* GET /api/projects/my-projects paginado → 200 OK.
* GET /api/projects/my-projects con ordenamiento por Title → 200 OK.

Resultado:
Bloque 12 completado y probado correctamente.
Impacto:
OfiPro queda mejor preparado para web responsiva y futura app móvil real, evitando respuestas grandes y permitiendo construir pantallas con carga progresiva.

---


# 15. PROBLEMAS DETECTADOS

## P001

Un proyecto puede requerir varios oficios.

Solución:

ProjectRequirement.

---

## P002

Los usuarios pueden cerrar acuerdos fuera de plataforma.

Solución:

Construir reputación como incentivo principal.

---

## P003

Un contratista puede necesitar contratar otro contratista.

Solución:

Todos son Users.

---

## P004

EF Core 10 incompatible con .NET 8.

Solución:

Migrar a EF Core 8.0.27.

---

## P005

HasColumnType no reconocido.

Causa:

Referencias EF Core incompletas.

Solución:

Agregar paquetes faltantes.

---

## P006

Add-Migration ejecutado desde Developer PowerShell.

Solución:

Usar la Consola del Administrador de paquetes.

---

## P007

RatingConfiguration e InvitationConfiguration utilizaban propiedades inexistentes.

Solución:

Validar nombres contra las entidades reales antes de generar configuraciones.

---

## P008

Git inicializado fuera de la raíz del proyecto.

Solución:

Reinicializar Git en la carpeta correcta.

---

---

## P009

Proposal mantenía una relación obsoleta con Project.

Síntomas:

* Existía ProjectId en Proposals.
* Existía FK_Proposals_Projects_ProjectId.
* Las propuestas no estaban correctamente asociadas a ProjectRequirement.

Solución:

Migrar Proposal para relacionarse exclusivamente con ProjectRequirement.
Eliminar la relación antigua.
Generar migración correctiva.

---

## P010

JWT Key expuesta en appsettings.json.

Riesgo:

* La clave podía quedar visible en GitHub.
* Cualquier persona con acceso al repositorio podía conocer la clave usada para firmar tokens.

Solución:

Mover la clave JWT fuera de appsettings.json.
Configurar User Secrets para entorno local.

---

## P011

Aceptar y rechazar propuestas no validaba correctamente al propietario del proyecto.

Riesgo:

* Un usuario autenticado podía intentar manipular propuestas de proyectos ajenos.

Solución:

Validar Project.CreatedByUserId contra el UserId del token antes de aceptar o rechazar propuestas.

---

## P012

Excepciones de negocio respondían como error 500.

Riesgo:

* Errores de permisos, datos inválidos o recursos inexistentes no devolvían códigos HTTP adecuados.

Solución:

Crear excepciones personalizadas.
Implementar middleware global de excepciones.

---

## P013

Los DTOs de actualización no tenían validaciones.

Riesgo:

* Se podían enviar títulos vacíos.
* Se podían enviar precios negativos.
* Se podían enviar textos incompletos.

Solución:

Agregar DataAnnotations a todos los DTOs Update.

---

## P014

AuthController interceptaba excepciones manualmente.

Riesgo:

Las respuestas no seguían el formato estándar del middleware global.

Solución:

Eliminar try/catch redundantes.

---

## P015

La expiración JWT estaba duplicada y desincronizada.

Riesgo:

Inconsistencia entre configuración y respuesta del login.

Solución:

Unificar expiración.

---

## P016

La hora guardada en base de datos puede parecer incorrecta.

Síntoma:

Al revisar registros en SQL Server, CreatedAt puede mostrar una hora distinta a la hora local del usuario.

Ejemplo:

* SQL Server muestra 2026-06-21 20:24.
* Hora local México corresponde aproximadamente a 2026-06-21 14:24.

Causa:

El backend guarda fechas en UTC mediante DateTime.UtcNow.

Solución:

Mantener UTC en base de datos.

La conversión a hora local debe hacerse únicamente al mostrar la fecha en frontend o app móvil.

Resultado:

No se cambia a DateTime.Now.

Razón:

Guardar fechas en UTC evita problemas futuros con zonas horarias, app móvil, usuarios en distintas regiones y consistencia de datos.

---

## P017

El modelo inicial de Rating no estaba alineado con el flujo real de contrataciones.

Síntoma:

Rating estaba orientado a Project y no a Contract.

Riesgo:

* Una calificación podía quedar desconectada del trabajo realmente contratado.
* No quedaba claro si la calificación correspondía a una propuesta, proyecto o contratación terminada.
* No se podía controlar correctamente la regla de una calificación por dirección.

Solución:

Modificar Rating para relacionarse directamente con Contract.

Resultado:

La reputación queda ligada a contrataciones finalizadas.

---

## P018

La reputación pública podía exponer información interna innecesaria.

Riesgo:

Si se reutilizaba el DTO interno de Rating para perfiles públicos, la API podía exponer:

* ContractId
* RaterUserId
* RatedUserId

Solución:

Crear DTOs públicos separados para reputación y ratings recibidos.

Resultado:

El perfil público muestra información útil sin exponer identificadores internos de contratación.

---

## P019

Faltaba notificación cuando un usuario recibía una calificación.

Riesgo:

El usuario calificado podía recibir una nueva evaluación sin enterarse dentro del sistema.

Solución:

Agregar NotificationType.RatingReceived y generar la notificación desde RatingService.CreateAsync.

Resultado:

El evento de calificación recibida queda cubierto por el sistema de notificaciones internas.

---

## P020

Existía lógica duplicada entre GetUserReputationAsync y GetPublicUserReputationAsync.

Riesgo:

Cambios futuros en el cálculo de reputación podían aplicarse en un método y olvidarse en el otro.

Solución:

Extraer lógica común a métodos privados para:

* Obtener usuario activo.
* Calcular promedio, total y última calificación.
* Mapear ratings públicos.

Resultado:

El módulo de reputación queda más limpio y mantenible.

---

## P021

Las pruebas de dashboard se confundían por usuarios multirol.

Síntoma:

[cliente@ofipro.com](mailto:cliente@ofipro.com) podía consultar el dashboard de contratista porque tenía rol Contratista agregado durante pruebas anteriores.

Causa:

El usuario ya no representaba un cliente puro.

Solución:

Reorganizar roles de prueba:

* [cliente@ofipro.com](mailto:cliente@ofipro.com) queda solo como Cliente.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) queda solo como Contratista.
* [admin@ofipro.com](mailto:admin@ofipro.com) queda como usuario multirol.

Resultado:

Las pruebas de autorización de dashboard quedan claras y confiables.

---

## P022

El conteo de usuarios por rol en dashboard administrativo no filtraba usuarios eliminados.

Síntoma:

TotalUsers filtraba DeletedAt == null, pero TotalClients, TotalContractors y TotalAdmins contaban roles aunque el usuario estuviera eliminado lógicamente.

Riesgo:

El dashboard administrativo podía mostrar métricas infladas si existían usuarios eliminados con roles asignados.

Solución:

Agregar filtro por User.DeletedAt == null en los conteos de UserRoles dentro de DashboardRepository.GetAdminSummaryAsync.

Resultado:

Las métricas administrativas de usuarios por rol ahora respetan soft delete.

---

## P023

El conteo de usuarios por rol en dashboard administrativo no filtraba usuarios eliminados.

Síntoma:

TotalUsers filtraba DeletedAt == null, pero TotalClients, TotalContractors y TotalAdmins podían contar roles aunque el usuario estuviera eliminado lógicamente.

Riesgo:

El dashboard administrativo podía mostrar métricas infladas si existían usuarios eliminados con roles asignados.

Solución:

Agregar filtro por User.DeletedAt == null en los conteos de UserRoles dentro de DashboardRepository.GetAdminSummaryAsync.

Resultado:

Las métricas administrativas de usuarios por rol ahora respetan soft delete.

---

## P024

No existía descubrimiento activo de contratistas.

Síntoma:

El cliente podía publicar proyectos y recibir propuestas, pero no tenía forma de buscar contratistas directamente.

Riesgo:

La plataforma dependía demasiado de que los contratistas encontraran proyectos publicados. Esto podía reducir la actividad inicial del marketplace.

Solución:

Implementar ProfessionalProfile y búsqueda básica de contratistas mediante GET /api/contractors.

Resultado:

El cliente ya puede descubrir contratistas por especialidad, estado y ciudad.

---

## P025

SearchContractorsAsync tenía riesgo de N+1 queries.

Síntoma:

La búsqueda de contratistas obtenía primero los perfiles profesionales y después, dentro de un foreach, consultaba las calificaciones de cada contratista de forma individual.

Riesgo:

Con pocos usuarios era aceptable, pero con más resultados podía generar demasiadas consultas a base de datos.

Ejemplo:

* 5 contratistas → 1 query de búsqueda + 5 queries de ratings.
* 50 contratistas → 1 query de búsqueda + 50 queries de ratings.

Solución:

Se agregó un método agregado en IRatingRepository para obtener estadísticas de reputación por lista de usuarios en una sola consulta:

GetReputationStatsByUserIdsAsync(List<Guid> userIds)

Resultado:

SearchContractorsAsync ahora obtiene los perfiles profesionales y después consulta los promedios y totales de ratings en una sola operación agrupada por RatedUserId.

Impacto:

La búsqueda de contratistas queda mejor preparada para crecer sin multiplicar consultas innecesarias.

---

## P026

No existe una migración separada AddProfessionalProfiles.

Síntoma:

La tabla ProfessionalProfiles no tiene una migración propia llamada AddProfessionalProfiles.

Análisis:

Esto no representa un error porque ProfessionalProfiles ya formaba parte del esquema inicial desde InitialCreate.

Riesgo:

Si el snapshot de EF Core estuviera desincronizado con ProfessionalProfileConfiguration, podrían aparecer cambios inesperados en migraciones futuras.

Verificación:

Se ejecutó la verificación de cambios pendientes del modelo de EF Core.

Resultado:

EF Core no detectó cambios pendientes. El snapshot está sincronizado con el modelo actual.

Impacto:

No se requiere migración adicional para ProfessionalProfiles.

---

## P027

Los proyectos publicados antiguos podían permanecer visibles indefinidamente.

Síntoma:

ProjectStatus ya tenía el valor Expirado, pero no existía un proceso automático que cambiara proyectos antiguos de Publicado a Expirado.

Riesgo:

El feed del contratista podía llenarse de proyectos fantasma, es decir, proyectos que seguían visibles aunque ya no fueran oportunidades realmente vigentes.

Solución:

Se implementó ProjectExpirationBackgroundService para ejecutar la expiración de proyectos publicados antiguos.

También se agregó en IProjectRepository el método:

ExpirePublishedProjectsAsync(DateTime expirationLimitUtc)

Y se implementó en ProjectRepository usando actualización masiva.

Resultado:

Los proyectos publicados con antigüedad mayor al límite configurado cambian automáticamente a ProjectStatus.Expirado.

Impacto:

GET /api/projects deja de mostrar proyectos expirados, reduciendo ruido en el feed y preparando mejor el backend para web responsiva y app móvil real.

---

## P028

Los listados críticos podían crecer sin control.

Síntoma:

Varios endpoints devolvían listas completas sin paginación.

Endpoints afectados:

* GET /api/projects
* GET /api/contractors
* GET /api/notifications
* GET /api/contracts/mine
* GET /api/proposals/my-proposals
* GET /api/projects/my-projects

Riesgo:

Con más usuarios, proyectos, propuestas, contratos y notificaciones, las respuestas podían volverse pesadas para web responsiva, app móvil y base de datos.

Solución:

Se implementó paginación y ordenamiento básico mediante DTOs comunes de paginación:

PaginationQueryDto
PaginatedResponseDto<T>

Resultado:

Los endpoints críticos ahora devuelven respuestas paginadas y metadata útil para construir interfaces web y móviles.

Impacto:

El backend queda mejor preparado para consumo real desde frontend y futura app móvil.

---

## P029

Los perfiles públicos de contratistas requerían autenticación.

Síntoma:

GET /api/contractors y GET /api/contractors/{userId} heredaban autorización del controlador y respondían 401 Unauthorized si el usuario no enviaba token.

Riesgo:

Un perfil de contratista compartido por WhatsApp, redes sociales o enlace directo no podía abrirse públicamente, generando fricción en el flujo de discovery.

Solución:

Agregar AllowAnonymous a los endpoints públicos de búsqueda y consulta de contratistas.

Resultado:

Los perfiles públicos de contratistas pueden consultarse sin iniciar sesión.

---

## P030

La expiración automática de proyectos no notificaba al dueño del proyecto.

Síntoma:

El BackgroundService marcaba proyectos antiguos como Expirado, pero el usuario que creó el proyecto no recibía ningún aviso.

Riesgo:

El cliente podía pensar que su proyecto desapareció por error o bug.

Solución:

Agregar NotificationType.ProjectExpired y generar una notificación interna al dueño del proyecto cuando la expiración automática ocurre.

Resultado:

El cliente recibe una notificación cuando su proyecto expira automáticamente.

---


# 16. RIESGOS

## R001

Aumento constante de alcance.

Mitigación:

Backlog obligatorio.

---

## R002

Abandono del proyecto por pérdida de interés.

Mitigación:

Seguimiento por sesión.

Documentación viva.

Objetivos pequeños.

---

# 17. BACKLOG

* Múltiples especialidades
* IA
* Chat
* App móvil
* Ferreterías
* Pagos
* Empresas avanzadas
* Geolocalización
* Verificaciones
* Contratos digitales
* Facturación

---

# 19. HITOS COMPLETADOS

## HITO 1

Fundación del proyecto completada.

Incluye:

* Arquitectura Clean Architecture.
* Entidades Domain.
* Enums.
* EF Core.
* ApplicationDbContext.
* Configuraciones EF.
* Migración InitialCreate.
* Base de datos OfiProDb.
* Repositorio GitHub.
* Documentación base.

---

## HITO 2

Auth completado.

Incluye:

* Register.
* Login.
* JWT.
* Swagger Authorize.
* Endpoint protegido.
* Roles funcionando.

---

## HITO 3

Usuarios completado.

Incluye:

* Obtener perfil.
* Actualizar perfil.
* Endpoints administrativos.
* Soft Delete.
* Seeder automático de administrador.

---

## HITO 4

Proyectos completado.

Incluye:

* Crear proyecto.
* Consultar proyectos.
* Consultar mis proyectos.
* Consultar proyecto por Id.
* Actualizar datos generales.
* Actualizar requerimientos por endpoint separado.
* Eliminar proyecto con Soft Delete.
* Seeder de categorías y subcategorías.

---

## HITO 5

Propuestas completado.

Incluye:

* Crear propuesta.
* Actualizar propuesta.
* Consultar mis propuestas.
* Consultar propuesta por Id.
* Retirar propuesta.
* Rechazar propuesta.
* Aceptar propuesta.
* Rechazo automático de propuestas competidoras.
* Migración Proposal → ProjectRequirement.
* Refactor SaveChangesAsync en repositories.

---

## HITO 5.5

Seguridad y Calidad Base completado.

Incluye:

* JWT Key removida de appsettings.json.
* JWT Key configurada mediante User Secrets.
* Autorización reforzada en aceptar propuesta.
* Autorización reforzada en rechazar propuesta.
* Validaciones base en DTOs de proyectos.
* Validaciones base en DTOs de requerimientos.
* Validaciones base en DTOs de propuestas.
* Excepciones personalizadas.
* Middleware global de excepciones.
* Respuestas HTTP correctas para errores 400, 403 y 404.

---

## HITO 5.6

Limpieza de Consistencia API completado.

Incluye:

* Reemplazo de excepciones genéricas.
* Eliminación de try/catch redundantes.
* Validaciones en DTOs Update.
* Confirmación de filtro Soft Delete en proyectos.
* Unificación de expiración JWT.
* Protección de TestController.

---

## HITO 6

Contrataciones completado funcionalmente.

Incluye:

* Entidad Contract.
* Enum ContractStatus.
* Tabla Contracts.
* ContractRepository.
* ContractService.
* ContractsController.
* Creación automática de contratación al aceptar propuesta.
* Consulta de mis contrataciones.
* Consulta de contratación por Id.
* Cambio de estados.
* Validación de permisos por cliente y contratista.
* Cancelación de contrataciones.
* Bloqueo de cambios en contrataciones finalizadas o canceladas.

---

## HITO 6.8

Refactor de nombres descriptivos en DTOs completado.

Incluye:

* UserProfileDto usa UserId.
* ProjectDto usa ProjectId.
* ProjectRequirementDto usa ProjectRequirementId.
* ProposalDto usa ProposalId.
* ContractDto usa ContractId.
* Mapeos actualizados.
* Endpoints principales probados correctamente.

Razón:

Facilitar pruebas, depuración, consultas SQL y consumo desde frontend.

---

## HITO 6.9

Flujo mínimo de Contratista completado.

Incluye:

* Validación de rol Contratista al crear propuestas.
* Métodos HasRoleAsync y AddRoleAsync en UserRepository.
* Endpoint PATCH /api/users/activate-contractor.
* Activación de rol Contratista para usuario autenticado.
* Pruebas de autorización en creación de propuestas.

Resultado:

Un usuario Cliente puede activar su rol Contratista y, a partir de eso, enviar propuestas.

---

## HITO 6.10

Orden de interfaces de Application completado.

Incluye:

* Interfaces/Repositories.
* Interfaces/Services.
* Namespaces actualizados.
* Compilación validada.
* Pruebas básicas correctas después del refactor.

Resultado:

La capa Application queda mejor organizada para continuar con Evidencias, Ratings y Perfil Profesional.

---

## HITO 6.11

Correcciones de diagnóstico pre-Bloque 7 completadas.

Incluye:

* Eliminación de filtro redundante DeletedAt == null en UserService.GetAllAsync.
* Validación de propietario en GET /api/proposals/requirement/{projectRequirementId}.
* Nuevo método GetProjectRequirementOwnerUserIdAsync en IProposalRepository.
* Implementación de GetProjectRequirementOwnerUserIdAsync en ProposalRepository.
* Actualización de ProposalService.GetByProjectRequirementAsync para recibir userId.
* Restricción para que solo el dueño del proyecto pueda consultar propuestas de un requerimiento.

Resultado:

Se eliminó código muerto y se cerró un hueco de seguridad donde un contratista podía consultar propuestas de competidores.

Pruebas realizadas:

* Dueño del proyecto consultando propuestas del requerimiento → 200 OK.
* Usuario no dueño consultando propuestas del requerimiento → 403 Forbidden.
* Requerimiento inexistente → 404 Not Found.

---

## HITO 7

Evidencias V1 completado funcionalmente.

Incluye:

* Creación de entidad Evidence.
* Creación de tabla Evidences mediante migración.
* Configuración EF de Evidence.
* Creación de EvidenceDto.
* Creación de CreateEvidenceDto.
* Creación de IEvidenceRepository.
* Implementación de EvidenceRepository.
* Creación de IEvidenceService.
* Implementación de EvidenceService.
* Registro de dependencias en Program.cs.
* Creación de EvidencesController.
* Endpoint POST /api/contracts/{contractId}/evidences.
* Endpoint GET /api/contracts/{contractId}/evidences.
* Endpoint DELETE /api/evidences/{evidenceId}.
* Validación de permisos para subir evidencias.
* Validación de permisos para consultar evidencias.
* Eliminación lógica de evidencias mediante DeletedAt.

Reglas implementadas:

* Solo el contratista asignado al contrato puede subir evidencias.
* Cliente y contratista pueden consultar evidencias del contrato.
* Usuarios ajenos no pueden consultar evidencias.
* El cliente no puede subir evidencias.
* No se pueden subir evidencias a contratos finalizados o cancelados.
* No se pueden eliminar evidencias de contratos finalizados o cancelados.
* Solo quien subió la evidencia puede eliminarla.

Pruebas realizadas:

* Contratista subiendo evidencia → 200 OK.
* Evidencia guardada correctamente en BD.
* Contratista consultando evidencias → 200 OK.
* Cliente dueño consultando evidencias → 200 OK.
* Cliente intentando subir evidencia → 403 Forbidden.
* Eliminación de evidencia por contratista → 204 No Content.
* Evidencia eliminada deja de aparecer en consultas.
* DeletedAt se asigna correctamente en BD.

Resultado:

El flujo Proyecto → Requerimiento → Propuesta → Contrato → Evidencia quedó funcionando de forma completa en V1.

---

## HITO 7.1

Corrección de diagnóstico de Evidencias completada.

Incluye:

* Se integró EvidenceType a la entidad Evidence.
* Se agregó EvidenceType a EvidenceDto.
* Se agregó EvidenceType a CreateEvidenceDto.
* Se configuró EvidenceType en EF como entero.
* Se generó y aplicó migración AddEvidenceTypeToEvidences.
* Se corrigió el problema de enum huérfano.
* Se validó FileType contra una lista de valores permitidos.
* Se normalizó FileType con Trim y ToLowerInvariant.
* Se validaron casos correctos e incorrectos desde Swagger.

Reglas agregadas:

* EvidenceType es obligatorio.
* EvidenceType solo permite valores definidos en el enum.
* FileType solo permite image/jpeg, image/png o application/pdf.

Pruebas realizadas:

* Evidencia con EvidenceType válido y FileType válido → 200 OK.
* Evidencia con FileType inválido → 400 Bad Request.
* Evidencia con EvidenceType inválido → 400 Bad Request.
* Evidencia creada correctamente aparece en BD con EvidenceType.
* Evidencia consultada devuelve EvidenceType en la respuesta.

Resultado:

El módulo de Evidencias V1 queda más completo, seguro y preparado para una futura app móvil y carga real de archivos.

---

## HITO 7.2

Notificaciones internas base completado.

Incluye:

* Creación de entidad Notification.
* Creación de enum NotificationType.
* Configuración EF de Notification.
* Creación de tabla Notifications mediante migración.
* Creación de NotificationDto.
* Creación de CreateNotificationDto.
* Creación de INotificationRepository.
* Implementación de NotificationRepository.
* Creación de INotificationService.
* Implementación de NotificationService.
* Registro de dependencias en Program.cs.
* Creación de NotificationsController.
* Endpoints protegidos con JWT para consultar y administrar notificaciones.
* Soft delete de notificaciones mediante DeletedAt.
* Contador de notificaciones no leídas.
* Marcado individual de notificación como leída.
* Marcado masivo de notificaciones como leídas.

Endpoints creados:

* GET /api/notifications
* GET /api/notifications/unread
* GET /api/notifications/unread-count
* PATCH /api/notifications/{notificationId}/read
* PATCH /api/notifications/read-all
* DELETE /api/notifications/{notificationId}

Eventos conectados:

* Contratista envía propuesta → cliente recibe notificación.
* Cliente acepta propuesta → contratista aceptado recibe notificación.
* Cliente rechaza propuesta → contratista recibe notificación.
* Cliente acepta una propuesta → contratistas pendientes rechazados automáticamente reciben notificación.
* Contratista sube evidencia → cliente recibe notificación.
* Contratista inicia contratación → cliente recibe notificación.
* Contratista marca contratación como pendiente de confirmación → cliente recibe notificación.
* Cliente finaliza contratación → contratista recibe notificación.
* Cliente o contratista cancela contratación → la otra parte recibe notificación.

Reglas implementadas:

* Las notificaciones se consultan usando el UserId del JWT.
* Un usuario solo puede consultar sus propias notificaciones.
* Un usuario solo puede marcar como leídas sus propias notificaciones.
* Un usuario solo puede eliminar sus propias notificaciones.
* Las notificaciones eliminadas no aparecen en consultas.
* Las notificaciones no se crean desde endpoint público.
* Las notificaciones se generan desde servicios de negocio.
* Las fechas se guardan en UTC.

Pruebas realizadas:

* Inserción manual de notificación de prueba en SQL Server → consulta correcta desde Swagger.
* GET /api/notifications → 200 OK.
* GET /api/notifications/unread → 200 OK.
* GET /api/notifications/unread-count → 200 OK.
* PATCH /api/notifications/{notificationId}/read → 200 OK.
* PATCH /api/notifications/read-all → 200 OK.
* DELETE /api/notifications/{notificationId} → 204 No Content.
* Soft delete validado en BD.
* Contratista crea propuesta → cliente recibe notificación.
* Cliente acepta propuesta → contratista recibe notificación.
* Cliente rechaza propuesta → contratista recibe notificación.
* Contratista sube evidencia → cliente recibe notificación.
* Contratista cambia contrato a EnProceso → cliente recibe notificación.
* Contratista cambia contrato a PendienteConfirmacion → cliente recibe notificación.
* Cliente cambia contrato a Finalizado → contratista recibe notificación.

Resultado:

El backend de OfiPro ya cuenta con un módulo funcional de notificaciones internas conectado a eventos reales del marketplace.

Impacto:

El sistema queda mejor preparado para consumo mobile-first y para una futura app móvil real con push notifications.

---

## HITO 8

Calificaciones y reputación V1 completado.

Incluye:

* Refactor de entidad Rating para asociarse a Contract.
* Configuración EF de Rating por contratación.
* Migración para actualizar Ratings.
* Creación de CreateRatingDto.
* Creación de RatingDto.
* Creación de UserReputationDto.
* Creación de IRatingRepository.
* Implementación de RatingRepository.
* Creación de IRatingService.
* Implementación de RatingService.
* Registro de dependencias en Program.cs.
* Creación de RatingsController.

Endpoints creados:

* POST /api/contracts/{contractId}/ratings
* GET /api/contracts/{contractId}/ratings
* GET /api/users/{userId}/reputation

Reglas implementadas:

* Solo se puede calificar una contratación finalizada.
* Solo cliente y contratista de la contratación pueden calificar.
* Un usuario no puede calificarse a sí mismo.
* Cliente califica automáticamente al contratista.
* Contratista califica automáticamente al cliente.
* Solo puede existir una calificación por dirección por contratación.
* Score solo permite valores de 1 a 5.

Pruebas realizadas:

* FileUrl inválido en evidencias se mantuvo validado con 400 Bad Request antes de iniciar Ratings.
* Evidencia en PendienteInicio se mantuvo bloqueada con 400 Bad Request.
* Evidencia en EnProceso funcionó correctamente.
* Cliente calificando contratista en contrato Finalizado → 200 OK.
* Contratista calificando cliente en contrato Finalizado → 200 OK.
* Consulta de ratings por contrato → 200 OK.
* Verificación en SQL Server de ratings creados correctamente.
* Consulta de reputación por usuario → 200 OK.

Resultado:

OfiPro ya puede construir reputación básica a partir de contrataciones finalizadas.

---

## HITO 8.1

Endurecimiento de Ratings y reputación completado.

Incluye:

* Reputación con fecha de última calificación recibida.
* Historial interno de calificaciones recibidas por usuario.
* DTO público para ratings recibidos.
* DTO público de reputación completa.
* Endpoint público limpio de ratings recibidos.
* Endpoint público completo de reputación para perfil.
* Separación entre información interna y pública.

Endpoints creados:

* GET /api/users/{userId}/ratings/received
* GET /api/users/{userId}/ratings/public
* GET /api/users/{userId}/reputation/public

Reglas implementadas:

* El historial interno puede mostrar datos completos de Rating.
* La vista pública no expone ContractId.
* La vista pública no expone RaterUserId.
* La vista pública no expone RatedUserId.
* La reputación pública devuelve promedio, total, última fecha y comentarios recibidos.

Pruebas realizadas:

* GET /api/users/{userId}/reputation con LastRatingAt → 200 OK.
* GET /api/users/{userId}/ratings/received → 200 OK.
* GET /api/users/{userId}/ratings/public → 200 OK.
* GET /api/users/{userId}/reputation/public → 200 OK.
* Respuestas públicas validadas sin IDs internos innecesarios.

Resultado:

La reputación queda lista para perfiles públicos en web responsiva y futura app móvil real.

Impacto:

OfiPro fortalece su propuesta de confianza con historial visible, promedio de calificaciones y comentarios públicos controlados.

---


## ESTADO ACTUAL ACTUALIZADO

Módulos completados:

* Bloque 1 - Fundación
* Bloque 2 - Auth
* Bloque 3 - Usuarios
* Bloque 4 - Proyectos
* Bloque 5 - Propuestas
* Bloque 5.5 - Seguridad y Calidad Base
* Bloque 5.6 - Limpieza de Consistencia API
* Bloque 6 - Contrataciones
* Bloque 6.8 - Refactor de nombres descriptivos en DTOs
* Bloque 6.9 - Flujo mínimo de Contratista
* Bloque 6.10 - Orden de interfaces Application
* Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7
* Bloque 7 - Evidencias V1
* Bloque 7.1 - Corrección de diagnóstico de Evidencias
* Bloque 7.2 - Notificaciones internas base
* Bloque 8 - Calificaciones y reputación V1
* Bloque 8.1 - Endurecimiento de Ratings y reputación
* Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación
* Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web
* Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas
* Bloque 11 - Expiración automática de proyectos
* Bloque 12 - Paginación y ordenamiento básico en listados críticos

Próximo bloque recomendado:

* Bloque 13 - Pruebas automatizadas mínimas de API

Razón:

El backend ya tiene varios flujos críticos funcionando. Antes de seguir agregando funcionalidad, conviene crear una red mínima de pruebas automatizadas para detectar errores cuando se modifiquen contratos de API, seguridad, autenticación, paginación o reglas de negocio.

Pruebas iniciales sugeridas:

* Login correcto.
* Login inválido.
* Endpoint protegido sin token.
* Endpoint protegido con rol incorrecto.
* GET /api/projects paginado.
* GET /api/contractors paginado.
* GET /api/notifications paginado.
* Flujo mínimo de proyecto/propuesta/contrato si el alcance del bloque lo permite.

Opciones posteriores:

* Invitaciones directas a contratistas.
* Tests de integración mínimos.
* Refresh tokens para experiencia móvil.
* Carga real de archivos para evidencias.
* FCM Token y push notifications cuando exista app móvil real.
* Panel administrativo operativo.
* Web responsiva.
* App móvil real en etapa pre-lanzamiento.

Notas estratégicas vigentes:

* No desarrollar PWA.
* No lanzar OfiPro solo con web.
* Mantener enfoque mobile-first.
* Preparar endpoints para consumo web responsivo y app móvil real.
* Mantener DateTime.UtcNow para fechas internas.
* No implementar push notifications reales hasta tener app móvil real.
* Mantener usuarios de prueba separados por escenario:

  * [cliente@ofipro.com](mailto:cliente@ofipro.com) como Cliente puro.
  * [contratista@ofipro.com](mailto:contratista@ofipro.com) como Contratista puro.
  * [admin@ofipro.com](mailto:admin@ofipro.com) como usuario multirol.

