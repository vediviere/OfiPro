# OFIPRO MASTER DOCUMENT

Versión: 1.2

Fecha de creación: 2026-06-08

Última actualización: 2026-06-21

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
5. Califique al contratista.

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

Próximo bloque recomendado:

* Bloque 8 - Calificaciones y reputación V1

Opciones posteriores:

* Perfil profesional del contratista
* Carga real de archivos para evidencias
* Mejoras de flujo de contratación
* App móvil real en etapa posterior

Notas estratégicas vigentes:

* No desarrollar PWA.
* Mantener enfoque mobile-first.
* Preparar endpoints para consumo web responsivo y app móvil real.
