=====================================
SESIÓN #1
Fecha: 2026-06-08
=================

Objetivo:

Validar la idea de negocio y definir el alcance inicial.

Completado:

✔ Se analizó la viabilidad del proyecto.
✔ Se eligió el modelo marketplace.
✔ Se identificó el problema de confianza.
✔ Se definió la propuesta de valor.
✔ Se identificaron riesgos del negocio.
✔ Se discutió el problema de acuerdos fuera de plataforma.
✔ Se decidió usar reputación como incentivo principal.

Decisiones importantes:

* El proyecto es viable.
* Se desarrollará.
* Se priorizará crecimiento y rentabilidad.
* Se trabajará por versiones cerradas.

Pendientes:

□ Definir arquitectura.
□ Definir entidades.
□ Crear solución.

Estado:

🟢 En tiempo.

=====================================
SESIÓN #2
Fecha: 2026-06-08
=================

Objetivo:

Construir la base técnica del proyecto.

Completado:

✔ Clean Architecture seleccionada.
✔ Monolito modular seleccionado.
✔ Solución creada.
✔ Proyectos creados:
- OfiPro.Api
- OfiPro.Application
- OfiPro.Domain
- OfiPro.Infrastructure

✔ Entidades principales creadas.
✔ Enums creados.
✔ ProjectRequirement implementado.
✔ ApplicationDbContext creado.
✔ EF Core configurado.
✔ OFIPRO_MASTER.md creado.
✔ SESSION_LOG.md creado.
✔ Repositorio GitHub creado.
✔ Primer commit realizado.
✔ Estructura física corregida.

Problemas encontrados:

1. EF Core 10 incompatible con .NET 8.
   Solución:
   Migrar a EF Core 8.0.27.

2. HasColumnType no reconocido.
   Solución:
   Agregar referencias EF Core faltantes.

3. Git inicializado en carpeta incorrecta.
   Solución:
   Reinicializar Git en la raíz real del proyecto.

Decisiones importantes:

* No usar microservicios.
* Todos los usuarios pueden publicar proyectos.
* ProfessionalProfile tendrá una sola especialidad.
* Se utilizará ProjectRequirement para múltiples necesidades.
* Se utilizará documentación viva.

Pendientes:

□ RatingConfiguration.
□ InvitationConfiguration.
□ InitialCreate.
□ Base de datos.
□ JWT.
□ Usuarios.
□ Proyectos.
□ Propuestas.

Estado:

🟢 En tiempo.

=====================================
SESIÓN #3
Fecha: 2026-06-09
=================

Objetivo:

Completar el Bloque 1 (Fundación) y generar la primera base de datos funcional.

Completado:

✔ ProjectRequirementConfiguration.
✔ ProjectPhotoConfiguration.
✔ ProposalConfiguration.
✔ EvidenceConfiguration.
✔ RatingConfiguration.
✔ InvitationConfiguration.

✔ Todas las configuraciones EF completadas.

✔ Migración creada:
InitialCreate

✔ Snapshot generado.

✔ Base de datos creada:
OfiProDb

✔ Tablas generadas correctamente en SQL Server.

✔ Repositorio GitHub configurado correctamente.

✔ Se validó que el Bloque 1 quedó terminado.

Problemas encontrados:

1. Add-Migration no reconocido.
   Causa:
   Uso de Developer PowerShell.
   Solución:
   Utilizar la Consola del Administrador de paquetes.

2. RatingConfiguration e InvitationConfiguration utilizaban propiedades inexistentes.
   Causa:
   Nombres no alineados con Domain.
   Solución:
   Corregir utilizando las propiedades reales de las entidades.

3. Git inicializado fuera de la raíz del proyecto.
   Solución:
   Reinicializar repositorio en la carpeta correcta.

Decisiones importantes:

* Validar requisitos antes de ejecutar comandos.
* Validar nombres contra Domain antes de generar configuraciones.
* Cerrar bloques formalmente antes de iniciar otros.

Resultado:

BLOQUE 1 - FUNDACIÓN
COMPLETADO ✅

Pendientes:

□ Commit final del Bloque 1.
□ Iniciar Bloque 2 - Auth.

Estado:

🟢 En tiempo.

# =====================================
# SESIÓN 2026-06-12

## Objetivo

Completar Bloque 2 - Auth.
# =====================================

## Completado

- Se creó JwtSettings.
- Se configuró JWT en appsettings.json.
- Se implementó IJwtService.
- Se implementó JwtService.
- Se configuró Authentication.
- Se configuró Authorization.
- Se creó IUserRepository.
- Se implementó UserRepository.
- Se implementó AuthService.
- Se creó AuthController.
- Se implementó Register.
- Se implementó Login.
- Se configuró Swagger Authorize.
- Se creó TestController protegido.

## Pruebas realizadas

### Register

Resultado:

200 OK

### Login

Resultado:

200 OK

### Endpoint protegido

GET /api/Test/secure

Resultado:

200 OK

## Decisiones tomadas

- JWT contendrá únicamente:
  - UserId
  - Email
  - Role

- Todo usuario nuevo se registra como Cliente.

- User tendrá colección UserRoles.

- BCrypt se implementa en Infrastructure.

- Repository Pattern será obligatorio para acceso a datos.

## Estado del proyecto

Bloque 1: Completo

Bloque 2: Completo

Bloque 3: Pendiente

## Próximo paso

Iniciar Bloque 3 - Usuarios.


# =====================================
# SESIÓN 2026-06-14

## Objetivo

Completar Bloque 2 (Auth) y Bloque 3 (Usuarios).

# =====================================

## Auth

Completado:

* JwtSettings
* IJwtService
* JwtService
* IAuthService
* AuthService
* IUserRepository
* UserRepository
* AuthController
* Register
* Login
* JWT Authentication
* JWT Authorization
* Swagger Authorize

Pruebas:

* POST /api/auth/register → 200
* POST /api/auth/login → 200
* GET /api/test/secure → 200

Resultado:

JWT funcionando correctamente.

## Usuarios

Completado:

* UserProfileDto
* UpdateUserProfileDto
* IUserService
* UserService
* GET /api/users/profile
* PUT /api/users/profile

Pruebas:

* GET profile → 200
* PUT profile → 200

## Administración

Completado:

* GET /api/users
* GET /api/users/{id}
* PATCH activate
* PATCH deactivate
* DELETE soft delete

Pruebas:

* GET /api/users → 200

## Seeder

Completado:

* ApplicationDbSeeder
* Admin automático

Usuario:

[admin@ofipro.com](mailto:admin@ofipro.com)

## Decisiones

D013

El primer administrador se crea mediante seed automático al iniciar la aplicación.

Razón:

* Facilita pruebas.
* Evita inserciones manuales.
* Reutilizable para futuros catálogos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Pendiente

## Próximo paso

Iniciar Bloque 4 - Proyectos.

# =====================================

# SESIÓN 2026-06-15

## Objetivo

Completar Bloque 4 - Proyectos.

# =====================================

## Seeder

Completado:

* SeedCategoriesAsync
* Categorías iniciales
* Subcategorías iniciales

## Repositorio

Completado:

* IProjectRepository
* ProjectRepository

Métodos:

* GetByIdAsync
* GetAllAsync
* GetByUserIdAsync
* AddAsync
* UpdateAsync
* UpdateRequirementsAsync

## Servicios

Completado:

* IProjectService
* ProjectService

Casos de uso:

* CreateAsync
* GetByIdAsync
* GetAllAsync
* GetMyProjectsAsync
* UpdateAsync
* UpdateRequirementsAsync
* DeleteAsync

## Controller

Completado:

* ProjectsController

Endpoints:

* POST /api/projects
* GET /api/projects
* GET /api/projects/my-projects
* GET /api/projects/{id}
* PUT /api/projects/{id}
* PUT /api/projects/{id}/requirements
* DELETE /api/projects/{id}

## Pruebas

POST /api/projects → 200

GET /api/projects → 200

GET /api/projects/my-projects → 200

GET /api/projects/{id} → 200

PUT /api/projects/{id} → 200

PUT /api/projects/{id}/requirements → 200

DELETE /api/projects/{id} → 200

## Decisiones

D014

ProjectRequirements se administran mediante endpoint independiente.

Razón:

* Responsabilidad separada.
* Arquitectura consistente.
* Mejor escalabilidad.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Pendiente

## Próximo paso

Iniciar Bloque 5 - Propuestas.


# =====================================

# SESIÓN 2026-06-16

## Objetivo

Completar Bloque 5 - Propuestas.

# =====================================

## Corrección de modelo

Completado:

* Proposal ahora se relaciona con ProjectRequirement.
* Se eliminó la relación obsoleta Proposal → Project.
* Se agregó navegación ProjectRequirement → Proposals.
* Se generó migración correctiva.

Problema detectado:

* FK_Proposals_Projects_ProjectId seguía existiendo.

Solución:

* Eliminar ProjectId de Proposal.
* Corregir ProposalConfiguration.
* Crear migración RemoveOldProjectRelationFromProposal.

## DTOs

Completado:

* CreateProposalDto
* UpdateProposalDto
* ProposalDto

## Repositorio

Completado:

* IProposalRepository
* ProposalRepository

## Servicio

Completado:

* IProposalService
* ProposalService

Reglas implementadas:

* Un contratista solo puede tener una propuesta activa por ProjectRequirement.
* Una propuesta retirada puede volver a crearse.
* Solo propuestas pendientes pueden modificarse.
* Solo propuestas pendientes pueden retirarse.

## Controller

Completado:

* ProposalsController

Endpoints:

* POST /api/proposals
* PUT /api/proposals/{id}
* GET /api/proposals/my-proposals
* GET /api/proposals/{id}
* GET /api/proposals/requirement/{id}
* PATCH /api/proposals/{id}/accept
* PATCH /api/proposals/{id}/reject
* PATCH /api/proposals/{id}/withdraw

## Pruebas

POST /api/proposals → 200

GET /api/proposals/my-proposals → 200

GET /api/proposals/{id} → 200

PUT /api/proposals/{id} → 200

PATCH withdraw → 200

PATCH accept → 200

PATCH reject → 200

## Regla D016 validada

Prueba realizada:

* Contratista A crea propuesta.
* Contratista B crea propuesta.
* Cliente acepta propuesta A.

Resultado:

* Propuesta A → Aceptada.
* Propuesta B → Rechazada.

Funcionamiento correcto.

## Refactor técnico

Completado:

* ProposalRepository.UpdateAsync → SaveChangesAsync
* ProjectRepository.UpdateAsync → SaveChangesAsync

Se conserva:

* UserRepository.UpdateAsync(User user)

porque realiza una actualización explícita.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 6 - Contrataciones → Pendiente

## Próximo paso

Iniciar Bloque 6 - Contrataciones.


# =====================================

# SESIÓN 2026-06-17

## Objetivo

Completar Bloque 5.5 - Seguridad y Calidad Base.

# =====================================

## Seguridad JWT

Completado:

* JWT Key removida de appsettings.json.
* JWT Key configurada mediante User Secrets.
* Login validado después del cambio.

## Autorización de propuestas

Completado:

* AcceptAsync ahora recibe el UserId del usuario autenticado.
* RejectAsync ahora recibe el UserId del usuario autenticado.
* Se valida Project.CreatedByUserId contra el UserId del token.
* Solo el propietario del proyecto puede aceptar propuestas.
* Solo el propietario del proyecto puede rechazar propuestas.

## Validaciones DTO

Completado:

* Validaciones agregadas en CreateProjectDto.
* Validaciones agregadas en CreateProjectRequirementDto.
* Validaciones agregadas en CreateProposalDto.
* Se validan textos requeridos.
* Se validan longitudes máximas.
* Se valida que Price no acepte valores negativos o cero.

## Excepciones

Completado:

* NotFoundException.
* ForbiddenException.
* BadRequestException.
* ExceptionMiddleware.
* Registro del middleware en Program.cs.

## Pruebas

JWT:

* Login → 200

Validaciones:

* Propuesta con precio negativo → 400

Excepciones:

* Propuesta inexistente → 404
* Propuesta no pendiente → 400
* Usuario no propietario intentando rechazar propuesta → 403

Autorización:

* Usuario no propietario no pudo aceptar/rechazar propuesta.
* Usuario propietario sí pudo aceptar/rechazar propuesta.

## Decisiones

D018

Los secretos no deben guardarse en appsettings.json ni en el repositorio.

D019

Aceptar o rechazar propuestas requiere validar al propietario del proyecto.

D020

Las pruebas que requieran IDs se apoyarán en consultas directas a base de datos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API.

## Próximo paso

Iniciar Bloque 5.6 - Limpieza de Consistencia API.

# =====================================

# SESIÓN 2026-06-17 (PARTE 2)

## Objetivo

Completar Bloque 5.6 - Limpieza de Consistencia API.

# =====================================

## Excepciones

Completado:

* Reemplazo de Exception genérica en servicios.
* Implementación consistente de:
  * BadRequestException
  * ForbiddenException
  * NotFoundException

Pruebas:

* Login inválido → 400
* Proyecto inexistente → 404
* Proyecto ajeno → 403
* Propuesta ajena → 403

## AuthController

Completado:

* Eliminación de try/catch redundantes.
* Delegación total al ExceptionMiddleware.

Resultado:

Formato estándar unificado en toda la API.

## DTOs Update

Completado:

* UpdateProjectDto
* UpdateProjectRequirementsDto
* UpdateProposalDto
* UpdateUserProfileDto

Pruebas:

* Título vacío → 400
* Precio negativo → 400

## Soft Delete

Validado:

* ProjectRepository.GetAllAsync ya excluye DeletedAt != null.

No se requirió cambio.

## JWT

Completado:

* Expiración sincronizada entre configuración y respuesta de login.

## TestController

Completado:

* Restricción a rol Administrador.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Pendiente

## Próximo paso

Iniciar Bloque 6 - Contrataciones.

# =====================================

# SESIÓN 2026-06-18

## Objetivo

Iniciar y completar funcionalmente Bloque 6 - Contrataciones.

# =====================================

## Núcleo de contratación

Completado:

* ContractStatus.
* Entidad Contract.
* ContractConfiguration.
* Tabla Contracts en SQL Server.
* IContractRepository.
* ContractRepository.
* IContractService.
* ContractService.
* ContractsController.

## Flujo automático

Completado:

* Al aceptar una propuesta se crea automáticamente una contratación.
* La contratación inicia en estado PendienteInicio.
* Se guarda ProposalId.
* Se guarda ProjectRequirementId.
* Se guarda ClientUserId.
* Se guarda ContractorUserId.
* Se guarda AgreedPrice.
* Se guarda EstimatedTime.

## Endpoints creados

* GET /api/contracts/mine
* GET /api/contracts/{id}
* PATCH /api/contracts/{id}/status

## Estados implementados

* PendienteInicio
* EnProceso
* PendienteConfirmacion
* Finalizado
* Cancelado

## Reglas implementadas

* Solo el contratista puede iniciar una contratación.
* Solo el contratista puede marcar una contratación como pendiente de confirmación.
* Solo el cliente puede finalizar una contratación.
* Cliente o contratista pueden cancelar una contratación activa.
* Una contratación finalizada no puede cambiar de estado.
* Una contratación cancelada no puede cambiar de estado.

## Pruebas realizadas

* Aceptar propuesta crea contratación automáticamente → 200
* GET /api/contracts/mine → 200
* GET /api/contracts/{id} → 200
* PendienteInicio → EnProceso → 200
* EnProceso → PendienteConfirmacion → 200
* PendienteConfirmacion → Finalizado → 200
* Cancelación de contratación → 200
* Intentar mover contratación cancelada → 400

## Decisiones

D024

Los DTOs de respuesta deben usar identificadores descriptivos para facilitar pruebas y consumo desde frontend.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo funcionalmente

## Pendiente técnico

Realizar Bloque 6.8 - Refactor de nombres descriptivos en DTOs.

## Próximo paso

Iniciar Bloque 6.8 - Refactor de nombres descriptivos en DTOs antes de comenzar Bloque 7 - Evidencias.

# =====================================

# SESIÓN 2026-06-19

## Objetivo

Cerrar pendientes derivados del Bloque 6 antes de iniciar Bloque 7 - Evidencias.

# =====================================

## Bloque 6.8 - Refactor de nombres descriptivos en DTOs

Completado:

* UserProfileDto cambió Id por UserId.
* ProjectDto cambió Id por ProjectId.
* ProjectRequirementDto cambió Id por ProjectRequirementId.
* ProposalDto cambió Id por ProposalId.
* ContractDto cambió Id por ContractId.
* Se actualizaron los mapeos correspondientes.

Pruebas:

* GET /api/projects → 200
* GET /api/proposals/my-proposals → 200
* GET /api/contracts/mine → 200
* GET /api/users/profile → 200

Resultado:

Las respuestas de API ahora son más claras y fáciles de depurar.

## Bloque 6.9 - Flujo mínimo de Contratista

Completado:

* Se agregaron métodos HasRoleAsync y AddRoleAsync en UserRepository.
* Se agregó validación de rol Contratista en ProposalService.CreateAsync.
* Se creó endpoint PATCH /api/users/activate-contractor.
* Un usuario Cliente puede activar también el rol Contratista.
* Se validó que solo contratistas puedan enviar propuestas.

Pruebas:

* Cliente sin rol Contratista intentando crear propuesta → 403
* Usuario con rol Contratista intentando crear propuesta → pasa validación de rol
* Activación de contratista → 200
* Verificación en BD muestra Role 1 y Role 2 para el usuario activado

Resultado:

El flujo mínimo Cliente → Contratista quedó implementado.

## Bloque 6.10 - Orden de interfaces Application

Completado:

* Interfaces de repositorios movidas a Interfaces/Repositories.
* Interfaces de servicios movidas a Interfaces/Services.
* Visual Studio actualizó namespaces y referencias.
* La solución compiló correctamente.
* Pruebas básicas posteriores correctas.

Resultado:

La capa Application queda más ordenada antes de continuar con nuevos módulos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

## Pendiente

Iniciar Bloque 7 - Evidencias.

## Próximo paso

Crear el módulo de Evidencias asociado a contrataciones.

# =====================================

# SESIÓN 2026-06-20

## Objetivo

Resolver diagnóstico posterior al Bloque 6 antes de iniciar Bloque 7 - Evidencias.

# =====================================

## Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7

Completado:

* Se revisó diagnóstico del estado actual del backend.
* Se identificó un filtro redundante en UserService.GetAllAsync.
* Se eliminó el filtro DeletedAt == null del servicio porque el repositorio ya filtra usuarios eliminados.
* Se detectó que GET /api/proposals/requirement/{projectRequirementId} permitía consultar propuestas a cualquier usuario autenticado.
* Se decidió que solo el dueño del proyecto puede consultar las propuestas de un requerimiento.
* Se agregó validación de propietario antes de devolver propuestas.
* Se agregó GetProjectRequirementOwnerUserIdAsync en IProposalRepository.
* Se implementó GetProjectRequirementOwnerUserIdAsync en ProposalRepository.
* Se actualizó ProposalService.GetByProjectRequirementAsync para recibir userId y projectRequirementId.
* Se actualizó ProposalsController para enviar el userId autenticado al servicio.

## Razón del ajuste

El endpoint de propuestas por requerimiento podía exponer información sensible de competencia entre contratistas, como precio, tiempos estimados, garantías y descripción de la propuesta.

El comportamiento correcto para el modelo de OfiPro es que solo el cliente dueño del proyecto pueda ver todas las propuestas recibidas.

## Resultado

El endpoint GET /api/proposals/requirement/{projectRequirementId} quedó protegido.

Comportamiento validado:

* Dueño del proyecto → 200 OK.
* Usuario no dueño → 403 Forbidden.
* Requerimiento inexistente → 404 Not Found.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

## Pendiente inmediato

Antes de iniciar Bloque 7 - Evidencias, revisar duda conceptual surgida del Bloque 6.

## Próximo paso

Conversar y resolver la duda puntual relacionada con Bloque 6 antes de continuar con Evidencias.


# =====================================

# SESIÓN 2026-06-20

## Objetivo

Iniciar y completar Bloque 7 - Evidencias V1.

# =====================================

## Decisión estratégica previa

Antes de iniciar Evidencias se discutió la orientación futura de plataforma.

Conclusión:

OfiPro será pensado como plataforma mobile-first, pero no se tomará el camino PWA.

Ruta acordada:

* Terminar backend/API correctamente.
* Crear web responsiva necesaria.
* En una etapa posterior, desarrollar app móvil real.
* Evitar PWA como paso intermedio para no consumir recursos en una ruta que no se considera prioritaria.

Razón:

El usuario final probablemente usará OfiPro desde celular, especialmente para evidencias, seguimiento, ubicación, notificaciones y operación en campo. Sin embargo, abrir el frente móvil ahora sería prematuro. Primero se debe consolidar el backend.

# =====================================

## Bloque 7 - Evidencias V1

Completado:

* Se creó la entidad Evidence en Domain.
* Se agregó DbSet<Evidence> en ApplicationDbContext.
* Se creó EvidenceConfiguration.
* Se generó migración AddEvidences.
* Se actualizó la base de datos.
* Se creó EvidenceDto.
* Se creó CreateEvidenceDto.
* Se creó IEvidenceRepository.
* Se implementó EvidenceRepository.
* Se creó IEvidenceService.
* Se implementó EvidenceService.
* Se registraron IEvidenceRepository y IEvidenceService en Program.cs.
* Se creó EvidencesController.

Endpoints creados:

* POST /api/contracts/{contractId}/evidences
* GET /api/contracts/{contractId}/evidences
* DELETE /api/evidences/{evidenceId}

Reglas implementadas:

* Solo el contratista de la contratación puede subir evidencias.
* Cliente y contratista pueden consultar evidencias del contrato.
* Usuarios ajenos no pueden consultar evidencias.
* Cliente no puede subir evidencias.
* Las evidencias no se suben a contratos finalizados o cancelados.
* Las evidencias no se eliminan si el contrato está finalizado o cancelado.
* Solo el usuario que subió la evidencia puede eliminarla.
* La eliminación se realiza mediante soft delete.

Pruebas realizadas:

* Se creó un contrato limpio desde flujo real:

  * Cliente crea proyecto.
  * Contratista envía propuesta.
  * Cliente acepta propuesta.
  * Sistema crea contrato automáticamente.
* Contratista subió evidencia correctamente.
* Evidencia apareció en tabla Evidences.
* Contratista consultó evidencias correctamente.
* Cliente dueño consultó evidencias correctamente.
* Cliente intentó subir evidencia y recibió 403.
* Contratista eliminó evidencia correctamente.
* DeletedAt se asignó en BD.
* Evidencia eliminada ya no apareció en consultas.

Resultado:

Bloque 7 - Evidencias V1 quedó completado funcionalmente.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

## Pendiente

Definir siguiente bloque.

Opciones posibles:

* Calificaciones y reputación.
* Perfil profesional del contratista.
* Carga real de archivos para evidencias.
* Mejoras de flujo de contratación.

## Observación

El flujo principal de OfiPro ya conecta:

Proyecto → Requerimiento → Propuesta → Contrato → Evidencia

Esto representa un avance importante hacia un MVP funcional real.
