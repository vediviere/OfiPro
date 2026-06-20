# OFIPRO MASTER DOCUMENT

Versión: 1.1

Fecha de creación: 2026-06-08

Última actualización: 2026-06-09

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
* ProjectPhoto
* Proposal
* Evidence
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


# 18. ESTADO ACTUAL

Arquitectura:
Completada

Entidades:
Completadas

Configuraciones EF:
Completadas

Migración inicial:
Completada

Base de datos:
Completada

JWT:
Completado y reforzado

Usuarios:
Completado

Proyectos:
Completado

Propuestas:
Completado y reforzado

Bloques completados:

* Bloque 1 - Fundación
* Bloque 2 - Auth
* Bloque 3 - Usuarios
* Bloque 4 - Proyectos
* Bloque 5 - Propuestas
* Bloque 5.5 - Seguridad y Calidad Base
* * Bloque 5.6 - Limpieza de Consistencia API
* Bloque 6 - Contrataciones
* * Bloque 6.8 - Refactor de nombres descriptivos en DTOs
* * Bloque 6.9 - Flujo mínimo de Contratista
* * Bloque 6.10 - Orden de interfaces Application

Próximo bloque:

* Bloque 7 - Evidencias
